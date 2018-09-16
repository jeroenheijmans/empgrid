using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EmpGrid.Api.Models.Auth
{
    public class EmpGridUser : IdentityUser<Guid>
    {
    }
}
