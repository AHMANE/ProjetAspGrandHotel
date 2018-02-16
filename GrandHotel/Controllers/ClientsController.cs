﻿using System;
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
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Civilite,Nom,Prenom,Email,CarteFidelite,Societe,Adresse,Tel")] Client client)
        {
            if (ModelState.IsValid)
            {
                var user = await _user.GetUserAsync(User);
                client.Email = user.Email;
                client.Telephone.Add(client.Tel);
                _context.Add(client);  
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                //return RedirectToAction(nameof(ClientsController.Edit),client.Id);
                //return RedirectToAction(nameof(HomeController.Index), "Home");
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
            return View(client);
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
            foreach(var tel in listTel)
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
