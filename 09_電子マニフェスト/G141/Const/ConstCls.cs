using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {

        /// <summary>
        /// 排出事業者の加入者番号のコントロール名称
        /// </summary>
        public static readonly string CTXT_HAISYUTU_KANYUSHANO = "ctxt_Haisyutu_KanyushaNo";

        /// <summary>
        /// 入力区分が[1.自動]の場合
        /// </summary>
        public static readonly string INPUT_KBN_JIDOU = "1";
        public static readonly string INPUT_KBN_SHUDOU = "2";

        /// <summary>
        /// 電子区分
        /// </summary>
        public static readonly string DENSHI_MEDIA_TYPE = "4";

        /// <summary>
        /// 報告不要処分事業者加入者番号
        /// </summary>
        public static readonly string NO_REP_SBN_EDI_MEMBER_ID = "0000000";

        /// <summary>
        /// 事業場区分
        /// </summary>
        public static readonly SqlInt16 JIGYOUJOU_KBN_SBN = 3;

        /// <summary>
        /// DT_RXXの数量で利用する書式
        /// DT_RXX_EXは環境将軍Rの数量書式を参照するが、DT_RXXについてはJWNETで入力できる書式に合わせる。
        /// </summary>
        public static readonly string ELEC_MANIFEST_SUURYO_FORMAT = "#,##0.000";

        /// <summary>
        /// 通知コード
        /// </summary>
        public static readonly string[] TUUCHICD_DT_R18 = new string[] { "118", "203", "303" };

        /// <summary>
        /// 通知コード
        /// </summary>
        public static readonly string[] TUUCHICD_DT_R19 = new string[] { "110", "203", "303" };

        /// <summary>
        /// 通知コード
        /// </summary>
        public static readonly string[] TUUCHICD_DT_R05 = new string[] { "203", "303" };

        /// <summary>
        /// 通知コード
        /// </summary>
        public static readonly string[] TUUCHICD_DT_R06 = new string[] { "203", "303" };

        /// <summary>
        /// 通知コード
        /// </summary>
        public static readonly string[] TUUCHICD_DT_R08 = new string[] { "203", "303" };

        /// <summary>
        /// 通知コード
        /// </summary>
        public static readonly string[] TUUCHICD_DT_R13 = new string[] { "203", "303" };

        /// <summary>
        /// 通知コード
        /// </summary>
        public static readonly string[] TUUCHICD_DT_R02 = new string[] { "203", "303" };
    }
}
