using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.Devices
{
    [Table("Cameras")]
    public class Camera : CameraInfoBase
    {

        public override void InitDevice(SDKControlParametes controlParametes)
        {

        }
    }
}
