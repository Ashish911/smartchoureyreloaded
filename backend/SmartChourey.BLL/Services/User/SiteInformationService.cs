using AutoMapper;
using Microsoft.AspNetCore.Http;
using SmartChourey.DAL.Repositories.Interfaces;
using SmartChourey.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using SmartChourey.BLL.Services.Interfaces.User;
using System.Data.Entity;

namespace SmartChourey.BLL.Services.User
{
    public class SiteInformationService: ISiteInformationService
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IRepository<SiteInformation> _siteInformationRepository;

        public SiteInformationService(IConnectionFactory connectionFactory, 
            IRepository<SiteInformation> siteInformationRepository)
        {
            _connectionFactory = connectionFactory;
            _siteInformationRepository = siteInformationRepository;
        }

        public async Task<string> GetSiteName(string siteId)
        {
            using var connection = _connectionFactory.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SiteId", siteId);
            var siteName = await connection.QueryAsync<string>("sp_SiteInformation_EmailDetail", parameters, commandType: CommandType.StoredProcedure);
            return siteName.FirstOrDefault();
        }

        public async Task<SiteInformation> GetSiteById(string siteId)
        {
            var result = await _siteInformationRepository.Table.FirstOrDefaultAsync(x => x.Id == siteId);
            return result;
        }
    }
}
