using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Park.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.CarUsers
{
    public class CarUserGroup : Entity, IHasCreationTime, ISynchronize
    {

        public virtual string GroupName { get; set; }


        public virtual ParkAreas.ParkAreas ParkArea { get; set; }

        public virtual DateTime CreationTime { get; set; }
        public virtual bool IsSuccess { get; set; }
        public string CloudId { get; set; }
    }
}
