using Abp.Events.Bus;
using Park.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.Box
{
    public class CarOuRecordEventData : EventData
    {
        public CarOutRecord CarOutRecord { get; set; }
    }

    public class CarOutRecordCreateedEventData : CarOuRecordEventData
    {
        public User CreatorUser { get; set; }
    }
}
