using Abp.Domain.Entities.Auditing;
using Park.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.CarUsers
{
    public class CarNumbers : FullAuditedEntity<long>, ISynchronize
    {
        public virtual string CarNumber { get; set; }
        
        [ForeignKey("CarUserId")]
        public virtual CarUsers CarUser { get; set; }

        public virtual long CarUserId { get; set; }

        public virtual bool IsSuccess { get; set; }

        public virtual string CloudId { get; set; }
    }
}
