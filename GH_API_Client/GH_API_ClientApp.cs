using GH_API_Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GH_API_Client
{
    class GH_API_ClientApp : ConsoleApplication
    {
        private static GH_API_ClientApp _instance;

        /// <summary>
        /// Obtient l'instance unique de l'application
        /// </summary>
        public static GH_API_ClientApp Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GH_API_ClientApp();

                return _instance;
            }
        }


        // Constructeur
        public GH_API_ClientApp()
        {
            // Définition des options de menu à ajouter dans tous les menus de pages
            MenuPage.DefaultOptions.Add(
               new Option("a", "Accueil", () => _instance.NavigateHome()));
        }


    }
}
