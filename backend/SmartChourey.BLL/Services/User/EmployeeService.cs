using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.Utilites;
using SmartChourey.BLL.ViewModel.Admin;
using SmartChourey.BLL.ViewModel.SuperAdmin;
using SmartChourey.BLL.ViewModel.User;
using SmartChourey.DAL;
//using SmartChourey.DAL;
using SmartChourey.DAL.Context;
using SmartChourey.DAL.Repositories.Interfaces;
using System;
using System.Data;
using System.Threading.Tasks;
using static BusinessLogicLayer.Configuration.EnumHelpers;

namespace SmartChourey.BLL.Services.User
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<EmployeeDetailInformation> _employeeDetailInformationRepository;
        private readonly IRepository<SiteAccessInformation> _siteAccessInformationRepository;
        private readonly IConnectionFactory _connectionFactory;
        //private readonly IRepository<LoginDetailInformation> _loginDetailInformationRepository;
        //private readonly IRepository<ErrorMessageInformation> _errorMessageInformationRepository;
        public EmployeeService(
            IMapper mapper,
            IRepository<EmployeeDetailInformation> employeeDetailInformationRepository,
            IRepository<LoginDetailInformation> loginDetailInformationRepository,
            IRepository<ErrorMessageInformation> errorMessageInformationRepository,
            IRepository<SiteAccessInformation> siteAccessInformationRepository,
            IConnectionFactory connectionFactory
         )
        {
            _mapper = mapper;
            _employeeDetailInformationRepository = employeeDetailInformationRepository;
            _siteAccessInformationRepository = siteAccessInformationRepository;
            _connectionFactory = connectionFactory;
        }
        public async Task<EmployeeDetailViewModel> InsertAsync(EmployeeDetailViewModel model)
        {
            var user = await _employeeDetailInformationRepository.Table.FirstOrDefaultAsync(x => x.Email == model.Email);
            model.IsSuccessful = true;
            if (user == null)
            {
                var entity = _mapper.Map<EmployeeDetailInformation>(model);

                entity.CreatedBy = model.UserId;
                entity.CreatedOn = new DateTimeHelper().GetTokyoDate();

                if (string.IsNullOrEmpty(model.DOB))
                {
                    entity.Dob = new DateTimeHelper().GetTokyoDate().ToShortTimeString();
                }
                if (string.IsNullOrEmpty(model.Country))
                {
                    entity.Country = "Japan";
                }
                try
                {
                    await _employeeDetailInformationRepository.Add(entity);
                    model.IsSuccessful = true;
                    model.Message = "Successfully Record Inserted";
                    LoginDetailInsert(model);
                }
                catch (Exception e)
                {
                    model.IsSuccessful = false;
                    model.Message = "Sorry, Record has not been Inserted";
                    var errorObject = new ErrorMessageInformation();
                    errorObject.CreatedOn = new DateTimeHelper().GetTokyoDate();
                    errorObject.ErrorMessage = e.Message;
                    errorObject.FileName = "BAL:Implementation:User:EmployeeImplementation";
                    errorObject.MethodName = "Insert";
                    errorObject.OperationDoneBy = model.UserName;
                    errorObject.Status = true;
                    //await _errorMessageInformationRepository.Add(errorObject);
                }
            }
            else
            {
                model.IsSuccessful = false;
                model.Message = "Sorry, Record has not been Inserted";
            }
            return model;
        }

        public async Task<EmployeeDetailViewModel> GetProfile(string id)
        {
            using var connection = _connectionFactory.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserId", id);
            IEnumerable<EmployeeDetailViewModel> models = await connection.QueryAsync<EmployeeDetailViewModel>("sp_EmployeeDetail", parameters, commandType: CommandType.StoredProcedure);
            return models.FirstOrDefault();
        }
        public async Task<EmployeeDetailViewModel> UpdateAsync(EmployeeDetailViewModel model)
        {
            //todo photo
            try
            {
                var entity = await _employeeDetailInformationRepository.Get((long)model.EmployeeId);
                //model.UserId = model.CurrentUserId;
                //model.RoleName = "User";
                //model.Id = model.EmployeeId;
                entity.FamilyNameKana = model.FamilyName_Kana;
                entity.GivenNameKana = model.GivenName_Kana;
                entity.FamilyNameRoman = model.FamilyName_Roman;
                entity.GivenNameRoman = model.GivenName_Roman;
                entity.FamilyNameChinese = model.FamilyName_Chinese;
                entity.GivenNameChinese = model.GivenName_Chinese;
                entity.Dob = model.DOB;
                entity.PhoneNumber = model.phoneNumber;
                entity.MobileNumber = model.MobileNumber;
                entity.EmergencyContactNumber = model.EmergencyContactNumber;
                entity.Country = model.Country;
                entity.Postbox = model.Postbox;
                entity.Prefecture = model.Prefecture;
                entity.City = model.City;
                entity.Address = model.Address;
                entity.UpdatedOn = new DateTimeHelper().GetTokyoDate();
                entity.UpdatedBy = model.CurrentUserId;
                await _employeeDetailInformationRepository.Update(entity);
                model.IsSuccessful = true;
                model.Message = "Successfully Record Updated";
            }
            catch
            {
                model.IsSuccessful = false;
                model.Message = "Sorry, Record Has not been Updated";
            }
            return model;
        }
        public async Task<UserDetailViewModel> AssignSubAdmin(string SiteId, string Email, string UserName, string SubAdminUserId, string AdminUserId)
        {
            var model = new UserDetailViewModel();
            model.UserName = UserName;

            try
            {
                var user = _employeeDetailInformationRepository.Table.Where(x => x.Email == Email).FirstOrDefault();
                if (user != null)
                {
                    var entityToUpdate = _siteAccessInformationRepository.Table.Where(x => x.SiteAccessGivenTo == user.UserId && x.SiteId == SiteId).FirstOrDefault();
                    if (entityToUpdate != null)
                    {
                        SubAdminUserId = user.UserId;
                        entityToUpdate.SiteId = SiteId;
                        entityToUpdate.UserId = AdminUserId;
                        entityToUpdate.SiteAccessGivenTo = user.UserId;
                        entityToUpdate.JoinedOn = new DateTimeHelper().GetTokyoDate();
                        await _siteAccessInformationRepository.Update(entityToUpdate);
                        model.IsSuccessful = true;
                        await UpdateEmployeeTableRole(SubAdminUserId, Email, model.UserName);
                        model.UserId = user.UserId;
                        model.Message = "Successfully SubAdmin Assigned";
                    }
                    else
                    {
                        SiteAccessInformation siteAccessInformation = new SiteAccessInformation()
                        {
                            SiteId = SiteId,
                            JoinedOn = new DateTimeHelper().GetTokyoDate(),
                            UserId = AdminUserId,
                            SiteAccessGivenTo = user.UserId
                        };
                        await _siteAccessInformationRepository.Add(siteAccessInformation);
                        model.IsSuccessful = true;
                        await UpdateEmployeeTableRole(user.UserId, Email, model.UserName);
                        model.UserId = user.UserId;
                        model.Message = "Successfully SubAdmin Assigned";
                    }
                }
            }
            catch (Exception e)
            {

                model.IsSuccessful = false;
                model.Message = "Unable to Update Records. Invalid";
            }
            return model;
        }
        private void LoginDetailInsert(EmployeeDetailViewModel model)
        {
            try
            {
                var entity = new LoginDetailInformation();
                entity.UserId = model.UserId;
                entity.CreatedDate = new DateTimeHelper().GetTokyoDate();
                //_loginDetailInformationRepository.Add(entity);
            }
            catch (Exception e)
            {
                var errorObject = new ErrorMessageInformation();
                errorObject.CreatedOn = new DateTimeHelper().GetTokyoDate();
                errorObject.ErrorMessage = e.Message;
                errorObject.FileName = "BAL:Implementation:User:EmployeeImplementation";
                errorObject.MethodName = "LoginDetailInsert";
                errorObject.OperationDoneBy = model.UserName;
                errorObject.Status = true;
                //_errorMessageInformationRepository.Add(errorObject);
            }
        }

        private async Task<bool> UpdateEmployeeTableRole(string userId, string Email, string UserName)
        {
            bool success = false;
            try
            {
                var getEmployeeDetail = _employeeDetailInformationRepository.Table.Where(x => x.Email == Email).FirstOrDefault();
                if (getEmployeeDetail != null)
                {
                    getEmployeeDetail.RoleName = "SubAdmin";
                    await _employeeDetailInformationRepository.Update(getEmployeeDetail);
                    success = true;
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        public async Task<UserDetailViewModel> CheckUserExist(string Email)
        {
            try
            {
                var userDetailViewModel = new UserDetailViewModel();
                var result = _employeeDetailInformationRepository.Table.Where(x => x.Email == Email).FirstOrDefault();
                if (result != null)
                {
                    userDetailViewModel.UserId = result.UserId;
                    userDetailViewModel.Email = result.Email;
                }
                return userDetailViewModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}