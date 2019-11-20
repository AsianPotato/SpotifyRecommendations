using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyRecommendations
{
    class RazerApi
    {
        public static async Task<AnalysedText> AnalyseText(string text)
        {
            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("text", File.ReadAllText("test.txt")));
            nvc.Add(new KeyValuePair<string, string>("extractors", "phrases"));
            var req = new HttpRequestMessage(HttpMethod.Post, "http://api.textrazor.com") { Content = new FormUrlEncodedContent(nvc) };
            var res = await Http.Client.SendAsync(req);
            return JsonConvert.DeserializeObject<AnalysedText>(await res.Content.ReadAsStringAsync());
        }
    }
}
