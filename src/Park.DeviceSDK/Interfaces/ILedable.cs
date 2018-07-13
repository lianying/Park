using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Devices.Interfaces
{
    public interface ILedable:ILoginable
    {
        bool Show(string text);


        bool Speak(string text);
    }
}
