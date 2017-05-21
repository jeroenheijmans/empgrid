using FluentAssertions;
using Xunit;

namespace EmpGrid.Domain.Core
{
    public class EmpTests
    {
        [Fact]
        public void Default_instance_has_empty_list_of_presences()
        {
            var emp = new Emp();
            emp.Presences.Should().BeEmpty();
        }
    }
}
