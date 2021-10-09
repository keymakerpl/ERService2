using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ERService.Mvvm.Base
{
    public abstract class DetailViewModelBase : BindableBase
    {
        public DetailViewModelBase()
        {
            SaveCommand = new DelegateCommand<object>(OnSaveExecute, OnSaveCanExecute);
            GoBackCommand = new DelegateCommand<object>(OnGoBackExecute);
        }

        protected virtual Action<object> OnSaveExecute { get; } = _ => Debug.WriteLine("OnSave executed!");
        protected virtual Func<object, bool> OnSaveCanExecute { get; } = _ => false;
        protected virtual Action<object> OnGoBackExecute { get; } = _ => Debug.WriteLine("OnGoBack executed!");

        protected ICommand SaveCommand { get; }
        protected ICommand GoBackCommand { get; }
    }
}
