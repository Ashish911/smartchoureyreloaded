using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartChourey.BLL.Configuration;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.Utilites;
using SmartChourey.BLL.ViewModel;
using SmartChourey.BLL.ViewModel.Admin;
using SmartChourey.BLL.ViewModel.User;
using SmartChourey.DAL;
using SmartChourey.DAL.Repositories.Interfaces;
using System.Device.Location;

namespace SmartChourey.BLL.Services.User
{
    public class SiteService : ISiteService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<SiteInformation> _siteRepository;
        private readonly IRepository<SiteCodeAccessInformation> _siteCodeAccessRepository;
        private readonly IRepository<SiteAccessInformation> _siteAccessInformationRepository;
        private readonly IRepository<EmployeeDetailInformation> _employeeDetailRepository;
        private readonly IRepository<UserSiteInformation> _userSiteInformationRepository;
        private readonly IRepository<Qrlog> _qrlogRepository;
        private readonly IRepository<ErrorMessageInformation> _errorMessageInformationRepository;

        public SiteService(
            IMapper mapper,
            IRepository<SiteInformation> siteRepository,
            IRepository<ErrorMessageInformation> errorMessageInformationRepository,
            IRepository<SiteCodeAccessInformation> siteCodeAccessRepository,
            IRepository<SiteAccessInformation> siteAccessInformationRepository,
            IRepository<EmployeeDetailInformation> employeeDetailRepository,
            IRepository<UserSiteInformation> userSiteInformationRepository,
            IRepository<Qrlog> qrlogRepository)
        {
            _mapper = mapper;
            _siteRepository = siteRepository;
            _siteAccessInformationRepository = siteAccessInformationRepository;
            _employeeDetailRepository = employeeDetailRepository;
            _siteCodeAccessRepository = siteCodeAccessRepository;
            _userSiteInformationRepository = userSiteInformationRepository;
            _qrlogRepository = qrlogRepository;
            _errorMessageInformationRepository = errorMessageInformationRepository;
        }
        public async Task<ResponseViewModel> InsertAsync(SiteCreateViewModel model)
        {
            ResponseViewModel retVal = new ResponseViewModel();
            try
            {
                var siteExists = await _siteRepository.Table.AnyAsync(x => x.SiteName == model.SiteName);
                if (siteExists)
                {
                    retVal.IsSuccessful = false;
                    retVal.Message = $"Site Name '{model.SiteName}' Already Exists. Please Use a different Name";
                    return retVal;
                }

                string siteId = Guid.NewGuid().ToString();
                while (await _siteRepository.Table.AnyAsync(x => x.Id == siteId))
                {
                    siteId = Guid.NewGuid().ToString();
                }

                model.SiteId = siteId;

                var checkAccessCode = await UpdateSiteCodeAccessInformationAsync(model);
                if (checkAccessCode)
                {

                    var entity = new SiteInformation
                    {
                        Id = siteId,
                        SiteName = model.SiteName,
                        PeriodEnd = model.PeriodEnd,
                        PeriodStart = model.PeriodStart,
                        Latitude = model.Latitude,
                        Longitude = model.Longitude,
                        QrCodeValue = model.QrCodeValue,
                        Address = model.Address,
                        City = model.City,
                        Country = model.Country ?? "Japan",
                        BrowseTimeFrom = model.BrowseTimeFrom,
                        BrowseTimeTo = model.BrowseTimeTo,
                        Gpsrange = model.GPSRange,
                        IsTrial = true,
                        TrialExpireOn = new DateTimeHelper().GetTokyoDate().AddMonths(1),
                        CreatedBy = model.UserId,
                        CreatedOn = new DateTimeHelper().GetTokyoDate(),
                        IsActive = true
                    };

                    entity.Id = Guid.NewGuid().ToString();
                    while (await _siteRepository.Table.AnyAsync(x => x.Id == entity.Id))
                    {
                        entity.Id = Guid.NewGuid().ToString();
                    }
                    model.SiteId = entity.Id;

                    await _siteRepository.Add(entity);

                    // Update or Insert to User_Site
                    var response = await UserSiteInsertUpdate(model);
                    if (!response.IsSuccessful)
                    {
                        return response;
                    }
                    retVal.IsSuccessful = true;
                    retVal.Message = "Successfully Record Inserted";
                }
                else
                {
                    retVal.Message = "Please Enter a valid Site Code";
                    retVal.IsSuccessful = false;
                }

                return retVal;
            }
            catch (Exception e)
            {
                retVal.IsSuccessful = false;
                retVal.Message = "Sorry, Record has not been Inserted";
                var entity = new ErrorMessageInformation
                {
                    CreatedOn = new DateTimeHelper().GetTokyoDate(),
                    ErrorMessage = e.Message,
                    FileName = "BAL:Implementation:User:SiteDetailImplementation",
                    MethodName = "Insert",
                    OperationDoneBy = model.UserName,
                    Status = true
                };
                return retVal;
            }
        }
        public async Task<List<SiteDetailSiteComponentDetailViewModel>> GetSitesByAdmin(string userId, string roleName)
        {
            var resultList = new List<SiteDetailSiteComponentDetailViewModel>();
            List<SiteInformation> result = new List<SiteInformation>();

            if (roleName == "SuperAdmin")
            {
                result = (from si in _siteRepository.Table
                          select si).ToList();
            }
            else
            {
                result = (from si in _siteRepository.Table
                          join sai in _siteAccessInformationRepository.Table on si.Id equals sai.SiteId
                          where sai.SiteAccessGivenTo == userId
                          select si).ToList();
            }

            foreach (var item in result)
            {
                var model = new SiteDetailSiteComponentDetailViewModel();
                model.Id = item.Id;
                model.SiteName = item.SiteName;
                model.PeriodStart = (Convert.ToDateTime(item.PeriodStart.Trim())).ToString("yyyy/MM/dd");
                model.PeriodEnd = (Convert.ToDateTime(item.PeriodEnd.Trim())).ToString("yyyy/MM/dd");
                model.Address = item.Address;
                model.City = item.City;
                model.Country = item.Country;
                model.IsActive = item.IsActive;
                model.GPSRange = item.Gpsrange;
                model.BrowseTimeFrom = item.BrowseTimeFrom;
                model.BrowseTimeTo = item.BrowseTimeTo;
                model.QRCodeValue = item.QrCodeValue;
                model.Longitude = item.Longitude;
                model.Latitude = item.Latitude;
                resultList.Add(model);
            }
            return resultList;
        }
        public async Task<SiteDetailSiteComponentDetailViewModel> GetSitesDetails(string siteId)
        {
            var result = (from si in _siteRepository.Table
                          join sai in _siteAccessInformationRepository.Table on si.Id equals sai.SiteId
                          where sai.SiteId == siteId
                          select si).FirstOrDefault();

            var model = new SiteDetailSiteComponentDetailViewModel();
            model.Id = result.Id;
            model.SiteName = result.SiteName;
            model.PeriodStart = (Convert.ToDateTime(result.PeriodStart.Trim())).ToString("yyyy/MM/dd");
            model.PeriodEnd = (Convert.ToDateTime(result.PeriodEnd.Trim())).ToString("yyyy/MM/dd");
            model.Address = result.Address;
            model.City = result.City;
            model.Country = result.Country;
            model.IsActive = result.IsActive;
            model.GPSRange = result.Gpsrange;
            model.BrowseTimeFrom = result.BrowseTimeFrom;
            model.BrowseTimeTo = result.BrowseTimeTo;
            model.QRCodeValue = result.QrCodeValue;
            model.Longitude = result.Longitude;
            model.Latitude = result.Latitude;
            return model;
        }
        public async Task<SiteImplementationViewModel> SiteDetailByQRCodeAsync(string QRCode, string UserId, string UserName, string latitude, string longitude)
        {
            var model = new SiteImplementationViewModel();
            var currentDate = DateTime.Now;
            var result = _siteRepository.Table.Where(x => x.QrCodeValue == QRCode).FirstOrDefault();

            if (result != null)
            {
                if (latitude != null && longitude != null && result.Latitude != null && result.Longitude != null)
                {
                    var sCoord = new GeoCoordinate(Convert.ToDouble(latitude), Convert.ToDouble(longitude));
                    var eCoord = new GeoCoordinate(Convert.ToDouble(result.Latitude), Convert.ToDouble(result.Longitude));
                    if (sCoord.GetDistanceTo(eCoord) > result.Gpsrange)
                    {
                        model.IsSuccessful = false;
                        model.Message = "You are not in Office range";
                        return model;
                    }
                }

                var fromDate = Convert.ToDateTime(result.BrowseTimeFrom);
                var toDate = Convert.ToDateTime(result.BrowseTimeTo);
                if (fromDate.Date <= currentDate.Date && toDate.Date >= currentDate.Date)
                {
                    model.Id = result.Id;
                    model.SiteId = result.Id;
                    model.SiteName = result.SiteName;
                    model.BrowseTimeFrom = result.BrowseTimeFrom;
                    model.BrowseTimeTo = result.BrowseTimeTo;
                    model.GPSRange = result.Gpsrange;
                    model.QrCodeValue = result.QrCodeValue;

                    DateTime timeFrom = Convert.ToDateTime(model.BrowseTimeFrom + ":59");
                    DateTime timeTo = Convert.ToDateTime(model.BrowseTimeTo + ":59");
                    bool AccessSite = false;

                    if (timeFrom.TimeOfDay <= currentDate.TimeOfDay && timeTo.TimeOfDay >= currentDate.TimeOfDay)
                    {
                        AccessSite = true;
                    }
                    if (AccessSite)
                    {
                        var userSiteModel = new SiteImplementationViewModel();
                        userSiteModel.UserId = UserId;
                        userSiteModel.SiteId = model.SiteId;
                        userSiteModel.UserName = UserName;
                        var result1 = DeleteAndInsertUserSiteInformation(userSiteModel);
                        model.IsSuccessful = true;
                    }
                    else
                    {
                        model.IsSuccessful = false;
                        model.Message = "You are not in office Time";
                    }
                }
                else
                {
                    model.IsSuccessful = false;
                    model.Message = "Date expried for Site";
                }
            }
            else
            {
                model.IsSuccessful = false;
                model.Message = "Please Scan Valid Site QR Code";
            }
            return model;
        }

