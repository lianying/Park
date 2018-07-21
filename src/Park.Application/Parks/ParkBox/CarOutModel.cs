﻿using Park.Entitys.CarUsers;
using Park.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Parks.ParkBox
{
    public class CarOutModel
    {
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
        public InOutTypeEnum InOutType { get; set; }

        /// <summary>
        /// 出场图片
        /// </summary>
        public Stream Imge { get; set; }
        /// <summary>
        /// 出场拍照时间
        /// </summary>

        public DateTime? OutPhotoTime { get; set; }



    }
}