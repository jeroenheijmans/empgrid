using FluentAssertions;
using Xunit;

namespace EmpGrid.Api.Models.Core
{
    public class MediumModelTests
    {
        [Fact]
        public void ToString_contains_Id()
        {
            var sut = new MediumModel { Id = "qwitter" };
            sut.ToString().Should().Contain("qwitter");
        }

        [Fact]
        public void ToString_contains_Name()
        {
            var sut = new MediumModel { Name = "Q Witter" };
            sut.ToString().Should().Contain("Q Witter");
        }
    }
}
