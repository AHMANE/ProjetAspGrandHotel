using System;
using System.Collections.Generic;

namespace GH_API_Client
{
    public partial class Reservation
    {
        public short NumChambre { get; set; }
        public DateTime Jour { get; set; }
        public int IdClient { get; set; }
        public byte NbPersonnes { get; set; }
        public byte HeureArrivee { get; set; }
        public bool? Travail { get; set; }

        public Client IdClientNavigation { get; set; }
    }
}
