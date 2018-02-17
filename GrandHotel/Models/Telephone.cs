using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GrandHotel.Models
{
    public partial class Telephone
    {
        [DisplayName("Numero de Téléphone")]
        [Required]              
        [RegularExpression(@"^0[1-9]([-. ]?[0-9]{2}){4}", ErrorMessage ="Saisir un Numero de Telephone avec 10 chiffres et qui commence par 0")]
        public string Numero { get; set; }
        public int IdClient { get; set; }
        [DisplayName("Type de Numero"),Required(ErrorMessage ="Champ requis")]
        public string CodeType { get; set; }
        [Required(ErrorMessage ="Champ requis")]
        public bool Pro { get; set; }

        public Client IdClientNavigation { get; set; }
    }
}
