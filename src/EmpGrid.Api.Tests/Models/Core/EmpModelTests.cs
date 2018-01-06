using System;
using FluentAssertions;
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

        [Fact]
        public void GravatarUrl_for_email_works()
        {
            // Uses Gravatar's example from https://en.gravatar.com/site/implement/hash/
            var sut = new EmpModel { EmailAddress = "MyEmailAddress@example.com " };
            sut.GravatarUrl.Should().Be("https://www.gravatar.com/avatar/0bc83cb571cd1c50ba6f3e8a78ef1346");
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void GravatarUrl_for_empty_mail_returns_empty_string(string emailAddress)
        {
            var sut = new EmpModel { EmailAddress = emailAddress };
            sut.GravatarUrl.Should().Be("");
        }
    }
}
