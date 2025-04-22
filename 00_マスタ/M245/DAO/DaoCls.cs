// $Id: DaoCls.cs 48144 2015-04-23 09:11:43Z chenzz@oec-h.com $
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Master.ZaikoHiritsuHoshu.DAO
{
    [Bean(typeof(M_ZAIKO_HIRITSU))]
    public interface DaoCls : IS2Dao
    {
        [Sql("SELECT * FROM M_ZAIKO_HIRITSU")]
        M_ZAIKO_HIRITSU[] GetAllData();

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_ZAIKO_HIRITSU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ZAIKO_HIRITSU data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="dataHiritsu">Entity</param>
        /// <param name="zaikoHinmei">在庫品名</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ZaikoHiritsuHoshu.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSqlFile(M_ZAIKO_HIRITSU dataHiritsu,
            string zaikoHinmei, bool deletechuFlg);

        /// <summary>
        /// データを取得
        /// </summary>
        /// <param name="zaikoHinmei">在庫品名</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ZaikoHiritsuHoshu.Sql.GetHinmeiDataSql.sql")]
        DataTable GetZaikoHinmeiSqlFile(string zaikoHinmei);

        /// <summary>
        /// PKにデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Master.ZaikoHiritsuHoshu.Sql.DuplicationCheckSql.sql")]
        DataTable GetDataByPKSqlFile(M_ZAIKO_HIRITSU data);

        /// <summary>
        /// 在庫比率を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Master.ZaikoHiritsuHoshu.Sql.GetZaikoHiritsuSql.sql")]
        DataTable GetDataBySqlFile(M_ZAIKO_HIRITSU data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("ZAIKO_HINMEI_CD = /*ZAIKO_HINMEI_CD*/ AND DENSHU_KBN_CD = /*DENSHU_KBN_CD*/ AND HINMEI_CD = /*HINMEI_CD*/")]
        M_ZAIKO_HIRITSU GetDataByCd(string ZAIKO_HINMEI_CD, string HINMEI_CD, int DENSHU_KBN_CD);

        /// <summary>
        /// ユーザ指定の更新条件によるデータ更新
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <param name="updateKey"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ZaikoHiritsuHoshu.Sql.UpdateZaikoHiritsuDataSql.sql")]
        int UpdateBySqlFile(M_ZAIKO_HIRITSU data, M_ZAIKO_HIRITSU updateKey);
    }

    [Bean(typeof(M_ZAIKO_HINMEI))]
    public interface ZaikoHinmeiDao : IS2Dao
    {
        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("ZAIKO_HINMEI_CD = /*cd*/")]
        M_ZAIKO_HINMEI GetDataByCd(string cd);

        [Sql("select M_ZAIKO_HINMEI.ZAIKO_HINMEI_CD,M_ZAIKO_HINMEI.ZAIKO_HINMEI_RYAKU ,M_UNIT.UNIT_NAME_RYAKU  FROM M_ZAIKO_HINMEI LEFT JOIN M_UNIT ON M_ZAIKO_HINMEI.ZAIKO_UNIT_CD = M_UNIT.UNIT_CD group by M_ZAIKO_HINMEI.ZAIKO_HINMEI_CD,M_ZAIKO_HINMEI.ZAIKO_HINMEI_RYAKU,M_UNIT.UNIT_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup();
    }
}