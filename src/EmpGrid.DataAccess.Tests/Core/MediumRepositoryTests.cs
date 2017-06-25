using EmpGrid.Domain;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EmpGrid.DataAccess.Core
{
    public class MediumRepositoryTests
    {
        public static readonly string[] TestMediumIds = new[]
        {
            "twitter",
            "facebook",
            "linkedin",
            "google-plus",
            "meetup",
            "goodreads",
            "github",
            "gitlab",
            "bitbucket",
            "personal-website",
            "blog",
            "stackoverflow",
        };

        public static IList<object[]> TestMediumIdsAsMemberData =>
            TestMediumIds.Select(id => new object[] { id }).ToList();

        [Theory]
        [MemberData(nameof(TestMediumIdsAsMemberData))]
        public void Can_find_all_known_mediums_by_id_as_string(string id)
        {
            var sut = new MediumRepository();
            var result = sut.FindById(id);
            result.Id.Should().Be(id);
            result.Name.Should().NotBeNullOrWhiteSpace();
        }
        [Theory]
        [MemberData(nameof(TestMediumIdsAsMemberData))]
        public void Can_find_all_known_mediums_by_id_as_identity(string id)
        {
            var sut = new MediumRepository();
            var result = sut.FindById(new StringEntityIdentity { Id = id });
            result.Id.Should().Be(id);
            result.Name.Should().NotBeNullOrWhiteSpace();
        }

        [Theory]
        [MemberData(nameof(TestMediumIdsAsMemberData))]
        public void Can_get_all_known_mediums_by_id_as_string(string id)
        {
            var sut = new MediumRepository();
            var result = sut.GetById(id);
            result.Id.Should().Be(id);
            result.Name.Should().NotBeNullOrWhiteSpace();
        }

        [Theory]
        [MemberData(nameof(TestMediumIdsAsMemberData))]
        public void Can_get_all_known_mediums_by_id_as_identity(string id)
        {
            var sut = new MediumRepository();
            var result = sut.GetById(new StringEntityIdentity { Id = id });
            result.Id.Should().Be(id);
            result.Name.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void List_will_enumerate_only_known_mediums()
        {
            var sut = new MediumRepository();
            var result = sut.List();
            result.Select(m => m.Id).Should().BeEquivalentTo(TestMediumIds);
        }

        [Fact]
        public void Get_with_identity_will_throw_if_item_doesnt_exist()
        {
            var sut = new MediumRepository();
            var exception = Assert.Throws<EntityNotFoundException>(() => sut.GetById((StringEntityIdentity)"non-existent-id"));
            exception.Message.Should().Contain("non-existent-id");
        }

        [Fact]
        public void Get_with_string_will_throw_if_item_doesnt_exist()
        {
            var sut = new MediumRepository();
            var exception = Assert.Throws<EntityNotFoundException>(() => sut.GetById("non-existent-id"));
            exception.Message.Should().Contain("non-existent-id");
        }
    }
}
