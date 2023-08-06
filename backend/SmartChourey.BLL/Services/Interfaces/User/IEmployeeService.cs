using SmartChourey.BLL.ViewModel.Admin;
using SmartChourey.BLL.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.Services.Interfaces.User
{
    public interface IEmployeeService
    {
        Task<EmployeeDetailViewModel> InsertAsync(EmployeeDetailViewModel model);
        Task<UserDetailViewModel> AssignSubAdmin(string SiteId, string Email, string UserName, string SubAdminUserId, string AdminUserId);        //EmployeeDetailViewModel Update(EmployeeDetailViewModel model);
        Task<EmployeeDetailViewModel> UpdateAsync(EmployeeDetailViewModel model);
        Task<EmployeeDetailViewModel> GetProfile(string id);
        Task<UserDetailViewModel> CheckUserExist(string Email);
        //EmployeeDetailViewModel GetProfile(string UserId);
    }
}
