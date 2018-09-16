using IdentityServer4.Models;
using System.Collections.Generic;

namespace EmpGrid.Api.Auth
{
    public class IdentityResources
    {
        public static IEnumerable<IdentityResource> Get()
        {
            return new[]
            {
                new IdentityServer4.Models.IdentityResources.OpenId(),
            };
        }
    }
}
