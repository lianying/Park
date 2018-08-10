using Park.Devices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;

namespace Park.Parks.ParkBox.Interfaces
{
    public interface IBoxMessage:ITransientDependency
    {

        void DoMessage(DeviceInfoDto deviceInfoDto);
    }
}
