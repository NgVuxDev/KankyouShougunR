using System.Collections.Generic;

namespace r_framework.Setting
{
    public class GridSetting
    {
        public string GridName { get; set; }

        public List<int> CellWidth { get; set; }

        public GridSetting()
        {
            this.CellWidth = new List<int>();
        }

        public bool Equals(GridSetting other)
        {
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
