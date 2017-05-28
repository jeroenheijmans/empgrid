using AutoMapper;
using EmpGrid.Api.Models.Core;
using EmpGrid.Domain;
using EmpGrid.Domain.Core;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace EmpGrid.Api.Controllers
{
    public class EmpControllerTests
    {
        private readonly Mock<ILogger<EmpController>> loggerMock = new Mock<ILogger<EmpController>>();
        private readonly Mock<IBulkEntityRepository<Emp>> empRepoMock = new Mock<IBulkEntityRepository<Emp>>();
        private readonly Mock<IMapper> mapperMock = new Mock<IMapper>();

        private EmpController CreateSut()
        {
            return new EmpController(
                empRepoMock.Object,
                loggerMock.Object,
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

            result.Length.Should().Be(2);
        }

        [Fact]
        public void Index_maps_to_models()
        {
            var sut = CreateSut();

            empRepoMock
                .Setup(r => r.Query())
                .Returns((new Emp[2]).AsQueryable());

            var result = sut.Index();

            mapperMock.Verify(m => m.Map<EmpModel>(It.IsAny<Emp>()), Times.Exactly(2));
        }

        [Fact]
        public void Get_retrieves_from_repo()
        {
            var sut = CreateSut();

            var fakeEntity = new Emp { Id = Guid.NewGuid() };
            var fakeModel = new EmpModel { Id = fakeEntity.Id };

            empRepoMock
                .Setup(r => r.GetById(fakeEntity.Id))
                .Returns(fakeEntity);

            mapperMock
                .Setup(m => m.Map<EmpModel>(fakeEntity))
                .Returns(fakeModel);

            var result = sut.Get(fakeEntity.Id);

            result.Should().Be(fakeModel);
        }

        [Fact]
        public void Put_saves_entity_from_mapper()
        {
            var sut = CreateSut();

            var fakeEntity = new Emp { Id = Guid.NewGuid() };
            var fakeModel = new EmpModel { Id = fakeEntity.Id };

            empRepoMock
                .Setup(r => r.GetById(fakeEntity.Id))
                .Returns(fakeEntity);

            mapperMock
                .Setup(m => m.Map<Emp>(fakeModel))
                .Returns(fakeEntity);

            sut.Put(fakeModel.Id, fakeModel);

            empRepoMock.Verify(r => r.Put(fakeEntity), Times.Once);
        }

        [Fact]
        public void Put_saves_entity_from_mapper_with_id_from_query()
        {
            var sut = CreateSut();

            var fakeEntity = new Emp { Id = Guid.NewGuid() };
            var fakeModel = new EmpModel { Id = Guid.Empty }; // Empty Guid!

            empRepoMock
                .Setup(r => r.GetById(fakeEntity.Id))
                .Returns(fakeEntity);

            mapperMock
                .Setup(m => m.Map<Emp>(fakeModel))
                .Returns(fakeEntity);

            sut.Put(fakeEntity.Id, fakeModel); // Pass entity (correct) id though!

            mapperMock.Verify(m => m.Map<Emp>(It.Is<EmpModel>(model => model.Id == fakeEntity.Id)));
        }

        [Fact]
        public void Delete_calls_repo()
        {
            var sut = CreateSut();
            var id = Guid.NewGuid();
            sut.Delete(id);
            empRepoMock.Verify(r => r.Delete(id));
        }
    }
}
