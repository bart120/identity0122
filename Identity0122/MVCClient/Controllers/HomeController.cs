using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCClient.Models;
using MVCClient.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherService _serv;

        public HomeController(ILogger<HomeController> logger, IWeatherService serv)
        {
            _logger = logger;
            _serv = serv;
        }

        public async  Task<IActionResult> Index()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");
            var u = User;

            var weathers = await _serv.GetWeathersAsync();

            return View(weathers);
        }


        [Authorize(Roles = "ADMIN")]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            //await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            //await HttpContext.SignOutAsync();
            //return RedirectToAction;
            return new SignOutResult(new[] { CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
