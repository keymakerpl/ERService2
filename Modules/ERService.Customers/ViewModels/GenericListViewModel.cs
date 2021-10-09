using ERService.FunctionalCSharp;
using ERService.Mvvm.Base;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ERService.Customers.ViewModels
{
    public abstract class GenericListViewModel<TProjection, TSource> : DetailViewModelBase
        where TProjection : class
        where TSource : class
    {
        public GenericListViewModel()
        {
            AddItemCommand = new DelegateCommand(OnAddItemExecute);
            RemoveItemCommand = new DelegateCommand(async () => await OnRemoveItemExecute());
            ItemMouseDoubleClickCommand = new DelegateCommand<TProjection>(OnMouseDoubleClickExecute);
            FilterChangingCommand = new DelegateCommand<Expression<Func<TSource, bool>>>(args => OnFilterChanging(args));
        }

        public ICommand AddItemCommand { get; }
        public ICommand RemoveItemCommand { get; }
        public ICommand ItemMouseDoubleClickCommand { get; }
        public ICommand FilterChangingCommand { get; }

        public TProjection SelectedItem { get; set; }
        public virtual IEnumerable<TProjection> Items { get; init; } = Array.Empty<TProjection>();

        protected abstract Func<Result> AddItem { get; }
        protected virtual Action OnItemAdded { get; } = () => Debug.WriteLine("OnItemAdded has been invoked");
        protected abstract Func<TProjection, Task<Result>> RemoveItem { get; }
        protected virtual Action OnItemRemoved { get; } = () => Debug.WriteLine("OnItemRemoved has been invoked");
        protected abstract Action<TProjection> OpenItem { get; }
        protected abstract Action<LoadItemsParameters<TSource>> LoadItems { get; }
        protected virtual Action<Expression<Func<TSource, bool>>> OnFilterChanged { get; } = 
            filter => Debug.WriteLine($"Filter changed to: {filter}");

        protected Expression<Func<TSource, bool>> Filter { get; set; } = _ => true;

        protected Action<Expression<Func<TSource, bool>>> OnFilterChanging => predicate =>
            Maybe.From(predicate)
                 .ToResult(error: "Empty predicate!")
                 .Tap(predicate => Filter = predicate)
                 .Tap(OnFilterChanged);

        private void OnAddItemExecute() => 
            AddItem().Tap(OnItemAdded)
                     .OnFailure(error => Debug.WriteLine(error));

        private async Task OnRemoveItemExecute() =>
            await Maybe.From(SelectedItem)
                       .ToResult("Nothing selected!")
                       .Bind(selectedItem => RemoveItem(selectedItem))
                       .Tap(OnItemRemoved)
                       .OnFailure(error => Debug.WriteLine(error));

        private Action<TProjection> OnMouseDoubleClickExecute =>
            selectedItem => Maybe.From(selectedItem)
                                 .ToResult("Selected item is null!")
                                 .Tap(OpenItem);
    }
}