using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Park.Entitys.ParkAreas;
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
    public class ParkEntrances : Entity<long>, ISynchronize, IAudited, IMayHaveTenant
    {
        public virtual string EntranceName { get; set; }

        public virtual EntranceType EntranceType { get; set; }
        [NotMapped]
        [ForeignKey("ParkLevelId")]
        public virtual ParkLevels.ParkLevels ParkLevel { get; set; }

        public virtual long ParkLevelId { get; set; }

        public virtual long PermissionId { get; set; }
        [ForeignKey("PermissionId")]
        public virtual ParkEntrancePermission ParkEntrancePermission { get; set; }
        public virtual bool IsSuccess { get; set; }
        public virtual string CloudId { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual int? TenantId { get; set; }
        [NotMapped]
        [ForeignKey("AreaId")]
        public virtual Entitys.ParkAreas.ParkAreas ParkAreas { get; set; }

        public virtual long AreaId { get; set; }
    }
}
