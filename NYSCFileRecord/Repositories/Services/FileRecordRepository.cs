using NYSCFileRecord.Data;
using NYSCFileRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NYSCFileRecord.Repositories.Services
{
    public class FileRecordRepository
    {

        public async Task<string> SaveFileRecord(ApplicationDbContext _db, FileRecord fileRecord)
        {
            var result = string.Empty;

            var newFileRecord = new FileRecord
            {
                CodeNumber = fileRecord.CodeNumber,
                Name = fileRecord.Name,
                Description = fileRecord.Description,
                PhoneNumber = fileRecord.PhoneNumber,
                IsActive = true,
                DateCreated = DateTime.UtcNow
            };

            try
            {
                _db.FileRecord.Add(newFileRecord);
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {

                result = e.Message;
            }

            return result;
        }

        public async Task<string> SaveEditFileRecord(ApplicationDbContext _db, FileRecord fileRecord)
        {
            var result = string.Empty;


            try
            {
                var fileRecordInfo = _db.FileRecord.SingleOrDefault(f => f.Id == fileRecord.Id);

                fileRecordInfo.CodeNumber = fileRecord.CodeNumber;
                fileRecordInfo.Name = fileRecord.Name;
                fileRecordInfo.Description = fileRecord.Description;
                fileRecordInfo.PhoneNumber = fileRecordInfo.PhoneNumber;

                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {

                result = e.Message;
            }

            return result;
        }

        public async Task<string> SaveDeleteFileRecord(ApplicationDbContext _db, FileRecord fileRecord)
        {
            var result = string.Empty;


            try
            {
                var fileRecordInfo = _db.FileRecord.SingleOrDefault(f => f.Id == fileRecord.Id);

                fileRecordInfo.IsActive = false;

                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {

                result = e.Message;
            }

            return result;
        }
    }
}
