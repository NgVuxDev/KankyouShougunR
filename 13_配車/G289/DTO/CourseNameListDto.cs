using System;

namespace Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku
{
    /// <summary>
    /// コース名称取得条件Dto
    /// </summary>
    public class CourseNameListDto
    {
        /// <summary>
        /// 作業日
        /// </summary>
        public DateTime SagyouDate { get; set; }

        /// <summary>
        /// 曜日
        /// </summary>
        public int DayCd { get; set; }

        /// <summary>
        /// コース名称CD
        /// </summary>
        public string CourseNameCd { get; set; }

        /// <summary>
        /// 拠点CD
        /// </summary>
        public string KyotenCd { get; set; }
    }
}
