using FluentAssertions;
using System;
using Xunit;

namespace EmpGrid.Api.Models.Core
{
    public class EmpModelTests
    {
        [Fact]
        public void ToString_includes_Id()
        {
            var sut = new EmpModel { Id = Guid.NewGuid() };
            sut.ToString().Should().Contain(sut.Id.ToString());
        }

        [Fact]
        public void ToString_includes_Name()
        {
            var sut = new EmpModel { Name = "dummy" };
            sut.ToString().Should().Contain("dummy");
        }

        [Fact]
        public void ToString_includes_nr_of_Presences()
        {
            var sut = new EmpModel { Presences = new[] { new PresenceModel() } };
            sut.ToString().Should().Contain("1");
        }

        [Fact]
        public void ToString_can_handle_null_for_presences()
        {
            var sut = new EmpModel { Presences = new PresenceModel[0] };
            sut.ToString().Should().NotBeEmpty();
        }
    }
}
