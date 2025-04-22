using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace SyaryoSentaku.DTO
{
    /// <summary>
    /// パラメータ
    /// </summary>
    public class SerchParameterDtoCls
    {
        /// <summary>検索条件  :車輌CD</summary>
        public string SHARYOU_CD { get; set; }

        /// <summary>検索条件  :業者CD</summary>
        public string GYOUSHA_CD { get; set; }

        /// <summary>検索条件  :車種CD</summary>
        public string SHASYU_CD { get; set; }

        /// <summary>検索条件  :社員CD</summary>
        public string SHAIN_CD { get; set; }

        /// <summary>検索条件  :業者区分</summary>
        public string GYOUSHAKBN { get; set; }

        /// <summary>検索条件  :休動日</summary>
        public string CLOSED_DATE { get; set; }

        /// <summary>検索条件  :削除フラグ</summary>
        public SqlBoolean DELETE_FLG { get; set; }

        /// <summary>検索条件  :適用フラグ</summary>
        public SqlBoolean GYOUSHA_TEKIYOU_FLG { get; set; }

        /// <summary>検索条件  :適用日</summary>
        public string TEKIYOU_DATE { get; set; }

        /// <summary>検索条件  :検索項目</summary>
        public string KENCONDITION_ITEM { get; set; }

        /// <summary>検索条件  :検索値</summary>
        public string KENCONDITION_VALUE { get; set; }

        /// <summary>検索条件  :運搬業者区分</summary>
        public SqlBoolean UNPAN_JUTAKUSHA_KAISHA_KBN { get; set; }

        /// <summary>検索条件  :マニ区分</summary>
        public SqlBoolean GYOUSHAKBN_MANI { get; set; }
    }
}
