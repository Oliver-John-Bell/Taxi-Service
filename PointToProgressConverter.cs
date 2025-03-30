using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRoupHI
{
    public class PointToProgressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert points to progress value between 0 and 1
            if (value is int points)
            {
                return (double)points / 10.0; // Assuming 10 points for full progress
            }
            return 0.0; // Default to 0 progress if points are not valid
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
