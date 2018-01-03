using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalBruResturant.Models
{
    public class Reservation
    {
        //public string stringMonth = "";
        [Required(ErrorMessage = "Please pick a date and time")]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "Please pick a date")]
        //public string Date { get; set; }
        //[Required(ErrorMessage = "Please choose a time")]
        //public string Time { get; set; }
        [Required(ErrorMessage = "Please select the number of people")]
        public int NumPeople { get; set; }
    }
}