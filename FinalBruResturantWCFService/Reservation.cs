//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalBruResturantWCFService
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservation
    {
        public int ReservationId { get; set; }
        public int NumPeople { get; set; }
        public System.DateTime DateTime { get; set; }
        public int UserId { get; set; }
    
        public virtual User User { get; set; }
    }
}