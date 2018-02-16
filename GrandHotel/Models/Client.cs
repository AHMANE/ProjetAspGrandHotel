using System;
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
        [Required]
        public string Civilite { get; set; }
        [Required(ErrorMessage ="Le nom est requis")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Le prenom est requis")]
        public string Prenom { get; set; }
        public string Email { get; set; }
        [Required]
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
