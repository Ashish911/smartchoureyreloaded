using BusinessLogicLayer.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SmartChourey.BLL.Services.Interfaces.User;
using System.Text;


namespace SmartChourey.BLL.Configuration
{
    public interface IHelpers
    {
        EnumHelpers.EViewMode GetViewMode(string viewModeSession);
        Task<float> GetSpaceUsed(string siteId);
        Task<int> GetSpaceAllocated(string siteId);
        Task<string> GetSiteName(string siteId);
        Task<bool> IsSpaceLimitExceeded(IEnumerable<IFormFile> images, IEnumerable<IFormFile> videos, string siteId);
    }

    public class Helpers: IHelpers
    {
        private readonly ISiteSpaceAggregateService _siteSpaceAggregateService;
        private readonly ISiteInformationService _siteInformationService;
        private static IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public Helpers(ISiteSpaceAggregateService siteSpaceAggregateService, ISiteInformationService siteInformationService, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _siteSpaceAggregateService = siteSpaceAggregateService;
            _siteInformationService = siteInformationService;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public EnumHelpers.EViewMode GetViewMode(string viewModeSession)
        {
            var resolvedEnum = EnumHelpers.EViewMode.Normal;
            if (viewModeSession != null)
            {
                resolvedEnum = (EnumHelpers.EViewMode)Enum.Parse(typeof(EnumHelpers.EViewMode), viewModeSession);
            }
            return resolvedEnum;
        }

        public async Task<float> GetSpaceUsed(string siteId)
        {
            float spaceUsed;
            if (_httpContextAccessor.HttpContext.Session.GetString("SpaceUsed") == null || _httpContextAccessor.HttpContext.Session.GetString("AllocatedSpace") == null)
            {
                var siteSpaceAggregate = await _siteSpaceAggregateService.GetAggregateSpace(siteId);

                if(siteSpaceAggregate == null)
                {
                    throw new Exception("SiteSpace Not allocated");
                }

                _httpContextAccessor.HttpContext.Session.SetString("SpaceUsed", siteSpaceAggregate.UsedSpaceInMb.ToString());
                _httpContextAccessor.HttpContext.Session.SetString("AllocatedSpace", siteSpaceAggregate.AllocatedSpace.ToString());
                spaceUsed = siteSpaceAggregate.UsedSpaceInMb;
            }
            else
            {
                spaceUsed = Convert.ToSingle(_httpContextAccessor.HttpContext.Session.GetString("SpaceUsed"));
            }
            return spaceUsed;
        }

        public async Task<int> GetSpaceAllocated(string siteId)
        {
            int spaceAllocated;
            if (_httpContextAccessor.HttpContext.Session.GetString("SpaceUsed") == null || _httpContextAccessor.HttpContext.Session.GetString("AllocatedSpace") == null)
            {
                var siteSpaceAggregate = await _siteSpaceAggregateService.GetAggregateSpace(siteId);
                _httpContextAccessor.HttpContext.Session.SetString("SpaceUsed", siteSpaceAggregate.UsedSpaceInMb.ToString());
                _httpContextAccessor.HttpContext.Session.SetString("AllocatedSpace", siteSpaceAggregate.AllocatedSpace.ToString());
                spaceAllocated = siteSpaceAggregate.AllocatedSpace;
            }
            else
            {
                spaceAllocated = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("AllocatedSpace"));
                if (spaceAllocated == 0)
                {
                    spaceAllocated = Convert.ToInt32(_configuration.GetSection("AllocatedSpace"));
                }
            }
            return spaceAllocated;
        }

        public async Task<string> GetSiteName(string siteId)
        {
            string siteName;
            if (_httpContextAccessor.HttpContext.Session.GetString("SiteName") == null)
            {
                siteName = await _siteInformationService.GetSiteName(siteId);
            }
            else
            {
                siteName = _httpContextAccessor.HttpContext.Session.GetString("SiteName");
            }
            return siteName;
        }

        public static string GetConnectionId(object connectionObject)
        {
            string connectionId = String.Empty;
            if (connectionObject != null)
            {
                connectionId = connectionObject.ToString();
            }
            return connectionId;
        }

        public async Task<bool> IsSpaceLimitExceeded(IEnumerable<IFormFile> images, IEnumerable<IFormFile> videos, string siteId)
        {
            var currentSpace = await GetSpaceUsed(siteId);
            float imageSize = 0.0f, videoSize = 0.0f;
            foreach (var image in images)
            {
                if (image != null)
                    imageSize = image.Length / 1000000.0f;
            }
            foreach (var video in videos)
            {
                if (video != null)
                    videoSize = video.Length / 1000000.0f;
            }
            var hasSpaceLimitExceeded = (currentSpace + imageSize + videoSize) > await GetSpaceAllocated(siteId);
            return hasSpaceLimitExceeded;
        }

        public static string StripHtmlTags(string html)
        {
            if (String.IsNullOrEmpty(html)) return "";
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            return System.Net.WebUtility.HtmlDecode(doc.DocumentNode.InnerText).Trim();
        }

        public static string SubstringWithDot(string fullString)
        {
            var subString = "";
            if (fullString.Length <= 200)
            {
                subString = fullString;
            }
            else
            {
                var indexOfSpace = fullString.IndexOf(' ', 190);
                subString = subString.Substring(0, indexOfSpace == -1 ? 200 : indexOfSpace);
            }
            return subString.Trim() + " . . .";
        }
    }
}

