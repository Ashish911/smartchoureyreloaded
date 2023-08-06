using SmartChourey.BLL.ViewModel.SuperAdmin;

namespace SmartChourey.BLL.Services.Interfaces.User
{
    public interface IDeviceSiteService
    {
        Task<DeviceSiteListViewModel> GetDeviceRegistrations();
        Task<bool> UpdateDeviceRegistration(DeviceRegistrationUpdateViewModel data);
        Task<bool> DeleteDeviceRegistration(int deviceRegistrationId);
        Task<bool> AssignDeviceToSite(DeviceSiteMapperViewModel model);
    }
}
