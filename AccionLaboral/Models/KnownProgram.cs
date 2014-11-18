using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class KnownProgram
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int KnownProgramId { get; set; }
        [Required]
        public int Name { get; set;  }
        [Required]
        public int ClientId { get; set; }

        public Client Client { get; set; }
    }
}
