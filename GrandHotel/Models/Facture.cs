using System;
using System.Collections.Generic;
using System.ComponentModel;

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
        [DisplayName("Date de la facture")]
        public DateTime DateFacture { get; set; }
        [DisplayName("Date de paiement")]
        public DateTime? DatePaiement { get; set; }
        public string CodeModePaiement { get; set; }

        [DisplayName("Mode de paiement")]
        public ModePaiement CodeModePaiementNavigation { get; set; }
        public Client IdClientNavigation { get; set; }
        public ICollection<LigneFacture> LigneFacture { get; set; }
    }
}
