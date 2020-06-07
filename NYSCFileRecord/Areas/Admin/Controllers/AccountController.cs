using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NYSCFileRecord.Data;
using NYSCFileRecord.Domain.Services;
using NYSCFileRecord.Models;
using NYSCFileRecord.Repositories.Queries;
using NYSCFileRecord.Utility;

namespace NYSCFileRecord.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        AccountQueries accountQueries = new AccountQueries();
        private readonly ApplicationDbContext _db;
        AccountService accountService = new AccountService();

        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            user.Username = "1234";
            user.PhoneNumber = "123";
            user.StreetAddress = "df";
            user.PostalCode = "dg";
            user.State = "jfj";
            user.City = "kv";


            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            
            var db = new ApplicationDbContext();

            var result = await accountQueries.GetUserByCodeNumber(_db, user.Username);
            //if(ModelState.IsValid)
            //{
            var result1 = await accountService.RegisterNewUser(_db, user);
            //}
            return RedirectToAction(nameof(Register));
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var UserId = string.Empty;

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            //if(!ModelState.IsValid)
            //{
            //    return View("Login", User);
            //}

            var isUserValid = await accountService.Login(_db, user);

            HttpContext.Session.SetInt32(SD.UserId, accountService.UserId);

            //return RedirectToAction(nameof(Login));

            if (!isUserValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                HttpContext.Session.SetString(SD.ErrorMessage, "Username or Password is Incorrect");
                return View("Login", user);
            }
            HttpContext.Session.SetString(SD.ErrorMessage, string.Empty);
            return Redirect("/Admin/Administration/Index");
        }

        public  IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
}