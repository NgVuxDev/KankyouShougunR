using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.Const
{
    public static class KenshuIshiranConstans
    {
        /// <summary>
        /// 範囲条件情報（戻り値）
        /// </summary>
        public struct ConditionInfo
        {
            /// <summary>
            /// 値格納フラグ
            /// </summary>
            public bool DataSetFlag;
            /// <summary>
            /// 拠点CD
            /// </summary>
            public String KyotenCD;
            /// <summary>
            /// 伝票日付範囲指定（開始日付
            /// </summary>
            public String DStartDay;
            /// <summary>
            /// 伝票日付範囲指定（終了日付
            /// </summary>
            public String DEndDay;
            /// <summary>
            /// 検収伝票日付範囲指定（開始日付
            /// </summary>
            public String KStartDay;
            /// <summary>
            /// 検収伝票日付範囲指定（終了日付
            /// </summary>
            public String KEndDay;
            /// <summary>
            /// 開始取引先CD
            /// </summary>
            public String StartTorihikisakiCD;
            /// <summary>
            /// 終了取引先CD
            /// </summary>
            public String EndTorihikisakiCD;
            /// <summary>
            /// 開始業者CD
            /// </summary>
            public String StartGyoushaCD;
            /// <summary>
            /// 終了業者CD
            /// </summary>
            public String EndGyoushaCD;
            /// <summary>
            /// 開始現場CD
            /// </summary>
            public String StartGenbaCD;
            /// <summary>
            /// 終了現場CD
            /// </summary>
            public String EndGenbaCD;

            /// <summary>
            /// 開始荷姿業者CD
            /// </summary>
            public String StartNGyoushaCD;
            /// <summary>
            /// 終了荷姿業者CD
            /// </summary>
            public String EndNGyoushaCD;
            /// <summary>
            /// 開始荷姿現場CD
            /// </summary>
            public String StartNGenbaCD;
            /// <summary>
            /// 終了荷姿現場CDD
            /// </summary>
            public String EndNGenbaCD;

            /// <summary>
            /// 開始出荷時品目CD
            /// </summary>
            public String StartSHinmokuCD;
            /// <summary>
            /// 終了出荷時品目CDD
            /// </summary>
            public String EndSHinmokuCD;
            /// <summary>
            /// 開始検収品目CD
            /// </summary>
            public String StartKHinmokuCD;
            /// <summary>
            /// 終了検収品目CDD
            /// </summary>
            public String EndKHinmokuCD;
            /// <summary>
            /// 検収状況
            /// </summary>
            public String KenshuJoKBN;
            /// <summary>
            /// 検収有無
            /// </summary>
            public String KenshuUmKBN;


        };
    }
}
