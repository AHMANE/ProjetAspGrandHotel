using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GH_API_Client.Pages
{
    class Page_SupprimerUnClient : MenuPage
    {
        private static HttpClient client;

        public Page_SupprimerUnClient() : base("Page de suppression d'un Client", false)
        {
            Menu.AddOption("0", "Supprimer un client", () => SupprimerClient());
            Menu.AddOption("1", "Retour à la page d'Accueil.", () => GH_API_ClientApp.Instance.NavigateTo(typeof(Page_Accueil)));
        }

        private void SupprimerClient()
        {
            Output.WriteLine("Vous allez supprimer un client, quel est son Id ? ");
            int idClientToDelete = Input.Read<int>("Id Client : ");

            using (client = new HttpClient())
            {
                // Modifier le port selon les besoins
                client.BaseAddress = new Uri("http://localhost:57019/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Client clientDeleted = DeleteClientAsync(idClientToDelete).Result;

                Console.WriteLine($"Client supprimé : Id {clientDeleted.Id} - {clientDeleted.Civilite} {clientDeleted.Nom}");
                Console.WriteLine("\n");
                //var statusCode = DeleteClientAsync(idClientToDelete);
                //Console.WriteLine($"Client supprimé (statut HTTP = {statusCode})");

            }
        }

        private static async Task<Client> DeleteClientAsync(int id)
        {
            Client cli = null;
            HttpResponseMessage response = await client.DeleteAsync($"api/Clients/{id}");
            if (response.IsSuccessStatusCode)
            {
                cli = await response.Content.ReadAsAsync<Client>();
            }

            return cli;
        }

        //private static async Task<HttpStatusCode> DeleteClientAsync(int id)
        //{
        //    HttpResponseMessage response = await client.DeleteAsync($"api/Clients/{id}");
        //    return response.StatusCode;
        //}

    }
}
