﻿using EmpGrid.Domain;
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
        };

        public static IList<object[]> TestMediumIdsAsMemberData =>
            TestMediumIds.Select(id => new object[] { id }).ToList();

        [Theory]
        [MemberData(nameof(TestMediumIdsAsMemberData))]
        public void Can_find_all_known_mediums_by_id(string id)
        {
            var sut = new MediumRepository();
            var result = sut.FindById(new StringEntityIdentity { Id = id }); // TODO: Use the implicit operator or some such
            result.Id.Should().Be(id);
            result.Name.Should().NotBeNullOrWhiteSpace();
        }

        [Theory]
        [MemberData(nameof(TestMediumIdsAsMemberData))]
        public void Can_get_all_known_mediums_by_id(string id)
        {
            var sut = new MediumRepository();
            var result = sut.GetById(new StringEntityIdentity { Id = id }); // TODO: Use the implicit operator or some such
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
    }
}