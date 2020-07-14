using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;
using NYSCFileRecord.Data;
using NYSCFileRecord.Domain.Services;
using NYSCFileRecord.Models.ViewModels;
using NYSCFileRecord.Utility;

namespace NYSCFileRecord.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BackUpController : Controller
    {
        private static string token = "noBqWNfrhOAAAAAAAAAAIX93YUO_lQ4JEE6VebzpYJUU0Pzbg7nUe6sRWrgbDN_C";
        private readonly IWebHostEnvironment _webHostEnvironment;
        public readonly ApplicationDbContext _db;
        //AccountService accountService = new AccountService();
        public BackUpController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }   
        public async Task<IActionResult> Index()
        {
            var UserId = HttpContext.Session.GetInt32(SD.UserId);
            BackUpViewModel backUpVM = new BackUpViewModel()
            {
                //UserModel = await accountService.GetUser(_db, UserId)
            };
            return View(backUpVM);
        }

        [HttpPost]
        public IActionResult OneDrive()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DropBox_Rerieve_USerInfo()
        {
            using (var dbx = new DropboxClient(token))
            {
                var id = await dbx.Users.GetCurrentAccountAsync();
                return View(id);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DropBox_Download_File()
        {
            

            using (var dbx = new DropboxClient(token))
            {
                string folder = "";
                string file = "";

                using (var response = await dbx.Files.DownloadAsync(folder + "/" + file))
                {
                    var s = response.GetContentAsByteArrayAsync();
                    s.Wait();
                    var d = s.Result;
                    System.IO.File.WriteAllBytes(file, d);
                    return View(d);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> DropBox_Upload_File()
        {

            string url = "";

            var files = HttpContext.Request.Form.Files;
            string folderName = "UploadDatabase";
            string webRootPath = _webHostEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);

            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            string filename = files[0].FileName;

            string fullPath = Path.Combine(newPath, filename);

            var dbx = new DropboxClient(token);

            if (System.IO.File.Exists(fullPath))
            {
                var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);

                byte[] content;
                using (var reader = new BinaryReader(fileStream))
                {
                    content = reader.ReadBytes((int)fileStream.Length);
                }

                using (var mem = new MemoryStream(content))
                {
                    await dbx.Files.UploadAsync("/" + folderName + "/" + filename, WriteMode.Overwrite.Instance, body:
                          mem);
                    var tx = dbx.Sharing.CreateSharedLinkWithSettingsAsync("/" + folderName + "/" + filename);
                    tx.Wait();
                    url = tx.Result.Url;
                   
                }
            }
            return RedirectToAction(nameof(Index));
        }



       
        public IActionResult BackUpService()
        {

            var trustedFileNameForFileStorage = Path.GetRandomFileName();

            var backUp = ScriptDatabase();

            //UplaodFileOnDrive(backUp);

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
            string folderName = "Credentials";
            string webRootPath = _webHostEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            string filename = Path.Combine(newPath, "client_secret_263519154515-3at0qlc6fsh90fo4jhjr6vr8sprok7qv.apps.googleusercontent.com.json");

            using (var stream =
        new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                string credPath = newPath;

                //string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                credPath = Path.Combine(newPath, "DriveServiceCredentials.json");

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