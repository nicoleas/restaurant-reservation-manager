using FinalBruResturant.Models;
using FinalBruResturant.ResturantService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FinalBruResturant.Controllers
{
    public class HomeController : Controller
    {
        //global variables
        private ResturantServiceClient client = new ResturantServiceClient();
        User currentUser;
        //cookies used througout the controller
        HttpCookie currentUserCookie = new HttpCookie("user");
        HttpCookie loginCookie = new HttpCookie("loginSentBy");
        HttpCookie loginRequestCookie;
        HttpCookie userRequestCookie;



        public ViewResult Index()
        {
            //cookie expiries are set so they dont expire before we get to use them
            currentUserCookie.Expires = DateTime.Now.AddDays(1);
            loginCookie.Expires = DateTime.Now.AddDays(1);
                
            return View("MainPage");
        }

        #region Main Page
        [HttpGet]
        public ActionResult MainPage()
        {
            //request for yelp api, by default the yelp api returns its data in JSON format
            HttpWebRequest webRequest = WebRequest.Create("https://api.yelp.com/v3/businesses/bru-restaurant-oakville") as HttpWebRequest;
            HttpWebResponse webResponse = null;

            //api credentials
            webRequest.Headers.Add("Authorization", "Bearer MSONs5aSzubPaRJXutDJ_PgokQX-P_MkzJ2bZJFwpnH4nwmYzWAXLEHMxpKTtncIOUSmAvJUtWB7h6C1gDtac0aAEy4Km0xgsdQimSk55r5P8V1BdNynw1gY45tPWnYx");
            webRequest.Method = "GET";
            webResponse = (HttpWebResponse)webRequest.GetResponse();
            if (webResponse.StatusCode == HttpStatusCode.OK) //checks response was succesfull
            {
                StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());

                //stores the response in a string array
                string[] responseData = responseReader.ReadToEnd().Split();

                //created a list type array
                List<string> bruInfoYelp = new List<string>();

                /*this foreach loop is a filter of storing what information we want
                    to display from the yelp api in the main page 
                 */
                foreach (string bruInfo in responseData)
                {
                    //the case checks for a certain key
                    switch (bruInfo)
                    {
                        case "\"name\":": 
                            bruInfoYelp.Add(bruInfo);
                            break;
                        case "\"Bru":
                            bruInfoYelp.Add(bruInfo);
                            break;
                        case "Restaurant\",":
                            bruInfoYelp.Add(bruInfo);
                            break;
                        case "\"display_phone\":":
                            bruInfoYelp.Add(bruInfo);
                            break;
                        case "905-844-4400\",":
                            bruInfoYelp.Add(bruInfo);
                            break;
                        case "\"rating\":":
                            bruInfoYelp.Add(bruInfo);
                            break;
                        case "4.0,":
                            bruInfoYelp.Add(bruInfo);
                            break;
                    }
                }

                /* The following 8 lines of code are to modify some of the values in the 
                 * bruInfoYelp array, because some values have a comma or quoation mark
                 * as there value, therefore not looking appealing when being dislpayed
                 * in the view.
                 */
                string bruModifiedQuotes = bruInfoYelp[1].Replace("\"", "");
                string restaurantModifiedQuotes = bruInfoYelp[2].Replace("\"", "");
                string phoneNumberModifiedQuotes = bruInfoYelp[4].Replace("\"", "");
                string ratingModifiedQuotes = bruInfoYelp[6].Replace("\"", "");

                string bruModifiedComma = bruModifiedQuotes.Replace(",", "");
                string restaurantModifiedComma = restaurantModifiedQuotes.Replace(",", "");
                string phoneNumberModifiedComma = phoneNumberModifiedQuotes.Replace(",", "");
                string ratingModifiedComma = ratingModifiedQuotes.Replace(",", "");

                //stores the final modified properties in the YelpInfo model properties
                YelpInfo yelpInfo = new YelpInfo
                {
                    restaurantOne = bruModifiedComma,
                    restaurantSecond = restaurantModifiedComma,
                    restaurantNumber = phoneNumberModifiedComma,
                    restaurantRating = ratingModifiedComma
                };

                //sends the yelpInfo object to the MainPage.cshtml view
                return View("MainPage", yelpInfo);

            }
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
            //cookie to hold the value of which page sent user to LoginPage
            loginRequestCookie = Request.Cookies["loginSentBy"];

            //if page validations defined in the Login model pass
            if (ModelState.IsValid)
            {
                //use service to find out if the user exists
                currentUser = client.findUserByUsername(username, password);
                ViewBag.currentUser = currentUser;

                //if the user exists
                if(currentUser != null)
                {
                    //insert data into cookie that contains the id 
                    //of the user trying to login
                    currentUserCookie.Value = currentUser.UserId.ToString();
                    Response.Cookies.Add(currentUserCookie);
                }
                else //if the user does not exist
                {
                    ViewBag.currentUser = "wrongCredentials";
                    return View("LoginPage", login);
                }

            }
            else
            {
                //there is a validation error
                return View();
            }

            if (loginRequestCookie != null)
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
            //if page validations defined in the Profile model pass
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

                //use service to find the user who just logged in
                //to get their current UserId(it autoincrements) 
                //and update the current user cookie
                currentUser = client.findUserByUsername(profile.Username, profile.Password);
                ViewBag.currentUser = user;
                currentUserCookie.Value = currentUser.UserId.ToString();
                Response.Cookies.Add(currentUserCookie);

                return View("ConfirmationPage");

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
            //if page validations defined in the Reservation model pass
            if (ModelState.IsValid)
            {
                Reservation reservation = new Reservation();
                //cookie to hold the userId of the current user
                userRequestCookie = Request.Cookies["user"];

                //if no user is logged in
                if (userRequestCookie == null)
                {
                    //send user to login page and update the value of the cookie
                    //to inform the LoginPage that the user needs to come back
                    //to the ReservationsPage after login
                    loginCookie.Value = "reservationsPage";
                    Response.Cookies.Add(loginCookie);
                    ViewBag.message= "notSignedIn";
                    return View("LoginPage");
                }
                else //if user is already logged in
                {
                    //insert into database using entity framework
                    //and reset cookie value
                    loginCookie.Value = null;
                    reservation.Name = reservationModel.Name;
                    reservation.DateTime = reservationModel.DateTime;
                    reservation.NumPeople = reservationModel.NumPeople;
                    reservation.UserId = Convert.ToInt32(userRequestCookie.Value);

                    client.InsertReservationIntoDB(reservation);

                    ViewBag.findEmailByUserId = client.findEmailByUserId(Convert.ToInt32(userRequestCookie.Value));

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
