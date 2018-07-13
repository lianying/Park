using Park.Parks.ParkBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Parks.ParkBox.Interfaces
{
    public interface ICarNumberPermission
    {

        PermissionResult CheckCarNumberPermission(string number);
    }
}
