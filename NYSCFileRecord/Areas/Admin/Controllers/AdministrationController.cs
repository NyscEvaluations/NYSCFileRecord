using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NYSCFileRecord.Data;
using NYSCFileRecord.Domain.Services;
using NYSCFileRecord.Models;
using NYSCFileRecord.Models.ViewModels;
using NYSCFileRecord.Repositories.Queries;
using NYSCFileRecord.Utility;
using System.IO;

namespace NYSCFileRecord.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdministrationController : Controller
    {
        private readonly ApplicationDbContext _db;
        AccountService accountService = new AccountService();
        AdministratorService administratorService = new AdministratorService();

        public AdministrationController(ApplicationDbContext db)
        {
            _db = db;
        }

        FileQueries filesValue = new FileQueries();
        public async Task<IActionResult> Index()
        {
            var UserId = HttpContext.Session.GetInt32(SD.UserId);
            DashboardViewModels dashboardVM = new DashboardViewModels()
            {
                FileRecord = await filesValue.GetAllFiles(_db),
                User = await accountService.GetAllUsers(_db),
                Top8Users = await accountService.Gettop8Users(_db),
                UserModel = await accountService.GetUser(_db, UserId)
            };

            return View(dashboardVM);
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile()
        {

            var UserId = HttpContext.Session.GetInt32(SD.UserId);

            var user = await accountService.GetUser(_db, UserId);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UserProfile(User user)
        {
            if(user.UserId == 0)
            {
                 throw new ArgumentNullException();
            }
            var files = HttpContext.Request.Form.Files;
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            if (files.Count > 0)
            {
                byte[] p1 = null;
                using (var fs1 = files[0].OpenReadStream())
                {
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                }
                user.Picture = p1;
            }

            var result = await accountService.UpdateUser(_db, user);

            if(result != null)
            {
                return View(user);
            }

            return RedirectToAction(nameof(UserProfile));
        }
        public async Task<IActionResult> UsersList()
        {
            var UserId = HttpContext.Session.GetInt32(SD.UserId);
            UsersViewModel usersVM = new UsersViewModel()
            {
                User = await administratorService.UserList(_db),
                UserModel = await administratorService.UserDetail(_db, UserId)
            };
            return View(usersVM);
        }



        [HttpGet]
        public IActionResult CreateUser()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            //if (ModelState.IsValid)
            //{
                var result = await administratorService.CreateNewUser(_db, user);

                return RedirectToAction(nameof(UsersList));
            //}

            //return PartialView(user);
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var result = await administratorService.UserDetail(_db, Id);

            if (result == null)
            {
                return NotFound();
            }

            return PartialView(result);
        }


        [HttpPost]
        public async Task<IActionResult> EditUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            //if (ModelState.IsValid)
            //{
                var result = await administratorService.UpdateUser(_db, user);

                return RedirectToAction(nameof(UsersList));
            //}

            //return PartialView(user);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var result = await administratorService.UserDetail(_db, Id);

            if (result == null)
            {
                return NotFound();
            }

            return PartialView(result);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            //if (ModelState.IsValid)
            //{
                var result = await administratorService.DeleteUser(_db, user);

                return RedirectToAction(nameof(UsersList));
            //}

            //return PartialView(user);
        }

        [HttpGet]
        public async Task<IActionResult> UserDetail(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var result = await administratorService.UserDetail(_db, Id);

            if (result == null)
            {
                return NotFound();
            }

            return PartialView(result);
        }
    }
}