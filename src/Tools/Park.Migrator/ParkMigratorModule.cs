using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Park.EntityFramework;

namespace Park.Migrator
{
    [DependsOn(typeof(ParkDataModule))]
    public class ParkMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<ParkDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}