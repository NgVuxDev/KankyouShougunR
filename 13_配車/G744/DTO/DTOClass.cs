using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.CarTransferTeiki
{
    public class DTOClass
    {
        /// <summary>
        /// 検索条件 : 定期配車番号
        /// </summary>
        public String TeikiHaishaNumber { get; set; }
        /// <summary>
        /// 検索条件  :SYSTEM_ID
        /// </summary>
        public long SystemId { get; set; }
        /// <summary>
        /// 検索条件 : SEQ
        /// </summary>
        public int Seq { get; set; }
        /// <summary>
        /// 検索条件 : 明細システムID
        /// </summary>
        public long DetailSystemId { get; set; }

        /// <summary>
        /// モバイル連携用：作業日FROM
        /// </summary>
        public string SAGYOU_DATE_FROM { get; set; }

        /// <summary>
        /// モバイル連携用：作業日TO
        /// </summary>
        public string SAGYOU_DATE_TO { get; set; }

        /// <summary>
        /// モバイル連携用：DETAIL_SYSTEM_ID
        /// </summary>
        public string DETAIL_SYSTEM_ID { get; set; }
    }
    public class PopupDTOCls
    {
        /// <summary>
        /// 配車番号
        /// </summary>
        public string TEIKI_HAISHA_NUMBER { get; set; }

        /// <summary>
        /// コースCD
        /// </summary>
        public string COURSE_NAME_CD { get; set; }

        /// <summary>
        /// コースFLG
        /// </summary>
        public bool courseOnly { get; set; }

        /// <summary>
        /// 検索POPFLG
        /// </summary>
        public bool POPFLG { get; set; }


    }

    /// <summary>
    /// モバイル連携用
    /// </summary>
    public class NiorosiClass
    {
        /// <summary>
        /// 配車番号
        /// </summary>
        public string TEIKI_HAISHA_NUMBER { get; set; }

        /// <summary>
        /// 荷降番号
        /// </summary>
        public string NIOROSHI_NUMBER { get; set; }

        /// <summary>
        /// 搬入シーケンシャルナンバー
        /// </summary>
        public SqlInt64 HANYU_SEQ_NO { get; set; }
    }
}
