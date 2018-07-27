using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Park.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using Park.Interfaces;
using Abp.Auditing;

namespace Park.Entitys.CarUsers
{
    public class CarUsers: Entity<long>, IHasCreationTime, IModificationAudited, IHasModificationTime, IDeletionAudited, IHasDeletionTime, ISoftDelete, ISynchronize
    {
        public virtual string Name { get; set; }

        public virtual Sex Sex { get; set; }

        public virtual string Phone { get; set; }
        [DisableAuditing]
        [ForeignKey("AreaId")]
        public virtual Park.Entitys.ParkAreas.ParkAreas ParkArea { get; set; }

        public virtual long AreaId { get; set; }

        public virtual int ParkId { get; set; }

        [ForeignKey("ParkId")]
        public virtual  Park.Entitys.Parks.ParkSet Park { get; set; }

        [DisableAuditing]
        public virtual ICollection<CarPort> CarPorts { get; set; }
        [DisableAuditing]
        public virtual ICollection<CarNumbers> CarNumbers { get; set; }


        public virtual FullInType FullInType { get; set; }



        public virtual DateTime CreationTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual bool IsSuccess { get; set; }
        public virtual string CloudId { get; set; }
    }
}
