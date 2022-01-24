using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
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
            Client client = new Client();
            client.ClientId = "client_console";
            client.ClientName = "Client en console";
            client.Description = "un client client credentials en formation";
            client.Enabled = true;
            client.AllowedGrantTypes = GrantTypes.ClientCredentials;
            client.ClientSecrets = new List<Secret> { new Secret("secret_console".Sha256())};
            
            _conf.Clients.Add(client.ToEntity());
            await _conf.SaveChangesAsync();
            return Ok();
        }

        public async Task<IActionResult> AddApiScope()
        {
            ApiScope apiScope = new ApiScope();
            apiScope.Name = "api_demo_scope";
            apiScope.Enabled = true;

            var scopOpenId = new IdentityResources.OpenId();
            var scopProfile = new IdentityResources.Profile();
            var scopEmail = new IdentityResources.Email();

            _conf.ApiScopes.Add(apiScope.ToEntity());
            
            /*_conf.IdentityResources.Add(scopOpenId.ToEntity());
            _conf.IdentityResources.Add(scopProfile.ToEntity());
            _conf.IdentityResources.Add(scopEmail.ToEntity());*/
            
            await _conf.SaveChangesAsync();
            return Ok();
        }
    }
}
