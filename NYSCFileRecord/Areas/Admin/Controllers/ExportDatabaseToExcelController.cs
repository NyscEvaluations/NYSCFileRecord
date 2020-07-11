using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NYSCFileRecord.Data;
using NYSCFileRecord.Domain.Services;
using NYSCFileRecord.Models.ViewModels;
using NYSCFileRecord.Utility;

namespace NYSCFileRecord.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ExportDatabaseToExcelController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public readonly ApplicationDbContext _db;
        //AccountService accountService = new AccountService();
        public ExportDatabaseToExcelController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Export()
        {
            var UserId = HttpContext.Session.GetInt32(SD.UserId);
            ExportDatabaseToExcelViewModel exportVM = new ExportDatabaseToExcelViewModel()
            {
                //UserModel = await accountService.GetUser(_db, UserId)
            };
            return View(exportVM);
        }

        [HttpPost, ActionName("Export")]
        public async Task<IActionResult> ExportPOST()
        {
            string sWebRootFolder = _webHostEnvironment.WebRootPath; ;
            string sFileName = @"FileRecordExcel.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();

            var fileRecord = _db.FileRecord.ToList();



            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("File Record");
                IRow row = excelSheet.CreateRow(0);



                row.CreateCell(0).SetCellValue("File Id");
                row.CreateCell(1).SetCellValue("Code Number");
                row.CreateCell(2).SetCellValue("Name");
                row.CreateCell(3).SetCellValue("Description");
                row.CreateCell(4).SetCellValue("Phone Number");
                row.CreateCell(5).SetCellValue("Taken To");
                row.CreateCell(6).SetCellValue("Returned From");
                row.CreateCell(7).SetCellValue("shelf Number");
                row.CreateCell(8).SetCellValue("Current Location");
                row.CreateCell(9).SetCellValue("Collecting Officer");
                row.CreateCell(10).SetCellValue("Recording Officer");
                row.CreateCell(11).SetCellValue("Date");

                int count = 1;
                foreach (var item in fileRecord)
                {
                    row = excelSheet.CreateRow(count);
                    row.CreateCell(0).SetCellValue(item.Id);
                    row.CreateCell(1).SetCellValue(item.CodeNumber);
                    row.CreateCell(2).SetCellValue(item.Name);
                    row.CreateCell(3).SetCellValue(item.Description);
                    row.CreateCell(4).SetCellValue(item.PhoneNumber);
                    row.CreateCell(5).SetCellValue(item.TakenTo);
                    row.CreateCell(6).SetCellValue(item.ReturnedFrom);
                    row.CreateCell(7).SetCellValue(item.shelfNumber);
                    row.CreateCell(8).SetCellValue(item.CurrentLocation);
                    row.CreateCell(9).SetCellValue(item.CollectingOfficer);
                    row.CreateCell(10).SetCellValue(item.RecordingOfficer);
                    row.CreateCell(11).SetCellValue(item.DateCreated);
                    count++;
                }

                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }
    }
}