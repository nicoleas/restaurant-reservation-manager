using FinalBruResturant.Models;
using FinalBruResturant.ResturantService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalBruResturant.Controllers
{
    public class HomeController : Controller
    {
        private ResturantServiceClient client = new ResturantServiceClient();

        public ActionResult Index()
        {
            return View("MainPage");
        }

        [HttpGet]
        public ActionResult MainPage()
        {
            return View("MainPage");
        }

        [HttpGet]
        public ActionResult CreateProfilePage()
        {
            return View("CreateProfilePage");
        }

        [HttpPost]
        public ActionResult CreateProfilePage(Profile profile)
        {
            if (ModelState.IsValid)
            {

                String sender = Request.Form["submit"]; //store retrieved values from form

                //insert into database using entity framework
                User user = new User();

                user.FirstName = profile.FirstName;
                user.LastName = profile.LastName;
                user.Email = profile.Email;
                user.Phone = Convert.ToInt64(profile.Phone);
                user.Username = profile.Username;
                user.Password = profile.Password;

                client.InsertUserIntoDB(user);

                return View("ConfirmationPage", profile);

            }
            else
            {
                //there is a validation error
                return View();
            }
        }

        [HttpGet]
        public ActionResult ReservationsPage()
        {
            return View("ReservationsPage");
        }

        [HttpPost]
        public ActionResult ReservationsPage(ReservationModel reservationModel)
        {
            if (ModelState.IsValid)
            {

                String sender = Request.Form["submit"]; //store retrieved values from form

                

                //ENTITY FRAMEWORK
                /*Student student = new Student();

                student.StudentId = studentId + 1;
                student.Name = studentResponse.Name;
                student.Email = studentResponse.Email;
                student.Phone = studentResponse.Phone;
                student.Address = studentResponse.Address;
                student.TechnicalInterest = studentResponse.TechnicalInterest.ToString();
                student.SocialNetworkInterest = studentResponse.SocialNetworkInterest;

                Attend attend = new Attend();

                attend.Id = attendId + 1;
                attend.Student = student.StudentId;
                switch (studentResponse.WillAttend)
                {
                    case true:
                        attend.AcceptRegret = "Accept";
                        break;
                    case false:
                        attend.AcceptRegret = "Regret";
                        break;
                    default:
                        attend.AcceptRegret = "Regret";
                        break;
                }

                client.InsertIntoDB(student, attend);
                */

                return View("Thanks", reservationModel);

            }
            else
            {
                //there is a validation error
                return View();
            }
        }
    }
}
