using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NYSCFileRecord.Data;
using NYSCFileRecord.Domain.Services;
using NYSCFileRecord.Models;
using NYSCFileRecord.Models.ViewModels;
using NYSCFileRecord.Utility;

namespace NYSCFileRecord.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ImportExcelToDatabaseController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public readonly ApplicationDbContext _db;
        AccountService accountService = new AccountService();
        public ImportExcelToDatabaseController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Import()
        {
            var UserId = HttpContext.Session.GetInt32(SD.UserId);
            ImportExcelToDatabaseViewModel importVM = new ImportExcelToDatabaseViewModel()
            {
                UserModel = await accountService.GetUser(_db, UserId)
            };
            return View(importVM);
        }

        [HttpPost, ActionName("Import")]
        public IActionResult ImportPOST()
        {
            //IFormFile file = Request.Form.Files[0];
            var file = HttpContext.Request.Form.Files;
            string folderName = "UploadExcel";
            string webRootPath = _webHostEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            StringBuilder sb = new StringBuilder();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file[0].Length > 0)
            {
                string sFileExtension = Path.GetExtension(file[0].FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file[0].FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file[0].CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }
                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;
                    List<FileRecord> fileRowList = new List<FileRecord>();
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        FileRecord f = new FileRecord
                        {
                            CodeNumber = row.GetCell(1).ToString(),
                            Name = row.GetCell(2).ToString(),
                            Description = row.GetCell(3).ToString(),
                            IsActive = true,
                            DateCreated = DateTime.UtcNow
                        };
                        _db.FileRecord.Add(f);
                        
                    }
                    _db.SaveChanges();
                }
            }
            return Redirect("/Admin/File/Index");
        }
    }
}