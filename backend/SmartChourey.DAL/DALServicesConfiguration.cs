using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartChourey.DAL.Context;
using SmartChourey.DAL.Repositories;
using SmartChourey.DAL.Repositories.Interfaces;

namespace SmartChourey.DAL
{
    public static class DALServicesConfiguration
    {
        public static void AddServicesFromDAL(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");


            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IConnectionFactory, ConnectionFactory>();
        }
    }
}