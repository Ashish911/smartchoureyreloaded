using Dapper;
using Microsoft.AspNetCore.Http;
using SmartChourey.BLL.Configuration;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.ViewModel.SuperAdmin;
using SmartChourey.BLL.ViewModel.User;
using SmartChourey.DAL;
using SmartChourey.DAL.Repositories.Interfaces;
using System.Data;

namespace SmartChourey.BLL.Services.User
{
    public class PublicUserBoardService: IPublicUserBoardService
    {
        private readonly IRepository<PublicUserboardInformation> _publicUserboardInformationRepository;
        private readonly IRepository<ErrorMessageInformation> _errorMessageInformationRepository;
        private readonly IRepository<PublicUserboardPhotoInformation> _publicUserboardPhotoInformationRepository;
        private readonly IConnectionFactory _connectionFactory;

        public PublicUserBoardService(IRepository<PublicUserboardInformation> publicUserboardInformationRepository, 
            IRepository<ErrorMessageInformation> errorMessageInformationRepository,
            IRepository<PublicUserboardPhotoInformation> publicUserboardPhotoInformationRepository,
            IConnectionFactory connectionFactory)
        {
            _publicUserboardInformationRepository = publicUserboardInformationRepository;
            _errorMessageInformationRepository = errorMessageInformationRepository;
            _publicUserboardPhotoInformationRepository = publicUserboardPhotoInformationRepository;
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> Delete(int publicUserboardId)
        {
            try
            {
                var publicUserboardInformation = _publicUserboardInformationRepository.Table.Where(x => x.PublicUserboardId == publicUserboardId).FirstOrDefault();
                if (publicUserboardInformation != null)
                {
                    bool isSuccessDeleteGallery = await DeletePhoto(publicUserboardInformation.PublicUserboardId);
                    if(isSuccessDeleteGallery)
                    {
                        await _publicUserboardInformationRepository.Remove(publicUserboardInformation);
                    } else
                    {
                        return false;
                    }
                } else
                {
                    throw new DataException("Public Userboard Information not found");
                }

                return true;
            } 
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<bool> DeletePhoto(long id)
        {
            var publicUserboardPhotoInformations = _publicUserboardPhotoInformationRepository.Table.Where(x => x.PublicUserboardInformationId == id).ToList();
            if (publicUserboardPhotoInformations.Count == 0)
            {
                return true;
            }

            if(publicUserboardPhotoInformations.Any())
            {
                await _publicUserboardPhotoInformationRepository.RemoveRange(publicUserboardPhotoInformations);
                return true;
            }

            return false;
        }

        public async Task<PublicUserBoardViewModel> InsertPhoto(PublicUserBoardViewModel publicUserBoardViewModel)
        {
            try
            {
                var publicUserboardPhotoInformation = new PublicUserboardPhotoInformation();
                publicUserboardPhotoInformation.CreatedBy = publicUserBoardViewModel.CurrentUserId;
                publicUserboardPhotoInformation.CreatedOn = new Utilities().GetTokyoDate();
                publicUserboardPhotoInformation.PublicUserboardInformationId = (int)publicUserBoardViewModel.PublicUserboardId;
                publicUserboardPhotoInformation.FileName = publicUserBoardViewModel.FileName;
                publicUserboardPhotoInformation.FilePath = publicUserBoardViewModel.FilePath;
                publicUserboardPhotoInformation.IsActive = publicUserBoardViewModel.IsActive;
                publicUserboardPhotoInformation.PhotoName = publicUserBoardViewModel.PhotoName;

                await _publicUserboardPhotoInformationRepository.Add(publicUserboardPhotoInformation);

                publicUserBoardViewModel.IsSuccessful = true;
                publicUserBoardViewModel.Message = "Successfully Inserted";
            } 
            catch (Exception ex)
            {
                publicUserBoardViewModel.IsSuccessful = false;
                publicUserBoardViewModel.Message = "Failed to Insert";
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new Utilities().GetTokyoDate();
                entity.ErrorMessage = ex.Message;
                entity.FileName = "BAL:Implementation:SuperAdmin:PublicUserBoardService";
                entity.MethodName = "InsertPhoto";
                entity.OperationDoneBy = publicUserBoardViewModel.UserName;
                entity.Status = true;
                await _errorMessageInformationRepository.Add(entity);
            }

            return publicUserBoardViewModel;
        }

        public async Task<PublicUserBoardViewModel> InsertUpdate(PublicUserBoardViewModel publicUserBoardViewModel)
        {
            try
            {
                if (publicUserBoardViewModel.PublicUserboardId != 0 && publicUserBoardViewModel.PublicUserboardId != null)
                {
                    var publicUserBoardInformation = _publicUserboardInformationRepository.Table.Where(x => x.PublicUserboardId == publicUserBoardViewModel.PublicUserboardId).FirstOrDefault();
                    if (publicUserBoardInformation != null)
                    {
                        publicUserBoardInformation.Description = publicUserBoardViewModel.Description;
                        publicUserBoardInformation.IsActive = publicUserBoardViewModel.IsActive;
                        publicUserBoardInformation.Title = publicUserBoardViewModel.Title;
                        publicUserBoardInformation.UpdatedBy = publicUserBoardViewModel.CurrentUserId;
                        publicUserBoardInformation.UpdatedOn = new Utilities().GetTokyoDate();

                        publicUserBoardViewModel.IsSuccessful = true;
                        publicUserBoardViewModel.Message = "Successfully Updated";
                    }
                }
                else
                {
                    var publicUserboardInformation = new PublicUserboardInformation();
                    publicUserboardInformation.CreatedBy = publicUserBoardViewModel.CurrentUserId;
                    publicUserboardInformation.CreatedOn = new Utilities().GetTokyoDate();
                    publicUserboardInformation.Description = publicUserBoardViewModel.Description;
                    publicUserboardInformation.IsActive = publicUserBoardViewModel.IsActive;
                    publicUserboardInformation.Title = publicUserBoardViewModel.Title;

                    var createdPublicUserboardInformation = await _publicUserboardInformationRepository.Add(publicUserboardInformation);
                    publicUserBoardViewModel.PublicUserboardId = createdPublicUserboardInformation.PublicUserboardId;

                    publicUserBoardViewModel.IsSuccessful = true;
                    publicUserBoardViewModel.Message = "Successfully Inserted";
                }
            }
            catch (Exception ex)
            {
                publicUserBoardViewModel.IsSuccessful = false;
                if (publicUserBoardViewModel.PublicUserboardId != 0)
                {
                    publicUserBoardViewModel.Message = "Failed to Update";
                }
                else
                {
                    publicUserBoardViewModel.Message = "Failed to Insert";
                }
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new Utilities().GetTokyoDate();
                entity.ErrorMessage = ex.Message;
                entity.FileName = "BAL.Implementation.SuperAdmin";
                entity.MethodName = "InsertUpdate: " + publicUserBoardViewModel.PublicUserboardId;
                entity.OperationDoneBy = publicUserBoardViewModel.UserName;
                entity.Status = true;
                
                await _errorMessageInformationRepository.Add(entity);
            }

            return publicUserBoardViewModel;
        }

        public async Task<List<PublicUserBoardViewModel>> ListAll()
        {
            using var connection = _connectionFactory.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            IEnumerable<PublicUserBoardViewModel> models = await connection.QueryAsync<PublicUserBoardViewModel>("sp_publicUserBoardInformationList", commandType: CommandType.StoredProcedure);
            return models.ToList();
        }

        public async Task<PublicUserBoardViewModel> ProcessFiles(PublicUserBoardViewModel publicUserBoardViewModel, IList<IFormFile> files, string rootPath)
        {
            if (files.Count() > 0)
            {
                //Create Gallery
                var subPath = "PublicImage/User/" + publicUserBoardViewModel.PublicUserboardId;

                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        var exists = Directory.Exists(Path.Combine(rootPath, subPath));

                        if (!exists)
                        {
                            Directory.CreateDirectory(Path.Combine(rootPath, subPath));
                        }

                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Path.Combine(rootPath, subPath), fileName);
                        SaveFileToPath(file, path);
                        publicUserBoardViewModel.FileName = fileName;
                        publicUserBoardViewModel.FilePath = path;
                        publicUserBoardViewModel.PhotoName = fileName;
                        publicUserBoardViewModel.PublicUserboardId = publicUserBoardViewModel.PublicUserboardId;
                        
                        var result = await InsertPhoto(publicUserBoardViewModel);

                    }
                }
            }

            return publicUserBoardViewModel;
        }


        private async void SaveFileToPath(IFormFile file, string path)
        {
            try
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<string> DeletePhoto(int publicUserboardId, long photoId)
        {
            try
            {
                var publicUserboardPhotoInformation = _publicUserboardPhotoInformationRepository.Table.Where(x => x.PublicUserboardInformationId == publicUserboardId && x.PhotoId == photoId).FirstOrDefault();
                if (publicUserboardPhotoInformation != null)
                {
                    await _publicUserboardPhotoInformationRepository.Remove(publicUserboardPhotoInformation);
                    return publicUserboardPhotoInformation.FilePath;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
