using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using LifeTrack.Core.Models;
using LifeTrack.Mobile.Helpers;

namespace LifeTrack.Mobile.Converters
{
    public class CategoryColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Category category)
            {
                return Color.FromArgb(CategoryColorHelper.GetColorForCategory(category.ColorHex));
            }

            return Colors.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
