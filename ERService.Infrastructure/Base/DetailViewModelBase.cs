﻿using ERService.Infrastructure.Dialogs;
using ERService.Infrastructure.Events;
using ERService.Infrastructure.Notifications.ToastNotifications;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERService.Infrastructure.Base
{
    public abstract class DetailViewModelBase : BindableBase, IDetailViewModelBase, IConfirmNavigationRequest, IRegionMemberLifetime
    {
        protected readonly IEventAggregator _eventAggregator;
        protected readonly IMessageDialogService _messageDialogService;

        private string _title;
        private bool _isReadOnly;
        private bool _hasChanges;
        private readonly ILogger _logger;

        public DetailViewModelBase(IEventAggregator eventAggregator, IMessageDialogService messageDialogService, ILogger logger) {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _logger = logger;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            CloseCommand = new DelegateCommand(OnCloseDetailViewExecute);
            CancelCommand = new DelegateCommand(OnCancelEditExecute);
        }

        public DelegateCommand SaveCommand { get; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }

        /// <summary>
        /// Właściwośc pomocnicza do przechowania zmiany z repo, odpala even jeśli w repo zaszły  zmiany
        /// </summary>
        public bool HasChanges {
            get { return _hasChanges; }
            set {
                SetProperty(ref _hasChanges, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public Guid ID { get; protected set; }

        public bool IsReadOnly { get => _isReadOnly; set { SetProperty(ref _isReadOnly, value); } }

        public string Title {
            get { return _title; }
            protected set {
                SetProperty(ref _title, value);
            }
        }

        public CultureInfo CustomCulture { get => new CultureInfo("pl-PL"); }

        public virtual void Load() { }

        public virtual void Load(Guid id) { }

        public virtual Task LoadAsync() => Task.CompletedTask;

        public virtual Task LoadAsync(Guid id) => Task.CompletedTask;

        #region Events and Events Handlers

        protected virtual void OnCancelEditExecute() { }

        protected virtual void OnCloseDetailViewExecute() {
            _eventAggregator.GetEvent<AfterDetailClosedEvent>().Publish(new AfterDetailClosedEventArgs() {
                Id = this.ID,
                ViewModelName = this.GetType().Name
            });
        }

        protected virtual bool OnSaveCanExecute() => HasChanges && !IsReadOnly;

        protected virtual void OnSaveExecute() { }

        protected virtual void RaiseDetailOpenedEvent(Guid modelId, string displayMember) {
            _eventAggregator.GetEvent<AfterDetailOpenedEvent>().Publish(new AfterDetailOpenedEventArgs {
                Id = modelId,
                DisplayableName = displayMember,
                ViewModelName = GetType().Name
            });
        }

        protected virtual void RaiseDetailSavedEvent(Guid modelId, string displayMember) {
            var title = String.IsNullOrWhiteSpace(displayMember) ? "Zapisano..." : displayMember;
            _messageDialogService.ShowInsideContainer(title, "Zmiany zostały poprawnie zapisane.", NotificationTypes.Success);

            _eventAggregator.GetEvent<AfterDetailSavedEvent>()
                .Publish(new AfterDetailSavedEventArgs() {
                    Id = modelId,
                    DisplayMember = displayMember,
                    ViewModelName = this.GetType().Name
                });
        }

        protected virtual void RaiseDetailDeletedEvent(Guid modelId, string displayMember) {
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>()
                .Publish(new AfterDetailDeletedEventArgs() {
                    Id = modelId,
                    ViewModelName = this.GetType().Name
                });
        }

        #endregion Events and Events Handlers

        /// <summary>
        /// Zapisuje optymistycznie, ze sprawdzaniem czy nadpisać
        /// </summary>
        /// <param name="saveFunc">Funkcja z repo SaveAsync() zwracająca Task</param>
        /// <param name="afterSaveAction">Metoda wykonuje instrukcje po zapisie, możesz użyć lambdy</param>
        /// <returns></returns>
        protected async Task SaveWithOptimisticConcurrencyAsync(Func<Task> saveFunc, Action afterSaveAction) {
            try {
                await saveFunc();
                RaiseDetailSavedEvent(ID, Title);
            }
            //catch (DbUpdateConcurrencyException e) // rowversion się zmienił - ktoś inny zmienił dane
            //{
            //    var databaseValues = e.Entries.Single().GetDatabaseValues();
            //    if (databaseValues == null) // sprawdzamy czy jest nadal w bazie
            //    {
            //        await _messageDialogService
            //            .ShowInformationMessageAsync(this, "Usunięty element...", "Element został w międzyczasie usunięty przez innego użytkownika.");

            //        return;
            //    }

            //    var dialogResult =
            //        await _messageDialogService
            //        .ShowConfirmationMessageAsync(this, "Dane zmienione przez innego użytkownika...", "Dane zostały zmienione w międzyczasie przez innego użytkownika, czy nadpisać aktualne dane?");

            //    if (dialogResult == DialogResult.OK) {
            //        var entry = e.Entries.Single(); //pobierz krotkę której nie można zapisać
            //        entry.OriginalValues.SetValues(entry.GetDatabaseValues()); //pobierz aktualne dane z db (aby zaktualizować rowversion w current row)
            //        await saveFunc(); //zapisz

            //        RaiseDetailSavedEvent(ID, Title);
            //    }
            //    else {
            //        await e.Entries.Single().ReloadAsync(); //przeładuj cache krotki z bazy
            //        await LoadAsync(ID); //załaduj ponownie model
            //    }
            //}
            catch (Exception e) {
                _logger.LogError(e, "ERROR");
                _messageDialogService.ShowInsideContainer("Błąd...", "Szczegóły błędu znajdują się w dzienniku aplikacji.", NotificationTypes.Error);
                return;
            }

            afterSaveAction();
        }

        #region Navigation

        public virtual async void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback) {
            var dialogResult = true;
            if (!IsReadOnly && HasChanges) {
                dialogResult = await _messageDialogService.ShowConfirmationMessageAsync(this, "Nie zapisane dane...", "Nie zapisano zmienionych danych, kontynuować?")
                    == DialogResult.OK;
            }

            continuationCallback(dialogResult);
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext) => false;

        public virtual void OnNavigatedTo(NavigationContext navigationContext) { }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext) =>
            _eventAggregator.GetEvent<AfterDetailClosedEvent>()
                            .Publish(new AfterDetailClosedEventArgs {
                                Id = ID,
                                ViewModelName = GetType().Name
                            });

        public virtual bool KeepAlive => false;

        protected virtual void OnNavigatedResult(NavigationResult arg) {
            var message = $"Navigated to: {arg.Context.Uri} with result: {arg.Result}.";
            var stringBuilder = new StringBuilder(message);
            if (arg.Error != null) {
                stringBuilder.Append(" Error: \n")
                             .Append(arg.Error.Message)
                             .Append('.');

                if (arg.Error.InnerException != null) {
                    stringBuilder.Append(" \nInner exception: \n")
                                 .Append(arg.Error.InnerException.Message)
                                 .Append('.');
                }
            }

            _logger.LogError(stringBuilder.ToString());
        }

        #endregion Navigation
    }
}