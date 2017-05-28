using AutoMapper;
using EmpGrid.Api.Models.Core;
using EmpGrid.Domain;
using EmpGrid.Domain.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EmpGrid.Api.Controllers
{
    [Route("api/emp")]
    public class EmpController
    {
        private readonly IBulkEntityRepository<Emp> empRepository;
        private readonly IMapper mapper;

        public EmpController(
            IBulkEntityRepository<Emp> empRepository,
            IMapper mapper)
        {
            this.empRepository = empRepository;
            this.mapper = mapper;
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
            // TODO: Validation
            empModel.Id = id;
            var emp = mapper.Map<Emp>(empModel);
            empRepository.Put(emp);
        }
        
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            // TODO: Authentication, authorization, and logging.
            empRepository.Delete(id);
        }
    }
}
