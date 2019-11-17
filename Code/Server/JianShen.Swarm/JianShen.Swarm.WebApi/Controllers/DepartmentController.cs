using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JianShen.Swarm.Common.Message;
using JianShen.Swarm.DTO;
using JianShen.Swarm.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JianShen.Swarm.WebApi.Controllers
{    
    public class DepartmentController : BaseApiController
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService,ILogger<DepartmentController> logger) : base(logger)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public ReponseMessage<List<DepartmentEntity>> GetList()
        {
            return _departmentService.GetList();
        }
    }
}