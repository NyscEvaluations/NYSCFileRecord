using NYSCFileRecord.Data;
using NYSCFileRecord.Models;
using NYSCFileRecord.Repositories.Queries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NYSCFileRecord.Domain.Services
{
    public class FileService
    {
        private readonly ApplicationDbContext _db;

        //private static readonly FileQueries fileRecordList;
        
        FileQueries filesValue = new FileQueries();
        public async Task<IEnumerable<FileRecord>> GetAllFilesView()
        {
            
            var fileRecordList = await filesValue.GetAllFiles(_db);

            return fileRecordList;
        }

        

        
    }
}
