using r_framework.Entity;

namespace r_framework.MasterAccess
{
    /// <summary>
    /// マスタデータチェッククラスのインタフェース
    /// </summary>
    public interface IMasterDataAccess
    {
        /// <summary>
        /// 検索結果を格納するEntity
        /// </summary>
        SuperEntity Entity { get; set; }
    }
}
