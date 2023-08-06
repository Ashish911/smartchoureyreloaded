using AutoMapper;
using BusinessLogicLayer.Configuration;
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
using static BusinessLogicLayer.Configuration.EnumHelpers;

namespace SmartChourey.BLL.Services.User
{
    public class SiteDeclarationService : ISiteDeclarationService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<SiteDeclarationInformation> _siteDeclarationInformationRepository;
        private readonly IConnectionFactory _connectionFactory;
        public SiteDeclarationService(
            IMapper mapper,
            IRepository<SiteDeclarationInformation> siteDeclarationInformationRepository,
            IConnectionFactory connectionFactory)
        {
            _mapper = mapper;
            _connectionFactory = connectionFactory;
            _siteDeclarationInformationRepository = siteDeclarationInformationRepository;
        }
        public async Task<List<SiteDeclarationViewModel>> GetSiteDeclarationBySiteIdAsync(string siteId, int viewMode)
        {
            using var connection = _connectionFactory.GetConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@siteId", siteId);
            parameters.Add("@viewMode", viewMode);
            return (await connection.QueryAsync<SiteDeclarationViewModel>("sp_SiteListDeclarationList", parameters, commandType: CommandType.StoredProcedure)).ToList();
        }
        public async Task<SiteDeclarationViewModel> InsertUpdateDeclaration(SiteDeclarationViewModel model)
        {
            try
            {
                var datas = _siteDeclarationInformationRepository.Table.Where(x => x.SiteId == model.SiteId).ToList();
                if (model.SiteDeclarationId != 0)
                {
                    var getData = datas.Where(x => x.SiteDeclarationId == model.SiteDeclarationId).FirstOrDefault();
                    if (getData != null)
                    {
                        getData.Description = model.Description;
                        getData.IsActive = model.IsActive;
                        getData.SiteId = model.SiteId;
                        getData.Title = model.Title;
                        getData.UpdatedBy = model.CurrentUserId;
                        getData.UpdatedOn = new DateTimeHelper().GetTokyoDate();

                        await _siteDeclarationInformationRepository.Update(getData);
                        model.IsSuccessful = true;
                        model.Message = "Successfully Updated";
                    }
                }
                else
                {
                    var table = new SiteDeclarationInformation();
                    table.CreatedBy = model.CurrentUserId;
                    table.CreatedOn = new DateTimeHelper().GetTokyoDate();
                    table.Description = model.Description;
                    table.IsActive = model.IsActive;
                    table.SiteId = model.SiteId;
                    table.Title = model.Title;
                    table.ViewMode = (int)model.EViewMode;

                    await _siteDeclarationInformationRepository.Add(table);

                    model.SiteDeclarationId = table.SiteDeclarationId;
                    model.IsSuccessful = true;
                    model.Message = "Successfully Inserted";
                }
                if (model.IsActive)
                {
                    foreach (var item in datas.Where(x => x.SiteDeclarationId != model.SiteDeclarationId))
                    {
                        var getData = datas.Where(x => x.SiteDeclarationId == item.SiteDeclarationId).FirstOrDefault();
                        getData.IsActive = false;
                        await _siteDeclarationInformationRepository.Update(getData);
                    }
                }
                return model;
            }
            catch (Exception e)
            {
                model.IsSuccessful = false;
                model.Message = "Sorry, Record has not been Inserted";
                var entity = new ErrorMessageInformation
                {
                    CreatedOn = new DateTimeHelper().GetTokyoDate(),
                    ErrorMessage = e.Message,
                    FileName = "BAL:Implementation:User:SiteDeclarationImplementation",
                    MethodName = "Insert/Update",
                    OperationDoneBy = model.UserName,
                    Status = true
                };
                return model;
            }
        }
        public async Task<SiteDeclarationViewModel> DeleteSiteDeclaration(long siteDeclarationId, string siteId)
        {
            SiteDeclarationViewModel model = new SiteDeclarationViewModel();
            try
            {
                var checkData = _siteDeclarationInformationRepository.Table.Where(x => x.SiteDeclarationId == siteDeclarationId && x.SiteId == siteId).FirstOrDefault();
                if (checkData != null)
                {
                    var deleteData = _siteDeclarationInformationRepository.Remove(checkData);
                    model.IsSuccessful = true;
                    model.Message = "Successfully Site declaration Deleted";
                    //IChangeLogService<SiteDeclaration_Information> changeLogService = new ChangeLogService<SiteDeclaration_Information>();
                    //changeLogService.Insert(model.SiteDeclarationId
                    //    , EnumHelpers.EChangeType.Deleted, EnumHelpers.ECategory.SafetyDeclaration, EnumHelpers.EChangeCategory.Info
                    //    , model.UserName, model.SiteId);

                }
            }
            catch (Exception e)
            {
                model.IsSuccessful = false;
                model.Message = "Failed to delete";
                //var entity = new ErrorMessage_Information();
                //entity.CreatedOn = new DateTimeHelper().GetTokyoDate();
                //entity.ErrorMessage = e.Message;
                //entity.FileName = "BAL:Implementation:User:SetUpService";
                //entity.MethodName = "DeleteDeclaration";
                //entity.OperationDoneBy = model.UserName;
                //entity.Status = true;
                //context.ErrorMessage_Information.Add(entity);
                //context.SaveChanges();
            }
            return model;
        }
        public async Task<SiteDeclarationViewModel> GetSiteDeclarationDetailsById(long siteDeclarationId, string siteId)
        {
            SiteDeclarationViewModel model = new SiteDeclarationViewModel();
            try
            {
                using var connection = _connectionFactory.GetConnection();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@siteId", siteId);
                parameters.Add("@viewMode", 1);
                IEnumerable<SiteDeclarationViewModel> siteDeclarations = await connection.QueryAsync<SiteDeclarationViewModel>("sp_SiteListDeclarationList", parameters, commandType: CommandType.StoredProcedure);
                model = siteDeclarations.Where(x => x.SiteDeclarationId == siteDeclarationId).FirstOrDefault();
                if (model != null)
                {
                    model.DescriptionList = model.Description == null ? new List<SiteDeclarationDescription>() : splitListOfSiteDeclarationDescription(model.Description);
                    model.DescriptionModel.Description = model.DescriptionModel == null ? null : splitListOfSiteDeclarationDescription(model.Description).First().Description;
                    model.DescriptionList.RemoveAt(0);
                }
                else
                {
                    model = new SiteDeclarationViewModel();
                    model.IsSuccessful = false;
                    model.Message = "Sorry, No such records found";
                }
                return model;
            }
            catch (Exception e)
            {
                model.IsSuccessful = false;
                model.Message = "Sorry, Record has not been Inserted";
                //var entity = new ErrorMessageInformation
                //{
                //    CreatedOn = new DateTimeHelper().GetTokyoDate(),
                //    ErrorMessage = e.Message,
                //    FileName = "BAL:Implementation:User:SiteDeclarationImplementation",
                //    MethodName = "Insert/Update",
                //    OperationDoneBy = model.UserName,
                //    Status = true
                //};
                return model;
            }
        }

        public static List<SiteDeclarationDescription> splitListOfSiteDeclarationDescription(string descList)
        {
            List<SiteDeclarationDescription> list = new List<SiteDeclarationDescription>();
            var description = descList.Split(':').ToList();
            foreach (var item in description)
            {
                var modelNumbers = new SiteDeclarationDescription();
                modelNumbers.Description = item;
                list.Add(modelNumbers);
            }
            return list;
        }
    }
}