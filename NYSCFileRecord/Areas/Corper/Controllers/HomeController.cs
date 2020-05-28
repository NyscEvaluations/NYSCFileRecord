using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NYSCFileRecord.Data;
using NYSCFileRecord.Domain.Services;
using NYSCFileRecord.Models;
using NYSCFileRecord.Repositories.Queries;

namespace NYSCFileRecord.Controllers
{
    [Area("Corper")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly FileService fileService;

        FileQueries filesValue = new FileQueries();
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        //FileQueries fileRecord;

        
        
        public async Task<IActionResult> Index()
        {

            //var result = fileService.GetAllFilesView();
            var result = await filesValue.GetAllFiles(_db);
            //var fileView = _db.FileRecord.ToList();
            return View(result);
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
