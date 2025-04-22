using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Common.KaisyuuHinmeShousai
{
    // 回収品名詳細
    public class DTOClass
    {
        /// <summary>
        /// 削除フラグ
        /// </summary>
        public int DELETE_FLG { get; set; }

        /// <summary>
        /// レコードID
        /// </summary>
        public long REC_ID { get; set; }

        /// <summary>
        /// レコードSEQ
        /// </summary>
        public long REC_SEQ { get; set; }

        /// <summary>
        /// 品名CD
        /// </summary>
        public string HINMEI_CD { get; set; }

        /// <summary>
        /// 品名
        /// </summary>
        public string HINMEI_NAME { get; set; }

        /// <summary>
        /// 単位CD
        /// </summary>
        public SqlInt16 UNIT_CD { get; set; }

        /// <summary>
        /// 単位    
        /// </summary>
        public string UNIT_NAME { get; set; }

        /// <summary>
        /// 換算値
        /// </summary>
        public SqlDecimal KANSANCHI { get; set; }

        /// <summary>
        /// 換算後単位CD
        /// </summary>
        public SqlInt16 KANSAN_UNIT_CD { get; set; }

        /// <summary>
        /// 単位名
        /// </summary>
        public string KANSAN_UNIT_NAME { get; set; }

        /// <summary>
        /// 伝票区分CD
        /// </summary>
        public SqlInt16 DENPYOU_KBN_CD { get; set; }
        /// <summary>
        /// 契約区分CD
        /// </summary>
        public SqlInt16 KEIYAKU_KBN { get; set; }
        /// <summary>
        /// 月極区分
        /// </summary>
        public SqlInt16 KEIJYOU_KBN { get; set; }
        /// <summary>
        /// 要記入
        /// </summary>
        public SqlBoolean KANSAN_UNIT_MOBILE_OUTPUT_FLG { get; set; }

        /// <summary>
        /// 入力区分
        /// </summary>
        public SqlInt16 INPUT_KBN { get; set; }
        /// <summary>
        /// 荷降No
        /// </summary>
        public SqlInt32 NIOROSHI_NUMBER { get; set; }
        /// <summary>
        /// 実数
        /// </summary>
        public SqlBoolean ANBUN_FLG { get; set; }
        /// <summary>
        /// 適用開始日
        /// </summary>
        public SqlDateTime TEKIYOU_BEGIN { get; set; }
        /// <summary>
        /// 適用終了日
        /// </summary>
        public SqlDateTime TEKIYOU_END { get; set; }
        /// <summary>
        /// 適用開始日
        /// </summary>
        public SqlDateTime GENBA_TEKIYOU_BEGIN { get; set; }
        /// <summary>
        /// 適用終了日
        /// </summary>
        public SqlDateTime GENBA_TEKIYOU_END { get; set; }

    }
}
