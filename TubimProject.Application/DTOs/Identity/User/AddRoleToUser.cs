using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.DTOs.Identity.User
{
    public class AddRoleToUser
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}
