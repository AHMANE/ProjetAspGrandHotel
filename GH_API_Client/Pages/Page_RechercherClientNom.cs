using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GH_API_Client.Pages
{
    class Page_RechercherClientNom : MenuPage
    {
        private static HttpClient client;

        public Page_RechercherClientNom() : base("Page de recherche d'un client à partir de son nom", false)
        {
            Menu.AddOption("0", "Rechercher un client à partir de son Nom", () => RechercherClientNom());
            Menu.AddOption("1", "Retour à la page d'Accueil.", () => GH_API_ClientApp.Instance.NavigateTo(typeof(Page_Accueil)));
            //Menu.AddOption("2", "Page d'Affichage de la liste des Clients", () => GH_API_ClientApp.Instance.NavigateTo(typeof(Page_ListeDesClients)));
        }

        private void RechercherClientNom()
        {
            string nomClient;
            Output.WriteLine("Saisissez un Nom de client. ");
            nomClient = Input.Read<string>("Nom : ");

            string url = "http://localhost:57019/api/Clients/Nom/" + nomClient;

            using (client = new HttpClient())
            {
                List<Client> clis = RunAsyncClient(url).Result;

                foreach (Client cli in clis)
                {
                    // Affichage du client
                    Console.WriteLine($" Id : {cli.Id} - {cli.Civilite} {cli.Nom} - Email : {cli.Email}");
                }
                Console.WriteLine("\n");
            }
        }

        // Appel à la liste des Clients par requête Get
        private static async Task<List<Client>> RunAsyncClient(string path)
        {
            List<Client> clie = new List<Client>();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                clie = await response.Content.ReadAsAsync<List<Client>>();
            }

            return clie;
        }
    }
}
