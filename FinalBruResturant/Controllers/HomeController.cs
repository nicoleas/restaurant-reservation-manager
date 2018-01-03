using FinalBruResturant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalBruResturant.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("MainPage");
        }

        [HttpGet]
        public ActionResult ReservationsPage()
        {
            return View("ReservationsPage");
        }

        [HttpPost]
        public ActionResult ReservationsPage(Reservation reservation)
        {
            if (ModelState.IsValid)
            {

                String sender = Request.Form["submit"]; //store retrieved values from form

                //ENTITY FRAMEWORK
                /*
                Student student = new Student();

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

                return View("Thanks", reservation);

            }
            else
            {
                //there is a validation error
                return View();
            }
        }
    }
}
