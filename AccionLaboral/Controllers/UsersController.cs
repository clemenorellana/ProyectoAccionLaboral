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
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Web;

namespace AccionLaboral.Controllers
{
    //[Authorize]
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
        [Authorize]
        public HttpResponseMessage GetUsers()
        {
            var users = db.Users.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        // GET api/UsersFree
        [Route("api/UsersFree")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetUsersFree()
        {
            var users = db.Users.Where(r => r.Busy == false).ToList();
            return Ok(users);
        }

        [Route("api/Users/Login")]
        [HttpPost]
        public EmployeeInfo Login(User user)
        {
            Employee employee = new Employee();
            //if (users.Count == 0)
            //return employee;
            var userId = User.Identity.GetUserId();
            var users = db.Users.ToList();

            employee = db.Employees.Include(r => r.Role).Include(r => r.User).Where(r => r.UserId == userId).ToList()[0];

            EmployeeInfo employeeInfo = new EmployeeInfo
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Birthday = employee.Birthday,
                Age = employee.Age,
                Cellphone = employee.Cellphone,
                HomePhone = employee.HomePhone,
                Address = employee.Address,
                Gender = employee.Gender,
                EmployeeAlias = employee.EmployeeAlias,
                CareerId = employee.CareerId,
                AdmissionDate = employee.AdmissionDate,
                RoleId = employee.RoleId,
                UserId = employee.UserId,
                Photo = employee.Photo,
                User = new UserInfo { UserName = employee.User.UserName, Active = employee.User.Active, Busy = employee.User.Busy },
                Role = new RoleInfo { Name = employee.Role.Name, Alias = employee.Role.Alias },
                Career = employee.Career
            };
            return employeeInfo;
        }


        [Route("api/Users/ValidateUserName")]
        [HttpPost]
        public bool ValidateUserName(User user)
        {
            return UserNameExists(user.UserName);
        }

        [Route("api/Users/RequestChangePassword")]
        [HttpPost]
        public bool RequestChangePassword(User user)
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
        [Authorize]
        public IHttpActionResult GetUser(string id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT api/Users/5
        [Authorize]
        public IHttpActionResult PutUser(string id, User user)
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
        public IHttpActionResult PostUser(User user)
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
        /* [ResponseType(typeof(User))]
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
         }*/

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

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion

    }
}