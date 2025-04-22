using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// 受付伝票データDao
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SLIP))]
    public interface IT_UKETSUKE_SLIPDao : IS2Dao
    {
        /// <summary>
        /// 全レコード取得処理
        /// </summary>
        [Sql("SELECT * FROM T_UKETSUKE_SLIP")]
        T_UKETSUKE_SLIP[] GetAllData();

        /// <summary>
        /// Insert処理
        /// </summary>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SLIP data);

        /// <summary>
        /// 更新処理（"CREATE_USER", "CREATE_DATE", "CREATE_PC"を更新対象に含めない）
        /// </summary>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UKETSUKE_SLIP data);

        /// <summary>
        /// レコード削除処理
        /// </summary>
        int Delete(T_UKETSUKE_SLIP data);

        /// <summary>
        /// 論理削除フラグ更新処理（"DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC"のみを更新する）
        /// </summary>
        [PersistentProps("DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC")]
        int UpdateLogicalDeleteFlag(T_UKETSUKE_SLIP data);

        /// <summary>
        /// 受付番号の最大値を取得する処理
        /// </summary>
        [Sql("SELECT ISNULL(MAX(UKETSUKE_NO),1) FROM T_UKETSUKE_SLIP")]
        int GetMaxKey();

        /// <summary>
        /// 受付番号の最大値+1を取得する処理
        /// </summary>
        [Sql("SELECT ISNULL(MAX(UKETSUKE_NO),0)+1 FROM T_UKETSUKE_SLIP")]
        int GetMaxPlusKey();

        /// <summary>
        /// 受付番号の最小値を取得する処理
        /// </summary>
        [Sql("SELECT ISNULL(MIN(UKETSUKE_NO),1) FROM T_UKETSUKE_SLIP")]
        int GetMinKey();

        /// <summary>
        /// 論理削除が行われていないデータに対して受付番号の最大値を取得する処理
        /// </summary>
        [Sql("SELECT ISNULL(MAX(UKETSUKE_NO),1) FROM T_UKETSUKE_SLIP")]
        int GetMaxKeyIsNotDelete();

        /// <summary>
        /// 論理削除が行われていないデータに対して受付番号の最大値+1を取得する処理
        /// </summary>
        [Sql("SELECT ISNULL(MAX(UKETSUKE_NO),0)+1 FROM T_UKETSUKE_SLIP")]
        int GetMaxPlusKeyNotDelete();

        /// <summary>
        /// 受付番号から伝票のデータを取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [Query("UKETSUKE_NO = /*data.UKETSUKE_NO*/")]
        T_UKETSUKE_SLIP GetUketsukeData(T_UKETSUKE_SLIP data);

        /// <summary>
        /// 受付番号にて対象のデータが存在しているかを確認する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT Count(*) FROM T_UKETSUKE_SLIP where UKETSUKE_NO = /*data.UKETSUKE_NO*/")]
        int GetExtistCheck(T_UKETSUKE_SLIP data);

        /// <summary>
        /// 伝票グループ番号の最大値+1を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [Sql("select MAX(SLIP_GROUP_NO)+1 from T_UKETSUKE_SLIP")]
        int GetMaxPlusDenpyoGroupNo();

        /// <summary>
        /// 現在の受付番号より小さくかつ、その中で一番大きい受付番号を持つデータの取得を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [Sql("select * from T_UKETSUKE_SLIP SLIP2, (select max(UKETSUKE_NO) as UKETSUKE_NO from T_UKETSUKE_SLIP where T_UKETSUKE_SLIP.UKETSUKE_NO < /*data.UKETSUKE_NO*/) SLIP1 where SLIP2.UKETSUKE_NO = SLIP1.UKETSUKE_NO")]
        T_UKETSUKE_SLIP GetBeforeUketsukeNo(T_UKETSUKE_SLIP data);

        /// <summary>
        /// 現在の受付番号より大きくかつ、その中で一番小さな受付番号を持つデータの取得を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [Sql("select * from T_UKETSUKE_SLIP SLIP2, (select MIN(UKETSUKE_NO) as UKETSUKE_NO from T_UKETSUKE_SLIP where T_UKETSUKE_SLIP.UKETSUKE_NO > /*data.UKETSUKE_NO*/) SLIP1 where SLIP2.UKETSUKE_NO = SLIP1.UKETSUKE_NO")]
        T_UKETSUKE_SLIP GetAfterUketsukeNo(T_UKETSUKE_SLIP data);

        /// <summary>
        /// 現在の受付番号の次の受付番号を持つ同一グループのデータを取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.UketsukeSlip.IT_UKETSUKE_SLIPDao_GetAfterDaisuu.sql")]
        T_UKETSUKE_SLIP GetAfterDaisuu(T_UKETSUKE_SLIP data);

        /// <summary>
        /// 現在の受付番号の前の受付番号を持つ同一グループのデータを取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.UketsukeSlip.IT_UKETSUKE_SLIPDao_GetBeforeDaisuu.sql")]
        T_UKETSUKE_SLIP GetBeforeDaisuu(T_UKETSUKE_SLIP data);

        /// <summary>
        /// 最大受付番号をもつデータを取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.UketsukeSlip.IT_UKETSUKE_SLIPDao_GetMaxDate.sql")]
        T_UKETSUKE_SLIP GetMaxUketsukeNoDate();

        /// <summary>
        /// 最小受付番号をもつデータを取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.UketsukeSlip.IT_UKETSUKE_SLIPDao_GetMinDate.sql")]
        T_UKETSUKE_SLIP GetMinUketsukeNoDate();

        /// <summary>
        /// 同一グループ内にて最大の受付番号を持つデータの取得を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.UketsukeSlip.IT_UKETSUKE_SLIPDao_GetMaxDateForGroup.sql")]
        T_UKETSUKE_SLIP GetMaxUketsukeNoDateForGroup(int groupNo);

        /// <summary>
        /// 同一グループ内にて最小の受付番号を持つデータの取得を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.UketsukeSlip.IT_UKETSUKE_SLIPDao_GetMinDateForGroup.sql")]
        T_UKETSUKE_SLIP GetMinUketsukeNoDateForGroup(int groupNo);

        /// <summary>
        /// 伝票グループ番号と台数_分子を基に受付番号を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>受付番号</returns>
        [Sql("SELECT UKETSUKE_NO FROM T_UKETSUKE_SLIP where SLIP_GROUP_NO = /*data.SLIP_GROUP_NO*/ and DAISUU_NUMERATOR = /*data.DAISUU_NUMERATOR*/")]
        int GetUketsukeDataForGroup(T_UKETSUKE_SLIP data);

        T_UKETSUKE_SLIP GetUketsukeDataForSqlFile(string path, T_UKETSUKE_SLIP data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, T_UKETSUKE_SLIP data);
    }
}