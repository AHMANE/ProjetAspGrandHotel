﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GrandHotel.Models
{
    public partial class Chambre
    {
        public Chambre()
        {
            Reservation = new HashSet<Reservation>();
            TarifChambre = new HashSet<TarifChambre>();
        }
        [Display(Name = "Numéro de chambre Disponible")]
        public short Numero { get; set; }
        public byte Etage { get; set; }
        public bool Bain { get; set; }
        public bool? Douche { get; set; }
        public bool? Wc { get; set; }
        public byte NbLits { get; set; }
        public short? NumTel { get; set; }

        public ICollection<Reservation> Reservation { get; set; }
        public ICollection<TarifChambre> TarifChambre { get; set; }
    }
}
