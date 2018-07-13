using Park.Entitys.CarUsers;
using Park.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Parks.ParkBox
{
    public class CarInModel
    {

        /// <param name="carNumber">车牌号</param>
        /// <param name="inTime">入场时间</param>
        /// <param name="carUsers">系统用户</param>
        /// <param name="isMonthTempIn">月租车是否已临时车入场</param>
        /// <param name="img">入场图片</param>
        /// <param name="inPhotoTime"></param>
        /// <returns></returns>
        public string CarNumber { get; set; }
        public DateTime InTime { get; set; }
        public Stream Img { get; set; }
        public DateTime? InPhotoTime { get; set; }


        public InOutTypeEnum InOutType { get; set; }

        
    }
}
