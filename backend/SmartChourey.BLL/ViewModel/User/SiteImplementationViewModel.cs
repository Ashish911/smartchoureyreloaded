using System.ComponentModel.DataAnnotations;

namespace SmartChourey.BLL.ViewModel.User
{
    public class SiteImplementationViewModel: UserStatusViewModel
    {
        #region siteDetail 
        public string? Id { get; set; }
        public string? SiteName { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? PeriodStart { get; set; }
        public string? PeriodEnd { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? BrowseTimeFrom { get; set; }
        public string? BrowzeTimeFrom_Hr { get; set; }
        public string? BrowzeTimeFrom_Min { get; set; }
        public string? BrowzeTimeFrom_AMPM { get; set; }
        public string? BrowseTimeTo { get; set; }
        public string? BrowzeTimeTo_Hr { get; set; }
        public string? BrowzeTimeTo_Min { get; set; }
        public string? BrowzeTimeTo_AMPM { get; set; }
        public string? QrCodeValue { get; set; }
        public Nullable<int> GPSRange { get; set; }
        public string? SiteAccessCode { get; set; }
        #endregion
        #region UserSite
        public long UserSiteId { get; set; }
        public string? UserId { get; set; }
        public string? SiteId { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
        public Nullable<bool> IsSubAdmin { get; set; }
        #endregion

    }
    public class SiteCreateViewModel
    {
        public string? SiteName { get; set; }
        public string? QrCodeValue { get; set; }
        public string? PeriodStart { get; set; }
        public string? PeriodEnd { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? BrowseTimeFrom { get; set; }
        public string? BrowseTimeTo { get; set; }
        public Nullable<int> GPSRange { get; set; }
        public string? UserId { get; set; }
        public string? SiteId { get; set; }
        public string? SiteAccessCode { get; set; }
        public string? UserName { get; set; }
    }

}
