using Abp.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Park
{

    [DependsOn(typeof(ParkApplicationModule), typeof(ParkDataModule))]
    public class ParkManagerWpfModule:AbpModule
    {
        public override void PreInitialize()
        {
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            

            //var temp = Component.For<ISetInfo>().ImplementedBy(typeof(MainWindow)).IsDefault().Named(Guid.NewGuid().ToString());
            //IocManager.IocContainer.Register(temp.LifestyleSingleton());
        }
    }
}
