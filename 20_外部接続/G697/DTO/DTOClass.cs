using System.Collections.Generic;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.ExternalConnection.HaisouKeikakuTeiki
{
    /// <summary>
    /// 配送計画(明細含み)を管理するDTO
    /// </summary>
    public class NaviDeliveryDTO
    {
        /// <summary>配送計画</summary>
        public List<T_NAVI_DELIVERY> NAVI_DELIVERY { get; set; }
        /// <summary>配送計画連携状況管理</summary>
        public List<T_NAVI_LINK_STATUS> NAVI_LINK_STATUS { get; set; }
    }

    public class SearchDto
    {
        public int KYOTEN_CD { get; set; }
        public int RENKEI_KBN { get; set; }
        public string SAGYOU_DATE { get; set; }
        public string DAY_CD { get; set; }
        public string COURSE_NAME_CD { get; set; }
        public string SHASHU_CD { get; set; }
        public string SHARYOU_CD { get; set; }
        public string UNTENSHA_CD { get; set; }
        public string UNPAN_GYOUSHA_CD { get; set; }
    }

    /// <summary>
    /// チェックを付けている明細行の情報(一部)を保持
    /// 全項目設定してデータバインドすると、DGVの制御が面倒になるのでやらない
    /// </summary>
    public class NaviCheckDetail
    {
        public long SYSTEM_ID { get; set; }
        public string DAY_CD { get; set; }
        public string COURSE_NAME_CD { get; set; }
        public long TEIKI_SYSTEM_ID { get; set; }
        public int TEIKI_SEQ { get; set; }
        public string DELIVERY_DATE { get; set; }
        public string SHASHU_CD { get; set; }
        public string SHARYOU_CD { get; set; }
        public string SHARYOU_TYPE { get; set; }
        public string UNTENSHA_CD { get; set; }
        public string UNPAN_GYOUSHA_CD { get; set; }
        public string SAGYOUSHA_CD { get; set; }
        public string SHUPPATSU_GYOUSHA_CD { get; set; }
        public string SHUPPATSU_GENBA_CD { get; set; }
        public string SHUPPATSU_EIGYOUSHO_CD { get; set; }
        public string NIOROSHI_GYOUSHA_CD { get; set; }
        public string NIOROSHI_GENBA_CD { get; set; }
        public string NIOROSHI_EIGYOUSHO_CD { get; set; }
        public string NAVI_DELIVERY_ORDER { get; set; }
        public bool TRAFFIC_CONSIDERATION { get; set; }
        public bool SMART_IC_CONSIDERATION { get; set; }
        public bool PRIORITY { get; set; }
        public string PROCESSING_ID { get; set; }
        public string DEPARTURE_TIME { get; set; }
        public string ARRIVAL_TIME { get; set; }
        public int BIN_NO { get; set; }
    }
}
