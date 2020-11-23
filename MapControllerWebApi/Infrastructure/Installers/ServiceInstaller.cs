
using MapControllerWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MapControllerWebApi.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            #region Add Business Services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ILakeFinder, LakeFinder>();
            #endregion

            #region request filters
            
            #endregion
        }
    }
}
