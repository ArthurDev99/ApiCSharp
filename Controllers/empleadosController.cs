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
    public class empleadosController : ApiController
    {
        private base_makingappsEntities db = new base_makingappsEntities();

        // GET: api/empleados
        public IEnumerable<Empleados> Getempleado()
        {
            var all = (from x in db.empleado select new Empleados { ID_EMPLEADO = x.ID_EMPLEADO, NOMBRE_EMPLEADO = x.NOMBRE_EMPLEADO,
                APELLIDOS_EMPLEADO = x.APELLIDOS_EMPLEADO, CORREO_EMPLEADO = x.CORREO_EMPLEADO, DIRECCION_EMPLEADO = x.DIRECCION_EMPLEADO, 
                TELEFONO_EMPLEADO=x.TELEFONO_EMPLEADO}).ToList();
            return all;
        }

        // GET: api/empleados/5
        [ResponseType(typeof(empleado))]
        public IHttpActionResult Getempleado(int id)
        {
            empleado empleado = db.empleado.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }

            return Ok(empleado);
        }

        // PUT: api/empleados/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putempleado(int id, empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empleado.ID_EMPLEADO)
            {
                return BadRequest();
            }

            db.Entry(empleado).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!empleadoExists(id))
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

        // POST: api/empleados
        [ResponseType(typeof(empleado))]
        public IHttpActionResult Postempleado(empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.empleado.Add(empleado);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (empleadoExists(empleado.ID_EMPLEADO))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = empleado.ID_EMPLEADO }, empleado);
        }

        // DELETE: api/empleados/5
        [ResponseType(typeof(empleado))]
        public IHttpActionResult Deleteempleado(int id)
        {
            empleado empleado = db.empleado.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }

            db.empleado.Remove(empleado);
            db.SaveChanges();

            return Ok(empleado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool empleadoExists(int id)
        {
            return db.empleado.Count(e => e.ID_EMPLEADO == id) > 0;
        }
    }
}