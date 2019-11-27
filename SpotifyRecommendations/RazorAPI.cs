using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyRecommendations
{
    public class RazorAPI
    {
        public static async Task<AnalysedText> AnalyseText(string title, string text)
        {
            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("text", title + ":" + text));
            nvc.Add(new KeyValuePair<string, string>("extractors", "entities"));
            var req = new HttpRequestMessage(HttpMethod.Post, "https://api.textrazor.com") { Content = new FormUrlEncodedContent(nvc) };
            var res = await Http.Client.SendAsync(req);
            var html = await res.Content.ReadAsStringAsync();
            Debug.WriteLine(html);
            return JsonConvert.DeserializeObject<AnalysedText>(html);
        }

        public static void Auth(string apikey)
        {

            Http.Client.DefaultRequestHeaders.Add("X-TextRazor-Key", apikey);
        }
    }
}
