using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandHotel.Models
{
    public class ContactVM
    {
        [StringLength(100)]
        [Required(ErrorMessage = "Le nom est requis")]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [EmailAddress(ErrorMessage = "Le mail est incorrect")]
        [Required(ErrorMessage = "L'email est requis")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "L'objet du message est requis")]
        [Display(Name = "Sujet")]
        public string Objet { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}
