using Syncfusion.UI.Xaml.TextInputLayout;
using System.Windows;

namespace ERService.Wpf.AttachedProperties
{
    public class SfTextInputLayoutExtensions : DependencyObject
    {
        public static readonly DependencyProperty HasErrorProperty =
        DependencyProperty.RegisterAttached("HasError", typeof(bool), typeof(SfTextInputLayout), 
            new PropertyMetadata(OnPropertyChange));

        private static void OnPropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sfTextInputLayout = d as SfTextInputLayout;
        }

        public static void SetNotifyDataErrorInfo(UIElement element, bool value)
        {
            element.SetValue(HasErrorProperty, value);
        }

        public static bool GetNotifyDataErrorInfo(UIElement element)
        {
            return (bool)element.GetValue(HasErrorProperty);
        }
    }
}
