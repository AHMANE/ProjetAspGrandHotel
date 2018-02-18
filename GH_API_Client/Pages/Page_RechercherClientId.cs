using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GH_API_Client.Pages
{
    class Page_RechercherClientId : MenuPage
    {
        private static HttpClient client;

        public Page_RechercherClientId() : base("Page de recherche d'un Client à partir de son Id", false)
        {
            Menu.AddOption("0", "Rechercher un client à partir de son Id", () => RechercherClientId());
            Menu.AddOption("1", "Retour à la page d'Accueil.", () => GH_API_ClientApp.Instance.NavigateTo(typeof(Page_Accueil)));
            //Menu.AddOption("2", "Page d'Affichage de la liste des Clients", () => GH_API_ClientApp.Instance.NavigateTo(typeof(Page_ListeDesClients)));
        }

        private void RechercherClientId()
        {
            int idClient;
            Output.WriteLine("Saisissez un Id de client. ");
            idClient = Input.Read<int>("Id Client : ");

            string url = "http://localhost:57019/api/Clients/" + idClient;

            using (client = new HttpClient())
            {
                Client cli = RunAsyncClient(url).Result;

                // Affichage du client
                Console.WriteLine($" Id : {cli.Id} - {cli.Civilite} {cli.Nom} - Email : {cli.Email}");
                Console.WriteLine("\n");
            }
        }

        // Appel à la liste des Clients par requête Get
        public static async Task<Client> RunAsyncClient(string path)
        {
            Client clie = new Client();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                clie = await response.Content.ReadAsAsync<Client>();
            }

            return clie;
        }
    }
}
