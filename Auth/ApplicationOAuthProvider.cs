using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace OWIN_JWT_Auth_API.Auth
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Logic to validate client
            context.Validated();
            return Task.CompletedTask;
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // Database >> Username and password

            using (var db = new ATCGSA_WithoutASPNETAuthEntities())
            {
                var username = context.UserName;
                var password = context.Password;

                var getUser = db.tblUsers.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();

                if (getUser != null)
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim("Age", "16"));
                    identity.AddClaim(new Claim(ClaimTypes.Name, getUser.UserName));
                    // Get >> UserRoles >> Get Roles and add into claim
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                    identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
                    identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
                    context.Validated(identity);

                    // Role base
                    // Calling method after generating token
                }
                else
                {
                    context.SetError("Invalid", "Invaid Username and password");
                    context.Rejected();
                }

            }


        }
    }
}