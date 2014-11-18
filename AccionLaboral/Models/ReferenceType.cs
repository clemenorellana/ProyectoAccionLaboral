using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class ReferenceType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int ReferenceTypeId { get; set; }
        [Required]
        public int Name { get; set; }
    }
}
