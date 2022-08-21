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
	using System.ComponentModel.DataAnnotations;

	public partial class Search
    {
        public int id { get; set; }
        [Required]
        public string origin { get; set; }
        [Required]
        public string destination { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        [Required]
        public string flightType { get; set; }
        public string tripType { get; set; }
        public string shortBy_Id { get; set; }
        [Required]
        public string classType { get; set; }
    
        public virtual ClassType ClassType1 { get; set; }
        public virtual Sort Sort { get; set; }
    }
}
