using AutoMapper;
using EmpGrid.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmpGrid.Api.Controllers
{
    [EntityNotFoundExceptionFilter]
    public abstract class EmpGridController : Controller
    {
        protected readonly ILogger<EmpController> logger;
        protected readonly IMapper mapper;

        public EmpGridController(ILogger<EmpController> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }
    }
}
