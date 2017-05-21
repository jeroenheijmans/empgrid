using EmpGrid.Api.Models.Core;
using EmpGrid.Domain;
using EmpGrid.Domain.Core;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EmpGrid.Api.Controllers
{
    [Route("api/grid")]
    public class GridController : Controller
    {
        private readonly IBulkEntityRepository<Emp> empRepo;

        public GridController(IBulkEntityRepository<Emp> empRepo)
        {
            this.empRepo = empRepo;
        }

        [HttpGet]
        public GridModel Index()
        {
            return new GridModel
            {
                Emps = empRepo
                    .Query()
                    .Select(e => new EmpModel
                    {
                        Name = e.Name,
                        EmailAddress = e.EmailAddress,
                        TagLine = e.TagLine,
                    })
                    .ToArray(),
            };
        }
    }
}
