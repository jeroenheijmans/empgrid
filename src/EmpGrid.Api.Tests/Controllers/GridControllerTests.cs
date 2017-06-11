using AutoMapper;
using EmpGrid.Api.Models.Core;
using EmpGrid.Domain;
using EmpGrid.Domain.Core;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using Xunit;

namespace EmpGrid.Api.Controllers
{
    public class GridControllerTests
    {
        private Mock<IBulkEntityRepository<Emp>> empRepoMock = new Mock<IBulkEntityRepository<Emp>>();
        private Mock<ISingularRepository<Medium>> mediumRepoMock = new Mock<ISingularRepository<Medium>>();
        private Mock<IMapper> mapperMock = new Mock<IMapper>();

        public GridControllerTests()
        {
            // Perhaps it makes more sense to just use the real mappers. Oh well, this
            // works for now...
            mapperMock
                .Setup(m => m.Map<MediumModel>(It.IsAny<Medium>()))
                .Returns((Medium m) => new MediumModel { Id = m.Id, Name = m.Name });
        }

        private GridController CreateSut()
        {
            return new GridController(
                empRepoMock.Object,
                mediumRepoMock.Object,
                new Mock<ILogger<EmpController>>().Object,
                mapperMock.Object
            );
        }

        [Fact]
        public void Index_queries_emp_repo()
        {
            var sut = CreateSut();

            empRepoMock
                .Setup(r => r.Query())
                .Returns((new Emp[2]).AsQueryable());

            var result = sut.Index();

            result.Emps.Length.Should().Be(2);
        }

        [Fact]
        public void Index_queries_medium_repo()
        {
            var sut = CreateSut();

            mediumRepoMock
                .Setup(r => r.List())
                .Returns((new[] { new Medium("x", "X"), new Medium("y", "Y") }).ToList());

            var result = sut.Index();

            result.Mediums.Count.Should().Be(2);
        }

        [Fact]
        public void Index_maps_emps()
        {
            var sut = CreateSut();

            empRepoMock
                .Setup(r => r.Query())
                .Returns((new Emp[2]).AsQueryable());

            var result = sut.Index();

            mapperMock.Verify(m => m.Map<EmpModel>(It.IsAny<Emp>()), Times.Exactly(2));
        }

        [Fact]
        public void Index_maps_mediums_to_dictionary()
        {
            var sut = CreateSut();

            mediumRepoMock
                .Setup(r => r.List())
                .Returns((new[] {
                    new Medium("id1", "Name 1"),
                    new Medium("id2", "Name 2"),
                }).ToList());

            var result = sut.Index();

            result.Mediums["id1"].Name.Should().Be("Name 1");
            result.Mediums["id2"].Name.Should().Be("Name 2");
        }
    }
}
