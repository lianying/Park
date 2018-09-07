using Abp.Authorization.Users;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Park.Authorization.Users;
using Park.Enum;
using Park.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.CarUsers
{
    public class RechargeRecord : Entity<long>, IHasCreationTime, ISynchronize
    {
        /// <summary>
        /// 应收
        /// </summary>
        public virtual decimal Receivable { get; set; }
        /// <summary>
        /// 实收
        /// </summary>
        public virtual decimal ActualHarvest { get; set; }

        public virtual long UserId { get; set; }


        public virtual long CarPortId { get; set; }

        [ForeignKey("UserId")]
        public virtual CarUsers CarUser { get; set; }

        [ForeignKey("CarPortId")]
        public virtual CarPort CarPort { get; set; }

        /// <summary>
        /// 用户延期时  名下车牌号
        /// </summary>
        public virtual string CarNumbers { get; set; }

        /// <summary>
        /// 延期月数
        /// </summary>
        public  virtual int ExtensionCount { get; set; }


        /// <summary>
        /// 原结束日期
        /// </summary>
        public virtual DateTime OldDate { get; set; }


        /// <summary>
        /// 新结束日期
        /// </summary>
        public virtual DateTime NewDate { get; set; }



        public virtual string Remark { get; set; }

        public virtual long SysUserId { get; set; }
        
        [ForeignKey("SysUserId")]
        public virtual User User { get; set; }



        public virtual DateTime CreationTime { get; set; }
        public virtual bool IsSuccess { get; set; }
        public virtual string CloudId { get; set; }

        public virtual PayTypeEnum PayType { get; set; }
    }
}
