using Abp.Domain.Entities;
using Park.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.CarTypes
{
    public class CarTypes:Entity<long>,IMayHaveTenant
    {

        
        public virtual string Name { get; set; }

        public virtual string CustomName { get; set; }

        /// <summary>
        /// 预警值
        /// </summary>
        public virtual decimal Warring { get; set; }


        public virtual CarType Category { get; set; }

        public virtual int? TenantId { get; set; }

    }


}
