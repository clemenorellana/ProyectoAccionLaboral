using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Description;

namespace AccionLaboral_Site.Controllers
{
    public class ClientContact 
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }


    public class ContactUsController : ApiController
    {
        // POST api/Employees
        [Route("api/Contactus")]
        [HttpPost]
        public bool Contactus(ClientContact clientContact)
        {
            bool result = true;
            try
            {
                // Gmail Address from where you send the mail
                var fromAddress = "accionlaboralhnsps@gmail.com";

                // any address where the email will be sending
                var toAddress = "info@accionlaboralhn.com";

                //Password of your gmail address
                const string fromPassword = "4ccionl4bor4l";

                // Passing the values and make a email formate to display
                string subject = clientContact.Subject;
                string body = "De: " + clientContact.FullName + "\n";
                body += "Correo Electrónico: " + clientContact.Email + "\n";
                body += "Asunto: " + clientContact.Subject + "\n";
                body += "Mensaje: \n" + clientContact.Message + "\n";
                // smtp settings
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                    smtp.Timeout = 20000;
                }
                // Passing values to smtp object
                smtp.Send(fromAddress, toAddress, subject, body);
                
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                result = false;
            }

            return result;
        }
    }
}
