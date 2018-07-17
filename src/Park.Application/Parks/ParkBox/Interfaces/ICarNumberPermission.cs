using Abp.Application.Services;
using Park.Parks.ParkBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Parks.ParkBox.Interfaces
{
    public interface ICarNumberPermission:IApplicationService
    {

        PermissionResult CheckCarNumberPermission(string number,long entranceId);
    }
}
