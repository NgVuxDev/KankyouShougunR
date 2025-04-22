using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Scale.KeiryouIchiran.DAO
{
    [Bean(typeof(T_KEIRYOU_ENTRY))]
    internal interface DAOClass : IS2Dao
    {
        /// <summary>
        /// 計量 表示用滞留数/台数取得
        /// </summary>
        /// <param name="tairyuKbn">滞留登録区分（0 通常登録　1 滞留登録）</param>
        /// <param name="honjitsuKbn">本日のみ取得する場合は1をセット、それ以外は0</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <param name="inoutKbn">基本計量　0：指定なし、1：受入、2：出荷</param>
        /// <returns>件数</returns>
        [SqlFile("Shougun.Core.Scale.KeiryouIchiran.Sql.GetEntryCount.sql")]
        long GetEntryCount(int tairyuKbn, int honjitsuKbn, string kyotenCd, int inoutKbn = 0);

        /// <summary>
        /// 計量 表示用数量取得
        /// </summary>
        /// <param name="kyotenCd">拠点CD</param>
        /// <param name="inoutKbn">基本計量　0：指定なし、1：受入、2：出荷</param>
        /// <returns>数量</returns>
        [SqlFile("Shougun.Core.Scale.KeiryouIchiran.Sql.GetNetTotal.sql")]
        decimal GetNetTotal(string kyotenCd, int inoutKbn = 0);

        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable getdateforstringsql(string sql);
    }
}
