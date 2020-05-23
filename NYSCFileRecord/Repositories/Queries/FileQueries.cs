using NYSCFileRecord.Data;
using NYSCFileRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NYSCFileRecord.Repositories.Queries
{
    public class FileQueries
    {
        private static ApplicationDbContext _db;
        public FileQueries(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public static IQueryable<object> GetAllFiles()
        {
            IQueryable<object> result;

             result = (from f in _db.FileRecord
                          select new
                          {
                              Name = f.Name,
                              //CodeNumber = f.CodeNumber,
                              //Description = f.Description
                          });
            //var result = _db.FileRecord.ToList();
            return result;
        }

        public static IQueryable<object> Test()
        {
            var vehicles = from v in _db.Users
                           select new
                           {
                               Id = v.Id,
                               
                           };
            return vehicles;
        }
    }
}
