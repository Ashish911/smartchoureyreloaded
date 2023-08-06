using BusinessLogicLayer.Configuration;
using Dapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartChourey.BLL.Configuration;
using SmartChourey.BLL.Dtos;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.DAL;
using SmartChourey.DAL.Repositories.Interfaces;
using System.Data;
using System.Data.Entity.Core.Objects;

namespace SmartChourey.BLL.Services.User
{
    public class ChangeLogService<T> : IChangeLogService<T> where T : class
    {
        private readonly IRepository<ChangeLog> _changeLogRepository;
        private readonly IRepository<ChangeDetailLog> _changeDetailLogRepository;
        private readonly IConnectionFactory _connectionFactory;

        public ChangeLogService(IRepository<ChangeLog> changeLogRepository, IRepository<ChangeDetailLog> changeDetailLogRepository, IConnectionFactory connectionFactory)
        {
            _changeLogRepository = changeLogRepository;
            _changeDetailLogRepository= changeDetailLogRepository;
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> Insert(long id, EnumHelpers.EChangeType changeType, EnumHelpers.ECategory category,
            EnumHelpers.EChangeCategory changeCategory, string createdBy, string siteId)
        {
            bool isSuccess = false;
            try
            {
                var changeLog = new ChangeLog();
                string changeLogId = Guid.NewGuid().ToString();

                changeLog.ChangeLogId = changeLogId;
                changeLog.Ecategory = (int)category;
                changeLog.EchangeCategory = (int)changeCategory;
                changeLog.EchangeType = (int)changeType;
                changeLog.Id = id.ToString();
                changeLog.CreatedBy = createdBy;
                changeLog.CreatedOn = new Utilities().GetTokyoDate();
                changeLog.ChangeCount = 0;
                changeLog.SiteId = siteId;
                changeLog.ChangedProperties = "";
                await _changeLogRepository.Add(changeLog);
                isSuccess = true;
            }
            catch (Exception)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public async Task<IList<ChangeLogDto>> GetAsync(string siteId, string fromDate, string toDate)
        {
            using var connection = _connectionFactory.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("siteId", siteId);
            parameters.Add("fromDate", fromDate);
            parameters.Add("toDate", toDate);
            IEnumerable<ChangeLogDto> models = await connection.QueryAsync<ChangeLogDto>("sp_GetChangeLogRepor", parameters, commandType: CommandType.StoredProcedure);
            return models.ToList();
        }

        public async Task<bool> Update(EntityEntry<T> entry, long id, EnumHelpers.EChangeType changeType, EnumHelpers.ECategory category, EnumHelpers.EChangeCategory changeCategory, string createdBy, string siteId)
        {
            var isSuccess = false;
            try
            {
                int count = 0;
                string changedProperties = "";
                var changeLog = new ChangeLog();
                string changeLogId = Guid.NewGuid().ToString();
                var test = entry.State;
                foreach (var item in entry.CurrentValues.Properties)
                {
                    var prop = entry.Property(item);
                    if (prop.IsModified && (prop.Metadata.Name != "UpdatedBy" && prop.Metadata.Name != "UpdatedOn"))
                    {
                        var changeDetailLog = new ChangeDetailLog();
                        changeDetailLog.NewValue = prop.CurrentValue?.ToString();
                        changeDetailLog.OldValue = prop.OriginalValue == null ? "" : prop.OriginalValue.ToString();
                        changeDetailLog.Property = prop.Metadata.Name;
                        changeDetailLog.FkChangeDetailId = changeLogId;
                        await _changeDetailLogRepository.Add(changeDetailLog);
                        count++;
                        changedProperties = changedProperties + ", " + prop.Metadata.Name;
                    }
                }

                if (count != 0)
                {
                    changeLog.ChangeLogId = changeLogId;
                    changeLog.Ecategory = (int)category;
                    changeLog.EchangeCategory = (int)changeCategory;
                    changeLog.EchangeType = (int)changeType;
                    changeLog.Id = id.ToString();
                    changeLog.CreatedBy = createdBy;
                    changeLog.CreatedOn = new Utilities().GetTokyoDate();
                    changeLog.ChangeCount = count;
                    changeLog.SiteId = siteId;
                    changeLog.ChangedProperties = changedProperties.Substring(1, changedProperties.Length - 1);
                    await _changeLogRepository.Add(changeLog);
                }
                isSuccess = true;
            }
            catch (Exception)
            {
                isSuccess = false;
            }

            return isSuccess;
        }
    }

}
