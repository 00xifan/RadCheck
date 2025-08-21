using RadCheck.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace RadCheck
{
    /// <summary>
    /// 将探头状态转换为颜色
    /// </summary>
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ProbeStatus status)
            {
                switch (status)
                {
                    case ProbeStatus.Normal:
                        return new SolidColorBrush(Colors.Green);
                    case ProbeStatus.Warning:
                        return new SolidColorBrush(Colors.Orange);
                    case ProbeStatus.Alarm:
                        return new SolidColorBrush(Colors.Red);
                }
            }
            return new SolidColorBrush(Colors.Gray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
