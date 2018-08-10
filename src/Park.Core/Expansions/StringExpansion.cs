using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Expansions
{
    public static class StringExpansion
    {
        /// <summary>
        /// 是否为无牌车
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsNoBrandCar(this string number) {
            return number.IsNullOrEmpty() || number.Equals("未识别") || number.Equals("无车牌");
        }

        //public static bool IsNullOrEmpty(this string str)
        //{
        //    return string.IsNullOrEmpty(str);
        //}

        public static bool IsNoNumber(this string str)
        {
            return IsNoBrandCar (str);
        }
    }
}
