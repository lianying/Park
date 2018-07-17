using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.Box
{
    public class CarInOutImage:Entity<long>, IHasCreationTime
    {
        public virtual string Path { get; set; }
        public DateTime CreationTime { get; set; }


    }
}
