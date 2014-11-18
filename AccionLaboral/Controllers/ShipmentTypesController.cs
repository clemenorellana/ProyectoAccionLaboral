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
using Proyecto_AccionLaboral.Models;

namespace Proyecto_AccionLaboral.Controllers
{
    public class ShipmentTypesController : ApiController
    {
        private Proyecto_AccionLaboralContext db = new Proyecto_AccionLaboralContext();

        // GET api/ShipmentTypes
        public IQueryable<ShipmentType> GetShipmentTypes()
        {
            return db.ShipmentTypes;
        }

        // GET api/ShipmentTypes/5
        [ResponseType(typeof(ShipmentType))]
        public IHttpActionResult GetShipmentType(int id)
        {
            ShipmentType shipmenttype = db.ShipmentTypes.Find(id);
            if (shipmenttype == null)
            {
                return NotFound();
            }

            return Ok(shipmenttype);
        }

        // PUT api/ShipmentTypes/5
        public IHttpActionResult PutShipmentType(int id, ShipmentType shipmenttype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shipmenttype.ShipmentTypeId)
            {
                return BadRequest();
            }

            db.Entry(shipmenttype).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipmentTypeExists(id))
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

        // POST api/ShipmentTypes
        [ResponseType(typeof(ShipmentType))]
        public IHttpActionResult PostShipmentType(ShipmentType shipmenttype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShipmentTypes.Add(shipmenttype);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shipmenttype.ShipmentTypeId }, shipmenttype);
        }

        // DELETE api/ShipmentTypes/5
        [ResponseType(typeof(ShipmentType))]
        public IHttpActionResult DeleteShipmentType(int id)
        {
            ShipmentType shipmenttype = db.ShipmentTypes.Find(id);
            if (shipmenttype == null)
            {
                return NotFound();
            }

            db.ShipmentTypes.Remove(shipmenttype);
            db.SaveChanges();

            return Ok(shipmenttype);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShipmentTypeExists(int id)
        {
            return db.ShipmentTypes.Count(e => e.ShipmentTypeId == id) > 0;
        }
    }
}