using Dapper;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.Utilites;
using SmartChourey.BLL.ViewModel.Admin;
using SmartChourey.BLL.ViewModel.Userboard;
using SmartChourey.DAL;
using SmartChourey.DAL.Repositories.Interfaces;
using System.Data;
using ChoureyOneViewModel = SmartChourey.BLL.ViewModel.Userboard.ChoureyOneViewModel;
using ChoureyTwoViewModel = SmartChourey.BLL.ViewModel.Userboard.ChoureyTwoViewModel;
using DisasterViewModel = SmartChourey.BLL.ViewModel.Userboard.DisasterViewModel;

namespace SmartChourey.BLL.Services.User
{
    public class UserboardService : IUserboardService
    {
        private IRepository<UserSiteDeclarationInformation> _userDeclarationInformationRepository;
        private readonly IConnectionFactory _connectionFactory;
        public UserboardService(
            IRepository<UserSiteDeclarationInformation> userDeclarationInformationRepository,
            IConnectionFactory connectionFactory)
        {
            _userDeclarationInformationRepository = userDeclarationInformationRepository;
            _connectionFactory = connectionFactory;
        }

        public async Task<UserBoardDashboardViewModel> GetUserBoard(string siteId, string userId, int viewMode)
        {
            var model = new UserBoardDashboardViewModel();
            var result = new SiteDeclarationViewModel();
            var choureyOneList = new List<UserBoardChoureyDto>();
            var choureyTwoList = new List<UserBoardChoureyDto>();
            var disasterList = new List<UserBoardChoureyDto>();

            using var connection = _connectionFactory.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@siteId", siteId);
            parameters.Add("@userId", userId);
            parameters.Add("@viewMode", viewMode);

            using (var multi = await connection.QueryMultipleAsync("sp_UserBoard", parameters, commandType: CommandType.StoredProcedure))
            {
                model = multi.Read<UserBoardDashboardViewModel>().FirstOrDefault();
                result = multi.Read<SiteDeclarationViewModel>().FirstOrDefault();
                choureyOneList = multi.Read<UserBoardChoureyDto>().ToList();
                choureyTwoList = multi.Read<UserBoardChoureyDto>().ToList();
                disasterList = multi.Read<UserBoardChoureyDto>().ToList();
            }

            if (model == null)
            {
                model = new UserBoardDashboardViewModel();
            }
            if (model.SiteDeclarationId != null && model.UserSiteDeclarationId == null && !model.isEditable)
            {
                var table = new UserSiteDeclarationInformation();
                table.CreatedOn = new DateTimeHelper().GetTokyoDate();
                table.IsChecked = true;
                table.SiteId = siteId;
                table.UserId = userId;
                table.SiteDeclarationId = model.SiteDeclarationId.Value;

                await _userDeclarationInformationRepository.Add(table);
            }

            model.SiteDeclarationList = result;
            model.choureyOneModelList = GetChoureyOneList(choureyOneList);
            model.choureyTwoModelList = GetChoureyTwoList(choureyTwoList);
            model.disasterModelList = GetDisasterList(disasterList);
            return model;
        }

        public List<ChoureyOneViewModel> GetChoureyOneList(List<UserBoardChoureyDto> dtos)
        {
            var resultList = new List<ChoureyOneViewModel>();
            var distinctDatas = dtos.GroupBy(x => x.Id)
                            .Select(grp => grp.First());
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            foreach (var item in distinctDatas)
            {
                doc.LoadHtml(item.Description ?? "");
                var sanitizedDescription = doc.DocumentNode.InnerText;
                var datas = dtos.Where(x => x.Id == item.Id);
                var photos = new List<PhotoInfoViewModel>();
                var videos = new List<VideoInfoViewModel>();
                foreach (var data in datas)
                {
                    if (data.PhotoId != null)
                    {
                        var photo = new PhotoInfoViewModel();
                        photo.GalleryId = data.GalleryId;
                        photo.PhotoId = data.PhotoId;
                        photo.PhotoName = data.PhotoName;
                        photo.CustomName = data.PhotoCustomName;
                        photo.FileName = data.PhotoFileName;
                        photo.FilePath = data.PhotoFilePath;
                        photos.Add(photo);
                    }
                    if (data.VideoId != null)
                    {
                        var video = new VideoInfoViewModel();
                        video.VideoId = data.VideoId;
                        video.choureyId = data.Id;
                        video.FileName = data.VideoFileName;
                        video.CustomName = data.VideoCustomName;
                        videos.Add(video);
                    }
                }
                var choureyOneViewModel = new ChoureyOneViewModel();
                choureyOneViewModel.ChoureyOneId = (int)item.Id;
                choureyOneViewModel.Title = item.Title;
                choureyOneViewModel.Description = sanitizedDescription.Length > 200 ? sanitizedDescription.Substring(0, 200) : sanitizedDescription;
                choureyOneViewModel.Position = item.Position;
                choureyOneViewModel.GalleryId = item.GalleryId;
                choureyOneViewModel.VideoId = item.VideoId;
                choureyOneViewModel.SiteId = item.SiteId;
                choureyOneViewModel.videoListModel = videos;
                choureyOneViewModel.PhotoListModel = photos;
                resultList.Add(choureyOneViewModel);
            }
            return resultList;
        }

