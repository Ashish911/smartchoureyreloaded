namespace SmartChourey.BLL.ViewModel.SuperAdmin
{
    public class DeviceRegistrationViewModel
    {
        public int DeviceRegistrationId { get; set; }
        public string DeviceUniqueId { get; set; }
        public string PhoneNumber { get; set; }
        public string DeviceType { get; set; }
        public int IsAssigned { get; set; }
    }
    public class DeviceSiteListViewModel
    {
        public IList<DeviceRegistrationViewModel> AssignedDevices { get; set; }
        public IList<DeviceRegistrationViewModel> UnassignedDevices { get; set; }
    }
    public class DeviceRegistrationUpdateViewModel
    {
        public long DeviceRegistrationId { get; set; }
        public string NewPhoneNumber { get; set; }
        public string OldPhoneNumber { get; set; }
        public string DeviceUniqueId { get; set; }
    }
    public class DeviceSiteMapperViewModel
    {
        public string FkSiteId { get; set; }
        public long FkDeviceRegistrationId { get; set; }
    }
}
