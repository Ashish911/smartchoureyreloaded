using SmartChourey.BLL.ViewModel.Admin;
using SmartChourey.BLL.ViewModel.SuperAdmin;
using SmartChourey.BLL.ViewModel.User;
using SmartChourey.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.Services.Interfaces.User
{
    public interface IReportService
    {
        Task<IEnumerable<ReportQrCodeViewModel>> GetQRCodeReportBySiteId(DateTime fromDate, DateTime toDate, string siteId);
        Task<IEnumerable<ReportSafetyDeclarationViewModel>> GetSafetyDeclarationReportBySiteId(DateTime fromDate, DateTime toDate, string siteId);
        Task<IEnumerable<ReportMobileSafetyDeclarationViewModel>> GetSafetyDeclarationMobileReportBySiteId(DateTime fromDate, DateTime toDate, string siteId);
        Task<IEnumerable<DeviceLogViewModel>> GetDeviceLogsReportBySiteId(DateTime fromDate, DateTime toDate, string siteId);
        Task<IEnumerable<ReportSmartPhoneUserViewModel>> GetSmartPhoneUserReportBySiteId(string siteId);
        Task<IEnumerable<ChangeLogViewModel>> GetChangeLogReportBySiteId(DateTime fromDate, DateTime toDate, string siteId);
        Task<IEnumerable<ReportAdminSubAdminViewModel>> GetAdminReportBySiteName(string siteName);
        Task<IEnumerable<ReportAdminSubAdminViewModel>> GetSubAdminReportBySiteName(string siteName);
        Task<IEnumerable<ReportDeviceLogViewModel>> GetDeviceLogsBySiteId(DateTime fromDate, DateTime toDate, string siteId);
        Task<IEnumerable<UserDetailViewModel>> OnlineUsers(string siteId);
    }
}
