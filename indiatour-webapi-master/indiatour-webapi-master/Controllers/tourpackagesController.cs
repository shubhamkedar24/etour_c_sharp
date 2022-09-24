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
    public class tourpackagesController : ApiController
    {
        private ModelData db = new ModelData();

        // this will get data required by front end to display of all packages
        // GET: api/tourpackages
        //public IQueryable<tourpackage> Gettourpackages()
        public IQueryable Gettourpackages()
        {
            //return db.tourpackages;
            return db.tourpackages.
                 Select(x =>
               new
               {
                   //packageid = x.Package_Id,
                   packageid = x.Sectormaster_Id,
                   sectormasterid = x.Sectormaster_Id,
                   packagename = x.Package_Name,
                   startdate = x.Startdate,
                   enddate = x.Enddate,
                   tourdate = x.Tourdates
                   //startdate = x.Startdate.ToString().Substring(0, 9),
                   //enddate = x.Enddate.ToString().Substring(0,9),
                   //tourdate = x.Tourdates.ToString().Substring(0, 9)
               });
        }

        // this will get data required by front end to display of particular package
        // GET: api/tourpackages/5
        [ResponseType(typeof(tourpackage))]
        public IHttpActionResult Gettourpackage(int id)
        {
            tourpackage tourpackage = db.tourpackages.
                Where(x => x.Sectormaster_Id == id).
                FirstOrDefault();

            if (tourpackage == null)
            {
                return NotFound();
            }
            var obj = db.tourpackages.Where(x => x.Sectormaster_Id == id).
               Select(x =>
               new
               {
                   //packageid = x.Package_Id,
                   packageid = x.Sectormaster_Id,
                   sectormasterid = x.Sectormaster_Id,
                   packagename = x.Package_Name,
                   startdate = x.Startdate,
                   enddate = x.Enddate,
                   tourdate = x.Tourdates
                   //startdate = x.Startdate.ToString().Substring(0, 9),
                   //enddate = x.Enddate.ToString().Substring(0, 9),
                   //tourdate = x.Tourdates.ToString().Substring(0, 9)
               });

            return Ok(obj);
        }

        // PUT: api/tourpackages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttourpackage(int id, tourpackage tourpackage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tourpackage.Package_Id)
            {
                return BadRequest();
            }

            db.Entry(tourpackage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tourpackageExists(id))
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

        // POST: api/tourpackages
        [ResponseType(typeof(tourpackage))]
        public IHttpActionResult Posttourpackage(tourpackage tourpackage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tourpackages.Add(tourpackage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tourpackage.Package_Id }, tourpackage);
        }

        // DELETE: api/tourpackages/5
        [ResponseType(typeof(tourpackage))]
        public IHttpActionResult Deletetourpackage(int id)
        {
            tourpackage tourpackage = db.tourpackages.Find(id);
            if (tourpackage == null)
            {
                return NotFound();
            }

            db.tourpackages.Remove(tourpackage);
            db.SaveChanges();

            return Ok(tourpackage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tourpackageExists(int id)
        {
            return db.tourpackages.Count(e => e.Package_Id == id) > 0;
        }
    }
}