using AccionLaboral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccionLaboral.Helpers.Filters
{
    public class ClientsFilter
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string CompleteAddress { get; set; }
        public string Cellphone { get; set; }
        public State State { get; set; }

        public List<Client> Clients { get; set; }

    }
}