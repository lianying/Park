using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.ParkEntrances
{
    public class CarTypePermissions : CreationAuditedEntity<long>, IMayHaveTenant
    {
        public virtual int? TenantId { get; set; }

        public virtual long PermissionId { get; set; }

        public virtual long CarTypeId { get; set; }
    }
}
