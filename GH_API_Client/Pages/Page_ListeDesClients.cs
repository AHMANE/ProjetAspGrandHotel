using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GH_API_Client.Pages
{
    class Page_ListeDesClients : MenuPage
    {
        private static HttpClient client;

        public Page_ListeDesClients() : base("Page d'Affichage de la liste Des Clients", false)
        {
            Menu.AddOption("0", "Afficher la liste des clients", () => AfficherListe());
            Menu.AddOption("1", "Retour à la page d'Accueil.", () => GH_API_ClientApp.Instance.NavigateTo(typeof(Page_Accueil)));
            //Menu.AddOption("3", "Créer un Client", () => GH_API_ClientApp.Instance.NavigateTo(typeof(Page_CreerUnClient)));
            //Menu.AddOption("4", "Supprimer un Client", () => GH_API_ClientApp.Instance.NavigateTo(typeof(Page_SupprimerUnClient)));
        }

        private void AfficherListe()
        {
            using (client = new HttpClient())
            {
                List<Client> listeClients = RunAsyncListe().Result;

                // Affichage des clients
                foreach (Client cli in listeClients)
                {
                    Console.WriteLine($" Id : {cli.Id} - {cli.Civilite} {cli.Nom} - Email : {cli.Email}");
                }
                Console.WriteLine("\n");
            }
        }

        // Appel à la liste des Clients par requête Get
        private static async Task<List<Client>> RunAsyncListe()
        {
            // Modifier le port selon les besoins
            client.BaseAddress = new Uri("http://localhost:57019/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            List<Client> listeClients = new List<Client>();

            try
            {
                var response = await client.GetAsync("http://localhost:57019/api/Clients");

                if (response.IsSuccessStatusCode)
                {
                    listeClients = await response.Content.ReadAsAsync<List<Client>>();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return listeClients;
        }

    }
}
