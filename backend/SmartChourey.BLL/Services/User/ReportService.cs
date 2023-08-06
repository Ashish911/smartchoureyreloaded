using AutoMapper;
using Dapper;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SmartChourey.BLL.Services.User
{
    public class ReportService : IReportService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<SiteInformation> _siteRepository;
        private readonly IRepository<SiteCodeAccessInformation> _siteCodeAccessRepository;
        private readonly IRepository<SiteAccessInformation> _siteAccessInformationRepository;
        private readonly IRepository<EmployeeDetailInformation> _employeeDetailRepository;
        private readonly IRepository<SiteSpaceAggregate> _siteSpaceAggregateRepository;
        private readonly IRepository<UserInformation> _userInformationRepository;
        private readonly IConnectionFactory _connectionFactory;
        public ReportService(
            IMapper mapper,
            IRepository<SiteInformation> siteRepository,
            IRepository<ErrorMessageInformation> errorMessageInformationRepository,
            IRepository<SiteCodeAccessInformation> siteCodeAccessRepository,
            IRepository<SiteAccessInformation> siteAccessInformationRepository,
            IRepository<EmployeeDetailInformation> employeeDetailRepository,
            IRepository<SiteSpaceAggregate> siteSpaceAggregateRepository,
            IConnectionFactory connectionFactory,
            IRepository<UserInformation> userInformationRepository)
        {
            _mapper = mapper;
            _siteRepository = siteRepository;
            _siteAccessInformationRepository = siteAccessInformationRepository;
            _employeeDetailRepository = employeeDetailRepository;
            _siteCodeAccessRepository = siteCodeAccessRepository;
            _siteSpaceAggregateRepository = siteSpaceAggregateRepository;
            _connectionFactory = connectionFactory;
            _userInformationRepository = userInformationRepository;
        }
        public async Task<IEnumerable<ReportAdminSubAdminViewModel>> GetAdminReportBySiteName(string siteName)
        {
            using var connection = _connectionFactory.GetConnection();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SiteName", siteName);
            return await connection.QueryAsync<ReportAdminSubAdminViewModel>("sp_SuperAdminGetReportAdminList", parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<ReportAdminSubAdminViewModel>> GetSubAdminReportBySiteName(string siteName)
        {
            using var connection = _connectionFactory.GetConnection();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SiteName", siteName);
            return await connection.QueryAsync<ReportAdminSubAdminViewModel>("sp_SuperAdminGetReportSubAdminList", parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<ReportDeviceLogViewModel>> GetDeviceLogsBySiteId(DateTime fromDate, DateTime toDate, string siteId)
        {
            using var connection = _connectionFactory.GetConnection();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@siteId", siteId);
            parameters.Add("@fromDate", fromDate);
            parameters.Add("@toDate", toDate);
            return await connection.QueryAsync<ReportDeviceLogViewModel>("sp_GetDeviceLog", parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<ReportQrCodeViewModel>> GetQRCodeReportBySiteId(DateTime fromDate, DateTime toDate, string siteId)
        {
            using var connection = _connectionFactory.GetConnection();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@siteId", siteId);
            parameters.Add("@fromDate", fromDate);
            parameters.Add("@toDate", toDate);
            return await connection.QueryAsync<ReportQrCodeViewModel>("sp_QRCodeReportBySiteId", parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<ReportSafetyDeclarationViewModel>> GetSafetyDeclarationReportBySiteId(DateTime fromDate, DateTime toDate, string siteId)
        {
            using var connection = _connectionFactory.GetConnection();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@siteId", siteId);
            parameters.Add("@fromDate", fromDate);
            parameters.Add("@toDate", toDate);
            return await connection.QueryAsync<ReportSafetyDeclarationViewModel>("sp_SafetyDeclarationReport", parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<ReportMobileSafetyDeclarationViewModel>> GetSafetyDeclarationMobileReportBySiteId(DateTime fromDate, DateTime toDate, string siteId)
        {
            using var connection = _connectionFactory.GetConnection();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@siteId", siteId);
            parameters.Add("@fromDate", fromDate);
            parameters.Add("@toDate", toDate);
            return await connection.QueryAsync<ReportMobileSafetyDeclarationViewModel>("sp_GetSafetyDeclarationMobileReport", parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<DeviceLogViewModel>> GetDeviceLogsReportBySiteId(DateTime fromDate, DateTime toDate, string siteId)
        {
            using var connection = _connectionFactory.GetConnection();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@siteId", siteId);
            parameters.Add("@fromDate", fromDate);
            parameters.Add("@toDate", toDate);
            return await connection.QueryAsync<DeviceLogViewModel>("sp_GetDeviceLog", parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<ReportSmartPhoneUserViewModel>> GetSmartPhoneUserReportBySiteId(string siteId)
        {
            var report = new List<ReportSmartPhoneUserViewModel>();
            var result = _userInformationRepository.Table.Where(x => x.SiteId.ToLower() == siteId.ToLower()).Select(x => new ReportSmartPhoneUserViewModel
            {
                Username = x.UserName,
                PhoneNumber = x.PhoneNumber,
                CompanyName = x.CompanyName,
                SiteName = x.SiteName,
            }).ToList();

            report = result;

            return report;
        }
        public async Task<IEnumerable<ChangeLogViewModel>> GetChangeLogReportBySiteId(DateTime fromDate, DateTime toDate, string siteId)
        {
            using var connection = _connectionFactory.GetConnection();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@siteId", siteId);
            parameters.Add("@fromDate", fromDate);
            parameters.Add("@toDate", toDate);
            return await connection.QueryAsync<ChangeLogViewModel>("sp_GetChangeLogReport", parameters, commandType: CommandType.StoredProcedure);
        }


        public async Task<IEnumerable<UserDetailViewModel>> OnlineUsers(string siteId)
        {
            using var connection = _connectionFactory.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SiteId", siteId);
            IEnumerable<UserDetailViewModel> models = await connection.QueryAsync<UserDetailViewModel>("sp_LoginDetailEmployee", parameters, commandType: CommandType.StoredProcedure);
            return models;
        }
    }
}