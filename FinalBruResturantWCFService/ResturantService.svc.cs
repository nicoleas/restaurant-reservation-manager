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
        private BruResturantDBEntities entities = new BruResturantDBEntities();

        public List<Reservation> findAllReservations()
        {
            return entities.Reservations.ToList();
        }

        public List<User> findAllUsers()
        {
            return entities.Users.ToList();
        }

        public void InsertIntoDB(Reservation reservation)
        {
            entities.Reservations.Add(reservation);
        }
    }
}
