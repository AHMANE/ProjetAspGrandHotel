using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandHotel.Models.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Votre Mot de Passe")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Le {0} doit contenir entre {2} et {1} cacractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nouveau Mot de Passe")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confimer le Nouveau Mot de Passe")]
        [Compare("NewPassword", ErrorMessage = "Les Mots de Passe ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
