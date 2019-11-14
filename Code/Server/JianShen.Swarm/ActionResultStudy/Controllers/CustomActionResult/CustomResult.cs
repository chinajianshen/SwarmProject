using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionResultStudy.Controllers.CustomActionResult
{
    public class CustomResult:ActionResult
    {
        public object Value { get; private set; }

        public CustomResult(object value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Result执行者
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ExecuteResultAsync(ActionContext context)
        {
            //return base.ExecuteResultAsync(context);
            var services = context.HttpContext.RequestServices;
            var executor = services.GetRequiredService<IActionResultExecutor<CustomResult>>();
            await executor.ExecuteAsync(context, new CustomResult(this));

        }
    }

    public class CustomResultExecutor<T> : IActionResultExecutor<T> where T : CustomResult
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(ActionContext context, T result)
        {
            var valueString = JsonConvert.SerializeObject(result.Value);
            context.HttpContext.Response.ContentType = "Content-Type:text/html;charset=utf-8";
            await context.HttpContext.Response.WriteAsync(valueString);
;        }
    }
}
