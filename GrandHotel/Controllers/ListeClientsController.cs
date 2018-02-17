//CODE DE NIRY RALISON//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GrandHotel.Data;
using GrandHotel.Models;
using System.Data.SqlClient;


//--------------------------------------------------CONTROLLER LISTECLIENTS----------------------------------------//

namespace GrandHotel.Controllers
{
    public class ListeClientsController : Controller
    {
        private readonly GrandHotelDbContext _context;

        //------------Instanciation du DbContext-------------------//
        public ListeClientsController(GrandHotelDbContext context)
        {
            _context = context;
        }

        //----------------//- GET: ListeClients-------------------//
        //--------- Action appelée depuis la vue Index------------//


        //-----Recupération des données de la liste des clients (Id/Nom/Email/Reservation)---//
        public async Task<IActionResult> Index(int Id)
        {
            var clients = await _context.Client.Include("Reservation").ToListAsync();

            foreach( Client cli in clients)
            {
                cli.ReservationEnCours = cli.Reservation.Where(r => r.Jour > DateTime.Today).Count();
            }

            //--------------AFFICHAGE DE LA VUE------------------//

            return View(clients);
        }

        //---- Action pour afficher clients selon leur 1ère lettre de nom-----//
        public async Task<IActionResult> ListByFirstLetter(char id)
        {
            var client = await _context.Client.Include("Reservation").Where(c => c.Nom[0] == id).OrderBy(c => c.Nom).AsNoTracking().ToListAsync();
            foreach (Client cli in client)
            {
                cli.ReservationEnCours = cli.Reservation.Where(r => r.Jour > DateTime.Today).Count();
            }

            //--------------AFFICHAGE DE LA VUE------------------//
            return View("Index", client);
        }


        //------------------------------------------PERSPECTIVES D'AMELIORATION POUR MODIFIER/AJOUTER/DETAIL DE CHAMBRES--------------------

        //public async Task<IActionResult> Reservation (bool EtatReservation)
        //{
        //    var resEnCours= @ "select count(Jour) from Reservation where IdClient = '67' and Jour > '2018-02-14'";
        //    return View ();
        //}

        //// GET: ListeClients/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var client = await _context.Client
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (client == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(client);
        //}

        //// GET: ListeClients/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ListeClients/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Civilite,Nom,Prenom,Email,CarteFidelite,Societe")] Client client)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(client);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(client);
        //}

        //// GET: ListeClients/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var client = await _context.Client.SingleOrDefaultAsync(m => m.Id == id);
        //    if (client == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(client);
        //}

        //// POST: ListeClients/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Civilite,Nom,Prenom,Email,CarteFidelite,Societe")] Client client)
        //{
        //    if (id != client.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(client);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ClientExists(client.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(client);
        //}

        //// GET: ListeClients/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var client = await _context.Client
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (client == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(client);
        //}

        //// POST: ListeClients/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var client = await _context.Client.SingleOrDefaultAsync(m => m.Id == id);
        //    _context.Client.Remove(client);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ClientExists(int id)
        //{
        //    return _context.Client.Any(e => e.Id == id);
        //}
        //------------------------------------------PERSPECTIVES D'AMELIORATION POUR MODIFIER/AJOUTER/DETAIL DE CHAMBRES--------------------
    }
}
