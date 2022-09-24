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
    public class bookingsController : ApiController
    {
        private ModelData db = new ModelData();

        // GET: api/bookings
        public IQueryable<booking> Getbookings()
        {
            return db.bookings;
        }

        // GET: api/bookings/5
        [ResponseType(typeof(booking))]
        public IHttpActionResult Getbooking(int id)
        {
            booking booking = db.bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        // PUT: api/bookings/5
        // predefined methods
        [ResponseType(typeof(void))]
        public IHttpActionResult Putbooking(int id, booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != booking.Booking_Id)
            {
                return BadRequest();
            }

            db.Entry(booking).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bookingExists(id))
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

        // POST: api/bookings
        // post request of a booking 
        [ResponseType(typeof(booking))]
        public IHttpActionResult Postbooking(booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // setting flag to zero which means booking is active
            // and setting flag to one will make it eligible to delete
            booking.Flag = 0;
            db.bookings.Add(booking);
            db.SaveChanges();

            // getting customer emait to perform email operation
            string customeremail = (db.customers.
                Where(x => x.Cust_Id == booking.Cust_Id).FirstOrDefault()).
                Email;

            // getting particular package name
            string packageName = (db.tourpackages.Where
                (pack => pack.Sectormaster_Id == booking.Package_Id).
                FirstOrDefault()).Package_Name;
            
            // calling email method to send particular customer email of his bill 
            EmailTo.sendmail(customeremail, booking, packageName);

            return CreatedAtRoute("DefaultApi", new { id = booking.Booking_Id }, booking);
        }

        // DELETE: api/bookings/5
        // this will delete particular booking which is been sent by admin to delete
        [ResponseType(typeof(booking))]
        public IHttpActionResult Deletebooking(int id)
        {
            booking booking = db.bookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }

            db.bookings.Remove(booking);
            db.SaveChanges();

            return Ok(booking);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool bookingExists(int id)
        {
            return db.bookings.Count(e => e.Booking_Id == id) > 0;
        }

        // method to get package on basic of customer id
        // this method will return booking associated with a specific customer id
        [Route("api/booking/{cid}")]
        [HttpGet]
        public IQueryable GetbookingByCustomer([FromUri]int cid)
        {
            return db.bookings.Where(x => x.Cust_Id == cid).
                Select(x => new
                {
                    //packagename = (db.tourpackages.Where(pack => pack.Package_Id == x.Package_Id).
                    //FirstOrDefault()).Package_Name,
                    packageid = x.Package_Id,
                    bookingid = x.Booking_Id,
                    totalpassanger = x.Passangers,
                    bookingdate = x.Bookingdate,
                    totalfinalcost = x.Totalamount
                });
        }


        // this is a put request which will set the flag to false 
        // as soon as customer requests for package cancellation
        // this will turn the flag of model state to false
        [Route("api/cancelBooking/{cid}")]
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult PutbookingOnDelete([FromUri] int cid, booking booking)
        {
            try
            {
                booking obj = db.bookings.Find(cid);
                if (obj != null)
                {
                    obj.Flag = 1;
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bookingExists(cid))
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



        // to show bookings that has been requested to cancelation
        //for admin, to delete tours
        [Route("api/bookingToDelete")]
        [HttpGet]
        public IQueryable DeleteRequests()
        {
            return db.bookings.Where(x => x.Flag == 1).
                Select(x => new
                {
                    //packagename = (db.tourpackages.Where(pack => pack.Package_Id == x.Package_Id).
                    //FirstOrDefault()).Package_Name,
                    packageid = x.Package_Id,
                    bookingid = x.Booking_Id,
                    totalpassanger = x.Passangers,
                    bookingdate = x.Bookingdate,
                    totalfinalcost = x.Totalamount
                });
        }
    }
}