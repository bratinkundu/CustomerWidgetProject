using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using CustomerWidgetMVC.Models;

namespace CustomerWidgetMVC.Controllers
{
   
    public class LoginApiController : ApiController
    {
        private CustomerWidgetEntities db = new CustomerWidgetEntities();

        // GET: api/LoginApi
        public IQueryable<Customer> GetCustomers()
        {
            return db.Customers;
        }

        // GET: api/LoginApi/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/LoginApi/5
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
        // POST: api/LoginApi
        [ResponseType(typeof(Customer))]
       
        public IHttpActionResult PostCustomer(Customer customer)

       {
          
            if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            //var v = db.Customers.Where(a => a.Email.Equals(customer.Email) && a.Password.Equals(customer.Password)).FirstOrDefault();
            var v = (from c in db.Customers
                     where c.Email == customer.Email &&
                     c.Password == customer.Password
                     select new
                     {
                         c.CustomerId,
                         c.FirstName

                     }).FirstOrDefault();
                if (v == null)
                {
                    return Content(HttpStatusCode.NotFound,"Username Doesn't exists!");
                }
                else
                { 

                customer.FirstName = v.FirstName;
                customer.CustomerId = v.CustomerId;
               // customer.LastName = v.LastName;
                //return CreatedAtRoute("DefaultApi", new { id = customer.CustomerId }, customer);
                return Ok(v);
            }
           
        }

        // DELETE: api/LoginApi/5
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