using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
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
using Newtonsoft.Json.Linq;

namespace CustomerWidgetMVC.Controllers
{
    public class SummaryController : ApiController
    {
        private CustomerWidgetEntities db = new CustomerWidgetEntities();

        [System.Web.Http.HttpGet]

        [System.Web.Http.Route("api/GetAppointmentDetails")]
        public IHttpActionResult GetSummary(int Dealerid, int CarId, int PackageId)//,DealerDate dealerDate)
        {
            var Data1 = (from customer in db.Customers
                         join customercar in db.CustomerCars
on customer.CustomerId equals customercar.CustomerId
                         join car in db.Cars on customercar.CarId equals car.CarId
                         where car.CarId == CarId
                         select new
                         {
                             customer.FirstName,
                             customer.LastName,
                             customer.Address,
                             customer.ContactNo,
                             customer.Email,
                             car.RegistrationNo,
                             car.Model,
                             car.BrandName
                         }).ToList();

            var Data2 = (from dealer in db.Dealers
                         join dealerservice in db.DealerServicePackages on dealer.DealerId equals dealerservice.DealerId
                         join service in db.ServicePackages on dealerservice.ServiceId equals service.ServiceId
                         where dealer.DealerId == Dealerid && dealerservice.ServiceId == PackageId
                         select new
                         {
                             dealer.DealerName,
                             dealer.ContactNo,
                             dealer.Address,
                             dealer.City,
                             dealerservice.Price,
                             dealerservice.Description,
                             service.ServiceName
                         }).ToList();

            List<Object> Data = (from x in Data1 select (Object)x).ToList();
            Data.AddRange((from x in Data2 select (Object)x).ToList());
           // String hourMinute = DateTime.Now.ToString("HH:mm");


            //var itemObject = (JsonConvert.SerializeObject(Data,
            // Formatting.None,
            // new JsonSerializerSettings()
            // {
            //     ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            // }));
            return Ok(Data);


        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult PostSummary(JObject data)

        {
            //int customerId, int DealerId, int CarId, int PackageId,
            //// dateOfbooking, string slotTime
            //     DateTime dateOfbooking = Convert.ToDateTime(date);
            var serializer = new JsonSerializer();
            dynamic json = data;

            int dealerid = json.DealerId;
            int packageid = json.PackageId;
            DateTime datetime = json.dateOfbooking;
            var dateofbooking = datetime.Date;
            string time = json.slotTime;
            string[] values = time.Split(':');

            // var d = time.ToString("hh:mm:ss");
            TimeSpan ts = new TimeSpan(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
            /* string Time = json.slotTime;
             var result = Convert.ToDateTime(Time);
             DateTime dates = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);
             string t =result.ToString("hh:mm:ss tt",CultureInfo.CurrentCulture);
             */
            var dealerServiceid = from dealerservicepackage in db.DealerServicePackages
                                  where dealerservicepackage.DealerId == dealerid
                                  && dealerservicepackage.ServiceId == packageid
                                  select dealerservicepackage.DealerServiceId;
            Appointment app = new Appointment();
            app.DealerServiceId = dealerServiceid.First();
            app.CustomerId = json.customerId;
            app.CarId = json.CarId;
            app.DateOfBooking = json.dateOfbooking;
            app.SlotTime = ts;
            var appointment = db.Appointments.Any(x => x.CarId == app.CarId && x.DateOfBooking == app.DateOfBooking);
            if (appointment == false)
            {
                db.Appointments.Add(app);

                db.SaveChanges();
                var s = (from slots in db.Slots
                         where slots.DealerId == dealerid && slots.Date == dateofbooking && slots.SlotTime == ts
                         select slots.SlotAvailable).SingleOrDefault();

                Slot slot = new Slot();
                slot.DealerId = json.DealerId;
                slot.Date = json.dateOfbooking;
                slot.SlotTime = ts;
                slot.SlotAvailable = (s - 1);
                db.Entry(slot).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, "You have already booked the appointment for this date.");
            }

            return Ok(true);

        }

    }
}


















//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Globalization;
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
//using Newtonsoft.Json.Linq;

//namespace CustomerWidgetMVC.Controllers
//{

//    public class AppoinmentDetailsController : ApiController
//    {
//        private CustomerWidgetEntities db = new CustomerWidgetEntities();

//        [System.Web.Http.HttpGet]
//        public IHttpActionResult GetSummary(int Dealerid, int CarId, int PackageId)//,DealerDate dealerDate)
//        {
//            var Data1 = (from customer in db.Customers
//                         join customercar in db.CustomerCars
//on customer.CustomerId equals customercar.CustomerId
//                         join car in db.Cars on customercar.CarId equals car.CarId
//                         where car.CarId == CarId
//                         select new
//                         {
//                             customer.FirstName,
//                             customer.LastName,
//                             customer.Address,
//                             customer.ContactNo,
//                             customer.Email,
//                             car.RegistrationNo,
//                             car.Model,
//                             car.BrandName
//                         }).ToList();

//            var Data2 = (from dealer in db.Dealers
//                         join dealerservice in db.DealerServicePackages on dealer.DealerId equals dealerservice.DealerId
//                         join service in db.ServicePackages on dealerservice.ServiceId equals service.ServiceId
//                         where dealer.DealerId == Dealerid && dealerservice.ServiceId == PackageId
//                         select new
//                         {
//                             dealer.DealerName,
//                             dealer.ContactNo,
//                             dealer.Address,
//                             dealer.City,
//                             dealerservice.Price,
//                             dealerservice.Description,
//                             service.ServiceName
//                         }).ToList();

//            List<Object> Data = (from x in Data1 select (Object)x).ToList();
//            Data.AddRange((from x in Data2 select (Object)x).ToList());


//            var itemObject = (JsonConvert.SerializeObject(Data,
//             Formatting.None,
//             new JsonSerializerSettings()
//             {
//                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore
//             }));
//            return Ok(itemObject);


//        }

//        [System.Web.Http.HttpPost]
//        public IHttpActionResult PostSummary(JObject data)

//        {
//            var serializer = new JsonSerializer();
//            dynamic json = data;

//            int dealerid = json.DealerId;
//            int packageid = json.PackageId;
//            DateTime datetime = json.dateOfbooking;
//            var dateofbooking = datetime.Date;
//            string time = json.slotTime;
//            string[] values = time.Split(':');

//            // var d = time.ToString("hh:mm:ss");
//            TimeSpan ts = new TimeSpan(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
//            /* string Time = json.slotTime;
//             var result = Convert.ToDateTime(Time);
//             DateTime dates = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);
//             string t =result.ToString("hh:mm:ss tt",CultureInfo.CurrentCulture);
//             */


//            var dealerServiceid = from dealerservicepackage in db.DealerServicePackages
//                                  where dealerservicepackage.DealerId == dealerid
//                                  && dealerservicepackage.ServiceId == packageid
//                                  select dealerservicepackage.DealerServiceId;
//            Appointment app = new Appointment();
//            app.DealerServiceId = dealerServiceid.First();
//            app.CustomerId = json.customerId;
//            app.CarId = json.CarId;
//            app.DateOfBooking = json.dateOfbooking;
//            app.SlotTime = ts;
//            var appointment = db.Appointments.Any(x => x.CarId == app.CarId && x.DateOfBooking == app.DateOfBooking);
//            if (appointment == false)
//            {
//                db.Appointments.Add(app);

//                db.SaveChanges();
//                var s = (from slots in db.Slots
//                         where slots.DealerId == dealerid && slots.Date == dateofbooking && slots.SlotTime == ts
//                         select slots.SlotAvailable).SingleOrDefault();

//                Slot slot = new Slot();
//                slot.DealerId = json.DealerId;
//                slot.Date = json.dateOfbooking;
//                slot.SlotTime = ts;
//                slot.SlotAvailable = (s - 1);
//                db.Entry(slot).State = EntityState.Modified;
//                db.SaveChanges();
//            }
//            else
//            {
//                return Content(HttpStatusCode.BadRequest, "You already booked appointment for this car on this day.");
//            }

//            return Ok(true);

//        }

//    }
//}

