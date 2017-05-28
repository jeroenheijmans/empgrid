using AutoMapper;
using EmpGrid.Api.Models.Core;
using EmpGrid.Domain;
using EmpGrid.Domain.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace EmpGrid.Api.Controllers
{
    [Route("api/grid")]
    public class GridController : Controller
    {
        private readonly IBulkEntityRepository<Emp> empRepo;
        private readonly ISingularRepository<Medium> mediumRepo;
        private readonly ILogger<EmpController> logger;
        private readonly IMapper mapper;

        public GridController(
            IBulkEntityRepository<Emp> empRepo,
            ISingularRepository<Medium> mediumRepo,
            ILogger<EmpController> logger,
            IMapper mapper)
        {
            this.empRepo = empRepo;
            this.mediumRepo = mediumRepo;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public GridModel Index()
        {
            return new GridModel
            {
                Emps = empRepo
                    .Query()
                    .Select(e => mapper.Map<EmpModel>(e))
                    .ToArray(),

                Mediums = mediumRepo
                    .List()
                    .Select(e => mapper.Map<MediumModel>(e))
                    .ToArray(),
            };
        }
    }
}
