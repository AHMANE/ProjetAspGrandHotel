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

//--------------------------------------------------CONTROLLER LISTECHAMBRES----------------------------------------//

namespace GrandHotel.Controllers
{
    public class ListeChambresController : Controller
    {
        private readonly GrandHotelDbContext _context;
        

        //------------Instanciation du DbContext-------------------//
        public ListeChambresController(GrandHotelDbContext context)
        {
            _context = context;
        }

        //----------------- GET: ListeChambres--------------------//
        //--------- Action appelée depuis la vue Index------------//


        public async Task<IActionResult> Index(int etat)
        //------------Filtrage selon disponibilité-----------------//
        {
            ViewBag.Etats = new Dictionary<int, string>()
            {
                { 0, "Toutes" },
                { -1, "Occupées" },
                { 1, "Disponibles" }
            };

            //----- Mémorisation de valeurs de filtres saisies------//
            ViewBag.EtatSelec = etat;
            var listChambres = new List<Chambre>();
            var listeDeschambresTotal = _context.Chambre.Include(c => c.TarifChambre).ThenInclude(t => t.CodeTarifNavigation).ToList();

            //---------Liste de toutes les chambres version1----------------//
            //if (etat == 0)
            //{
            //    string req = @"select TF.NumChambre, T.Prix 
            //                from Tarif T inner join TarifChambre TF on (T.Code=TF.CodeTarif)
            //                where year(T.DateDebut)=year(GETDATE())";

            //    using (var conn = (SqlConnection)_context.Database.GetDbConnection())
            //    {
            //        var cmd = new SqlCommand(req, conn);
            //        await conn.OpenAsync();

            //        using (var sdr = await cmd.ExecuteReaderAsync())
            //        {
            //            while (sdr.Read())
            //            {
            //                var c = new Chambre();
            //                c.Numero = (short)sdr["NumChambre"];
            //                c.Prix = (decimal)sdr["Prix"];

            //                listChambres.Add(c);
            //            }
            //        }
            //    }

            //    foreach (Chambre c in listChambres)
            //    {
            //        c.Disponibilite = "Voir filtrage";
            //    }
            //}

            //---------Liste de toutes les chambres version2----------------//
            if (etat == 0)
            {
                bool hasChambre = false;
                //--------------Requete pour Recuperer toute les chambre occupé--------//
                string req = @"select C.Numero,T.Prix
                               from Reservation R 
                               inner join Chambre C  on (R.NumChambre=C.Numero)
                               inner join TarifChambre Tc on Tc.NumChambre = C.Numero
                               inner join Tarif T on T.Code = Tc.CodeTarif
                               where Jour=cast(GETDATE() as date) and year(T.DateDebut)=year(GETDATE())";

                using (var conn = (SqlConnection)_context.Database.GetDbConnection())
                {
                    var cmd = new SqlCommand(req, conn);
                    await conn.OpenAsync();

                    using (var sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (sdr.Read())
                        {
                            var c = new Chambre();
                            c.Numero = (short)sdr["Numero"];
                            c.Prix = (decimal)sdr["Prix"];

                            listChambres.Add(c);
                        }
                    }
                }
                foreach (Chambre c in listChambres)
                {
                    c.Disponibilite = "Occupée";
                }
                //--------Parcourir Toute les chambre de l'hotel et verifier si elles appartiennent à liste des chambres occupé---////
                foreach(Chambre c in listeDeschambresTotal)
                {
                    foreach(Chambre ocupe in listChambres)
                    {
                        if(c.Numero==ocupe.Numero)
                        {
                            hasChambre = true || hasChambre;
                        }                        
                    }
                    if(!hasChambre)
                    {
                        c.Disponibilite = "Disponible";
                        c.Prix = c.TarifChambre.Where(d => d.CodeTarifNavigation.DateDebut.Year == DateTime.Today.Year).FirstOrDefault().CodeTarifNavigation.Prix;
                        listChambres.Add(c);
                    }
                    hasChambre = false;
                    
                }
            }


            //------------Liste des chambres occupées------------//

            if (etat == -1)
            {
                string req = @"select C.Numero,T.Prix
                               from Reservation R 
                               inner join Chambre C  on (R.NumChambre=C.Numero)
                               inner join TarifChambre Tc on Tc.NumChambre = C.Numero
                               inner join Tarif T on T.Code = Tc.CodeTarif
                               where Jour=cast(GETDATE() as date) and year(T.DateDebut)=year(GETDATE())";

                using (var conn = (SqlConnection)_context.Database.GetDbConnection())
                {
                    var cmd = new SqlCommand(req, conn);
                    await conn.OpenAsync();

                    using (var sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (sdr.Read())
                        {
                            var c = new Chambre();
                            c.Numero = (short)sdr["Numero"];
                            c.Prix = (decimal)sdr["Prix"];

                            listChambres.Add(c);
                        }
                    }
                }
                foreach (Chambre c in listChambres)
                {
                    c.Disponibilite = "Occupée";
                }
            }

            //------------Liste des chambres disponibles-----------//

            if (etat == 1)
            {
                string req = @"select TF.NumChambre, T.Prix 
                            from Tarif T inner join TarifChambre TF on (T.Code=TF.CodeTarif)
                            where year(T.DateDebut)=year(GETDATE())
                            except
                            select C.Numero,T.Prix
                            from Reservation R 
                            inner join Chambre C  on (R.NumChambre=C.Numero)
                            inner join TarifChambre Tc on Tc.NumChambre = C.Numero
                            inner join Tarif T on T.Code = Tc.CodeTarif
                            where Jour=cast(GETDATE() as date) and year(T.DateDebut)=year(GETDATE())";

                using (var conn = (SqlConnection)_context.Database.GetDbConnection())
                {
                    var cmd = new SqlCommand(req, conn);
                    await conn.OpenAsync();

                    using (var sdr = await cmd.ExecuteReaderAsync())
                    {
                        while (sdr.Read())
                        {
                            var c = new Chambre();
                            c.Numero = (short)sdr["NumChambre"];
                            c.Prix = (decimal)sdr["Prix"];

                            listChambres.Add(c);
                        }
                    }
                }

                foreach (Chambre c in listChambres)
                {
                    c.Disponibilite = "Disponible";
                }
            }

            //--------------AFFICHAGE DE LA VUE------------------------//

            return View(listChambres);
        }


