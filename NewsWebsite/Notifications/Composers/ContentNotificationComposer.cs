using NewsWebsite.Notifications.Handlers;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Notifications;

namespace NewsWebsite.Notifications.Composers;

public class ContentNotificationComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.AddNotificationHandler<ContentPublishingNotification, ContentPublishingHandler>();
    }
}
