using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Park.Enum;
using Park.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.ParkEntrances
{
    public class ParkEntrancePermission : Entity<long>,IMayHaveTenant
    {
        public virtual int? TenantId { get; set; }
        
        /// <summary>
        /// 允许 通行车辆
        /// </summary>
        public virtual ICollection<CarTypes.CarTypes>  CarTypes { get; set; }

        /// <summary>
        /// 临时通行方式(设置是否允许临时车通行)
        /// </summary>
        public virtual bool IsTempCarIn { get; set; }

        /// <summary>
        /// 临时车进入 弹窗确认
        /// </summary>
        public virtual bool IsTempCarConfirm { get; set; }

        /// <summary>
        /// 临时车收费为0  确认放行
        /// </summary>
        public virtual bool IsTempCarZeoPayOut { get; set; }

        /// <summary>
        /// 非机动车 是否允许进入
        /// </summary>
        public virtual bool IsNonVehicleIn { get; set; }
        /// <summary>
        /// 无牌车通行权限
        /// </summary>
        public virtual NoNumberOptions NoNumberOptions { get; set; }

    }
}
