using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.Devices
{
    [Table("Leds")]
    public class Led : CameraInfoBase
    {
        public override void InitDevice(SDKControlParametes controlParametes)
        {

        }
    }
}
