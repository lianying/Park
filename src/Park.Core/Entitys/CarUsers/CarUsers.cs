using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Park.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Park.Entitys.CarUsers
{
    public class CarUsers: FullAuditedEntity<long>
    {
        public virtual string Name { get; set; }

        public virtual Sex Sex { get; set; }

        public virtual string Phone { get; set; }

        [ForeignKey("AreaId")]
        public virtual Park.Entitys.ParkAreas.ParkAreas ParkArea { get; set; }

        public virtual long AreaId { get; set; }

        public virtual int ParkId { get; set; }

        [ForeignKey("ParkId")]
        public virtual  Park.Entitys.Parks.ParkSet Park { get; set; }


        public virtual ICollection<CarPort> CarPorts { get; set; }

        public virtual ICollection<CarNumbers> CarNumbers { get; set; }



    }
}
