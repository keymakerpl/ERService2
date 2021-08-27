using Prism.Events;

namespace ERService.Infrastructure.Events
{
    public class AuthorizationResponseEvent : PubSubEvent<AuthorisationResultArgs>
    {

    }

    public class AuthorisationResultArgs
    {
        public AuthorizationResult Result { get; set; }
    }

    public class AuthorizationRequestEvent : PubSubEvent<UserCredentialsEventArgs>
    {
        
    }

    public class UserCredentialsEventArgs
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public enum AuthorizationResult{
        Success,
        Failed
    }
}
