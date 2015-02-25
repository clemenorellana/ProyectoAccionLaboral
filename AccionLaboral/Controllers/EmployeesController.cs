using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AccionLaboral.Models;
using Newtonsoft.Json;
using System.Net.Mail;

namespace AccionLaboral.Controllers
{
    public class EmployeesController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        // GET api/Employees
        [Route("api/Employees")]
        [HttpGet]
        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees.Include("Career").Include("User");//.Include("Role");
        }


        // GET api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
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

            
            User user = db.Users.Find(employee.UserId);

            MailMessage m = new MailMessage(new MailAddress("accionlaboralhnsps@gmail.com", "Acción Laboral"),
                                             new MailAddress(employee.Email)
                                           );
            m.Subject = "Confirmación de Registro";
            m.Body = string.Format(@"Bienvenido(a) {0}
                                    <BR/>
                                    Usted ahora forma parte de la familia Acción Laboral.
                                    <BR/>
                                    Su usuario es: {1}
                                    <BR/>
                                    Su contraseña temporal es: {2}"
                                  , employee.FirstName
                                  , user.UserName
                                  , user.Password);
                                    //<BR/>
                                    //Clic para activar su cuenta: <a href=\"{1}\" title=\"User Email Confirm\">{1}</a>", user.UserName, Url.Action("ConfirmEmail", "Account", new { Token = employee.EmployeeId, Email = employee.Email }, Request.Url.Scheme));
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Credentials = new NetworkCredential("accionlaboralhnsps@gmail.com", "4ccionl4bor4l");
            smtp.EnableSsl = true;
            smtp.Send(m);
            

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