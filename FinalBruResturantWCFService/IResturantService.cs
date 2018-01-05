using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FinalBruResturantWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IResturantService" in both code and config file together.
    [ServiceContract]
    public interface IResturantService
    {

        [OperationContract]
        List<User> findAllUsers();

        [OperationContract]
        List<Reservation> findAllReservations();

        [OperationContract]
        User findUserByUsername(String username, String password);

        [OperationContract]
        void InsertIntoDB(Reservation reservation, User user);

        [OperationContract]
        void InsertReservationIntoDB(Reservation reservation);

        [OperationContract]
        void InsertUserIntoDB(User user);

    }
}
