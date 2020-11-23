using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapControllerWebApi.Installers
{
    public class ApiVersionInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddVersionedApiExplorer(
                        options =>
                        {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
                        });

            //Add api versioning
            services.AddApiVersioning(v =>
            {
                v.ReportApiVersions = true;
                //v.AssumeDefaultVersionWhenUnspecified = true;
                //v.DefaultApiVersion = new ApiVersion(1, 0);

            });


        }
    }
}
