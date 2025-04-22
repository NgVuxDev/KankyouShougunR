using System;
using System.Data.SqlTypes;

namespace Shougun.Core.Common.KokyakuKarute
{
    public class ContenaDTO
    {
        /// <summary>
        /// 部署CD
        /// </summary>
        public string BUSHO_CD { get; set; }

        /// <summary>
        /// 拠点CD
        /// </summary>
        public string KYOTEN_CD { get; set; }

        /// <summary>
        /// 取引先CD
        /// </summary>
        public string TORIHIKISAKI_CD { get; set; }

        /// <summary>
        /// 業者CD
        /// </summary>
        public string GYOUSHA_CD { get; set; }

        /// <summary>
        /// コンテナ種類CD
        /// </summary>
        public string CONTENA_SHURUI_CD { get; set; }

        /// <summary>
        /// 現場CD
        /// </summary>
        public string GENBA_CD { get; set; }

        /// <summary>
        /// コンテナCD
        /// </summary>
        public string CONTENA_CD { get; set; }

        /// <summary>
        /// 営業担当者CD
        /// </summary>
        public string EIGYOU_TANTOU_CD { get; set; }

        /// <summary>
        /// 設置日
        /// </summary>
        public string SECCHI_DATE { get; set; }

        /// <summary>
        /// 設置日
        /// </summary>
        public string SECCHI_DATE_FROM { get; set; }

        /// <summary>
        /// 設置日
        /// </summary>
        public string SECCHI_DATE_TO { get; set; }

        /// <summary>
        /// 経過日数
        /// </summary>
        public Int16 ELAPSED_DAYS { get; set; }

        /// <summary>
        /// システム設定ファイルの日数
        /// </summary>
        public SqlInt16 SYS_DAYS_COUNT { get; set; }
    }
}
