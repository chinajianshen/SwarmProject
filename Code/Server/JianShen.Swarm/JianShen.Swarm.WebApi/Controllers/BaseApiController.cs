using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JianShen.Swarm.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected readonly ILogger m_Logger;

        public BaseApiController(ILogger<BaseApiController> logger)
        {
            m_Logger = logger;
        }
    }
}