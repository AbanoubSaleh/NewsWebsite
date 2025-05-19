using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsWebsite.Models.ViewModels;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;
using System.Linq;
using System;

namespace NewsWebsite.Controllers.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IPublishedValueFallback _publishedValueFallback;
        private readonly IPublishedContentQuery _publishedContentQuery;
        public EventsController(IPublishedContentQuery publishedContentQuery,
                                IPublishedValueFallback publishedValueFallback)
        {
            _publishedContentQuery = publishedContentQuery;
            _publishedValueFallback = publishedValueFallback;
        }
        
        [HttpGet]
        public IActionResult GetAllEvents(
            int pageSize = 10, 
            int currentPage = 1, 
            DateTime? dateFrom = null, 
            DateTime? dateTo = null)
        {
            // Get all event detail pages
            var query = _publishedContentQuery.ContentAtRoot()
                .DescendantsOrSelf<EventDetailPage>()
                .Where(content => content != null)
                .Where(e => e.IsVisible());
                
            // Apply date filtering if provided
            if (dateFrom.HasValue)
            {
                query = query.Where(e => e.EventDate >= dateFrom.Value);
            }
            
            if (dateTo.HasValue)
            {
                query = query.Where(e => e.EventDate <= dateTo.Value);
            }
            
            // Order by event date
            var orderedEvents = query.OrderByDescending(e => e.EventDate);
            
            // Convert to EventDetails objects
            var eventDetailsList = orderedEvents
                .Select(e => EventDetails.FromContent(e))
                .ToList();
                
            // Apply pagination
            var totalItems = eventDetailsList.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            
            // Ensure current page is valid
            currentPage = Math.Max(1, Math.Min(currentPage, totalPages));
            
            // Get items for current page
            var pagedEvents = eventDetailsList
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            
            // Create and populate the view model
            var viewModel = new EventsModel(pagedEvents)
            {
                PageSize = pageSize,
                currentPage = currentPage,
                totalPages = totalPages,
                totalItems = totalItems,
                totalItemsInPage = pagedEvents.Count
            };

            return Ok(viewModel);
        }
    }
}
