using System.Collections.Generic;
using r_framework.Entity;

namespace Shougun.Core.ExternalConnection.HaisouKeikakuNyuuryoku
{
    /// <summary>
    /// 検索用配送計画一覧のDTO
    /// </summary>
    public class SearchDeliveryPlanDTO
    {
        /// <summary>配車区分</summary>
        public string HAISHA_KBN { get; set; }
        /// <summary>作業日FROM</summary>
        public string SAGYOU_DATE_FROM { get; set; }
        /// <summary>作業日TO</summary>
        public string SAGYOU_DATE_TO { get; set; }
        /// <summary>業者CD</summary>
        public string GYOUSHA_CD { get; set; }
        /// <summary>現場CD</summary>
        public string GENBA_CD { get; set; }
        /// <summary>運搬業者CD</summary>
        public string UNPAN_GYOUSHA_CD { get; set; }
        /// <summary>車輌CD</summary>
        public string SHARYOU_CD { get; set; }
        /// <summary>車種CD</summary>
        public string SHASHU_CD { get; set; }
        /// <summary>運転者CD</summary>
        public string UNTENSHA_CD { get; set; }
        /// <summary>コースCD</summary>
        public string COURSE_NAME_CD { get; set; }
    }

    /// <summary>
    /// 配送計画候補一覧用のDTO
    /// </summary>
    public class DeliveryPlanDTO
    {
        /// <summary>組込</summary>
        public bool KUMIKOMI { get; set; }
        /// <summary>定期配車番号</summary>
        public string TEIKI_HAISHA_NUMBER { get; set; }
        /// <summary>コースCD</summary>
        public string COURSE_NAME_CD { get; set; }
        /// <summary>コース名</summary>
        public string COURSE_NAME { get; set; }
        /// <summary>車種CD</summary>
        public string SHASHU_CD { get; set; }
        /// <summary>車種名</summary>
        public string SHASHU_NAME_RYAKU { get; set; }
        /// <summary>車両CD</summary>
        public string SHARYOU_CD { get; set; }
        /// <summary>車両名</summary>
        public string SHARYOU_NAME_RYAKU { get; set; }
        /// <summary>運転者CD</summary>
        public string UNTENSHA_CD { get; set; }
        /// <summary>運転者名</summary>
        public string UNTENSHA_NAME_RYAKU { get; set; }
        /// <summary>運搬業者CD</summary>
        public string UNPAN_GYOUSHA_CD { get; set; }
        /// <summary>運搬業者名</summary>
        public string UNPAN_GYOUSHA_NAME_RYAKU { get; set; }
    }

    /// <summary>
    /// 配送Noとして登録する配送データ一覧用のDTO
    /// </summary>
    public class DeliveryDataDTO
    {
        /// <summary>対象</summary>
        public bool TAISHO { get; set; }
        /// <summary>順番</summary>
        public string ROW_NUMBER { get; set; }
        /// <summary>非売上(荷降し)</summary>
        public bool NIOROSHI { get; set; }
        /// <summary>受付番号</summary>
        public string UKETSUKE_NUMBER { get; set; }
        /// <summary>現着時間</summary>
        public string GENCHAKU_TIME_NAME { get; set; }
        /// <summary>時間</summary>
        public string GENCHAKU_TIME { get; set; }
        /// <summary>定期配車番号</summary>
        public string TEIKI_HAISHA_NUMBER { get; set; }
        /// <summary>コースCD</summary>
        public string COURSE_NAME_CD { get; set; }
        /// <summary>コース名</summary>
        public string COURSE_NAME { get; set; }
        /// <summary>業者CD</summary>
        public string GYOUSHA_CD { get; set; }
        /// <summary>業者名</summary>
        public string GYOUSHA_NAME_RYAKU { get; set; }
        /// <summary>現場CD</summary>
        public string GENBA_CD { get; set; }
        /// <summary>現場名</summary>
        public string GENBA_NAME_RYAKU { get; set; }
        /// <summary>現場住所</summary>
        public string GENBA_ADDRESS { get; set; }
        /// <summary>車種CD</summary>
        public string SHASHU_CD { get; set; }
        /// <summary>車輌CD</summary>
        public string SHARYOU_CD { get; set; }
        /// <summary>運転者CD</summary>
        public string UNTENSHA_CD { get; set; }
        /// <summary>運搬業者CD</summary>
        public string UNPAN_GYOUSHA_CD { get; set; }
        /// <summary>配送開始日(作業日 or 伝票日付)</summary>
        public string DELIVERY_DATE { get; set; }
        /// <summary>システムID</summary>
        public string SYSTEM_ID { get; set; }
        /// <summary>明細システムID</summary>
        public string DETAIL_SYSTEM_ID { get; set; }
        /// <summary>設定システムID</summary>
        public string REF_SYSTEM_ID { get; set; }
        /// <summary>設定明細システムID</summary>
        public string REF_DETAIL_SYSTEM_ID { get; set; }
        /// <summary>伝票区別</summary>
        /// <remarks>1.収集受付, 2.出荷受付, 3.定期</remarks>
        public string DENPYOU_ATTR { get; set; }
        /// <summary>配車区分</summary>
        public string HAISHA_KBN { get; set; }
        /// <summary>配送No</summary>
        public string DELIVERY_NO { get; set; }
        /// <summary>配送名</summary>
        public string DELIVERY_NAME { get; set; }
        /// <summary>地点ID</summary>
        public string POINT_ID { get; set; }
        /// <summary>デジタコ車輌グループID</summary>
        public string DIGI_SHASHU_CD { get; set; }
        /// <summary>デジタコ車輌ID</summary>
        public string DIGI_SHARYOU_CD { get; set; }
        /// <summary>連携状態</summary>
        public int LINK_STATUS { get; set; }
        /// <summary>連携状況</summary>
        public string RENKEI_JYOUKYOU { get; set; }
        /// <summary>荷積荷降フラグ</summary>
        public bool NIZUMI_NIOROSHI_FLG { get; set; }
    }

    /// <summary>
    /// 配送計画(明細含み)を管理するDTO
    /// </summary>
    public class LogiDeliveryDTO
    {
        /// <summary>配送計画</summary>
        public T_LOGI_DELIVERY LOGI_DELIVERY { get; set; }
        /// <summary>配送計画明細</summary>
        public List<T_LOGI_DELIVERY_DETAIL> LOGI_DELIVERY_DETAIL_LIST { get; set; }
        /// <summary>配送計画連携状況管理</summary>
        public T_LOGI_LINK_STATUS LOGI_LINK_STATUS { get; set; }
    }
}
