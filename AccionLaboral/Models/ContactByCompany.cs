using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class ContactByCompany
    {
        public int ContactByCompanyId { get; set; }        
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string Telephone { get; set; }
        public string Extension { get; set; }
        public string Observations { get; set; }
        public int CompanyId { get; set; }

        public Company Company { get; set; }

    }
}
