// $Id: ConstCls.cs 15945 2014-02-13 10:34:29Z y-sato $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.MobileJoukyouIchiran
{
    class ConstCls
    {
        /// <summary>
        /// モバイル連携
        /// </summary>
        public static SqlInt16 MOBILE_RENKEI = 125;

        /// <summary>
        /// 配車区分1
        /// </summary>
        public static string HAISHA_KBN_1 = "1";

        /// <summary>
        /// 配車区分2
        /// </summary>
        public static string HAISHA_KBN_2 = "2";

        /// <summary>
        /// 連携1
        /// </summary>
        public static string RENKEI_KBN_1 = "1";

        /// <summary>
        /// 連携2
        /// </summary>
        public static string RENKEI_KBN_2 = "2";

        // 配車状況
        public static SqlInt16 HAISHA_JOKYO_CD = 3;

        /// <summary>
        /// 配車状況名
        /// </summary>
        public static string HAISHA_JOKYO_NAME = "計上";

        /// <summary>
        /// 入力区分_1_直接入力
        /// </summary>
        public static readonly SqlInt16 INPUT_KBN_1 = 1;
        /// <summary>
        /// 入力区分_2_組込
        /// </summary>
        public static readonly SqlInt16 INPUT_KBN_2 = 2;

        /// <summary>
        /// 2:設置
        /// </summary>
        public const int CONTENA_JOUKYOU_KBN_SECCHI = 2;
        /// <summary>
        /// 3:引揚
        /// </summary>
        public const int CONTENA_JOUKYOU_KBN_HIKIAGE = 3;

        // 配車状況
        public static SqlInt16 HAISHA_JOKYO_CD_5 = 5;

        /// <summary>
        /// 配車状況名
        /// </summary>
        public static string HAISHA_JOKYO_NAME_5 = "回収なし";

        /// <summary>
        /// 回収状況（未回収）
        /// </summary>
        public static string KAISHU_JOKYO_MI = "未回収";

    }
}
