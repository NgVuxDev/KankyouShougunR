using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.ExternalConnection.FileUpload
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    class ConstCls
    {
        /// <summary>チェックボックスセル名</summary>
        public static readonly string CELL_CHECKBOX = "CHECKBOX";

        /// <summary>アップロード可能な拡張子リスト</summary>
        public static readonly string[] EXTENSION_KYOKA_LIST = {".pdf", ".png", ".bmp", ".jpg", ".jpeg", ".gif"};

        /// <summary>容量チェック時の警告を出力する割合設定</summary>
        /// <remarks>「0.01～1」の範囲(1%～100%)で設定願います</remarks>
        internal static readonly double CAPACITY_WARNING_RANGE = 0.8;

        /// <summary>表示区分：1.アップ済みファイル</summary>
        internal static readonly string HYOUJI_KBN_UP = "1";
        /// <summary>表示区分：2.ローカルファイル</summary>
        internal static readonly string HYOUJI_KBN_LOCAL = "2";
    }
}
