using System.ComponentModel;

namespace ERService.Contracts.Mvvm
{
    public interface IModelWrapper<T> : INotifyDataErrorInfo, INotifyPropertyChanged, IModelWrapper
    {
        T Model { get; }
    }

    public interface IModelWrapper { }
}
