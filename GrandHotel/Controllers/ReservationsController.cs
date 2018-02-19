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
using Microsoft.AspNetCore.Authorization;
using GrandHotel.Extensions;
using Microsoft.AspNetCore.Identity;

namespace GrandHotel.Controllers
{
    public class ReservationsController : Controller, IDataReservation
    {
        private readonly GrandHotelDbContext _context;
        private readonly UserManager<ApplicationUser> _user;
        public ReservationsController(GrandHotelDbContext context, UserManager<ApplicationUser>user)
        {
            _user = user;
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index(DateTime JourDebutSejour, int NombreDeNuit, byte NbPersonnes, byte HeureArrivee, bool? Travail)
        {
           
            ReservationVM tvm = new ReservationVM();
            tvm.JourDebutSejour = DateTime.Today;
            JourDebutSejour = DateTime.Today;


            

            // ne recuprère pas direct les données mais juste les données dont on a besoin
            IQueryable<Reservation> tac = _context.Reservation;
            if (Travail.HasValue)
                tac = tac.Where(s => s.Travail == Travail);
            if (ModelState.IsValid)
            {
                tac = tac.Where(s => s.Jour == JourDebutSejour && s.NbPersonnes == NbPersonnes && s.HeureArrivee == HeureArrivee);

            }

            tvm.Reservations = await tac.ToListAsync();

            //var grandHotelDbContext = _context.Reservation.Include(r => r.IdClientNavigation).Include(r => r.JourNavigation).Include(r => r.NumChambreNavigation);
            return View("Index", tvm);
           
        }
        //public async Task<IActionResult> VéficationDisponi(DateTime JourDebutSejour, int NombreDeNuit, byte NbPersonnes, byte HeureArrivee, bool? Travail)
        public async Task<IActionResult> VéficationDisponi(DateTime JourDebutSejour, int NombreDeNuit, byte NbPersonnes, byte HeureArrivee, bool? Travail)
        {
            
            //*****************************************Créationn d'un session pour stocker les valeurs saisies**********************************//
            List<ReserVationSession> reservations = new List<ReserVationSession>();
            for (int jours = 0; jours < NombreDeNuit; jours++)
            {
                ReserVationSession reservation = new ReserVationSession();
                reservation.Jour = JourDebutSejour.AddDays(jours);                
                reservation.NombreDeNuit = NombreDeNuit;
                reservation.NbPersonnes = NbPersonnes;
                reservation.HeureArrivee = HeureArrivee;
                
                reservation.Travail = Travail;
                reservations.Add(reservation);
                
            }

            HttpContext.Session.SetObjectAsJson("Resa", reservations);

            //*****************************************Fin_Créationn d'un session pour stocker les valeurs saisies**********************************//

            ViewBag.NbreNuits = NombreDeNuit;
            ViewBag.NbPersonnes = NbPersonnes;
            //ViewBag.Jour = JourDebutSejour;

            
            IQueryable<Reservation> tac = _context.Reservation;
            if (Travail.HasValue)
                tac = tac.Where(s => s.Travail == Travail);


            if (ModelState.IsValid)
            {
                ViewBag.Travail = Travail;

                // var myComplexObject = new ReservationVM();

                ReservationVM tvm = new ReservationVM();

                tvm.JourDebutSejour = DateTime.Today;
                tvm.Reservations = new List<Reservation>();
                var DateDebutNbreNuit = JourDebutSejour.AddDays(NombreDeNuit);
                // Requête SQL optimisée : on ramène uniquement les infos nécessaires
                string req = @"select Numero
                    from Chambre 
                    where NbLits BETWEEN @NbreNuits and '5'
                    except
                    select NumChambre
                    from Reservation
                    where   Jour BETWEEN @DateDebutSejour and @DateDebutNbreNuit";

                using (var conn = (SqlConnection)_context.Database.GetDbConnection())
                {

                    var cmd = new SqlCommand(req, conn);
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "NbreNuits", Value = NbPersonnes });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "DateDebutSejour", Value = JourDebutSejour });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "DateDebutNbreNuit", Value = DateDebutNbreNuit });
                    await conn.OpenAsync();
                  
                    using (var sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (sdr.Read())
                        {

                            var res = new Reservation();

                            res.NumeroDeChambre = (short)sdr["Numero"];
                            
                            tvm.Reservations.Add(res);


                        }
                        ViewBag.rest = tvm.Reservations.Count;
                        ViewBag.NbPersonnes = NbPersonnes;

                    }
                   
                }

                return View("Index", tvm);

            }

            return View("Index");
            
        }
       
        // **************************Afficher les détait de la chambre selectionnée Il prend en parametre Id=Numerod e chambre*****************************************//
        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(short id, int ids)
        {

          

            if (id == 0)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                
                var cmb = new List<Chambre>();
               
                // Requête SQL optimisée : on ramène uniquement les infos nécessaires
                string req = @" select Numero, Etage, Bain, WC, NbLits, Prix, Prix*@NbreNuit as PrixTotale
                            from Chambre
                            inner join TarifChambre on NumChambre=Numero
                            inner join Tarif on Code = CodeTarif
                            where Numero = @NumChambre and YEAR(DateDebut) = YEAR(GETDATE())
                            group by  Numero, Etage, Bain, WC, NbLits, Prix";

                using (var conn = (SqlConnection)_context.Database.GetDbConnection())
                {

                    var cmd = new SqlCommand(req, conn);
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "NumChambre", Value = id });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "NbreNuit", Value = ids });

                    await conn.OpenAsync();

                    using (var sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (sdr.Read())
                        {

                            var res = new Chambre();

                            res.Numero = (short)sdr["Numero"];
                            res.Etage = (byte)sdr["Etage"];
                            res.Bain = (bool)sdr["Bain"];
                            res.Wc = (bool)sdr["WC"];
                            res.NbLits = (byte)sdr["NbLits"];
                            res.PrixChambre = (decimal)sdr["Prix"];
                            res.PrixTotal = (decimal)sdr["PrixTotale"];

                            cmb.Add(res);

                            
                        }
                        
                    }
                    HttpContext.Session.SetObjectAsJson("Cham", cmb);
                }

                return View("Details", cmb);

            }
            return View("Details");

        }
        // ************************** Fin -Afficher les détait de la chambre selectionnée*****************************************//

        //[Authorize]
        public async Task<IActionResult> DetailsReservarion(short id)
        {
            var user = await _user.GetUserAsync(User);
            var client = _context.Client.Where(c => c.Email == user.Email).FirstOrDefault();
            var reservations = HttpContext.Session.GetObjectFromJson<List<ReserVationSession>>("Resa");
           
            
            ViewBag.Jour = reservations[0].Jour.Date;
            ViewBag.NbrePersonne = reservations[0].NbPersonnes;
            ViewBag.travail = reservations[0].Travail;
            ViewBag.NombreDeNuit = reservations[0].NombreDeNuit;
            ViewBag.IdClient = client.Id;

            //*******************Detail Chambre****//
            var ch = HttpContext.Session.GetObjectFromJson<List<Chambre>>("Cham");
            ViewBag.Numero = ch[0].Numero;
            ViewBag.Etage = ch[0].Etage;
            ViewBag.NbLits = ch[0].NbLits;
            ViewBag.PrixTotal = ch[0].PrixTotal;
            return View();
        }

        [Authorize]
        // GET: Reservations/Create
        public async Task<IActionResult> Create(short id)
        {
            var user = await _user.GetUserAsync(User);
            var client = _context.Client.Where(c => c.Email == user.Email).FirstOrDefault();
            var reservations = HttpContext.Session.GetObjectFromJson<List<ReserVationSession>>("Resa");
            Reservation reservation; 

            for (int res = 0; res < reservations.Count; res++)
            {
                reservation = new Reservation
                {
                    IdClient = client.Id,
                    HeureArrivee = reservations[res].HeureArrivee,
                    Jour = reservations[res].Jour,
                    NumChambre = id,
                    NbPersonnes = reservations[res].NbPersonnes,
                    Travail = reservations[res].Travail
                };
                if (ModelState.IsValid)
                {
                    reservations[res].NumChambre = id;
                    _context.Add(reservation);
                }
            }
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(DetailsReservarion));


        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumChambre,Jour,IdClient,NbPersonnes,HeureArrivee,Travail")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdClient"] = new SelectList(_context.Client, "Id", "Civilite", reservation.IdClient);
            ViewData["Jour"] = new SelectList(_context.Calendrier, "Jour", "Jour", reservation.Jour);
            ViewData["NumChambre"] = new SelectList(_context.Chambre, "Numero", "Numero", reservation.NumChambre);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.SingleOrDefaultAsync(m => m.NumChambre == id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["IdClient"] = new SelectList(_context.Client, "Id", "Civilite", reservation.IdClient);
            ViewData["Jour"] = new SelectList(_context.Calendrier, "Jour", "Jour", reservation.Jour);
            ViewData["NumChambre"] = new SelectList(_context.Chambre, "Numero", "Numero", reservation.NumChambre);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("NumChambre,Jour,IdClient,NbPersonnes,HeureArrivee,Travail")] Reservation reservation)
        {
            if (id != reservation.NumChambre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.NumChambre))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdClient"] = new SelectList(_context.Client, "Id", "Civilite", reservation.IdClient);
            ViewData["Jour"] = new SelectList(_context.Calendrier, "Jour", "Jour", reservation.Jour);
            ViewData["NumChambre"] = new SelectList(_context.Chambre, "Numero", "Numero", reservation.NumChambre);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.IdClientNavigation)
                .Include(r => r.JourNavigation)
                .Include(r => r.NumChambreNavigation)
                .SingleOrDefaultAsync(m => m.NumChambre == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var reservation = await _context.Reservation.SingleOrDefaultAsync(m => m.NumChambre == id);
            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(short id)
        {
            return _context.Reservation.Any(e => e.NumChambre == id);
        }
    }
}
