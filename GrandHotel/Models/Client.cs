﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandHotel.Models
{
    public partial class Client
    {
        public Client()
        {
            Facture = new HashSet<Facture>();
            Reservation = new HashSet<Reservation>();
            Telephone = new List<Telephone>();
        }

        public int Id { get; set; }
        public string Civilite { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public bool CarteFidelite { get; set; }
        public string Societe { get; set; }
        [NotMapped]
        public Telephone Tel { get; set; }

        public Adresse Adresse { get; set; }
        public ICollection<Facture> Facture { get; set; }

        [Display(Name = "Reservations")]
        public ICollection<Reservation> Reservation { get; set; }
        public IList<Telephone> Telephone { get; set; }

        [NotMapped]
        [Display(Name = "Reservations en cours")]
        public int ReservationEnCours{ get; set; }
        
    }
}
