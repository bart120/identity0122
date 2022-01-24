using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    public class ManageController : Controller
    {
        private readonly ConfigurationDbContext _conf;

        public ManageController(ConfigurationDbContext conf)
        {
            _conf = conf;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
