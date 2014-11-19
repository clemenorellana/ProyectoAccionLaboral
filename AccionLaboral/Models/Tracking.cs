using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AccionLaboral.Models
{
    public class Tracking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        public int TrackingId { get; set; }  
        public int TrackingTypeId { get; set; }
        public int ClientId { get; set; }
        public int CompanyId { get; set; }
        public int StateId { get; set; }
        public int VacantByCompanyId { get; set; }


        public VacantByCompany VacantByCompany { get; set; }
        public TrackingType TrackingType { get; set; }
        public Client Client { get; set; }
        
        public Company Company { get; set; }
        public State State { get; set; }
        public ICollection<TrackingDetail> TrackingDetails { get; set; }


    }
}