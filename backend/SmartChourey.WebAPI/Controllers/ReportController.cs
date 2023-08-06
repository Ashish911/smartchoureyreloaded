using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.ViewModel.Admin;


namespace SmartChourey.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IReportService _reportService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReportController(UserManager<IdentityUser> userManager, IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("getAdminReportBySiteName")]
        public async Task<IEnumerable<ReportAdminSubAdminViewModel>> GetAdminReportBySiteName(string? siteName)
        {
            return await _reportService.GetAdminReportBySiteName(siteName);
        }

        [HttpGet("getSubAdminReportBySiteName")]
        public async Task<IEnumerable<ReportAdminSubAdminViewModel>> GetSubAdminReportBySiteName(string? siteName)
        {
            return await _reportService.GetSubAdminReportBySiteName(siteName);
        }

        [HttpGet("getDeviceLogsBySiteId")]
        public async Task<IEnumerable<ReportDeviceLogViewModel>> GetDeviceLogsBySiteId(DateTime fromDate, DateTime toDate, string? siteId)
        {
            return await _reportService.GetDeviceLogsBySiteId(fromDate, toDate, siteId);
        }

        [HttpGet("getQRCodeReportBySiteId")]
        public async Task<IEnumerable<ReportQrCodeViewModel>> GetQRCodeReportBySiteId(DateTime fromDate, DateTime toDate, string siteId)
        {
            return await _reportService.GetQRCodeReportBySiteId(fromDate, toDate, siteId);
        }

        [HttpGet("getSafetyDeclarationReportBySiteId")]
        public async Task<IEnumerable<ReportSafetyDeclarationViewModel>> GetSafetyDeclarationReportBySiteId(DateTime fromDate, DateTime toDate, string siteId)
        {
            return await _reportService.GetSafetyDeclarationReportBySiteId(fromDate, toDate, siteId);
        }

        [HttpGet("getSafetyDeclarationMobileReportBySiteId")]
        public async Task<IEnumerable<ReportMobileSafetyDeclarationViewModel>> GetSafetyDeclarationMobileReportBySiteId(DateTime fromDate, DateTime toDate, string siteId)
        {
            return await _reportService.GetSafetyDeclarationMobileReportBySiteId(fromDate, toDate, siteId);
        }

        [HttpGet("getDeviceLogsReportBySiteId")]
        public async Task<IEnumerable<DeviceLogViewModel>> GetDeviceLogsReportBySiteId(DateTime fromDate, DateTime toDate, string siteId)
        {
            return await _reportService.GetDeviceLogsReportBySiteId(fromDate, toDate, siteId);
        }

        [HttpGet("getSmartPhoneUserReportBySiteId")]
        public async Task<IEnumerable<ReportSmartPhoneUserViewModel>> GetSmartPhoneUserReportBySiteId(string siteId)
        {
            return await _reportService.GetSmartPhoneUserReportBySiteId(siteId);
        }

        [HttpGet("getChangeLogReportBySiteId")]
        public async Task<IEnumerable<ChangeLogViewModel>> GetChangeLogReportBySiteId(DateTime fromDate, DateTime toDate, string siteId)
        {
            return await _reportService.GetChangeLogReportBySiteId(fromDate, toDate, siteId);
        }

        [HttpGet("getLoggedInUsersBySiteId")]
        public async Task<IEnumerable<UserDetailViewModel>> GetLoggedInUsersBySiteId(string siteId)
        {
            return await _reportService.OnlineUsers(siteId);
        }
    }
}