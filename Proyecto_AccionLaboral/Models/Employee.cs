using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_AccionLaboral.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
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


        public Role Role { get; set; }
        public User User { get; set; }
        public Career Career { get; set; }
    }
}
