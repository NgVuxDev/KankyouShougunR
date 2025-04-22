using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox.GeoCodingAPI
{
    /// <summary>
    /// 住所から緯度経度を返すAPIで使用
    /// </summary>
    public class GeoCodingAPI : IApiDto
    {
        public string type { get; set; }
        public List<string> query { get; set; }
        public List<Feature> features { get; set; }
        public string attribution { get; set; }
    }

    public class Properties
    {
        public string accuracy { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        public List<string> coordinates { get; set; }
    }

    public class Context
    {
        public string id { get; set; }
        public string wikidata { get; set; }
        public string text_ja { get; set; }
        public string language_ja { get; set; }
        public string text { get; set; }
        public string language { get; set; }
        public string short_code { get; set; }
    }

    public class Feature
    {
        public string id { get; set; }
        public string type { get; set; }
        public List<string> place_type { get; set; }
        public double relevance { get; set; }
        public Properties properties { get; set; }
        public string text_ja { get; set; }
        public string place_name_ja { get; set; }
        public string text { get; set; }
        public string place_name { get; set; }
        public List<double> center { get; set; }
        public Geometry geometry { get; set; }
        public List<Context> context { get; set; }
    }

}
