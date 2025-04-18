﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace LifeTrack.Desktop.Converters
{
    public class CapitalizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value?.ToString().ToLower());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}