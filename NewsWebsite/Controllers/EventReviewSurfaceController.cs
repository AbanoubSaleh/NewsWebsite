﻿using Microsoft.AspNetCore.Mvc;
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

public class EventReviewSurfaceController : SurfaceController
{
    private readonly IPublishedValueFallback _publishedValueFallback;
    private readonly IPublishedContentQuery _contentQuery;
    private readonly IContentService _contentService;


    public EventReviewSurfaceController(IUmbracoContextAccessor umbracoContextAccessor,
                                        IUmbracoDatabaseFactory databaseFactory,
                                        ServiceContext services,
                                        AppCaches appCaches,
                                        IProfilingLogger profilingLogger,
                                        IPublishedUrlProvider publishedUrlProvider,
                                        IPublishedValueFallback publishedValueFallback,
                                        IPublishedContentQuery contentQuery,
                                        IContentService contentService) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
        _publishedValueFallback = publishedValueFallback;
        _contentQuery = contentQuery;
        _contentService = contentService;
    }

    [HttpPost]
    [ValidateUmbracoFormRouteString]
    [ValidateAntiForgeryToken]
    public IActionResult SubmitReview(AddReviewViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return CurrentUmbracoPage();
        }

        string reviewName = $"Review for event: {model.EventName}";

        // how i can link with the parent event ??? her we doesn't use page.key like the demo in the session 
        IContent content = _contentService.Create(reviewName, model.EventId, "eventReview");

        content.SetValue("comment", model.Comment);
        content.SetValue("reviewerName", model.ReviewerName);
        content.SetValue("rateStars", model.RateStars);

        PublishResult publishResult = _contentService.SaveAndPublish(content);
        if (publishResult.Success == false)
        {
            ModelState.AddModelError(nameof(EventReview.ReviewerName), "Failed to submit review. Please try again.");
            return CurrentUmbracoPage();
        }

        TempData["SuccessMessage"] = "Your review has been submitted successfully!";
        return RedirectToCurrentUmbracoPage();
    }
}
