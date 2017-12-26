using System.Linq;
using AutoMapper;
using EmpGrid.Api.Models.Core;
using EmpGrid.Domain;
using EmpGrid.Domain.Core;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EmpGrid.Api.Controllers
{
    public class GridControllerTests
    {
        private Mock<IBulkEntityRepository<Emp>> _empRepoMock = new Mock<IBulkEntityRepository<Emp>>();
        private Mock<ISingularRepository<Medium>> _mediumRepoMock = new Mock<ISingularRepository<Medium>>();
        private Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public GridControllerTests()
        {
            // Perhaps it makes more sense to just use the real mappers. Oh well, this
            // works for now...
            _mapperMock
                .Setup(m => m.Map<MediumModel>(It.IsAny<Medium>()))
                .Returns((Medium m) => new MediumModel { Id = m.Id, Name = m.Name });
        }

        private GridController CreateSut()
        {
            return new GridController(
                _empRepoMock.Object,
                _mediumRepoMock.Object,
                new Mock<ILogger<GridController>>().Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public void Index_queries_emp_repo()
        {
            var sut = CreateSut();

            _empRepoMock
                .Setup(r => r.Query())
                .Returns((new Emp[2]).AsQueryable());

            var result = sut.Index();

            result.Emps.Length.Should().Be(2);
        }

        [Fact]
        public void Index_queries_medium_repo()
        {
            var sut = CreateSut();

            _mediumRepoMock
                .Setup(r => r.List())
                .Returns((new[] { new Medium("x", "X", "x"), new Medium("y", "Y", "y") }).ToList());

            var result = sut.Index();

            result.Mediums.Count.Should().Be(2);
        }

        [Fact]
        public void Index_maps_emps()
        {
            var sut = CreateSut();

            _empRepoMock
                .Setup(r => r.Query())
                .Returns((new Emp[2]).AsQueryable());

            var result = sut.Index();

            _mapperMock.Verify(m => m.Map<EmpModel>(It.IsAny<Emp>()), Times.Exactly(2));
        }

        [Fact]
        public void Index_maps_mediums_to_dictionary()
        {
            var sut = CreateSut();

            _mediumRepoMock
                .Setup(r => r.List())
                .Returns((new[] {
                    new Medium("id1", "Name 1", "fa1"),
                    new Medium("id2", "Name 2", "fa2"),
                }).ToList());

            var result = sut.Index();

            result.Mediums["id1"].Name.Should().Be("Name 1");
            result.Mediums["id2"].Name.Should().Be("Name 2");
        }
    }
}
