using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NYSCFileRecord.Models
{
    public class FileRecord
    {
        [Key]
        public int Id { get; set; }

        public string CodeNumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        public string TakenTo { get; set; }

        public string ReturnedFrom { get; set; }

        public string CollectingOfficer { get; set; }

        public string RecordingOfficer { get; set; }

        public bool IsActive { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
