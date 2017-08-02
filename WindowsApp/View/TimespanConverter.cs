using System;
using System.Globalization;
using System.Windows.Data;

namespace Workday.View
{
    /// <summary>
    /// Class for the <see cref="TimeSpan"/> converter that creates a time string to display.
    /// </summary>
    public class TimeSpanConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="TimeSpan"/> to a time string.
        /// </summary>
        /// <param name="value">TimeSpan to convert.</param>
        /// <returns>Time string to display.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeSpan = value as TimeSpan?;
            return timeSpan.Value.ToString(@"hh\:mm\:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
