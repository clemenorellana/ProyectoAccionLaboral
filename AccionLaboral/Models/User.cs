using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int UserId {get; set;}
        [Required]
        [Index(IsUnique=true)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public ICollection<Role> Rols { get; set; }
    }
}