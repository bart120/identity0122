using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MVCClient.Handlers
{
    public class HttpClientWeatherAuthorizationHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpClientWeatherAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            //request.Headers.Add("Authorization", new List<string>() { authorizationHeader });

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
