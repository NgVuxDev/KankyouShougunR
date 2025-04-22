using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Data;

namespace r_framework.Dao
{
    [Bean(typeof(M_FILE_LINK_CHIIKIBETSU_KYOKA))]
    public interface IM_FILE_LINK_CHIIKIBETSU_KYOKADao : IS2Dao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_FILE_LINK_CHIIKIBETSU_KYOKA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_FILE_LINK_CHIIKIBETSU_KYOKA data);

        int Delete(M_FILE_LINK_CHIIKIBETSU_KYOKA data);

        [Query("KYOKA_KBN = /*kyokaKbn*/ AND GYOUSHA_CD = /*gyoushaCd*/ AND GENBA_CD = /*genbaCd*/ AND CHIIKI_CD = /*chiikiCd*/")]
        List<M_FILE_LINK_CHIIKIBETSU_KYOKA> GetDataByCd(short kyokaKbn, string gyoushaCd, string genbaCd, string chiikiCd);

        /// <summary>
        /// SQLを実行してデータを取得する。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
