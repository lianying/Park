using Park.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Park.EnumHelper;
using System.ComponentModel;

namespace Park.Converter
{
    public class EntranceTypeEnumConverter : IValueConverter
    {
        /// <summary>
        /// in Out Enum Converter
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EntranceType)
            {
                var type = (EntranceType)value;
                return type.GetAttributeOfType<DescriptionAttribute>();
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
