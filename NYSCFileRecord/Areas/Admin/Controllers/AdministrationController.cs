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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NYSCFileRecord.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdministrationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AdministrationController> logger;

        public AdministrationController(ApplicationDbContext db,
                                        RoleManager<IdentityRole> roleManager,
                                        UserManager<ApplicationUser> userManager,
                                        SignInManager<ApplicationUser> signInManager,
                                        ILogger<AdministrationController> logger)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            this.logger = logger;
        }

        FileQueries filesValue = new FileQueries();
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var UserId = HttpContext.Session.GetInt32(SD.UserId);
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            DashboardViewModels dashboardVM = new DashboardViewModels()
            {
                FileRecord = await filesValue.GetAllFiles(_db),
            };

            return View(dashboardVM);
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile()
        {

            //var UserId = HttpContext.Session.GetString(SD.UserId);

            var user = await _userManager.GetUserAsync(User);

            //var userId = await _userManager.FindByIdAsync(user.Id);
            //var user = await accountService.GetUser(_db, UserId);
            var model = new User
            {
                UserId = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Position = user.Position,
                Level = user.Level,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                StreetAddress = user.StreetAddress,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode,
                Qualification = user.Qualification,
                Course = user.Course,
                Institution = user.Institution,
                Biography = user.Biography,
                Picture = user.Picture,
                IsActive = true
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UserProfile(User model)
        {

            var files = HttpContext.Request.Form.Files;

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
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
                    model.Picture = p1;
                }

                var user = await _userManager.FindByIdAsync(model.UserId);

                user.Id = model.UserId;
                user.UserName = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Position = model.Position;
                user.Level = model.Level;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.StreetAddress = model.StreetAddress;
                user.City = model.City;
                user.State = model.State;
                user.PostalCode = model.PostalCode;
                user.Picture = model.Picture;
                user.Qualification = model.Qualification;
                user.Course = model.Course;
                user.Institution = model.Institution;
                user.Biography = model.Biography;
                user.IsActive = true;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(UserProfile));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);

            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UsersList()
        {
            var users = _userManager.Users;
            return View(users);
        }



        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateUser()
        {

            return PartialView();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser(RegisterModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    IsActive = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return Redirect("/Admin/Administration/UsersList");
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect("/Admin/Administration/UserList");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(string Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            //var result = await administratorService.UserDetail(_db, Id);
            var user = await _userManager.FindByIdAsync(Id);

            if (user == null)
            {
                return NotFound();
            }
            var model = new User
            {
                UserId = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                StreetAddress = user.StreetAddress,
                City = user.City,
                State = user.State,
                Picture = user.Picture,
                IsActive = user.IsActive,
                DateCreated = user.DateCreated
            };

            return PartialView(model);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
                var user = await _userManager.FindByIdAsync(model.UserId);

                user.Id = model.UserId;
                user.UserName = model.Username;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.StreetAddress = model.StreetAddress;
                user.City = model.City;
                user.State = model.State;
                user.Picture = user.Picture;
                user.IsActive = model.IsActive;
                user.DateCreated = model.DateCreated;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(UsersList));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            //var result = await administratorService.UserDetail(_db, Id);
            var user = await _userManager.FindByIdAsync(Id);

            if (user == null)
            {
                return NotFound();
            }
            var model = new User
            {
                UserId = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                StreetAddress = user.StreetAddress,
                City = user.City,
                State = user.State,
                Picture = user.Picture,
                IsActive = user.IsActive,
                DateCreated = user.DateCreated
            };

            return PartialView(model);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }
                model.IsActive = false;
                user.IsActive = model.IsActive;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(UsersList));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserDetail(string Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var user = await _userManager.FindByIdAsync(Id);

            if (user == null)
            {
                return NotFound();
            }
            var model = new User
            {
                UserId = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                StreetAddress = user.StreetAddress,
                City = user.City,
                State = user.State,
                Picture = user.Picture,
                IsActive = user.IsActive,
                DateCreated = user.DateCreated
            };

            return PartialView(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> RolesList()
        {
            var UserId = HttpContext.Session.GetInt32(SD.UserId);
            RolesViewModel rolesVM = new RolesViewModel()
            {
                RolesModel = _roleManager.Roles
            };
            return View(rolesVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRole()
        {
            RolesViewModel rolesVM = new RolesViewModel();
            return PartialView(rolesVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(RoleModel roleModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = roleModel.RoleName
                };

                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(RolesList));
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(roleModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var model = new EditRoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                };
            }

            return PartialView(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role.Id));
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(RolesList));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var model = new EditRoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                };
            }

            return PartialView(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return Redirect("/Admin/Administration/ErrorOccured");
            }
            else
            {
                try
                {
                    var result = await _roleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(RolesList));
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
                catch(DbUpdateException ex)
                {
                    logger.LogError($"Error deleting role {ex}");
                    ViewBag.ErrorTitle = $"{role.Name} role is in use";
                    ViewBag.ErrorMessage = $"{role.Name} role cannot be deleted as there are users in this role. " +
                        $"If you want to delete this role, please remove the users from the role and then delete";
                    return View("ErrorOccured");
                }
               
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RoleDetail(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var model = new EditRoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                };
            }

            return PartialView(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                throw new ArgumentNullException(nameof(roleId));
            }

            var model = new List<UserRoleModel>();

            foreach (var user in _userManager.Users)
            {
                var userRoleModel = new UserRoleModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleModel.IsSelected = true;
                }
                else
                {
                    userRoleModel.IsSelected = false;
                }
                model.Add(userRoleModel);
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleModel> model, string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                throw new ArgumentNullException(nameof(roleId));
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = role.Id });
                }
            }

            return RedirectToAction("RolesList", new { Id = role.Id });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ErrorOccured()
        {
            return View();
        }
    }
}