using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JianShen.Swarm.Common.Message;
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

        [ApiExplorerSettings(IgnoreApi = true)]
        protected IActionResult Json(object data)
        {
            return new JsonResult(data) { ContentType= "application/json" };
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected IActionResult OK(object data)
        {
            return new JsonResult(new ReponseMessage { Data = data, IsSuccess=true  }) { ContentType = "application/json" };
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected IActionResult Error(string errMsg)
        {
            return new JsonResult(new ReponseMessage { Message=errMsg, IsSuccess = false }) { ContentType = "application/json" };
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        protected async Task<IActionResult> ExecuteQueryData<T1,T2>(T1 queryEntity,Func<T1,Task<T2>> func) where T2 : class where T1 : class
        {
            var queryResult = await func(queryEntity);

            ReponseMessage<T2> resResult = new ReponseMessage<T2>();
            resResult.Data = queryResult;
            resResult.IsSuccess = true;
            return Json(resResult);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ExecuteQueryData<T1,T2>(T1 queryEntity,Func<T1,T2> queryFunc) where T2:class where T1:class
        {
            T2 queryResult  = queryFunc(queryEntity);

            ReponseMessage<T2> resResult = new ReponseMessage<T2>();
            resResult.Data = queryResult;
            resResult.IsSuccess = true;
            return Json(resResult);
        }
      
    }
}