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
    public class passangersController : ApiController
    {
        private ModelData db = new ModelData();

        // GET: api/passangers
        public IQueryable<passanger> Getpassangers()
        {
            return db.passangers;
        }

        // GET: api/passangers/5
        //[ResponseType(typeof(passanger))]
        //public IHttpActionResult Getpassanger(int id)
        //{
        //    passanger passanger = db.passangers.Find(id);
        //    if (passanger == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(passanger);
        //}

        // PUT: api/passangers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpassanger(int id, passanger passanger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != passanger.Pass_Id)
            {
                return BadRequest();
            }

            db.Entry(passanger).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!passangerExists(id))
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


        // this post will add passanger to passanger table
        // it will take the package id customerid from url 
        // and save those information to passanger and save it to object
        // POST: api/passangers
        [Route("api/passangers/{pkid}/{cid}", Name ="postPassanger")]
        [HttpPost]
        [ResponseType(typeof(passanger))]
        //public IHttpActionResult Postpassanger(passanger passanger)
        public IHttpActionResult Postpassanger(passanger passanger, [FromUri] int pkid, [FromUri] int cid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // wrote extra
            passanger.Package_Id = pkid;
            passanger.Cust_Id = cid;
            passanger.customer_cust_id = cid;
            // wrote extra

            db.passangers.Add(passanger);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (passangerExists(passanger.Pass_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtRoute("DefaultApi", new { id = passanger.Pass_Id }, passanger);
            return CreatedAtRoute("postPassanger", new { id = passanger.Pass_Id }, passanger);
        }

        // deleting passanger based on customer id 
        // DELETE: api/passangers/5
        [Route("api/deletePassanger/{cid}")]
        [ResponseType(typeof(passanger))]
        [HttpDelete]
        public IHttpActionResult Deletepassanger([FromUri] int cid)
        {
            passanger passanger = db.passangers.Find(cid);
            if (passanger == null)
            {
                return NotFound();
            }

            db.passangers.Remove(passanger);
            db.SaveChanges();

            return Ok(passanger);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool passangerExists(int id)
        {
            return db.passangers.Count(e => e.Pass_Id == id) > 0;
        }

        // GET: api/passangers/5
        //[ResponseType(typeof(passanger))]
        [Route("api/passangers/{cid}")]
        [HttpGet]
        public IQueryable GetpassangerByCustId([FromUri] int cid)
        {
            //passanger passanger = db.passangers.Find(id);
            IQueryable passanger = db.passangers.Where(x => x.customer_cust_id == cid).
                Select(x => new
                {
                    passangerid = x.Pass_Id,
                    firstname = x.Firstname,
                    lastname = x.Lastname,
                    gender = x.Gender,
                    dob = x.DOB,
                    cost = x.cost
                });
         
            return passanger;
        }

        [Route("api/passangersCost/{cid}")]
        [HttpGet]
        public IQueryable GetCostByCustId([FromUri] int cid)
        {
            //passanger passanger = db.passangers.Find(id);
            IQueryable cost = db.passangers.Where(x => x.customer_cust_id == cid).
                Select(x => new
                {
                    //passangerid = x.Pass_Id,
                    //firstname = x.Firstname,
                    //lastname = x.Lastname,
                    //gender = x.Gender,
                    //dob = x.DOB,
                    cost = x.cost
                });

            return cost;
        }

        [Route("api/deleteAllPassanger/{cid}")]
        [ResponseType(typeof(passanger))]
        [HttpDelete]
        public IHttpActionResult DeleteMultiplePassanger([FromUri] int cid)
        {
            var passanger = db.passangers.Where(x => x.customer_cust_id == cid);
            if (passanger == null)
            {
                return NotFound();
            }

            db.passangers.RemoveRange(passanger);
            db.SaveChanges();

            return Ok(passanger);
        }
    }
}