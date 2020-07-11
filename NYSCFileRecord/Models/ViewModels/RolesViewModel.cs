using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NYSCFileRecord.Models.ViewModels
{
    public class RolesViewModel
    {
        public User UserModel { get; set; }
        public IEnumerable<IdentityRole> RolesModel { get; set; }
        public RoleModel RoleName { get; set; }
    }
}
