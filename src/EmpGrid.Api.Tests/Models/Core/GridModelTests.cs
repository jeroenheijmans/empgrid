using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace EmpGrid.Api.Models.Core
{
    public class GridModelTests
    {
        [Fact]
        public void ToString_includes_nr_of_emps()
        {
            var sut = new GridModel { Emps = new EmpModel[2] };
            sut.ToString().Should().Contain("2");
        }

        [Fact]
        public void ToString_includes_nr_of_mediums()
        {
            var sut = new GridModel {
                Mediums = new Dictionary<string, MediumModel>
                {
                    { "a", new MediumModel() },
                    { "b", new MediumModel() },
                    { "c", new MediumModel() },
                    { "d", new MediumModel() },
                }
            };
            sut.ToString().Should().Contain("4");
        }
    }
}
