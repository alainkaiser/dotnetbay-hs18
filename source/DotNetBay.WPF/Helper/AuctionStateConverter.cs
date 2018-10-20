using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DotNetBay.WPF.Helper
{
    public class AuctionStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = "Offen";
            if (!(bool)value)
            {
                result = "Abgeschlossen";
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = true;
            if (((string)value).ToLower() == "abgeschlossen")
            {
                result = false;
            }

            return result;
        }
    }
}
