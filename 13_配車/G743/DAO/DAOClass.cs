using System.Data.SqlTypes;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Data;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Allocation.CarTransferSpot
{

    [Bean(typeof(T_MOBISYO_RT))]
    public interface T_MOBISYO_RTDao : IS2Dao
    {
        /// <summary>
        /// モバイル将軍業務TBL削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferSpot.Sql.GetRtDataByCD.sql")]
        T_MOBISYO_RT GetRtDataByCD(SqlInt64 HAISHA_DENPYOU_NO, int HAISHA_KBN);

        /// <summary>
        /// モバイル将軍業務TBL削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferSpot.Sql.GetRtDataByCD.sql")]
        T_MOBISYO_RT[] GetRtDataByCDList(SqlInt64 HAISHA_DENPYOU_NO, int HAISHA_KBN);
    }

    [Bean(typeof(T_MOBISYO_RT_CONTENA))]
    public interface T_MOBISYO_RT_CONTENADao : IS2Dao
    {
        /// <summary>
        /// モバイル将軍業務TBL削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferSpot.Sql.GetRtCTNDataByCD.sql")]
        T_MOBISYO_RT_CONTENA[] GetRtCTNDataByCD(SqlInt64 SEQ_NO);
    }

    [Bean(typeof(T_MOBISYO_RT_DTL))]
    public interface T_MOBISYO_RT_DTLDao : IS2Dao
    {
        /// <summary>
        /// モバイル将軍業務詳細TBL削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferSpot.Sql.GetDtlDataByCD.sql")]
        T_MOBISYO_RT_DTL[] GetDtlDataByCD(SqlInt64 SEQ_NO);
    }

    [Bean(typeof(T_MOBISYO_RT_HANNYUU))]
    public interface T_MOBISYO_RT_HANNYUUDao : IS2Dao
    {
        /// <summary>
        /// モバイル将軍業務搬入TBL 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferSpot.Sql.GetHannyuuDataByCD.sql")]
        T_MOBISYO_RT_HANNYUU[] GetHannyuuDataByCD(SqlInt64 HANYU_JISSEKI_SEQ_NO);
    }

    [Bean(typeof(M_SHARYOU))]
    public interface M_SHARYOUDao : IS2Dao
    {
        /// <summary>
        /// 車輌名を取得する
        /// </summary>
        /// <param name="data">検索対象情報</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferSpot.Sql.GetSharyouNameData.sql")]
        new DataTable GetSharyouNameData(M_SHARYOU data, SqlDateTime TEKIYOU_DATE);
    }

}
