using r_framework.Entity;

namespace Shougun.Core.Stock.ZaikoShimeSyori.Entity
{
    /// <summary>
    /// 在庫基準単価データクラス
    /// </summary>
    public class ZaikoBaseTanka : SuperEntity
    {
        public decimal RET_ZAIKO_BASE_TANKA { get; set; } // 在庫基準単価
    }
}