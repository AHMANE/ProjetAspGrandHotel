using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GrandHotel.Data;
using GrandHotel.Models;
using Microsoft.AspNetCore.Identity;

namespace GrandHotel.Controllers
{
    public class ClientsController : Controller
    {
        private readonly GrandHotelDbContext _context;
        private readonly UserManager<ApplicationUser> _user;

        public ClientsController(GrandHotelDbContext context, UserManager<ApplicationUser> user)
        {
            _user = user;
            _context = context;

        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Client.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .SingleOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public async Task<IActionResult> Create()
        {
            var user = await _user.GetUserAsync(User);
            @ViewBag.Email = user.Email;
            var client = _context.Client.Include(a=>a.Adresse).Include(t=>t.Telephone).Where(c => c.Email == user.Email).FirstOrDefault();
            if(client!=null)
            {
                client.Tel = client.Telephone[0];
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Civilite,Nom,Prenom,Email,CarteFidelite,Societe,Adresse,Tel")] Client client)
        //{
        //    var uniqueTel = _context.Telephone.Where(t => t.Numero == client.Tel.Numero).FirstOrDefault();
        //    if (ModelState.IsValid && uniqueTel==null)
        //    {
        //        var user = await _user.GetUserAsync(User);
        //        client.Email = user.Email;
        //        client.Telephone.Add(client.Tel);
        //        if(client.Adresse.Rue==null || client.Adresse.Ville==null || client.Adresse.CodePostal == null)
        //        {
        //            client.Adresse.Rue = "non renseigné";
        //            client.Adresse.CodePostal = "00000";
        //            client.Adresse.Ville = "non renseigné";
        //        }
        //        _context.Add(client);  

        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(HomeController.Index),"Home");
        //        //return RedirectToAction(nameof(ClientsController.Edit),client.Id);
        //        //return RedirectToAction(nameof(HomeController.Index), "Home");
        //    }
        //    ViewBag.ErreurTelephone = "le numero " + client.Tel.Numero + " est déjà utilisé, veuillez en saisir un nouveau";
        //    return View(client);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Civilite,Nom,Prenom,Email,CarteFidelite,Societe,Adresse,Telephone")] Client client)
        {
            
            var uniqueTel = _context.Telephone.Where(t => t.Numero == client.Tel.Numero).FirstOrDefault();
            var clientBase = _context.Client.Where(c => c.Email == client.Email).FirstOrDefault();
            if (ModelState.IsValid && uniqueTel==null && clientBase==null )
            {
                var user = await _user.GetUserAsync(User);
                client.Email = user.Email;

                if (client.Adresse.Rue == null || client.Adresse.Ville == null || client.Adresse.CodePostal == null)
                {
                    client.Adresse.Rue = "non renseigné";
                    client.Adresse.CodePostal = "00000";
                    client.Adresse.Ville = "non renseigné";
                }
                _context.Add(client);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            if (uniqueTel != null)
            {
                ViewBag.ErreurTelephone = "le numero " + client.Telephone[0].Numero + " est déjà utilisé, veuillez en saisir un nouveau";
            }
            if(clientBase!=null)
                return RedirectToAction(nameof(HomeController.Index), "Home");
            if(uniqueTel !=null)
            {
                ViewBag.ErreurTelephone = "le numero " + client.Tel.Numero + " est déjà utilisé, veuillez en saisir un nouveau";
            }
            return View(client);         
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.SingleOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            return View("Views/Manage/EditCoordonnees.cshtml", client);
            //return View("Views/Manage/EditCoordonnees.cshtml", client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Civilite,Nom,Prenom,Email,CarteFidelite,Societe")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .SingleOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var client = _context.Client.Where(m => m.Id == id).FirstOrDefault();
            var adresse = _context.Adresse.Where(m => m.IdClient == id).FirstOrDefault();
            var listTel = _context.Telephone.Where(m => m.IdClient == id).ToList();
            //var telephone = await _context.Telephone.SingleOrDefaultAsync(m => m.IdClient == id);
            //var adresse = await _context.Adresse.SingleOrDefaultAsync(m => m.IdClient == id);
            _context.Client.Remove(client);
            _context.Adresse.Remove(adresse);
            foreach (var tel in listTel)
            {
                _context.Telephone.Remove(tel);
            }
           
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.Id == id);
        }
    }
}
