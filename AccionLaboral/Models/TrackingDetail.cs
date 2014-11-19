using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class TrackingDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int TrackingDetailId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        public int ShipmentTypeId { get; set; }
        public int TrackingId { get; set; }


        public ShipmentType ShipmentType { get; set; }
        public Tracking Tracking { get; set; }
        

    }
}
