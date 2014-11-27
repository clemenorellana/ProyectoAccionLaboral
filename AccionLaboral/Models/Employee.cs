using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AccionLaboral.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        public int EmployeeId { get; set; }
        [Required]
        [Index("IX_EmployeeFirstNameLastName", 1, IsUnique = true)]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [Index("IX_EmployeeFirstNameLastName", 2, IsUnique = true)]
        [StringLength(50)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        public string Cellphone { get; set; }
        public string HomePhone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string EmployeeAlias { get; set; }        
        public int CareerId { get; set; }
        public DateTime AdmissionDate { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }


        //public Role Role { get; set; }
        public User User { get; set; }
        public Career Career { get; set; }
    }
}
