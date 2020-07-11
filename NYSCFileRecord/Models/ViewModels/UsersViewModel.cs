using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NYSCFileRecord.Models.ViewModels
{
    public class UsersViewModel
    {
        public int AppRoleId { get; set; }
        public IEnumerable<User> User { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<User> Top8Users { get; set; }
        public IEnumerable<SelectListItem> AppRoleList { get; set; }
        public User UserModel { get; set; }
    }
}
