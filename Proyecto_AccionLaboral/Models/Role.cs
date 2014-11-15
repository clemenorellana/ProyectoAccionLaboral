using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }

        public ICollection<PrivilegesByRole> Privileges { get; set; }

    }
}