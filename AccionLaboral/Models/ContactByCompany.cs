using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class ContactByCompany
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        public int ContactByCompanyId { get; set; }
        [Required]
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string Telephone { get; set; }
        public string Extension { get; set; }
        public string Observations { get; set; }
        public int CompanyId { get; set; }

        public Company Company { get; set; }

    }
}
