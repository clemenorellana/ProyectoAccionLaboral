using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AccionLaboral.Models
{
    public class VacantByCompany
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
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
        public DateTime CoveredDate { get; set; }
        public string CoveredTime { get; set; }
        public int CompanyId { get; set; }
        public int AcademicLevelId { get; set; }
        public int CareerId { get; set; }
        public int CityId { get; set; }
        public int VacantLevelId { get; set; }
        public int InterviewTypeId { get; set; }



        public InterviewType InterviewType { get; set; }
        public Company Company { get; set; }
        public AcademicLevel AcademicLevel { get; set; }
        public Career Career { get; set; }
        public City City { get; set; }
        public VacantLevel VacantLevel { get; set; }
    }
}
