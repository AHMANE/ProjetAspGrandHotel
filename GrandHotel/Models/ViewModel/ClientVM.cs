using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandHotel.Models
{
    public partial class ClientVM
    {
        public Client Client_TelPlus { get; set; }

        public Telephone TelephonePlus { get; set; }

 //       public List<Telephone> ListeTelephone { get; set; }
    }
}
