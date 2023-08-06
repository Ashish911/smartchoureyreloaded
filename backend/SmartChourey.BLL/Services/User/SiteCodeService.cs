using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.Utilites;
using SmartChourey.BLL.ViewModel.SuperAdmin;
using SmartChourey.BLL.ViewModel.User;
using SmartChourey.DAL;
//using SmartChourey.DAL;
using SmartChourey.DAL.Context;
using SmartChourey.DAL.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace SmartChourey.BLL.Services.User
{
    public class SiteCodeService : ISiteCodeService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<SiteInformation> _siteRepository;
        private readonly IRepository<SiteCodeAccessInformation> _siteCodeAccessRepository;
        private readonly IRepository<SiteAccessInformation> _siteAccessInformationRepository;
        private readonly IRepository<EmployeeDetailInformation> _employeeDetailRepository;
        private readonly IRepository<UserSiteInformation> _userSiteInformationRepository;
        public SiteCodeService(
            IMapper mapper,
            IRepository<SiteInformation> siteRepository,
            IRepository<ErrorMessageInformation> errorMessageInformationRepository,
            IRepository<SiteCodeAccessInformation> siteCodeAccessRepository,
            IRepository<SiteAccessInformation> siteAccessInformationRepository,
            IRepository<EmployeeDetailInformation> employeeDetailRepository,
            IRepository<UserSiteInformation> userSiteInformationRepository
         )
        {
            _mapper = mapper;
            _siteRepository = siteRepository;
            _siteAccessInformationRepository = siteAccessInformationRepository;
            _siteCodeAccessRepository = siteCodeAccessRepository;
            _employeeDetailRepository = employeeDetailRepository;
            _userSiteInformationRepository = userSiteInformationRepository;
        }
        public async Task<SiteCodeAccessViewModel> GenerateSiteCodeAccessAsync(SiteCodeAccessViewModel model)
        {
            try
            {
                var checkSiteCodeExistorNot = _siteCodeAccessRepository.Table.Where(x => x.SiteCode == model.SiteCode).Any();
                if (!checkSiteCodeExistorNot)
                {
                    var checkDetail = _siteCodeAccessRepository.Table.Where(x => x.SiteCodeId == model.SiteCodeId).FirstOrDefault();

                    if (checkDetail != null) {
                        checkDetail.SiteCodeId = model.SiteCodeId;
                        checkDetail.IsActive = model.IsActive;
                        checkDetail.SiteCode = model.SiteCode;
                        checkDetail.UpdatedBy = model.CurrentUserId;
                        checkDetail.UpdatedOn = new DateTimeHelper().GetTokyoDate();
                        await _siteCodeAccessRepository.Update(checkDetail);
                        model.IsSuccess = true;
                        model.Message = "Successfully Code Updated";
                        return model;
                    }
                    else
                    {
                        var table = new SiteCodeAccessInformation();
                        table.CreatedBy = model.CurrentUserId;
                        table.CreatedDate = new DateTimeHelper().GetTokyoDate();
                        table.IsActive = true;
                        table.IsOccupied = false;
                        table.SiteCode = model.SiteCode;
                        table.UserId = model.CurrentUserId;
                        await _siteCodeAccessRepository.Add(table);
                        model.IsSuccess = true;
                        model.Message = "Successfully Site Code Generated";
                        return model;
                    }
                }
                else
                {
                    model.IsSuccess = false;
                    model.Message = "Site Code Already Exists";
                    return model;
                }
            }
            catch (Exception e)
            {
                model.IsSuccess = false;
                model.Message = "Sorry, Record has not been Inserted";
                var entity = new ErrorMessageInformation
                {
                    CreatedOn = new DateTimeHelper().GetTokyoDate(),
                    ErrorMessage = e.Message,
                    FileName = "BAL:Implementation:User:SiteDetailImplementation",
                    MethodName = "Insert",
                    OperationDoneBy = model.UserName,
                    Status = true
                };
                return model;
            }
        }
        public async Task<bool> AccessSiteCreation(string code)
        {
            bool result = _siteCodeAccessRepository.Table.Where(x => x.SiteCode == code && x.IsOccupied == false && x.IsActive == true).Any();
            return result;
        }

        public async Task<List<SiteCodeAccessViewModel>> ListSiteCodes()
        {
            var data = _siteCodeAccessRepository.Table.OrderByDescending(x => x.SiteCodeId).ToList();

            List<SiteCodeAccessViewModel> list = new List<SiteCodeAccessViewModel>();
            foreach (var item in data)
            {
                var model = new SiteCodeAccessViewModel();
                model.SiteCodeId = item.SiteCodeId;
                model.IsActive = item.IsActive;
                model.IsOccupied = item.IsOccupied;
                model.SiteCode = item.SiteCode;
                model.SiteId = item.SiteId;
                model.SiteName = _siteRepository.Table.Where(x => x.Id == item.SiteId).Select(x => x.SiteName).FirstOrDefault();
                list.Add(model);
            }

            return list;
        }

        public async Task<bool> DeleteSiteCode(long siteCodeId)
        {
            var siteCodeAccessViewModel = new SiteCodeAccessViewModel();

            try
            {
                var siteCodeInformation = _siteCodeAccessRepository.Table.Where(x => x.SiteCodeId == siteCodeId).FirstOrDefault();
                if (siteCodeInformation != null)
                {
                    await _siteCodeAccessRepository.Remove(siteCodeInformation);

                    var siteCodeAccessInformations = _siteAccessInformationRepository.Table.Where(x => x.SiteId == siteCodeInformation.SiteId).ToList();
                    if (siteCodeAccessInformations.Any())
                    {
                        foreach (var siteCodeAccessInformation in siteCodeAccessInformations)
                        {
                            await _siteAccessInformationRepository.Remove(siteCodeAccessInformation);
                        }
                    }

                    var userSiteInformations = _userSiteInformationRepository.Table.Where(x => x.SiteId == siteCodeInformation.SiteId).ToList();
                    if (userSiteInformations.Any())
                    {
                        foreach (var userSiteInformation in userSiteInformations)
                        {
                            await _userSiteInformationRepository.Remove(userSiteInformation);
                        }
                    }

                    var siteInformation = _siteRepository.Table.Where(x => x.Id == siteCodeInformation.SiteId).FirstOrDefault();
                    if (siteInformation != null)
                    {
                        await _siteRepository.Remove(siteInformation);
                    }

                    return true;
                }

                return false;
            }
            catch(Exception ex)
            {
                throw;

            }
        }
    }
}