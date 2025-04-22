using System;
using System.Data.SqlTypes;

namespace r_framework.Entity
{
    /// <summary>
    /// 電子申請入力エンティティ
    /// </summary>
    public class T_DENSHI_SHINSEI_ENTRY : SuperEntity
    {
        /// <summary>
        /// システムIDを取得・設定します
        /// </summary>
        public SqlInt64 SYSTEM_ID { get; set; }

        /// <summary>
        /// 枝番を取得・設定します
        /// </summary>
        public SqlInt32 SEQ { get; set; }

        /// <summary>
        /// 申請マスタ区分を取得・設定します。
        /// </summary>
        public SqlInt16 SHINSEI_MASTER_KBN { get; set; }

        /// <summary>
        /// 拠点CDを取得・設定します
        /// </summary>
        public SqlInt16 KYOTEN_CD { get; set; }

        /// <summary>
        /// 申請番号を取得・設定します
        /// </summary>
        public SqlInt64 SHINSEI_NUMBER { get; set; }

        /// <summary>
        /// 申請日付を取得・設定します
        /// </summary>
        public SqlDateTime SHINSEI_DATE { get; set; }

        /// <summary>
        /// 申請者CDを取得・設定します
        /// </summary>
        public String SHINSEISHA_CD { get; set; }

        /// <summary>
        /// 重要度CDを取得・設定します
        /// </summary>
        public string JYUYOUDO_CD { get; set; }

        /// <summary>
        /// 申請内容CDを取得・設定します
        /// </summary>
        public SqlInt16 NAIYOU_CD { get; set; }

        /// <summary>
        /// 申請経路CDを取得・設定します
        /// </summary>
        public string DENSHI_SHINSEI_ROUTE_CD { get; set; }

        /// <summary>
        /// 申請者コメントを取得・設定します
        /// </summary>
        public String SHINSEISHA_COMMENT { get; set; }

        /// <summary>
        /// 見積書添付を取得・設定します
        /// </summary>
        public String MITSUMORI_TENPU { get; set; }

        /// <summary>
        /// 引合取引先CDを取得・設定します
        /// </summary>
        public String HIKIAI_TORIHIKISAKI_CD { get; set; }

        /// <summary>
        /// 引合業者CDを取得・設定します
        /// </summary>
        public String HIKIAI_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 引合現場CDを取得・設定します
        /// </summary>
        public String HIKIAI_GENBA_CD { get; set; }

        /// <summary>
        /// 取引先CDを取得・設定します
        /// </summary>
        public String TORIHIKISAKI_CD { get; set; }

        /// <summary>
        /// 業者CDを取得・設定します
        /// </summary>
        public String GYOUSHA_CD { get; set; }

        /// <summary>
        /// 現場CDを取得・設定します
        /// </summary>
        public String GENBA_CD { get; set; }

        /// <summary>
        /// 削除フラグを取得・設定します
        /// </summary>
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
