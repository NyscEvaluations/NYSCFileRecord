using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NYSCFileRecord.Models.ViewModels
{
    public class UserRoleViewModel
    {
        public IEnumerable<UserRoleModel> UserRoleModel { get; set; }
        public User UserModel { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
