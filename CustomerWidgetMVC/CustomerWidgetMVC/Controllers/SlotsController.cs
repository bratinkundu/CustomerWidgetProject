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

namespace CustomerWidgetMVC.Controllers
{
    public class SlotsController : ApiController
    {
        private CustomerWidgetEntities db = new CustomerWidgetEntities();

        // GET: api/Slots
        [HttpGet]

        public IQueryable<Slot> GetSlots()
        {
            return db.Slots;
        }
        [HttpGet]
        [System.Web.Http.Route("api/GetAvailableSlots")]
        // GET: api/Slots/5
        [ResponseType(typeof(Slot))]
        public IHttpActionResult GetSlot(int id, DateTime date)//,DealerDate dealerDate)
        {
            var slots = (from slot in db.Slots
                         where slot.DealerId == id && slot.Date == date
                         select new
                         {
                             slot.Date,
                             slot.SlotTime,
                             slot.DealerId,
                             slot.SlotAvailable
                         });


            return Ok(slots);


        }

        // PUT: api/Slots/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSlot(int id, Slot slot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != slot.DealerId)
            {
                return BadRequest();
            }

            db.Entry(slot).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlotExists(id))
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

        // POST: api/Slots
        [ResponseType(typeof(Slot))]
        public IHttpActionResult PostSlot(int dealerId)
        {
            var name = from dealer in db.Dealers
                       where dealer.DealerId == dealerId
                       select dealer.DealerName;
            return Ok(name);
        }







        // DELETE: api/Slots/5
        [ResponseType(typeof(Slot))]
        public IHttpActionResult DeleteSlot(int id)
        {
            Slot slot = db.Slots.Find(id);
            if (slot == null)
            {
                return NotFound();
            }

            db.Slots.Remove(slot);
            db.SaveChanges();

            return Ok(slot);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SlotExists(int id)
        {
            return db.Slots.Count(e => e.DealerId == id) > 0;
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

//namespace CustomerWidgetMVC.Controllers
//{
//    public class GetAvailableSlotsController : ApiController
//    {
//        private CustomerWidgetEntities db = new CustomerWidgetEntities();

//        // GET: api/Slots
//        [HttpGet]
//        public IQueryable<Slot> GetSlots()
//        {
//            return db.Slots;
//        }

//        // GET: api/Slots/5
//        [ResponseType(typeof(Slot))]
//        public IHttpActionResult GetSlot(int id, DateTime date)//,DealerDate dealerDate)
//        {
//            var slots = (from slot in db.Slots
//                         where slot.DealerId == id && slot.Date == date
//                         select new
//                         {
//                             slot.Date,
//                             slot.SlotTime,
//                             slot.DealerId,
//                             slot.SlotAvailable
//                         });


//            return Ok(slots);


//        }

//        // POST: api/Slots
//        [ResponseType(typeof(Slot))]
//        public IHttpActionResult PostSlot(int dealerId)
//        {
//            var name = from dealer in db.Dealers
//                       where dealer.DealerId == dealerId
//                       select dealer.DealerName;
//            return Ok(name);
//        }
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private bool SlotExists(int id)
//        {
//            return db.Slots.Count(e => e.DealerId == id) > 0;
//        }
//    }
//}
