using Prism.Events;

namespace ERService.Infrastructure.Events
{
    public class DatabaseStatusEvent : PubSubEvent<DatabaseStatusEventArgs>
    {
    }

    public class DatabaseStatusEventArgs 
    {
        public DatabaseState State { get; set; }
    }
    
    public enum DatabaseState
    {
        Ready,
        Fault
    }
}
