using System;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.FileUpload.FileUploadCommon
{

    /// <summary>
    /// ファイルデータDAO
    /// </summary>
    [Bean(typeof(T_FILE_DATA))]
    public interface FILE_DATADAO : IS2Dao
    {
        /// <summary>
        /// ファイルデータ
        /// ファイルIDに合致するデータを取得する。
        /// </summary>
        /// <returns></returns>
        [Query("FILE_ID = /*idCD*/")]
        T_FILE_DATA GetDataByKey(long idCD);

        /// <summary>
        /// ファイルデータ
        /// ファイルIDに合致するデータを取得する。
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.FileUpload.FileUploadCommon.Sql.GetDataByKeyList.sql")]
        List<T_FILE_DATA> GetDataByKeyList(List<long> fileIdList);

        /// <summary>
        /// ファイルデータ
        /// ファイルIDに合致するデータを取得する。
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.FileUpload.FileUploadCommon.Sql.GetLightDataByKey.sql")]
        T_FILE_DATA GetLightDataByKey(long idCD);

        /// <summary>
        /// ファイルデータ
        /// ファイルIDに合致するデータを取得する。
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.FileUpload.FileUploadCommon.Sql.GetLightDataByKeyList.sql")]
        List<T_FILE_DATA> GetLightDataByKeyList(List<long> fileIdList);

        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_FILE_DATA data);

        /// <summary>
        /// Entityを元に更新処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_FILE_DATA data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_FILE_DATA data);

        /// <summary>
        /// SQLを実行してデータを取得する。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        DataTable GetDataBySqlFile(string sql);

        /// <summary>
        /// ファイルサイズの合計取得
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT ISNULL(SUM(FILE_LENGTH),0) FROM T_FILE_DATA")]
        SqlInt64 GetSumFileLength();
    }

    [Bean(typeof(S_NUMBER_FILE))]
    public interface NUMBER_FILEDAO : IS2Dao
    {
        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM S_NUMBER_FILE")]
        List<S_NUMBER_FILE> GetAllData();

        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(S_NUMBER_FILE data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(S_NUMBER_FILE data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        int Delete(S_NUMBER_FILE data);

        /// <summary>
        /// 伝種連番の最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(CURRENT_NUMBER),0)+1 FROM S_NUMBER_FILE")]
        int GetMaxPlusKey();
    }
}
