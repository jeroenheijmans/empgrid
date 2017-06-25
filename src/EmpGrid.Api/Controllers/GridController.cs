using AutoMapper;
using EmpGrid.Api.Models.Core;
using EmpGrid.Domain;
using EmpGrid.Domain.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace EmpGrid.Api.Controllers
{
    [Route("api/v1/grid")]
    public class GridController : BaseController
    {
        private readonly IBulkEntityRepository<Emp> empRepo;
        private readonly ISingularRepository<Medium> mediumRepo;

        public GridController(
            IBulkEntityRepository<Emp> empRepo,
            ISingularRepository<Medium> mediumRepo,
            ILogger<EmpController> logger,
            IMapper mapper)
            : base(logger, mapper)
        {
            this.empRepo = empRepo;
            this.mediumRepo = mediumRepo;
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
                    .ToDictionary(m => m.Id, m => m),
            };
        }
    }
}
