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
        HttpCookie currentUserCookie = new HttpCookie("user");
        HttpCookie loginCookie = new HttpCookie("loginSentBy");
        HttpCookie loginRequestCookie;
        HttpCookie userRequestCookie;

        public ViewResult Index()
        {
            ViewBag.listAllUsers = client.findAllUsers();
            currentUserCookie.Expires = DateTime.Now.AddDays(1);
            loginCookie.Expires = DateTime.Now.AddDays(1);
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

            if (ModelState.IsValid)
            {
                currentUser = client.findUserByUsername(username, password);
                ViewBag.currentUser = currentUser;

                if(currentUser != null)
                {
                    currentUserCookie.Value = currentUser.UserId.ToString();
                    Response.Cookies.Add(currentUserCookie);
                }
                else
                {
                    ViewBag.currentUser = "wrongCredentials";
                    return View("LoginPage");
                }

            }
            else
            {
                //ViewBag.currentUser = null;

                //there is a validation error
                return View();
            }

            loginRequestCookie = Request.Cookies["loginSentBy"];

            if (loginRequestCookie.Value == "reservationsPage")
            {
                return View("ReservationsPage");
            }
            else
            {
                return View("MainPage");
            }

            
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
                userRequestCookie = Request.Cookies["user"];

                if (userRequestCookie == null)
                {
                    loginCookie.Value = "reservationsPage";
                    Response.Cookies.Add(loginCookie);
                    //ViewBag.loginSentBy = "reservationsPage";
                    return View("LoginPage");
                }
                else
                {
                    loginCookie.Value = null;
                    reservation.Name = reservationModel.Name;
                    reservation.DateTime = reservationModel.DateTime;
                    reservation.NumPeople = reservationModel.NumPeople;
                    reservation.UserId = Convert.ToInt32(userRequestCookie.Value);

                    client.InsertReservationIntoDB(reservation);

                    return View("Thanks", reservationModel);
                }

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
