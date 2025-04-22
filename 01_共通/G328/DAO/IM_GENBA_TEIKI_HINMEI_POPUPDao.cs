using System.Data.SqlTypes;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace Shougun.Core.Common.KaisyuuHinmeShousai
{
    [Bean(typeof(M_GENBA_TEIKI_HINMEI))]
    public interface IM_GENBA_TEIKI_HINMEI_POPUPDao : IS2Dao
    {
        /// <summary>
        /// 物販在庫保管場所ポップアップ画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.KaisyuuHinmeShousai.Sql.IM_GENBA_TEIKI_HINMEIDao_GetAllValidDataForPopUp.sql")]
        M_GENBA_TEIKI_HINMEI GetAllValidData(M_GENBA_TEIKI_HINMEI data);

        [SqlFile("Shougun.Core.Common.KaisyuuHinmeShousai.Sql.IM_GENBA_TEIKI_HINMEIDao_GetHinmeiDataForPopUp.sql")]
        M_GENBA_TEIKI_HINMEI GetHinmeiData(string gyoushaCD, string genbaCD, string hinmeiCd, int denpyouKbnCd);
    }
}
