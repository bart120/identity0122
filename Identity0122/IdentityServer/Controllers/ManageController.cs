using IdentityServer.AspIdentity;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    public class ManageController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ConfigurationDbContext _conf;

        public ManageController(ConfigurationDbContext conf, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _conf = conf;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> AddApi()
        {
            ApiResource api = new ApiResource();
            api.Name = "api_demo";
            api.Description = "une api en formation";
            api.Enabled = true;

            _conf.ApiResources.Add(api.ToEntity());
            await _conf.SaveChangesAsync();
            return Ok();
        }

        public async Task<IActionResult> AddClient()
        {
            /*Client client = new Client();
            client.ClientId = "client_console";
            client.ClientName = "Client en console";
            client.Description = "un client client credentials en formation";
            client.Enabled = true;
            client.AllowedGrantTypes = GrantTypes.ClientCredentials;
            client.ClientSecrets = new List<Secret> { new Secret("secret_console".Sha256())};*/

            Client client = new Client();
            client.ClientId = "client_mvc";
            client.ClientName = "Client en MVC";
            client.Description = "un client hybrid en formation";
            client.Enabled = true;
            client.AllowedGrantTypes = GrantTypes.Code;
            client.ClientSecrets = new List<Secret> { new Secret("secret_mvc".Sha256()) };
            client.AllowedScopes = new List<string> { "api_demo_scope" };

            _conf.Clients.Add(client.ToEntity());
            await _conf.SaveChangesAsync();
            return Ok();
        }

        public async Task<IActionResult> AddApiScope()
        {
            /* ApiScope apiScope = new ApiScope();
             apiScope.Name = "api_demo_scope";
             apiScope.Enabled = true;
             _conf.ApiScopes.Add(apiScope.ToEntity());*/

            /* var scopOpenId = new IdentityResources.OpenId();
             var scopProfile = new IdentityResources.Profile();
             var scopEmail = new IdentityResources.Email();
             _conf.IdentityResources.Add(scopOpenId.ToEntity());
             _conf.IdentityResources.Add(scopProfile.ToEntity());
             _conf.IdentityResources.Add(scopEmail.ToEntity());*/


           

            await _conf.SaveChangesAsync();
            return Ok();
        }

        public async Task<IActionResult> AddUser()
        {
            /*var utilisateur = new IdentityRole { Name = "UTILISATEUR", NormalizedName = "UTILISATEUR" };
            var admin = new IdentityRole { Name = "ADMIN", NormalizedName = "ADMIN" };
            var result = await _roleManager.CreateAsync(utilisateur);
            await _roleManager.CreateAsync(admin);*/

            var user1 = new User { Email = "vleclerc@inow.fr", UserName = "vleclerc@inow.fr", Lastname = "Leclerc", Firstname = "Vincent" };
            var user2 = new User { Email = "aroyer@tuifrance.com", UserName = "aroyer@tuifrance.com", Lastname = "Royer", Firstname = "Albin" };

            var res = await _userManager.CreateAsync(user1, "Toto007$");
            if (res.Succeeded)
            {
                await _userManager.AddToRolesAsync(user1, new List<string> { "UTILISATEUR" });
            }

            res = await _userManager.CreateAsync(user2, "Toto007$");
            if (res.Succeeded)
            {
                await _userManager.AddToRolesAsync(user2, new List<string> { "UTILISATEUR", "ADMIN" });
            }

            var user = await _userManager.FindByNameAsync("vleclerc@inow.fr");
            //await _userManager.ChangePasswordAsync(user, null, "NewPassword25*")


            return Ok();
        }
    }
}
