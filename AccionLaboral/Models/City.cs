using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        public int CityId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}