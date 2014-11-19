using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AccionLaboral.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int RoleId { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Privilege> Privileges { get; set; }
        public ICollection<User> Users { get; set; }


    }
}