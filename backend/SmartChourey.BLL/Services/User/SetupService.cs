using AutoMapper;
using BusinessLogicLayer.Configuration;
using Dapper;
using Microsoft.AspNetCore.Http;
using SmartChourey.BLL.Configuration;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.ViewModel;
using SmartChourey.BLL.ViewModel.Admin;
using SmartChourey.DAL;
using SmartChourey.DAL.Context;
using SmartChourey.DAL.Repositories.Interfaces;
using System.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data.Entity.Core;
using static BusinessLogicLayer.Configuration.EnumHelpers;
using Microsoft.AspNetCore.Identity;

namespace SmartChourey.BLL.Services.User
{
    public class SetupService : ISetupService
    {
        private readonly IRepository<ChoureyOneInformation> _choureyOneRepository;
        private readonly IRepository<ChoureyTwoInformation> _choureyTwoRepository;
        private readonly IRepository<ChoureyOnePhotoInformation> _choureyOnePhotoRepository;
        private readonly IRepository<ChoureyOneVideoInformation> _choureyOneVideoRepository;
        private readonly IRepository<ChoureyTwoPhotoInformation> _choureyTwoPhotoRepository;
        private readonly IRepository<ChoureyTwoVideoInformation> _choureyTwoVideoRepository;
        private readonly IRepository<ChoureyOneGalleryInformation> _choureyOneGalleryRepository;
        private readonly IRepository<ChoureyTwoGalleryInformation> _choureyTwoGalleryRepository;
        private readonly IRepository<DisasterPhotoInformation> _disasterPhotoRepository;
        private readonly IRepository<DisasterInformation> _disasterInformationRepository;
        private readonly IRepository<DisasterGalleryInformation> _disasterGalleryInformationRepository;
        private readonly IRepository<ErrorMessageInformation> _errorMessageInformationRepository;

        private readonly IConnectionFactory _connectionFactory;
        private static IHttpContextAccessor _httpContextAccessor;
        private readonly IHelpers _helpers;
        private readonly ApplicationDbContext _applicationDbContext;

        private readonly IChangeLogService<ChoureyOneInformation> _changeLogChoureyOneInformationService;
        private readonly IChangeLogService<ChoureyOneVideoInformation> _changeLogChoureyOneVideoInformationService;
        private readonly IChangeLogService<ChoureyOnePhotoInformation> _changeLogChoureyOnePhotoInformationService;

        private readonly IChangeLogService<ChoureyTwoInformation> _changeLogChoureyTwoInformationService;
        private readonly IChangeLogService<ChoureyTwoVideoInformation> _changeLogChoureyTwoVideoInformationService;
        private readonly IChangeLogService<ChoureyTwoPhotoInformation> _changeLogChoureyTwoPhotoInformationService;

        private readonly IChangeLogService<DisasterPhotoInformation> _changeLogDisasterPhotoInformationService;
        
        private readonly IChangeLogService<DisasterInformation> _changeLogDisasterInformationService;

        private readonly IFileUploadService _fileUploadService;

        private readonly ISiteSpaceDetailService _siteSpaceDetailService;
        private readonly ISiteSpaceAggregateService _siteSpaceAggregateService;

