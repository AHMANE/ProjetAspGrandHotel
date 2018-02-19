using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandHotel.Models;

namespace GrandHotel.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public FileResult Download()
        {
            string fileName = "DossierAppli.rar";
            byte[] fileBytes = System.IO.File.ReadAllBytes($"wwwroot/Appli_Cliente/DossierAppli.rar");
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }


    }
}
