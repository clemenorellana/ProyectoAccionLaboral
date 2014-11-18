using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class WorkExperience
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int WorkExperienceId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyArea { get; set; }
        public string Charge { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }        
        public string Achievements { get; set; }
        public int CityId { get; set; }
        public int ClientId { get; set; }

        public City City { get; set; }
        public Client Client { get; set; }

    }
}
