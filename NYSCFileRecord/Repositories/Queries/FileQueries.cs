﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<List<FileRecord>> GetAllFiles(ApplicationDbContext _db)
        {
            //IQueryable<object> result;

            var result = await (from f in _db.FileRecord
                                where f.IsActive.Equals(true)
                                select new FileRecord
                                {
                                    Id = f.Id,
                                    Name = f.Name,
                                    CodeNumber = f.CodeNumber,
                                    Description = f.Description,
                                    PhoneNumber = f.PhoneNumber
                                }).ToListAsync();

            return result;
        }
        //    var result = await _db.FileRecord
        //                  .Select(f =>
        //                  new FileRecord
        //                  {
        //                      Id = f.Id,
        //                      Name = f.Name,
        //                      CodeNumber = f.CodeNumber,
        //                      Description = f.Description,
        //                      PhoneNumber = f.PhoneNumber
        //                  }).Where(f => f.IsActive.Equals(true)).ToListAsync();

            
        //    return result;
        //}

        public async Task<FileRecord> GetFilesById(ApplicationDbContext _db, int? fileId)
        {
            var result = await _db.FileRecord
                        .Select(f =>
                                  new FileRecord
                                  {
                                      Id = f.Id,
                                      Name = f.Name,
                                      CodeNumber = f.CodeNumber,
                                      Description = f.Description,
                                      PhoneNumber = f.PhoneNumber
                                  }).Where(f => f.Id == fileId).FirstOrDefaultAsync();
            return result;     
         }
    }
}
