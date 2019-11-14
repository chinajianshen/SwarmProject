using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ActionResultStudy.Models;
using ActionResultStudy.Controllers.CustomActionResult;

namespace ActionResultStudy.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Get()
        {
            return new CustomResult(new
            {
                Data = new { a = 1, B = "bbb", C = DateTime.Now, d = new string[] { "a", "b", "c" } },
                Status = true,
                Message = "成功"
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
