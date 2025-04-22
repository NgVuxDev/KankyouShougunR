// $Id: ConstCls.cs 16965 2014-03-05 12:02:04Z y-sato $
using System.Collections.ObjectModel;

namespace Shougun.Core.Master.ContenaJoukyouHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>M_CONTENA_JOUKYOUのCONTENA_JOUKYOU_CD</summary>
        public static readonly string CONTENA_JOUKYOU_CD = "CONTENA_JOUKYOU_CD";

        /// <summary>M_CONTENA_JOUKYOUのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>M_CONTENA_JOUKYOUのDELETE_FLGフラグ</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>変更不可処理を行うCDリスト</summary>
        public static ReadOnlyCollection<string> FixedRowList = System.Array.AsReadOnly(new string[] { "1", "2", "3" });

        /// <summary>変更不可処理を行う項目リスト</summary>
        public static ReadOnlyCollection<string> FixedColumnList = System.Array.AsReadOnly(new string[] { "CONTENA_JOUKYOU_CD", "CONTENA_JOUKYOU_NAME", "CONTENA_JOUKYOU_NAME_RYAKU", "DELETE_FLG" });
    }
}
