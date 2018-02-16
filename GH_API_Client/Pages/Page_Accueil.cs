using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GH_API_Client.Pages
{
    public class Page_Accueil : MenuPage
    {
        public Page_Accueil() : base("Accueil", false)
        {

            Menu.AddOption("0", "Quitter l'application", () => Environment.Exit(0));
            Menu.AddOption("1", "Page d'Affichage de la liste des Clients", () => GH_API_ClientApp.Instance.NavigateTo(typeof(Page_ListeDesClients)));
            Menu.AddOption("2", "Page Rechercher un client avec par son Id",  () => GH_API_ClientApp.Instance.NavigateTo(typeof(Page_RechercherClientId)));
            Menu.AddOption("3", "page Rechercher un client par un Nom", () => GH_API_ClientApp.Instance.NavigateTo(typeof(Page_RechercherClientNom)));
            Menu.AddOption("4", "Page Créer un Client",  () => GH_API_ClientApp.Instance.NavigateTo(typeof(Page_CreerUnClient)));
            Menu.AddOption("5", "Page Supprimer un Client",  () => GH_API_ClientApp.Instance.NavigateTo(typeof(Page_SupprimerUnClient)));
        }
    }
}
