using Prism.Events;

namespace ERService.Infrastructure.Events
{
    public class AfterUserLogedoutEvent : PubSubEvent<UserAuthorizationEventArgs>
    {
    }
}
