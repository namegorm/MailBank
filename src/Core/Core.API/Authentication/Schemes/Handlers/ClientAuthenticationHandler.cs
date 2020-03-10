using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using Core.API.Authentication.Schemes.Options;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Core.API.Authentication.Schemes.Handlers
{
    public class ClientAuthenticationHandler : AuthenticationHandler<ClientAuthenticationOptions>
    {
        public ClientAuthenticationHandler(IOptionsMonitor<ClientAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.NoResult();
            }

            if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out AuthenticationHeaderValue headerValue))
            {
                return AuthenticateResult.NoResult();
            }

            if (!"Client".Equals(headerValue.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.NoResult();
            }

            if (!long.TryParse(headerValue.Parameter, out long clientValue))
            {
                return AuthenticateResult.Fail("Invalid Client authentication header.");
            }

            if (clientValue < 1 || clientValue % 2 == 0)
            {
                return AuthenticateResult.Fail("Invalid Client authentication header.");
            }

            var claims = new[] { new Claim("ClientValue", clientValue.ToString()) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