        //------------------------------------------PERSPECTIVES D'AMELIORATION POUR MODIFIER/AJOUTER/DETAIL DE CHAMBRES--------------------

        // GET: ListeChambres/Details/5
        //public async Task<IActionResult> Details(short? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var chambre = await _context.Chambre
        //        .SingleOrDefaultAsync(m => m.Numero == id);
        //    if (chambre == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(chambre);
        //}

        //// GET: ListeChambres/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ListeChambres/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Numero,Etage,Bain,Douche,Wc,NbLits,NumTel")] Chambre chambre)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(chambre);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(chambre);
        //}

        //// GET: ListeChambres/Edit/5
        //public async Task<IActionResult> Edit(short? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var chambre = await _context.Chambre.SingleOrDefaultAsync(m => m.Numero == id);
        //    if (chambre == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(chambre);
        //}

        //// POST: ListeChambres/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(short id, [Bind("Numero,Etage,Bain,Douche,Wc,NbLits,NumTel")] Chambre chambre)
        //{
        //    if (id != chambre.Numero)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(chambre);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ChambreExists(chambre.Numero))
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
        //    return View(chambre);
        //}

        //// GET: ListeChambres/Delete/5
        //public async Task<IActionResult> Delete(short? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var chambre = await _context.Chambre
        //        .SingleOrDefaultAsync(m => m.Numero == id);
        //    if (chambre == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(chambre);
        //}

        //// POST: ListeChambres/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(short id)
        //{
        //    var chambre = await _context.Chambre.SingleOrDefaultAsync(m => m.Numero == id);
        //    _context.Chambre.Remove(chambre);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ChambreExists(short id)
        //{
        //    return _context.Chambre.Any(e => e.Numero == id);
        //}
        //------------------------------------------PERSPECTIVE D'AMELIORATION POUR MODIFIER/AJOUTER/DETAIL DE CHAMBRES--------------------

    }
}