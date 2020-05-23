using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NYSCFileRecord.Data;
using NYSCFileRecord.Models;
using NYSCFileRecord.Repositories.Queries;

namespace NYSCFileRecord.Controllers
{
    [Area("Corper")]
    public class HomeController : Controller
    {

        public readonly ApplicationDbContext _db;
        FileQueries fileRecord;
        public HomeController(ApplicationDbContext db, FileQueries fileRecord)
        {
            _db = db;
            
        }

        public IActionResult Index()
        {

            //var result = (from f in _db.FileRecord
            //              select new 
            //              {
            //                  Name = f.Name,
            //                  CodeNumber = f.CodeNumber,
            //                  Description = f.Description
            //              });

            var result = FileQueries.GetAllFiles();
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
