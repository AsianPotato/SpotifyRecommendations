using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using SpotifyRecommendations;

namespace SpotifyRecommendationsUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool LoggedIn;
        string Region;
        public MainWindow()
        {
            InitializeComponent();
            Region = Spotify.GetRegion();
            RazorAPI.Auth("17839221580e7053352fd25f8012a5f33c111d3f1922565b437ac229");
            UserLogin();
        }
        SpotifyAPI.Web.SpotifyWebAPI SpotifyAPI = null;
        SpotifyAPI.Web.Models.PrivateProfile UserProfile = null;
        async Task UserLogin()
        {
            SpotifyAPI = await Spotify.UserLogin("7f08980f1dae4f3d98a40d44ef235b03");
            UserProfile = await SpotifyAPI.GetPrivateProfileAsync();
            txtUserProfile.Text = "Logged in as: " + UserProfile.DisplayName;
            lvArticles.SelectionChanged += SelectedArticleChanged;


        }

        void SelectedArticleChanged(object sender, System.EventArgs e)
        {
            
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearchTerm.Text) || string.IsNullOrWhiteSpace(txtSearchTerm.Text))
                return;
            DisplayNewsAPIResults(txtSearchTerm.Text);
            
        }

        private async void DisplayNewsAPIResults(string keyword)
        {
           
          

            var results = await NewsApi.SearchByKeyword(keyword, Region);
            lvArticles.ItemsSource = results.articles.GroupBy(a => a.title).Take(10);
            /*for (int i = 0; i < results.articles.Count && i < 10; i++)
            {
                //var article = results.articles[i];
                //listView.Items.Add(article);
                
                
                //cmbNewsResults.Items.Add(article.title);
            }*/
        }

        private void analyse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var grouping = (System.Linq.IGrouping<string, Article>)lvArticles.SelectedItem;
                var article = grouping.First();

                DisplaySpotifyResults(article);
            }
            catch
            {

            }
        }
        private async void OpenBrowserUrl(string url)
        {
            await Task.Run(() => { 
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.FileName = "cmd";
            proc.StartInfo.Arguments = "/C start " + url;
            proc.Start();
            });
        }
        private void view_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var grouping = (System.Linq.IGrouping<string, Article>)lvArticles.SelectedItem;
                var article = grouping.First();
                if (article == null)
                    return;

                OpenBrowserUrl(article.url);



            }
            catch
            {

            }

            
        }

        private async void DisplaySpotifyResults(Article article)
        {
            try
            {
                if (article == null)
                    return;


                var analysedtext = await RazorAPI.AnalyseText(article.title, article.content);
                var sortedentities = analysedtext.response.entities.OrderBy(a => a.confidenceScore).Take(1);
                string searchstring = "";
                foreach (var entity in sortedentities)
                {
                    searchstring += entity.matchedText;
                }
                Debug.WriteLine("Searchstring: " + searchstring);
                var result = await SpotifyAPI.SearchItemsAsync(searchstring, SearchType.All);
                var playlistresult = result.Playlists.Items.GroupBy(a => a.Name);
                Debug.WriteLine(JsonConvert.SerializeObject(playlistresult, Formatting.Indented));
                txtSpotifyResults.Text = "Playlists from keyword(" + searchstring + "):";
                lvSpotifyResults.ItemsSource = playlistresult;
                  
            }
            catch
            {

            }

            
            

           

        }

        private void playlist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Debug.WriteLine(lvSpotifyResults.SelectedItem);
                var grouping = (System.Linq.IGrouping <string, SimplePlaylist>)lvSpotifyResults.SelectedItem;
                if (grouping == null)
                    return;
                var playlist = grouping.FirstOrDefault();
                Debug.WriteLine(playlist.Name);
                OpenBrowserUrl(playlist.ExternalUrls.FirstOrDefault().Value);
            }
            catch
            {

            }

        }
    }
}
