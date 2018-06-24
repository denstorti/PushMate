using System.Collections.Generic;

namespace PushMate.Domain.Interfaces
{
    public interface IMessage
    {
        string CollapseKey { get; set; }
        string Condition { get; set; }
        bool? ContentAvailable { get; set; }
        object Data { get; set; }
        bool? DryRun { get; set; }
        INotification Notification { get; set; }
        //Priority Priority { get; set; }
        List<string> RegistrationIds { get; set; }
        string RestrictedPackageName { get; set; }
        int? TimeToLive { get; set; }
        string To { get; set; }
    }
}