using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AccionLaboral.Models
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        public int CompanyId { get; set; }
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        public string Area { get; set; }
        public DateTime DateCreated { get; set; }

        public ICollection<ContactByCompany> ContactsByCompany { get; set; }
        public ICollection<VacantByCompany> VacantsByCompany { get; set; }
    }
}
