using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NYSCFileRecord.Models.ViewModels
{
    public class FileViewModel
    {
        public IEnumerable<FileRecord> FileRecord { get; set; }
        public FileRecord FileRecordModel { get; set; }
        public User UserModel { get; set; }
    }
}
