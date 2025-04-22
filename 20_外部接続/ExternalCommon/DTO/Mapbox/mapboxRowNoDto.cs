using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox
{
    /// <summary>
    /// HTMLに順番を表示するために使用
    /// </summary>
    [DataContract]
    public class mapboxRowNoDto
    {
        [DataMember(Name = "id")]
        public string id { get; set; }
        [DataMember(Name = "type")]
        public string type { get; set; }
        [DataMember(Name = "source")]
        public mapboxRowNoSource source { get; set; }
        [DataMember(Name = "layout")]
        public mapboxRowNoLayout layout { get; set; }
        [DataMember(Name = "paint")]
        public mapboxRowNoPaint paint { get; set; }
    }

    [DataContract]
    public class mapboxRowNoSource
    {
        [DataMember(Name = "type")]
        public string type { get; set; }
        [DataMember(Name = "data")]
        public mapboxRowNoData data { get; set; }
    }

    [DataContract]
    public class mapboxRowNoData
    {
        [DataMember(Name = "type")]
        public string type { get; set; }
        [DataMember(Name = "features")]
        public List<mapboxRowNoFeatures> features { get; set; }
    }

    [DataContract]
    public class mapboxRowNoFeatures
    {
        [DataMember(Name = "type")]
        public string type { get; set; }
        [DataMember(Name = "geometry")]
        public mapboxRowNoGeometry geometry { get; set; }
        [DataMember(Name = "properties")]
        public mapboxRowNoProperties properties { get; set; }
    }

    [DataContract]
    public class mapboxRowNoGeometry
    {
        [DataMember(Name = "type")]
        public string type { get; set; }
        [DataMember(Name = "coordinates")]
        public List<double> coordinates { get; set; }
    }

    [DataContract]
    public class mapboxRowNoProperties
    {
        [DataMember(Name = "name")]
        public string name { get; set; }
        [DataMember(Name = "color")]
        public string color { get; set; }
        [DataMember(Name = "image")]
        public string image { get; set; }
    }

    [DataContract]
    public class mapboxRowNoLayout
    {
        [DataMember(Name = "visibility")]
        public string visibility { get; set; }
        [DataMember(Name = "icon-image")]
        public string iconImage { get; set; }
        [DataMember(Name = "icon-size")]
        public double iconSize { get; set; }
        [DataMember(Name = "text-field")]
        public string textField { get; set; }
        [DataMember(Name = "text-offset")]
        public List<double> textOffset { get; set; }
        [DataMember(Name = "text-size")]
        public double textSize { get; set; }
        [DataMember(Name = "text-anchor")]
        public string textAnchor { get; set; }
        [DataMember(Name = "icon-allow-overlap")]
        public bool iconAllowOverlap { get; set; }
        [DataMember(Name = "text-allow-overlap")]
        public bool textAllowOverlap { get; set; }
    }

    [DataContract]
    public class mapboxRowNoPaint
    {
        [DataMember(Name = "text-color")]
        public List<string> textColor { get; set; }
        [DataMember(Name = "icon-color")]
        public List<string> iconColor { get; set; }
    }
}
