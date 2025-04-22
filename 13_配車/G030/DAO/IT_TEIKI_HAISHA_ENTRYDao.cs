// $Id: IT_TEIKI_HAISHA_ENTRYDao.cs 36292 2014-12-02 02:43:29Z fangjk@oec-h.com $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.TeikiHaishaNyuuryoku
{
    [Bean(typeof(T_TEIKI_HAISHA_ENTRY))]
    public interface IT_TEIKI_HAISHA_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// Entityを元にアップデート処理を行う（論理削除）
        /// </summary>
        /// <parameparam name="data">T_TEIKI_HAISHA_ENTRY</parameparam>
        [NoPersistentProps("KYOTEN_CD", "TEIKI_HAISHA_NUMBER", "FURIKAE_HAISHA_KBN", "DENPYOU_DATE",
            "SAGYOU_DATE","SAGYOU_BEGIN_HOUR","SAGYOU_BEGIN_MINUTE","SAGYOU_END_HOUR","SAGYOU_END_MINUTE",
            "COURSE_NAME_CD", "SHARYOU_CD", "SHASHU_CD", "UNTENSHA_CD", "HOJOIN_CD", "DAY_CD",
            "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// 定期配車番号をもとに定期配車入力のデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetEntryData.sql")]
        new DataTable GetEntryData(DTOClass data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        new DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// モバイル連携可能かチェックし、データを取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetDetailForMiTourokuHaisha.sql")]
        DataTable GetDataToMRDataTable(DTOClass data);

        /// <summary>
        /// 定期配車荷降行取得
        /// </summary>
        /// <param name="HAISHA_DENPYOU_NO">HAISHA_DENPYOU_NO</param>
        /// <param name="NIOROSHI_NUMBER">NIOROSHI_NUMBER</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetMobilNioroshiData.sql")]
        DataTable GetMobilNioroshiData(SqlInt64 TEIKI_HAISHA_NUMBER, int NIOROSHI_NUMBER);

        /// <summary>
        /// 定期配車荷降データを取得
        /// </summary>
        /// <param name="SYSTEM_ID">SYSTEM_ID</param>
        /// <param name="SEQ">SEQ</param>
        /// <param name="NIOROSHI_NUMBER">NIOROSHI_NUMBER</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetTeikiHaishaNioroshiData.sql")]
        DataTable GetTeikiHaishaNioroshiData(SqlInt64 SYSTEM_ID, SqlInt32 SEQ, SqlInt32 NIOROSHI_NUMBER);

        /// <summary>
        /// 定期配車取引先有無検索
        /// </summary>
        /// <param name="HAISHA_DENPYOU_NO">HAISHA_DENPYOU_NO</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetTeikiHaishaTorihikisakiUmu.sql")]
        DataTable GetTeikiHaishaTorihikisakiUmu(SqlInt64 TEIKI_HAISHA_NUMBER);

        /// <summary>
        /// 定期配車取引先有無検索
        /// </summary>
        /// <param name="HAISHA_DENPYOU_NO">HAISHA_DENPYOU_NO</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetTeikiHaishaTorihikisakiUmuall.sql")]
        DataTable GetTeikiHaishaTorihikisakiUmuall(string GYOUSHA_CD);

        /// <summary>
        /// SEQの最高値を取得する
        /// </summary>
        /// <returns>SEQのMAX値</returns>
        [Sql("SELECT ISNULL(MAX(SEQ),1) FROM T_TEIKI_HAISHA_ENTRY WHERE TEIKI_HAISHA_NUMBER = /*teikiHaishaNumber*/")]
        string GetMaxSeq(string teikiHaishaNumber);
    }
}
