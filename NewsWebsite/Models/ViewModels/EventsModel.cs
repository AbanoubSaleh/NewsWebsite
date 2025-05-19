using Umbraco.Cms.Core.Models.PublishedContent;

namespace NewsWebsite.Models.ViewModels;

public class EventsModel
{
   public int PageSize { get; set; } = 10;
   public int currentPage { get; set; } = 1;
   public int totalPages { get; set; } = 1;
   public int totalItems { get; set; } = 0;
   public int totalItemsInPage { get; set; } = 0;
    public IEnumerable<EventDetails> Events { get; set; } = Enumerable.Empty<EventDetails>();
    public EventsModel(IEnumerable<EventDetails> events)
    {
        this.Events = events;
        this.PageSize = 10;
        this.currentPage = 1;
        this.totalItems = events.Count();
        this.totalPages = (int)Math.Ceiling((double)totalItems / PageSize);
        this.totalItemsInPage = Math.Min(PageSize, totalItems);

    }
}