        public List<ChoureyTwoViewModel> GetChoureyTwoList(List<UserBoardChoureyDto> dtos)
        {
            var resultList = new List<ChoureyTwoViewModel>();
            var distinctDatas = dtos.GroupBy(x => x.Id)
                            .Select(grp => grp.First());
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            foreach (var item in distinctDatas)
            {
                doc.LoadHtml(item.Description ?? "");
                var sanitizedDescription = doc.DocumentNode.InnerText;
                var datas = dtos.Where(x => x.Id == item.Id);
                var photos = new List<PhotoInfoViewModel>();
                var videos = new List<VideoInfoViewModel>();
                foreach (var data in datas)
                {
                    if (data.PhotoId != null)
                    {
                        var photo = new PhotoInfoViewModel();
                        photo.GalleryId = data.GalleryId;
                        photo.PhotoId = data.PhotoId;
                        photo.PhotoName = data.PhotoName;
                        photo.CustomName = data.PhotoCustomName;
                        photo.FileName = data.PhotoFileName;
                        photo.FilePath = data.PhotoFilePath;
                        photos.Add(photo);
                    }
                    if (data.VideoId != null)
                    {
                        var video = new VideoInfoViewModel();
                        video.VideoId = data.VideoId;
                        video.choureyId = data.Id;
                        video.FileName = data.VideoFileName;
                        video.CustomName = data.VideoCustomName;
                        videos.Add(video);
                    }
                }
                var choureyOneViewModel = new ChoureyTwoViewModel();
                choureyOneViewModel.ChoureyTwoId = item.Id;
                choureyOneViewModel.Title = item.Title;
                choureyOneViewModel.Description = sanitizedDescription.Length > 200 ? sanitizedDescription.Substring(0, 200) : sanitizedDescription;
                choureyOneViewModel.Position = item.Position;
                choureyOneViewModel.GalleryId = item.GalleryId;
                choureyOneViewModel.VideoId = item.VideoId;
                choureyOneViewModel.SiteId = item.SiteId;
                choureyOneViewModel.videoListModel = videos;
                choureyOneViewModel.PhotoListModel = photos;
                resultList.Add(choureyOneViewModel);
            }
            return resultList;
        }

        public List<DisasterViewModel> GetDisasterList(List<UserBoardChoureyDto> dtos)
        {
            var resultList = new List<DisasterViewModel>();
            var distinctDatas = dtos.GroupBy(x => x.Id)
                            .Select(grp => grp.First());
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            foreach (var item in distinctDatas)
            {
                doc.LoadHtml(item.Description ?? "");
                var sanitizedDescription = doc.DocumentNode.InnerText;
                var datas = dtos.Where(x => x.Id == item.Id);
                var photos = new List<PhotoInfoViewModel>();
                var videos = new List<VideoInfoViewModel>();
                foreach (var data in datas)
                {
                    if (data.PhotoId != null)
                    {
                        var photo = new PhotoInfoViewModel();
                        photo.GalleryId = data.GalleryId;
                        photo.PhotoId = data.PhotoId;
                        photo.PhotoName = data.PhotoName;
                        photo.CustomName = data.PhotoCustomName;
                        photo.FileName = data.PhotoFileName;
                        photo.FilePath = data.PhotoFilePath;
                        photos.Add(photo);
                    }
                }
                var disasterViewModel = new DisasterViewModel();
                disasterViewModel.DisasterId = item.Id;
                disasterViewModel.Title = item.Title;
                disasterViewModel.Description = sanitizedDescription.Length > 200 ? sanitizedDescription.Substring(0, 200) : sanitizedDescription;
                disasterViewModel.Position = item.Position;
                disasterViewModel.GalleryId = item.GalleryId;
                disasterViewModel.VideoId = item.VideoId;
                disasterViewModel.SiteId = item.SiteId;
                disasterViewModel.PhotoListModel = photos;
                resultList.Add(disasterViewModel);
            }
            return resultList;
        }
    }
}