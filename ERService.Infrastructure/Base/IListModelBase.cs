﻿using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Regions;

namespace ERService.Infrastructure.Base
{
    public interface IListModelBase<T>
    {
        DelegateCommand AddCommand { get; set; }
        DelegateCommand DeleteCommand { get; set; }
        ObservableCollection<T> Models { get; set; }
        T SelectedModel { get; set; }
        bool IsReadOnly { get; set; }
        
        Task LoadAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProps);

        Task RefreshListAsync();

        void ShowDetail(NavigationParameters parameters);
        void OnMouseDoubleClickExecute();
        void OnAddExecute();
        void OnDeleteExecute();
        bool OnDeleteCanExecute();
    }
}