        public SetupService(
            IRepository<ChoureyOneInformation> choureyOneRepository,
            IRepository<ChoureyTwoInformation> choureyTwoRepository,
            IRepository<ChoureyOnePhotoInformation> choureyOnePhotoRepository,
            IRepository<ChoureyOneVideoInformation> choureyOneVideoRepository,
            IRepository<ChoureyTwoPhotoInformation> choureyTwoPhotoRepository,
            IRepository<ChoureyTwoVideoInformation> choureyTwoVideoRepository,
            IRepository<ChoureyOneGalleryInformation> choureyOneGalleryRepository,
            IRepository<ChoureyTwoGalleryInformation> choureyTwoGalleryRepository,
            IRepository<DisasterPhotoInformation> disasterPhotoRepository,
            IRepository<ErrorMessageInformation> errorMessageInformationRepository,
            IRepository<DisasterInformation> disasterInformationRepository,
            IRepository<DisasterGalleryInformation> disasterGalleryInformationRepository,
            IConnectionFactory connectionFactory,
            IHttpContextAccessor httpContextAccessor,
            IHelpers helpers,
            ApplicationDbContext applicationDbContext,
            IChangeLogService<ChoureyOneInformation> changeLogChoureyOneInformationService,
            IChangeLogService<ChoureyOneVideoInformation> changeLogChoureyOneVideoInformationService,
            IChangeLogService<ChoureyOnePhotoInformation> changeLogChoureyOnePhotoInformationService,
            IChangeLogService<ChoureyTwoInformation> changeLogChoureyTwoInformationService,
            IChangeLogService<ChoureyTwoVideoInformation> changeLogChoureyTwoVideoInformationService,
            IChangeLogService<ChoureyTwoPhotoInformation> changeLogChoureyTwoPhotoInformationService,
            IChangeLogService<DisasterInformation> changeLogDisasterInformationService,
            IChangeLogService<DisasterPhotoInformation> changeLogDisasterPhotoInformationService,
            IFileUploadService fileUploadService,
            ISiteSpaceDetailService siteSpaceDetailService,
            ISiteSpaceAggregateService siteSpaceAggregateService)
        {
            _choureyOneRepository = choureyOneRepository;
            _choureyTwoRepository = choureyTwoRepository;
            _choureyOnePhotoRepository = choureyOnePhotoRepository;
            _choureyOneVideoRepository = choureyOneVideoRepository;
            _choureyTwoPhotoRepository = choureyTwoPhotoRepository;
            _choureyTwoVideoRepository = choureyTwoVideoRepository;
            _choureyOneGalleryRepository = choureyOneGalleryRepository;
            _choureyTwoGalleryRepository = choureyTwoGalleryRepository;
            _disasterPhotoRepository = disasterPhotoRepository;
            _connectionFactory = connectionFactory;
            _httpContextAccessor = httpContextAccessor;
            _helpers = helpers;
            _applicationDbContext = applicationDbContext;
            _changeLogChoureyOneInformationService = changeLogChoureyOneInformationService;
            _changeLogChoureyOnePhotoInformationService = changeLogChoureyOnePhotoInformationService;
            _changeLogChoureyOneVideoInformationService = changeLogChoureyOneVideoInformationService;
            _changeLogChoureyTwoInformationService = changeLogChoureyTwoInformationService;
            _changeLogChoureyTwoPhotoInformationService = changeLogChoureyTwoPhotoInformationService;
            _changeLogChoureyTwoVideoInformationService = changeLogChoureyTwoVideoInformationService;
            _changeLogDisasterInformationService = changeLogDisasterInformationService;
            _errorMessageInformationRepository = errorMessageInformationRepository;
            _fileUploadService = fileUploadService;
            _siteSpaceDetailService = siteSpaceDetailService;
            _siteSpaceAggregateService = siteSpaceAggregateService;
            _disasterInformationRepository= disasterInformationRepository;
            _disasterGalleryInformationRepository= disasterGalleryInformationRepository;
            _changeLogDisasterPhotoInformationService = changeLogDisasterPhotoInformationService;
        }
        public async Task<List<ChoureyOneViewModel>> ListChoureyOne(string siteId, int viewMode)
        {
            using var connection = _connectionFactory.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@siteId", siteId);
            parameters.Add("@viewMode", viewMode);

            var result = await connection.QueryAsync<ChoureyOneViewModel>("sp_ChoureyOneInformationList", parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
        public async Task<ChoureyOneViewModel> ChoureyOneDetails(long choureyOneId, string siteId, int viewMode)
        {
            using var connection = _connectionFactory.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@siteId", siteId);
            parameters.Add("@viewMode", viewMode);
            IEnumerable<ChoureyOneViewModel> resultList = await connection.QueryAsync<ChoureyOneViewModel>("sp_ChoureyOneInformationList", parameters, commandType: CommandType.StoredProcedure);
            ChoureyOneViewModel choureyOne = resultList.Where(x => x.ChoureyOneId == choureyOneId).FirstOrDefault();

            DynamicParameters commentParameters = new DynamicParameters();  //make service later : todo
            commentParameters.Add("@choureyId", choureyOneId);
            commentParameters.Add("@categoryId", 1);   //use enum later : todo
            commentParameters.Add("@choureyMediaId", null);
            commentParameters.Add("@requestedBy", null);
            var comments = await connection.QueryAsync<ChoureyMediaGetDto>("sp_GetChoureyMediaComment", commentParameters, commandType: CommandType.StoredProcedure);

            if (choureyOne != null)
            {
                var listPhoto = new List<PhotoModel>();
                var listVideo = new List<VideoModel>();
                var databaseImageList = _choureyOnePhotoRepository.Table.Where(x => x.GalleryId == choureyOne.GalleryId).OrderByDescending(x => x.CreatedOn).ToList();
                if (databaseImageList.Count != 0)
                {
                    foreach (var item in databaseImageList)
                    {
                        var photoModel = new PhotoModel();
                        photoModel.PhotoId = item.PhotoId;
                        photoModel.PhotoName = item.PhotoName;
                        photoModel.FileName = item.FileName;
                        photoModel.FilePath = item.FilePath;
                        photoModel.CustomName = item.CustomName;
                        photoModel.Comments = comments.Where(x => x.ChoureyMediaId == item.PhotoId && x.EUploadType == (int)EnumHelpers.EUploadType.Image).ToList();
                        choureyOne.ImageList.Add(photoModel);
                    }
                }
                var databaseVideoList = _choureyOneVideoRepository.Table.Where(x => x.ChoureyOneId == choureyOneId).OrderByDescending(x => x.CreatedOn).ToList();
                if (databaseVideoList.Count != 0)
                {
                    foreach (var item in databaseVideoList)
                    {
                        var videoModel = new VideoModel();
                        videoModel.VideoId = item.VideoId;
                        videoModel.VideoFilePath = item.FilePath;
                        videoModel.VideoFileName = item.FileName;
                        videoModel.CustomName = item.CustomName;
                        videoModel.ThumbFileName = item.ThumbFileName;
                        videoModel.Comments = comments.Where(x => x.ChoureyMediaId == item.VideoId && x.EUploadType == (int)EnumHelpers.EUploadType.Video).ToList();
                        choureyOne.VideoList.Add(videoModel);
                    }
                }
            }

            var fileUploads = await _fileUploadService.GetUploadedFiles(choureyOneId, EnumHelpers.ECategory.ChoureyOne, siteId, _helpers.GetViewMode(_httpContextAccessor.HttpContext.Session.GetString("ViewMode")));
            choureyOne.FileList = fileUploads;

            if (choureyOne != null)
            {
                return choureyOne;
            }
            else
            {
                choureyOne.IsSuccessful = false;
                choureyOne.Message = "No record found";
                return choureyOne;
            }
        }
        public async Task<List<ChoureyTwoViewModel>> ListChoureyTwo(string siteId, int viewMode)
        {
            using var connection = _connectionFactory.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@siteId", siteId);
            parameters.Add("@viewMode", viewMode);

            var result = await connection.QueryAsync<ChoureyTwoViewModel>("sp_ChoureytwoInformationList", parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
        public async Task<ChoureyTwoViewModel> ChoureyTwoDetails(long choureyTwoId, string siteId, int viewMode)
        {
            using var connection = _connectionFactory.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@siteId", siteId);
            parameters.Add("@viewMode", viewMode);
            IEnumerable<ChoureyTwoViewModel> resultList = await connection.QueryAsync<ChoureyTwoViewModel>("sp_ChoureyTwoInformationList", parameters, commandType: CommandType.StoredProcedure);
            var x = resultList.FirstOrDefault();
            ChoureyTwoViewModel choureyTwo = resultList.Where(x => x.ChoureyTwoId == choureyTwoId).FirstOrDefault();

            DynamicParameters commentParameters = new DynamicParameters();  //make service later : todo
            commentParameters.Add("@choureyId", choureyTwoId);
            commentParameters.Add("@categoryId", 1);   //use enum later : todo
            commentParameters.Add("@choureyMediaId", null);
            commentParameters.Add("@requestedBy", null);
            var comments = await connection.QueryAsync<ChoureyMediaGetDto>("sp_GetChoureyMediaComment", commentParameters, commandType: CommandType.StoredProcedure);

            if (choureyTwo != null)
            {
                var listPhoto = new List<PhotoModel>();
                var listVideo = new List<VideoModel>();
                var databaseImageList = _choureyTwoPhotoRepository.Table.Where(x => x.GalleryId == choureyTwo.GalleryId).OrderByDescending(x => x.CreatedOn).ToList();
                if (databaseImageList.Count != 0)
                {
                    foreach (var item in databaseImageList)
                    {
                        var photoModel = new PhotoModel();
                        photoModel.PhotoId = item.PhotoId;
                        photoModel.PhotoName = item.PhotoName;
                        photoModel.FileName = item.FileName;
                        photoModel.FilePath = item.FilePath;
                        photoModel.CustomName = item.CustomName;
                        photoModel.Comments = comments.Where(x => x.ChoureyMediaId == item.PhotoId && x.EUploadType == (int)EnumHelpers.EUploadType.Image).ToList();
                        choureyTwo.ImageList.Add(photoModel);
                    }
                }
                var databaseVideoList = _choureyTwoVideoRepository.Table.Where(x => x.ChoureyTwoId == choureyTwoId).OrderByDescending(x => x.CreatedOn).ToList();
                if (databaseVideoList.Count != 0)
                {
                    foreach (var item in databaseVideoList)
                    {
                        var videoModel = new VideoModel();
                        videoModel.VideoId = item.VideoId;
                        videoModel.VideoFilePath = item.FilePath;
                        videoModel.VideoFileName = item.FileName;
                        videoModel.CustomName = item.CustomName;
                        videoModel.ThumbFileName = item.ThumbFileName;
                        videoModel.Comments = comments.Where(x => x.ChoureyMediaId == item.VideoId && x.EUploadType == (int)EnumHelpers.EUploadType.Video).ToList();
                        choureyTwo.VideoList.Add(videoModel);
                    }
                }
            }

            var fileUploads = await _fileUploadService.GetUploadedFiles(choureyTwoId, EnumHelpers.ECategory.ChoureyOne, siteId, _helpers.GetViewMode(_httpContextAccessor.HttpContext.Session.GetString("ViewMode")));
            choureyTwo.FileList = fileUploads;

            if (choureyTwo != null)
            {
                return choureyTwo;
            }
            else
            {
                choureyTwo.IsSuccessful = false;
                choureyTwo.Message = "No record found";
                return choureyTwo;
            }
        }
        public async Task<ResponseViewModel> DeleteChourey(ChoureyDeleteModel model)
        {
            ResponseViewModel retVal = new ResponseViewModel();

            if (model.IsChoureyOne)
            {
                var checkData = _choureyOneRepository.Table.Where(x => x.SiteId == model.SiteId && x.ChoureyOneId == model.ChoureyId).FirstOrDefault();

                if (checkData != null)
                {
                    try
                    {
                        //Delete Gallery and Photo  and Video First
                        bool successVideo = await DeleteVideo(model);
                        if (successVideo)
                        {
                            bool successDeleteGallery = await DeleteGallery(model);
                            if (successDeleteGallery)
                            {

                                await _choureyOneRepository.Remove(checkData);
                                retVal.IsSuccessful = true;
                                retVal.Message = "Deleted Successfully";
                            }
                        }
                    }
                    catch
                    {
                        retVal.IsSuccessful = false;
                        retVal.Message = "Error while deleting";
                    }
                }
            }
            else
            {
                var checkData = _choureyTwoRepository.Table.Where(x => x.SiteId == model.SiteId && x.ChoureyTwoId == model.ChoureyId).FirstOrDefault();

                if (checkData != null)
                {
                    try
                    {
                        //Delete Gallery and Photo  and Video First
                        bool successVideo = await DeleteVideo(model);
                        if (successVideo)
                        {
                            bool successDeleteGallery = await DeleteGallery(model);
                            if (successDeleteGallery)
                            {

                                await _choureyTwoRepository.Remove(checkData);
                                retVal.IsSuccessful = true;
                                retVal.Message = "Deleted Successfully";
                            }
                        }
                    }
                    catch
                    {
                        retVal.IsSuccessful = false;
                        retVal.Message = "Error while deleting";
                    }
                }
            }

            return retVal;
        }
        public async Task<List<DisasterViewModel>> ListDisaster(string siteId, int viewMode)
        {
            using var connection = _connectionFactory.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@siteId", siteId);
            parameters.Add("@viewMode", viewMode);
            var result = await connection.QueryAsync<DisasterViewModel>("sp_DisasterInfromationList", parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
        public async Task<DisasterViewModel> GetDisasterDetailsById(long disasterId, string siteId, int viewMode)
        {
            using var connection = _connectionFactory.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@siteId", siteId);
            parameters.Add("@viewMode", viewMode);
            IEnumerable<DisasterViewModel> disasters = await connection.QueryAsync<DisasterViewModel>("sp_DisasterInfromationList", parameters, commandType: CommandType.StoredProcedure);
            DisasterViewModel disaster = disasters.FirstOrDefault(x => x.DisasterId == disasterId);

            DynamicParameters commentParameters = new DynamicParameters();  //make service later : todo
            commentParameters.Add("@choureyId", disasterId);
            commentParameters.Add("@categoryId", 3);   //use enum later : todo
            commentParameters.Add("@choureyMediaId", null);
            commentParameters.Add("@requestedBy", null);
            var comments = await connection.QueryAsync<ChoureyMediaGetDto>("sp_GetChoureyMediaComment", commentParameters, commandType: CommandType.StoredProcedure);

            if (disaster != null)
            {
                var listPhoto = new List<PhotoModel>();
                var listVideo = new List<VideoModel>();
                var databaseImageList = _disasterPhotoRepository.Table.Where(x => x.GalleryId == disaster.GalleryId).OrderByDescending(x => x.CreatedOn).ToList();
                if (databaseImageList.Count != 0)
                {
                    foreach (var item in databaseImageList)
                    {
                        var photoModel = new PhotoModel();
                        photoModel.PhotoId = item.PhotoId;
                        photoModel.PhotoName = item.PhotoName;
                        photoModel.FileName = item.FileName;
                        photoModel.FilePath = item.FilePath;
                        photoModel.CustomName = item.CustomName;
                        photoModel.Comments = comments.Where(x => x.ChoureyMediaId == item.PhotoId && x.EUploadType == (int)EnumHelpers.EUploadType.Image).ToList();
                        disaster.ImageList.Add(photoModel);
                    }
                }

                var fileUploads = await _fileUploadService.GetUploadedFiles(disasterId, EnumHelpers.ECategory.Disaster, siteId, _helpers.GetViewMode(_httpContextAccessor.HttpContext.Session.GetString("ViewMode")));
                disaster.FileList = fileUploads;
            }
            return disaster;
        }

        #region choureyOneInsertUpdate Operations
        public async Task<float> ProcessVideos(ChoureyOneViewModel choureyOneViewModel, IList<IFormFile> videos, IList<IFormFile> thumbs, float addedSpace, IList<Task> uploadTasks, string rootPath)
        {
            if (videos != null)
            {
                if (videos.Count() > 0 && videos[0] != null)
                {
                    //Create Gallery
                    var videoSubPath = "ChoureyOneVideo/Video/" + choureyOneViewModel.SiteId + "/" + choureyOneViewModel.ChoureyOneId;
                    var videoFilePath = "";
                    var thumbSubPath = "ChoureyOneVideo/Thumbnail/" + choureyOneViewModel.SiteId + "/" + choureyOneViewModel.ChoureyOneId;

                    bool exists = Directory.Exists(Path.Combine(rootPath, videoSubPath));

                    if (!exists)
                    {
                        Directory.CreateDirectory(Path.Combine(rootPath, videoSubPath));
                    }

                    bool existsThumb = Directory.Exists(Path.Combine(rootPath, thumbSubPath));
                    if (!existsThumb)
                    {
                        Directory.CreateDirectory(Path.Combine(rootPath, thumbSubPath));
                    }

                    foreach (var video in videos)
                    {
                        var contentLength = video.Length;
                        var guid = Guid.NewGuid();

                        if (video.Length > 0)
                        {
                            if (thumbs != null)
                            {
                                if (thumbs.Count() > 0 && thumbs[0] != null)
                                {
                                    var thumbnail = thumbs[0];
                                    if (thumbnail.Length > 0)
                                    {
                                        var thumbFileName = guid + Path.GetExtension(thumbnail.FileName);
                                        var thumbFilePath = Path.Combine(Path.Combine(rootPath, thumbSubPath), thumbFileName);
                                        uploadTasks.Add(Task.Run(() => SaveFileToPath(thumbnail, thumbFilePath)));
                                        contentLength += thumbnail.Length;
                                        choureyOneViewModel.ThumbFileName = thumbFileName;
                                    }
                                }
                            }

                            var fileName = guid + Path.GetExtension(video.FileName);
                            videoFilePath = Path.Combine(Path.Combine(rootPath, videoSubPath), fileName);
                            choureyOneViewModel.FileName = fileName;
                            choureyOneViewModel.FilePath = videoFilePath;
                            uploadTasks.Add(Task.Run(() => SaveFileToPath(video, videoFilePath)));
                            var choureyOneVideoInformation = await InsertChoureyOneVideoFileAsync(choureyOneViewModel);

                            var siteSpaceDetail = new SiteSpaceDetail();
                            siteSpaceDetail.Ecategory = (int)EnumHelpers.ECategory.ChoureyOne;
                            siteSpaceDetail.CategoryId = (long)choureyOneViewModel.ChoureyOneId;
                            siteSpaceDetail.CategoryDetailId = (long)choureyOneVideoInformation.VideoId;
                            siteSpaceDetail.Estatus = (int)EnumHelpers.EStatus.Active;
                            siteSpaceDetail.EuploadType = (int)EnumHelpers.EUploadType.Video;
                            siteSpaceDetail.SiteId = choureyOneViewModel.SiteId;
                            siteSpaceDetail.UsedSpaceInMb = contentLength / 1000000.0f;
                            addedSpace += siteSpaceDetail.UsedSpaceInMb;
                            await _siteSpaceDetailService.Insert(siteSpaceDetail);
                        }
                    }
                }
            }

            return addedSpace;
        }

        public async Task<float> ProcessFiles(ChoureyOneViewModel choureyOneViewModel, IList<IFormFile> files, float addedSpace, IList<Task> uploadTasks, string rootPath)
        {
            if (files != null)
            {
                if (files.Count() > 0 && files[0] != null)
                {
                    choureyOneViewModel.GalleyName = choureyOneViewModel.Title;
                    choureyOneViewModel.IsActive = choureyOneViewModel.IsActive;

                    var choureyOneGalleryInformation = await InsertUpdateChoureyOneGalleryAsync(choureyOneViewModel);

                    var subPath = "Gallery/ChoureyOne/" + choureyOneViewModel.SiteId + "/" + choureyOneGalleryInformation.GalleryId;

                    foreach (var file in files)
                    {
                        if (file != null)
                        {
                            var contentLength = file.Length;
                            if (contentLength > 0)
                            {
                                bool exists = Directory.Exists(Path.Combine(rootPath, subPath));

                                if (!exists)
                                {
                                    Directory.CreateDirectory(Path.Combine(rootPath, subPath));
                                }

                                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                                var pathWithFileName = Path.Combine(Path.Combine(rootPath, subPath), fileName);
                                uploadTasks.Add(Task.Run(() => SaveFileToPath(file, pathWithFileName)));

                                choureyOneViewModel.FileName = fileName;
                                choureyOneViewModel.FilePath = pathWithFileName;
                                choureyOneViewModel.PhotoName = Path.GetFileName(file.FileName);
                                choureyOneViewModel.ChoureyOneId = choureyOneViewModel.ChoureyOneId;
                                choureyOneViewModel.GalleryId = choureyOneGalleryInformation.GalleryId;
                                var choureyOnePhotoInformaton = await InsertChoureyOnePhotoFileAsync(choureyOneViewModel);

                                var siteSpaceDetail = new SiteSpaceDetail();
                                siteSpaceDetail.Ecategory = (int)EnumHelpers.ECategory.ChoureyOne;
                                siteSpaceDetail.CategoryId = (long)choureyOneViewModel.ChoureyOneId!;
                                siteSpaceDetail.CategoryDetailId = (long)choureyOnePhotoInformaton.PhotoId;
                                siteSpaceDetail.Estatus = (int)EnumHelpers.EStatus.Active;
                                siteSpaceDetail.EuploadType = (int)EnumHelpers.EUploadType.Image;
                                siteSpaceDetail.SiteId = choureyOneViewModel.SiteId;
                                siteSpaceDetail.UsedSpaceInMb = contentLength / 1000000.0f;
                                addedSpace += siteSpaceDetail.UsedSpaceInMb;
                                await _siteSpaceDetailService.Insert(siteSpaceDetail);
                            }
                        }
                    }
                }
            }

            return addedSpace;
        }

        public async Task<float> ProcessUploads(ChoureyOneViewModel choureyOneViewModel, IList<IFormFile> uploads, float addedSpace, IList<Task> uploadTasks, string rootPath)
        {
            if (uploads != null)
            {
                if (uploads.Count > 0)
                {
                    foreach (var file in uploads)
                    {
                        if (file != null)
                        {
                            var fileUploadPath = $"Gallery/ChoureyOne/{choureyOneViewModel.SiteId}/{choureyOneViewModel.ChoureyOneId}/uploads";
                            bool exists = Directory.Exists(Path.Combine(rootPath, fileUploadPath));

                            if (!exists)
                            {
                                Directory.CreateDirectory(Path.Combine(rootPath, fileUploadPath));
                            }

                            var contentLength = file.Length;

                            var extension = Path.GetExtension(file.FileName);
                            var fileName = Guid.NewGuid().ToString() + extension;

                            var path = Path.Combine(Path.Combine(rootPath, fileUploadPath), fileName);
                            uploadTasks.Add(Task.Run(() => SaveFileToPath(file, path)));

                            var fileUploadModel = new FileUpload();
                            fileUploadModel.FileName = fileName;
                            fileUploadModel.FilePath = path;
                            fileUploadModel.ActualFileName = Path.GetFileNameWithoutExtension(file.FileName);
                            fileUploadModel.CreatedBy = choureyOneViewModel.CurrentUserId;
                            fileUploadModel.CreatedOn = new Utilities().GetTokyoDate();
                            fileUploadModel.EfileType = (int)EnumHelpers.EFileType.PDF;
                            fileUploadModel.Estatus = (int)EnumHelpers.EStatus.Active;
                            fileUploadModel.SiteId = choureyOneViewModel.SiteId;
                            fileUploadModel.CategoryId = choureyOneViewModel.ChoureyOneId;
                            fileUploadModel.Ecategory = (int)EnumHelpers.ECategory.ChoureyOne;
                            fileUploadModel.ViewMode = (int)_helpers.GetViewMode(_httpContextAccessor.HttpContext.Session.GetString("ViewMode"));

                            await _fileUploadService.InsertFile(fileUploadModel, choureyOneViewModel.UserName);

                            

                            var siteSpaceDetail = new SiteSpaceDetail();
                            siteSpaceDetail.Ecategory = (int)EnumHelpers.ECategory.ChoureyOne;
                            siteSpaceDetail.CategoryId = (long)choureyOneViewModel.ChoureyOneId!;
                            siteSpaceDetail.CategoryDetailId = (long)fileUploadModel.FileUploadId;
                            siteSpaceDetail.Estatus = (int)EnumHelpers.EStatus.Active;
                            siteSpaceDetail.EuploadType = (int)EnumHelpers.EUploadType.Pdf;
                            siteSpaceDetail.SiteId = choureyOneViewModel.SiteId;
                            siteSpaceDetail.UsedSpaceInMb = contentLength / 1000000.0f;
                            addedSpace += siteSpaceDetail.UsedSpaceInMb;
                            await _siteSpaceDetailService.Insert(siteSpaceDetail);
                        }
                    }
                }
            }

            return addedSpace;
        }

        public async Task<ChoureyOneViewModel> InsertUpdateChoureyOneAsync(ChoureyOneViewModel choureyOneViewModel)
        {
            try
            {
                if (choureyOneViewModel.ChoureyOneId != 0)
                {
                    var choureyOneInformation = _choureyOneRepository.Table.Where(x => x.SiteId == choureyOneViewModel.SiteId && x.ChoureyOneId == choureyOneViewModel.ChoureyOneId).FirstOrDefault();

                    if (choureyOneInformation != null)
                    {
                        choureyOneInformation.BrowseRange = choureyOneViewModel.BrowseRange;
                        choureyOneInformation.Description = choureyOneViewModel.Description;
                        choureyOneInformation.IsActive = choureyOneViewModel.IsActive;
                        choureyOneInformation.SiteId = choureyOneViewModel.SiteId;
                        choureyOneInformation.Title = choureyOneViewModel.Title;
                        choureyOneInformation.UpdatedBy = choureyOneViewModel.CurrentUserId;
                        choureyOneInformation.UpdatedOn = new Utilities().GetTokyoDate();

                        await _choureyOneRepository.Update(choureyOneInformation);

                        EntityEntry<ChoureyOneInformation> entry = _applicationDbContext.Entry(choureyOneInformation);
                        await _changeLogChoureyOneInformationService.Update(entry, choureyOneInformation.ChoureyOneId
                                    , EnumHelpers.EChangeType.Modified, EnumHelpers.ECategory.ChoureyOne, EnumHelpers.EChangeCategory.Info
                                    , choureyOneViewModel.UserName, choureyOneViewModel.SiteId);

                        choureyOneViewModel.IsSuccessful = true;
                        choureyOneViewModel.Message = "Successfully Updated";
                    } else
                    {
                        throw new ObjectNotFoundException("ChoureyOneId for SiteId does not exist.");
                    }
                }
                else
                {
                    int positionId = 1;
                    var choureyOneInformation = _choureyOneRepository.Table.Where(x => x.SiteId == choureyOneViewModel.SiteId).OrderByDescending(x => x.Position).FirstOrDefault();
                    if (choureyOneInformation != null)
                    {
                        positionId = choureyOneInformation.Position + 1;
                    }

                    var createChoureyOneInformation = new ChoureyOneInformation();
                    createChoureyOneInformation.CreatedBy = choureyOneViewModel.CurrentUserId;
                    createChoureyOneInformation.CreatedOn = new Utilities().GetTokyoDate();
                    createChoureyOneInformation.Description = choureyOneViewModel.Description;
                    createChoureyOneInformation.IsActive = choureyOneViewModel.IsActive;
                    createChoureyOneInformation.Position = positionId;
                    createChoureyOneInformation.SiteId = choureyOneViewModel.SiteId;
                    createChoureyOneInformation.Title = choureyOneViewModel.Title;
                    createChoureyOneInformation.ViewMode = (int)choureyOneViewModel.EViewMode;
                    createChoureyOneInformation.BrowseRange = choureyOneViewModel.BrowseRange;

                    var createdChoureyOneInformation = await _choureyOneRepository.Add(createChoureyOneInformation);

                    choureyOneViewModel.ChoureyOneId = createdChoureyOneInformation.ChoureyOneId;
                    choureyOneViewModel.IsSuccessful = true;
                    choureyOneViewModel.Message = "Successfully Inserted";

                    await _changeLogChoureyOneInformationService.Insert(createdChoureyOneInformation.ChoureyOneId
                                , EnumHelpers.EChangeType.Added, EnumHelpers.ECategory.ChoureyOne, EnumHelpers.EChangeCategory.Info
                                , choureyOneViewModel.UserName, choureyOneViewModel.SiteId);
                }
            }
            catch (Exception ex)
            {
                choureyOneViewModel.IsSuccessful = false;
                if (choureyOneViewModel.ChoureyOneId != 0)
                {
                    choureyOneViewModel.Message = "Failed to Update";
                }
                else
                {
                    choureyOneViewModel.Message = "Failed to Insert";
                }
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new Utilities().GetTokyoDate();
                entity.ErrorMessage = ex.Message;
                entity.FileName = "BAL:Implementation:User:SetUpService";
                entity.MethodName = "InsertUpdateChoureyOne: " + choureyOneViewModel.ChoureyOneId;
                entity.OperationDoneBy = choureyOneViewModel.UserName;
                entity.Status = true;
                await _errorMessageInformationRepository.Add(entity);
                throw;
            }

            return choureyOneViewModel;
        }

        private async Task<ChoureyOneViewModel> InsertUpdateChoureyOneGalleryAsync(ChoureyOneViewModel choureyOneViewModel)
        {
            try
            {
                if (choureyOneViewModel.GalleryId != 0 && choureyOneViewModel.GalleryId != null)
                {
                    var choureyOneGalleryInformation = _choureyOneGalleryRepository.Table.Where(x => x.SiteId == choureyOneViewModel.SiteId && x.GalleryId == choureyOneViewModel.GalleryId).FirstOrDefault();

                    if (choureyOneGalleryInformation != null)
                    {
                        choureyOneGalleryInformation.ChoureyOneId = (long)choureyOneViewModel.ChoureyOneId;
                        choureyOneGalleryInformation.GalleyName = choureyOneViewModel.GalleyName;
                        choureyOneGalleryInformation.IsActive = choureyOneViewModel.IsActive;
                        choureyOneGalleryInformation.SiteId = choureyOneViewModel.SiteId;
                        choureyOneGalleryInformation.UpdatedBy = choureyOneViewModel.CurrentUserId;
                        choureyOneGalleryInformation.UpdatedOn = new Utilities().GetTokyoDate();
                        _choureyOneGalleryRepository.Update(choureyOneGalleryInformation);

                        choureyOneViewModel.IsSuccessful = true;
                        choureyOneViewModel.Message = "Successfully Updated";
                    }
                    else
                    {
                        choureyOneViewModel.IsSuccessful = false;
                        choureyOneViewModel.Message = "Cannot find the Gallery Detail";
                    }
                }
                else
                {
                    //Insert
                    var choureyOneGalleryInformation = new ChoureyOneGalleryInformation();
                    choureyOneGalleryInformation.CreatedBy = choureyOneViewModel.CurrentUserId;
                    choureyOneGalleryInformation.CreatedOn = new Utilities().GetTokyoDate();
                    choureyOneGalleryInformation.ChoureyOneId = (long)choureyOneViewModel.ChoureyOneId;
                    choureyOneGalleryInformation.GalleyName = choureyOneViewModel.Title;
                    choureyOneGalleryInformation.IsActive = choureyOneViewModel.IsActive;
                    choureyOneGalleryInformation.SiteId = choureyOneViewModel.SiteId;

                    var createdChoureyOneGalleryReposiotry = await _choureyOneGalleryRepository.Add(choureyOneGalleryInformation);

                    choureyOneViewModel.GalleryId = createdChoureyOneGalleryReposiotry.GalleryId;
                    choureyOneViewModel.IsSuccessful = true;
                    choureyOneViewModel.Message = "Successfully Inserted";
                }
            } catch (Exception ex)
            {
                choureyOneViewModel.IsSuccessful = false;
                if (choureyOneViewModel.ChoureyOneId != 0)
                {
                    choureyOneViewModel.Message = "Failed to Update";
                }
                else
                {
                    choureyOneViewModel.Message = "Failed to Insert";
                }
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new Utilities().GetTokyoDate();
                entity.ErrorMessage = ex.Message;
                entity.FileName = "BAL:Implementation:User:SetUpService";
                entity.MethodName = "InsertUpdateChoureyOneGalleryAsync: " + choureyOneViewModel.ChoureyOneId;
                entity.OperationDoneBy = choureyOneViewModel.UserName;
                entity.Status = true;
                await _errorMessageInformationRepository.Add(entity);
            }

            return choureyOneViewModel;
        }

        private async Task<ChoureyOneViewModel> InsertChoureyOnePhotoFileAsync(ChoureyOneViewModel choureyOneViewModel)
        {
            try
            {
                var choureyOnePhotoInformation = new ChoureyOnePhotoInformation();
                choureyOnePhotoInformation.CreatedBy = choureyOneViewModel.CurrentUserId;
                choureyOnePhotoInformation.CreatedOn = new Utilities().GetTokyoDate();
                choureyOnePhotoInformation.ChoureyOneId =   (long)choureyOneViewModel.ChoureyOneId;
                choureyOnePhotoInformation.FileName = choureyOneViewModel.FileName;
                choureyOnePhotoInformation.FilePath = choureyOneViewModel.FilePath;
                choureyOnePhotoInformation.GalleryId = choureyOneViewModel.GalleryId;
                choureyOnePhotoInformation.IsActive = choureyOneViewModel.IsActive;
                choureyOnePhotoInformation.SiteId = choureyOneViewModel.SiteId;
                choureyOnePhotoInformation.PhotoName = choureyOneViewModel.PhotoName;
                choureyOnePhotoInformation.CustomName = choureyOneViewModel.CustomName;

                var createdChoureyOnePhotoInformation = await _choureyOnePhotoRepository.Add(choureyOnePhotoInformation);

                choureyOneViewModel.PhotoId = createdChoureyOnePhotoInformation.PhotoId;

                await _changeLogChoureyOnePhotoInformationService.Insert(createdChoureyOnePhotoInformation.PhotoId
                        , EnumHelpers.EChangeType.Added, EnumHelpers.ECategory.ChoureyOne, EnumHelpers.EChangeCategory.Image
                        , choureyOneViewModel.UserName, choureyOneViewModel.SiteId);

                choureyOneViewModel.IsSuccessful = true;
                choureyOneViewModel.Message = "Successfully Inserted";
            }
            catch (Exception e)
            {
                choureyOneViewModel.IsSuccessful = false;
                choureyOneViewModel.Message = "Failed to Insert";
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new Utilities().GetTokyoDate();
                entity.ErrorMessage = e.Message;
                entity.FileName = "BAL:Implementation:User:SetUpService";
                entity.MethodName = "InsertChoureyOnePhotoFileAsync";
                entity.OperationDoneBy = choureyOneViewModel.UserName;
                entity.Status = true;
                await _errorMessageInformationRepository.Add(entity);
            }

            return choureyOneViewModel;
        }

        private async Task<ChoureyOneViewModel> InsertChoureyOneVideoFileAsync(ChoureyOneViewModel choureyOneViewModel)
        {
            try
            {
                var choureyOneVideoInformation = new ChoureyOneVideoInformation();
                choureyOneVideoInformation.ChoureyOneId = (long)choureyOneViewModel.ChoureyOneId;
                choureyOneVideoInformation.Description = choureyOneViewModel.Description;
                choureyOneVideoInformation.FileName = choureyOneViewModel.FileName;
                choureyOneVideoInformation.FilePath = choureyOneViewModel.FilePath;
                choureyOneVideoInformation.IsActive = choureyOneViewModel.IsActive;
                choureyOneVideoInformation.SiteId = choureyOneViewModel.SiteId;
                choureyOneVideoInformation.Title = choureyOneViewModel.Title;
                choureyOneVideoInformation.ThumbFileName = choureyOneViewModel.ThumbFileName;
                choureyOneVideoInformation.CreatedBy = choureyOneViewModel.CurrentUserId;
                choureyOneVideoInformation.CreatedOn = new Utilities().GetTokyoDate();

                var createdChoureyOneVideoInformation = await _choureyOneVideoRepository.Add(choureyOneVideoInformation);

                choureyOneViewModel.VideoId = createdChoureyOneVideoInformation.VideoId;

                await _changeLogChoureyOneVideoInformationService.Insert(createdChoureyOneVideoInformation.VideoId
                                , EnumHelpers.EChangeType.Added, EnumHelpers.ECategory.ChoureyOne, EnumHelpers.EChangeCategory.Video
                                , choureyOneViewModel.UserName, choureyOneViewModel.SiteId);

                choureyOneViewModel.IsSuccessful = true;
                choureyOneViewModel.Message = "Successfully Inserted";
            }
            catch (Exception e)
            {
                choureyOneViewModel.IsSuccessful = false;
                choureyOneViewModel.Message = "Invalid!! Failed to Upload Video";
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new Utilities().GetTokyoDate();
                entity.ErrorMessage = e.Message;
                entity.FileName = "BAL:Implementation:Admin:SetUpService";
                entity.MethodName = "ChoureyOneVideoUpload:" + choureyOneViewModel.VideoId;
                entity.OperationDoneBy = choureyOneViewModel.UserName;
                entity.Status = true;
                await _errorMessageInformationRepository.Add(entity);
            }

            return choureyOneViewModel;
        }

        public async Task UpdateChoureyOnePhotoCustomName(string customName, long photoId, string userName)
        {
            try
            {
                var choureyOnePhotoInformation = _choureyOnePhotoRepository.Table.Where(x => x.PhotoId == photoId && x.CustomName != customName).FirstOrDefault();
                if (choureyOnePhotoInformation != null)
                {
                    choureyOnePhotoInformation.CustomName = customName;
                    await _choureyOnePhotoRepository.Update(choureyOnePhotoInformation);
                }
            }
            catch (Exception e)
            {
                var errorMessageInformation = new ErrorMessageInformation();
                errorMessageInformation.CreatedOn = new Utilities().GetTokyoDate();
                errorMessageInformation.ErrorMessage = e.Message;
                errorMessageInformation.FileName = "BAL:Implementation:User:SetUpService";
                errorMessageInformation.MethodName = "UpdateChoureyOnePhotCustomName";
                errorMessageInformation.OperationDoneBy = userName;
                errorMessageInformation.Status = true;
                await _errorMessageInformationRepository.Add(errorMessageInformation);
            }
        }

        public async Task UpdateChoureyOneVideoCustomName(string customName, long videoId, string userName)
        {
            try
            {
                var choureyOneVideoInformation = _choureyOneVideoRepository.Table.Where(x => x.VideoId == videoId && x.CustomName != customName).FirstOrDefault();
                if (choureyOneVideoInformation != null)
                {
                    choureyOneVideoInformation.CustomName = customName;
                    await _choureyOneVideoRepository.Update(choureyOneVideoInformation);
                }
            }
            catch (Exception e)
            {
                var errorMessageInformation = new ErrorMessageInformation();
                errorMessageInformation.CreatedOn = new Utilities().GetTokyoDate();
                errorMessageInformation.ErrorMessage = e.Message;
                errorMessageInformation.FileName = "BAL:Implementation:User:SetUpService";
                errorMessageInformation.MethodName = "UpdateChoureyOneVideoCustomName";
                errorMessageInformation.OperationDoneBy = userName;
                errorMessageInformation.Status = true;
                await _errorMessageInformationRepository.Add(errorMessageInformation);
            }
        }

        #endregion

        #region ChoureyTwoInsertUpdate Operations
        public async Task<ChoureyTwoViewModel> InsertUpdateChoureyTwoAsync(ChoureyTwoViewModel choureyTwoViewModel)
        {
            try
            {
                if (choureyTwoViewModel.ChoureyTwoId != 0)
                {
                    var choureyTwoInformation = _choureyTwoRepository.Table.Where(x => x.SiteId == choureyTwoViewModel.SiteId && x.ChoureyTwoId == choureyTwoViewModel.ChoureyTwoId).FirstOrDefault();

                    if (choureyTwoInformation != null)
                    {
                        choureyTwoInformation.BrowseRange = choureyTwoViewModel.BrowseRange;
                        choureyTwoInformation.Description = choureyTwoViewModel.Description;
                        choureyTwoInformation.IsActive = choureyTwoViewModel.IsActive;
                        choureyTwoInformation.SiteId = choureyTwoViewModel.SiteId;
                        choureyTwoInformation.Title = choureyTwoViewModel.Title;
                        choureyTwoInformation.UpdatedBy = choureyTwoViewModel.CurrentUserId;
                        choureyTwoInformation.UpdatedOn = new Utilities().GetTokyoDate();

                        await _choureyTwoRepository.Update(choureyTwoInformation);

                        EntityEntry<ChoureyTwoInformation> entry = _applicationDbContext.Entry(choureyTwoInformation);
                        await _changeLogChoureyTwoInformationService.Update(entry, choureyTwoInformation.ChoureyTwoId
                                    , EnumHelpers.EChangeType.Modified, EnumHelpers.ECategory.ChoureyTwo, EnumHelpers.EChangeCategory.Info
                        , choureyTwoViewModel.UserName, choureyTwoViewModel.SiteId);

                        choureyTwoViewModel.IsSuccessful = true;
                        choureyTwoViewModel.Message = "Successfully Updated";
                    }
                    else
                    {
                        throw new ObjectNotFoundException("ChoureyTwoId for SiteId does not exist.");
                    }
                }
                else
                {
                    int positionId = 1;
                    var choureyTwoInformation = _choureyTwoRepository.Table.Where(x => x.SiteId == choureyTwoViewModel.SiteId).OrderByDescending(x => x.Position).FirstOrDefault();
                    if (choureyTwoInformation != null)
                    {
                        positionId = choureyTwoInformation.Position + 1;
                    }

                    var createChoureyTwoInformation = new ChoureyTwoInformation();
                    createChoureyTwoInformation.CreatedBy = choureyTwoViewModel.CurrentUserId;
                    createChoureyTwoInformation.CreatedOn = new Utilities().GetTokyoDate();
                    createChoureyTwoInformation.Description = choureyTwoViewModel.Description;
                    createChoureyTwoInformation.IsActive = choureyTwoViewModel.IsActive;
                    createChoureyTwoInformation.Position = positionId;
                    createChoureyTwoInformation.SiteId = choureyTwoViewModel.SiteId;
                    createChoureyTwoInformation.Title = choureyTwoViewModel.Title;
                    createChoureyTwoInformation.ViewMode = (int)choureyTwoViewModel.EViewMode;
                    createChoureyTwoInformation.BrowseRange = choureyTwoViewModel.BrowseRange;

                    var createdChoureyTwoInformation = await _choureyTwoRepository.Add(createChoureyTwoInformation);

                    choureyTwoViewModel.ChoureyTwoId = createdChoureyTwoInformation.ChoureyTwoId;
                    choureyTwoViewModel.IsSuccessful = true;
                    choureyTwoViewModel.Message = "Successfully Inserted";

                    await _changeLogChoureyTwoInformationService.Insert(createdChoureyTwoInformation.ChoureyTwoId
                                , EnumHelpers.EChangeType.Added, EnumHelpers.ECategory.ChoureyTwo, EnumHelpers.EChangeCategory.Info
                                , choureyTwoViewModel.UserName, choureyTwoViewModel.SiteId);
                }
            }
            catch (Exception ex)
            {
                choureyTwoViewModel.IsSuccessful = false;
                if (choureyTwoViewModel.ChoureyTwoId != 0)
                {
                    choureyTwoViewModel.Message = "Failed to Update";
                }
                else
                {
                    choureyTwoViewModel.Message = "Failed to Insert";
                }
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new Utilities().GetTokyoDate();
                entity.ErrorMessage = ex.Message;
                entity.FileName = "BAL:Implementation:User:SetUpService";
                entity.MethodName = "InsertUpdateChoureyTwo: " + choureyTwoViewModel.ChoureyTwoId;
                entity.OperationDoneBy = choureyTwoViewModel.UserName;
                entity.Status = true;
                await _errorMessageInformationRepository.Add(entity);
            }

            return choureyTwoViewModel;
        }

        public async Task<float> ProcessUploads(ChoureyTwoViewModel choureyTwoViewModel, IList<IFormFile> uploads, float addedSpace, IList<Task> uploadTasks, string rootPath)
        {
            if (uploads != null)
            {
                if (uploads.Count > 0)
                {
                    foreach (var file in uploads)
                    {
                        if (file != null)
                        {
                            var fileUploadPath = $"Gallery/ChoureyTwo/{choureyTwoViewModel.SiteId}/{choureyTwoViewModel.ChoureyTwoId}/uploads";
                            bool exists = Directory.Exists(Path.Combine(rootPath, fileUploadPath));

                            if (!exists)
                            {
                                Directory.CreateDirectory(Path.Combine(rootPath, fileUploadPath));
                            }

                            var contentLength = file.Length;

                            var extension = Path.GetExtension(file.FileName);
                            var fileName = Guid.NewGuid().ToString() + extension;

                            var path = Path.Combine(Path.Combine(rootPath, fileUploadPath), fileName);
                            uploadTasks.Add(Task.Run(() => SaveFileToPath(file, path)));

                            var fileUploadModel = new FileUpload();
                            fileUploadModel.FilePath = path;
                            fileUploadModel.FileName = Guid.NewGuid().ToString() + extension;
                            fileUploadModel.ActualFileName = Path.GetFileNameWithoutExtension(file.FileName);
                            fileUploadModel.CreatedBy = choureyTwoViewModel.CurrentUserId!;
                            fileUploadModel.CreatedOn = new Utilities().GetTokyoDate();
                            fileUploadModel.EfileType = (int)EnumHelpers.EFileType.PDF;
                            fileUploadModel.Estatus = (int)EnumHelpers.EStatus.Active;
                            fileUploadModel.SiteId = choureyTwoViewModel.SiteId;
                            fileUploadModel.CategoryId = choureyTwoViewModel.ChoureyTwoId;
                            fileUploadModel.Ecategory = (int)EnumHelpers.ECategory.ChoureyTwo;
                            fileUploadModel.ViewMode = (int)_helpers.GetViewMode(_httpContextAccessor.HttpContext.Session.GetString("ViewMode"));

                            await _fileUploadService.InsertFile(fileUploadModel, choureyTwoViewModel.UserName!);

                            var siteSpaceDetail = new SiteSpaceDetail();
                            siteSpaceDetail.Ecategory = (int)EnumHelpers.ECategory.ChoureyTwo;
                            siteSpaceDetail.CategoryId = (long)choureyTwoViewModel.ChoureyTwoId!;
                            siteSpaceDetail.CategoryDetailId = (long)fileUploadModel.FileUploadId;
                            siteSpaceDetail.Estatus = (int)EnumHelpers.EStatus.Active;
                            siteSpaceDetail.EuploadType = (int)EnumHelpers.EUploadType.Pdf;
                            siteSpaceDetail.SiteId = choureyTwoViewModel.SiteId;
                            siteSpaceDetail.UsedSpaceInMb = contentLength / 1000000.0f;
                            addedSpace += siteSpaceDetail.UsedSpaceInMb;
                            await _siteSpaceDetailService.Insert(siteSpaceDetail);
                        }
                    }
                }
            }

            return addedSpace;
        }

        public async Task<float> ProcessFiles(ChoureyTwoViewModel choureyTwoViewModel, IList<IFormFile> files, float addedSpace, IList<Task> uploadTasks, string rootPath)
        {
            if (files != null)
            {
                if (files.Count() > 0 && files[0] != null)
                {

                    var choureyOneGalleryInformation = await InsertUpdateChoureyTwoGalleryAsync(choureyTwoViewModel);

                    var subPath = "Gallery/ChoureyTwo/" + choureyTwoViewModel.SiteId + "/" + choureyOneGalleryInformation.GalleryId;

                    foreach (var file in files)
                    {
                        if (file != null)
                        {
                            var contentLength = file.Length;
                            if (contentLength > 0)
                            {
                                bool exists = Directory.Exists(Path.Combine(rootPath, subPath));

                                if (!exists)
                                {
                                    Directory.CreateDirectory(Path.Combine(rootPath, subPath));
                                }

                                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                                var pathWithFileName = Path.Combine(Path.Combine(rootPath, subPath), fileName);
                                uploadTasks.Add(Task.Run(() => SaveFileToPath(file, pathWithFileName)));

                                choureyTwoViewModel.FileName = fileName;
                                choureyTwoViewModel.FilePath = pathWithFileName;
                                choureyTwoViewModel.PhotoName = Path.GetFileName(file.FileName);
                                var choureyTwoPhotoInformaton = await InsertChoureyTwoPhotoFileAsync(choureyTwoViewModel);

                                var siteSpaceDetail = new SiteSpaceDetail();
                                siteSpaceDetail.Ecategory = (int)EnumHelpers.ECategory.ChoureyTwo;
                                siteSpaceDetail.CategoryId = (long)choureyTwoPhotoInformaton.ChoureyTwoId;
                                siteSpaceDetail.CategoryDetailId = (long)choureyTwoPhotoInformaton.PhotoId;
                                siteSpaceDetail.Estatus = (int)EnumHelpers.EStatus.Active;
                                siteSpaceDetail.EuploadType = (int)EnumHelpers.EUploadType.Image;
                                siteSpaceDetail.SiteId = choureyTwoViewModel.SiteId;
                                siteSpaceDetail.UsedSpaceInMb = contentLength / 1000000.0f;
                                addedSpace += siteSpaceDetail.UsedSpaceInMb;
                                await _siteSpaceDetailService.Insert(siteSpaceDetail);
                            }
                        }
                    }
                }
            }

            return addedSpace;
        }
        private async Task<ChoureyTwoViewModel> InsertUpdateChoureyTwoGalleryAsync(ChoureyTwoViewModel choureyTwoViewModel)
        {
            try
            {
                if (choureyTwoViewModel.GalleryId != 0 && choureyTwoViewModel.GalleryId != null)
                {
                    var choureyTwoGalleryInformation = _choureyTwoGalleryRepository.Table.Where(x => x.SiteId == choureyTwoViewModel.SiteId && x.GalleryId == choureyTwoViewModel.GalleryId).FirstOrDefault();

                    if (choureyTwoGalleryInformation != null)
                    {
                        choureyTwoGalleryInformation.ChoureyTwoId = (long)choureyTwoViewModel.ChoureyTwoId;
                        choureyTwoGalleryInformation.GalleyName = choureyTwoViewModel.GalleyName;
                        choureyTwoGalleryInformation.IsActive = choureyTwoViewModel.IsActive;
                        choureyTwoGalleryInformation.SiteId = choureyTwoViewModel.SiteId;
                        choureyTwoGalleryInformation.UpdatedBy = choureyTwoViewModel.CurrentUserId;
                        choureyTwoGalleryInformation.UpdatedOn = new Utilities().GetTokyoDate();
                        await _choureyTwoGalleryRepository.Update(choureyTwoGalleryInformation);

                        choureyTwoViewModel.IsSuccessful = true;
                        choureyTwoViewModel.Message = "Successfully Updated";
                    }
                    else
                    {
                        choureyTwoViewModel.IsSuccessful = false;
                        choureyTwoViewModel.Message = "Cannot find the Gallery Detail";
                    }
                }
                else
                {
                    //Insert
                    var choureyTwoGalleryInformation = new ChoureyTwoGalleryInformation();
                    choureyTwoGalleryInformation.CreatedBy = choureyTwoViewModel.CurrentUserId;
                    choureyTwoGalleryInformation.CreatedOn = new Utilities().GetTokyoDate();
                    choureyTwoGalleryInformation.ChoureyTwoId = (long)choureyTwoViewModel.ChoureyTwoId;
                    choureyTwoGalleryInformation.GalleyName = choureyTwoViewModel.Title;
                    choureyTwoGalleryInformation.IsActive = choureyTwoViewModel.IsActive;
                    choureyTwoGalleryInformation.SiteId = choureyTwoViewModel.SiteId;

                    var createdChoureyTwoGalleryReposiotry = await _choureyTwoGalleryRepository.Add(choureyTwoGalleryInformation);

                    choureyTwoViewModel.GalleryId = createdChoureyTwoGalleryReposiotry.GalleryId;
                    choureyTwoViewModel.IsSuccessful = true;
                    choureyTwoViewModel.Message = "Successfully Inserted";
                }
            }
            catch (Exception ex)
            {
                choureyTwoViewModel.IsSuccessful = false;
                if (choureyTwoViewModel.ChoureyTwoId != 0)
                {
                    choureyTwoViewModel.Message = "Failed to Update";
                }
                else
                {
                    choureyTwoViewModel.Message = "Failed to Insert";
                }
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new Utilities().GetTokyoDate();
                entity.ErrorMessage = ex.Message;
                entity.FileName = "BAL:Implementation:User:SetUpService";
                entity.MethodName = "InsertUpdateChoureyTwoGallery: " + choureyTwoViewModel.ChoureyTwoId;
                entity.OperationDoneBy = choureyTwoViewModel.UserName;
                entity.Status = true;
                await _errorMessageInformationRepository.Add(entity);
            }

            return choureyTwoViewModel;
        }

        private async Task<ChoureyTwoViewModel> InsertChoureyTwoPhotoFileAsync(ChoureyTwoViewModel choureyTwoViewModel)
        {
            try
            {
                var choureyTwoPhotoInformation = new ChoureyTwoPhotoInformation();
                choureyTwoPhotoInformation.CreatedBy = choureyTwoViewModel.CurrentUserId;
                choureyTwoPhotoInformation.CreatedOn = new Utilities().GetTokyoDate();
                choureyTwoPhotoInformation.ChoureyTwoId = (long)choureyTwoViewModel.ChoureyTwoId;
                choureyTwoPhotoInformation.FileName = choureyTwoViewModel.FileName;
                choureyTwoPhotoInformation.FilePath = choureyTwoViewModel.FilePath;
                choureyTwoPhotoInformation.GalleryId = choureyTwoViewModel.GalleryId;
                choureyTwoPhotoInformation.IsActive = choureyTwoViewModel.IsActive;
                choureyTwoPhotoInformation.SiteId = choureyTwoViewModel.SiteId;
                choureyTwoPhotoInformation.PhotoName = choureyTwoViewModel.PhotoName;
                choureyTwoPhotoInformation.CustomName = choureyTwoViewModel.CustomName;

                var createdChoureyTwoPhotoInformation = await _choureyTwoPhotoRepository.Add(choureyTwoPhotoInformation);

                choureyTwoViewModel.PhotoId = createdChoureyTwoPhotoInformation.PhotoId;

                await _changeLogChoureyTwoPhotoInformationService.Insert(createdChoureyTwoPhotoInformation.PhotoId
                        , EnumHelpers.EChangeType.Added, EnumHelpers.ECategory.ChoureyTwo, EnumHelpers.EChangeCategory.Image
                        , choureyTwoViewModel.UserName, choureyTwoViewModel.SiteId);

                choureyTwoViewModel.IsSuccessful = true;
                choureyTwoViewModel.Message = "Successfully Inserted";
            }
            catch (Exception e)
            {
                choureyTwoViewModel.IsSuccessful = false;
                choureyTwoViewModel.Message = "Failed to Insert";
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new Utilities().GetTokyoDate();
                entity.ErrorMessage = e.Message;
                entity.FileName = "BAL:Implementation:User:SetUpService";
                entity.MethodName = "InsertChoureyTwoPhotoFile";
                entity.OperationDoneBy = choureyTwoViewModel.UserName;
                entity.Status = true;
                await _errorMessageInformationRepository.Add(entity);
            }

            return choureyTwoViewModel;
        }

        public async Task<float> ProcessVideos(ChoureyTwoViewModel choureyTwoViewModel, IList<IFormFile> videos, IList<IFormFile> thumbs, float addedSpace, IList<Task> uploadTasks, string rootPath)
        {
            if (videos != null)
            {
                if (videos.Count() > 0 && videos[0] != null)
                {
                    //Create Gallery
                    var videoSubPath = "ChoureyTwoVideo/Video/" + choureyTwoViewModel.SiteId + "/" + choureyTwoViewModel.ChoureyTwoId;
                    var videoFilePath = "";
                    var thumbSubPath = "ChoureyTwoVideo/Thumbnail/" + choureyTwoViewModel.SiteId + "/" + choureyTwoViewModel.ChoureyTwoId;

                    bool exists = Directory.Exists(Path.Combine(rootPath, videoSubPath));

                    if (!exists)
                    {
                        Directory.CreateDirectory(Path.Combine(rootPath, videoSubPath));
                    }

                    bool existsThumb = Directory.Exists(Path.Combine(rootPath, thumbSubPath));
                    if (!existsThumb)
                    {
                        Directory.CreateDirectory(Path.Combine(rootPath, thumbSubPath));
                    }

                    foreach (var video in videos)
                    {
                        var contentLength = video.Length;
                        var guid = Guid.NewGuid();

                        if (video.Length > 0)
                        {
                            if (thumbs != null)
                            {
                                if (thumbs.Count() > 0 && thumbs[0] != null)
                                {
                                    var thumbnail = thumbs[0];
                                    if (thumbnail.Length > 0)
                                    {
                                        var thumbFileName = guid + Path.GetExtension(thumbnail.FileName);
                                        var thumbFilePath = Path.Combine(Path.Combine(rootPath, thumbSubPath), thumbFileName);
                                        uploadTasks.Add(Task.Run(() => SaveFileToPath(thumbnail, thumbFilePath)));
                                        contentLength += thumbnail.Length;
                                        choureyTwoViewModel.ThumbFileName = thumbFileName;
                                    }
                                }
                            }

                            var fileName = guid + Path.GetExtension(video.FileName);
                            videoFilePath = Path.Combine(Path.Combine(rootPath, videoSubPath), fileName);
                            choureyTwoViewModel.FileName = fileName;
                            choureyTwoViewModel.FilePath = videoFilePath;
                            uploadTasks.Add(Task.Run(() => SaveFileToPath(video, videoFilePath)));
                            var choureyTwoVideoInformation = await InsertChoureyTwoVideoFileAsync(choureyTwoViewModel);

                            var siteSpaceDetail = new SiteSpaceDetail();
                            siteSpaceDetail.Ecategory = (int)EnumHelpers.ECategory.ChoureyTwo;
                            siteSpaceDetail.CategoryId = (long)choureyTwoVideoInformation.ChoureyTwoId;
                            siteSpaceDetail.CategoryDetailId = (long)choureyTwoVideoInformation.VideoId;
                            siteSpaceDetail.Estatus = (int)EnumHelpers.EStatus.Active;
                            siteSpaceDetail.EuploadType = (int)EnumHelpers.EUploadType.Video;
                            siteSpaceDetail.SiteId = choureyTwoViewModel.SiteId;
                            siteSpaceDetail.UsedSpaceInMb = contentLength / 1000000.0f;
                            addedSpace += siteSpaceDetail.UsedSpaceInMb;
                            await _siteSpaceDetailService.Insert(siteSpaceDetail);
                        }
                    }
                }
            }

            return addedSpace;
        }

        private async Task<ChoureyTwoViewModel> InsertChoureyTwoVideoFileAsync(ChoureyTwoViewModel choureyTwoViewModel)
        {
            try
            {
                var choureyTwoVideoInformation = new ChoureyTwoVideoInformation();
                choureyTwoVideoInformation.ChoureyTwoId = (long)choureyTwoViewModel.ChoureyTwoId;
                choureyTwoVideoInformation.Description = choureyTwoViewModel.Description;
                choureyTwoVideoInformation.FileName = choureyTwoViewModel.FileName;
                choureyTwoVideoInformation.FilePath = choureyTwoViewModel.FilePath;
                choureyTwoVideoInformation.IsActive = choureyTwoViewModel.IsActive;
                choureyTwoVideoInformation.SiteId = choureyTwoViewModel.SiteId;
                choureyTwoVideoInformation.Title = choureyTwoViewModel.Title;
                choureyTwoVideoInformation.ThumbFileName = choureyTwoViewModel.ThumbFileName;
                choureyTwoVideoInformation.CreatedBy = choureyTwoViewModel.CurrentUserId;
                choureyTwoVideoInformation.CreatedOn = new Utilities().GetTokyoDate();

                var createdChoureyTwoVideoInformation = await _choureyTwoVideoRepository.Add(choureyTwoVideoInformation);

                choureyTwoViewModel.VideoId = createdChoureyTwoVideoInformation.VideoId;

                await _changeLogChoureyTwoVideoInformationService.Insert(createdChoureyTwoVideoInformation.VideoId
                                , EnumHelpers.EChangeType.Added, EnumHelpers.ECategory.ChoureyTwo, EnumHelpers.EChangeCategory.Video
                                , choureyTwoViewModel.UserName, choureyTwoViewModel.SiteId);

                choureyTwoViewModel.IsSuccessful = true;
                choureyTwoViewModel.Message = "Successfully Inserted";
            }
            catch (Exception e)
            {
                choureyTwoViewModel.IsSuccessful = false;
                choureyTwoViewModel.Message = "Invalid!! Failed to Upload Video";
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new Utilities().GetTokyoDate();
                entity.ErrorMessage = e.Message;
                entity.FileName = "BAL:Implementation:Admin:SetUpService";
                entity.MethodName = "ChoureyTwoVideoUpload:" + choureyTwoViewModel.VideoId;
                entity.OperationDoneBy = choureyTwoViewModel.UserName;
                entity.Status = true;
                await _errorMessageInformationRepository.Add(entity);
            }

            return choureyTwoViewModel;
        }

        #endregion

        #region ChoureyOne and ChoureyTwo Common Methods

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

        public async Task UpdateSiteSpaceAggregate(string siteId, float addedSpace)
        {
            var siteSpaceAggregate = new SiteSpaceAggregate();
            siteSpaceAggregate.SiteId = siteId;
            siteSpaceAggregate.AllocatedSpace = await _helpers.GetSpaceAllocated(siteId);
            siteSpaceAggregate.UsedSpaceInMb = addedSpace;

            _siteSpaceAggregateService.InsertAndUpdate(siteSpaceAggregate);
        }

        public async Task UpdateChoureyTwoPhotoCustomName(string customName, long photoId, string userName)
        {
            try
            {
                var choureyTwoPhotoInformation = _choureyTwoPhotoRepository.Table.Where(x => x.PhotoId == photoId && x.CustomName != customName).FirstOrDefault();
                if (choureyTwoPhotoInformation != null)
                {
                    choureyTwoPhotoInformation.CustomName = customName;
                    await _choureyTwoPhotoRepository.Update(choureyTwoPhotoInformation);
                }
            }
            catch (Exception e)
            {
                var errorMessageInformation = new ErrorMessageInformation();
                errorMessageInformation.CreatedOn = new Utilities().GetTokyoDate();
                errorMessageInformation.ErrorMessage = e.Message;
                errorMessageInformation.FileName = "BAL:Implementation:User:SetUpService";
                errorMessageInformation.MethodName = "UpdateChoureyTwoPhotCustomName";
                errorMessageInformation.OperationDoneBy = userName;
                errorMessageInformation.Status = true;
                await _errorMessageInformationRepository.Add(errorMessageInformation);
            }
        }

        public async Task UpdateChoureyTwoVideoCustomName(string customName, long videoId, string userName)
        {
            try
            {
                var choureyTwoVideoInformation = _choureyTwoVideoRepository.Table.Where(x => x.VideoId == videoId && x.CustomName != customName).FirstOrDefault();
                if (choureyTwoVideoInformation != null)
                {
                    choureyTwoVideoInformation.CustomName = customName;
                    await _choureyTwoVideoRepository.Update(choureyTwoVideoInformation);
                }
            }
            catch (Exception e)
            {
                var errorMessageInformation = new ErrorMessageInformation();
                errorMessageInformation.CreatedOn = new Utilities().GetTokyoDate();
                errorMessageInformation.ErrorMessage = e.Message;
                errorMessageInformation.FileName = "BAL:Implementation:User:SetUpService";
                errorMessageInformation.MethodName = "UpdateChoureyTwoVideoCustomName";
                errorMessageInformation.OperationDoneBy = userName;
                errorMessageInformation.Status = true;
                await _errorMessageInformationRepository.Add(errorMessageInformation);
            }
        }

        #endregion

        #region DisasterInsertUpdate Operations

        public async Task<DisasterViewModel> InsertUpdateDisaster(DisasterViewModel disasterViewModel)
        {
            try
            {
                if (disasterViewModel != null && disasterViewModel.DisasterId != null)
                {
                    var disasterInformation = _disasterInformationRepository.Table.Where(x => x.SiteId == disasterViewModel.SiteId && x.DisasterId == disasterViewModel.DisasterId).FirstOrDefault();

                    if (disasterInformation != null)
                    {
                        disasterInformation.BrowseRange = disasterViewModel.BrowseRange;
                        disasterInformation.Description = disasterViewModel.Description;
                        disasterInformation.IsActive = disasterViewModel.IsActive;
                        disasterInformation.SiteId = disasterViewModel.SiteId;
                        disasterInformation.Title = disasterViewModel.Title;
                        disasterInformation.UpdatedBy = disasterViewModel.CurrentUserId;
                        disasterInformation.UpdatedOn = new Utilities().GetTokyoDate();

                        await _disasterInformationRepository.Update(disasterInformation);

                        EntityEntry<DisasterInformation> entry = _applicationDbContext.Entry(disasterInformation);
                        await _changeLogDisasterInformationService.Update(entry, disasterInformation.DisasterId
                            , EnumHelpers.EChangeType.Modified, EnumHelpers.ECategory.Disaster, EnumHelpers.EChangeCategory.Info
                            , disasterViewModel.UserName, disasterViewModel.SiteId);

                        disasterViewModel.IsSuccessful = true;
                        disasterViewModel.Message = "Successfully Updated";
                    }
                    else
                    {
                        throw new ObjectNotFoundException("DisasterId for SiteId does not exist.");
                    }
                }
                else
                {
                    int positionId = 1;
                    var disasterInformation = _disasterInformationRepository.Table.Where(x => x.SiteId == disasterViewModel.SiteId).OrderByDescending(x => x.Position).FirstOrDefault();
                    if (disasterInformation != null)
                    {
                        positionId = disasterInformation.Position + 1;
                    }

                    var createDisasterInformation = new DisasterInformation();
                    createDisasterInformation.CreatedBy = disasterViewModel.CurrentUserId;
                    createDisasterInformation.CreatedOn = new Utilities().GetTokyoDate();
                    createDisasterInformation.Description = disasterViewModel.Description;
                    createDisasterInformation.IsActive = disasterViewModel.IsActive;
                    createDisasterInformation.Position = positionId;
                    createDisasterInformation.SiteId = disasterViewModel.SiteId;
                    createDisasterInformation.Title = disasterViewModel.Title;
                    createDisasterInformation.ViewMode = (int)disasterViewModel.EViewMode;
                    createDisasterInformation.BrowseRange = disasterViewModel.BrowseRange;

                    var createdDisasterInformation = await _disasterInformationRepository.Add(createDisasterInformation);

                    disasterViewModel.DisasterId = createdDisasterInformation.DisasterId;
                    disasterViewModel.IsSuccessful = true;
                    disasterViewModel.Message = "Successfully Inserted";

                    await _changeLogDisasterInformationService.Insert(createdDisasterInformation.DisasterId
                                , EnumHelpers.EChangeType.Added, EnumHelpers.ECategory.Disaster, EnumHelpers.EChangeCategory.Info
                                , disasterViewModel.UserName, disasterViewModel.SiteId);
                }
            }
            catch (Exception ex)
            {
                disasterViewModel.IsSuccessful = false;
                if (disasterViewModel.DisasterId != 0)
                {
                    disasterViewModel.Message = "Failed to Update";
                }
                else
                {
                    disasterViewModel.Message = "Failed to Insert";
                }
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new Utilities().GetTokyoDate();
                entity.ErrorMessage = ex.Message;
                entity.FileName = "BAL:Implementation:User:SetUpService";
                entity.MethodName = "InsertUpdateDisaster: " + disasterViewModel.DisasterId;
                entity.OperationDoneBy = disasterViewModel.UserName;
                entity.Status = true;
                await _errorMessageInformationRepository.Add(entity);
                throw;
            }

            return disasterViewModel;
        }

        public async Task<float> ProcessUploads(DisasterViewModel disasterViewModel, IList<IFormFile> uploads, float addedSpace, IList<Task> uploadTasks, string rootPath) {
            if (uploads != null)
            {
                if (uploads.Count > 0)
                {
                    foreach (var file in uploads)
                    {
                        if (file != null)
                        {
                            var fileUploadPath = $"Gallery/Disaster/{disasterViewModel.SiteId}/{disasterViewModel.DisasterId}/uploads";
                            bool exists = Directory.Exists(Path.Combine(rootPath, fileUploadPath));

                            if (!exists)
                            {
                                Directory.CreateDirectory(Path.Combine(rootPath, fileUploadPath));
                            }

                            var contentLength = file.Length;

                            var extension = Path.GetExtension(file.FileName);
                            var fileName = Guid.NewGuid().ToString() + extension;

                            var path = Path.Combine(Path.Combine(rootPath, fileUploadPath), fileName);
                            uploadTasks.Add(Task.Run(() => SaveFileToPath(file, path)));

                            var fileUploadModel = new FileUpload();
                            fileUploadModel.FilePath = path;
                            fileUploadModel.FileName = Guid.NewGuid().ToString() + extension;
                            fileUploadModel.ActualFileName = Path.GetFileNameWithoutExtension(file.FileName);
                            fileUploadModel.CreatedBy = disasterViewModel.CurrentUserId;
                            fileUploadModel.CreatedOn = new Utilities().GetTokyoDate();
                            fileUploadModel.EfileType = (int)EnumHelpers.EFileType.PDF;
                            fileUploadModel.Estatus = (int)EnumHelpers.EStatus.Active;
                            fileUploadModel.SiteId = disasterViewModel.SiteId;
                            fileUploadModel.CategoryId = disasterViewModel.DisasterId;
                            fileUploadModel.Ecategory = (int)EnumHelpers.ECategory.Disaster;
                            fileUploadModel.ViewMode = (int)_helpers.GetViewMode(_httpContextAccessor.HttpContext.Session.GetString("ViewMode"));

                            await _fileUploadService.InsertFile(fileUploadModel, disasterViewModel.UserName);

                            var siteSpaceDetail = new SiteSpaceDetail();
                            siteSpaceDetail.Ecategory = (int)EnumHelpers.ECategory.Disaster;
                            siteSpaceDetail.CategoryId = (long)disasterViewModel.DisasterId!;
                            siteSpaceDetail.CategoryDetailId = (long)fileUploadModel.FileUploadId;
                            siteSpaceDetail.Estatus = (int)EnumHelpers.EStatus.Active;
                            siteSpaceDetail.EuploadType = (int)EnumHelpers.EUploadType.Pdf;
                            siteSpaceDetail.SiteId = disasterViewModel.SiteId;
                            siteSpaceDetail.UsedSpaceInMb = contentLength / 1000000.0f;
                            addedSpace += siteSpaceDetail.UsedSpaceInMb;
                            await _siteSpaceDetailService.Insert(siteSpaceDetail);
                        }
                    }
                }
            }

            return addedSpace;
        }

        public async Task<float> ProcessFiles(DisasterViewModel disasterViewModel, IList<IFormFile> files, float addedSpace, IList<Task> uploadTasks, string rootPath)
        {
            if (files != null)
            {
                if (files.Count() > 0 && files[0] != null)
                {
                    disasterViewModel.GalleyName = disasterViewModel.Title;
                    disasterViewModel.IsActive = disasterViewModel.IsActive;

                    var disasterGalleryInformation = await InsertUpdateDisasterGalleryAsync(disasterViewModel);

                    var subPath = "Gallery/Disaster/" + disasterViewModel.SiteId + "/" + disasterGalleryInformation.GalleryId;

                    foreach (var file in files)
                    {
                        if (file != null)
                        {
                            var contentLength = file.Length;
                            if (contentLength > 0)
                            {
                                bool exists = Directory.Exists(Path.Combine(rootPath, subPath));

                                if (!exists)
                                {
                                    Directory.CreateDirectory(Path.Combine(rootPath, subPath));
                                }

                                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                                var pathWithFileName = Path.Combine(Path.Combine(rootPath, subPath), fileName);
                                uploadTasks.Add(Task.Run(() => SaveFileToPath(file, pathWithFileName)));

                                disasterViewModel.FileName = fileName;
                                disasterViewModel.FilePath = pathWithFileName;
                                disasterViewModel.PhotoName = Path.GetFileName(file.FileName);
                                disasterViewModel.DisasterId = disasterViewModel.DisasterId;
                                disasterViewModel.GalleryId = disasterViewModel.GalleryId;
                                var disasterPhotoInformaton = await InsertDisasterPhotoFileAsync(disasterViewModel);

                                var siteSpaceDetail = new SiteSpaceDetail();
                                siteSpaceDetail.Ecategory = (int)EnumHelpers.ECategory.Disaster;
                                siteSpaceDetail.CategoryId = (long)disasterViewModel.DisasterId!;
                                siteSpaceDetail.CategoryDetailId = (long)disasterPhotoInformaton.PhotoId;
                                siteSpaceDetail.Estatus = (int)EnumHelpers.EStatus.Active;
                                siteSpaceDetail.EuploadType = (int)EnumHelpers.EUploadType.Image;
                                siteSpaceDetail.SiteId = disasterViewModel.SiteId;
                                siteSpaceDetail.UsedSpaceInMb = contentLength / 1000000.0f;
                                addedSpace += siteSpaceDetail.UsedSpaceInMb;
                                await _siteSpaceDetailService.Insert(siteSpaceDetail);
                            }
                        }
                    }
                }
            }

            return addedSpace;
        }

        private async Task<DisasterViewModel> InsertUpdateDisasterGalleryAsync(DisasterViewModel disasterViewModel)
        {
            try
            {
                if (disasterViewModel.GalleryId != 0 && disasterViewModel.GalleryId != null)
                {
                    var disasterGalleryInformation = _disasterGalleryInformationRepository.Table.Where(x => x.SiteId == disasterViewModel.SiteId && x.GalleryId == disasterViewModel.GalleryId).FirstOrDefault();

                    if (disasterGalleryInformation != null)
                    {
                        disasterGalleryInformation.DisasterId = (long)disasterViewModel.DisasterId;
                        disasterGalleryInformation.GalleyName = disasterViewModel.GalleyName;
                        disasterGalleryInformation.IsActive = disasterViewModel.IsActive;
                        disasterGalleryInformation.SiteId = disasterViewModel.SiteId;
                        disasterGalleryInformation.UpdatedBy = disasterViewModel.CurrentUserId;
                        disasterGalleryInformation.UpdatedOn = new Utilities().GetTokyoDate();
                        await _disasterGalleryInformationRepository.Update(disasterGalleryInformation);

                        disasterViewModel.IsSuccessful = true;
                        disasterViewModel.Message = "Successfully Updated";
                    }
                    else
                    {
                        disasterViewModel.IsSuccessful = false;
                        disasterViewModel.Message = "Cannot find the Gallery Detail";
                    }
                }
                else
                {
                    //Insert
                    var disasterGalleryInformation = new DisasterGalleryInformation();
                    disasterGalleryInformation.CreatedBy = disasterViewModel.CurrentUserId;
                    disasterGalleryInformation.CreatedOn = new Utilities().GetTokyoDate();
                    disasterGalleryInformation.DisasterId = (long)disasterViewModel.DisasterId;
                    disasterGalleryInformation.GalleyName = disasterViewModel.Title;
                    disasterGalleryInformation.IsActive = disasterViewModel.IsActive;
                    disasterGalleryInformation.SiteId = disasterViewModel.SiteId;

                    var createdDisasterGalleryReposiotry = await _disasterGalleryInformationRepository.Add(disasterGalleryInformation);

                    disasterViewModel.GalleryId = createdDisasterGalleryReposiotry.GalleryId;
                    disasterViewModel.IsSuccessful = true;
                    disasterViewModel.Message = "Successfully Inserted";
                }
            }
            catch (Exception ex)
            {
                disasterViewModel.IsSuccessful = false;
                if (disasterViewModel.DisasterId != 0)
                {
                    disasterViewModel.Message = "Failed to Update";
                }
                else
                {
                    disasterViewModel.Message = "Failed to Insert";
                }
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new Utilities().GetTokyoDate();
                entity.ErrorMessage = ex.Message;
                entity.FileName = "BAL:Implementation:User:SetUpService";
                entity.MethodName = "InsertUpdateDisasterGalleryAsync: " + disasterViewModel.DisasterId;
                entity.OperationDoneBy = disasterViewModel.UserName;
                entity.Status = true;
                await _errorMessageInformationRepository.Add(entity);
            }

            return disasterViewModel;
        }

        private async Task<DisasterViewModel> InsertDisasterPhotoFileAsync(DisasterViewModel disasterViewModel)
        {
            try
            {
                var disasterPhotoInformation = new DisasterPhotoInformation();
                disasterPhotoInformation.CreatedBy = disasterViewModel.CurrentUserId;
                disasterPhotoInformation.CreatedOn = new Utilities().GetTokyoDate();
                disasterPhotoInformation.DisasterId = (long)disasterViewModel.DisasterId;
                disasterPhotoInformation.FileName = disasterViewModel.FileName;
                disasterPhotoInformation.FilePath = disasterViewModel.FilePath;
                disasterPhotoInformation.GalleryId = disasterViewModel.GalleryId;
                disasterPhotoInformation.IsActive = disasterViewModel.IsActive;
                disasterPhotoInformation.SiteId = disasterViewModel.SiteId;
                disasterPhotoInformation.PhotoName = disasterViewModel.PhotoName;
                disasterPhotoInformation.CustomName = disasterViewModel.CustomName;

                var createdDisasterPhotoInformation = await _disasterPhotoRepository.Add(disasterPhotoInformation);

                disasterViewModel.PhotoId = createdDisasterPhotoInformation.PhotoId;

                await _changeLogDisasterPhotoInformationService.Insert(createdDisasterPhotoInformation.PhotoId
                        , EnumHelpers.EChangeType.Added, EnumHelpers.ECategory.Disaster, EnumHelpers.EChangeCategory.Image
                        , disasterViewModel.UserName, disasterViewModel.SiteId);

                disasterViewModel.IsSuccessful = true;
                disasterViewModel.Message = "Successfully Inserted";
            }
            catch (Exception e)
            {
                disasterViewModel.IsSuccessful = false;
                disasterViewModel.Message = "Failed to Insert";
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new Utilities().GetTokyoDate();
                entity.ErrorMessage = e.Message;
                entity.FileName = "BAL:Implementation:User:SetUpService";
                entity.MethodName = "InsertDisasterPhotoFileAsync";
                entity.OperationDoneBy = disasterViewModel.UserName;
                entity.Status = true;
                await _errorMessageInformationRepository.Add(entity);
            }

            return disasterViewModel;
        }

        public async Task UpdateDisasterPhotoCustomName(string customName, long photoId, string userName)
        {
            try
            {
                var disasterPhotoInformation = _disasterPhotoRepository.Table.Where(x => x.PhotoId == photoId && x.CustomName != customName).FirstOrDefault();
                if (disasterPhotoInformation != null)
                {
                    disasterPhotoInformation.CustomName = customName;
                    await _disasterPhotoRepository.Update(disasterPhotoInformation);
                }
            }
            catch (Exception e)
            {
                var errorMessageInformation = new ErrorMessageInformation();
                errorMessageInformation.CreatedOn = new Utilities().GetTokyoDate();
                errorMessageInformation.ErrorMessage = e.Message;
                errorMessageInformation.FileName = "BAL:Implementation:User:SetUpService";
                errorMessageInformation.MethodName = "UpdateDisasterPhotoCustomName";
                errorMessageInformation.OperationDoneBy = userName;
                errorMessageInformation.Status = true;
                await _errorMessageInformationRepository.Add(errorMessageInformation);
            }
        }
        #endregion

        public async Task<ResponseViewModel> DeleteDisaster(long disasterId, string siteId, int viewMode, IdentityUser user)
        {
            var responseViewModel = new ResponseViewModel();
            var disasterInformation = _disasterInformationRepository.Table.Where(x => x.SiteId == siteId && x.DisasterId == disasterId && x.ViewMode == viewMode).FirstOrDefault();
            if (disasterInformation != null)
            {
                try
                {
                    bool isDeleteSuccess = await DeleteDisasterFiles(disasterId, siteId, viewMode);
                    if (isDeleteSuccess)
                    {
                        //If successful the delete choureyone
                        await _disasterInformationRepository.Remove(disasterInformation);

                        await _changeLogDisasterInformationService.Insert(disasterId
                            , EnumHelpers.EChangeType.Deleted, EnumHelpers.ECategory.Disaster, EnumHelpers.EChangeCategory.Info
                            , user.UserName, siteId);

                        responseViewModel.IsSuccessful = true;
                        responseViewModel.Message = "Successfully Deleted";
                    }


                }
                catch (Exception e)
                {

                    responseViewModel.IsSuccessful = false;
                    responseViewModel.Message = "Failed to delete";
                    var entity = new ErrorMessageInformation();
                    entity.CreatedOn = new Utilities().GetTokyoDate();
                    entity.ErrorMessage = e.Message;
                    entity.FileName = "BAL:Implementation:User:SetUpService";
                    entity.MethodName = "DeleteDisaster";
                    entity.OperationDoneBy = user.UserName;
                    entity.Status = true;
                    await _errorMessageInformationRepository.Add(entity);
                }
            }
            return responseViewModel;
        }

        private async Task<bool> DeleteDisasterFiles(long disasterId, string siteId, int viewMode)
        {
            try
            {
                var disasterPhotoInformations = _disasterPhotoRepository.Table.Where(x => x.DisasterId == disasterId && x.SiteId == siteId).ToList();
                if (disasterPhotoInformations.Any())
                {
                    await _disasterPhotoRepository.RemoveRange(disasterPhotoInformations);

                }

                var disasterGalleryInformations = _disasterGalleryInformationRepository.Table.Where(x => x.DisasterId == disasterId && x.SiteId == siteId).ToList();
                if (disasterGalleryInformations.Any())
                {
                    await _disasterGalleryInformationRepository.RemoveRange(disasterGalleryInformations);
                }

                var disasterFileUploads = await _fileUploadService.GetUploadedFiles(disasterId, ECategory.Disaster, siteId, (EViewMode)viewMode);
                if(disasterFileUploads.Any())
                {
                    var result = await _fileUploadService.DeleteFiles(disasterFileUploads.ToList());
                }
                return true;
            } 
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> DeleteVideo(long videoId, string siteId, long categoryId, ECategory category, string userName)
        {
            try
            {
                switch(category)
                {
                    case ECategory.ChoureyOne:
                        {
                            var choureyOneVideo = _choureyOneVideoRepository.Table.Where(x => x.VideoId == videoId && x.SiteId == siteId && x.ChoureyOneId == categoryId).FirstOrDefault();

                            if (choureyOneVideo != null)
                            {
                                await _choureyOneVideoRepository.Remove(choureyOneVideo);

                                await _changeLogChoureyOneVideoInformationService.Insert(choureyOneVideo.VideoId
                                            , EnumHelpers.EChangeType.Deleted, EnumHelpers.ECategory.ChoureyOne, EnumHelpers.EChangeCategory.Video
                                            , userName, siteId);

                                return choureyOneVideo.FilePath;
                            }

                            break;
                        }
                    case ECategory.ChoureyTwo:
                        {
                            var choureyTwoVideo = _choureyTwoVideoRepository.Table.Where(x => x.VideoId == videoId && x.SiteId == siteId && x.ChoureyTwoId == categoryId).FirstOrDefault();

                            if (choureyTwoVideo != null)
                            {
                                await _choureyTwoVideoRepository.Remove(choureyTwoVideo);

                                await _changeLogChoureyTwoVideoInformationService.Insert(choureyTwoVideo.VideoId
                                            , EnumHelpers.EChangeType.Deleted, EnumHelpers.ECategory.ChoureyTwo, EnumHelpers.EChangeCategory.Video
                                            , userName, siteId);

                                return choureyTwoVideo.FilePath;
                            }

                            break;
                        }
                    default:
                        {
                            return "";
                        }
                }
            } catch (Exception ex)
            {
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new Utilities().GetTokyoDate();
                entity.ErrorMessage = ex.Message;
                entity.FileName = "BAL:Implementation:User:SetUpService";
                entity.MethodName = "DeleteVideo" + $" for category = {category}";
                entity.OperationDoneBy = userName;
                entity.Status = true;
                await _errorMessageInformationRepository.Add(entity);
            }

            return "";
        }

        public async Task<string> DeletePhoto(long photoId, string siteId, long categoryId, ECategory category, string userName)
        {
            try
            {
                switch (category)
                {
                    case ECategory.ChoureyOne:
                        {
                            var choureyOnePhoto = _choureyOnePhotoRepository.Table.Where(x => x.PhotoId == photoId && x.SiteId == siteId && x.ChoureyOneId == categoryId).FirstOrDefault();

                            if (choureyOnePhoto != null)
                            {
                                await _choureyOnePhotoRepository.Remove(choureyOnePhoto);

                                await _changeLogChoureyOnePhotoInformationService.Insert(choureyOnePhoto.PhotoId
                                            , EnumHelpers.EChangeType.Deleted, EnumHelpers.ECategory.ChoureyOne, EnumHelpers.EChangeCategory.Image
                                            , userName, siteId);

                                return choureyOnePhoto.FilePath;
                            }
                            break;
                        }
                    case ECategory.ChoureyTwo:
                        {
                            var choureyTwoPhoto = _choureyTwoPhotoRepository.Table.Where(x => x.PhotoId == photoId && x.SiteId == siteId && x.ChoureyTwoId == categoryId).FirstOrDefault();

                            if (choureyTwoPhoto != null)
                            {
                                await _choureyTwoPhotoRepository.Remove(choureyTwoPhoto);

                                await _changeLogChoureyTwoVideoInformationService.Insert(choureyTwoPhoto.PhotoId
                                            , EnumHelpers.EChangeType.Deleted, EnumHelpers.ECategory.ChoureyTwo, EnumHelpers.EChangeCategory.Image
                                            , userName, siteId);

                                return choureyTwoPhoto.FilePath;
                            }
                            break;
                        }
                    case ECategory.Disaster:
                        {
                            var disasterPhoto = _disasterPhotoRepository.Table.Where(x => x.PhotoId == photoId && x.SiteId == siteId && x.DisasterId == categoryId).FirstOrDefault();

                            if(disasterPhoto != null)
                            {
                                await _disasterPhotoRepository.Remove(disasterPhoto);

                                await _changeLogDisasterPhotoInformationService.Insert(photoId, EChangeType.Deleted, ECategory.Disaster, EChangeCategory.Image,
                                    userName, siteId);

                                return disasterPhoto.FilePath;
                            }

                            break;
                        }
                    default:
                        {
                            return "";
                        }
                }
            }
            catch (Exception ex)
            {
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new Utilities().GetTokyoDate();
                entity.ErrorMessage = ex.Message;
                entity.FileName = "BAL:Implementation:User:SetUpService";
                entity.MethodName = "DeletePhoto" + $" for category = {category}";
                entity.OperationDoneBy = userName;
                entity.Status = true;
                await _errorMessageInformationRepository.Add(entity);
            }

            return "";
        }

        public async Task<string> DeleteFileUpload(long uploadId, string siteId, long categoryId, ECategory category, string userName)
        {
            try
            {
                var choureyFileUpload = await _fileUploadService.GetUploadedFile(uploadId, categoryId, category, siteId);

                if (choureyFileUpload != null)
                {
                    await _fileUploadService.DeleteFile(choureyFileUpload.FileUploadId, userName);

                    return choureyFileUpload.FilePath;
                }

                return "";
            }
            catch (Exception ex)
            {
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new Utilities().GetTokyoDate();
                entity.ErrorMessage = ex.Message;
                entity.FileName = "BAL:Implementation:User:SetUpService";
                entity.MethodName = "DeleteFileUpload" + $" for category = {category}";
                entity.OperationDoneBy = userName;
                entity.Status = true;
                await _errorMessageInformationRepository.Add(entity);
            }

            return "";
        }

        #region Private Methods
        private async Task<bool> DeleteVideo(ChoureyDeleteModel model)
        {
            bool success = false;
            bool checkExists = false;

            try
            {
                if (model.IsChoureyOne)
                {
                    checkExists = _choureyOneVideoRepository.Table.Any(x => x.ChoureyOneId == model.ChoureyId && x.SiteId == model.SiteId);
                    if (checkExists)
                    {
                        await _choureyOneVideoRepository.RemoveRange(_choureyOneVideoRepository.Table.Where(x => x.ChoureyOneId == model.ChoureyId && x.SiteId == model.SiteId).ToList());
                    }
                }
                else
                {
                    checkExists = _choureyTwoVideoRepository.Table.Any(x => x.ChoureyTwoId == model.ChoureyId && x.SiteId == model.SiteId);
                    if (checkExists)
                    {
                        await _choureyTwoVideoRepository.RemoveRange(_choureyTwoVideoRepository.Table.Where(x => x.ChoureyTwoId == model.ChoureyId && x.SiteId == model.SiteId).ToList());
                    }
                }

                success = true; // Set success to true if the video deletion is attempted, regardless of existence
            }
            catch (Exception ex)
            {
                success = false;
            }

            return success;
        }
        private async Task<bool> DeleteGallery(ChoureyDeleteModel model)
        {
            bool success = false;
            bool photoExists = false;

            try
            {
                if (model.IsChoureyOne)
                {
                    photoExists = _choureyOnePhotoRepository.Table.Any(x => x.ChoureyOneId == model.ChoureyId);
                    if (photoExists)
                    {
                        await _choureyOnePhotoRepository.RemoveRange(_choureyOnePhotoRepository.Table.Where(x => x.ChoureyOneId == model.ChoureyId && x.SiteId == model.SiteId).ToList());
                        await _choureyOneGalleryRepository.RemoveRange(_choureyOneGalleryRepository.Table.Where(x => x.ChoureyOneId == model.ChoureyId && x.SiteId == model.SiteId).ToList());
                    }
                }
                else
                {
                    photoExists = _choureyTwoPhotoRepository.Table.Any(x => x.ChoureyTwoId == model.ChoureyId);
                    if (photoExists)
                    {
                        await _choureyTwoPhotoRepository.RemoveRange(_choureyTwoPhotoRepository.Table.Where(x => x.ChoureyTwoId == model.ChoureyId && x.SiteId == model.SiteId).ToList());
                        await _choureyTwoGalleryRepository.RemoveRange(_choureyTwoGalleryRepository.Table.Where(x => x.ChoureyTwoId == model.ChoureyId && x.SiteId == model.SiteId).ToList());
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
            }

            return success;
        }

        #endregion
    }
}