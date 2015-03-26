using AccionLaboral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccionLaboral.Helpers.Filters
{
    public class CompaniesFilters
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<Company> Companies { get; set; }
        public string ReportName { get; set; }
    }
}