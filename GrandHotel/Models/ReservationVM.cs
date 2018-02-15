using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandHotel.Models
{
    public class ReservationVM : IValidatableObject
    {
        public List<Reservation> Reservations { get; set; }
        

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}"), Required(ErrorMessage ="Veuillez saisir une date")]
        public DateTime JourDebutSejour { get; set; }

 

        [Range(1,365, ErrorMessage ="Nombre de  nuits et de 365jours"), Required(ErrorMessage ="Veuillez saisir le nombre de nit souhaitées")]
        
        public int NombreDeNuit { get; set; }

        [Range(1,3, ErrorMessage ="Nombre de personnes dans une chambre est de 3"), Required(ErrorMessage ="Veuillez saisir le nombre de personnes souhaités")]
        public byte NbPersonnes { get; set; }

        [Range(1,24, ErrorMessage ="heure d'arrivée entre 1 et 24h "), Required(ErrorMessage ="Veuillez saisir votre heure d'arrivée")]
        public byte HeureArrivee { get; set; }

        public bool? Travail { get; set; }

        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            
            ReservationVM res = (ReservationVM)validationContext.ObjectInstance;
            
             
            
            if (res.JourDebutSejour < DateTime.Today.Date)
            {
                yield return new ValidationResult("la date reservation doit etre supérieur à la date d'Aujourdui ", new string[] { "JourDebutSejour" });
            }


        }


    }
}
