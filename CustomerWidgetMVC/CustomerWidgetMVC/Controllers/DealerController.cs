using CustomerWidgetMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CustomerWidgetMVC.Controllers
{
    public class DealerController : ApiController
    {

    private CustomerWidgetEntities db = new CustomerWidgetEntities();

        [System.Web.Http.Route("api/GetDealerList")]
        public IHttpActionResult GetDealer(int packageId)
    {
      var dealerlist = (from dealer in db.Dealers
                        join dealerService in db.DealerServicePackages on dealer.DealerId equals dealerService.DealerId 
                        where dealerService.ServiceId == packageId
       
                        select new
                        {dealer.DealerId,
                          dealer.DealerName,
                          dealer.ContactNo,
                          dealer.Address,
                          dealer.City,
                          dealerService.Price,
                          dealerService.Description
          
                        }).ToList();
      //var itemObject = JsonConvert.SerializeObject(dealerlist,
      //       Formatting.None,
      //       new JsonSerializerSettings()
      //       {
      //         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
      //       });
      return Ok(dealerlist);
    }

  }
}

