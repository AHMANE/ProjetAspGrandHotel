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
    public class BOL
    {
        //static HttpClient client = new HttpClient();

        //// Appel à la liste des Clients par requête Get
        //public static async Task RunAsyncListe()
        //{
        //    // Modifier le port selon les besoins
        //    client.BaseAddress = new Uri("http://localhost:57019/");
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));

        //    try
        //    {
        //        // Creation d'une liste de Clients
        //        List<Client> listeClients = new List<Client>();

        //        Console.WriteLine("Envoie de la requête");
        //        // envoi de la requete Get
                
        //        listeClients = await GetClientsAsync("http://localhost:57019/api/Clients");

        //        foreach (Client cli in listeClients)
        //        {
        //            Console.WriteLine($" Id : {cli.Id} - {cli.Civilite} {cli.Nom} - Email : {cli.Email}");
        //        }


        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //    Menu.AddOption("0", "Retour à la page d'Accueil.", () => GH_API_ClientApp.Instance.NavigateTo(typeof(Page_Accueil)));
        //    Menu.AddOption("1", "Rechercher un client avec par son Id", () => GH_API_ClientApp.Instance.NavigateTo(typeof(Page_RechercherClientId)));
        //    Menu.AddOption("2", "Rechercher un client par un Nom", () => GH_API_ClientApp.Instance.NavigateTo(typeof(Page_RechercherClientNom)));
        //    Menu.AddOption("3", "Créer un Client", () => GH_API_ClientApp.Instance.NavigateTo(typeof(Page_CreerUnClient)));
        //    Menu.AddOption("4", "Supprimer un Client", () => GH_API_ClientApp.Instance.NavigateTo(typeof(Page_SupprimerUnClient)));
        //}

        //static async Task<List<Client>> GetClientsAsync(string path)
        //{
        //    List<Client> listeCli = null;
        //    HttpResponseMessage response = await client.GetAsync(path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        listeCli = await response.Content.ReadAsAsync<List<Client>>();
        //    }
        //    return listeCli;
        //}


    }
}
