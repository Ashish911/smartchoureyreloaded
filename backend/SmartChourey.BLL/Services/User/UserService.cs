using SmartChourey.BLL.ViewModel.Admin;
using SmartChourey.DAL.Repositories.Interfaces;
using SmartChourey.DAL;
using Microsoft.EntityFrameworkCore;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.ViewModel.SuperAdmin;

namespace SmartChourey.BLL.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRepository<EmployeeDetailInformation> _employeeDetailRepository;
        private readonly IRepository<SiteAccessInformation> _siteAccessInformation;
        private readonly IRepository<UserInformation> _userInformationRepository;

        public UserService(
            IRepository<EmployeeDetailInformation> employeeDetailRepository,
            IRepository<SiteAccessInformation> siteAccessInformation,
            IRepository<UserInformation> userInformationRepository)
        {
            _employeeDetailRepository = employeeDetailRepository;
            _siteAccessInformation = siteAccessInformation;
            _userInformationRepository = userInformationRepository;
        }

        public async Task<UserDetailViewModel> CheckUserExistAsync(string email)
        {
            var model = new UserDetailViewModel();
            var result = await _employeeDetailRepository.Table.FirstOrDefaultAsync(x => x.Email == email);
            if (result != null)
            {
                model.UserId = result.UserId;
                model.Email = result.Email;
            }
            return model;
        }
        public async Task<bool> IsSubAdminBySiteId(string siteId, string userId)
        {
            bool isSubAdmin = true;
            isSubAdmin = await _siteAccessInformation.Table.Where(x => x.SiteAccessGivenTo == userId && x.SiteId == siteId).FirstOrDefaultAsync() != null;

            return isSubAdmin;
        }
        public async Task<List<UserInformationViewModel>> GetUserList()
        {
            var data = await _userInformationRepository.GetAll();
            var userList = data.Select(x => new UserInformationViewModel
            {
                UserName = x.UserName,
                PhoneNumber = x.PhoneNumber,
                CompanyName = x.CompanyName,
                SiteName = x.SiteName
            }).ToList();

            return userList;
        }

        public async Task<UserInformationViewModel> CreateUser(UserInformationViewModel userInformationViewModel)
        {
            try
            {
                var userInformation = new UserInformation()
                {
                    CompanyName = userInformationViewModel.CompanyName,
                    UserName = userInformationViewModel.UserName,
                    PhoneNumber = userInformationViewModel.PhoneNumber,
                    SiteName = userInformationViewModel.SiteName,
                    SiteId = userInformationViewModel.SiteId
                };
                await _userInformationRepository.Add(userInformation);
            }
            catch (Exception e)
            {

            }
            return userInformationViewModel;
        }
    }
}
