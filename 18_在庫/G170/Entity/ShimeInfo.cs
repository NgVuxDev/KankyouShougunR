using r_framework.Entity;

namespace Shougun.Core.Stock.ZaikoShimeSyori.Entity
{
    /// <summary>
    /// 在庫締めデータクラス
    /// </summary>
    public class ShimeInfo : SuperEntity
    {
        public string RET_SYSTEM_ID { get; set; } //システムID
        public string RET_ZAIKO_SHIME_DATE { get; set; } //在庫締実行日
        public string RET_GYOUSHA_CD { get; set; } //業者CD
        public string RET_GENBA_CD { get; set; } //現場CD
        public string RET_GENBA_NAME_RYAKU { get; set; } //現場名
        public string RET_ZAIKO_HINMEI_CD { get; set; } //在庫CD
        public string RET_ZAIKO_HINMEI_RYAKU { get; set; } //在庫品名
        public string RET_REMAIN_SUU { get; set; } //前月残数
        public string RET_ENTER_SUU { get; set; } //当月受入数
        public string RET_OUT_SUU { get; set; } //当月出荷量
        public string RET_ADJUST_SUU { get; set; } //調整量
        public string RET_TOTAL_SUU { get; set; } //当月在庫残
        public string RET_TANKA { get; set; } //評価単価
        public string RET_MULT { get; set; } //在庫金額

    }
}