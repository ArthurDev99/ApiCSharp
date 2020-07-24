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
    public class areasController : ApiController
    {
        private base_makingappsEntities db = new base_makingappsEntities();

        // GET: api/areas
        public IEnumerable<AreasObject> Getarea()
        {
            var all = (from x in db.area
                       select new AreasObject
                       {
                           ID_AREA = x.ID_AREA,
                           NOMBRE_AREA = x.NOMBRE_AREA
                       }).ToList();
            return all;
        }

        // GET: api/areas/5
        [ResponseType(typeof(area))]
        public IHttpActionResult Getarea(int id)
        {
            area area = db.area.Find(id);
            if (area == null)
            {
                return NotFound();
            }

            AreasObject ar = new AreasObject();
            ar.ID_AREA = area.ID_AREA;
            ar.NOMBRE_AREA = area.NOMBRE_AREA;

            return Ok(ar);
        }

        // PUT: api/areas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putarea(int id, area area)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != area.ID_AREA)
            {
                return BadRequest();
            }

            db.Entry(area).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!areaExists(id))
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

        // POST: api/areas
        [ResponseType(typeof(area))]
        public IHttpActionResult Postarea(area area)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.area.Add(area);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (areaExists(area.ID_AREA))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = area.ID_AREA }, area);
        }

        // DELETE: api/areas/5
        [ResponseType(typeof(area))]
        public IHttpActionResult Deletearea(int id)
        {
            area area = db.area.Find(id);
            if (area == null)
            {
                return NotFound();
            }

            db.area.Remove(area);
            db.SaveChanges();

            return Ok(area);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool areaExists(int id)
        {
            return db.area.Count(e => e.ID_AREA == id) > 0;
        }
    }
}