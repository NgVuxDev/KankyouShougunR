using System;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SqlTypes;
using Shougun.Core.Common.IchiranCommon.Dto;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Common.IchiranCommon.Dao
{
    [Bean(typeof(M_OUTPUT_PATTERN))]
    public interface  MOPDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_OUTPUT_PATTERN data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_OUTPUT_PATTERN data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(M_OUTPUT_PATTERN data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        System.Data.DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.IchiranCommon.Sql.GetIchiranPaterndataSql.sql")]
        new DataTable GetDataForEntity(PatternIchiranDto data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        new DataTable GetDateForStringSql(string sql);


        //データの有無を確認
        /// <summary>
        /// 明細の更新前に、削除されていないか確認する。
        /// SEQをあえて入れないことで、変更した場合でもデータを確認できるようにしている。
        /// </summary>
        /// <param name="SYSTEM_ID">SYSTEM_ID</param>
        /// <param name="DELETE_FLG">DELETE_FLG</param>
        /// <returns></returns>
        DataTable CheckExistRecord(SqlInt64 SYSTEM_ID , SqlBoolean DELETE_FLG);

    }

    [Bean(typeof(M_OUTPUT_PATTERN_KOBETSU))]
    public interface MOPKDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_OUTPUT_PATTERN_KOBETSU data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_OUTPUT_PATTERN_KOBETSU data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(M_OUTPUT_PATTERN_KOBETSU data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        new DataTable GetDateForStringSql(string sql);
    }
}
