using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox
{
    /// <summary>
    /// HTMLにルートを表示するために使用
    /// </summary>
    [DataContract]
    public class mapboxRootDto
    {
        [DataMember(Name = "id")]
        public string id { get; set; }
        [DataMember(Name = "type")]
        public string type { get; set; }
        [DataMember(Name = "source")]
        public mapboxRootSource source { get; set; }
        [DataMember(Name = "layout")]
        public mapboxRootLayout layout { get; set; }
        [DataMember(Name = "paint")]
        public mapboxRootPaint paint { get; set; }
    }

    [DataContract]
    public class mapboxRootSource
    {
        [DataMember(Name = "type")]
        public string type { get; set; }
        [DataMember(Name = "data")]
        public mapboxRootData data { get; set; }
    }

    [DataContract]
    public class mapboxRootData
    {
        [DataMember(Name = "type")]
        public string type { get; set; }
        [DataMember(Name = "geometry")]
        public mapboxRootGeometry geometry { get; set; }
    }

    [DataContract]
    public class mapboxRootGeometry
    {
        [DataMember(Name = "type")]
        public string type { get; set; }
        [DataMember(Name = "coordinates")]
        public List<List<double>> coordinates { get; set; }
    }

    [DataContract]
    public class mapboxRootLayout
    {
        [DataMember(Name = "visibility")]
        public string visibility { get; set; }
        [DataMember(Name = "line-join")]
        public string lineJoin { get; set; }
        [DataMember(Name = "line-cap")]
        public string lineCap { get; set; }

    }

    [DataContract]
    public class mapboxRootPaint
    {
        [DataMember(Name = "line-color")]
        public string lineColor { get; set; }
        [DataMember(Name = "line-width")]
        public double lineWidth { get; set; }
    }
}
