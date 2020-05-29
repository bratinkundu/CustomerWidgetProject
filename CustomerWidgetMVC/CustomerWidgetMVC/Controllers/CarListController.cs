using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Mvc;
using CustomerWidgetMVC.Models;
using Newtonsoft.Json;

namespace CustomerWidgetMVC.Controllers
{
    public class CarListController : ApiController
    {
        private CustomerWidgetEntities db = new CustomerWidgetEntities();

        // GET: api/CarList
        [System.Web.Mvc.HttpGet]
        [System.Web.Http.Route("api/GetCarList")]
        public IHttpActionResult GetCars(int CustomerId)
        {
            var carlist = (from car in db.Cars
                           join customercar in db.CustomerCars on car.CarId equals customercar.CarId
                           join customer in db.Customers on customercar.CustomerId equals customer.CustomerId
                           where customercar.CustomerId == CustomerId
                           select new
                           {
                               car.CarId,
                               car.RegistrationNo,
                               car.Model,
                               car.BrandName
                           }).ToList();
            //var itemObject = JsonConvert.SerializeObject(carlist,
            //       Formatting.None,
            //       new JsonSerializerSettings()
            //       {
            //           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //       });
            return Content(HttpStatusCode.OK, carlist);
        }

        // GET: api/CarList/5
        [ResponseType(typeof(Car))]
      
        public IHttpActionResult GetCar(int id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        // PUT: api/CarList/5
        public IHttpActionResult PutCar(int id, Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != car.CarId)
            {
                return BadRequest();
            }

            db.Entry(car).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/RegisterCar/")]
        // POST: api/CarList
        [HandleError]
        public IHttpActionResult PostCar(CarDetails cardetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var isCarAlreadyExists = db.Cars.Any(x => x.RegistrationNo == cardetails.RegistrationNo);
            if (isCarAlreadyExists)
            {
                return Content(HttpStatusCode.NotFound,"Car already exist");
            }
            else
            {
                Car car = new Car();

                car.RegistrationNo = cardetails.RegistrationNo;
                car.BrandName = cardetails.BrandName;
                car.Model = cardetails.Model;
                db.Cars.Add(car);

                var id = from c in db.Customers where c.Email == cardetails.Email select c.CustomerId;

                CustomerCar customerCar = new CustomerCar();
                customerCar.CarId = car.CarId;
                customerCar.CustomerId = id.First();
                db.CustomerCars.Add(customerCar);
                db.SaveChanges();
                // var session = HttpContext.Current.Session;
                // session["CustomerCarId"] = customerCar.CarId;
                return Ok(true);
            }
            // return NotFound();
        }

        // DELETE: api/CarList/5
        public IHttpActionResult DeleteCar(int id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            db.Cars.Remove(car);
            db.SaveChanges();

            return Ok(car);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarExists(int id)
        {
            return db.Cars.Count(e => e.CarId == id) > 0;
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
//using System.Web;
//using System.Web.Http;
//using System.Web.Http.Cors;
//using System.Web.Http.Description;
//using System.Web.Mvc;

//using CustomerWidgetMVC.Models;
//using Newtonsoft.Json;

//namespace CustomerWidgetMVC.Controllers
//{

//    public class AddCarController : ApiController
//    {
//        private CustomerWidgetEntities db = new CustomerWidgetEntities();

//        // GET: api/CarList
//        [System.Web.Mvc.HttpGet]
//        // [System.Web.Http.Authorize]

//        public IHttpActionResult GetCars(int CustomerId)
//        {
//            var carlist = (from car in db.Cars
//                           join customercar in db.CustomerCars on car.CarId equals customercar.CarId
//                           join customer in db.Customers on customercar.CustomerId equals customer.CustomerId
//                           where customercar.CustomerId == CustomerId
//                           select new
//                           {
//                               car.CarId,
//                               car.RegistrationNo,
//                               car.Model,
//                               car.BrandName
//                           }).ToList();
//            var itemObject = JsonConvert.SerializeObject(carlist,
//                   Formatting.None,
//                   new JsonSerializerSettings()
//                   {
//                       ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//                   });
//            return Ok(itemObject);
//        }


//        [System.Web.Http.HttpPost]
//        // POST: api/CarList
//        [HandleError]
//        public IHttpActionResult PostCar(CarDetails cardetails)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);

//            }
//            var isCarAlreadyExists = db.Cars.Any(x => x.RegistrationNo == cardetails.RegistrationNo);
//            if (isCarAlreadyExists)
//            {
//                return NotFound();
//            }
//            else
//            {
//                Car car = new Car();

//                car.RegistrationNo = cardetails.RegistrationNo;
//                car.BrandName = cardetails.BrandName;
//                car.Model = cardetails.Model;
//                db.Cars.Add(car);

//                var id = (from c in db.Customers where c.Email == cardetails.Email select c.CustomerId).SingleOrDefault();

//                CustomerCar customerCar = new CustomerCar();
//                customerCar.CarId = car.CarId;
//                customerCar.CustomerId = id;
//                db.CustomerCars.Add(customerCar);
//                db.SaveChanges();
//                // var session = HttpContext.Current.Session;
//                // session["CustomerCarId"] = customerCar.CarId;
//                return Ok(true);
//            }
//            // return NotFound();
//        }


//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private bool CarExists(int id)
//        {
//            return db.Cars.Count(e => e.CarId == id) > 0;
//        }

//    }
//}
