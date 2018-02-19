using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GrandHotel.Models;
using MimeKit;
using MailKit.Security;

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

        public IActionResult Contact(ContactVM contact)
        {
            if (!string.IsNullOrWhiteSpace(contact.Email) && !string.IsNullOrWhiteSpace(contact.Nom) && !string.IsNullOrWhiteSpace(contact.Message))
            {

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("GrandHotel", "from.gtmtest94@gmail.com"));
                //message.To.Add(new MailboxAddress("amanehafid@yahoo.fr"));
                message.To.Add(new MailboxAddress(contact.Email));
                message.To.Add(new MailboxAddress("gtmtest94@gmail.com"));

                message.Subject = $"[GrandHotel] {contact.Objet}";

                var builder = new BodyBuilder
                {
                    HtmlBody = $"<div><span style='font-weight: bold'>De</span> : {contact.Nom} </div><div><span style='font-weight: bold'>Mail</span> : {contact.Email}</div><div style='margin-top: 30px'>{contact.Message}</div>"
                };

                message.Body = builder.ToMessageBody();

                //mtpClient client = new SmtpClient(contact);
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {

                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                    client.Authenticate("gtmtest94@gmail.com", "gtmtest24031988");


                    client.Send(message);
                    ViewBag.message = "Merci ! Votre message a bien été envoyée.";

                    client.Disconnect(true);




                }
                // var cont = contact.Email.Remove(0);

                //ViewData["Message"] = "Your contact page.";
                //
                return View();
            }

            return View(contact);
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
