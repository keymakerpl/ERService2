using ERService.Contracts.Messages;
using Notifications.Wpf.Core;
using Prism.Ioc;
using Prism.Modularity;

namespace ERService.MessageProvider
{
    public class MessageProviderModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry) =>
            containerRegistry.Register<INotificationManager, NotificationManager>()
                             .Register<INotificationProvider, NotificationProvider>();
    }
}
