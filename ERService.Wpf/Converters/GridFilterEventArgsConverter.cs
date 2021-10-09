using ERService.FunctionalCSharp;
using ERService.Wpf.Extensions;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;

namespace ERService.Wpf.Converters
{
    public class GridFilterEventArgsConverter : IValueConverter
    {
        Func<object, Type> GetGenericListViewModelSourceType { get; } = 
            parameter => parameter.GetType().BaseType.GenericTypeArguments[1];

        Func<Type, MethodInfo> CreateMethodToInvoke { get; } =
            type => typeof(GridFilterEventArgsExtensions).GetMethods(BindingFlags.Static | BindingFlags.Public)
                                                         .Where(method => method.Name == "ToExpressionOf")
                                                         .SingleOrDefault()
                                                         .MakeGenericMethod(type);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            Maybe.From(parameter)
                 .ToResult($"{nameof(parameter)} is null!")
                 .Map(parameter => GetGenericListViewModelSourceType(parameter))
                 .Map(sourceType => CreateMethodToInvoke(sourceType))
                 .Ensure(_ => value is GridFilterEventArgs, "Expected value to be of type GridFilterEventArgs")
                 .Map(methodInfo => (Method: methodInfo, EventArgs: value as GridFilterEventArgs))
                 .OnFailure(error => Debug.WriteLine(error))
                 .Match(onFailure: _ => null,
                        onSuccess: methodData =>
                        methodData.Method.Invoke(methodData.EventArgs, new object[] { methodData.EventArgs })
                        );

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            throw new NotImplementedException();
    }
}
