using System.Collections.Generic;
using IdentityServer4.Models;

namespace EmpGrid.Api.Auth
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                // We own both the Client, IDServer, and Resource API, so we'll just
                // use the Resource Owner Password flow, including refresh tokens, as
                // it is simple to implement client side. If we need more secure, modern
                // login mechanisms quite a bit of extra work is to be done, which we
                // leave as a TODO.
                new Client
                {
                    ClientId = "empgridv1-js",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowAccessTokensViaBrowser = true,
                    AllowOfflineAccess = true,
                    AllowedScopes = { "empgridv1" },
                    AccessTokenLifetime = 5 * 60,
                    RequireClientSecret = false, // It would be inside our JS for everyone to see anyways
                }
            };
        }
    }
}
