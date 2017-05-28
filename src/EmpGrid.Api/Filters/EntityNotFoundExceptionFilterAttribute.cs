using EmpGrid.Domain;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace EmpGrid.Api.Filters
{
    public class EntityNotFoundExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is EntityNotFoundException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.ExceptionHandled = true;
            }

            base.OnException(context);
        }
    }
}
