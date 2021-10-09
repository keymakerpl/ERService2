using ERService.FunctionalCSharp;
using System;
using System.Linq;
using System.Globalization;
using System.Windows.Data;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace ERService.Wpf.Converters
{
    [ValueConversion(typeof(ReadOnlyObservableCollection<ValidationError>), typeof(bool))]
    public class HasErrorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            Maybe.From(value as ReadOnlyObservableCollection<ValidationError>).ToResult("Value is empty!")
                 .Ensure(validationErrors => validationErrors.Any(), "No errors!")
                 .OnFailure(error => Debug.WriteLine($"HasErrorConverter: {error}"))
                 .Match(onSuccess: _ => true, onFailure: _ => false);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
