using Prism.Events;

namespace ERService.Infrastructure.Events
{
    public class AfterUserLogedinEvent : PubSubEvent<UserAuthorizationEventArgs>
    {
    }    
}
