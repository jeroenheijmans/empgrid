using System;
using System.Threading;
using System.Threading.Tasks;
using EmpGrid.Api.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace EmpGrid.Api.Auth
{
    // TODO: Implement this, or make it explicitly noop.
    public class EmpGridRoleStore : IRoleStore<EmpGridRole>
    {
        public Task<IdentityResult> CreateAsync(EmpGridRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(EmpGridRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<EmpGridRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<EmpGridRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedRoleNameAsync(EmpGridRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(EmpGridRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(EmpGridRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(EmpGridRole role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(EmpGridRole role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(EmpGridRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}
