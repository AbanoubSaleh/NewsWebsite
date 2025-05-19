using Examine;
using Microsoft.AspNetCore.Mvc;
using NewsWebsite.Models.ViewModels;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Filters;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Cms.Web.Website.Controllers;

namespace NewsWebsite.Controllers;

public class EventListingPageSurfaceController : SurfaceController
{
    private readonly IContentService _contentService;
    private readonly IPublishedValueFallback _publishedValueFallback;
    private readonly IPublishedContentQuery _publishedContentQuery;
    private readonly IExamineManager _examineManager;

    public EventListingPageSurfaceController(IUmbracoContextAccessor umbracoContextAccessor,
                                             IUmbracoDatabaseFactory databaseFactory,
                                             ServiceContext services,
                                             AppCaches appCaches,
                                             IProfilingLogger profilingLogger,
                                             IPublishedUrlProvider publishedUrlProvider,
                                             IContentService contentService,
                                             IPublishedValueFallback publishedValueFallback,
                                             IPublishedContentQuery publishedContentQuery,
                                             IExamineManager  examineManager) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
        _contentService = contentService;
        _publishedValueFallback = publishedValueFallback;
        _publishedContentQuery = publishedContentQuery;
        _examineManager = examineManager;
    }
    [HttpGet]
    public IActionResult SearchEvents(string searchTerm)
    {
        IEnumerable<IPublishedContent> events;

      var  currentEventListingPage = _publishedContentQuery.ContentAtRoot()
    .DescendantsOrSelf<EventListingPage>().FirstOrDefault();
                var currentPage = currentEventListingPage?.Id != null
    ? _publishedContentQuery.Content(currentEventListingPage?.Id!)
    : null;
        // Use Examine for searching
        if (_examineManager.TryGetIndex("ExternalIndex", out var index))
        {
            var searcher = index.Searcher;
            var query = searcher.CreateQuery()
                .NodeTypeAlias("eventDetailPage")
                .And()
                .GroupedOr(new[] { "nodeName", "location", "description" }, searchTerm);

            var searchResults = query.Execute();

            // Convert search results to IPublishedContent
            var eventNodes = searchResults
                .Select(result => _publishedContentQuery.Content(result.Id))
                .Where(content => content != null)
                .OfType<EventDetailPage>()
                .Where(e => e.IsVisible())
                .OrderByDescending(e => e.EventDate);

            events = eventNodes;
        }
        else
        {
            // Fallback if index not found
            events = _publishedContentQuery.ContentAtRoot()
                .DescendantsOrSelf<EventDetailPage>()
                .Where(e => e.IsVisible() &&
                    (e.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                     (e.Location != null && e.Location.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                     (e.Description != null && e.Description.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase))))
                .OrderByDescending(e => e.EventDate);
        }
        EventListingPageViewModel model = new EventListingPageViewModel(currentPage, _publishedValueFallback, events);

        return View("EventListingPage", model);
    }
    [HttpGet]
    public IActionResult GetPageContent(int pageNumber, int pageSize = 10,int? pageId =null)
    {
        // Use _publishedContentQuery to get all event detail pages
        IEnumerable<IPublishedContent> listOfEvents = _publishedContentQuery.ContentAtRoot()
            .DescendantsOrSelf<EventDetailPage>()
            .Where(e => e != null && e.IsVisible())
            .OrderByDescending(e => e.EventDate)
            .AsEnumerable();

        var currentPage = pageId.HasValue
    ? _publishedContentQuery.Content(pageId.Value)
    : null;
        //pagination info
        var totalItems = listOfEvents.Count();
        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        listOfEvents =
            listOfEvents
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
        var viewModel = new EventListingPageViewModel(currentPage, _publishedValueFallback, listOfEvents);
        viewModel.PageSize = pageSize;
        viewModel.totalItems = totalItems;
        viewModel.totalPages = totalPages;
        viewModel.currentPage = pageNumber;
        viewModel.totalItemsInPage = Math.Min(pageSize, listOfEvents.Count());
        return View("EventListingPage", viewModel);
    }
}
