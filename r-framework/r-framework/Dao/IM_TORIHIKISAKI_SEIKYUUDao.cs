using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    [Bean(typeof(M_TORIHIKISAKI_SEIKYUU))]
    public interface IM_TORIHIKISAKI_SEIKYUUDao : IS2Dao
    {
        [Sql("SELECT * FROM M_TORIHIKISAKI_SEIKYUU")]
        M_TORIHIKISAKI_SEIKYUU[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_TORIHIKISAKI_SEIKYUU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_TORIHIKISAKI_SEIKYUU data);

        int Delete(M_TORIHIKISAKI_SEIKYUU data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_TORIHIKISAKI_SEIKYUU data);

        /// <summary>
        /// 取引先CDコードをもとに取引先_請求情報マスタのデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_TORIHIKISAKI_SEIKYUU GetDataByCd(string cd);

        /// <summary>
        /// 住所の一部データ書き換え機能
        /// </summary>
        /// <param name="path">SQLファイルのパス</param>
        /// <param name="data">取引先請求情報マスタエンティティ</param>
        /// <param name="oldPost">旧郵便番号</param>
        /// <param name="oldAddress">旧住所</param>
        /// <param name="newPost">新郵便番号</param>
        /// <param name="newAddress">新住所</param>
        /// <returns></returns>
        int UpdatePartData(string path, M_TORIHIKISAKI_SEIKYUU data, string oldPost, string oldAddress, string newPost, string newAddress);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        //Begin: LANDUONG - 20220209 - refs#160050
        [SqlFile("r_framework.Dao.SqlFile.Torihikisaki.GetMaxPlusRakurakuCustomerCode.sql")]
        decimal GetMaxPlusRakurakuCode(M_TORIHIKISAKI_SEIKYUU data);

        [SqlFile("r_framework.Dao.SqlFile.Torihikisaki.GetAllRakurakuCustomerCode.sql")]
        string[] GetAllRakurakuCodeData(M_TORIHIKISAKI_SEIKYUU data);

        [SqlFile("r_framework.Dao.SqlFile.Torihikisaki.GetMinBlankRakurakuCustomerCode.sql")]
        decimal GetMinBlankRakurakuCode(M_TORIHIKISAKI_SEIKYUU data);

        [Sql("SELECT * FROM M_TORIHIKISAKI_SEIKYUU MTS INNER JOIN M_TORIHIKISAKI MT ON MTS.TORIHIKISAKI_CD = MT.TORIHIKISAKI_CD WHERE MT.DELETE_FLG = 0 AND RAKURAKU_CUSTOMER_CD = /*code*/")]
        M_TORIHIKISAKI_SEIKYUU[] GetDataByRakurakuCode(string code);
        //End: LANDUONG - 20220209 - refs#160050
    }
}
