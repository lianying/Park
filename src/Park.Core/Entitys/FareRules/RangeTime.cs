using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.FareRules
{
    public class RangeTime:Entity
    {
        private TimeSpan m_startTime;

        private TimeSpan m_entTime;

        private double m_FeeMinutes;

        private decimal m_FeeMoney;

        private decimal m_TopMoney;

        private double m_MinSpanTime;

        private int m_FareRuleId;

        public RangeTime()
        {
            
        }

       

        public TimeSpan StartTime
        {
            get
            {
                return this.m_startTime;
            }
            set
            {
                this.m_startTime = value;
            }
        }

        public TimeSpan EndTime
        {
            get
            {
                return this.m_entTime;
            }
            set
            {
                this.m_entTime = value;
            }
        }

        public double FeeMinutes
        {
            get
            {
                return this.m_FeeMinutes;
            }
            set
            {
                this.m_FeeMinutes = value;
            }
        }

        public decimal FeeMoney
        {
            get
            {
                return this.m_FeeMoney;
            }
            set
            {
                this.m_FeeMoney = value;
            }
        }

        public decimal TopMoney
        {
            get
            {
                return this.m_TopMoney;
            }
            set
            {
                this.m_TopMoney = value;
            }
        }

        public double MinSpanTime
        {
            get
            {
                return this.m_MinSpanTime;
            }
            set
            {
                this.m_MinSpanTime = value;
            }
        }

        public int FareRuleId
        {
            get
            {
                return this.m_FareRuleId;
            }
            set
            {
                this.m_FareRuleId = value;
            }
        }


        [ForeignKey("FareRuleId")]
        public FareRule FareRule { get; set; }


        private TimeSpan? _marginTime = null;
        /// <summary>
        /// 相差时间段   endTime-StartTime
        /// </summary>
        public TimeSpan MarginTime
        {
            get
            {
                if (!_marginTime.HasValue)
                {
                    _marginTime = StartTime.MarginTime(EndTime);
                    #region 注释代码 用扩展方法重写
                    //if (EndTime.Hours > StartTime.Hours) { //不超过0点
                    //	_marginTime = EndTime - StartTime;
                    //}
                    //else {
                    //	/*
                    //		 光比较timeSpan endTime比startTime小的算法
                    //	 * entTime +1天 减去startTime
                    //	 */
                    //	var tempsTime = DateTime.Now;
                    //	var tempeTime = DateTime.Now.AddDays(1);
                    //	DateTime startTime = new DateTime(tempsTime.Year, tempsTime.Month, tempsTime.Day, StartTime.Hours, StartTime.Minutes, StartTime.Seconds);
                    //	DateTime endTime = new DateTime(tempeTime.Year, tempeTime.Month, tempeTime.Day, EndTime.Hours, EndTime.Minutes, EndTime.Seconds);
                    //	_marginTime = endTime - startTime;
                    //} 
                    #endregion
                }
                return _marginTime.Value;
            }
        }
        /// <summary>
        /// 区间收费金额
        /// </summary>
        public decimal RangeFees
        {
            get
            {
                var rangeMoney = ((decimal)(MarginTime.TotalMinutes / FeeMinutes)) * FeeMoney;
                if (FareRule.IsStartTopMoney) return rangeMoney > TopMoney ? TopMoney : rangeMoney;
                else return rangeMoney;
            }
        }

        /// <summary>
        /// 区间段是否超过0点  
        /// 结束时间 小于开始时间
        /// </summary>
        public bool isMoreThan0Time
        {
            get
            {
                return EndTime.Hours > StartTime.Hours;
            }
        }
    }
}
