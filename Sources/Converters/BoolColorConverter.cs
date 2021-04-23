using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace DxTBoxCore.Converters
{
    class BoolColorConverter : IMultiValueConverter
    {
        /*
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
         * Color c = Colors.Red;
        if ((bool)value)
            c = Colors.Green;

        return new SolidColorBrush(c);
    }
        */
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool? state = (bool?)values[0];
            Color c4True = (Color)values[1];
            Color c4False = (Color)values[2];
            Color c4Null = (Color)values[3];

            if (state == true)
                return new SolidColorBrush(c4True);
            else if (state == false)
                return new SolidColorBrush(c4False);
            else
                return new SolidColorBrush( c4Null);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
