using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyRecommendations
{ 
    public class Word
    {
        public int position { get; set; }
        public int startingPos { get; set; }
        public int endingPos { get; set; }
        public string stem { get; set; }
        public string token { get; set; }
        public string partOfSpeech { get; set; }
    }

    public class Sentence
    {
        public int position { get; set; }
        public List<Word> words { get; set; }
    }

    public class NounPhras
    {
        public int id { get; set; }
        public List<int> wordPositions { get; set; }
    }

    public class Response
    {
        public List<Sentence> sentences { get; set; }
        public string language { get; set; }
        public bool languageIsReliable { get; set; }
        public List<NounPhras> nounPhrases { get; set; }
    }

    public class AnalysedText
    {
        public Response response { get; set; }
        public double time { get; set; }
        public bool ok { get; set; }
    }
}
