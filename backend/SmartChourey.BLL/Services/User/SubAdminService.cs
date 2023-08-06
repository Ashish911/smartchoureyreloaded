using AutoMapper;
using BusinessLogicLayer.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.Utilites;
using SmartChourey.BLL.ViewModel;
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

namespace SmartChourey.BLL.Services.User
{
    public class SubAdminService : ISubAdminService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<ChoureyOneInformation> _choureyOneRepository;
        private readonly IRepository<ChoureyTwoInformation> _choureyTwoRepository;
        private readonly IRepository<ChoureyOnePhotoInformation> _choureyOnePhotoRepository;
        private readonly IRepository<ChoureyOneVideoInformation> _choureyOneVideoRepository;
        private readonly IRepository<ChoureyTwoPhotoInformation> _choureyTwoPhotoRepository;
        private readonly IRepository<ChoureyTwoVideoInformation> _choureyTwoVideoRepository;
        private readonly IRepository<ChoureyOneGalleryInformation> _choureyOneGalleryRepository;
        private readonly IRepository<ChoureyTwoGalleryInformation> _choureyTwoGalleryRepository;
        private readonly IRepository<DisasterPhotoInformation> _disasterPhotoRepository;
        private readonly IRepository<SiteInformation> _siteRepository;
        private readonly IRepository<SiteCodeAccessInformation> _siteCodeAccessRepository;
        private readonly IRepository<SiteAccessInformation> _siteAccessInformationRepository;
        private readonly IRepository<EmployeeDetailInformation> _employeeDetailRepository;
        private readonly IRepository<SiteSpaceAggregate> _siteSpaceAggregateRepository;
        private readonly IConnectionFactory _connectionFactory;
        public SubAdminService(
            IMapper mapper,
            IRepository<ChoureyOneInformation> choureyOneRepository,
            IRepository<ChoureyTwoInformation> choureyTwoRepository,
            IRepository<ChoureyOnePhotoInformation> choureyOnePhotoRepository,
            IRepository<ChoureyOneVideoInformation> choureyOneVideoRepository,
            IRepository<ChoureyTwoPhotoInformation> choureyTwoPhotoRepository,
            IRepository<ChoureyTwoVideoInformation> choureyTwoVideoRepository,
            IRepository<ChoureyOneGalleryInformation> choureyOneGalleryRepository,
            IRepository<ChoureyTwoGalleryInformation> choureyTwoGalleryRepository,
            IRepository<DisasterPhotoInformation> disasterPhotoRepository,
            IRepository<SiteInformation> siteRepository,
            IRepository<ErrorMessageInformation> errorMessageInformationRepository,
            IRepository<SiteCodeAccessInformation> siteCodeAccessRepository,
            IRepository<SiteAccessInformation> siteAccessInformationRepository,
            IRepository<EmployeeDetailInformation> employeeDetailRepository,
            IRepository<SiteSpaceAggregate> siteSpaceAggregateRepository,
            IConnectionFactory connectionFactory)
        {
            _mapper = mapper;
            _choureyOneRepository = choureyOneRepository;
            _choureyTwoRepository = choureyTwoRepository;
            _choureyOnePhotoRepository = choureyOnePhotoRepository;
            _choureyOneVideoRepository = choureyOneVideoRepository;
            _choureyTwoPhotoRepository = choureyTwoPhotoRepository;
            _choureyTwoVideoRepository = choureyTwoVideoRepository;
            _choureyOneGalleryRepository = choureyOneGalleryRepository;
            _choureyTwoGalleryRepository = choureyTwoGalleryRepository;
            _disasterPhotoRepository = disasterPhotoRepository;
            _siteRepository = siteRepository;
            _siteAccessInformationRepository = siteAccessInformationRepository;
            _employeeDetailRepository = employeeDetailRepository;
            _siteCodeAccessRepository = siteCodeAccessRepository;
            _siteSpaceAggregateRepository = siteSpaceAggregateRepository;
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> DeleteSiteSubAdmin(long employeeId)
        {
            var employeeDetailViewModel = new EmployeeDetailViewModel();
            try
            {
                var employeeInformation = _employeeDetailRepository.Table.Where(x => x.Id == employeeId).FirstOrDefault();
                if (employeeInformation != null)
                {
                    await _employeeDetailRepository.Remove(employeeInformation);
                    return true;
                }

                return false;
            } catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UserDetailViewModel>> ListSubAdminBySiteId(string siteId)
        {
            using var connection = _connectionFactory.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@siteId", siteId);
            return (await connection.QueryAsync<UserDetailViewModel>("sp_SubAdminList", parameters, commandType: CommandType.StoredProcedure)).ToList();
        }
    }
}