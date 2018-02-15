using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GH_API_Client.Pages
{
    class Page_CreerUnClient : MenuPage
    {
        private static HttpClient client;

        public Page_CreerUnClient() : base(" Page de Création d'un Client", false)
        {
            Menu.AddOption("0", "Créer un client", () => CreerClient());
            Menu.AddOption("1", "Retour à la page d'Accueil.", () => GH_API_ClientApp.Instance.NavigateTo(typeof(Page_Accueil)));
        }

        private void CreerClient()
        {
            Output.WriteLine("Création du nouveau client. ");
            Client nouveauClient = new Client();
            nouveauClient.Civilite = Input.Read<string>("Civilité : ");
            while (nouveauClient.Civilite != "M" && nouveauClient.Civilite != "Mme" && nouveauClient.Civilite != "Mlle")
            {
                nouveauClient.Civilite = Input.Read<string>("Veuillez saisir la Civilité ( M ou Mme ou Mlle) : ");
            }

            nouveauClient.Nom = Input.Read<string>("Nom : ");
            nouveauClient.Prenom = Input.Read<string>("Prenom : ");
            nouveauClient.Email = Input.Read<string>("Email : ");
            nouveauClient.CarteFidelite = Input.Read<bool>("Carte de Fidelité (tapez true OU false) : ");
            while (nouveauClient.CarteFidelite != false && nouveauClient.CarteFidelite != true)
            {
                nouveauClient.Civilite = Input.Read<string>("Veuillez saisir true si vous avez une Carte de Fidélité sinon tapez false : ");
            }
            nouveauClient.Societe = Input.Read<string>("Société : ");

            using (client = new HttpClient())
            {
                // Modifier le port selon les besoins
                client.BaseAddress = new Uri("http://localhost:57019/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var url = CreateClientAsync(nouveauClient).Result;
                Console.WriteLine($"Client créé à l'url {url}");
                Console.WriteLine("\n");
            }
        }

        // requete Post création client
        private static async Task<Uri> CreateClientAsync(Client clie)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Clients", clie);
            response.EnsureSuccessStatusCode();

            // retourne l'uri de la ressource créée
            return response.Headers.Location;
        }

    }
}
