using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalBruResturant.Models
{
    public class ReservationParentModel
    {
        public Login LoginModel { get; set; }
        public ReservationModel ReservationModel { get; set; }
        public Profile ProfileModel { get; set; }
    }
}