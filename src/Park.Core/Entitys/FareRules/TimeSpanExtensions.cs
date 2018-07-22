using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.FareRules
{
    public static class TimeSpanExtensions
    {
        public static TimeSpan MarginTime(this TimeSpan start, TimeSpan end)
        {
            //TimeSpan? _marginTime = null;
            if (end.Hours > start.Hours)
            { //不超过0点
                return end - start;
            }
            else
            {
                /*
					 光比较timeSpan endTime比startTime小的算法
				 * entTime +1天 减去startTime
				 */
                var tempsTime = DateTime.Now;
                var tempeTime = DateTime.Now.AddDays(1);
                DateTime startTime = new DateTime(tempsTime.Year, tempsTime.Month, tempsTime.Day, start.Hours, start.Minutes, start.Seconds);
                DateTime endTime = new DateTime(tempeTime.Year, tempeTime.Month, tempeTime.Day, end.Hours, end.Minutes, end.Seconds);
                return endTime - startTime;
            }
        }
        /// <summary>
        /// TimeSpanToDateTime
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="inTime"></param>
        /// <returns></returns>
        public static Tuple<DateTime, DateTime> TimeSpan2DateTime(this TimeSpan start, TimeSpan end, DateTime inTime)
        {
            DateTime startTime, endTime;

            startTime = new DateTime(inTime.Year, inTime.Month, inTime.Day, start.Hours, start.Minutes, start.Seconds);

            if (start.Hours < end.Hours)
            {
                endTime = new DateTime(inTime.Year, inTime.Month, inTime.Day, end.Hours, end.Minutes, end.Seconds);
            }
            else
            {
                var tempTime = inTime.AddDays(1);
                endTime = new DateTime(tempTime.Year, tempTime.Month, tempTime.Day, end.Hours, end.Minutes, end.Seconds);
            }
            if (inTime < startTime)
            {
                startTime = startTime.AddDays(-1);
                endTime = endTime.AddDays(-1);
            }
            return new Tuple<DateTime, DateTime>(startTime, endTime);

        }
    }
}
