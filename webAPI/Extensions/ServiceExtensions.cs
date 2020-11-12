using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Contracts;
using Entities;


namespace webApi.Extensions
{
    public static class ServiceExtensions
    {
        //Prepairing Connection Into Mysql
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            //IConfiguration => this method it's for accessing appsettings.json
            var connectionString = config["mysqlconnection:connectionString"];
            services.AddDbContext<RepositoryContext>(o => o.UseMySql(connectionString));
        }

        //Injecting Dependency Repository
        public static void ConfigureRepositoryWrapper(this IServiceCollection services) 
        { 
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>(); 
        }
    }
}

