using Abp.Modules;
using Castle.MicroKernel.Registration;
using Microsoft.Owin.Security;
using Park.ParkBox;
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
            
            
        }
       


    }
}
