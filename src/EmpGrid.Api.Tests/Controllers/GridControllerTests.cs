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
                .Returns((new Medium[2]).ToList());

            var result = sut.Index();

            result.Mediums.Length.Should().Be(2);
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
        public void Index_maps_mediums()
        {
            var sut = CreateSut();

            mediumRepoMock
                .Setup(r => r.List())
                .Returns((new Medium[2]).ToList());

            var result = sut.Index();

            mapperMock.Verify(m => m.Map<MediumModel>(It.IsAny<Medium>()), Times.Exactly(2));
        }
    }
}
