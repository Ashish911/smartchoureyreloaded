using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.ViewModel.Admin
{
    public class ReportAdminSubAdminViewModel
    {
        public string SiteName { get; set; }
        public string FullName_Kana { get; set; }
        public string FullName_Kanji { get; set; }
        public string Email { get; set; }
        public string SiteTimePeriod { get; set; }
        public DateTime? SiteCreatedDate { get; set; }
        public DateTime? JoinedOn { get; set; }
        public int? LoggedInCount { get; set; }
    }
    public class ReportDeviceLogViewModel
    {
        public string DeviceUniqueId { get; set; }
        public string PhoneNumber { get; set; }
        public string Date { get; set; }
        public string SiteName { get; set; }
        public string Time { get; set; }
    }
    public class ReportQrCodeViewModel
    {
        public string SiteName { get; set; }
        public string FullName_Kana { get; set; }
        public string FullName_Kanji { get; set; }
        public string Email { get; set; }
        public string EntryDate { get; set; }
        public string EntryTime { get; set; }
        public string Fullname { get; set; }
        public string KanaName { get; set; }
        public string CompanyName { get; set; }
    }
    public class ReportSafetyDeclarationViewModel
    {
        public string SiteName { get; set; }
        public string FullName_Kana { get; set; }
        public string FullName_Kanji { get; set; }
        public string EntryDate { get; set; }
        public string EntryTime { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string KanaName { get; set; }
        public string Is_Checked { get; set; }
        public string CompanyName { get; set; }
    }
    public class ReportMobileSafetyDeclarationViewModel
    {
        public string Title { get; set; }
        public string SiteName { get; set; }
        public string PhoneNumber { get; set; }
        public string DeviceType { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
    public class ReportSmartPhoneUserViewModel
    {
        public string Username { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string SiteName { get; set; }
    }
    public class DeviceLogViewModel
    {
        public string DeviceUniqueId { get; set; }
        public string PhoneNumber { get; set; }
        public string Date { get; set; }
        public string SiteName { get; set; }
        public string Time { get; set; }
    }

    public class ChangeLogViewModel
    {
        public string Email { get; set; }
        public string Category { get; set; }
        public string ChangeCategory { get; set; }
        public string ChangeType { get; set; }
        public string ChangedProperties { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

    }
}
