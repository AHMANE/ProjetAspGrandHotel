using System;
using System.Collections.Generic;

namespace GH_API_Web.Models
{
    public partial class Calendrier
    {
        public Calendrier()
        {
            Reservation = new HashSet<Reservation>();
        }

        public DateTime Jour { get; set; }

        public ICollection<Reservation> Reservation { get; set; }
    }
}
