using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AccionLaboral.Models
{
    //public class MyUser : IdentityUser
    //{
    //    public virtual User MyUserInfo { get; set; }
    //}
    public class User
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        //public int UserId {get; set;}
        //[Required]
        //[Index(IsUnique = true)]
        //[StringLength(25)]
        //public string UserName { get; set; }
        //[Required]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int UserId {get; set;}
        //[Required]
        //[Index(IsUnique=true)]
        //[StringLength(25)]
        //public string UserName { get; set; }
        //[Required]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }
        public bool Active { get; set; }
        public bool Busy { get; set; } // know if the user is assigned to an employee

        //public ICollection<Role> Rols { get; set; }
    }

}