using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.HaishaWariateDay
{
    public class DTO_Haisha : SuperEntity
    {
        /// <summary>
        /// 拠点CD
        /// </summary>
        public String KyotenCd { get; set; }

        /// <summary>
        /// 作業日
        /// </summary>
        public String SagyouDate { get; set; }

        /// <summary>
        /// 車種CD
        /// </summary>
        public String ShashuCd { get; set; }

        /// <summary>
        /// 社員CD
        /// </summary>
        public String ShainCd { get; set; }

        /// <summary>
        /// 車両CD
        /// </summary>
        public String SharyouCd { get; set; }

        /// <summary>
        /// 業者CD
        /// </summary>
        public string GyoushaCd { get; set; }

        /// <summary>
        /// 配車入区分
        /// </summary>
        public bool HaisyaKubun { get; set; }

        /// <summary>
        /// 配車入区分
        /// </summary>
        public string ShoriKbn { get; set; }

        /// <summary>
        /// 運転者CD
        /// </summary>
        public String UntenshaCd { get; set; }
    }
    public class DTO_IdSeq : SuperEntity
    {
        /// <summary>
        /// システムID
        /// </summary>
        public Int64 SystemId { get; set; }

        /// <summary>
        /// 枝番
        /// </summary>
        public int Seq { get; set; }
    }
    public class DTO_IdSeqDetid : SuperEntity
    {
        /// <summary>
        /// システムID
        /// </summary>
        public Int64 SystemId { get; set; }
        /// <summary>
        /// 枝番
        /// </summary>
        public int Seq { get; set; }
        /// <summary>
        /// 明細システムID
        /// </summary>
        public Int64 DetailSystemId { get; set; }
    }
    public class MapDTO_IdSeq : SuperEntity
    {
        /// <summary>
        /// システムID
        /// </summary>
        public Int64 SystemId { get; set; }

        /// <summary>
        /// 枝番
        /// </summary>
        public int Seq { get; set; }

        /// <summary>
        /// 伝票種類 0：割当済み 1：未配車
        /// </summary>
        public int DenpyouShurui { get; set; }

        public int ShubetsuKBN { get; set; }
    }
    /// <summary>
    /// スクリプトの実行結果を格納するDTO
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// 伝種区分CD
        /// </summary>
        public short DENSHU_KBN_CD { get; set; }

        /// <summary>
        /// 種類CD
        /// </summary>
        public string CONTENA_SHURUI_CD { get; set; }

        /// <summary>
        /// 種類名
        /// </summary>
        public string CONTENA_SHURUI_NAME_RYAKU { get; set; }

        /// <summary>
        /// コンテナCD
        /// </summary>
        public string CONTENA_CD { get; set; }

        /// <summary>
        /// コンテナ名
        /// </summary>
        public string CONTENA_NAME_RYAKU { get; set; }

        /// <summary>
        /// 業者CD
        /// </summary>
        public string GYOUSHA_CD { get; set; }

        /// <summary>
        /// 業者名
        /// </summary>
        public string GYOUSHA_NAME_RYAKU { get; set; }

        /// <summary>
        /// 現場CD
        /// </summary>
        public string GENBA_CD { get; set; }

        /// <summary>
        /// 現場名
        /// </summary>
        public string GENBA_NAME_RYAKU { get; set; }

        /// <summary>
        /// 営業担当者CD
        /// </summary>
        public string EIGYOU_TANTOU_CD { get; set; }

        /// <summary>
        /// 社員名(営業担当者名)
        /// </summary>
        public string SHAIN_NAME_RYAKU { get; set; }

        /// <summary>
        /// 設置日
        /// </summary>
        public string SECCHI_DATE { get; set; }

        /// <summary>
        /// 経過日数
        /// </summary>
        public Int32 DAYSCOUNT { get; set; }

        /// <summary>
        /// グラフ
        /// </summary>
        public string GRAPH { get; set; }

        /// <summary>
        /// 設置引揚区分
        /// </summary>
        public Int16 CONTENA_SET_KBN { get; set; }

        /// <summary>
        /// 台数
        /// </summary>
        public Int16 DAISUU_CNT { get; set; }

        /// <summary>
        /// 重複
        /// </summary>
        public string SecchiChouhuku { get; set; }
    }
    public class DTOCls
    {
        /// <summary>
        /// 部署CD
        /// </summary>
        public string BUSHO_CD { get; set; }

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
        /// 経過日数
        /// </summary>
        public Int16 ELAPSED_DAYS { get; set; }

        /// <summary>
        /// システム設定ファイルの日数
        /// </summary>
        public SqlInt16 SYS_DAYS_COUNT { get; set; }

        /// <summary>
        /// 作業日
        /// </summary>
        public string SAGYOU_DATE { get; set; }
    }

    public class DTO_GENBA_DATA
    {
        public string GYOUSHA_CD { get; set; }
        public string GYOUSHA_NAME { get; set; }
        public string GENBA_CD { get; set; }
        public string GENBA_NAME { get; set; }
        public string POST { get; set; }
        public string ADDRESS { get; set; }
        public string TEL { get; set; }
        public string BIKOU1 { get; set; }
        public string BIKOU2 { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }
    }
}
