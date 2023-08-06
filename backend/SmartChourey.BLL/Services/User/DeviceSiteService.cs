using Dapper;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.ViewModel.SuperAdmin;
using SmartChourey.DAL;
using SmartChourey.DAL.Repositories.Interfaces;
using System.Data;

namespace SmartChourey.BLL.Services.User
{
    public class DeviceSiteService : IDeviceSiteService
    {
        private readonly IRepository<DeviceRegistration> _deviceRegistrationRepository;
        private readonly IRepository<DeviceSiteMapper> _deviceSiteMapperRepository;
        private readonly IConnectionFactory _connectionFactory;
        public DeviceSiteService(
            IConnectionFactory connectionFactory,
            IRepository<DeviceRegistration> deviceRegistrationRepository,
            IRepository<DeviceSiteMapper> deviceSiteMapperRepository)
        {
            _connectionFactory = connectionFactory;
            _deviceRegistrationRepository = deviceRegistrationRepository;
            _deviceSiteMapperRepository = deviceSiteMapperRepository;
        }
        public async Task<bool> AssignDeviceToSite(DeviceSiteMapperViewModel model)
        {
            DeviceSiteMapper data = new DeviceSiteMapper()
            {
                //DeviceSiteMapperId = model.DeviceSiteMapperId,
                FkSiteId = model.FkSiteId,
                FkDeviceRegistrationId = model.FkDeviceRegistrationId
            };

            try
            {
                await _deviceSiteMapperRepository.Add(data);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<DeviceSiteListViewModel> GetDeviceRegistrations()
        {
            using var connection = _connectionFactory.GetConnection();
            DynamicParameters parameters = new DynamicParameters();
            var data = await connection.QueryAsync<DeviceRegistrationViewModel>("sp_DeviceSiteList", parameters, commandType: CommandType.StoredProcedure);
            DeviceSiteListViewModel model = new DeviceSiteListViewModel();
            model.AssignedDevices = data.Where(x => x.IsAssigned == 1).ToList();
            model.UnassignedDevices = data.Where(x => x.IsAssigned == 0).ToList();
            return model;
        }
        public async Task<bool> UpdateDeviceRegistration(DeviceRegistrationUpdateViewModel data)
        {
            var operationStatus = true;
            
            try
            {
                if (!data.NewPhoneNumber.Equals(data.OldPhoneNumber))
                {
                    var deviceRegistrationNumber = _deviceRegistrationRepository.Table.Where(x => x.PhoneNumber == data.NewPhoneNumber).FirstOrDefault();
                    if (deviceRegistrationNumber == null)
                    {
                        var deviceRegistration = _deviceRegistrationRepository.Table.Where(x => x.DeviceRegistrationId == data.DeviceRegistrationId).FirstOrDefault();
                        if (deviceRegistration != null)
                        {
                            deviceRegistration.PhoneNumber = data.NewPhoneNumber;
                            await _deviceRegistrationRepository.Update(deviceRegistration);
                            operationStatus = true;
                        }
                        else
                        {
                            operationStatus = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                operationStatus = false;
            }
            return operationStatus;
        }

        public async Task<bool> DeleteDeviceRegistration(int deviceRegistrationId)
        {
            var operationStatus = true;

            var deviceRegistration = _deviceRegistrationRepository.Table.Where(x => x.DeviceRegistrationId == deviceRegistrationId).FirstOrDefault();
            if (deviceRegistration != null)
            {
                await _deviceRegistrationRepository.Remove(deviceRegistration);
                var deviceSiteMappers = _deviceSiteMapperRepository.Table.Where(x => x.FkDeviceRegistrationId == deviceRegistrationId).ToList();
                if (deviceSiteMappers.Count != 0)
                {
                    await _deviceSiteMapperRepository.RemoveRange(deviceSiteMappers);
                }
                operationStatus = true;
            }
            return operationStatus;
        }
    }
}