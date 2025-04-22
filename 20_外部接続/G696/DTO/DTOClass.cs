using System.Collections.Generic;
using System.Data.SqlTypes;
using r_framework.Entity;

namespace Shougun.Core.ExternalConnection.HaisouKeikakuIchiran
{
    /// <summary>
    /// 取込データ登録用DTO
    /// </summary>
    public class TorikomiDTO
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TorikomiDTO()
        {
            this.LOGI_LINK_STATUS_LIST = new List<T_LOGI_LINK_STATUS>();
            this.LOGI_TO_TEIKI_LIST = new List<T_LOGI_TO_TEIKI>();
            this.LOGI_TO_URSH_LIST = new List<T_LOGI_TO_URSH>();
            this.TEIKI_JISSEKI_ENTRY_LIST = new List<T_TEIKI_JISSEKI_ENTRY>();
            this.TEIKI_JISSEKI_DETAIL_LIST = new List<T_TEIKI_JISSEKI_DETAIL>();
            this.TEIKI_JISSEKI_NIOROSHI_LIST = new List<T_TEIKI_JISSEKI_NIOROSHI>();
            this.UR_SH_ENTRY_LIST = new List<T_UR_SH_ENTRY>();
            this.UR_SH_DETAIL_LIST = new List<T_UR_SH_DETAIL>();
            this.DEL_UKETSUKE_SS_ENTRY_LIST = new List<T_UKETSUKE_SS_ENTRY>();
            this.INS_UKETSUKE_SS_ENTRY_LIST = new List<T_UKETSUKE_SS_ENTRY>();
            this.INS_UKETSUKE_SS_DETAIL_LIST = new List<T_UKETSUKE_SS_DETAIL>();
            this.DEL_UKETSUKE_SK_ENTRY_LIST = new List<T_UKETSUKE_SK_ENTRY>();
            this.INS_UKETSUKE_SK_ENTRY_LIST = new List<T_UKETSUKE_SK_ENTRY>();
            this.INS_UKETSUKE_SK_DETAIL_LIST = new List<T_UKETSUKE_SK_DETAIL>();
            this.CONTENA_RESULT_LIST = new List<T_CONTENA_RESULT>();
            this.CONTENA_RESERVE_LIST = new List<T_CONTENA_RESERVE>();
        }

        /// <summary>配送計画連携状況管理</summary>
        public List<T_LOGI_LINK_STATUS> LOGI_LINK_STATUS_LIST { get; set; }
        /// <summary>配送計画to定期実績</summary>
        public List<T_LOGI_TO_TEIKI> LOGI_TO_TEIKI_LIST { get; set; }
        /// <summary>配送計画to売上支払</summary>
        public List<T_LOGI_TO_URSH> LOGI_TO_URSH_LIST { get; set; }
        /// <summary>定期実績入力</summary>
        public List<T_TEIKI_JISSEKI_ENTRY> TEIKI_JISSEKI_ENTRY_LIST { get; set; }
        /// <summary>定期実績明細</summary>
        public List<T_TEIKI_JISSEKI_DETAIL> TEIKI_JISSEKI_DETAIL_LIST { get; set; }
        /// <summary>定期実績荷降</summary>
        public List<T_TEIKI_JISSEKI_NIOROSHI> TEIKI_JISSEKI_NIOROSHI_LIST { get; set; }

        /// <summary>売上／支払入力</summary>
        public List<T_UR_SH_ENTRY> UR_SH_ENTRY_LIST { get; set; }
        /// <summary>売上／支払明細</summary>
        public List<T_UR_SH_DETAIL> UR_SH_DETAIL_LIST { get; set; }

        /// <summary>収集受付伝票(最新伝票削除用)</summary>
        public List<T_UKETSUKE_SS_ENTRY> DEL_UKETSUKE_SS_ENTRY_LIST { get; set; }
        /// <summary>収集受付伝票(新規追加用)</summary>
        public List<T_UKETSUKE_SS_ENTRY> INS_UKETSUKE_SS_ENTRY_LIST { get; set; }
        /// <summary>収集受付明細</summary>
        public List<T_UKETSUKE_SS_DETAIL> INS_UKETSUKE_SS_DETAIL_LIST { get; set; }
        /// <summary>出荷受付伝票(最新伝票削除用)</summary>
        public List<T_UKETSUKE_SK_ENTRY> DEL_UKETSUKE_SK_ENTRY_LIST { get; set; }
        /// <summary>出荷受付伝票(新規追加用)</summary>
        public List<T_UKETSUKE_SK_ENTRY> INS_UKETSUKE_SK_ENTRY_LIST { get; set; }
        /// <summary>出荷受付明細</summary>
        public List<T_UKETSUKE_SK_DETAIL> INS_UKETSUKE_SK_DETAIL_LIST { get; set; }

        /// <summary>コンテナ</summary>
        public List<T_CONTENA_RESULT> CONTENA_RESULT_LIST { get; set; }
        public List<T_CONTENA_RESERVE> CONTENA_RESERVE_LIST { get; set; }
    }

    /// <summary>
    /// 定期情報取込時の検索用DTO
    /// </summary>
    public class SearchTeikiTorikomiDTO
    {
        /// <summary>単位CD</summary>
        public SqlInt16 UNIT_CD { get; set; }

        /// <summary>業者CD</summary>
        public string GYOUSHA_CD { get; set; }

        /// <summary>品名CD</summary>
        public string HINMEI_CD { get; set; }

        /// <summary>現場CD</summary>
        public string GENBA_CD { get; set; }

        /// <summary>行番号</summary>
        public SqlInt16 ROW_NO { get; set; }

        /// <summary>(配車)伝票番号</summary>
        public int HAISHA_DENPYOU_NO { get; set; }

        /// <summary>伝票区分CD</summary>
        public SqlInt16 DENPYOU_KBN_CD { get; set; }
    }

    /// <summary>
    /// データ取込確認用DTO
    /// </summary>
    /// <remarks>
    /// 配送開始日と配送NOをもとに地点数を取得
    /// </remarks>
    public class TorikomiCheckDTO
    {
        /// <summary>配送開始日(yyyyMMdd形式)</summary>
        public string DELIVERY_DATE { get; set; }
        /// <summary>配送NO</summary>
        public string DELIVERY_NO { get; set; }
        /// <summary>配送計画で登録された地点数</summary>
        public int POINT_COUNT { get; set; }
    }
}
