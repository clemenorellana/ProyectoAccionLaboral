using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AccionLaboral.Models
{
    public class VacantCovered
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VacantCoveredId { get; set; }
        public int VacantByCompanyId { get; set; }
        public int EmployeeId { get; set; }
        public int NumberOfProfiles { get; set; }

        public Employee Employee { get; set; }
        public VacantByCompany VacantByCompany { get; set; }

    }
}