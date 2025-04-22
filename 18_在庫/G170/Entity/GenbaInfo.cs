using r_framework.Entity;

namespace Shougun.Core.Stock.ZaikoShimeSyori.Entity
{
    /// <summary>
    /// 現場情報データクラス
    /// </summary>
    public class GenbaInfo : SuperEntity
    {
        public string RET_GYOUSHA_CD { get; set; } //業者CD
        public string RET_GYOUSHA_NAME_RYAKU { get; set; } //業者名
        public string RET_GENBA_CD { get; set; } //現場CD
        public string RET_GENBA_NAME_RYAKU { get; set; } //現場名
    }
}