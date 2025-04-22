using GrapeCity.Win.MultiRow;

namespace r_framework.Dto
{
    public class MultiRowLocationDto
    {
        public int LocationX { get; set; }

        public int LocationY { get; set; }

        public Cell Cells { get; set; }

        /// <summary>
        /// 比較用メソッド
        /// </summary>
        public static int ComparisonX(MultiRowLocationDto condition1, MultiRowLocationDto condition2)
        {
            return condition1.LocationX.CompareTo(condition2.LocationX);
        }
    }
}
