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
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class customersController : ApiController
    {
        private ModelData db = new ModelData();
        private roleData rb = new roleData();

        // GET: api/customers
        public IQueryable Getcustomers()
        //public IQueryable<customer> Getcustomers()
        {
            //return db.customers;
            return db.customers.Select(x=> new
            {
                firstname = x.FirstName,
                lastname = x.LastName,
                email = x.Email
            });
        }


        // this method will just return necessecary information of customers
        // this request is for profile page api
        // GET: api/customers/5
        //[ResponseType(typeof(customer))]
        public IHttpActionResult Getcustomer(int id)
        {
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            var obj = db.customers.Where(x => x.Cust_Id == id).
                Select(x => new {
                firstname = customer.FirstName,
                lastname = customer.LastName,
                email = customer.Email
            });

            return Ok(obj);
        }

        // PUT: api/customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcustomer(int id, customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Cust_Id)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!customerExists(id))
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


        // method to register a new customer post request
        // here the customer is added to customer table
        // and also mapped customer table with role table 
        // every signed up customer will get role user 
        // and the user role table will also be inserted data as per role user
        [Route("api/auth/signup", Name = "signup")]
        [HttpPost]
        [ResponseType(typeof(customer))]
        public IHttpActionResult Postcustomer(customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.customers.Add(customer);
            db.SaveChanges();
            user_roles user=new user_roles();
            user.user_id = customer.Cust_Id;
            user.role_id = 1;
            rb.user_roles.Add(user);
            rb.SaveChanges();

            return CreatedAtRoute("signup", new { id = customer.Cust_Id }, customer);
        }

        // DELETE: api/customers/5
        [ResponseType(typeof(customer))]
        public IHttpActionResult Deletecustomer(int id)
        {
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.customers.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool customerExists(int id)
        {
            return db.customers.Count(e => e.Cust_Id == id) > 0;
        }

        // this will return login model and important information
        // necessecary for a user to login 
        // this is done by checking email and password and
        // getting a role associated with that credentials and return that role object to front end 
        // to authenticate
        //[Route("api/auth/login", Name = "login")]
        [Route("api/auth/login")]
        [HttpPost]
        //[ResponseType(typeof(customer))]
        public IHttpActionResult PostLogin([FromBody] login model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Credentials");
                //return Ok(new { message="invalid user" });
            }


            customer customer = db.customers.
                Where(x => (x.Email.Equals(model.email) &&
                x.Password.Equals(model.password))).
                FirstOrDefault();

            if (customer != null)
            {
                string msg = "login successful";

                int userId = (rb.user_roles.Where(x => x.user_id == customer.Cust_Id).
                    FirstOrDefault()).role_id;

                role role = db.roles.Where(x => x.id == userId).FirstOrDefault();

                var obj = new
                {
                    email = customer.Email,
                    password = customer.Password,
                    cust_Id = customer.Cust_Id,
                    accessToken = true,
                    //roles = customer.roles.FirstOrDefault().name
                    roles = role.name,
                    message = msg
                };
                return Ok(obj);
            }
            else
            {
                return BadRequest("Bad Credentials");
            }
        }


        //[Route("api/auth/login")]
        //[HttpGet]
        ////[ResponseType(typeof(customer))]
        //public IHttpActionResult GetLogin(login model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //        //return Ok(new { message = "invalid user" });
        //    }

        //    return Ok(new { message = "invalid user" });
        //}
    }
}