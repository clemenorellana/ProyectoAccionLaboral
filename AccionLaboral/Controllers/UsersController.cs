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
using System.Threading.Tasks;
using System.Net.Mail;

namespace AccionLaboral.Controllers
{
    public class UsersController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        // GET api/Users
        [Route("api/Users")]
        [HttpGet]
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET api/UsersFree
        [Route("api/UsersFree")]
        [HttpGet]
        public IQueryable<User> GetUsersFree()
        {
            return db.Users.Where(r => r.Busy == false);
        }

        [Route("api/Users/Login")]
        [HttpPost]
        public Employee Login(User user)
        {
            var users = db.Users.Where(r => r.UserName == user.UserName && r.Password == user.Password).ToList();
            Employee employee = new Employee();
            if (users.Count == 0)
                return employee;
            

            var userId = users[0].UserId;

            employee = db.Employees.Include(r => r.Role).Include(r => r.Role.Privileges).Where(r => r.UserId == userId).ToList()[0];

            return employee;

            //return users.Count > 0;
        }


        [Route("api/Users/RequestChangePassword")]
        [HttpPost]
        public bool RequestChangePassword(User user)
        {
            var users = db.Users.Where(r => r.UserName == user.UserName).ToList();

            if (users.Count == 0)
                return false;

            var userId = users[0].UserId;

            Employee employee = db.Employees.Where(r => r.UserId == userId).ToList()[0];


            MailMessage m = new MailMessage(new MailAddress("accionlaboralhnsps@gmail.com", "Acción Laboral"),
                                             new MailAddress(employee.Email)
                                           );
            m.Subject = "Cambiar Contraseña";
            m.Body = string.Format(@"Estimado(a) {0}
                                    <BR/>
                                    Se ha solicitado un cambio de contraseña.
                                    <BR/>
                                    Su usuario es: {1}
                                    <BR/>
                                    Favor de clic al siguiente link para cambiar su contraseña"
                                  , employee.FirstName
                                  , employee.User.UserName
                                  );

//            m.Body = string.Format(@"Dear {0}
//<BR/>
//Thank you for your registration, please click on the below link to complete your registration: <a href=\"{1}\" title=\"User Email Confirm\">{1}</a>"
//            , user.UserName
//            , Url.Action("ConfirmEmail", "Account", new { Token = user.UserId, Email = employee.Email }, Request.Url.Scheme));


            //<BR/>
            //Clic para activar su cuenta: <a href=\"{1}\" title=\"User Email Confirm\">{1}</a>", user.UserName, Url.Action("ConfirmEmail", "Account", new { Token = employee.EmployeeId, Email = employee.Email }, Request.Url.Scheme));
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Credentials = new NetworkCredential("accionlaboralhnsps@gmail.com", "4ccionl4bor4l");
            smtp.EnableSsl = true;
            smtp.Send(m);

            return users.Count > 0;
        }


        // GET api/Users/5
        [Route("api/Users")]
        [HttpGet]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT api/Users/5
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST api/Users
        [Route("api/Users")]
        [HttpPost]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        // DELETE api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}