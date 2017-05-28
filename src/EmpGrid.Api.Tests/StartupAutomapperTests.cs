using AutoMapper;
using EmpGrid.Api.Models.Core;
using EmpGrid.Domain.Core;
using FluentAssertions;
using Xunit;

namespace EmpGrid.Api
{
    public class StartupAutomapperTests
    {
        public StartupAutomapperTests()
        {
            StartupAutomapper.InitializeMappers();
        }

        [Fact]
        public void AutoMapper_configuration_is_valid()
        {
            Mapper.Configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Can_map_default_Emp_to_Model()
        {
            var entity = new Emp();
            var model = Mapper.Map<EmpModel>(entity);
            model.Should().NotBeNull();
        }

        [Fact]
        public void Can_map_Emp_with_presence_to_Model()
        {
            var entity = new Emp();
            entity.Presences.Add(new Presence());
            var model = Mapper.Map<EmpModel>(entity);
            model.Should().NotBeNull();
            model.Presences.Should().NotBeEmpty();
        }

        [Fact]
        public void Can_map_Emp_from_default_Model()
        {
            var model = new EmpModel();
            var entity = Mapper.Map<Emp>(model);
            entity.Should().NotBeNull();
        }

        [Fact]
        public void Can_map_Emp_from_Model_with_presence()
        {
            var model = new EmpModel { Presences = new[] { new PresenceModel() } };
            var entity = Mapper.Map<Emp>(model);
            entity.Should().NotBeNull();
            entity.Presences.Should().NotBeEmpty();
        }
    }
}
