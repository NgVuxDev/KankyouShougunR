using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Message
{
    /// <summary>
    /// メッセージ区分
    /// </summary>
    public enum MESSAGE_KUBUN : int
    {
        /// <summary>無し</summary>
        NONE = 0,
        /// <summary>確認</summary>
        CONFIRM = 1,
        /// <summary>警告</summary>
        WARN = 2,
        /// <summary>エラー</summary>
        ERROR = 3,
        /// <summary>インフォメーション</summary>
        INFO = 4,
    }

    /// <summary>
    /// メッセージ区分拡張
    /// </summary>
    public static class MESSAGE_KUBUNExt
    {
        /// <summary>
        /// メッセージ区分を日本語名にて取得する
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string ToKubunString(this MESSAGE_KUBUN e)
        {
            switch (e)
            {
                case MESSAGE_KUBUN.CONFIRM:
                    return "確認";
                case MESSAGE_KUBUN.WARN:
                    return "警告";
                case MESSAGE_KUBUN.ERROR:
                    //return "エラー";
                    return "アラート"; //受入 No892での指摘
                case MESSAGE_KUBUN.INFO:
                    return "インフォメーション";
            }
            return String.Empty;
        }
    }
}
