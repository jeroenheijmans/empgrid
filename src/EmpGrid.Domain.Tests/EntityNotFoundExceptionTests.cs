using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace EmpGrid.Domain
{
    public class EntityNotFoundExceptionTests
    {
        [Fact]
        public void Constructor_with_1_arg_puts_id_in_message()
        {
            var id = new DummyIdentity { Id = "A123" };
            var exception = new EntityNotFoundException(id);
            exception.Message.Should().Contain(id.HumanReadableId);
        }

        [Fact]
        public void Constructor_with_2_arg_puts_id_in_message()
        {
            var id = new DummyIdentity { Id = "A123" };
            var exception = new EntityNotFoundException(id, "addendum");
            exception.Message.Should().Contain(id.HumanReadableId);
            exception.Message.Should().Contain("addendum");
        }

        [Fact]
        public void Constructor_with_3_arg_puts_id_in_message()
        {
            var id = new DummyIdentity { Id = "A123" };
            var innerException = new KeyNotFoundException();
            var exception = new EntityNotFoundException(id, "addendum", innerException);
            exception.Message.Should().Contain(id.HumanReadableId);
            exception.Message.Should().Contain("addendum");
            exception.InnerException.Should().Be(innerException);
        }

        private class DummyIdentity : IEntityIdentity<string>
        {
            public string Id { get; set; }
            public string HumanReadableId => $"Dummy '{Id}'";
        }
    }
}
