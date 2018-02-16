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

namespace GrandHotel.Controllers
{
    public class ListeChambresController : Controller
    {
        private readonly GrandHotelDbContext _context;

        public ListeChambresController(GrandHotelDbContext context)
        {
            _context = context;
        }

        // GET: ListeChambres
        public async Task<IActionResult> Index()
        {
            var listChambres = new List<Chambre>();

            string req = @"select C.Numero,T.Prix, R.Jour
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
                        c.Disponibilite= (DateTime)sdr["Jour"];

                        listChambres.Add(c);
                    }
                }
            }

            return View(listChambres);
        }


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
    }
}