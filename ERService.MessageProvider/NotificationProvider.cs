using ERService.Contracts.Messages;
using Notifications.Wpf.Core;
using System.Threading.Tasks;

namespace ERService.MessageProvider
{
    public class NotificationProvider : INotificationProvider
    {
        private readonly INotificationManager notificationManager;

        public NotificationProvider()
        {
            this.notificationManager = new NotificationManager();
        }

        public async Task ShowInformation(string title, string message) =>
            await ShowNotification(title, message, NotificationType.Information);

        public async Task ShowError(string title, string message) =>
            await ShowNotification(title, message, NotificationType.Error);

        public async Task ShowWarning(string title, string message) =>
            await ShowNotification(title, message, NotificationType.Warning);

        public async Task ShowSuccess(string title, string message) =>
            await ShowNotification(title, message, NotificationType.Success);

        private async Task ShowNotification(string title, string message, NotificationType notificationType) =>
            await notificationManager.ShowAsync(new NotificationContent
            {
                Title = title,
                Message = message,
                Type = notificationType
            }, areaName: "WindowArea");
    }
}
