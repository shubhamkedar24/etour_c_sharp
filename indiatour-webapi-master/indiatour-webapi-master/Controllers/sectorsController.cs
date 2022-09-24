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
    public class sectorsController : ApiController
    {
        private ModelData db = new ModelData();

        // getting data of all sectors where subsectors doesnt exists
        // so this will return all sectors whose subsector is null
        // this means that this will display main category
        //public IQueryable<sector> Getsectors()
        // GET: api/sectors

        public IQueryable Getsectors()
        {
            //return db.sectors;
            return db.sectors.Where(x => x.Subsector_Id == null).
                Select(x => new
                {
                    flag=x.Flag,
                    sectormasterid = x.Sectormaster_Id,
                    imgpath = x.Imgpath,
                    name = x.Sectorname,
                    sectorid = x.Sector_Id,
                });
        }

        // GET: api/sectors/5
        [ResponseType(typeof(sector))]
        public IHttpActionResult Getsector(int id)
        {
            sector sector = db.sectors.Find(id);
            if (sector == null)
            {
                return NotFound();
            }

            return Ok(sector);
        }

        // PUT: api/sectors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putsector(int id, sector sector)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sector.Sectormaster_Id)
            {
                return BadRequest();
            }

            db.Entry(sector).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!sectorExists(id))
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

        // POST: api/sectors
        [ResponseType(typeof(sector))]
        public IHttpActionResult Postsector(sector sector)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sectors.Add(sector);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sector.Sectormaster_Id }, sector);
        }

        // DELETE: api/sectors/5
        [ResponseType(typeof(sector))]
        public IHttpActionResult Deletesector(int id)
        {
            sector sector = db.sectors.Find(id);
            if (sector == null)
            {
                return NotFound();
            }

            db.sectors.Remove(sector);
            db.SaveChanges();

            return Ok(sector);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool sectorExists(int id)
        {
            return db.sectors.Count(e => e.Sectormaster_Id == id) > 0;
        }


        // getting data of all sectors where subsectors equals selected sector
        // so this will return all sectors whose subsector is selected sector
        // ex this will get id of all sectors where you selected UAE 
        // selected id is UAE
        // this means that this will display main category
        [Route("api/subsectors/{id}")]
        [HttpGet]
        public IQueryable GetsectorsById([FromUri]string id)
        {
            //return db.sectors;
            return db.sectors.Where(x => x.Subsector_Id.Equals(id)).
                Select(x => new
                {
                    sectormasterid = x.Sectormaster_Id,
                    imgpath = x.Imgpath,
                    name = x.Sectorname,
                    sectorid = x.Sector_Id,
                    flag = x.Flag
                });
        }
    }
}