using NewsWebsite.Notifications.Handlers;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Notifications;
namespace NewsWebsite.Notifications.Composers;
public class NotificationComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        // ربط الحدث بـ handler
        builder.AddNotificationHandler<ContentSavingNotification, ContentSavingHandler>();
    }
}
