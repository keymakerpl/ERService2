using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ERService.Infrastructure.Helpers
{
    /// <summary>
    /// The attached DependencyProperty solution uses a helper class so that it can be reused across your application. It also assumes that your DataGrid’s DataContext is your view model. If that is not the case, you will need to modify it.
    /// </summary>
    public sealed class RowDoubleClickHandler : FrameworkElement
    {
        public RowDoubleClickHandler(DataGrid dataGrid)
        {
            MouseButtonEventHandler handler = (sender, args) =>
            {
                var row = sender as DataGridRow;
                if (row != null && row.IsSelected)
                {
                    var methodName = GetMethodName(dataGrid);

                    var dataContextType = dataGrid.DataContext.GetType();
                    var method = dataContextType.GetMethod(methodName);
                    if (method == null)
                    {
                        throw new MissingMethodException(methodName);
                    }

                    method.Invoke(dataGrid.DataContext, null);
                }
            };

            dataGrid.LoadingRow += (s, e) =>
            {
                e.Row.MouseDoubleClick += handler;
            };

            dataGrid.UnloadingRow += (s, e) =>
            {
                e.Row.MouseDoubleClick -= handler;
            };
        }

        public static string GetMethodName(DataGrid dataGrid)
        {
            return (string)dataGrid.GetValue(MethodNameProperty);
        }

        public static void SetMethodName(DataGrid dataGrid, string value)
        {
            dataGrid.SetValue(MethodNameProperty, value);
        }

        public static readonly DependencyProperty MethodNameProperty = DependencyProperty.RegisterAttached(
            "MethodName",
            typeof(string),
            typeof(RowDoubleClickHandler),
            new PropertyMetadata((o, e) =>
        {
            var dataGrid = o as DataGrid;
            if (dataGrid != null)
            {
                new RowDoubleClickHandler(dataGrid);
            }
        }));
    }
}
