using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace FinalBruResturant.Models
{
    public class Profile
    {
        public bool LoggedIn = true;

        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Phone number must be 10 digits with no spaces")]
        [StringLength(32)]
        [Required(ErrorMessage = "Please enter your phone number")]
        public string Phone { get; set; }

        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 6, ErrorMessage ="Username must be at between 6 and 30 characters")]
        [Required(ErrorMessage = "Please choose a username")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        [Required(ErrorMessage = "Please choose a password")]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8)]
        [Required(ErrorMessage = "Please retype your password")]
        public string ConfirmPassword { get; set; }


    
        public void Logout()
        {
            LoggedIn = false;
        }
    }
}