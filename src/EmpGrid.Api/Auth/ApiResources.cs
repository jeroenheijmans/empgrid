using System.Collections.Generic;
using IdentityServer4.Models;

namespace EmpGrid.Api.Auth
{
    public class ApiResources
    {
        public const string ApiResourceName = "empgridv1";

        public static IEnumerable<ApiResource> Get()
        {
            return new[]
            {
                new ApiResource(ApiResourceName, "EmpGrid V1"),
            };
        }
    }
}
