using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyRecommendations
{

    public class AnalysedText
    {
        public Response response { get; set; }
        public float time { get; set; }
        public bool ok { get; set; }
    }

    public class Response
    {
        public string language { get; set; }
        public bool languageIsReliable { get; set; }
        public Entity[] entities { get; set; }
    }

    public class Entity
    {
        public int id { get; set; }
        public string[] type { get; set; }
        public int[] matchingTokens { get; set; }
        public string entityId { get; set; }
        public float confidenceScore { get; set; }
        public string wikiLink { get; set; }
        public string matchedText { get; set; }
        public float relevanceScore { get; set; }
        public string entityEnglishId { get; set; }
        public int startingPos { get; set; }
        public int endingPos { get; set; }
        public string unit { get; set; }
        public string[] freebaseTypes { get; set; }
        public string freebaseId { get; set; }
        public string wikidataId { get; set; }
        public string crunchbaseId { get; set; }
        public string permid { get; set; }
    }



}
