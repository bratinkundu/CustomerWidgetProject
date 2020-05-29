using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CustomerWidgetMVC.Models;
using Newtonsoft.Json;

namespace CustomerWidgetMVC.Controllers
{
    public class ServicePackageController : ApiController
    {
        private CustomerWidgetEntities db = new CustomerWidgetEntities();

        // GET: api/ServicePackage
        //    public IQueryable<ServicePackage> GetServicePackages()
        //{
        [Route("api/GetServicePackage")]
        [System.Web.Http.Authorize]
        public IHttpActionResult GetServicePackages()
        {

            var services = (from service in db.ServicePackages
                            select new
                            {
                                service.ServiceId,
                                service.ServiceName

                            }).ToList();
            //var itemObject = (JsonConvert.SerializeObject(services,
            //      Formatting.None,
            //      new JsonSerializerSettings()
            //      {
            //          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //      }));
            return Ok(services);
            // return Ok(services);
        }


        // GET: api/ServicePackage/5
        [ResponseType(typeof(ServicePackage))]
        [System.Web.Http.Authorize]
        public IHttpActionResult GetServicePackage(int id)
        {
            ServicePackage servicePackage = db.ServicePackages.Find(id);
            if (servicePackage == null)
            {
                return NotFound();
            }

            return Ok(servicePackage);
        }


        // PUT: api/ServicePackage/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutServicePackage(int id, ServicePackage servicePackage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != servicePackage.ServiceId)
            {
                return BadRequest();
            }

            db.Entry(servicePackage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicePackageExists(id))
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

        // POST: api/ServicePackage
        [ResponseType(typeof(ServicePackage))]
        public IHttpActionResult PostServicePackage(ServicePackage servicePackage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ServicePackages.Add(servicePackage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = servicePackage.ServiceId }, servicePackage);
        }

        // DELETE: api/ServicePackage/5
        [ResponseType(typeof(ServicePackage))]
        public IHttpActionResult DeleteServicePackage(int id)
        {
            ServicePackage servicePackage = db.ServicePackages.Find(id);
            if (servicePackage == null)
            {
                return NotFound();
            }

            db.ServicePackages.Remove(servicePackage);
            db.SaveChanges();

            return Ok(servicePackage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServicePackageExists(int id)
        {
            return db.ServicePackages.Count(e => e.ServiceId == id) > 0;
        }
    }
}





//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using System.Web.Http.Description;

//using CustomerWidgetMVC.Models;
//using Newtonsoft.Json;

//namespace CustomerWidgetMVC.Controllers
//{

//    public class GetServicePackageController : ApiController
//    {
//        private CustomerWidgetEntities db = new CustomerWidgetEntities();

//        // GET: api/ServicePackage

//        public IHttpActionResult GetServicePackages()
//        {

//            var services = (from service in db.ServicePackages
//                            select new
//                            {
//                                service.ServiceId,
//                                service.ServiceName

//                            }).ToList();
//            var itemObject = (JsonConvert.SerializeObject(services,
//                  Formatting.None,
//                  new JsonSerializerSettings()
//                  {
//                      ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//                  }));
//            return Ok(itemObject);
//            // return Ok(services);
//        }


//        // GET: api/ServicePackage/5
//        [ResponseType(typeof(ServicePackage))]
//        public IHttpActionResult GetServicePackage(int id)
//        {
//            ServicePackage servicePackage = db.ServicePackages.Find(id);
//            if (servicePackage == null)
//            {
//                return NotFound();
//            }

//            return Ok(servicePackage);
//        }


//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private bool ServicePackageExists(int id)
//        {
//            return db.ServicePackages.Count(e => e.ServiceId == id) > 0;
//        }
//    }
//}

