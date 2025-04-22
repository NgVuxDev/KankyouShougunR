// $Id: ContenaSousaHoshuConstans.cs 16965 2014-03-05 12:02:04Z y-sato $
using System.Collections.ObjectModel;

namespace Shougun.Core.Master.ContenaSousaHoshu
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ContenaSousaHoshuConstans
    {
        /// <summary>M_CONTENA_SOUSAのCONTENA_SOUSA_CD</summary>
        public static readonly string CONTENA_SOUSA_CD = "CONTENA_SOUSA_CD";

        /// <summary>M_CONTENA_SOUSAのCONTENA_SOUSA_NAME</summary>
        public static readonly string CONTENA_SOUSA_NAME = "CONTENA_SOUSA_NAME";

        /// <summary>M_CONTENA_SOUSAのCONTENA_SOUSA_BIKOU</summary>
        public static readonly string CONTENA_SOUSA_BIKOU = "CONTENA_SOUSA_BIKOU";

        /// <summary>M_CONTENA_SOUSAのCREATE_USER</summary>
        public static readonly string CREATE_USER = "CREATE_USER";

        /// <summary>M_CONTENA_SOUSAのCREATE_DATE</summary>
        public static readonly string CREATE_DATE = "CREATE_DATE";

        /// <summary>M_CONTENA_SOUSAのUPDATE_USER</summary>
        public static readonly string UPDATE_USER = "UPDATE_USER";

        /// <summary>M_CONTENA_SOUSAのUPDATE_DATE</summary>
        public static readonly string UPDATE_DATE = "UPDATE_DATE";

        /// <summary>M_CONTENA_SOUSAのDELETE_FLGフラグ</summary>
        public static readonly string DELETE_FLG = "DELETE_FLG";

        /// <summary>M_CONTENA_SOUSAのTIME_STAMP</summary>
        public static readonly string TIME_STAMP = "TIME_STAMP";

        /// <summary>コンテナ操作CD</summary>
        public static readonly string ITEM_CONTENA_SOUSA_CD = "コンテナ操作CD";

        /// <summary>コンテナ操作名</summary>
        public static readonly string ITEM_CONTENA_SOUSA_NAME = "コンテナ操作名";

        /// <summary>コンテナ操作備考</summary>
        public static readonly string ITEM_CONTENA_SOUSA_BIKOU = "備考";

        /// <summary>作成者</summary>
        public static readonly string ITEM_CREATE_USER = "作成者";

        /// <summary>作成日時</summary>
        public static readonly string ITEM_CREATE_DATE = "作成日";

        /// <summary>更新者</summary>
        public static readonly string ITEM_UPDATE_USER = "更新者";

        /// <summary>更新日</summary>
        public static readonly string ITEM_UPDATE_DATE = "更新日";

        /// <summary>削除フラグ</summary>
        public static readonly string ITEM_DELETE_FLG = "削除";

        /// <summary>変更不可処理を行うCDリスト</summary>
        public static ReadOnlyCollection<string> FixedRowList = System.Array.AsReadOnly(new string[] { "1", "2", "9" });

        /// <summary>変更不可処理を行う項目リスト</summary>
        public static ReadOnlyCollection<string> FixedColumnList = System.Array.AsReadOnly(new string[] { "CONTENA_SOUSA_CD", "CONTENA_SOUSA_NAME", "CONTENA_SOUSA_NAME_RYAKU", "DELETE_FLG" });
    }
}
