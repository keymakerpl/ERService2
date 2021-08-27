using System.Windows;
using System.Windows.Controls;

namespace ERService.Infrastructure.Helpers
{
    public static class ReadOnlyContainer
    {
        public static bool GetIsReadOnly(UIElement element)
        {
            return (bool)element.GetValue(IsReadOnlyProperty);
        }

        public static void SetIsReadOnly(UIElement element, bool value)
        {
            element.SetValue(IsReadOnlyProperty, value);
        }

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.RegisterAttached("IsReadOnly", typeof(bool), typeof(ReadOnlyContainer), 
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, OnReadOnlyChanged));

        private static void OnReadOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var container = d as Panel;
            if (container != null)
            {
                foreach (var child in container.Children)
                {
                    var control = child as UIElement;
                    if (control != null)
                        control.IsEnabled = !(bool)e.NewValue;
                }
            }
        }
    }

    public static class WebBrowserUtility
    {
        public static string GetContent(DependencyObject obj)
        {
            return (string)obj.GetValue(ContentProperty);
        }

        public static void SetContent(DependencyObject obj, string value)
        {
            obj.SetValue(ContentProperty, value);
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.RegisterAttached("Content", typeof(string), typeof(WebBrowserUtility),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.Inherits, OnContentChanged));

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var webBrowser = d as WebBrowser;
            if (webBrowser != null)
            {                
                webBrowser.NavigateToString((string)e.NewValue);
            }
        }
    }

}
