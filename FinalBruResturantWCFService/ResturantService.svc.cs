using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FinalBruResturantWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ResturantService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ResturantService.svc or ResturantService.svc.cs at the Solution Explorer and start debugging.
    public class ResturantService : IResturantService
    {
        private BruRestaurantDBEntities entities = new BruRestaurantDBEntities();

        public List<Reservation> findAllReservations()
        {
            return entities.Reservations.ToList();
        }

        public List<User> findAllUsers()
        {
            return entities.Users.ToList();
        }

        public User findUserByUsername(String username, String password)
        {
            List<User> userList = entities.Users.Where(u => (u.Username == username && u.Password == password)).ToList();

            //if there is exactly one user with that same username and matching password
            if (entities.Users.Where(u => (u.Username == username && u.Password == password)).Count() == 1)
            {
                return userList[0];
            }
            else
            {
                return null;
            }
        }

        public String findEmailByUserId(int userId)
        {
            List<User> userList = entities.Users.Where(u => (u.UserId == userId)).ToList();

            //if there is exactly one user with that same username and matching password
            if (entities.Users.Where(u => (u.UserId == userId)).Count() == 1)
            {
                return userList[0].Email;
            }
            else
            {
                return null;
            }
        }

        public void InsertIntoDB(Reservation reservation, User user)
        {
            entities.Reservations.Add(reservation);
            entities.Users.Add(user);
            entities.SaveChanges();
        }

        public void InsertReservationIntoDB(Reservation reservation)
        {
            entities.Reservations.Add(reservation);
            entities.SaveChanges();
        }

        public void InsertUserIntoDB(User user)
        {
            entities.Users.Add(user);
            entities.SaveChanges();
        }
    }
}
