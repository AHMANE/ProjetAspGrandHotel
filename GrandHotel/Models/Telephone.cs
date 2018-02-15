using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GrandHotel.Models
{
    public partial class Telephone
    {
        public string Numero { get; set; }
        public int IdClient { get; set; }
        [DisplayName("Type de Numero")]
        public string CodeType { get; set; }
        public bool Pro { get; set; }

        public Client IdClientNavigation { get; set; }
    }
}
