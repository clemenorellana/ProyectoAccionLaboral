using AccionLaboral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccionLaboral.Helpers.Filters
{
    public class ContractFilter
    {
        public string ContractContent { get; set; }
        public Client ClientData { get; set; }
        public ContractTemplate ContractTemplateData { get; set; }
        public string FileName { get; set; }
    }
}