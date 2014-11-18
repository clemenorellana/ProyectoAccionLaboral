using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class Privilege
    {
        public int PrivilegeId { get; set; }
        public string Name { get; set; }

        public ICollection<Role> Rols { get; set; }
    }
}