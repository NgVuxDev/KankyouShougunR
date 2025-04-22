using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class S_MAPBOX_ACCESS_COUNT : SuperEntity
    {
        public string USER_NAME { get; set; }
        public string PC_NAME { get; set; }
        public string MENU_NAME { get; set; }
        public int ACCESS_COUNT { get; set; }
    }
}
