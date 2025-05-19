using Umbraco.Cms.Core.Models.PublishedContent;

namespace NewsWebsite.Models.ViewModels;

public class EventListingPageViewModel : PublishedContentWrapped
{
    public readonly IEnumerable<IPublishedContent> events;
    public EventListingPageViewModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback, IEnumerable<IPublishedContent> events) : base(content, publishedValueFallback)
    {
        this.events = events;
    }
    public int PageSize { get; set; } = 10;

}
