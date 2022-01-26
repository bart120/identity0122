using IdentityServer.AspIdentity;
using IdentityServer.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly SignInManager<User> _signManager;

        public AuthenticationController(SignInManager<User> signManager, IIdentityServerInteractionService interaction)
        {
            _signManager = signManager;
            _interaction = interaction;
        }


        [Route("login")]
        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginViewModel { ReturnUrl = ReturnUrl };
            return View(model);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    return Redirect(model.ReturnUrl);
                }
                ModelState.AddModelError("Email", "Login/mot de passe invalide.");
            }
            return View(model);
        }

        [Route("logout")]
        public async Task<IActionResult> Logout(LogoutViewModel model)
        {
            if (User?.Identity.IsAuthenticated == true)
            {
                //externe
                /*var url = Url.Action("Logout", new { logoutId = model.LogoutId });
                return SignOut(new Microsoft.AspNetCore.Authentication.AuthenticationProperties { RedirectUri = url }, "schema");*/

                var contextLogout = await _interaction.GetLogoutContextAsync(model.LogoutId);

                await _signManager.SignOutAsync();

                return Redirect(contextLogout.PostLogoutRedirectUri);
                //return RedirectToAction("index", "home");
            }
            return BadRequest();
        }
    }
}
