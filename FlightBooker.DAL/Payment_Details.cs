//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlightBooker.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Payment_Details
    {
        public int id { get; set; }
        public Nullable<int> payment_id { get; set; }
        public Nullable<int> flight_reservation_id { get; set; }
        public Nullable<decimal> amount { get; set; }
    
        public virtual Flight_Reservation Flight_Reservation { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
