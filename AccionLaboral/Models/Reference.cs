using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class Reference
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int ReferenceId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Charge { get; set; }
        public string Cellphone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string CompanyName { get; set; }        
        public string Relationship { get; set; }
        public int ReferenceTypeId { get; set; }
        public int CityId { get; set; }
        public int ClientId { get; set; }

        public ReferenceType ReferenceType { get; set; }
        public City City { get; set; }
        public Client Client { get; set; }
    }
}
