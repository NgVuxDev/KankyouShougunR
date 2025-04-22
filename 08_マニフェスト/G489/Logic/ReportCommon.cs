using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Utility;

namespace Shougun.Core.PaperManifest.InsatsuBusuSettei
{
    internal static class ReportCommonUtility
    {
        /// <summary> 文字列の帳票データを文字列配列データへの変換を実行する </summary>
        /// <param name="tmp">帳票データを表す文字列</param>
        /// <returns>文字列配列の帳票データ</returns>
        public static string[] ReportSplit(string tmp)
        {
            string[] ret = new string[0];
            LogUtility.DebugMethodStart(tmp);
            // 値が空の項目を半角スペースに置き換える(""⇒" ")
            tmp = tmp.Replace("\"\"", "\" \"");

            // 先頭と末尾の"(ダブルコーテーション)を削除する
            // 先頭と末尾の空白を削除
            tmp = tmp.Trim();

            // 先頭と末尾以外を抽出(先頭と、末尾は"(ダブルコーテーション))
            tmp = tmp.Substring(1, tmp.Length - 2);

            // ","(ダブルコーテーション カンマ ダブルコーテーション)で区切って配列に格納
            string[] splt = { "\",\"" };
            ret = tmp.Split(splt, StringSplitOptions.RemoveEmptyEntries);

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 郵便記号を付加して返す ただしnullや空文字、空白のみの場合はそのまま返す
        /// </summary>
        /// <param name="postCd"></param>
        /// <returns></returns>
        public static string AddPostMark(string postCd)
        {
            if (string.IsNullOrWhiteSpace(postCd))
            {
                return postCd;
            }

            return "〒" + postCd;
        }

        /// <summary>
        /// No.を付加して返す ただしnullや空文字、空白のみの場合はそのまま返す
        /// </summary>
        /// <param name="postCd"></param>
        /// <returns></returns>
        public static string AddNoMark(string no)
        {
            if (string.IsNullOrWhiteSpace(no))
            {
                return no;
            }

            return "No." + no;
        }

    }
}
