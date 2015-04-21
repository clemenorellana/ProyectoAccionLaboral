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
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AccionLaboral.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        private AccionLaboralContext db = new AccionLaboralContext();

        public UsersController()
        {
            db.Database.CommandTimeout = 180;
        }

        // GET api/Users
        [Route("api/Users")]
        [HttpGet]
        public IQueryable<IdentityUser> GetUsers()
        {
            return db.Users;
        }

        // GET api/UsersFree
        [Route("api/UsersFree")]
        [HttpGet]
        public IQueryable<IdentityUser> GetUsersFree()
        {
            return db.Users.Where(r => r.Busy == false);
        }

        [Route("api/Users/Login")]
        [HttpPost]
        public Employee Login(IdentityUser user)
        {
            user.PasswordHash = user.PasswordHash;
            var users = db.Users.Where(r => r.UserName == user.UserName && r.PasswordHash == user.PasswordHash).ToList();
            Employee employee = new Employee();
            if (users.Count == 0)
                return employee;
            

            var userId = users[0].Id;

            employee = db.Employees.Include(r => r.Role).Where(r => r.UserId == userId).ToList()[0];

            return employee;
        }


        [Route("api/Users/ValidateUserName")]
        [HttpPost]
        public bool ValidateUserName(IdentityUser user)
        {
            return UserNameExists(user.UserName);
        }

        /*[Route("api/Users/ChangePassword")]
        [HttpPut]
        public IHttpActionResult ChangePassword(IdentityUser user)
        {
            user.PasswordHash = Encryptdata(user.PasswordHash);

            var userToUpdate = db.Users.Where(r => r.UserName == user.UserName).ToList();
            userToUpdate[0].PasswordHash = user.PasswordHash;

            user = userToUpdate[0];

            return PutUser(user.Id, user);
        }*/


        [Route("api/Users/RequestChangePassword")]
        [HttpPost]
        public bool RequestChangePassword(IdentityUser user)
        {
            var users = db.Users.Where(r => r.UserName == user.UserName).ToList();

            if (users.Count == 0)
                return false;

            var userId = users[0].Id;

            Employee employee = db.Employees.Where(r => r.UserId == userId).ToList()[0];


            MailMessage m = new MailMessage(new MailAddress("accionlaboralhnsps@gmail.com", "Acción Laboral"),
                                             new MailAddress(employee.Email)
                                           );
            m.Subject = "Cambiar Contraseña";

            string uri = this.Url.Link("Default", new { controller = "User", action = "ResetPassword" });
            uri = uri.Replace("User", "#");

            m.Body = string.Format(@"Estimado(a) {0}
                                    <BR/>
                                    Se ha solicitado un cambio de contraseña.
                                    <BR/>
                                    Su usuario es: {1}
                                    <BR/>
                                    <a href={2}>De clic aquí para activar su cuenta</a>"
                                  , employee.FirstName
                                  , employee.User.UserName
                                  , uri
                                  );

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
        public IHttpActionResult GetUser(string id)
        {
            IdentityUser user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT api/Users/5
        public IHttpActionResult PutUser(string id, IdentityUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
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
        /*[Route("api/Users")]
        [HttpPost]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(IdentityUser user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                user.PasswordHash = user.PasswordHash; //Encryptdata(user.Password);

                db.Users.Add(user);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { controller = "users", id = user.UserId }, user);
            }
            catch (Exception e)
            {
                var x = e.Message;
            }
            return CreatedAtRoute("DefaultApi", new { controller = "users", id = user.UserId }, user);
        }*/

        // DELETE api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
        {
            ApplicationUser user = db.Users.Find(id);
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

        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }

        private bool UserNameExists(string userName)
        {
            return db.Users.Count(e => e.UserName == userName) > 0;
        }

        public static string Encryptdata(string data)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(data);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    data = Convert.ToBase64String(ms.ToArray());
                }
            }
            return data;
        }

    }
}