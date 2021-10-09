using ERService.FunctionalCSharp;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace ERService.Wpf.Converters
{
    public class MouseButtonEventArgsForOpenItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            Maybe.From(value as MouseButtonEventArgs)
                 .ToResult("Expected MouseButtonEventArgs!")
                 .Bind(args => GetSfDataGridFromArgs(args))
                 .Ensure(grid => grid.SelectedItem != null, "Nothing selected")
                 .Map(grid => grid.SelectedItem)
                 .Finally(selectedItem => selectedItem.Value);

        private static Func<MouseButtonEventArgs, Result<SfDataGrid>> GetSfDataGridFromArgs { get; } =
            args => Maybe.From(args.Source as SfDataGrid).ToResult("Expected SfDataGrid!");

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
