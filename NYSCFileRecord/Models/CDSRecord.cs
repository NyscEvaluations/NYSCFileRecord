using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NYSCFileRecord.Models
{
    public class CDSRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string StateCode { get; set; }

        [Required]
        public string Fullname { get; set; }

        [Required]
        public string Status { get; set; }
        public enum EStatus { Ative=0, PassedOut=1}

        [Required]
        public DateTime AttendanceTimeIn { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
