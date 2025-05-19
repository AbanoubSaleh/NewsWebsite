using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using NewsWebsite.Models.ViewModels;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace NewsWebsite.Controllers.RenderControllers;

public class EventListingPageController : RenderController
{
    private readonly ILogger<RenderController> _logger;
    private readonly ICompositeViewEngine _compositeViewEngine;
    private readonly IUmbracoContextAccessor _umbracoContextAccessor;
    private readonly IPublishedValueFallback _publishedValueFallback;

    public EventListingPageController(ILogger<RenderController> logger,
                                      ICompositeViewEngine compositeViewEngine,
                                      IUmbracoContextAccessor umbracoContextAccessor,IPublishedValueFallback publishedValueFallback) : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
        _logger = logger;
        _compositeViewEngine = compositeViewEngine;
        _umbracoContextAccessor = umbracoContextAccessor;
        _publishedValueFallback = publishedValueFallback;
    }
    public override IActionResult Index()
    {
        IEnumerable<IPublishedContent> listOfEvents = CurrentPage?.Siblings()?.Where(content => content != null)
                .OfType<EventDetailPage>()
                .Where(e => e.IsVisible())
                .OrderByDescending(e => e.EventDate).AsEnumerable() ?? [];
        var currentPage = CurrentPage as EventListingPage;
        var viewModel = new EventListingPageViewModel(CurrentPage, _publishedValueFallback, listOfEvents);
        viewModel.PageSize = currentPage?.PageSize ?? 10;
        return View("EventListingPage", viewModel);    
    }
}
