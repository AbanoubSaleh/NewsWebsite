using Clean.Core.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Core.Services;

namespace NewsWebsite.Controllers.RenderControllers
{
    public class ProfilePageController : RenderController
    {
        private readonly IMemberManager _memberManager;
        private readonly IMemberService _memberService;

        public ProfilePageController(
            ILogger<RenderController> logger,
            ICompositeViewEngine compositeViewEngine,
            IUmbracoContextAccessor umbracoContextAccessor,
            IMemberManager memberManager,
            IMemberService memberService)
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _memberManager = memberManager;
            _memberService = memberService;
        }

        public override IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/login");
            }

            var memberEmail = User.Identity.Name;
            if (string.IsNullOrEmpty(memberEmail))
            {
                return Redirect("/login");
            }

            // Get member data from IMemberService
            var member = _memberService.GetByEmail(memberEmail);
            if (member == null)
            {
                return Redirect("/login");
            }

            var model = new ProfileViewModel
            {
                Name = member.Name,
                Email = member.Email,
                Bio = member.HasProperty("bio") ? member.GetValue<string>("bio") : string.Empty
            };

            ViewData["ProfileModel"] = model;
            return CurrentTemplate(CurrentPage);
        }
    }
}