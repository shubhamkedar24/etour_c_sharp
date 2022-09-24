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
    public class costsController : ApiController
    {
        private ModelData db = new ModelData();

        // GET: api/costs
        public IQueryable<cost> Getcosts()
        {
            return db.costs;
        }


        // this will return overall costs of a particular package
        // based on a particular id that is sectormaster id
        // GET: api/costs/5
        [ResponseType(typeof(cost))]
        public IHttpActionResult Getcost(int id)
        {
            cost cost = db.costs.Where(x => x.Sectormaster_Id == id).FirstOrDefault();
          
            if (cost == null)
            {
                return NotFound();
            }

            var obj = db.costs.Where(x => x.Sectormaster_Id == id).
                Select(x =>
                new
                {
                    singleoccupancy = x.Singleoccupancy,
                    twinperson = x.Twinperson,
                    tripplesharing = x.Triplesharing,
                    childwithparent = x.Childwithparents,
                    childwithoutparents = x.Childwithoutparents
                });
            return Ok(obj);
        }

        // PUT: api/costs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcost(int id, cost cost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cost.Cost_Id)
            {
                return BadRequest();
            }

            db.Entry(cost).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!costExists(id))
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

        // POST: api/costs
        [ResponseType(typeof(cost))]
        public IHttpActionResult Postcost(cost cost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.costs.Add(cost);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cost.Cost_Id }, cost);
        }

        // DELETE: api/costs/5
        [ResponseType(typeof(cost))]
        public IHttpActionResult Deletecost(int id)
        {
            cost cost = db.costs.Find(id);
            if (cost == null)
            {
                return NotFound();
            }

            db.costs.Remove(cost);
            db.SaveChanges();

            return Ok(cost);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool costExists(int id)
        {
            return db.costs.Count(e => e.Cost_Id == id) > 0;
        }

        // this will return singleoccupance cost of packages
        // this will return all package singleoccupancy costs and
        // all information to display
        // here tourpackage is joined with cost table
        // on sectormasterid
        [Route("api/findcost", Name = "cost")]
        [ResponseType(typeof(customer))]
        [HttpGet]
        public IQueryable FindCost()
        {
            var cos = from tour in db.tourpackages
                      join cost in db.costs
                      on tour.Sectormaster_Id equals cost.Sectormaster_Id
                      select new
                      {

                          sectormasterid = tour.Sectormaster_Id,
                          packagename = tour.Package_Name,
                          startdate = tour.Startdate,
                          enddate = tour.Enddate,
                          tourdate = tour.Tourdates,
                          singleoccupancy =cost.Singleoccupancy
                      };

            return cos;
        }

        // this method will return array of prices of a particular package sector
        // for example if subsector id is uae it will return all uae package costs
        [Route("api/findcostbysubsector/{ssid}")]
        [ResponseType(typeof(customer))]
        [HttpGet]
        public IQueryable FindCostBySubSectorId([FromUri] string ssid)
        {
            var cos = from sector in db.sectors
                      join cost in db.costs
                      on sector.Sectormaster_Id equals cost.Sectormaster_Id
                      where sector.Subsector_Id == ssid
                      select new
                      {
                          singleoccupancy = cost.Singleoccupancy
                      };

            return cos;
        }

    }
}