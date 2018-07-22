using Abp.Domain.Entities.Auditing;
using Park.Entitys.CarUsers;
using Park.Enum;
using Park.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.Box
{
    public class CarOutRecord: FullAuditedEntity<long>, ISynchronize
    {
        public virtual string CarNumber { get; set; }

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

        /// <summary>
        /// 入场时场内集团车数量
        /// </summary>
        public virtual int CarInCount { get; set; }

        /// <summary>
        /// 入场类型
        /// </summary>
        public virtual InOutTypeEnum InType { get; set; }

        /// <summary>
        /// 车位满时月租车以临时车入场
        /// </summary>
        public virtual bool IsMonthTempIn { get; set; }

        /// <summary>
        /// 是否为月租车过期入场
        /// </summary>
        public virtual bool IsMonthTimeOutInt { get; set; }

        [ForeignKey("ParkId")]
        public virtual Parks.ParkSet Park { get; set; }

        public virtual int ParkId { get; set; }


        /// <summary>
        /// 临时车转月租车的时间
        /// </summary>
        public virtual DateTime? TempConvertMonthTime { get; set; }


        [ForeignKey("CarInPhotoId")]
        public virtual CarInOutImage CarInImage { get; set; }

        public virtual long? CarInPhotoId { get; set; }



        public virtual bool IsInSuccess { get; set; }
        

        public virtual string InCloudId { get; set; }

        public virtual bool IsSuccess { get; set ; }

        public virtual string CloudId { get; set; }



        /// <summary>
        /// 出场时间
        /// </summary>
        public virtual DateTime OutTime { get; set; }


        /// <summary>
        /// 实际付款金额
        /// </summary>
        public virtual decimal Pay { get; set; }

        /// <summary>
        /// 本地优惠金额
        /// </summary>
        public virtual decimal OfferMoney { get; set; }


        /// <summary>
        /// 本地优惠时长
        /// </summary>
        public virtual TimeSpan? OfferTime { get; set; }


        /// <summary>
        /// 优惠券
        /// </summary>
        public virtual CarDiscount CarDiscount { get; set; }

        /// <summary>
        /// 进出类型  拍照进出场、手工进出场
        /// </summary>
        public virtual InOutTypeEnum OutType { get; set; }

        [ForeignKey("CarOutPhotoId")]
        /// <summary>
        /// 出场图片
        /// </summary>
       // public virtual Stream Imge { get; set; }
       public virtual CarInOutImage CarOutImage { get; set; }


        public virtual long? CarOutPhotoId { get; set; }
        /// <summary>
        /// 出场拍照时间
        /// </summary>

        public virtual DateTime? OutPhotoTime { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public virtual PayTypeEnum PayType { get; set; }


        public virtual OutOfferTypeEnum OutOfferType { get; set; }


        public virtual string Remark { get; set; }


    }
}
