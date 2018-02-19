using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestGrandHotel
{
    [TestClass]
    public class ReservationsTest
    {
        private static IDataReservation _dataReservation;

        [ClassInitialize]
        public static void InitClass(TestContext context)
        {
            _dataReservation = new ReservationsController();
        }

        [TestMethod]
        public void Index()
        {
            int IdClient = 200;
            Reservation res = new Reservation
            {
                JourDebutSejour = 19 / 02 / 2018,
                NbPersonnes = 3,
                HeureArrivee = 19
            };
            _dataReservation.Edit();
        }

        [TestMethod]
        public void VéficationDisponi()
        {
        }

        [TestMethod]
        public void Details()
        {
        }

        [TestMethod]
        public void DetailsReservarion()
        {
        }

        [TestMethod]
        public void Create()
        {
        }

        [TestMethod]
        public void Edit()
        {
        }

        [TestMethod]
        public void Delete()
        {
        }

        public void DeleteConfirmed()
        {
        }

        //public void ReservationExists()
        //{
        //}


    }
}
