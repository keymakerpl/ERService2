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

    [ValueConversion(typeof(ReadOnlyObservableCollection<ValidationError>), typeof(string))]
    public class ValidationErrorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            Maybe.From(value as ReadOnlyObservableCollection<ValidationError>).ToResult("Value is empty!")
                 .Ensure(validationErrors => validationErrors.Any(), "No errors!")
                 .OnFailure(error => Debug.WriteLine($"ValidationErrorConverter: {error}"))
                 .Match(onSuccess: errors => errors.First().ErrorContent, onFailure: _ => "");

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
