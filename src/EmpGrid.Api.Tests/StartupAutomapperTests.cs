using AutoMapper;
using EmpGrid.Api.Models.Core;
using EmpGrid.Domain.Core;
using FluentAssertions;
using Xunit;

namespace EmpGrid.Api
{
    public class StartupAutomapperTests
    {
        [Fact]
        public void AutoMapper_configuration_is_valid()
        {
            StartupAutomapper.InitializeMappers();
            Mapper.Configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Can_map_default_EmpModel()
        {
            StartupAutomapper.InitializeMappers();
            var entity = new Emp();
            var model = Mapper.Map<EmpModel>(entity);
            model.Should().NotBeNull();
        }


        [Fact]
        public void Can_map_EmpModel_with_presence()
        {
            StartupAutomapper.InitializeMappers();
            var entity = new Emp();
            entity.Presences.Add(new Presence());
            var model = Mapper.Map<EmpModel>(entity);
            model.Should().NotBeNull();
        }
    }
}
