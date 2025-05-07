using Clean.Core.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Security;
using Umbraco.Cms.Web.Website.Controllers;

namespace NewsWebsite.Controllers
{
    public class AccountSurfaceController : SurfaceController
    {
        private readonly IMemberManager _memberManager;
        private readonly IMemberService _memberService;
        private readonly IMemberSignInManager _memberSignInManager;
        private readonly ILogger<AccountSurfaceController> _logger;

        public AccountSurfaceController(
            IUmbracoContextAccessor umbracoContextAccessor,
            IUmbracoDatabaseFactory databaseFactory,
            ServiceContext services,
            AppCaches appCaches,
            IProfilingLogger profilingLogger,
            IPublishedUrlProvider publishedUrlProvider,
            IMemberManager memberManager,
            IMemberService memberService,
            IMemberSignInManager memberSignInManager,
            ILogger<AccountSurfaceController> logger)
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _memberManager = memberManager;
            _memberService = memberService;
            _memberSignInManager = memberSignInManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            try
            {
                // Check if user already exists
                var existingMember = await _memberManager.FindByEmailAsync(model.Email);
                if (existingMember != null)
                {
                    ModelState.AddModelError("", "A user with this email already exists");
                    return CurrentUmbracoPage();
                }

                // Create a new member identity user
                var memberUser = MemberIdentityUser.CreateNew(
                    model.Email,
                    model.Email,
                    "Member",
                    isApproved: true,
                    model.Name);

                // Create the member
                var result = await _memberManager.CreateAsync(
                    memberUser,
                    model.Password);

                if (result.Succeeded)
                {
                    // Sign in the user
                    await _memberSignInManager.SignInAsync(memberUser, isPersistent: false);

                    // Redirect to home page or profile page
                    return Redirect("/");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration");
                ModelState.AddModelError("", "An error occurred during registration. Please try again.");
            }

            return CurrentUmbracoPage();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            try
            {
                var result = await _memberSignInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return Redirect("/");
                }

                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Account locked. Please try again later.");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                ModelState.AddModelError("", "An error occurred during login. Please try again.");
            }

            return CurrentUmbracoPage();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _memberSignInManager.SignOutAsync();
            return Redirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            var member = await _memberManager.FindByEmailAsync(model.Email);
            if (member == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ForgotPasswordConfirmation");
            }

            try
            {
                // Generate password reset token
                var token = await _memberManager.GeneratePasswordResetTokenAsync(member);
                
                // Create reset password link
                var resetLink = Url.Action("ResetPassword", "Account", 
                    new { email = model.Email, token = token }, 
                    protocol: Request.Scheme);

                // TODO: Send email with reset link
                // This would typically involve using an email service
                _logger.LogInformation($"Password reset link: {resetLink}");

                return RedirectToAction("ForgotPasswordConfirmation");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during forgot password");
                ModelState.AddModelError("", "An error occurred. Please try again.");
                return CurrentUmbracoPage();
            }
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View("~/Views/Partials/Account/ForgotPasswordConfirmation.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            var member = await _memberManager.FindByEmailAsync(model.Email);
            if (member == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation");
            }

            try
            {
                var result = await _memberManager.ResetPasswordAsync(member, model.Token, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during password reset");
                ModelState.AddModelError("", "An error occurred. Please try again.");
            }

            return CurrentUmbracoPage();
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View("~/Views/Partials/Account/ResetPasswordConfirmation.cshtml");
        }

        // Add these methods to the AccountSurfaceController class 

        public async Task<IActionResult> Profile()
        {
            if (User != null && !User.Identity!.IsAuthenticated)
            {
                return Redirect("/login");
            }

            var member = await _memberManager.GetCurrentMemberAsync();
            if (member == null)
            {
                return Redirect("/login");
            }

            var model = new ProfileViewModel
            {
                Name = member.Name!,
                Email = member.Email!,
                Bio = "this is my bio"

            };

            return View("~/Views/Partials/Account/Profile.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ProfileViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/login");
            }

            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            try
            {
                var member = await _memberManager.GetCurrentMemberAsync();
                if (member == null)
                {
                    return Redirect("/login");
                }

                // Update member properties
                member.Name = model.Name;
                
                // Update custom properties if they exist
                var memberEntity = _memberService.GetByEmail(member.Email);
                if (memberEntity != null)
                {
                    memberEntity.SetValue("bio", model.Bio);
                    _memberService.Save(memberEntity);
                }

                var result = await _memberManager.UpdateAsync(member);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Profile updated successfully";
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating profile");
                ModelState.AddModelError("", "An error occurred while updating your profile");
            }

            return CurrentUmbracoPage();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ProfileViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/login");
            }

            if (string.IsNullOrEmpty(model.CurrentPassword) || 
                string.IsNullOrEmpty(model.NewPassword) || 
                string.IsNullOrEmpty(model.ConfirmNewPassword))
            {
                ModelState.AddModelError("", "All password fields are required");
                return CurrentUmbracoPage();
            }

            if (model.NewPassword != model.ConfirmNewPassword)
            {
                ModelState.AddModelError("", "New password and confirmation do not match");
                return CurrentUmbracoPage();
            }

            try
            {
                var member = await _memberManager.GetCurrentMemberAsync();
                if (member == null)
                {
                    return Redirect("/login");
                }

                var result = await _memberManager.ChangePasswordAsync(member, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Password changed successfully";
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password");
                ModelState.AddModelError("", "An error occurred while changing your password");
            }

            return CurrentUmbracoPage();
        }
    }
}