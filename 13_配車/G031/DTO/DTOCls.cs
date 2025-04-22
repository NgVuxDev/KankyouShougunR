// $Id: DTOCls.cs 15795 2014-02-10 04:25:37Z ogawa@takumi-sys.co.jp $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.CourseHaishaIraiNyuuryoku
{
    public class DTOCls
    {
        /// <summary>
        /// 拠点CD
        /// </summary>
        public string KYOTEN_CD { get; set; }

        /// <summary>
        /// 作業日FROM
        /// </summary>
        public string SAGYOU_DATE_BEGIN { get; set; }

        /// <summary>
        /// 作業日TO
        /// </summary>
        public string SAGYOU_DATE_END { get; set; }

        /// <summary>
        /// 組込状態
        /// </summary>
        public string KUMIKOMI_STATUS { get; set; }

        /// <summary>
        /// 受付番号
        /// </summary>
        public long UketsukeNumber { get; set; }

        /// <summary>
        /// システムID
        /// </summary>
        public long SYSTEM_ID { get; set; }

        /// <summary>
        /// シーケンス番号
        /// </summary>
        public int SEQ { get; set; }

        /// <summary>
        /// コンテナ設定区分
        /// </summary>
        public int ContenaSetKbn { get; set; }

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
        /// 作業日
        /// </summary>
        public string SAGYOU_DATE { get; set; }

        /// <summary>
        /// 曜日
        /// </summary>
        public string DAY_CD { get; set; }

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
        /// 拠点
        /// </summary>
        public string KYOTEN_CD { get; set; }
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
