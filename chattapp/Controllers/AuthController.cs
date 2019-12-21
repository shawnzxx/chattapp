﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OAuth.GitHub;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace chattapp.Controllers
{
    [Route("/")]
    public class AuthController : Controller
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge(GitHubAuthenticationDefaults.AuthenticationScheme);
            }
            HttpContext.Response.Cookies.Append("githubchat_username", User.Identity.Name);
            HttpContext.SignInAsync(User);
            return Redirect("/");
        }
        
        //[HttpGet("signin-github")]
        //public IActionResult LoginCallBack()
        //{
        //    if (!User.Identity.IsAuthenticated) 
        //    {
        //        return Challenge(GitHubAuthenticationDefaults.AuthenticationScheme);                   
        //    }

        //    HttpContext.Response.Cookies.Append("githubchat_username", User.Identity.Name);
        //    HttpContext.SignInAsync(User);
        //    return Redirect("/");
        //}
    }
}