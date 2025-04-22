// $Id: ConstCls.cs 21312 2014-05-23 08:49:32Z seven1@bh.mbn.or.jp $

using System.Data;
using System;
namespace Shougun.Core.Master.TabOrderSettei.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>M_CONTENA_SHURUIのCONTENA_SHURUI_CD</summary>
        public static readonly string CONTENA_SHURUI_CD = "CONTENA_SHURUI_CD";

        /// <summary>M_CONTENA_SHURUIのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_CONTENA_SHURUIのDELETE_FLGフラグ</summary>
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

    /*public class TabDataCls
    {
        /// <summary>ID</summary>
        internal int DATA_ID  { get; set; }

        /// <summary>コントロール名</summary>
        internal string CTRL_NAME { get; set; }

        /// <summary>名称</summary>
        internal string NAME { get; set; }

        /// <summary>タブストップ</summary>
        internal bool STOP { get; set; }

        /// <summary>順番</summary>
        internal int NUMBER { get; set; }

    }*/
}
