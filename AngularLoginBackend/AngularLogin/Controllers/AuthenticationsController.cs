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
using AngularLogin.Data;
using AngularLogin.Models;

namespace AngularLogin.Controllers
{
    [RoutePrefix("Authentications")]
    public class AuthenticationsController : ApiController
    {
        private AngularLoginContext db = new AngularLoginContext();

        // GET: /Authentications
        public IQueryable<Authentication> GetSignUps()
        {
            return db.SignUps;
        }

        // GET: /Authentications/5
        [ResponseType(typeof(Authentication))]
        public IHttpActionResult GetAuthentication(string id)
        {
            Authentication authentication = db.SignUps.Find(id);
            if (authentication == null)
            {
                return NotFound();
            }

            return Ok(authentication);
        }
        [ResponseType(typeof(IAsyncResult))]
        [HttpPost]
        [Route("{SignIn}")]
        public IHttpActionResult SignIn(Authentication User)
        {
            Authentication user = db.SignUps.Find(User.userName);
            if (user == null)
            {
                return NotFound();
            }
            else if (user.password == user.password)
            {
                return Ok(user);
            }
            return BadRequest();
        }

        // PUT: /Authentications/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuthentication(string id, Authentication authentication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != authentication.userName)
            {
                return BadRequest();
            }

            db.Entry(authentication).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthenticationExists(id))
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

        // POST: /Authentications
        [ResponseType(typeof(Authentication))]
        public IHttpActionResult PostAuthentication(Authentication authentication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SignUps.Add(authentication);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AuthenticationExists(authentication.userName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = authentication.userName }, authentication);
        }

        // DELETE: /Authentications/5
        [ResponseType(typeof(Authentication))]
        public IHttpActionResult DeleteAuthentication(string id)
        {
            Authentication authentication = db.SignUps.Find(id);
            if (authentication == null)
            {
                return NotFound();
            }

            db.SignUps.Remove(authentication);
            db.SaveChanges();

            return Ok(authentication);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuthenticationExists(string id)
        {
            return db.SignUps.Count(e => e.userName == id) > 0;
        }
    }
}