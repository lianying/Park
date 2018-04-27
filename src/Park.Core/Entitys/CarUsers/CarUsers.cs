using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Park.Enum;

namespace Park.Entitys.CarUsers
{
    public class CarUsers: FullAuditedEntity<long>
    {
        public virtual string Name { get; set; }

        public virtual Sex Sex { get; set; }

        public virtual string Phone { get; set; }

        public virtual Park.Entitys.ParkAreas.ParkAreas ParkArea { get; set; }

        public virtual int ParkId { get; set; }

        public virtual Parks.JinQuPark Park { get; set; }



    }
}
