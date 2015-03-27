using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccionLaboral.Models;

namespace AccionLaboral.Helpers.Filters
{
    public class VacantFilter
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public VacantByCompany VacantByCompany { get; set; }
        public List<VacantCovered> VacantCovered { get; set; }
        public string ReportName { get; set; }
    }

    public class VacantReportFilter
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<VacantFilter> Vacants { get; set; }
        public string ReportName { get; set; }
    }
}