using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
namespace NewsWebsite.Notifications.Handlers;
public class ContentSavingHandler : INotificationHandler<ContentSavingNotification>
{
    public void Handle(ContentSavingNotification notification)
    {
        foreach (var content in notification.SavedEntities)
        {
            var title = content.GetValue<string>("title");

            if (string.IsNullOrWhiteSpace(title) || title.Trim().ToLower().Equals("title"))
            {
                notification.CancelOperation(
                    new EventMessage("Error", "Invalid Title", EventMessageType.Error)
                );
            }

            content.SetValue("title", title?.Trim());
        }
    }
}

