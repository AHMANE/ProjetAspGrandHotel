using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandHotel.Models
{
    public partial class Facture
    {
        public Facture()
        {
            LigneFacture = new HashSet<LigneFacture>();
        }

        public int Id { get; set; }
        public int IdClient { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Date de la facture")]
        public DateTime DateFacture { get; set; }
        [DisplayName("Date de paiement")]
        [DataType(DataType.Date)]
        public DateTime? DatePaiement { get; set; }
        public string CodeModePaiement { get; set; }

        [DisplayName("Mode de paiement")]
        public ModePaiement CodeModePaiementNavigation { get; set; }
        public Client IdClientNavigation { get; set; }
        public ICollection<LigneFacture> LigneFacture { get; set; }

        
     
    }
}
