using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace FixChouhyou
{
    /// <summary>マスターリストパターンに関するインターフェイス</summary>
    [Bean(typeof(M_LIST_PATTERN))]
    public interface IMLPDao : IS2Dao
    {
        /// <summary>Insert</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_LIST_PATTERN data);

        /// <summary>Update</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_LIST_PATTERN data);

        /// <summary>Delete</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(M_LIST_PATTERN data);

        /// <summary>使用しない</summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>Entityで絞り込んで値を取得する</summary>
        /// <param name="date"></param>
        /// <returns></returns>
        //[SqlFile("Shougun.Core.Billing.SeikyuushoHakkou.Sql.GetSeikyuuDenpyodataSql.sql")]
        new DataTable GetDataForEntity(M_LIST_PATTERN date);

        /// <summary>使用しない</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>SQL構文からデータの取得を行う</summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>Entityで絞り込んでレコード数を取得する</summary>
        /// <param name="data"></param>
        /// <returns>レコード数</returns>
        [SqlFile("Shougun.Core.Common.MeisaihyoSyukeihyoPatternSentakuPopup.Sql.MListPatternCount.sql")]
        int GetMListPatternCount();

        /// <summary>Entityで絞り込んで値を取得する</summary>
        /// <param name="data"></param>
        /// <returns>データーテーブル</returns>
        [SqlFile("Shougun.Core.Common.MeisaihyoSyukeihyoPatternSentakuPopup.Sql.MListPatternData.sql")]
        DataTable GetMListPatternData(int windowID);
    }
}
