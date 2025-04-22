using r_framework.Entity;
using System;

namespace Shougun.Core.Stock.ZaikoShimeSyori.Entity
{
    /// <summary>
    /// 在庫締め対象データクラス
    /// </summary>
    public class ShimeTargetInfo : SuperEntity
    {
        public string RET_GYOUSHA_CD { get; set; }          // 業者CD
        public string RET_GENBA_CD { get; set; }            // 現場CD
        public string RET_GENBA_NAME { get; set; }          // 現場名
        public string RET_ZAIKO_HINMEI_CD { get; set; }     // 在庫CD
        public string RET_ZAIKO_HINMEI_NAME { get; set; }   // 在庫品名
        public decimal RET_JYUURYOU { get; set; }             // 重量
        public decimal RET_TANKA { get; set; }                // 単価
        public decimal RET_KINGAKU { get; set; }              // 金額
        public DateTime RET_DENPYOU_DATE { get; set; }      // 伝票日付
        public int RET_TARGET_FLG { get; set; }             // 対象データフラグ

    }
}