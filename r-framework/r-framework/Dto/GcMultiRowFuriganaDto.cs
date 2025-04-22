using System;
using GrapeCity.Win.MultiRow;

namespace r_framework.Dto
{
    /// <summary>
    /// プロパティにてプルダウン表示を行うためのDto
    /// </summary>
    [Serializable]
    public class GcMultiRowFuriganaDto
    {
        /// <summary>
        /// GcMultiRow名
        /// </summary>
        public string GcMultiRowName { get; set; }

        /// <summary>
        /// セル名
        /// </summary>
        public string CellName { get; set; }

        /// <summary>
        /// フリガナを自動設定するセル名
        /// </summary>
        public string FuriganaAutoSetCellName { get; set; }
    }
}
