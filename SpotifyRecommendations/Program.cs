using System;
using System.Diagnostics;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using System.Globalization;
using System.IO;

namespace SpotifyRecommendations
{
    class Program
    {
        static void Main(string[] args)
        {
            //add the razer api key to all future requests made by the client
            Http.Client.DefaultRequestHeaders.Add("X-TextRazor-Key", "17839221580e7053352fd25f8012a5f33c111d3f1922565b437ac229 ");
            Example();
            Console.ReadLine();

        }

        async static void Example()
        {
            //getting country of user
            string name = RegionInfo.CurrentRegion.EnglishName;
            var analysedtext = await RazerApi.AnalyseText(File.ReadAllText("test.txt"));

            var auth = new ImplicitGrantAuth(
                  "7f08980f1dae4f3d98a40d44ef235b03",
                  "http://localhost:4002",
                  "http://localhost:4002",
                  Scope.UserReadPrivate
                );
                auth.AuthReceived += async (sender, payload) =>
                {
                    auth.Stop(); // `sender` is also the auth instance
                    var api = new SpotifyWebAPI()
                    {
                        TokenType = payload.TokenType,
                        AccessToken = payload.AccessToken
                    };
                    // FeaturedPlaylists playlists = api.GetFeaturedPlaylists();
                    //Console.WriteLine(playlists.Message);
                    //playlists.Playlists.Items.ForEach(playlist => Console.WriteLine(playlist.Name));

                    //getting playlists from categories and outputting names and links of the playlist.
                    CategoryPlaylist playlists = api.GetCategoryPlaylists("party");
                    playlists.Playlists.Items.ForEach(playlist => Console.WriteLine("Playlist Name: " + playlist.Name + ",\nLink: " + playlist.Uri));

                    // Do requests with API client
                    var newsapiresults = await NewsApi.SearchByKeyword("bitcoin", name);
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
