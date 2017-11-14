using FluentAssertions;
using Xunit;

namespace EmpGrid.Api.Controllers
{
    public class PingControllerTests
    {
        [Fact]
        public void Ping_returns_pong()
        {
            var sut = new PingController();
            var result = sut.Get();
            result.Text.Should().Be("Pong!");
        }
    }
}
