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
        User currentUser;

        public ViewResult Index()
        {
            ViewBag.listAllUsers = client.findAllUsers();
            return View("MainPage");
        }

        #region Main Page
        [HttpGet]
        public ActionResult MainPage()
        {
            return View("MainPage");
        }
        #endregion

        #region About Page
        public ActionResult About()
        {
            return View("About");
        }
        #endregion

        #region Login Page
        [HttpGet]
        public ActionResult LoginPage()
        {
            return View("LoginPage");
        }

        [HttpPost]
        public ActionResult LoginPage(Login login)
        {
            String username = login.Username;
            String password = login.Password;
            String sender = Request.Form["submit"]; //store retrieved values from form
            HttpCookie cookie = new HttpCookie("cookieName");
            cookie.Expires = DateTime.Now.AddDays(1); 

            if (ModelState.IsValid)
            {
                currentUser = client.findUserByUsername(username, password);
                ViewBag.currentUser = currentUser;

                cookie.Value = "true";
                Response.Cookies.Add(cookie);

            }
            else
            {
                ViewBag.currentUser = null;

                cookie.Value = "false";
                Response.Cookies.Add(cookie);
            }

            return View("MainPage");
        }
        #endregion

        #region Create Profile Page

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

                //insert into database using entity framework
                User user = new User();

                user.FirstName = profile.FirstName;
                user.LastName = profile.LastName;
                user.Email = profile.Email;
                user.Phone = profile.Phone;
                user.Username = profile.Username;
                user.Password = profile.Password;
                user.RewardPoints = null;

                client.InsertUserIntoDB(user);

                return View("ConfirmationPage"/*,profile*/);

            }
            else
            {
                //there is a validation error
                return View();
            }
        }

        #endregion

        #region Reservations Page
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
                Reservation reservation = new Reservation();


                reservation.DateTime = reservationModel.DateTime;
                reservation.NumPeople = reservationModel.NumPeople;

                client.InsertReservationIntoDB(reservation);

                return View("Thanks", reservationModel);

            }
            else
            {
                //there is a validation error
                return View();
            }
        }
        #endregion
    }
}
