using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using LabModulo03WebApi.Models;

namespace LabModulo03WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VehiculoesController : ApiController
    {
        private Concesionario20Entities db = new Concesionario20Entities();

        // GET: api/Vehiculoes
        public IQueryable<Vehiculo> GetVehiculo()
        {
            return db.Vehiculo;
        }

        // GET: api/Vehiculoes/5
        [ResponseType(typeof(Vehiculo))]
        public IHttpActionResult GetVehiculo(int id)
        {
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return Ok(vehiculo);
        }

        // PUT: api/Vehiculoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVehiculo(int id, Vehiculo vehiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehiculo.IdVehiculo)
            {
                return BadRequest();
            }

            db.Entry(vehiculo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiculoExists(id))
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

        // POST: api/Vehiculoes
        [ResponseType(typeof(Vehiculo))]
        public IHttpActionResult PostVehiculo(Vehiculo vehiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vehiculo.Add(vehiculo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vehiculo.IdVehiculo }, vehiculo);
        }

        // DELETE: api/Vehiculoes/5
        [ResponseType(typeof(Vehiculo))]
        public IHttpActionResult DeleteVehiculo(int id)
        {
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            db.Vehiculo.Remove(vehiculo);
            db.SaveChanges();

            return Ok(vehiculo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VehiculoExists(int id)
        {
            return db.Vehiculo.Count(e => e.IdVehiculo == id) > 0;
        }
    }
}