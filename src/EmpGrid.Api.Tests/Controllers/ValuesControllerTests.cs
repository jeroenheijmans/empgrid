using Xunit;
using FluentAssertions;

namespace EmpGrid.Api.Controllers
{
    public class ValuesControllerTests
    {
        [Fact]
        public void Get_returns_hard_coded_string()
        {
            var sut = new ValuesController();
            var result = sut.Get(123);
            result.Should().Be("value");
        }
    }
}
