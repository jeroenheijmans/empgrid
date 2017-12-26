using System.Collections.Generic;
using EmpGrid.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Xunit;

namespace EmpGrid.Api.Filters
{
    public class EntityNotFoundExceptionFilterAttributeTests
    {
        [Fact]
        public void OnException_sets_status_code_when_entity_not_found()
        {
            var sut = new EntityNotFoundExceptionFilterAttribute();
            var context = CreateTestContext(new EntityNotFoundException(new StringEntityIdentity()));
            sut.OnException(context);
            context.HttpContext.Response.StatusCode.Should().Be(404);
        }

        [Fact]
        public void OnException_sets_exception_handled_when_entity_not_found()
        {
            var sut = new EntityNotFoundExceptionFilterAttribute();
            var context = CreateTestContext(new EntityNotFoundException(new StringEntityIdentity()));
            sut.OnException(context);
            context.ExceptionHandled.Should().BeTrue();
        }

        private static ExceptionContext CreateTestContext(EntityNotFoundException exception)
        {
            return new ExceptionContext(
                new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor()),
                new List<IFilterMetadata>()
            )
            {
                Exception = exception
            };
        }
    }
}
