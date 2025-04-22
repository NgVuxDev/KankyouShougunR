using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox
{
    /// <summary>
    /// HTMLにマーカーを表示するために使用
    /// </summary>
    [DataContract]
    public class mapboxMarkerDto
    {
        [DataMember(Name = "id")]
        public string id { get; set; }
        [DataMember(Name = "type")]
        public string type { get; set; }
        [DataMember(Name = "source")]
        public mapboxMarkerSource source { get; set; }
        [DataMember(Name = "layout")]
        public mapboxMarkerLayout layout { get; set; }
    }

    [DataContract]
    public class mapboxMarkerSource
    {
        [DataMember(Name = "type")]
        public string type { get; set; }
        [DataMember(Name = "data")]
        public mapboxMarkerData data { get; set; }
    }

    [DataContract]
    public class mapboxMarkerData
    {
        [DataMember(Name = "type")]
        public string type { get; set; }
        /// <summary>MapboxAralListDtoのmapboxArrayListを参照</summary>
        [DataMember(Name = "features")]
        public List<mapboxArrayList> features { get; set; }
    }

    [DataContract]
    public class mapboxMarkerLayout
    {
        [DataMember(Name = "visibility")]
        public string visibility { get; set; }
        [DataMember(Name = "icon-image")]
        public string iconImage { get; set; }
        [DataMember(Name = "icon-size")]
        public double iconSize { get; set; }
        [DataMember(Name = "icon-padding")]
        public int iconPadding { get; set; }
        [DataMember(Name = "icon-allow-overlap")]
        public bool iconAllowOverlap { get; set; }
    }
}
