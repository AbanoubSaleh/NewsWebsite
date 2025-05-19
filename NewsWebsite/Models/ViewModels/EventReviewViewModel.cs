using System;
using System.ComponentModel.DataAnnotations;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace NewsWebsite.Models.ViewModels
{
    public class EventReviewViewModel : PublishedContentWrapped
    {
        public EventReviewViewModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }

        public string ReviewerName { get; set; }
        
        public string Comment { get; set; }
        
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5 stars")]
        public int RateStars { get; set; }
        
        // Event information
        [Required]
        public int EventId { get; set; }
        
        public string EventName { get; set; }
    }
}
