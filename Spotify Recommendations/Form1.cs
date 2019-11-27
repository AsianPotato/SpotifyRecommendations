using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using SpotifyRecommendations;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Spotify_Recommendations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        async static void button2_Click(EventArgs e)
        {
            string name = RegionInfo.CurrentRegion.EnglishName;
            var analysedtext = await RazerApi.AnalyseText(File.ReadAllText("test.txt"));

            var auth = new ImplicitGrantAuth(
                  "7f08980f1dae4f3d98a40d44ef235b03",
                  "http://localhost:4002",
                  "http://localhost:4002",
                  Scope.UserReadPrivate
                );
          
                // FeaturedPlaylists playlists = api.GetFeaturedPlaylists();
                //Console.WriteLine(playlists.Message);
                //playlists.Playlists.Items.ForEach(playlist => Console.WriteLine(playlist.Name));

                //getting playlists from categories and outputting names and links of the playlist.
                CategoryPlaylist playlists = api.GetCategoryPlaylists("party");
                playlists.Playlists.Items.ForEach(playlist => Console.WriteLine("Playlist Name: " + playlist.Name + ",\nLink: " + playlist.Uri));

                // Do requests with API client
                var newsapiresults = await NewsApi.SearchByKeyword(TextBox., name);
                if (newsapiresults == null)
                    return;
                Debug.WriteLine(newsapiresults);
                Debug.WriteLine(newsapiresults.totalResults);
                foreach (var result in newsapiresults.articles)
                {
                    //Console.WriteLine();
                    Debug.WriteLine(result.title);
                }


                var spotifyresults = await api.SearchItemsAsync("drake", SearchType.All);
                if (spotifyresults == null)
                    return;

                Console.WriteLine(spotifyresults);
            };

            auth.Start(); // Starts an internal HTTP Server
            auth.OpenBrowser();
        }

    }
}
