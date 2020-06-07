using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NYSCFileRecord.Data;
using NYSCFileRecord.Models;

namespace NYSCFileRecord.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FileRecordUploadAndExportController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        
        private readonly string[] _systemDatabaseNames = { "master", "tempdb", "model", "msdb" };

        public readonly ApplicationDbContext _db;
        public FileRecordUploadAndExportController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Import()
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
                    //sb.Append("<table class='table table-bordered'><tr>");
                    
                    //for (int j = 0; j<cellCount; j++)
                    //{
                    //    NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                    //    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                    //    sb.Append("<th>" + cell.ToString() + "</th>");
                    //}
                    //sb.Append("</tr>");
                    //sb.AppendLine("<tr>");
                    List<FileRecord> fileRowList = new List<FileRecord>();
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        FileRecord f = new FileRecord
                        {
                            CodeNumber = row.GetCell(1).ToString(),
                            Name = row.GetCell(2).ToString(),
                            Description = row.GetCell(3).ToString()
                        };
                        _db.FileRecord.Add(f);
                        //fileRowList.Add(f);
                        //if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        //for (int j = row.FirstCellNum; j<cellCount; j++)
                        //{
                        //    if (row.GetCell(j) != null)
                        //        sb.Append("<td>" + row.GetCell(j).ToString() + "</td>");
                        //}
                        //sb.AppendLine("</tr>");
                        //_db.FileRecord.Add(fileRowList);
                        
                    }
                    _db.SaveChanges();


                    //sb.Append("</table>");
                }
            }
            //return this.Content(sb.ToString());
            return RedirectToAction(nameof(Index));
        }

        public bool SaveClass(string codeNumber, ApplicationDbContext _db)
        {
            var result = false;
            try
            {
               

                if(_db.FileRecord.Where(f => f.CodeNumber.Equals(codeNumber)).Count() == 0)
                {
                    var item = new FileRecord();
                    item.CodeNumber = codeNumber;
                    _db.FileRecord.Add(item);
                    _db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return result;
        }


        public ActionResult Download()
        {
            string Files = "wwwroot/UploadExcel/CoreProgramm_ExcelImport.xlsx";
            byte[] fileBytes = System.IO.File.ReadAllBytes(Files);
            System.IO.File.WriteAllBytes(Files, fileBytes);
            MemoryStream ms = new MemoryStream(fileBytes);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "employee.xlsx");
        }


        public async Task<IActionResult> Export()
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
                ISheet excelSheet = workbook.CreateSheet("employee");
                IRow row = excelSheet.CreateRow(0);

                

                row.CreateCell(0).SetCellValue("File Id");
                row.CreateCell(1).SetCellValue("Code Number");
                row.CreateCell(2).SetCellValue("Name");
                row.CreateCell(3).SetCellValue("Description");
                row.CreateCell(4).SetCellValue("Date");

                int count = 1;
                foreach (var item in fileRecord)
                {
                    row = excelSheet.CreateRow(count);
                    row.CreateCell(0).SetCellValue(item.Id);
                    row.CreateCell(1).SetCellValue(item.CodeNumber);
                    row.CreateCell(2).SetCellValue(item.Name);
                    row.CreateCell(3).SetCellValue(item.Description);
                    row.CreateCell(4).SetCellValue(item.DateCreated);
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

        
        public IActionResult BackUpService()
        {
            
            var trustedFileNameForFileStorage = Path.GetRandomFileName();
            //BackupAllUserDatabases();

            
            var backUp = ScriptDatabase();

            UplaodFileOnDrive(backUp);

            var databasename = _db.Database.GetDbConnection().Database;

            string filePath = BuildBackupPathWithFilename(databasename);
            
            using (StreamWriter sw = System.IO.File.CreateText(filePath))
            {
                sw.Write(backUp);

                
            }

            return RedirectToAction(nameof(Index));
        }

        public string ScriptDatabase()
        {
            var sb = new StringBuilder();

            var server = new Server(@"DICKSON");
            var databse = server.Databases["NYSCFileRecord"];

            var scripter = new Scripter(server);
            scripter.Options.ScriptDrops = false;
            scripter.Options.WithDependencies = true;
            scripter.Options.IncludeHeaders = true;
            //And so on ....


            var smoObjects = new Urn[1];
            foreach (Microsoft.SqlServer.Management.Smo.Table t in databse.Tables)
            {
                smoObjects[0] = t.Urn;
                if (t.IsSystemObject == false)
                {
                    StringCollection sc = scripter.Script(smoObjects);

                    foreach (var st in sc)
                    {
                        sb.Append(st);
                    }
                }
            }
            return sb.ToString();
        }

        //public void BackupAllUserDatabases()
        //{
        //    foreach (string databaseName in GetAllUserDatabases())
        //    {
        //        //BackupDatabase(databaseName);
        //        BackupDatabase(_db.Database.GetDbConnection().Database);
        //    }



        //}

        //public void BackupDatabase(string databaseName)
        //{

        //    string filePath = BuildBackupPathWithFilename(databaseName);

        //    using (var connection = new SqlConnection("Server=DICKSON;Database=NYSCFileRecord;Trusted_Connection=True;MultipleActiveResultSets=true"))
        //    {
        //        var query = String.Format("BACKUP DATABASE [{0}] TO DISK='{1}'", databaseName, filePath);

        //        using (var command = new SqlCommand(query, connection))
        //        {
        //            connection.Open();
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}

        //private IEnumerable<string> GetAllUserDatabases()
        //{

        //    var databases = new List<String>();

        //    DataTable databasesTable;

        //    using (var connection = new SqlConnection("Server=DICKSON;Database=NYSCFileRecord;Trusted_Connection=True;MultipleActiveResultSets=true"))
        //    {
        //        connection.Open();

        //        databasesTable = connection.GetSchema("Databases");

        //        connection.Close();
        //    }


        //    foreach (DataRow row in databasesTable.Rows)
        //    {
        //        string databaseName = row["database_name"].ToString();

        //        if (_systemDatabaseNames.Contains(databaseName))
        //            continue;

        //        databases.Add(databaseName);
        //    }

        //    return databases;
        //}

        private string BuildBackupPathWithFilename(string databaseName)
        {
            string folderName = "UploadDatabase";
            string webRootPath = _webHostEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            StringBuilder sb = new StringBuilder();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            string filename = string.Format("{0}-{1}.sql", databaseName, DateTime.Now.ToString("yyyy-MM-dd"));

            string fullPath = Path.Combine(newPath, filename);



            return fullPath;
        }




        //add scope
        public static string[] Scopes = { Google.Apis.Drive.v3.DriveService.Scope.Drive };

        //create Drive API service.
        public DriveService GetService()
        {
            //get Credentials from client_secret.json file 
            UserCredential credential;
            //Root Folder of project
            using (var stream =
        new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = _webHostEnvironment.WebRootPath;

                //string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                credPath = Path.Combine(credPath, "credentials.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            //create Drive API service.
            DriveService service = new Google.Apis.Drive.v3.DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "NYSCFileRecord",
            });
            return service;
        }


        //file Upload to the Google Drive root folder.
        public string UplaodFileOnDrive(string path)
        {

            var service = GetService();
            var fileMetadata = new Google.Apis.Drive.v3.Data.File();
            fileMetadata.Name = Path.GetFileName(path);
            fileMetadata.MimeType = "application/unknown";
            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(path, FileMode.Open))
            {
                request = service.Files.Create(fileMetadata, stream, "image/jpeg");
                request.Fields = "id";
                request.Upload();
            }

            var file = request.ResponseBody;

            return file.Id;


            }
    }

}