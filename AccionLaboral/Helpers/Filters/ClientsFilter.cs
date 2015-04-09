using AccionLaboral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccionLaboral.Helpers.Filters
{
    public class ClientsFilter
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Title { get; set; }
        public List<Client> Clients { get; set; }

    }
}