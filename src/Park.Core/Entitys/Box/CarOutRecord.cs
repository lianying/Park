using Abp.Domain.Entities.Auditing;
using Park.Entitys.CarUsers;
using Park.Enum;
using Park.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.Box
{
    public class CarOutRecord: FullAuditedEntity<long>, ISynchronize
    {
        public string CarNumber { get; set; }

        /// <summary>
        /// 入场时间
        /// </summary>
        public DateTime InTime { get; set; }

        /// <summary>
        /// 系统用户
        /// </summary>
        public CarUsers.CarUsers CarUser { get; set; }


        public CarUsers.CarPort CarPort { get; set; }

        /// <summary>
        /// 入场时场内集团车数量
        /// </summary>
        public int CarInCount { get; set; }

        /// <summary>
        /// 入场类型
        /// </summary>
        public InOutTypeEnum InType { get; set; }

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



        public bool IsInSuccess { get; set; }
        

        public string InCloudId { get; set; }

        public bool IsSuccess { get; set ; }

        public string CloudId { get; set; }



        /// <summary>
        /// 出场时间
        /// </summary>
        public DateTime OutTime { get; set; }


        /// <summary>
        /// 实际付款金额
        /// </summary>
        public decimal Pay { get; set; }

        /// <summary>
        /// 本地优惠金额
        /// </summary>
        public decimal OfferMoney { get; set; }


        /// <summary>
        /// 本地优惠时长
        /// </summary>
        public TimeSpan? OfferTime { get; set; }


        /// <summary>
        /// 优惠券
        /// </summary>
        public CarDiscount CarDiscount { get; set; }

        /// <summary>
        /// 进出类型  拍照进出场、手工进出场
        /// </summary>
        public InOutTypeEnum OutType { get; set; }

        /// <summary>
        /// 出场图片
        /// </summary>
        public Stream Imge { get; set; }
        /// <summary>
        /// 出场拍照时间
        /// </summary>

        public DateTime? OutPhotoTime { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public PayTypeEnum payType { get; set; }


    }
}
