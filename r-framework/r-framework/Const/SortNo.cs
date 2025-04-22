using System;

namespace r_framework.Const
{
    /// <summary>
    /// ソートコンボボックスの生成用定数
    /// </summary>
    public enum SortNo
    {
        ASC = 1,
        DESC = 2,
    }

    /// <summary>
    /// ソート拡張メソッド
    /// </summary>
    public static class SortNoExt
    {
        /// <summary>
        /// 昇順・降順文字列
        /// </summary>
        /// <param name="e">ソートクラス</param>
        /// <returns>ソート名</returns>
        public static string ToString(this SortNo e)
        {
            switch (e)
            {
                case SortNo.ASC:
                    return "昇順";
                case SortNo.DESC:
                    return "降順";
            }
            return String.Empty;
        }
    }
}
