using Clean.Core.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace NewsWebsite.Controllers.RenderControllers;

public class LoginPageController : RenderController
{

    public LoginPageController(
        ILogger<RenderController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor
        )
        : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
    }

    // Remove any Route attributes and use Umbraco's routing system
    public override IActionResult Index()
    {
        // This will handle the default action for the controller
        return CurrentTemplate(CurrentPage);
    }
}