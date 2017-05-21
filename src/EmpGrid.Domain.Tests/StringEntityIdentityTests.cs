using FluentAssertions;
using Xunit;

namespace EmpGrid.Domain
{
    public class StringEntityIdentityTests
    {
        [Fact]
        public void Can_implicitly_convert_from_string()
        {
            var idString = "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee";
            StringEntityIdentity id = idString;
            id.HumanReadableId.Should().Be(idString);
        }
    }
}
