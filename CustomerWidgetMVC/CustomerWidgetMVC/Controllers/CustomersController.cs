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
using CustomerWidgetMVC.Models;
using Newtonsoft.Json;

namespace CustomerWidgetMVC.Controllers
{
    public class CustomersController : ApiController
    {
        private CustomerWidgetEntities db = new CustomerWidgetEntities();

        // GET: api/Customers
        public IHttpActionResult GetCustomers()
        {
            var customerList = (from customer in db.Customers
                                select new
                                {
                                    customer.CustomerId,
                                    customer.FirstName,
                                    customer.LastName,
                                    customer.Email,
                                    customer.Address

                                }).ToList();

           // var itemObject = JsonConvert.SerializeObject(customerList,
           //Formatting.None,
           //new JsonSerializerSettings()
           //{
           //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
           //});
            return Ok(customerList);
        }

        // GET: api/Customers/5 
        [ResponseType(typeof(Customer))]
        [Route("api/getCustomer")]
        public IHttpActionResult GetCustomer(int id)
        {
            var customerList = (from customer in db.Customers
                                where customer.CustomerId == id
                                select new
                                {
                                    customer.CustomerId,
                                    customer.FirstName,
                                    customer.LastName,
                                    customer.Email,
                                    customer.Address

                                }).ToList();

            if ((customerList == null))
            {
                return NotFound();
            }

       
            return Ok((customerList));
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
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
                if (!CustomerExists(id))
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

        // POST: api/Customers
        [ResponseType(typeof(Customer))]
        [Route("api/CreateCustomer")]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isEmailAlreadyExists = db.Customers.Any(x => x.Email == customer.Email);
            if (isEmailAlreadyExists)
            {
                return Content(HttpStatusCode.BadRequest, "User with this email already exists. Please provide another Email.");
                return NotFound();
            }
            else
            {
                db.Customers.Add(customer);
                db.SaveChanges();
            }

            //return CreatedAtRoute("DefaultApi", new { id = customer.CustomerId }, customer);
            return Content(HttpStatusCode.OK, "Register");

        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
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

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.CustomerId == id) > 0;
        }
    }
}