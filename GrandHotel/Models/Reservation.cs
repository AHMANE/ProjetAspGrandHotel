using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandHotel.Models
{
    public partial class Reservation
    {
        [Display(Name = "Numéro de chambre ")]

        public short NumChambre { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime Jour { get; set; }
        public int IdClient { get; set; }
        [Display(Name = "Nombre de personnes ")]
        public byte NbPersonnes { get; set; }
        public byte HeureArrivee { get; set; }
        public bool? Travail { get; set; }

        public Client IdClientNavigation { get; set; }
        public Calendrier JourNavigation { get; set; }
        public Chambre NumChambreNavigation { get; set; }
       
        [NotMapped]
        public short NumeroDeChambre { get; set; }
        [Display(Name = "Nombre de nuit ")]
        [NotMapped]
        public int NombreDeNuit { get; set; }

        // Stockage des donnée de la chambre

        
    }
}
