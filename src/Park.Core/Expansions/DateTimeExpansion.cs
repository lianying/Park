using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Expansions
{
    public static  class DateTimeExpansion
    {
        public static string ToTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd hh:mm:ss");
        }
    }
}
