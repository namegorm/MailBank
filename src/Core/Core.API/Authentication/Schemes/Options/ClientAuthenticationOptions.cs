using Microsoft.AspNetCore.Authentication;

namespace Core.API.Authentication.Schemes.Options
{
    public class ClientAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string Data { get; set; }
    }
}
