using AutoMapper;
using BusinessLogicLayer.Configuration;
using Microsoft.AspNetCore.Http;
using SmartChourey.BLL.Dtos;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.ViewModel.SuperAdmin;
using SmartChourey.DAL;
using SmartChourey.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;
using static Dapper.SqlMapper;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace SmartChourey.BLL.Services.User
{
    public class SiteSpaceAggregateService : ISiteSpaceAggregateService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<SiteSpaceAggregate> _siteSpaceAggregateRepository;
        private readonly IConnectionFactory _connectionFactory;
        private static IHttpContextAccessor? _httpContextAccessor;

        public SiteSpaceAggregateService(IMapper mapper, IRepository<SiteSpaceAggregate> siteSpaceAggregateRepository, IConnectionFactory connectionFactory, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _siteSpaceAggregateRepository = siteSpaceAggregateRepository;
            _connectionFactory = connectionFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<SiteSpaceAggregate?> GetAggregateSpace(string siteId)
        {
            return await _siteSpaceAggregateRepository.Table.FirstOrDefaultAsync(x => x.SiteId == siteId);
        }

        public bool InsertAndUpdate(SiteSpaceAggregate siteSpaceAggregate)
        {
            bool isSuccess = false;
            try
            {
                float spaceUsed = siteSpaceAggregate.UsedSpaceInMb;
                var entity = _siteSpaceAggregateRepository.Table.Where(x => x.SiteId == siteSpaceAggregate.SiteId).FirstOrDefault();
                if (entity != null)
                {
                    entity.UsedSpaceInMb += siteSpaceAggregate.UsedSpaceInMb;
                    spaceUsed = entity.UsedSpaceInMb;
                    _siteSpaceAggregateRepository.Update(entity);
                }
                else
                {
                    siteSpaceAggregate.Estatus = (int)EnumHelpers.EStatus.Active;
                    _siteSpaceAggregateRepository.Add(siteSpaceAggregate);
                }
                isSuccess = true;
                _httpContextAccessor.HttpContext.Session.SetString("SpaceUsed", spaceUsed.ToString());

            }
            catch (Exception)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public async Task<SiteSpaceAggregateDto> InsertAndUpdateSpaceAllocation(string siteId, int newAllocatedSpace)
        {
            try
            {
                SiteSpaceAggregate? siteSpaceAggregate = await _siteSpaceAggregateRepository.Table.FirstOrDefaultAsync(x => x.SiteId == siteId);

                if (siteSpaceAggregate != null)
                {
                    siteSpaceAggregate.AllocatedSpace = newAllocatedSpace;
                    await _siteSpaceAggregateRepository.Update(siteSpaceAggregate);
                }
                else
                {
                    siteSpaceAggregate = new SiteSpaceAggregate
                    {
                        Estatus = (int)EnumHelpers.EStatus.Active,
                        AllocatedSpace = newAllocatedSpace,
                        SiteId = siteId,
                        UsedSpaceInMb = 0.0f
                    };
                    await _siteSpaceAggregateRepository.Add(siteSpaceAggregate);
                }
                var siteSpaceAggregateDto = _mapper.Map<SiteSpaceAggregateDto>(siteSpaceAggregate);
                return siteSpaceAggregateDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<SiteSpaceAggregateViewModel>> GetAggregateSpaces()
        {
            using var connection = _connectionFactory.GetConnection();
            return await connection.QueryAsync<SiteSpaceAggregateViewModel>("sp_GetSitesStorage", CommandType.StoredProcedure);
        }
    }
}
