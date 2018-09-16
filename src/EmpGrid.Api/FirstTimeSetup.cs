using System;
using System.Linq;
using System.Threading.Tasks;
using EmpGrid.Api.Models.Auth;
using EmpGrid.DataAccess.Core;
using Microsoft.AspNetCore.Identity;

namespace EmpGrid.Api.Auth
{
    public class FirstTimeSetup
    {
        private const string DefaultAdminUserName = "admin";
        private const string DefaultAdminPassword = "changemequickly";

        private readonly UserManager<EmpGridUser> _userManager;

        public FirstTimeSetup(UserManager<EmpGridUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Run()
        {
            EmpRepository.SeedFakeDatabase("ExampleDataSeed.json");

            if (await _userManager.FindByNameAsync(DefaultAdminUserName) == null)
            {
                await SeedAdminUser();
            }
        }

        private async Task SeedAdminUser()
        {
            var user = new EmpGridUser
            {
                Id = Guid.NewGuid(),
                UserName = DefaultAdminUserName,
            };

            var adminCreationResult = await _userManager.CreateAsync(user, DefaultAdminPassword);

            if (adminCreationResult != IdentityResult.Success)
            {
                var message = String.Join(", ", adminCreationResult.Errors.Select(e => $"{e.Code}: {e.Description}"));
                throw new InvalidOperationException("First time setup failed to create initial user. " + message);
            }
        }
    }
}
