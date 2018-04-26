using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Parks.Devices.Interfaces
{
    public interface ILedable : IControlable
    {
        bool ReSet();


    }
}
