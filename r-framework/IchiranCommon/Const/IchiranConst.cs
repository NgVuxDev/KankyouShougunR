using System;

namespace Shougun.Core.Common.IchiranCommon.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public enum OUTPUT_KBN
    {
        /// <summary>なし</summary>
        NONE = 0,

        /// <summary>伝票</summary>
        DENPYOU = 1,

        /// <summary>明細</summary>
        MEISAI = 2,

        /// <summary>受入実績</summary>
        JISSEKI = 3,
    }

    /// <summary>
    /// 書式
    /// </summary>
    public class FORMAT_TYPE
    {
        /// <summary>なし</summary>
        public const string NONE = "0";

        /// <summary>数量</summary>
        public const string SUURYOU = "1";

        /// <summary>重量</summary>
        public const string JYURYOU = "2";

        /// <summary>単価</summary>
        public const string TANKA = "3";

        /// <summary>委託契約数量</summary>
        public const string ITAKU_KEIYAKU_SUURYOU = "4";

        /// <summary>委託契約単価</summary>
        public const string ITAKU_KEIYAKU_TANKA = "5";

        /// <summary>マニフェスト数量</summary>
        public const string MANIFEST_SUURYOU = "6";
    }

    /// <summary>
    /// 定数クラス
    /// </summary>
    public class PatternIchiranConst
    {
        /// <summary>OuptKbn_DenPyou</summary>
        public static readonly string HeaderTitle = "伝票パターン一覧";
        /// <summary>インフォメーション</summary>
        public static readonly string DialogTitle = "インフォメーション";
        /// <summary>エラー</summary>
        public static readonly string DialogTitleErr = "エラー";
        /// <summary>削除</summary>
        public static readonly string DelInfo = "削除";
        /// <summary>更新</summary>
        public static readonly string UpdInfo = "更新";

        //2013-11-25 Del ogawamut PT 東北
        ///// <summary>SearchEmptInfo</summary>
        //public static readonly string SearchEmptInfo = "該当検索条件に対して、検索結果は見つかりません。";

        /// <summary>ErrStop1</summary>
        public static readonly string ErrStop1 = "遷移先画面が未作成のため、遷移処理を中止します。";
        /// <summary>ErrStop2</summary>
        public static readonly string ErrStop2 = "遷移先の画面は準備中です。";
        /// <summary>ErrStop2</summary>
        public static readonly string ErrStop3 = "削除対象レコードを選択してください。";
        /// <summary>ErrStop4</summary>
        public static readonly string ErrStop4 = "重複しない表示順序";
        /// <summary>ErrStop5</summary>
        public static readonly string ErrStop5 = "デフォルトが未チェックです。";
        /// <summary>ErrStop6</summary>
        [Obsolete("メッセージID E093 を使ってください", true)]
        public static readonly string ErrStop6 = "データ更新時にエラーが発生しました。";
        /// <summary>ErrStop7</summary>
        [Obsolete("メッセージID E080 を使ってください", true)]
        public static readonly string ErrStop7 = "他のユーザーによって、関連するデータが更新されていました。\r\n再度検索を行い、メンテナンスをやり直してください。";
        /// <summary>ErrStop8</summary>
        public static readonly string ErrStop8 = "削除チェックされているレコードがあるため登録に失敗しました。";

        /// <summary>ToolTipText1</summary>
        public static readonly string ToolTipText1 = "削除したいパターンにチェックを付けてください";
        /// <summary>ToolTipText2</summary>
        public static readonly string ToolTipText2 = "デフォルト表示させたいパターンにチェックを付けてください";
        /// <summary>ToolTipText3</summary>
        public static readonly string ToolTipText3 = "【1～5】のいずれかで入力してください";
        /// <summary>ToolTipText4</summary>
        public static readonly string ToolTipText4 = "ダブルクリックすると一覧出力項目選択画面に切り替わります";

        /// <summary>OuptKbn_Meisai</summary>
        public static readonly string OuptKbn_Meisai = "明細";
        /// <summary>OuptKbn_DenPyou</summary>
        public static readonly string OuptKbn_DenPyou = "伝票";
        /// <summary>OuptKbn_Jisseki</summary>
        public static readonly string OuptKbn_Jisseki = "受入実績";
    }
}
