using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.PaperManifest.InsatsuBusuSettei.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>
        /// 画面ヘッダタイトル
        /// </summary>
        public static readonly string FORM_HEADER_TITLE = "印刷部数の設定";

        /// <summary>
        /// 印刷部数の初期値
        /// </summary>
        public static readonly string INSATSU_BUSU_DEFAULT = "1";

        /// <summary>
        /// 印刷部数が「0」の場合
        /// </summary>
        public static readonly int INSATSU_BUSU_ZERO = 0;

        /// <summary>
        /// 単票区分
        /// </summary>
        public static readonly int INSATST_MODE_TANHYOU = 1;

        /// <summary>
        /// 実行ボタン
        /// </summary>
        public static readonly string BUTTON_JIKKOU_NAME = "[F9]\n印刷登録";

        /// <summary>
        /// 閉じる
        /// </summary>
        public static readonly string BUTTON_CLOSE_NAME = "[F12]\n閉じる";

        /// <summary>
        /// 印刷
        /// </summary>
        public static readonly string BUTTON_PRINT_NAME = "[F1]\n印刷";

        /// <summary>
        /// 登録
        /// </summary>
        public static readonly string BUTTON_TOROKU_NAME = "[F4]\n登録";

        /// <summary>
        /// 印刷設定
        /// </summary>
        public static readonly string BUTTON_OPEN_PRINT_NAME = "[F11]\n印刷設定";

        /// <summary>
        /// 産廃マニフェスト（単票）
        /// </summary>
        public static readonly string REPORT_NAME_CHOKKOU_TANHYOU = "産廃マニフェスト（単票）";

        /// <summary>
        /// 産廃マニフェスト（連票）
        /// </summary>
        public static readonly string REPORT_NAME_CHOKKOU_RENHYOU = "産廃マニフェスト（連票）";

        /// <summary>
        /// 建廃マニフェスト（単票）
        /// </summary>
        public static readonly string REPORT_NAME_KENHAI_TANHYOU = "建廃マニフェスト（単票）";

        /// <summary>
        /// 建廃マニフェスト（連票）
        /// </summary>
        public static readonly string REPORT_NAME_KENHAI_RENHYOU = "建廃マニフェスト（連票）";

        /// <summary>
        /// 積替マニフェスト（単票）
        /// </summary>
        public static readonly string REPORT_NAME_SEKITAI_TANHYOU = "積替マニフェスト（単票）";

        /// <summary>
        /// 積替マニフェスト（連票）
        /// </summary>
        public static readonly string REPORT_NAME_SEKITAI_RENHYOU = "積替マニフェスト（連票）";

        /// <summary>
        /// 変換36進数使用
        /// </summary>
        public static readonly char[] BASES = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9','A', 'B', 
                                                   'C', 'D', 'E', 'F', 'G', 'H', 'I','J', 'K', 'L', 'M', 'N','O', 'P',
                                                   'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' }; 


        /// <summary>
        /// 変換10進数使用
        /// </summary>
        public static readonly string BASES_36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// グリッドカラム
        /// </summary>
        public enum enumFormKbn
        {
            /// <summary></summary>
            None = 0,
            /// <summary>直行</summary>
            Chokkou,
            /// <summary>建廃</summary>
            Kenhai,
            /// <summary>積替</summary>
            Sekitai,
        }

        /// <summary>
        /// 画面チェック区分
        /// </summary>
        public enum enumChkKbn
        {
            /// <summary>
            /// 全てチェック
            /// </summary>
            All = 0,

            /// <summary>
            /// 印刷部数のみ
            /// </summary>
            InsatsuOnly ,

            /// <summary>
            /// 交付番号のみ
            /// </summary>
            KoufuOnly,

        }

        /// <summary>
        /// 画面ボタン区分
        /// </summary>
        public enum enumButtonKbn
        {
            /// <summary>
            /// 全てチェック
            /// </summary>
            All = 0,

            /// <summary>
            /// 印刷
            /// </summary>
            Print,

            /// <summary>
            /// 登録
            /// </summary>
            Toroku,

        }

        /// <summary>
        /// 画面チェックエラー区分
        /// </summary>
        public enum enumChkErrorKbn
        {
            /// <summary>
            /// エラーなし
            /// </summary>
            None = 0,

            /// <summary>
            /// 印刷部数のめ
            /// </summary>
            Insatsu,

            /// <summary>
            /// 交付番号のめ
            /// </summary>
            Koufu,

        }

        /// <summary>実行結果</summary>
        internal enum JikkouResult
        {
            /// <summary>はいを押された</summary>
            YES,
            /// <summary>いいえを押された</summary>
            NO,
            /// <summary>エラー（印刷設定なし）</summary>
            ERROR,
            /// <summary>エラー（データ重複）</summary>
            DUPLICATION
        }

        /// <summary>印刷モード</summary>
        internal enum InsatsuMode
        {
            /// <summary>単票</summary>
            Single = 1,
            /// <summary>連票</summary>
            Multi = 2
        }
    }
}
