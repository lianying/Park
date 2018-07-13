using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Devices.Interfaces
{

    public interface IControlable:ILoginable
    {
        ICamerable Camerable { get; set; }

        ILedable Led { get; set; }
    }
}
