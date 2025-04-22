using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Shougun.Core.ElectronicManifest.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>
        /// 確認の初期値
        /// </summary>
        public static readonly string KAKUNIN_DEFAULT = "1";

        /// <summary>
        /// 画面タイトル表示メッセージ
        /// </summary>
        public static readonly string TITLE_MSG = "内容を確認の上、承認または否認を行ってください。";

        /// <summary>
        /// 通知コード
        /// </summary>
        public static readonly string[] TuuchiCd = 
            new string[] { "110", "113", "118", "121", "203", "206", "303", "306" };

        /// <summary>
        /// 承認列のヘッダ
        /// </summary>
        public static readonly string SHOUNIN_HEADERTEXT = "承認";

        /// <summary>
        /// 否認列のヘッダ
        /// </summary>
        public static readonly string HININ_HEADERTEXT = "否認";

        /// <summary>
        /// 確認列のヘッダ
        /// </summary>
        public static readonly string KAKUNIN_HEADERTEXT = "確認";

        /// <summary>
        /// 承認列のヘッダ
        /// </summary>
        public static readonly string SHOUNIN_TOOLTIPTEXT = "承認する場合はチェックしてください";

        /// <summary>
        /// 否認列のヘッダ
        /// </summary>
        public static readonly string HININ_TOOLTIPTEXT = "否認する場合はチェックしてください";

        /// <summary>
        /// 確認列のヘッダ
        /// </summary>
        public static readonly string KAKUNIN_TOOLTIPTEXT = "確認済とする場合はチェックしてください";

        /// <summary>
        /// 通知履歴明細画面タイトル
        /// </summary>
        public static readonly string TUUCHI_MEISAI_TITLE = "通知履歴明細";

        /// <summary>
        /// 承認列インデックス
        /// </summary>
        public static readonly int SHOUNIN_COLUMN_INDEX = 2;

        /// <summary>
        /// 否認列インデックス
        /// </summary>
        public static readonly int HININ_COLUMN_INDEX = 3;

        /// <summary>
        /// 確認列インデックス
        /// </summary>
        public static readonly int KAKUNIN_COLUMN_INDEX = 4;

        /// <summary>
        /// 通知区分
        /// </summary>
        public enum TsuuchiEnumKbn
        {
            /// <summary>
            /// 重要通知
            /// </summary>
            JyuuyouTsuuchi,

            /// <summary>
            /// お知らせ通知
            /// </summary>
            OshiraseTsuuchi,
        }
       
    }
}
