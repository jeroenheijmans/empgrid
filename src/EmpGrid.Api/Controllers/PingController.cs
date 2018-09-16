using EmpGrid.Api.Models.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmpGrid.Api.Controllers
{
    [Route("api/v1/ping")]
    public class PingController
    {
        private static readonly PingResult pingResult = new PingResult();

        [HttpGet]
        [AllowAnonymous]
        public PingResult Get()
        {
            return pingResult;
        }
    }
}
