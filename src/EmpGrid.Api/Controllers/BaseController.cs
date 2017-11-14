using AutoMapper;
using EmpGrid.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmpGrid.Api.Controllers
{
    [EntityNotFoundExceptionFilter]
    public abstract class BaseController : Controller
    {
        protected readonly ILogger logger;
        protected readonly IMapper mapper;

        public BaseController(ILogger logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }
    }
}
