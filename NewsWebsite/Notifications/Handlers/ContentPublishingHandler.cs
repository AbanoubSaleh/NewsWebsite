using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace NewsWebsite.Notifications.Handlers;

public class ContentPublishingHandler : INotificationHandler<ContentPublishingNotification>
{
    public void Handle(ContentPublishingNotification notification)
    {
        foreach (var content in notification.PublishedEntities)
        {
            var title = content.GetValue<string>("title");

            if (string.IsNullOrWhiteSpace(title) || title.Trim().ToLower().Equals("title2"))
            {
                notification.CancelOperation(
                    new EventMessage("Error", "Invalid Title", EventMessageType.Error));
            }

            // مثال: منع النشر بعد الساعة 10 مساءً
            if (DateTime.Now.Hour >= 15)
            {
                notification.CancelOperation(
                    new EventMessage("Warning", "content can't published after 3 pm", EventMessageType.Warning));
            }
        }
    }
}