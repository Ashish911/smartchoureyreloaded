using Microsoft.Extensions.DependencyInjection;
using SmartChourey.BLL.Configuration;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.Services.User;
using System.Reflection;

namespace SmartChourey.DAL
{
    public static class BLLServicesConfiguration
    {
        public static void AddServicesFromBLL(this IServiceCollection services)
        {
            services.AddScoped<IHelpers, Helpers>();
            services.AddScoped<IEmailHelpers, EmailHelpers>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ISiteService, SiteService>();
            services.AddScoped<ISiteCodeService, SiteCodeService>();
            services.AddScoped<IChoureyCustomNameService, ChoureyCustomNameService>();
            services.AddScoped<ISiteSpaceAggregateService, SiteSpaceAggregateService>();
            services.AddScoped<ISiteDeclarationService, SiteDeclarationService>();
            services.AddScoped<ISetupService, SetupService>();
            services.AddScoped<ISubAdminService, SubAdminService>();
            services.AddScoped<IUserboardService, UserboardService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IDeviceSiteService, DeviceSiteService>();

            services.AddScoped<ISiteInformationService, SiteInformationService>();
            services.AddScoped<IChangeLogService<ChoureyOneInformation>, ChangeLogService<ChoureyOneInformation>>();
            services.AddScoped<IChangeLogService<ChoureyOneVideoInformation>, ChangeLogService<ChoureyOneVideoInformation>>();
            services.AddScoped<IChangeLogService<ChoureyOnePhotoInformation>, ChangeLogService<ChoureyOnePhotoInformation>>();

            services.AddScoped<IChangeLogService<ChoureyTwoInformation>, ChangeLogService<ChoureyTwoInformation>>();
            services.AddScoped<IChangeLogService<ChoureyTwoVideoInformation>, ChangeLogService<ChoureyTwoVideoInformation>>();
            services.AddScoped<IChangeLogService<ChoureyTwoPhotoInformation>, ChangeLogService<ChoureyTwoPhotoInformation>>();
            
            services.AddScoped<IChangeLogService<DisasterInformation>, ChangeLogService<DisasterInformation>>();
            services.AddScoped<IChangeLogService<DisasterPhotoInformation>, ChangeLogService<DisasterPhotoInformation>>();

            services.AddScoped<IChangeLogService<FileUpload>, ChangeLogService<FileUpload>>();
            services.AddScoped<IFileUploadService, FileUploadService>();

            services.AddScoped<ISiteSpaceDetailService, SiteSpaceDetailService>();

            services.AddScoped<IMediaCommentService, MediaCommentService>();

            services.AddScoped<IPublicUserBoardService, PublicUserBoardService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
