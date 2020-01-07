﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopWebsite.Models;

namespace ShopWebsite.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public UsersController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(userManager.Users);
        }
    }
}