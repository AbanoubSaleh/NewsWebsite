using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace NewsWebsite.Controllers;

public class SportsNewsController : RenderController
{
    public SportsNewsController(
        ILogger<RenderController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
    }

    public IActionResult GetScoresPartial()
    {
        return PartialView("~/Views/Partials/_ScoresPartial.cshtml");
    }

    public IActionResult GetTransfersPartial()
    {
        return PartialView("~/Views/Partials/_TransfersPartial.cshtml");
    }

    public IActionResult GetFootballNewsPartial()
    {
        return PartialView("~/Views/Partials/_FootballNewsPartial.cshtml");
    }
}