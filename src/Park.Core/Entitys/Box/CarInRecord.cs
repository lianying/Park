using Abp.Domain.Entities.Auditing;
using Park.Enum;
using Park.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.Box
{
    public class CarInRecord : FullAuditedEntity<long>, ISynchronize
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        public virtual string CarNumber { get; set; }

        [ForeignKey("ParkId")]
        public virtual Parks.ParkSet Park { get; set; }

        public virtual int ParkId { get; set; }

        /// <summary>
        /// 入场时间
        /// </summary>
        public virtual DateTime InTime { get; set; }

        [ForeignKey("CarId")]
        /// <summary>
        /// 系统用户
        /// </summary>
        public virtual CarUsers.CarUsers CarUser { get; set; }

        public virtual long? CarId { get; set; }

        [ForeignKey("CarPortId")]
        public virtual CarUsers.CarPort CarPort { get; set; }

        public virtual long? CarPortId { get; set; }


        public virtual InOutTypeEnum InType { get; set; }



        /// <summary>
        /// 入场时集团场内车辆数
        /// </summary>
        public virtual int CarInCount { get; set; }

        /// <summary>
        /// 车位满时月租车以临时车入场
        /// </summary>
        public virtual bool IsMonthTempIn { get; set; }

        /// <summary>
        /// 是否为月租车过期入场
        /// </summary>
        public virtual bool IsMonthTimeOutInt { get; set; }


        /// <summary>
        /// 临时车转月租车的时间
        /// </summary>
        public virtual DateTime? TempConvertMonthTime { get; set; }



        public virtual bool IsSuccess { get; set; }



        public virtual string CloudId { get; set; }

        [ForeignKey("CarInPhotoId")]
        public virtual CarInOutImage CarInOutImage { get; set; }

        public virtual long? CarInPhotoId { get; set; }
    }
}
