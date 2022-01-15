using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace DxTBoxCore.Converters
{
    public class BoolColorConverter : IMultiValueConverter
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

            if (values.Length > 0 && state == true)
                return new SolidColorBrush((Color)values[1]);
            else if (values.Length > 1 && state == false)
                return new SolidColorBrush((Color)values[2]);
            else if (values.Length > 2)
                return new SolidColorBrush((Color)values[3]);
            else
                return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
