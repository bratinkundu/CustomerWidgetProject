//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CustomerWidgetMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public int DealerServiceId { get; set; }
        public System.DateTime DateOfBooking { get; set; }
        public System.TimeSpan SlotTime { get; set; }
    
        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual DealerServicePackage DealerServicePackage { get; set; }
    }
}
