using SteamWebApiLib;
using SteamWebApiLib.Models;
using SteamWebAPI2;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace MOAD
{
    public partial class Form1 : Form
    {
        private List<string> searchResults;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text;
            searchResults = GetSearchResults(searchTerm);
            lstMods.Items.Clear();
            lstMods.Items.AddRange(searchResults.ToArray());
        }

        private List<string> GetSearchResults(string searchTerm)
        {
            List<string> results = new List<string>();

            // Replace these placeholders with your actual Steam API key and game ID
            string apiKey = "YOUR_STEAM_API_KEY";
            int gameId = 123456789; // Replace with your actual game ID

            // Create a SteamWebAPI client using the SteamWebAPI2 NuGet package
            SteamWebInterfaceFactory factory = new SteamWebInterfaceFactory(apiKey);
            var publishedFile = factory.CreateSteamWebInterface<PublishedFile>();
            var fileDetails = publishedFile.GetDetails(gameId, searchTerm).Result;
            if (fileDetails?.Data != null)
            {
                foreach (var fileDetail in fileDetails.Data)
                {
                    // Add the title of each mod to the results list
                    results.Add(fileDetail.Title);
                }
            }

            return results;
        }
    }
}
