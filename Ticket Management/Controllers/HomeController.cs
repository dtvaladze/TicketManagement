using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using TicketManagement.Helpers;

namespace TicketManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(
                   UserManager<AppUser> userManager,
                   SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;  
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.UserObj = await UserHelper.getUser(HttpContext, _userManager); 
            return View();
        }


        [Authorize]
        public async Task<IActionResult> UserInfo()
        {
            var user = await UserHelper.getUser(HttpContext, _userManager);
            ViewBag.UserObj = user;

            if (user == null)
            {
                RedirectToAction("Login");
            }

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            ViewBag.UserObj = await UserHelper.getUser(HttpContext, _userManager);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AppUser appUser)
        {

            //login functionality
            var user = await _userManager.FindByNameAsync(appUser.UserName);
            
            if (user != null)
            {
                //sign in
                var signInResult = await _signInManager.PasswordSignInAsync(user, appUser.Password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Register");
        }

        public async Task<IActionResult> Register()
        {
            ViewBag.UserObj = await UserHelper.getUser(HttpContext, _userManager);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AppUser appUser)
        {
            //register functionality

            var user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = appUser.UserName,
                Email = appUser.Email,
                Name = appUser.Name,
                Role = appUser.Role,
                Password = appUser.Password
            };

            var result = await _userManager.CreateAsync(user, user.Password);


            if (result.Succeeded)
            {
                // User sign
                // sign in 
                var signInResult = await _signInManager.PasswordSignInAsync(user, user.Password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        } 

    }
}
