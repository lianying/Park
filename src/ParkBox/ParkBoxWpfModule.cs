using Abp.Modules;
using Castle.MicroKernel.Registration;
using Microsoft.Owin.Security;
using Park.Froms;
using Park.ParkBox;
using Park.Parks.ParkBox.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Park
{
    [DependsOn(typeof(ParkApplicationModule),typeof(ParkDataModule))]
    public class ParkBoxWpfModule:AbpModule
    {
        public override void PreInitialize()
        {
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            IocManager.Register<IBoxMessage>(Abp.Dependency.DependencyLifeStyle.Transient);

            //var temp = Component.For<ISetInfo>().ImplementedBy(typeof(MainWindow)).IsDefault().Named(Guid.NewGuid().ToString());
            //IocManager.IocContainer.Register(temp.LifestyleSingleton());
        }
       


    }
}
