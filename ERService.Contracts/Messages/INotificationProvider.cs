using System.Threading.Tasks;

namespace ERService.Contracts.Messages
{
    public interface INotificationProvider
    {
        Task ShowError(string title, string message);
        Task ShowInformation(string title, string message);
        Task ShowSuccess(string title, string message);
        Task ShowWarning(string title, string message);
    }
}