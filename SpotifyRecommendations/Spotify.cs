using System;
using System.Diagnostics;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace SpotifyRecommendations
{
    public class Spotify
    {
        static void Main(string[] args)
        {
            //add the razer api key to all future requests made by the client
            RazorAPI.Auth("17839221580e7053352fd25f8012a5f33c111d3f1922565b437ac229");
            #if DEBUG
            RunTest();
            #endif
            Console.ReadLine();

        }
        public async static Task<SpotifyWebAPI> UserLogin(string apikey)
        {
            return await Task.Run(async() => { 

                var auth = new ImplicitGrantAuth(
                    apikey,
                    "http://localhost:4002",
                    "http://localhost:4002",
                    Scope.UserReadPrivate
                );
                SpotifyWebAPI api = null;
                auth.AuthReceived += async (sender, payload) =>
                {
                    auth.Stop(); // `sender` is also the auth instance
                    api = new SpotifyWebAPI()
                    {
                        TokenType = payload.TokenType,
                        AccessToken = payload.AccessToken
                    };

                };

                auth.Start(); // Starts an internal HTTP Server
                auth.OpenBrowser();
                while (api == null)
                    await Task.Delay(100);

                return api;

            });
        }
        public static string GetRegion()
        {
            return RegionInfo.CurrentRegion.EnglishName;
        }
        async static void RunTest()
        {
            //getting country of user
            string name = GetRegion();
            var analysedtext = await RazorAPI.AnalyseText("test", File.ReadAllText("test.txt"));

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
                var profile = await api.GetPrivateProfileAsync();
                Console.WriteLine(JsonConvert.SerializeObject(profile, Formatting.Indented));
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
                Console.WriteLine(newsapiresults);
                Console.WriteLine(newsapiresults.totalResults);
                foreach (var result in newsapiresults.articles)
                {
                    //Console.WriteLine();
                    Console.WriteLine(result.title);
                }
                   
                    
                var spotifyresults = await api.SearchItemsAsync("drake", SearchType.All);
                if (spotifyresults == null)
                    return;

                Console.WriteLine(JsonConvert.SerializeObject(spotifyresults, Formatting.Indented));
            };
             
            auth.Start(); // Starts an internal HTTP Server
            auth.OpenBrowser();          
        }
    }
}
