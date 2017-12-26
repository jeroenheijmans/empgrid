using System;
using FluentAssertions;
using Xunit;

namespace EmpGrid.Domain
{
    public class GuidEntityIdentityTests
    {
        [Fact]
        public void Can_implicitly_convert_from_string()
        {
            var guidString = "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee";
            GuidEntityIdentity id = guidString;
            id.HumanReadableId.Should().Be(guidString);
        }

        [Fact]
        public void Can_implicitly_convert_from_guid()
        {
            var guid = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee");
            GuidEntityIdentity id = guid;
            id.HumanReadableId.Should().Be(guid.ToString());
        }
    }
}
