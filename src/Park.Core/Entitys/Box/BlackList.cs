using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.Box
{
    public  class BlackList:Entity
    {
        public virtual string CarNumber { get; set; }


        public virtual DateTime OperationDate { get; set; }


        public virtual string Remark { get; set; }

    }
}
