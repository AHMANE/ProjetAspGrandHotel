using GrandHotel.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandHotel.Controllers
{
    public interface IDataReservation
    {
        Task<IActionResult> Index(DateTime JourDebutSejour, int NombreDeNuit, byte NbPersonnes, byte HeureArrivee, bool? Travail);
        Task<IActionResult> VéficationDisponi(DateTime JourDebutSejour, int NombreDeNuit, byte NbPersonnes, byte HeureArrivee, bool? Travail);
        Task<IActionResult> Create([Bind("NumChambre,Jour,IdClient,NbPersonnes,HeureArrivee,Travail")] Reservation reservation);
        Task<IActionResult> Edit(short? id);
        Task<IActionResult> Edit(short id, [Bind("NumChambre,Jour,IdClient,NbPersonnes,HeureArrivee,Travail")] Reservation reservation);
        Task<IActionResult> Delete(short? id);
        Task<IActionResult> DeleteConfirmed(short id);
        //bool ReservationExists(short id);
    }
}
