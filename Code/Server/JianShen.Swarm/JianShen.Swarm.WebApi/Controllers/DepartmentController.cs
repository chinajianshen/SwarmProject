using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JianShen.Swarm.Common;
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
        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger) : base(logger)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var result = _departmentService.GetList();
            return OK(result);
        }

        [HttpGet]
        public IActionResult GetPageList()
        {
            BaseQueryEntity queryEntity = new BaseQueryEntity() { PageSize = 2, PageIndex = 1, OrderBy = "DepartmentID desc", WhereCondition = "where DepartmentID in (1,2,3,4)" };          
            return ExecuteQueryData<BaseQueryEntity, PagedList<DepartmentEntity>>(queryEntity, query => _departmentService.GetPagedListDto(queryEntity));
        }

        [HttpGet]
        public async Task<IActionResult> GetPageList2()
        {
            BaseQueryEntity queryEntity = new BaseQueryEntity() { PageSize = 2, PageIndex = 1, OrderBy = "DepartmentID desc", WhereCondition = "where DepartmentID in @ids", Parameters = new { ids = new[] { 1, 2, 3, 4 } } };

            return await ExecuteQueryData<BaseQueryEntity, PagedList<DepartmentEntity>>(queryEntity, query => _departmentService.GetPagedListDtoAsync(queryEntity));
        }
    }
}