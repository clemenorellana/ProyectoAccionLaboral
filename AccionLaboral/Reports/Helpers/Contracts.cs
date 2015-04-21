using AccionLaboral.Helpers.Filters;
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

        public static void CreateContract(this DocX document, ContractFilter contract)
        {
            Formatting Bold = new Formatting();
            Bold.Bold = true;
            Formatting noBold = new Formatting();
            noBold.Bold = false;
            DateTime enrollDate = contract.ClientData.EnrollDate ?? DateTime.Now;

            document.ReplaceText("{ContractTemplateContent}", contract.ContractContent);
            document.ReplaceText("{CorrelativeCode}", contract.ClientData.CorrelativeCode);
            document.ReplaceText("{Cellphone}", contract.ClientData.Cellphone);
            document.ReplaceText("{EnrollDate}", enrollDate.ToString("dd/MM/yyyy"));
            document.ReplaceText("{FacebookEmail}", contract.ClientData.FacebookEmail);
            document.ReplaceText("{FirstName}", contract.ClientData.FirstName);
            document.ReplaceText("{LastName}", contract.ClientData.LastName);
            document.ReplaceText("{BBPin}", contract.ClientData.BBPin);
            document.ReplaceText("{CompleteAddress}", contract.ClientData.CompleteAddress);
            document.ReplaceText("{Twitter}", contract.ClientData.Twitter);
            document.ReplaceText("{Age}", contract.ClientData.Age.ToString());
            document.ReplaceText("{Email}", contract.ClientData.Email);
            document.ReplaceText("{Occupation}", contract.ClientData.Occupation);
            document.ReplaceText("{IsStudying}", contract.ClientData.IsStudying == true ? "X" : "");
            document.ReplaceText("{IsNotStudying}", (contract.ClientData.IsStudying == false) ? "X" : "");
            document.ReplaceText("{DesiredEmployment}", contract.ClientData.DesiredEmployment);
            document.ReplaceText("{CurrentStudies}", contract.ClientData.CurrentStudies);
            document.ReplaceText("{QtyClasses}", contract.ClientData.QtyClasses.ToString());
            document.ReplaceText("{WageAspiration}", contract.ClientData.WageAspiration.ToString());
            document.ReplaceText("{HaveCar}", contract.ClientData.HaveCar == true ? "X" : "");
            document.ReplaceText("{HaveMotorcycle}", contract.ClientData.HaveMotorcycle == true ? "X" : "");
            document.ReplaceText("{HaveLicense}", contract.ClientData.HaveLicense == true ? "X" : "");
            document.ReplaceText("{LicenseType}", contract.ClientData.LicenseType);
            document.ReplaceText("{CompaniesWithPreviouslyRequested}", contract.ClientData.CompaniesWithPreviouslyRequested);
            document.ReplaceText("{EnglishPercentage}", contract.ClientData.EnglishPercentage.ToString());

            //referncias
            int count = contract.ClientData.References.Where(r => r.ReferenceTypeId == 2).Count();
            if (count == 0)
            {
                Reference r = new Reference();
                r.ReferenceTypeId = 2;
                contract.ClientData.References.Add(r);
                contract.ClientData.References.Add(r);
            }
            else if (count == 1)
            {
                Reference r = new Reference();
                r.ReferenceTypeId = 2;
                contract.ClientData.References.Add(r);
            }

            var id = 1;
            foreach (Reference reference in contract.ClientData.References.Where(r => r.ReferenceTypeId == 2))
            {
                document.ReplaceText("{RefenceFullName" + id + "}", reference.FirstName + " " + reference.LastName);
                document.ReplaceText("{RefenceCellphone" + id + "}", reference.Cellphone);
                document.ReplaceText("{RefenceRelationship" + id + "}", reference.Relationship);
                document.ReplaceText("{RefenceCompanyName" + id + "}", reference.CompanyName);
                id++;
            }
        }
    }
}