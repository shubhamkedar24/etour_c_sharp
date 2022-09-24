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
using indiatour_webapi_master.Models;

namespace indiatour_webapi_master.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class itinerariesController : ApiController
    {
        private ModelData db = new ModelData();

        // GET: api/itineraries
        public IQueryable<itinerary> Getitineraries()
        {
            return db.itineraries;
        }


        // this method will return iternaries details 
        // based on sectormaster id
        // this request is for detail page
        // GET: api/itineraries/5
        [ResponseType(typeof(itinerary))]
        public IHttpActionResult Getitinerary(int id)
        {
            itinerary itinerary = db.itineraries.
                Where(x => x.SectorMaster_Id == id).
                FirstOrDefault();
            if (itinerary == null)
            {
                return NotFound();
            }

            var obj = db.itineraries.Where(x => x.SectorMaster_Id == id).
               Select(x =>
               new
               {
                   day = x.Day,
                   description = x.Description
               });

            return Ok(obj);
        }

        // PUT: api/itineraries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putitinerary(int id, itinerary itinerary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itinerary.Iternery_Id)
            {
                return BadRequest();
            }

            db.Entry(itinerary).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!itineraryExists(id))
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

        // POST: api/itineraries
        [ResponseType(typeof(itinerary))]
        public IHttpActionResult Postitinerary(itinerary itinerary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.itineraries.Add(itinerary);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = itinerary.Iternery_Id }, itinerary);
        }

        // DELETE: api/itineraries/5
        [ResponseType(typeof(itinerary))]
        public IHttpActionResult Deleteitinerary(int id)
        {
            itinerary itinerary = db.itineraries.Find(id);
            if (itinerary == null)
            {
                return NotFound();
            }

            db.itineraries.Remove(itinerary);
            db.SaveChanges();

            return Ok(itinerary);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool itineraryExists(int id)
        {
            return db.itineraries.Count(e => e.Iternery_Id == id) > 0;
        }
    }
}