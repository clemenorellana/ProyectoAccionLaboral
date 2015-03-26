using AccionLaboral.Models;
using Novacode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace AccionLaboral.Reports.Helpers
{
    public static class Contracts
    {

        public static void CreateContract(this DocX document, ContractTemplate contract)
        {
            Formatting Bold = new Formatting();
            Bold.Bold = true;
            Formatting noBold = new Formatting();
            noBold.Bold = false;
            document.ReplaceText("{ContractTemplateContent}", contract.Description);
        }

    }
}