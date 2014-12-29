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

        [Route("api/Users/Login")]
        [HttpPost]
        public bool Login(User user)
        {
            var users = db.Users.Where(r => r.UserName == user.UserName && r.Password == user.Password).ToList();
            return users.Count > 0;
        }
        /*
        // GET api/Users/Login
        [Route("api/Users/Login")]
        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<System.Web.Mvc.ActionResult> Login(User model)
        {
            var user = model;
            if (ModelState.IsValid)
            {
                user = db.Users.First(r=> r.UserName == model.UserName);
                user = await db.Users.SingleOrDefaultAsync(r=> r.UserName == model.UserName && r.Password == model.Password);
                if (user != null)
                {
                    //await SignInAsync(user, model.RememberMe);
                    //return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError("", "Invalid username or password.");
            }

            // If we got this far, something failed, redisplay form
            return new System.Web.Mvc.FilePathResult("/Views/Home/Index.html", "text/html");

            //var result = await db.Users.FindAsync(user.UserName, user.Password);
            //if (result.UserId > 0)
            //    return true;
            //else
            //    return false;
        }*/

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