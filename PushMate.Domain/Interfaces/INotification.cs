using System.Collections.Generic;

namespace PushMate.Domain.Interfaces
{
    public interface INotification
    {
        string Badge { get; set; }
        string Body { get; set; }
        List<string> BodyLocalizationArguments { get; set; }
        string BodyLocalizationKey { get; set; }
        string ClickAction { get; set; }
        string Color { get; set; }
        string Icon { get; set; }
        string Sound { get; set; }
        string Tag { get; set; }
        string Title { get; set; }
        List<string> TitleLocalizationArguments { get; set; }
        string TitleLocalizationKey { get; set; }
    }
}