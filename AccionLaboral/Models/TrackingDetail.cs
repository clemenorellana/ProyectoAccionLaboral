using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class TrackingDetail
    {
        public int TrackingDetailId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int ShipmentTypeId { get; set; }
        public int ClientTrackingId { get; set; }


        public ShipmentType ShipmentType { get; set; }
        public ClientTracking ClientTracking { get; set; }
        

    }
}
