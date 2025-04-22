using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 銀行支店マスタ(コードチェック拡張)のDaoクラス
    /// </summary>
    [Bean(typeof(M_BANK_SHITEN_FOR_CODECHECK))]
    public interface IM_BANK_SHITEN_FOR_CODECHECKDao : IS2Dao
    {
        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する(コードチェック)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.BankShiten.IM_BANK_SHITENDao_GetAllValidDataForCodeCheck.sql")]
        M_BANK_SHITEN_FOR_CODECHECK[] GetAllValidDataForCodeCheck(M_BANK_SHITEN data);
    }
}