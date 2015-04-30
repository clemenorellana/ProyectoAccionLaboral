using AccionLaboral.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Description;

namespace AccionLaboral.Controllers
{
    [Authorize]
    public class EmployeesController : ApiController
    {
        private AccionLaboralContext db;

        public EmployeesController()
        {
            db = new AccionLaboralContext();
            db.Database.CommandTimeout = 180;
            
        }

        // GET api/Employees
        [Route("api/Employees")]
        [HttpGet]
        public IHttpActionResult GetEmployees()
        {
            var employees = db.Employees.Include("Career").Include("Role").ToList();
            return Ok(employees);
        }


        [Route("api/RecruitmentEmployees")]
        [HttpGet]
        public IQueryable<Employee> RecruitmentEmployees()
        {
            return db.Employees.Include("Role").Where(r => r.Role.Alias == "ASREC");
        }

        // GET api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = db.Employees.Include(r=>r.User).Include(r=>r.Role).Where(r=> r.EmployeeId == id).First();
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT api/Employees/5
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Employees
        [Route("api/Employees")]
        [HttpPost]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            db.SaveChanges();

            
            IdentityUser user = db.Users.Find(employee.UserId);

            MailMessage message = new MailMessage(new MailAddress("accionlaboralhnsps@gmail.com", "Acción Laboral"),
                                             new MailAddress(employee.Email)
                                           );
            message.Subject = "Confirmación de Registro";


            string uri = this.Url.Link("Default", new { controller = "User", action = "ResetPassword" });
            uri = uri.Replace("User", "#");

            message.Body = string.Format(@"Bienvenido(a) {0}
                                    <BR/>
                                    Usted ahora forma parte de la familia Acción Laboral.
                                    <BR/>
                                    Su usuario es: {1}
                                    <BR/>
                                    <BR/>
                                    <a href={3}>De clic aquí para activar su cuenta</a>"
                                  , employee.FirstName
                                  , user.UserName
                                  //, user.Password
                                  , uri);
            //<BR/>
            //Clic para activar su cuenta: <a href=\"{1}\" title=\"User Email Confirm\">{1}</a>", user.UserName, Url.Action("ConfirmEmail", "Account", new { Token = employee.EmployeeId, Email = employee.Email }, Request.Url.Scheme));

            
            //string uri = Url.Action("Action2", "Controller2", new { }, Request.Url.Scheme);

            

            //message.Body = string.Format("Dear {0}<BR/>Thank you for your registration, please click on the below link to complete your registration: <a href=\"{1}\">Registrarse</a>",
            //    user.UserName,
            //    uri);

           
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Credentials = new NetworkCredential("accionlaboralhnsps@gmail.com", "4ccionl4bor4l");
            smtp.EnableSsl = true;
            smtp.Send(message);
            

            return CreatedAtRoute("DefaultApi", new { controller = "employees", id = employee.EmployeeId }, employee);
        }

        // DELETE api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeId == id) > 0;
        }
    }
}