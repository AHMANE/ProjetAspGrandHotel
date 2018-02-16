using GH_API_Client.Pages;
using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GH_API_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            GH_API_ClientApp app = GH_API_ClientApp.Instance;
            app.Title = "GrandHotel Appli Client";

            // Ajout des pages
            Page accueil = new Page_Accueil();
            app.AddPage(accueil);
            Page creerUnClients = new Page_CreerUnClient();
            app.AddPage(creerUnClients);
            Page listeDesClients = new Page_ListeDesClients();
            app.AddPage(listeDesClients);
            Page rechercherClientId = new Page_RechercherClientId();
            app.AddPage(rechercherClientId);
            Page rechercherClientNom = new Page_RechercherClientNom();
            app.AddPage(rechercherClientNom);
            Page supprimerUnClient = new Page_SupprimerUnClient();
            app.AddPage(supprimerUnClient);

            // Affichage de la page d'accueil
            app.NavigateTo(accueil);
            app.Run();





        }



    }
}
