using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReadingRoomStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ReadingRoomStore.Handler
{
    public class StoreDonorsAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ReadingRoomDBContext _context;

        public StoreDonorsAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options ,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock systemClock,
            ReadingRoomDBContext context) :
                base (options, logger, encoder, systemClock)
        {
            _context = context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            if(!Request.Headers.ContainsKey("Autherization"))
                return AuthenticateResult.Fail("Not Found");

            try
            {
                var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Autherization"]);
                var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
                String[] credentials = Encoding.UTF8.GetString(bytes).Split(":");
                String id = credentials[0];


                Donator donator = _context.Donators.Where(donator => donator.DonatorUserEmail == id).FirstOrDefault();

                if(donator == null)
                    AuthenticateResult.Fail("invalid");
                else
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, donator.DonatorUserEmail) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principle = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principle, Scheme.Name);
                }
            }
            catch (Exception)
            {

                return AuthenticateResult.Fail("error occured");
            }

            

            return AuthenticateResult.Fail("failed to Authenticate");
        }
    }
}
