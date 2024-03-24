using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using StockControlAPI.Models;
using StockControlAPI.Models.Entities;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace StockControlAPI.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOption>
    {
        private readonly StockApiDBContext context;

        public BasicAuthenticationHandler(IOptionsMonitor<BasicAuthenticationOption> options,
                                          ILoggerFactory logger,
                                          UrlEncoder urlEncoder,
                                          ISystemClock systemClock,
                                          StockApiDBContext context) : base(options, logger, urlEncoder, systemClock)
        {
            this.context = context;

        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.Path != "/api/User/CreateUser")
            {

                if (!Request.Headers.ContainsKey("Authorization"))
                {
                    return Task.FromResult(AuthenticateResult.NoResult());
                }

                if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out AuthenticationHeaderValue headerValue))
                {
                    return Task.FromResult(AuthenticateResult.NoResult());
                }

                if (!headerValue.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
                {
                    return Task.FromResult(AuthenticateResult.NoResult());
                }

                byte[] headerValueBytes = Convert.FromBase64String(headerValue.Parameter);
                string usernamePassword = Encoding.UTF8.GetString(headerValueBytes);

                string[] parts = usernamePassword.Split(':');
                string username = parts[0];
                string password = parts[1];
                User user = context.Users.FirstOrDefault(x => x.UserName == username && x.Password == x.Password);

                Claim[] claims = new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, Scheme.Name);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
            else
            {
                Claim[] claims = new[]
                    {
                    new Claim(ClaimTypes.Name, ""),
                    new Claim(ClaimTypes.NameIdentifier, "1")
                };
            
                ClaimsIdentity identity = new ClaimsIdentity(claims, Scheme.Name);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
        }
    }
}
