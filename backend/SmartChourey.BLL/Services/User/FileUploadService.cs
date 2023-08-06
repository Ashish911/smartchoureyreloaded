using BusinessLogicLayer.Configuration;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.DAL;
using SmartChourey.DAL.Repositories.Interfaces;


namespace SmartChourey.BLL.Services.User
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IRepository<FileUpload> _fileUploadRepository;
        private readonly IChangeLogService<FileUpload> _changeLogService;

        public FileUploadService(IRepository<FileUpload> fileUploadRepository, IChangeLogService<FileUpload> changeLogService)
        {
            _fileUploadRepository = fileUploadRepository;
            _changeLogService = changeLogService;
        }

        public async Task<bool> DeleteFile(long fileUploadId, string userName)
        {
            bool isSuccess = false;
            try
            {
                var fileUploadObject = _fileUploadRepository.Table.Where(x => x.FileUploadId == fileUploadId).FirstOrDefault();
                if (fileUploadObject != null)
                {
                    await _fileUploadRepository.Remove(fileUploadObject);

                    var category = fileUploadObject.Ecategory == null ? EnumHelpers.ECategory.ChoureyOne : (EnumHelpers.ECategory)fileUploadObject.Ecategory;
                    await _changeLogService.Insert(fileUploadObject.FileUploadId
                        , EnumHelpers.EChangeType.Deleted, category, EnumHelpers.EChangeCategory.File
                        , userName, fileUploadObject.SiteId);

                    isSuccess = true;
                }
            } catch(Exception ex)
            {
                return isSuccess;
            }

            return isSuccess;
        }

        public async Task<bool> DeleteFiles(List<FileUpload> fileUploads)
        {
            try
            {
                await _fileUploadRepository.RemoveRange(fileUploads);

                return true;
            } catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<IList<FileUpload>> GetUploadedFiles(long categoryId, EnumHelpers.ECategory category, string siteId, EnumHelpers.EViewMode viewMode)
        {
            var fileUploads = _fileUploadRepository.Table.Where(x => x.SiteId == siteId && x.Ecategory == (int)category && x.CategoryId == categoryId && x.Estatus == (int)EnumHelpers.EStatus.Active && x.ViewMode == (int)viewMode);
            if (viewMode == EnumHelpers.EViewMode.Demo)
            {
                fileUploads = _fileUploadRepository.Table.Where(x => x.Estatus == (int)EnumHelpers.EStatus.Active && x.ViewMode == (int)viewMode);
            }
            return fileUploads.ToList();
        }

        public async Task<FileUpload> GetUploadedFile(long fileUploadId, long categoryId, EnumHelpers.ECategory category, string siteId)
        {
            var fileUpload = _fileUploadRepository.Table.Where(x => x.FileUploadId == fileUploadId &&  x.SiteId == siteId && x.Ecategory == (int)category && x.CategoryId == categoryId).FirstOrDefault();
            
            return fileUpload;
        }

        public async Task<bool> InsertFile(FileUpload fileUpload, string createdByEmail)
        {
            bool isSuccess = false;
            try
            {
                await _fileUploadRepository.Add(fileUpload);
                var category = fileUpload.Ecategory == null ? EnumHelpers.ECategory.ChoureyOne : (EnumHelpers.ECategory)fileUpload.Ecategory;

                await _changeLogService.Insert(fileUpload.FileUploadId
                    , EnumHelpers.EChangeType.Added, category, EnumHelpers.EChangeCategory.File
                    , createdByEmail, fileUpload.SiteId);

                isSuccess = true;
            } catch (Exception ex)
            {
                return isSuccess;
            }

            return isSuccess;

        }
    }
}
