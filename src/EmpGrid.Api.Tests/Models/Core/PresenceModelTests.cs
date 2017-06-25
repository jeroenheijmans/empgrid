using FluentAssertions;
using Xunit;

namespace EmpGrid.Api.Models.Core
{
    public class PresenceModelTests
    {
        [Fact]
        public void ToString_includes_MediumId()
        {
            var sut = new PresenceModel { MediumId = "qwitter" };
            sut.ToString().Should().Contain("qwitter");
        }

        [Fact]
        public void ToString_includes_Url()
        {
            var sut = new PresenceModel { Url = "http://fake-url" };
            sut.ToString().Should().Contain("http://fake-url");
        }
    }
}
