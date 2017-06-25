using AutoMapper;
using EmpGrid.Api.Models.Core;
using EmpGrid.Domain;
using EmpGrid.Domain.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace EmpGrid.Api.Controllers
{
    [Route("api/v1/emp")]
    public class EmpController : BaseController
    {
        private readonly IBulkEntityRepository<Emp> empRepository;

        public EmpController(
            IBulkEntityRepository<Emp> empRepository,
            ILogger<EmpController> logger,
            IMapper mapper)
            : base(logger, mapper)
        {
            this.empRepository = empRepository;
        }

        [HttpGet()]
        public EmpModel[] Index()
        {
            return empRepository
                .Query()
                .Select(e => mapper.Map<EmpModel>(e))
                .ToArray();
        }

        [HttpGet("{id}")]
        public EmpModel Get(Guid id)
        {
            var entity = empRepository.GetById(id);
            return mapper.Map<EmpModel>(entity);
        }

        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]EmpModel empModel)
        {
            logger.LogInformation("PUT api/emp/{0}", id);
            logger.LogTrace("Adding model {0}", empModel);

            // TODO: Authentication, authorization.
            // TODO: Validation.

            empModel.Id = id;
            var emp = mapper.Map<Emp>(empModel);
            empRepository.Put(emp);
        }
        
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            logger.LogInformation("DELETE api/emp/{0}", id);
            
            // TODO: Authentication, authorization.

            empRepository.Delete(id);
        }
    }
}
