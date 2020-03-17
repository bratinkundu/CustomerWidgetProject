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
            //IHttpCookie cookie = new IHttpCookie("UserInfo");
            //cookie["Email1"] = customer.Email;
            //cookie["Password1"] = log.Password;
            //cookie.Expires.AddHours(0.5);
            //Response.Cookies.Add(cookie);
            //HttpResponseMessage respMessage = new HttpResponseMessage();

            //respMessage.Content = new ObjectContent<string[]>(new string[] { "value1", "value2" }, new JsonMediaTypeFormatter());

            //var se = new NameValueCollection();
            //se["Email1"] = "123";

            //se["Password1"] = "89898";

            //var cookie = new CookieHeaderValue("session", se);

            //cookie.Expires = DateTimeOffset.Now.AddHours(0.5);

            //cookie.Domain = Request.RequestUri.Host;

            //cookie.Path = "/";

            //respMessage.Headers.AddCookies(new CookieHeaderValue[] { cookie });

            ////return respMessage;

            //string sessionId = "";
            //string sessionPassword = "";
            //CookieHeaderValue cookie1 = Request.Headers.GetCookies("session").FirstOrDefault();
            //if (cookie1 != null)
            //{
            //    sessionId = cookie1["Email1"].Value;
            //    sessionPassword = cookie1["Password1"].Value;

            //    Debug.WriteLine(cookie1);

            if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var v = db.Customers.Where(a => a.Email.Equals(customer.Email) && a.Password.Equals(customer.Password)).FirstOrDefault();
                 
                if (v == null)
                {
                    return NotFound();
                }
                else
                {
                //return Ok(cookie);
                //var resp = new HttpResponseMessage();

                //var cookie = new CookieHeaderValue("Email1", customer.Email);
                //cookie.Expires = DateTimeOffset.Now.AddHours(0.5);
                //cookie.Domain = Request.RequestUri.Host;
                //cookie.Path = "/";
                //resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });

                //string sessionId = "";

                //CookieHeaderValue cookie1 = Request.Headers.GetCookies("Email1").FirstOrDefault();
                //if (cookie != null)
                //{
                //    sessionId = cookie1["Email1"].Value;
                //}

                //return Ok(resp);

                customer.FirstName = v.FirstName;
                customer.CustomerId = v.CustomerId;
                customer.LastName = v.LastName;
                return CreatedAtRoute("DefaultApi", new { id = customer.CustomerId }, customer);
            }
            //return CreatedAtRoute("DefaultApi", new { id = customer.CustomerId }, customer);
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