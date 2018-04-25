using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Park.Entitys.ParkEntrances;
using Park.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Parks.Entrance.Dto
{
    [AutoMap(typeof(ParkEntrancePermission))]
    public class EntrancePermissionDto:EntityDto<long>
    {
        public string CarTypes { get; set; }

        /// <summary>
        /// 临时车是否允许进入
        /// </summary>
        public bool IsTempCarIn { get; set; }


        public bool IsTempCarConfirm { get; set; }

        /// <summary>
        /// 临时车收费为0  确认放行
        /// </summary>
        public bool IsTempCarZeoPayOut { get; set; }

        /// <summary>
        /// 非机动车 是否允许进入
        /// </summary>
        public bool IsNonVehicleIn { get; set; }
        /// <summary>
        /// 无牌车通行权限
        /// </summary>
        public NoNumberOptions NoNumberOptions { get; set; }
    }
}
