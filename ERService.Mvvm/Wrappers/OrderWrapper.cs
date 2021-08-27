using ERService.DataAccess.EntityFramework.Entities;
using ERService.Mvvm.Base;
using System;

namespace ERService.Mvvm.Wrappers
{
    public class OrderWrapper : AuditableWrapper<int, Order>, IWrappable
    {
        private string _comment;

        private string _cost;

        private DateTime? _dateEnded;

        private DateTime _dateRegistered;

        private string _externalNumber;

        private string _fault;

        private string _number;

        private OrderStatus _orderStatus;

        private Guid? _orderStatusId;

        private OrderType _orderType;

        private Guid? _orderTypeId;

        private int _progress;

        private string _solution;

        private User _user;

        private Guid? _userId;

        public OrderWrapper(Order model) : base(model)
        {
        }

        /// <summary>
        /// Returns Barcode in Base64String format for Html usage
        /// </summary>
        public string BarcodeBase64
        {
            get
            {
                return string.Empty;
            }
        }

        public string Comment
        {
            get { return GetProperty<string>(); }
            set { SetProperty(ref _comment, value); }
        }

        public string Cost
        {
            get { return GetProperty<string>(); }
            set { SetProperty(ref _cost, value); }
        }

        public DateTime DateAdded
        {
            get { return GetProperty<DateTime>(); }
        }

        public DateTime? DateEnded
        {
            get { return GetProperty<DateTime?>(); }
            set { SetProperty(ref _dateEnded, value.Value.Date.AddDays(1).AddMilliseconds(-1)); }
        }

        public DateTime DateModified
        {
            get { return GetProperty<DateTime>(); }
        }

        public DateTime DateRegistered
        {
            get { return GetProperty<DateTime>(); }
            set { SetProperty(ref _dateRegistered, value); }
        }

        public string ExternalNumber
        {
            get { return GetProperty<string>(); }
            set { SetProperty(ref _externalNumber, value); }
        }

        public string Fault
        {
            get { return GetProperty<string>(); }
            set { SetProperty(ref _fault, value); }
        }

        public OrderStatus OrderStatus
        {
            get { return GetProperty<OrderStatus>(); }
            set { SetProperty(ref _orderStatus, value); }
        }

        public Guid? OrderStatusId
        {
            get { return GetProperty<Guid?>(); }
            internal set { SetProperty(ref _orderStatusId, value); }
        }

        public OrderType OrderType
        {
            get { return GetProperty<OrderType>(); }
            set { SetProperty(ref _orderType, value); }
        }

        public Guid? OrderTypeId
        {
            get { return GetProperty<Guid?>(); }
            internal set { SetProperty(ref _orderTypeId, value); }
        }

        public int Progress
        {
            get { return GetProperty<int>(); }
            set { SetProperty(ref _progress, value); }
        }

        public string Solution
        {
            get { return GetProperty<string>(); }
            set { SetProperty(ref _solution, value); }
        }

        /// <summary>
        /// Właściwość pomocnicza do ustawiania czasu w dacie rejestracji przez kontrolke DateTimePicker
        /// </summary>
        public TimeSpan TimeRegistered
        {
            get { return DateRegistered.TimeOfDay; }
            set { DateRegistered = DateRegistered.Date.Add(value); RaisePropertyChanged(); }
        }
        
        public User User
        {
            get { return GetProperty<User>(); }
            set { SetProperty(ref _user, value); }
        }

        public string UserFullName { get => User.FullName; }

        public Guid? UserId
        {
            get { return GetProperty<Guid?>(); }
            set { SetProperty(ref _userId, value); }
        }
    }
}