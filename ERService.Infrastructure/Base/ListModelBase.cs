using ERService.Infrastructure.Constants;
using ERService.Infrastructure.Events;
using ERService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERService.Infrastructure.Base
{
    public class FilterParameters<TEntity> where TEntity : class
    {
        public Expression<Func<TEntity, bool>> Predicate { get; set; }
        public Expression<Func<TEntity, object>>[] IncludeProp { get; set; }
    }

    public abstract class ListModelBase<TEntity, TContext> : GenericRepository<TEntity, TContext>, IListModelBase<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        public readonly IRegionManager _regionManager;
        public readonly IEventAggregator _eventAggregator;
        private TEntity _selectedModel;
        private bool _isReadOnly;
        private readonly ILogger _logger;

        public ListModelBase(TContext context, IRegionManager regionManager, IEventAggregator eventAggregator, ILogger logger) : base(context)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _logger = logger;

            AddCommand = new DelegateCommand(OnAddExecute, OnAddCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute, OnDeleteCanExecute);

            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(OnDetailSaved, true);
        }

        public FilterParameters<TEntity> CurrentFilterParameters { get; set; }

        private async void OnDetailSaved(AfterDetailSavedEventArgs args) => await RefreshListAsync();

        public DelegateCommand AddCommand { get; set; }

        public DelegateCommand DeleteCommand { get; set; }

        public ObservableCollection<TEntity> Models { get; set; } = new ObservableCollection<TEntity>();

        public virtual TEntity SelectedModel
        {
            get { return _selectedModel; }
            set { _selectedModel = value; DeleteCommand.RaiseCanExecuteChanged(); }
        }

        public bool IsReadOnly
        {
            get => _isReadOnly;
            set { _isReadOnly = value; AddCommand.RaiseCanExecuteChanged(); }
        }

        public virtual async Task LoadAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProps)
        {
            CurrentFilterParameters = new FilterParameters<TEntity>
            {
                Predicate = predicate,
                IncludeProp = includeProps
            };

            Models.Clear();
            var models = await FindByIncludeAsync(predicate, includeProps);
            foreach (var model in models)
            {
                Models.Add(model);
            }
        }

        public abstract void OnAddExecute();

        private bool OnAddCanExecute() => !IsReadOnly;

        public virtual bool OnDeleteCanExecute() => SelectedModel != null && !IsReadOnly;

        public virtual async void OnDeleteExecute()
        {
            Remove(SelectedModel);
            Models.Remove(SelectedModel);
            await SaveAsync();
        }

        public abstract void OnMouseDoubleClickExecute();
        
        public virtual void ShowDetail(NavigationParameters parameters)
        {
            var viewName = parameters.GetValue<string>("ViewFullName");
            var region = parameters.GetValue<string>("REGION") ?? RegionNames.ContentRegion;
            _regionManager.Regions[region].RequestNavigate(viewName, parameters);
        }

        public virtual async Task RefreshListAsync()
        {
            if (CurrentFilterParameters == null)
                return;

            await ReloadEntitiesAsync().ContinueWith(async (t) =>
            {
                if (t.Status == TaskStatus.RanToCompletion)
                {

                    await LoadAsync(CurrentFilterParameters.Predicate, CurrentFilterParameters.IncludeProp);
                }

                if (t.Status == TaskStatus.Faulted && t.Exception != null)
                {
                    _logger.LogError(t.Exception, t.Exception.Message);
                }
            },
            TaskContinuationOptions.ExecuteSynchronously);
        }
    }
}