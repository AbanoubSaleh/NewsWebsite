using Clean.Core.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace NewsWebsite.Controllers
{
    public class AccountController : RenderController
    {
        private readonly IMemberManager _memberManager;

        public AccountController(
            ILogger<RenderController> logger,
            ICompositeViewEngine compositeViewEngine,
            IUmbracoContextAccessor umbracoContextAccessor,
            IMemberManager memberManager)
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _memberManager = memberManager;
        }

        public IActionResult Register()
        {
            return CurrentTemplate(CurrentPage);
        }

        public IActionResult Login()
        {
            return CurrentTemplate(CurrentPage);
        }

        public IActionResult ForgotPassword()
        {
            return CurrentTemplate(CurrentPage);
        }

        public IActionResult ResetPassword(string email, string token)
        {
            var model = new ResetPasswordViewModel
            {
                Email = email,
                Token = token
            };
            return View("ResetPassword", model);
        }

        public IActionResult Logout()
        {
            return CurrentTemplate(CurrentPage);
        }
    }
}