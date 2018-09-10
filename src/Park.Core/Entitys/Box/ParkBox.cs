using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.Box
{
    public class ParkBox:Entity<int>
    {
        public virtual string Name { get; set; }


        public virtual string Ip { get; set; }

        //[ForeignKey("CameraId")]
        public virtual ICollection<Devices.Camera> Cameras { get; set; }

        //[ForeignKey("LedId")]
        public virtual ICollection<Devices.Led> Leds { get; set; }


    }
}
