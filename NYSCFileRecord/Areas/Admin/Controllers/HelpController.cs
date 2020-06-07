using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NYSCFileRecord.Data;
using NYSCFileRecord.Domain.Services;
using NYSCFileRecord.Models.ViewModels;
using NYSCFileRecord.Utility;
using System.Threading.Tasks;

namespace NYSCFileRecord.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HelpController : Controller
    {
        public HelpController(ApplicationDbContext db)
        {
            _db = db;
        }
        private readonly ApplicationDbContext _db;
        AccountService accountService = new AccountService();
        public async Task<IActionResult> Index()
        {
            var UserId = HttpContext.Session.GetInt32(SD.UserId);
            HelpViewModel helpVM = new HelpViewModel()
            {
                UserModel = await accountService.GetUser(_db, UserId)
            };
            return View(helpVM);
        }
    }
}