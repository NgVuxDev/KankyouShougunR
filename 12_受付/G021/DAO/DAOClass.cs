using System.Data;
// $Id: DAOClass.cs 20055 2014-04-28 05:20:13Z ogawa@takumi-sys.co.jp $
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Reception.UketukeiIchiran
{
    [Bean(typeof(T_UKETSUKE_SS_ENTRY))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable getdateforstringsql(string sql);
    }

    /// <summary>
    /// 受入入力DAO
    /// </summary>
    [Bean(typeof(T_UKEIRE_ENTRY))]
    public interface UkeireDAOClass : IS2Dao
    {
        /// <summary>
        /// 受入入力データを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns>データ</returns>
        [Sql("SELECT COUNT(*) FROM T_UKEIRE_ENTRY WHERE UKETSUKE_NUMBER = /*data.UKETSUKE_NUMBER*/ AND DELETE_FLG = 0 ")]
        int GetUkeireCount(T_UKEIRE_ENTRY data);
    }

    /// <summary>
    /// 出荷入力DAO
    /// </summary>
    [Bean(typeof(T_SHUKKA_ENTRY))]
    public interface ShukkaDAOClass : IS2Dao
    {
        /// <summary>
        /// 出荷入力データを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns>データ</returns>
        [Sql("SELECT COUNT(*) FROM T_SHUKKA_ENTRY WHERE UKETSUKE_NUMBER = /*data.UKETSUKE_NUMBER*/ AND DELETE_FLG = 0 ")]
        int GetShukkaCount(T_SHUKKA_ENTRY data);
    }

    /// <summary>
    /// 売上／支払入力DAO
    /// </summary>
    [Bean(typeof(T_UR_SH_ENTRY))]
    public interface UR_SHDAOClass : IS2Dao
    {
        /// <summary>
        /// 出荷入力データを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns>データ</returns>
        [Sql("SELECT COUNT(*) FROM T_UR_SH_ENTRY WHERE UKETSUKE_NUMBER = /*data.UKETSUKE_NUMBER*/ AND DELETE_FLG = 0 ")]
        int GetUR_ShCount(T_UR_SH_ENTRY data);
    }

    /// <summary>
    /// 計量入力DAO
    /// </summary>
    [Bean(typeof(T_KEIRYOU_ENTRY))]
    public interface KeiryoDAOClass : IS2Dao
    {
        /// <summary>
        /// 計量入力の件数を取得します
        /// </summary>
        /// <param name="keyEntity">条件エンティティ</param>
        /// <returns>件数</returns>
        [Sql("SELECT COUNT(*) FROM T_KEIRYOU_ENTRY WHERE UKETSUKE_NUMBER = /*keyEntity.UKETSUKE_NUMBER*/ AND DELETE_FLG = 0")]
        int GetKeiryoCount(T_KEIRYOU_ENTRY keyEntity);
    }

    /// <summary>
    /// 定期配車詳細DAO
    /// </summary>
    [Bean(typeof(T_TEIKI_HAISHA_DETAIL))]
    public interface TeikiHaishaDetailDao : IS2Dao
    {
        /// <summary>
        /// 定期配車詳細を受付番号で検索した件数を取得します
        /// </summary>
        /// <param name="keyEntity">条件エンティティ</param>
        /// <returns>件数</returns>
        [Sql("SELECT COUNT(*) FROM T_TEIKI_HAISHA_ENTRY JOIN T_TEIKI_HAISHA_DETAIL ON T_TEIKI_HAISHA_ENTRY.SYSTEM_ID = T_TEIKI_HAISHA_DETAIL.SYSTEM_ID AND T_TEIKI_HAISHA_ENTRY.SEQ = T_TEIKI_HAISHA_DETAIL.SEQ WHERE T_TEIKI_HAISHA_DETAIL.UKETSUKE_NUMBER = /*keyEntity.UKETSUKE_NUMBER*/ AND T_TEIKI_HAISHA_ENTRY.DELETE_FLG = 0")]
        int GetTeikiHaishaDetailCount(T_TEIKI_HAISHA_DETAIL keyEntity);
    }
}
