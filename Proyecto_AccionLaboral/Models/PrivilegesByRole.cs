using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class PrivilegesByRole
    {
        public int PrivilegesByRoleId { get; set; }
        public int RoleId { get; set; }
        public int PrivilegeId { get; set; }


        public Role Role { get; set; }
        public Privilege Privilege { get; set; }

    }
}