        private async Task<bool> UpdateSiteCodeAccessInformationAsync(SiteCreateViewModel model)
        {
            var getContext = _siteCodeAccessRepository.Table.Where(x => x.SiteCode == model.SiteAccessCode && x.IsActive == true && x.IsOccupied == false).FirstOrDefault();
            if (getContext != null)
            {
                getContext.IsOccupied = true;
                getContext.OccupiedOn = new DateTimeHelper().GetTokyoDate();
                getContext.SiteId = model.SiteId;
                getContext.UpdatedBy = model.UserId;
                getContext.UpdatedOn = new DateTimeHelper().GetTokyoDate();
                getContext.UserId = model.UserId;
                await _siteCodeAccessRepository.Update(getContext);
                return true;
            }
            return false;
        }
        private async Task<ResponseViewModel> UserSiteInsertUpdate(SiteCreateViewModel model)
        {
            ResponseViewModel retVal = new ResponseViewModel();

            try
            {
                var entity = await _siteAccessInformationRepository.Table.FirstOrDefaultAsync(x => x.UserId == model.UserId && x.SiteId == model.SiteId);
                if (entity != null)
                {
                    //Update
                    entity.SiteId = model.SiteId;
                    entity.UserId = model.UserId;
                    entity.SiteAccessGivenTo = model.UserId;
                    entity.JoinedOn = new DateTimeHelper().GetTokyoDate();

                    await _siteAccessInformationRepository.Update(entity);
                }
                else
                {
                    //Insert
                    entity = new SiteAccessInformation();
                    entity.SiteId = model.SiteId;
                    entity.UserId = model.UserId;
                    entity.SiteAccessGivenTo = model.UserId;
                    entity.JoinedOn = new DateTimeHelper().GetTokyoDate();

                    await _siteAccessInformationRepository.Add(entity);
                }

                retVal.IsSuccessful = await UpdateEmployeeTable(model.UserId, model.UserName);

            }
            catch (Exception e)
            {

                retVal.IsSuccessful = false;
                retVal.Message = "Sorry, Record has not been Inserted, Failed to insert UserDetail";
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new DateTimeHelper().GetTokyoDate();
                entity.ErrorMessage = e.Message;
                entity.FileName = "BAL:Implementation:User:SiteDetailImplementation";
                entity.MethodName = "UserSiteInsertUpdate";
                entity.OperationDoneBy = model.UserName;
                entity.Status = true;
            }
            return retVal;
        }
        private async Task<bool> UpdateEmployeeTable(string UserId, string UserName)
        {
            bool success = false;
            try
            {
                var EmpTable = _employeeDetailRepository.Table.Where(x => x.UserId == UserId).FirstOrDefault();
                if (EmpTable != null)
                {
                    EmpTable.RoleName = "Admin";
                    EmpTable.UpdatedBy = UserId;
                    EmpTable.UpdatedOn = new DateTimeHelper().GetTokyoDate();
                    await _employeeDetailRepository.Update(EmpTable);
                    success = true;
                }
            }
            catch (Exception e)
            {

                success = false;
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new DateTimeHelper().GetTokyoDate();
                entity.ErrorMessage = e.Message;
                entity.FileName = "BAL:Implementation:User:SiteDetailImplementation";
                entity.MethodName = "UpdateEmployeeTable";
                entity.OperationDoneBy = UserName;
                entity.Status = true;
            }
            return success;
        }

