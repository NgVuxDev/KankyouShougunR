using System.Data.SqlTypes;
using System.Linq;
using r_framework.Entity;

namespace Shougun.Core.ExternalConnection.CourseSaitekikaNyuuryoku
{
    /// <summary>
    /// コース最適化DTO(NAVITIME連携結果取得用)
    /// </summary>
    public class SaitekikaDTO
    {
        /// <summary>ID(コース：M_COURSE_DETAIL.REC_ID、定期：T_TEIKI_HAISHA_DETAIL.DETAIL_SYSTEM_ID)</summary>
        public string ID { get; set; }
        /// <summary>連番</summary>
        public string INDEX_NO { get; set; }
        /// <summary>変更前順番</summary>
        public string PRE_ROW_NO { get; set; }
        /// <summary>業者CD</summary>
        public string GYOUSHA_CD { get; set; }
        /// <summary>現場CD</summary>
        public string GENBA_CD { get; set; }
        /// <summary>現場名略称</summary>
        public string GENBA_NAME_RYAKU { get; set; }
        /// <summary>希望時間</summary>
        public string KIBOU_TIME { get; set; }
        /// <summary>到着予定時刻(HH:mm形式)</summary>
        public string ESTIMATED_ARRIVAL_TIME
        {
            get
            {
                if (string.IsNullOrEmpty(ESTIMATED_ARRIVAL_DATE))
                {
                    return string.Empty;
                }

                //「yyyyMMdd HH:mm」形式のためスペースで分割
                return this.ESTIMATED_ARRIVAL_DATE.Split(' ').Last();
            }
        }
        /// <summary>回数</summary>
        public string ROUND_NO { get; set; }
        /// <summary>順番</summary>
        public string ROW_NO { get; set; }
        /// <summary>備考</summary>
        public string BIKOU { get; set; }
        /// <summary>訪問先コード</summary>
        public string VISIT_CODE { get; set; }
        /// <summary>案件コード</summary>
        public string MATTER_CODE { get; set; }
        /// <summary>作業時間_分</summary>
        public string SAGYOU_TIME_MINUTE { get; set; }
        /// <summary>NAVITIMEで新規追加された現場か</summary>
        public bool IS_ADD_GENBA { get; set; }
        /// <summary>到着予定日時(yyyyMMdd HH:mm形式)</summary>
        public string ESTIMATED_ARRIVAL_DATE { get; set; }
    }

    /// <summary>
    /// コース最適化入力画面に表示
    /// </summary>
    public class T_NAVI_DELIVERY_DTO : T_NAVI_DELIVERY
    {
        /// <summary>拠点CD</summary>
        public SqlInt16 KYOTEN_CD { get; set; }
        /// <summary>拠点略称名</summary>
        public string KYOTEN_NAME_RYAKU { get; set; }
        /// <summary>定期配車番号</summary>
        public SqlInt64 TEIKI_HAISHA_NUMBER { get; set; }
        /// <summary>コース略称名</summary>
        public string COURSE_NAME_RYAKU { get; set; }
        /// <summary>運搬業者略称名</summary>
        public string UNPAN_GYOUSHA_NAME_RYAKU { get; set; }
        /// <summary>運転者略称名</summary>
        public string UNTENSHA_NAME_RYAKU { get; set; }
        /// <summary>車種略称名</summary>
        public string SHASHU_NAME_RYAKU { get; set; }
        /// <summary>車輌略称名</summary>
        public string SHARYOU_NAME_RYAKU { get; set; }
        /// <summary>出発業者略称名</summary>
        public string SHUPPATSU_GYOUSHA_NAME_RYAKU { get; set; }
        /// <summary>出発現場略称名</summary>
        public string SHUPPATSU_GENBA_NAME_RYAKU { get; set; }
        /// <summary>荷降業者略称名</summary>
        public string NIOROSHI_GYOUSHA_NAME_RYAKU { get; set; }
        /// <summary>荷降現場略称名</summary>
        public string NIOROSHI_GENBA_NAME_RYAKU { get; set; }
        /// <summary>作業開始時間_時</summary>
        public SqlInt16 SAGYOU_BEGIN_HOUR { get; set; }
        /// <summary>作業開始時間_分</summary>
        public SqlInt16 SAGYOU_BEGIN_MINUTE { get; set; }
    }
}
