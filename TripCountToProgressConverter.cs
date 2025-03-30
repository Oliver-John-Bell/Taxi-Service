using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRoupHI
{
    public class TripCountToProgressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert the trip count to a progress value between 0 and 1
            if (value is int tripCount)
            {
                // Assuming a maximum trip count of 100 for simplicity
                return (double)tripCount / 100.0;
            }
            return 0.0; // Default to 0 progress if the trip count is not valid
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
