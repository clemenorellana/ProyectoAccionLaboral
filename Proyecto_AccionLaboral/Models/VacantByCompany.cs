using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class VacantByCompany
    {
        public int VacantByCompanyId { get; set; }
        public string VacantName { get; set; }
        public bool Active { get; set; }
        public int Quantity { get; set; }
        public int StartAge { get; set; }
        public int EndAge { get; set; }
        public string Gender { get; set; }        
        public string Requirements { get; set; }
        public string ChargeDescription { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime CoverdDate { get; set; }
        public int CompanyId { get; set; }
        public int AcademicLevelId { get; set; }
        public int CareerId { get; set; }
        public int CityId { get; set; }
        public int VacantLevelId { get; set; }


        public Company Company { get; set; }
        public AcademicLevel AcademicLevel { get; set; }
        public Career Career { get; set; }
        public City City { get; set; }
        public VacantLevel VacantLevel { get; set; }
    }
}
