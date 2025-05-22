using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using NewsWebsite.Models.ViewModels;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace NewsWebsite.Controllers.RenderControllers;

public class EventReviewController : RenderController
{
    public readonly IPublishedValueFallback _publishedValueFallback;
    public EventReviewController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor, IPublishedValueFallback publishedValueFallback) : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
        _publishedValueFallback = publishedValueFallback;
    }
    public override IActionResult Index()
    {

        var eventPage = CurrentPage;
        var model = new EventReviewViewModel(eventPage, _publishedValueFallback)
        {
            EventId = CurrentPage.Id,
            EventName = "eventName"
        };
        return View("EventReview", model);
    }
}