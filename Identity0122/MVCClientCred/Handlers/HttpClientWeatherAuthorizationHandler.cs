using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using MVCClientCred.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MVCClientCred.Handlers
{
    public class HttpClientWeatherAuthorizationHandler : DelegatingHandler
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApiTokenCacheClientService _apiTokenCache;

        public HttpClientWeatherAuthorizationHandler(/*IHttpContextAccessor httpContextAccessor*/ApiTokenCacheClientService apiTokenCache)
        {
            //_httpContextAccessor = httpContextAccessor;
            _apiTokenCache = apiTokenCache;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            //var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            var token =await  _apiTokenCache.GetApiTokenAsync("client_cred", "api_demo_scope", "cred");
            request.Headers.Add("Authorization", $"Bearer {token}");
            //request.Headers.Add("Authorization", new List<string>() { authorizationHeader });

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
