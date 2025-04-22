using System;
using GrapeCity.Win.MultiRow;

namespace r_framework.Dto
{
    /// <summary>
    /// プロパティにてプルダウン表示を行うためのDto
    /// </summary>
    [Serializable]
    public class GcMultiRowZeroPaddingDto
    {
        /// <summary>
        /// ゼロパディング対象のGcMultiRow名
        /// </summary>
        public string GcMultiRowName { get; set; }

        /// <summary>
        /// ゼロパディング対象のセル名
        /// </summary>
        public string CellName { get; set; }
        
        /// <summary>
        /// ゼロパディング文字数
        /// </summary>
        public int ZeroPaddingNumber { get; set; }
    }
}
