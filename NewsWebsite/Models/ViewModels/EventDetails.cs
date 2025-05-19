using Umbraco.Cms.Web.Common.PublishedModels;

namespace NewsWebsite.Models.ViewModels;

public class EventDetails
{
    public string? Title { get; set; }
    public string? Location { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime EventDate { get; set; }
    public string? Description { get; set; } 
    public string? ImageUrl { get; set; }

    public static EventDetails FromContent(EventDetailPage? content)
    {
        return new EventDetails
        {
            Title = content.Title,
            Location = content.Location,
            CreatedDate = content.CreatedDate,
            EventDate = content.EventDate,
            Description = content.Description?.ToHtmlString(), 
            ImageUrl = content.Image?.Url() 
        };
    }
}
