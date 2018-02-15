using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandHotel.Models
{
    public partial class Chambre
    {
        public Chambre()
        {
            Reservation = new HashSet<Reservation>();
            TarifChambre = new HashSet<TarifChambre>();
        }
        [Display(Name = "Numéro de chambre ")]
        public short Numero { get; set; }
        public byte Etage { get; set; }
        public bool Bain { get; set; }
        public bool? Douche { get; set; }
        public bool? Wc { get; set; }
        [Display(Name = "Nombre de lit")]
        public byte NbLits { get; set; }
        public short? NumTel { get; set; }
        [Display(Name = "prix de la chmabre")]
        [NotMapped]
        public decimal PrixChambre { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Display(Name = "prix Total du séjour")]
        [NotMapped]
        public decimal PrixTotal { get; set; }
        public ICollection<Reservation> Reservation { get; set; }
        public ICollection<TarifChambre> TarifChambre { get; set; }
    }
}
