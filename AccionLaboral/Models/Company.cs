using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
        public DateTime DateCreated { get; set; }

        public ICollection<ContactByCompany> ContactsByCompany { get; set; }
        public ICollection<VacantByCompany> VacantsByCompany { get; set; }
    }
}