        private async Task<SiteImplementationViewModel> DeleteAndInsertUserSiteInformation(SiteImplementationViewModel model)
        {
            try
            {
                var data = _userSiteInformationRepository.Table.Where(x => x.UserId == model.UserId).FirstOrDefault();
                if (data != null)
                {
                    //Delete And Insert
                    var deleteRecord = _userSiteInformationRepository.Remove(data);
                    model.IsSuccessful = await InsertUserSiteInformation(model);
                }
                else
                {
                    //Insert
                    model.IsSuccessful = await InsertUserSiteInformation(model);
                }

            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }

        private async Task<bool> InsertUserSiteInformation(SiteImplementationViewModel model)
        {
            try
            {
                var entity = new UserSiteInformation();
                entity.SiteId = model.SiteId;
                entity.UserId = model.UserId;
                entity.IsAdmin = false;
                entity.IsSubAdmin = false;
                entity.JoinedOn = new DateTimeHelper().GetTokyoDate();
                entity.UserAddedBy = model.UserId;
                await _userSiteInformationRepository.Add(entity);
                model.IsSuccessful = true;
                InsertQRLog(model);
            }
            catch (Exception e)
            {

                model.IsSuccessful = false;
                model.Message = "Sorry, Record has not been Inserted, Failed to insert UserDetail";
                //var entity = new ErrorMessage_Information();
                //entity.CreatedOn = new Utilities().GetTokyoDate();
                //entity.ErrorMessage = e.Message;
                //entity.FileName = "BAL:Implementation:User:SiteDetailImplementation";
                //entity.MethodName = "UserSiteInsertUpdateUser";
                //entity.OperationDoneBy = model.UserName;
                //entity.Status = true;
                //context.ErrorMessage_Information.Add(entity);
                //context.SaveChanges();
            }
            return model.IsSuccessful;
        }
        private void InsertQRLog(SiteImplementationViewModel model)
        {
            //Check did he/she already scan QR or not 
            var getCurrentDate = new DateTimeHelper().GetTokyoDate().ToShortDateString();
            var QrScan = _qrlogRepository.Table.Where(x => x.UserId == model.UserId && x.SiteId == model.SiteId).OrderByDescending(x => x.QrlogId).FirstOrDefault();

            if (QrScan == null)
            {
                //if not  then insert to table QRLogs.
                var table = new Qrlog();
                table.UserId = model.UserId;
                table.SiteId = model.SiteId;
                table.EntryTime = new DateTimeHelper().GetTokyoDate();
                _qrlogRepository.Add(table);
            }
            else
            {
                if (QrScan.EntryTime.ToShortDateString() != getCurrentDate)
                {
                    var table = new Qrlog();
                    table.UserId = model.UserId;
                    table.SiteId = model.SiteId;
                    table.EntryTime = new DateTimeHelper().GetTokyoDate();
                    _qrlogRepository.Add(table);
                }
            }
        }

        public async Task<bool> CheckIfSiteExist(string siteId)
        {
            var siteInformation = _siteRepository.Table.Where(x => x.Id == siteId).FirstOrDefault();
            if(siteInformation == null)
            {
                return false;
            }

            return true;
        }

        public async Task<SiteDetailSiteComponentDetailViewModel> UpdateSite(SiteDetailSiteComponentDetailViewModel siteDetailSiteComponentDetailViewModel)
        {
            try
            {
                var siteInformation = await _siteRepository.Table.Where(x => x.Id == siteDetailSiteComponentDetailViewModel.Id).FirstOrDefaultAsync();
                var isSiteDuplicate = _siteRepository.Table.Where(x => x.Id != siteDetailSiteComponentDetailViewModel.Id && x.SiteName == siteDetailSiteComponentDetailViewModel.SiteName).Any();
                if (!isSiteDuplicate)
                {
                    if (siteInformation != null)
                    {
                        siteInformation.Id = siteDetailSiteComponentDetailViewModel.Id;
                        siteInformation.SiteName = siteDetailSiteComponentDetailViewModel.SiteName;
                        siteInformation.PeriodEnd = siteDetailSiteComponentDetailViewModel.PeriodEnd;
                        siteInformation.PeriodStart = siteDetailSiteComponentDetailViewModel.PeriodStart;
                        siteInformation.Address = siteDetailSiteComponentDetailViewModel.Address;
                        siteInformation.City = siteDetailSiteComponentDetailViewModel.City;
                        siteInformation.Country = siteDetailSiteComponentDetailViewModel.Country;
                        siteInformation.BrowseTimeFrom = siteDetailSiteComponentDetailViewModel.BrowseTimeFrom;
                        siteInformation.BrowseTimeTo = siteDetailSiteComponentDetailViewModel.BrowseTimeTo;
                        siteInformation.Gpsrange = siteDetailSiteComponentDetailViewModel.GPSRange;
                        siteInformation.QrCodeValue = siteDetailSiteComponentDetailViewModel.QRCodeValue;
                        siteInformation.UpdatedBy = siteDetailSiteComponentDetailViewModel.CurrentUserId;
                        siteInformation.UpdatedOn = new Utilities().GetTokyoDate();
                        siteInformation.IsActive = true;
                        siteInformation.Longitude = siteDetailSiteComponentDetailViewModel.Longitude;
                        siteInformation.Latitude = siteDetailSiteComponentDetailViewModel.Latitude;
                        await _siteRepository.Update(siteInformation);

                        siteDetailSiteComponentDetailViewModel.IsSuccessful = true;
                        siteDetailSiteComponentDetailViewModel.IsActive = true;
                        siteDetailSiteComponentDetailViewModel.Message = "Successfully Site Updated";
                    }
                }
                else
                {
                    siteDetailSiteComponentDetailViewModel.IsSuccessful = false;
                    siteDetailSiteComponentDetailViewModel.Message = "Site Name already exist. Please user another Site Name";
                }
            }
            catch (Exception e)
            {

                siteDetailSiteComponentDetailViewModel.IsSuccessful = false;
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new Utilities().GetTokyoDate();
                entity.ErrorMessage = e.Message;
                entity.FileName = "BAL:Implementation:User:SiteService";
                entity.MethodName = "UpdateSite";
                entity.OperationDoneBy = siteDetailSiteComponentDetailViewModel.UserName;
                entity.Status = true;
                await _errorMessageInformationRepository.Add(entity);
            }

            return siteDetailSiteComponentDetailViewModel;
        }

        public async Task<bool> AssignSubAdminToSite(string siteId, string AdminUserId, string AdminUserName, string SubAdminUserId)
        {
            try
            {
                var userHasSiteAccess = _siteAccessInformationRepository.Table.Where(x => x.SiteId == siteId && x.SiteAccessGivenTo == SubAdminUserId).FirstOrDefault();
                if (userHasSiteAccess != null)
                {
                    return true;
                }
                else
                {
                    var siteAccessInformation = new SiteAccessInformation();
                    siteAccessInformation.SiteId = siteId;
                    siteAccessInformation.UserId = AdminUserId;
                    siteAccessInformation.SiteAccessGivenTo = SubAdminUserId;
                    siteAccessInformation.JoinedOn = new Utilities().GetTokyoDate();
                    await _siteAccessInformationRepository.Add(siteAccessInformation);

                    return true;
                }
            }
            catch (Exception ex)
            {
                var entity = new ErrorMessageInformation();
                entity.CreatedOn = new Utilities().GetTokyoDate();
                entity.ErrorMessage = ex.Message;
                entity.FileName = "BLL:Service:User:SiteService";
                entity.MethodName = "AssignSubAdminToSite";
                entity.OperationDoneBy = AdminUserName;
                entity.Status = true;
                await _errorMessageInformationRepository.Add(entity);

                return false;
            }
        }
    }
}