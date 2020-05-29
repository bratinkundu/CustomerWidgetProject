using CustomerWidgetMVC.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CustomerWidgetMVC.Controllers
{
    public class AppoinmentController : ApiController
    {
        private CustomerWidgetEntities db = new CustomerWidgetEntities();

        // GET: api/Customers
        [System.Web.Http.HttpPost]
        public IHttpActionResult PostAppoinment(JObject data)
        {
            var serializer = new JsonSerializer();
            dynamic json = data;

            int customerId = json.CustomerId;
            int carId = json.CarId;
            DateTime datetime = json.Slotdate;
            var slotdate = datetime.Date;
            string time = json.Slottime;
            string[] values = time.Split(':');
            string date = datetime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);

            // var d = time.ToString("hh:mm:ss");
            TimeSpan ts = new TimeSpan(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));

            var ID = (from app in db.Appointments
                      where app.CarId == carId &&
                      app.CustomerId == customerId &&
                      app.DateOfBooking == slotdate &&
                      app.SlotTime == ts
                      select app.AppointmentId).SingleOrDefault();
        


            return Ok(ID);
        }
    }
}
