using System;
using System.Linq;
using AutoMapper;
using EmpGrid.Api.Models.Core;
using EmpGrid.Domain;
using EmpGrid.Domain.Core;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EmpGrid.Api.Controllers
{
    public class EmpControllerTests
    {
        private readonly Mock<ILogger<EmpController>> _loggerMock = new Mock<ILogger<EmpController>>();
        private readonly Mock<IBulkEntityRepository<Emp>> _empRepoMock = new Mock<IBulkEntityRepository<Emp>>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        private EmpController CreateSut()
        {
            return new EmpController(
                _empRepoMock.Object,
                _loggerMock.Object,
                _mapperMock.Object
            )
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext(),
                }
            };
        }

        [Fact]
        public void Index_queries_emp_repo()
        {
            var sut = CreateSut();

            _empRepoMock
                .Setup(r => r.Query())
                .Returns((new Emp[2]).AsQueryable());

            var result = sut.Index();

            result.Length.Should().Be(2);
        }

        [Fact]
        public void Index_maps_to_models()
        {
            var sut = CreateSut();

            _empRepoMock
                .Setup(r => r.Query())
                .Returns((new Emp[2]).AsQueryable());

            var result = sut.Index();

            _mapperMock.Verify(m => m.Map<EmpModel>(It.IsAny<Emp>()), Times.Exactly(2));
        }

        [Fact]
        public void Get_retrieves_from_repo()
        {
            var sut = CreateSut();

            var fakeEntity = new Emp { Id = Guid.NewGuid() };
            var fakeModel = new EmpModel { Id = fakeEntity.Id };

            _empRepoMock
                .Setup(r => r.GetById(fakeEntity.Id))
                .Returns(fakeEntity);

            _mapperMock
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

            _empRepoMock
                .Setup(r => r.GetById(fakeEntity.Id))
                .Returns(fakeEntity);

            _mapperMock
                .Setup(m => m.Map<Emp>(fakeModel))
                .Returns(fakeEntity);

            sut.Put(fakeModel.Id, fakeModel);

            _empRepoMock.Verify(r => r.Put(fakeEntity), Times.Once);
        }

        [Fact]
        public void Put_saves_entity_from_mapper_with_id_from_query()
        {
            var sut = CreateSut();

            var fakeEntity = new Emp { Id = Guid.NewGuid() };
            var fakeModel = new EmpModel { Id = Guid.Empty }; // Empty Guid!

            _empRepoMock
                .Setup(r => r.GetById(fakeEntity.Id))
                .Returns(fakeEntity);

            _mapperMock
                .Setup(m => m.Map<Emp>(fakeModel))
                .Returns(fakeEntity);

            sut.Put(fakeEntity.Id, fakeModel); // Pass entity (correct) id though!

            _mapperMock.Verify(m => m.Map<Emp>(It.Is<EmpModel>(model => model.Id == fakeEntity.Id)));
        }

        [Theory]
        [InlineData(PutResult.Created, StatusCodes.Status201Created)]
        [InlineData(PutResult.Updated, StatusCodes.Status200OK)]
        public void Put_returns_http_status_corresponding_to_PutResult(PutResult repoReturnResult, int expectedStatusCode)
        {
            var sut = CreateSut();

            _empRepoMock
                .Setup(r => r.Put(It.IsAny<Emp>()))
                .Returns(repoReturnResult);

            sut.Put(Guid.NewGuid(), new EmpModel());

            sut.HttpContext.Response.StatusCode.Should().Be(expectedStatusCode);
        }

        [Fact]
        public void Delete_calls_repo()
        {
            var sut = CreateSut();
            var id = Guid.NewGuid();
            sut.Delete(id);
            _empRepoMock.Verify(r => r.Delete(id));
        }

        [Fact]
        public void Delete_sets_http_status_no_content()
        {
            var sut = CreateSut();
            sut.Delete(Guid.NewGuid());
            sut.HttpContext.Response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }
    }
}
