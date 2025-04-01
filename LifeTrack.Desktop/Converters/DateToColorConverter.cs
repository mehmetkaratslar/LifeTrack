using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LifeTrack.Desktop.Converters
{
    public class DateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            var days = (DateTime.Now - date).TotalDays;

            return days > 30
                ? new LinearGradientBrush(Colors.LightPink, Colors.Red, 90)
                : new LinearGradientBrush(Colors.LightGreen, Colors.MediumSeaGreen, 90);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}