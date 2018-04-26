using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Parks.Devices.Interfaces
{
    public interface IDeviceable
    {
        string Ip { get; set; }

        string UserName { get; set; }

        string Password { get; set; }

        long Port { get; set; }
        

    }
}
