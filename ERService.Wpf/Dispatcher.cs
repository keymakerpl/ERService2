using System;
using System.Windows;

namespace ERService.Wpf
{
    public static class Dispatcher
    {
        public static void Invoke(Action action) => 
            Application.Current.Dispatcher.Invoke(action);
    }
}
