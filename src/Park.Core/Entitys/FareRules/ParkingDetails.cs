using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.FareRules
{
    public class ParkingDetails
    {
        private DateTime inTime, outTime;

        private List<RangeTime> timeRangeList;
        public ParkingDetails(DateTime inTime, DateTime outTime, List<RangeTime> timeRange)
        {
            this.inTime = inTime;
            this.outTime = outTime;
            this.timeRangeList = timeRange;
        }

        #region 停车时长相关TimeSpan
        private TimeSpan? parkInfo;
        /// <summary>
        /// 停车时长相关
        /// </summary>
        public TimeSpan ParkInfo
        {

            get
            {
                if (!parkInfo.HasValue)
                {
                    parkInfo = outTime - inTime;
                }
                return parkInfo.Value;
            }
        }

        public int FullDays
        {
            get
            {
                return ParkInfo.Days;
            }
        }
        #endregion
        private Tuple<RangeTime, Tuple<DateTime, DateTime>> inTimeRange = null;

        /// <summary>
        /// 入场时间在哪个时间段
        /// </summary>
        public Tuple<RangeTime, Tuple<DateTime, DateTime>> InTimeRange
        {
            get
            {
                if (inTimeRange == null)
                {
                    var timeList = timeRangeList.Select(x => {
                        return new Tuple<RangeTime, Tuple<DateTime, DateTime>>(x, x.StartTime.TimeSpan2DateTime(x.EndTime, inTime));
                    });
                    var first = timeList.First(x => x.Item2.Item1 <= inTime && x.Item2.Item2 >= inTime);
                    inTimeRange = first;
                }
                return inTimeRange;
            }
        }
    }
}
