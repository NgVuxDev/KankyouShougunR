using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Master.DenshiShinseiJyuyoudoHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>
        /// 重要度CD
        /// </summary>
        public static readonly string JYUYOUDO_CD = "JYUYOUDO_CD";

        /// <summary>
        /// 申請初期値
        /// </summary>
        public static readonly string JYUYOUDO_DEFAULT = "JYUYOUDO_DEFAULT";

        /// <summary>
        /// タイムスタンプ
        /// </summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>
        /// デリートフラグ
        /// </summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public class ExceptionErrMsg
        {
            public const string HAITA = "排他エラーが発生しました。";
            public const string REIGAI = "例外エラーが発生しました。";
        }
    }
}
