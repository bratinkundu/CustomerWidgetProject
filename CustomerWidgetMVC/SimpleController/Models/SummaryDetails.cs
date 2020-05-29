using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleController.Models
{
    public class SummaryDetails
    {
        public string Email { get; set; }
        public int DealerId { get; set; }
        public int CarId { get; set; }
        public int PackageId { get; set; }
    }
}