
namespace r_framework.Utility
{
    /// <summary>
    /// 文字列操作クラス
    /// </summary>
    public static class StringUtility
    {

        /// <summary>
        /// 最終文字列が特定の文字の場合は削除を行う
        /// </summary>
        /// <param name="str">チェック対象文字列</param>
        /// <param name="deleteStr">削除対象文字</param>
        /// <returns>削除処理後の文字列</returns>
        public static bool DeleteLastStr(ref string str, string deleteStr)
        {

            //最終文字が「.」かをチェック、「.」の場合は削除を行う
            if (str.IndexOf(".") + 1 == str.Length)
            {
                str = str.Replace(deleteStr, "");
                return true;
            }
            return false;
        }

        /// <summary>
        /// 特定文字で区切った最終文字列が特定文字の羅列の場合は削除を行う
        /// </summary>
        /// <param name="str">チェック対象文字列</param>
        /// <param name="deleteStr">削除対象文字</param>
        /// <param name="separateStr">区切り文字</param>
        /// <returns>加工処理後の文字列</returns>
        public static bool DeleteLastStr(ref string str, string deleteStr, char separateStr)
        {

            //「.」以降がすべて0の場合は削除を行う
            var splitStr = str.Split(separateStr);
            if (splitStr.Length >= 2)
            {
                var checkStr = splitStr[1].Replace(deleteStr, "");

                if (checkStr.Length == 0)
                {
                    str = splitStr[0];
                    return true;
                }
            }

            return false;

        }

    }
}
