using Abp.Application.Services;
using Park.Entitys.CarUsers;
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

        /// <summary>
        /// 获取在有效期内的用户
        /// </summary>
        /// <param name="parkId"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        CarUsers GetUser(int parkId, string number);
    }
}
