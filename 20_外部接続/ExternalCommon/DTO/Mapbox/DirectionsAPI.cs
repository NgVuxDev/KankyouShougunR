using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Mapbox.DirectionsAPI
{
    /// <summary>
    /// ルートの情報を返すAPIで使用
    /// </summary>
    public class DirectionsAPI : IApiDto
    {
        public List<Route> routes { get; set; }
        public List<Waypoint> waypoints { get; set; }
        public string code { get; set; }
        public string uuid { get; set; }
    }

    public class Leg
    {
        public string summary { get; set; }
        public List<object> steps { get; set; }
        public string distance { get; set; }
        public string duration { get; set; }
        public string weight { get; set; }
    }

    public class Geometry
    {
        public List<List<string>> coordinates { get; set; }
        public string type { get; set; }
    }

    public class Route
    {
        public string weight_name { get; set; }
        public List<Leg> legs { get; set; }
        public Geometry geometry { get; set; }
        public string distance { get; set; }
        public string duration { get; set; }
        public string weight { get; set; }
    }

    public class Waypoint
    {
        public string distance { get; set; }
        public string name { get; set; }
        public List<string> location { get; set; }
    }

}
