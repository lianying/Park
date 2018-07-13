using Abp.Domain.Entities.Auditing;
using Park.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.Box
{
    public class CarInRecord: FullAuditedEntity<long>,ISynchronize
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }

        /// <summary>
        /// 入场时间
        /// </summary>
        public DateTime InTime { get; set; }

        /// <summary>
        /// 系统用户
        /// </summary>
        public CarUsers.CarUsers CarUser { get; set; }


        public ICollection< CarUsers.CarPort> CarPort { get; set; } 

        /// <summary>
        /// 入场时集团场内车辆数
        /// </summary>
        public int CarInCount { get; set; }

        /// <summary>
        /// 车位满时月租车以临时车入场
        /// </summary>
        public bool IsMonthTempIn { get; set; }

        /// <summary>
        /// 是否为月租车过期入场
        /// </summary>
        public bool IsMonthTimeOutInt { get; set; }


        /// <summary>
        /// 临时车转月租车的时间
        /// </summary>
        public DateTime TempConvertMonthTime { get; set; }



        public bool IsSuccess { get ; set ; }



        public string CloudId { get ; set ; }
    }
}
