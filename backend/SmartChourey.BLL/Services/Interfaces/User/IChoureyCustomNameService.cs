using SmartChourey.BLL.ViewModel.Admin;
using SmartChourey.BLL.ViewModel.User;
using SmartChourey.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.Services.Interfaces.User
{
    public interface IChoureyCustomNameService
    {
        Task<bool> InsertAsync(ChoureyCustomName data);
        Task<ChoureyCustomName> GetChoureyCustomNameBySiteId(string siteId);
    }
}
