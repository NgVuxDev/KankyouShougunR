// $Id: DTOClass.cs 14413 2014-01-17 07:38:37Z sys_dev_20 $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.TeikiHaishaNyuuryoku
{
    public class DTOClass
    {
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
        /// 検索条件 : 定期配車番号
        /// </summary>
        public String TeikiHaishaNumber { get; set; }
        /// <summary>
        /// 検索条件 : 曜日CD
        /// </summary>
        public string DayCd { get; set; }
        /// <summary>
        /// 検索条件 : コース名称CD
        /// </summary>
        public String CourseNameCd { get; set; }
        /// <summary>
        /// 検索条件 : レコードID
        /// </summary>
        public int RecId { get; set; }
        /// <summary>
        /// 検索条件 : 拠点CD
        /// </summary>
        public String KyotenCd { get; set; }
        /// <summary>
        /// 検索条件 : 受付番号
        /// </summary>
        public long UketsukeNumber { get; set; }
        /// <summary>
        /// 検索条件 : 作業日
        /// </summary>
        public DateTime SagyouDate { get; set; }

        /// <summary>
        /// モバイル連携用：作業日FROM
        /// </summary>
        public string SAGYOU_DATE_FROM { get; set; }

        /// <summary>
        /// モバイル連携用：作業日TO
        /// </summary>
        public string SAGYOU_DATE_TO { get; set; }

        /// <summary>
        /// モバイル連携用：検索条件 : 定期配車番号
        /// </summary>
        public long RenkeiTeikiHaishaNumber { get; set; }

    }
    public class NioroshiDTOClass
    {
        public string NIOROSHI_NUMBER { get; set; }
        public string NIOROSHI_GYOUSHA_CD { get; set; }
        public string NIOROSHI_GYOUSHA_NAME { get; set; }
        public string NIOROSHI_GENBA_CD { get; set; }
        public string NIOROSHI_GENBA_NAME { get; set; }
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
