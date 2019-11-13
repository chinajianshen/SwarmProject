using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolicyPrivilegeManagement.Models;

namespace PolicyPrivilegeManagement.Controllers
{
    [Authorize(Policy = "RequireClaim")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Roles = "system")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        [Authorize(Roles = "system")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login(string returnUrl = null)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(string userName, string password, string returnUrl = null)
        {
            var list = new List<dynamic> {
               new { UserName = "zh", Password = "111", Role = "admin",Name="张三",Country="中国",Date="2017-09-02",BirthDay="1979-06-22"},
               new { UserName = "zh2", Password = "111", Role = "system",Name="测试A" ,Country="美国",Date="2017-09-03",BirthDay="1999-06-22"}
           };

            var user = list.SingleOrDefault(s => s.UserName == userName && s.Password == password);
            if (user != null)
            {
                //用户标识
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Sid, userName));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
                identity.AddClaim(new Claim(ClaimTypes.Country, user.Country));
                identity.AddClaim(new Claim("date", user.Date));

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                if (returnUrl == null)
                {
                    returnUrl = TempData["returnUrl"]?.ToString();
                }
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            else
            {
                const string message = "用户名或密码错误！";
                return BadRequest(message);
            }
        }

        [AllowAnonymous]
        [HttpGet("Denied")]
        public IActionResult Denied()
        {
            return View();
        }

        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index", "Home");
        }


        public IActionResult T1()
        {
            return Content("1111");
        }

        public IActionResult T2()
        {
            return Content("2222");
        }
    }
}
