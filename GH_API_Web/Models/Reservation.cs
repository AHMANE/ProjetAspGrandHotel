﻿using System;
using System.Collections.Generic;

namespace GH_API_Web.Models
{
    public partial class Reservation
    {
        public short NumChambre { get; set; }
        public DateTime Jour { get; set; }
        public int IdClient { get; set; }
        public byte NbPersonnes { get; set; }
        public byte HeureArrivee { get; set; }
        public bool? Travail { get; set; }

        public ClientVM IdClientNavigation { get; set; }
        public Calendrier JourNavigation { get; set; }
        public Chambre NumChambreNavigation { get; set; }
    }
}
