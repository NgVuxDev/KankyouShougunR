using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.PaperManifest.ManifestPatternTouroku.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>
        /// 画面ヘッダタイトル
        /// </summary>
        public static readonly string FORM_HEADER_TITLE = "パターン登録";

        /// <summary>
        /// 画面コメント
        /// </summary>
        public static readonly string FORM_COMMENT = "パターン名を入力してください。";

        /// <summary>
        /// 登録ボタン
        /// </summary>
        public static readonly string BUTTON_JIKKOU_NAME = "[F9]\r\n登録";

        /// <summary>
        /// 閉じるボタン
        /// </summary>
        public static readonly string BUTTON_CLOSE_NAME = "[F12]\r\n閉じる";

        /// <summary>
        /// 一次マニフェスト
        /// </summary>
        public static readonly string ITIJI_MANIFEST_KBN = "一次";

        /// <summary>
        /// 一次マニフェスト以外（2次）
        /// </summary>
        public static readonly string NIJI_MANIFEST_KBN = "二次";

        /// <summary>
        /// 一次マニフェスト区分
        /// 0：一次マニフェスト　1:一次マニフェスト以外（2次）
        /// </summary>
        public enum enumFirstManifestKbn
        {
            /// <summary>
            /// 0：一次マニフェスト
            /// </summary>
            ItijiManifestKbn=0,

            /// <summary>
            /// 1:一次マニフェスト以外（2次）
            /// </summary>
            NijiManifestKbn
        }

        /// <summary>
        /// 必須入力チェックのエラー区分
        /// </summary>
        public enum enumChkErKbn
        {
            /// <summary>
            /// 正常の場合
            /// </summary>
            None=0,

            /// <summary>
            /// フリガナ　空白エラーの場合
            /// </summary>
            PatternNameKana,

            /// <summary>
            /// フリガナ　カタカナエラーの場合
            /// </summary>
            PatternNameKanaKana,
            
            /// <summary>
            /// パターン名　空白エラーの場合
            /// </summary>
            PatternName
        }
    }
}
