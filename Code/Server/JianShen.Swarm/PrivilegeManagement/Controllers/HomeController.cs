﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrivilegeManagement.Models;

namespace PrivilegeManagement.Controllers
{
    //固定角色，在授权时带上角色，这种一般不常用，常用的是自定义角色，用户可访问配置到数据库
    //[Authorize(Roles = "admin,system")]  
    public class HomeController : BaseController //继承基础Controller,权限验证[Authorize]放在基础控制器控制，需要权限的页面继承就行了
    {       
        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles ="system")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
        public async Task<IActionResult> Login(string userName,string password,string returnUrl = null)
        {
            var list = new List<dynamic> {
                new { UserName = "zh", Password = "111", Role = "admin",Name="桂素伟" },
                new { UserName = "zh2", Password = "111", Role = "system",Name="测试A" }
            };

            var user = list.SingleOrDefault(s => s.UserName == userName && s.Password == password);
            if (user != null)
            {
                //用户标识
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Sid, userName));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));              


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
                const string message = "用户名或密码错误!";
                return BadRequest(message);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }
    }
}
