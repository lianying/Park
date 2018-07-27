using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using Park.EntityFramework;

namespace Park
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(ParkCoreModule))]
    public class ParkDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ParkDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";

            
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
