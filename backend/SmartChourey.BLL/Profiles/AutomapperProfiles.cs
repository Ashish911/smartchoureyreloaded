using AutoMapper;
using SmartChourey.BLL.Dtos;
using SmartChourey.BLL.ViewModel.User;
using SmartChourey.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.Profiles
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<EmployeeDetailViewModel, EmployeeDetailInformation>().ReverseMap();
            CreateMap<SiteSpaceAggregate, SiteSpaceAggregateDto>().ReverseMap();
        }
    }
}
