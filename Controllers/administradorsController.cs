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
using MVCAPI.Models;

namespace MVCAPI.Controllers
{
    public class administradorsController : ApiController
    {
        private base_makingappsEntities db = new base_makingappsEntities();

        // GET: api/administradors
        public IEnumerable<Administradores> Getadministrador()
        {
            var all = (from x in db.administrador
                       select new Administradores
                       {
                           CORREO_ADMINSITRADOR = x.CORREO_ADMINSITRADOR,
                           CONTRASENA_ADMINISTRADOR = x.CONTRASENA_ADMINISTRADOR
                       }).ToList();
            return all;
        }

        // GET: api/administradors/5
        [ResponseType(typeof(administrador))]
        public IHttpActionResult Getadministrador(String email)
        {
            administrador administrador = db.administrador.FirstOrDefault(f => f.CORREO_ADMINSITRADOR.Equals(email));
            if (administrador == null)
            {
                return NotFound();
            }

            Administradores admin = new Administradores();
            admin.CORREO_ADMINSITRADOR = administrador.CORREO_ADMINSITRADOR;
            admin.CONTRASENA_ADMINISTRADOR = administrador.CONTRASENA_ADMINISTRADOR;
            return Ok(admin);
        }

        // PUT: api/administradors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putadministrador(int id, administrador administrador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != administrador.ID_ADMINISTRADOR)
            {
                return BadRequest();
            }

            db.Entry(administrador).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!administradorExists(id))
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

        // POST: api/administradors
        [ResponseType(typeof(administrador))]
        public IHttpActionResult Postadministrador(administrador administrador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.administrador.Add(administrador);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (administradorExists(administrador.ID_ADMINISTRADOR))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = administrador.ID_ADMINISTRADOR }, administrador);
        }

        // DELETE: api/administradors/5
        [ResponseType(typeof(administrador))]
        public IHttpActionResult Deleteadministrador(int id)
        {
            administrador administrador = db.administrador.Find(id);
            if (administrador == null)
            {
                return NotFound();
            }

            db.administrador.Remove(administrador);
            db.SaveChanges();

            return Ok(administrador);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool administradorExists(int id)
        {
            return db.administrador.Count(e => e.ID_ADMINISTRADOR == id) > 0;
        }
    }
}