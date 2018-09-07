using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Park.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.FareRules
{
    public class MonthFee: Entity, IHasCreationTime, ISynchronize
    {
        public virtual string Name { get; set; }


        [ForeignKey("CarTypeId")]

        public virtual CarTypes.CarTypes CarTypes { get; set; }



        public virtual long CarTypeId { get; set; }


        public virtual decimal Month { get; set; }


        public virtual decimal Quarter { get; set; }

        public virtual decimal HalfYear { get; set; }


        public virtual decimal Year { get; set; }
        public virtual bool IsSuccess { get; set; }
        public virtual string CloudId { get; set; }
        public virtual DateTime CreationTime { get; set; }
    }
}
