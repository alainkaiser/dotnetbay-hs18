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
        private const string AuctionStateOpen = "Offen";
        private const string AuctionStateClosed = "Abgeschlossen";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && (bool)value)
            {
                return AuctionStateClosed;
            }

            return AuctionStateOpen;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
