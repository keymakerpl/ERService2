using ERService.Contracts.Navigation;
using Prism.Events;

namespace ERService.Contracts.Events
{
    public class RegisterSideMenuItemEvent : PubSubEvent<MenuItem>
    {
    }
}
