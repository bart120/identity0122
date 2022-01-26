using IdentityModel;
using IdentityServer.AspIdentity;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class ProfilService : IProfileService
    {
        private readonly UserManager<User> _userManager;

        public ProfilService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //if (context.RequestedResources.RawScopeValues.Any(x => x == "role"))
            //{
            /*if (context.Caller == "UserInfoEndpoint")
            {*/
                var tokenProfileClaims = new List<Claim>();
                User user = await _userManager.GetUserAsync(context.Subject);
                if (user == null)
                    throw new ArgumentException("Invalid subject identifier");

                var roles = await _userManager.GetRolesAsync(user);
            //_userManager.GetCl
                foreach (var item in roles)
                {
                    tokenProfileClaims.Add(new Claim(JwtClaimTypes.Role, item));
                }
                tokenProfileClaims.Add(new Claim("product", "write"));
                context.IssuedClaims.AddRange(tokenProfileClaims);
            //}
            //}
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }
}
