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

            if(!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Authorization header Not Found");

            ///Donator donator1 = null;
            //var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            //var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
            //var credentials = Encoding.UTF8.GetString(bytes).Split(new[] { ':' }, 2);
            //string id = credentials[0];
            //string password = credentials[1];

            try
            {
                var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
                var bytecredentials = Encoding.UTF8.GetString(bytes);
                string[] credentials = bytecredentials.Split(":");
                string id = credentials[0];
                string password = credentials[1];


                Donator donator = _context.Donators.Where(donator => donator.DonatorId == id && donator.DonatorPassword == password).FirstOrDefault();

                if (donator == null)
                {
                    AuthenticateResult.Fail("invalid user");
                }
                    
                else
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, donator.DonatorId) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principle = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principle, Scheme.Name);

                   return AuthenticateResult.Success(ticket);
                }
            }
            catch (Exception)
            {

                return AuthenticateResult.Fail("error occured");
            }



            return AuthenticateResult.Fail("need to implement");
        }
    }
}
