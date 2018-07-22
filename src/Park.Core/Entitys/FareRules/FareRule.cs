using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.FareRules
{
    public class FareRule
    {
        private int m_id;

        private int m_parkId;

        private int m_ModeId;

        private int m_CarTypeId;

        private string m_Name;

        private float m_FreeTime;

        private bool m_IsStartTopMoney;

        private decimal m_DayMaxMoney;

        private float m_preFeeDate;

        public FareRule()
        {
            base.InitMetaData();
        }

        public int id
        {
            get
            {
                return this.m_id;
            }
            set
            {
                this.m_id = value;
            }
        }

        public int parkId
        {
            get
            {
                return this.m_parkId;
            }
            set
            {
                this.m_parkId = value;
            }
        }

        public int ModeId
        {
            get
            {
                return this.m_ModeId;
            }
            set
            {
                this.m_ModeId = value;
            }
        }

        public int CarTypeId
        {
            get
            {
                return this.m_CarTypeId;
            }
            set
            {
                this.m_CarTypeId = value;
            }
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
            }
        }

        public float FreeTime
        {
            get
            {
                return this.m_FreeTime;
            }
            set
            {
                this.m_FreeTime = value;
            }
        }

        public bool IsStartTopMoney
        {
            get
            {
                return this.m_IsStartTopMoney;
            }
            set
            {
                this.m_IsStartTopMoney = value;
            }
        }

        public decimal DayMaxMoney
        {
            get
            {
                return this.m_DayMaxMoney;
            }
            set
            {
                this.m_DayMaxMoney = value;
            }
        }
        public float preFeeDate
        {
            get
            {
                return this.m_preFeeDate;
            }
            set
            {
                this.m_preFeeDate = value;
            }
        }

        public bool IsInculdeFeeTime { get; set; }
        /// <summary>
        /// 时间段
        /// </summary>
        public List<RangeTime> TimeRangeList { get; set; }

        /// <summary>
        /// 算费
        /// </summary>
        /// <param name="inTime"></param>
        /// <param name="outTime"></param>
        /// <returns></returns>
        public decimal CalculateFees(DateTime inTime, DateTime outTime, double disCountTime)
        {
            //var task = Task.Run(() => {
            try
            {
                if (disCountTime > 0)
                {
                    outTime = outTime.AddMinutes(-disCountTime);
                }

                /*
                    判断停车时长是否超过一天 
                 * 将金额拆 为两部分
                 * 一部分为 整天数
                 * 和不足一天的停车时长
                 */
                var span = outTime - inTime;
                if (span.Days > 0)
                { // 超过一天 拆为两部分算钱
                  //计算一天的费用
                    var dayMoney = fareRegister(inTime, inTime.AddDays(1)); ;
                    var letMoney = fareRegister(inTime.AddDays(span.Days), outTime);
                    letMoney = letMoney > DayMaxMoney ? DayMaxMoney : letMoney;
                    dayMoney = dayMoney > DayMaxMoney ? DayMaxMoney : dayMoney; //判断是否超过一天最高金额
                    return dayMoney * span.Days + letMoney;

                }
                else
                {
                    var money = fareRegister(inTime, outTime);
                    return money > DayMaxMoney ? DayMaxMoney : money;
                }

            }
            catch (Exception e)
            {
                return 0;
            }
            //});
            //Task.WaitAll(new Task[1] { task }, 1000);
            //if (task.IsCompleted) {
            //return task.Result;
            //}
            //算费超时
            //return -3; 
        }

        /// <summary>
        /// 计费器 只负责计算费用  最多只会计算1天的金额
        /// </summary>
        /// <param name="inTime"></param>
        /// <param name="outTime"></param>
        /// <returns></returns>
        private decimal fareRegister(DateTime inTime, DateTime outTime)
        {
            var parkDetail = new ParkingDetails(inTime, outTime, TimeRangeList);
            if (parkDetail.ParkInfo.TotalMinutes < 1 || parkDetail.ParkInfo.TotalMinutes <= FreeTime || inTime > outTime) //不足1分钟和在免费时长内 收费为0
                return 0;
            var inTimeRange = parkDetail.InTimeRange;
            if (!IsInculdeFeeTime)
            {
                inTime = inTime.AddMinutes(FreeTime); //不包含免费时长   算费时-去免费时长
            }
            var whileInTime = inTime;
            decimal payMoney = 0, rangeMoney = 0;
            while (true)
            {
                whileInTime = whileInTime.AddMinutes(inTimeRange.Item1.FeeMinutes);

                /*
					判断当前时间是否超出当前区间  
				 * 超出后 重新为inTimeRange赋值
				 * 并判断出场时间是否小于最小跨度时间
				 *  若小于最小跨度时间 则不跳费
				 *  大于最大跨度时间 则跳一次费
				 */
                if (whileInTime >= inTimeRange.Item2.Item2)
                { //当前时间大于区间结束时间
                    if (whileInTime == inTimeRange.Item2.Item2)
                    {
                        rangeMoney += inTimeRange.Item1.FeeMoney;
                    }

                    if (inTimeRange.Item2.Item2 != whileInTime && (inTimeRange.Item2.Item2 - whileInTime.AddMinutes(-inTimeRange.Item1.FeeMinutes)).TotalMinutes > inTimeRange.Item1.MinSpanTime)
                    {
                        //距离区间结束最近的跳费时间 是否大于当前区间的最小跨度时间  如果大于则跳一次费 
                        rangeMoney += inTimeRange.Item1.FeeMoney;
                    }

                    whileInTime = inTimeRange.Item2.Item2.AddMinutes(1); //区间算费结束 将算费时间置于下个区间的开始时间

                    if (IsStartTopMoney)
                    { //启用封顶金额
                        rangeMoney = rangeMoney > inTimeRange.Item1.TopMoney ? inTimeRange.Item1.TopMoney : rangeMoney;
                        payMoney += rangeMoney;
                        rangeMoney = 0; //当前区间结束 重置区间金额
                                        //continue;
                                        /*
                                                                                                                                bug : 跨多区间跨天时   多区间的价格肯定大于当前区间的封顶金额  此时payMoney被重置为当前区间的封顶金额  
                                                                                                                                   */
                    }
                    //payMoney -= inTimeRange.Item1.FeeMoney;   //此时循环金额已经加了  但是已经进入下个区间 应以下个区间段算费  减去之前加的金额  

                    parkDetail = new ParkingDetails(inTimeRange.Item2.Item2.AddMinutes(1), outTime, TimeRangeList);
                    inTimeRange = parkDetail.InTimeRange;
                    continue;
                    //payMoney += inTimeRange.Item1.FeeMoney;
                }

                rangeMoney += inTimeRange.Item1.FeeMoney;  //跳费   区间收费

                if (whileInTime >= outTime)
                { //超出出场时间
                    if (rangeMoney != 0)
                    {  //结算当前区间的收费金额
                        rangeMoney = rangeMoney > inTimeRange.Item1.TopMoney ? inTimeRange.Item1.TopMoney : rangeMoney;
                        payMoney += rangeMoney;
                    }
                    if ((outTime - whileInTime.AddMinutes(-inTimeRange.Item1.FeeMinutes)).TotalMinutes <= inTimeRange.Item1.MinSpanTime)
                    { //出场时间超出最小跨度时间
                        payMoney -= inTimeRange.Item1.FeeMoney;
                    }
                    break;
                }

            }

            return payMoney;
        }


        public double NextFeeTime(DateTime inTime, DateTime selTime)
        {
            try
            {
                var allTime = (selTime - inTime).TotalMinutes;
                var parkDetail = new ParkingDetails(selTime, selTime.AddDays(1), TimeRangeList);
                var inTimeRange = parkDetail.InTimeRange;

                var count = (int)(allTime / inTimeRange.Item1.FeeMinutes);
                var nextAllTime = (count + 1) * inTimeRange.Item1.FeeMinutes;

                //parkDetail=new ParkingDetails()
                return (count + 1) * inTimeRange.Item1.FeeMinutes - allTime;

            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
