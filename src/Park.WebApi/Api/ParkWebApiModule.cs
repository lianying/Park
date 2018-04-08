using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Swashbuckle.Application;
using System.Linq;

namespace Park.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(ParkApplicationModule))]
    public class ParkWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(ParkApplicationModule).Assembly, "app")
                .Build();

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
            ConfigureSwaggerUi();
        }

        private void ConfigureSwaggerUi()
        {
            Configuration.Modules.AbpWebApi().HttpConfiguration.EnableSwagger(
                c =>
                {
                    c.SingleApiVersion("v1", "ParkApi");
                    c.ResolveConflictingActions(apiDesciptions => apiDesciptions.First());
                })
                .EnableSwaggerUi();
        }
    }
}
