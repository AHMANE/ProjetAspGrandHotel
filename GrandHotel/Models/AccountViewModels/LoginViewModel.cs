using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandHotel.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mot de Passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Se Souvenir de moi?")]
        public bool RememberMe { get; set; }
    }
}
