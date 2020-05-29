using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace CustomerWidgetMVC.TokenbasedAuthentication
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (UserMasterRepositry _repo = new UserMasterRepositry())
            {
                var user = _repo.ValidateUser(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                    return;
                }
                               var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                             identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
                             // identity.AddClaim(new Claim(ClaimTypes.Name, user.FirstName));
                              identity.AddClaim(new Claim("Email", user.Email));
                identity.AddClaim(new Claim(ClaimTypes.PostalCode, user.Password));
                context.Validated(identity);
            }
        }
    }
}