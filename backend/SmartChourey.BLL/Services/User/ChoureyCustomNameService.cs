using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.Utilites;
using SmartChourey.BLL.ViewModel.Admin;
using SmartChourey.BLL.ViewModel.User;
using SmartChourey.DAL;
//using SmartChourey.DAL;
using SmartChourey.DAL.Context;
using SmartChourey.DAL.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace SmartChourey.BLL.Services.User
{
    public class ChoureyCustomNameService : IChoureyCustomNameService
    {
        private readonly IRepository<ChoureyCustomName> _choureyCustomNameRepository;
        public ChoureyCustomNameService(
            IRepository<ChoureyCustomName> choureyCustomNameRepository
         )
        {
            _choureyCustomNameRepository = choureyCustomNameRepository;
        }
        public async Task<bool> InsertAsync(ChoureyCustomName data)
        {
            try
            {
                var model = _choureyCustomNameRepository.Table.Where(x => x.ChoureyCustomNameId == data.ChoureyCustomNameId).FirstOrDefault();
                if (model == null)
                {
                    await _choureyCustomNameRepository.Add(data);
                }
                else
                {
                    model.Chourey1 = data.Chourey1;
                    model.Chourey2 = data.Chourey2;
                    model.Disaster = data.Disaster;
                    model.SaftetyDeclaration = data.SaftetyDeclaration;
                    model.Chourey1Japanese = data.Chourey1Japanese;
                    model.Chourey2Japanese = data.Chourey2Japanese;
                    model.DisasterJapanese = data.DisasterJapanese;
                    model.SaftetyDeclarationJapanese = data.SaftetyDeclarationJapanese;
                    await _choureyCustomNameRepository.Update(model);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<ChoureyCustomName> GetChoureyCustomNameBySiteId(string siteId)
        {
            var data = _choureyCustomNameRepository.Table.Where(x => x.SiteId == siteId).FirstOrDefault();
            if (data == null)
                data = new ChoureyCustomName();
            if (String.IsNullOrEmpty(data.Chourey1))
                data.Chourey1 = "Chourey 1";
            if (String.IsNullOrEmpty(data.Chourey2))
                data.Chourey2 = "Chourey 2";
            if (String.IsNullOrEmpty(data.Disaster))
                data.Disaster = "Disaster";
            if (String.IsNullOrEmpty(data.SaftetyDeclaration))
                data.SaftetyDeclaration = "Safety Declaration";
            if (String.IsNullOrEmpty(data.Chourey1Japanese))
                data.Chourey1Japanese = "朝礼 １";
            if (String.IsNullOrEmpty(data.Chourey2Japanese))
                data.Chourey2Japanese = "朝礼 2";
            if (String.IsNullOrEmpty(data.DisasterJapanese))
                data.DisasterJapanese = "災害事例";
            if (String.IsNullOrEmpty(data.SaftetyDeclarationJapanese))
                data.SaftetyDeclarationJapanese = "安全宣言";

            return data;
        }
    }
}