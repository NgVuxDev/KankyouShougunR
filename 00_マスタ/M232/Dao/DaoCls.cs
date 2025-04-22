using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Collections.Generic;

namespace Shougun.Core.Master.CourseNyuryoku.Dao
{
    [Bean(typeof(M_COURSE))]
    public interface M_COURSE_DaoCls : IS2Dao
    {
        /*
            DAY_CD
            COURSE_NAME_CD
            COURSE_BIKOU
            TEKIYOU_BEGIN
            TEKIYOU_END
            CREATE_USER
            CREATE_DATE
            CREATE_PC
            UPDATE_USER
            UPDATE_DATE
            UPDATE_PC
            DELETE_FLG
            TIME_STAMP
        */

        //[Sql("SELECT * FROM M_COURSE")]
        //M_COURSE[] GetAllData();

        ///// <summary>
        ///// 削除フラグがたっていない適用期間内の情報を取得する
        ///// </summary>
        ///// <parameparam name="data">Entity</parameparam>
        ///// <returns>取得したデータのリスト</returns>
        //[SqlFile("")]
        //M_COURSE[] GetAllValidData(M_COURSE data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_COURSE data);

        [NoPersistentProps("TEKIYOU_BEGIN", "TEKIYOU_END", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "DELETE_FLG", "TIME_STAMP")]
        int Update(M_COURSE data);

        //int Delete(M_COURSE data);

        //[Sql("select M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_CD AS CD,M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_NAME_RYAKU AS NAME FROM M_CONTENA_JOUKYOU /*$whereSql*/ group by  M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_CD,M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_NAME_RYAKU")]
        //DataTable GetAllMasterDataForPopup(string whereSql);

        ///// <summary>
        ///// ユーザ指定の検索条件による一覧用データ取得
        ///// </summary>
        ///// <param name="path">SQLファイルパス</param>
        ///// <param name="data">Entity</param>
        ///// <returns></returns>
        //DataTable GetDataBySqlFile(string path, M_COURSE data);

        ///// <summary>
        ///// コードをもとにデータを取得する
        ///// </summary>
        ///// <returns>取得したデータ</returns>
        [Query("DAY_CD = /*DAY_CD*/ AND COURSE_NAME_CD = /*COURSE_NAME_CD*/")]
        M_COURSE GetDataByCd(string DAY_CD, string COURSE_NAME_CD);

        /// <summary>
        /// コース_荷降先の一覧データを取得
        /// </summary>
        /// <param name="data">M_COURSE data_M_COURSE</param>
        /// <param name="data">M_COURSE_NAME data_M_COURSE_NAME</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.CourseNyuryoku.Sql.M_COURSE_GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_COURSE data_M_COURSE, M_COURSE_NAME data_M_COURSE_NAME);
    }

    [Bean(typeof(M_COURSE_NIOROSHI))]
    public interface M_COURSE_NIOROSHI_DaoCls : IS2Dao
    {
        /*
                DAY_CD
                COURSE_NAME_CD
           *    NIOROSHI_NO
           *    NIOROSHI_GYOUSHA_CD
           *    NIOROSHI_GENBA_CD
                TIME_STAMP
        */

        //[Sql("SELECT * FROM M_COURSE_NIOROSHI")]
        //M_COURSE_NIOROSHI[] GetAllData();

        ///// <summary>
        ///// 削除フラグがたっていない適用期間内の情報を取得する
        ///// </summary>
        ///// <parameparam name="data">Entity</parameparam>
        ///// <returns>取得したデータのリスト</returns>
        //[SqlFile("")]
        //M_COURSE_NIOROSHI[] GetAllValidData(M_COURSE_NIOROSHI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_COURSE_NIOROSHI data);

        [NoPersistentProps("TIME_STAMP")]
        int Update(M_COURSE_NIOROSHI data);

        int Delete(M_COURSE_NIOROSHI data);

        //[Sql("select M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_CD AS CD,M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_NAME_RYAKU AS NAME FROM M_CONTENA_JOUKYOU /*$whereSql*/ group by  M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_CD,M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_NAME_RYAKU")]
        //DataTable GetAllMasterDataForPopup(string whereSql);

        ///// <summary>
        ///// ユーザ指定の検索条件による一覧用データ取得
        ///// </summary>
        ///// <param name="path">SQLファイルパス</param>
        ///// <param name="data">Entity</param>
        ///// <returns></returns>
        //DataTable GetDataBySqlFile(string path, M_COURSE_NIOROSHI data);

        ///// <summary>
        ///// コードをもとにデータを取得する
        ///// </summary>
        ///// <returns>取得したデータ</returns>
        [Query("DAY_CD = /*DAY_CD*/ AND COURSE_NAME_CD = /*COURSE_NAME_CD*/ AND NIOROSHI_NO = /*NIOROSHI_NO*/")]
        M_COURSE_NIOROSHI GetDataByCd(string DAY_CD, string COURSE_NAME_CD, string NIOROSHI_NO);

        /// <summary>
        /// コース_荷降先の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.CourseNyuryoku.Sql.M_COURSE_NIOROSHI_GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_COURSE_NIOROSHI data_M_COURSE_NIOROSHI, M_GYOUSHA data_M_GYOUSHA, M_GENBA data_M_GENBA);

        [SqlFile("Shougun.Core.Master.CourseNyuryoku.Sql.M_COURSE_NIOROSHI_GetDataForEntity.sql")]
        List<M_COURSE_NIOROSHI> GetDataForEntity(M_COURSE_NIOROSHI data);
    }

    [Bean(typeof(M_COURSE_DETAIL))]
    public interface DaoCls : IS2Dao
    {
        /*
                DAY_CD
                COURSE_NAME_CD
                ROW_NO
                SETTING_OFF_KBN
                NIOROSHI_NO
                GYOUSHA_CD
                GENBA_CD
                BIKOU
                SETTING_STATUS_KBN
                TEKIYOU_BEGIN
                TEKIYOU_END
                CREATE_USER
                CREATE_DATE
                CREATE_PC
                UPDATE_USER
                UPDATE_DATE
                UPDATE_PC
                DELETE_FLG
                TIME_STAMP
        */

        [Sql("SELECT * FROM M_COURSE_DETAIL")]
        M_COURSE_DETAIL[] GetAllData();

        ///// <summary>
        ///// 削除フラグがたっていない適用期間内の情報を取得する
        ///// </summary>
        ///// <parameparam name="data">Entity</parameparam>
        ///// <returns>取得したデータのリスト</returns>
        //[SqlFile("")]
        //M_COURSE_DETAIL[] GetAllValidData(M_COURSE_DETAIL data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_COURSE_DETAIL data);

        [NoPersistentProps("DAY_CD", "COURSE_NAME_CD", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_COURSE_DETAIL data);

        /// <summary>
        /// コース詳細のレコードを削除します
        /// </summary>
        /// <param name="data">条件エンティティ</param>
        /// <returns>削除した件数</returns>
        int Delete(M_COURSE_DETAIL data);

        //[Sql("select M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_CD AS CD,M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_NAME_RYAKU AS NAME FROM M_CONTENA_JOUKYOU /*$whereSql*/ group by  M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_CD,M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_NAME_RYAKU")]
        //DataTable GetAllMasterDataForPopup(string whereSql);

        ///// <summary>
        ///// ユーザ指定の検索条件による一覧用データ取得
        ///// </summary>
        ///// <param name="path">SQLファイルパス</param>
        ///// <param name="data">Entity</param>
        ///// <returns></returns>
        //DataTable GetDataBySqlFile(string path, M_COURSE_DETAIL data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("DAY_CD = /*DAY_CD*/ AND COURSE_NAME_CD = /*COURSE_NAME_CD*/ AND REC_ID = /*REC_ID*/")]
        M_COURSE_DETAIL GetDataByCd(string DAY_CD, string COURSE_NAME_CD, string REC_ID);

        // QN Tue Anh #158986 START
        [Sql("SELECT * FROM M_COURSE_DETAIL WHERE DAY_CD = /*DAY_CD*/ AND COURSE_NAME_CD = /*COURSE_NAME_CD*/")]
        M_COURSE_DETAIL[] GetCourseDetailDatabyCd(string DAY_CD, string COURSE_NAME_CD);
        // QN Tue Anh #158986 END

        /// <summary>
        /// コース詳細の一覧用データを取得します
        /// </summary>
        /// <param name="data_M_COURSE_DETAIL">条件エンティティ</param>
        /// <returns>コース詳細一覧</returns>
        [SqlFile("Shougun.Core.Master.CourseNyuryoku.Sql.M_COURSE_DETAIL_GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_COURSE_DETAIL data_M_COURSE_DETAIL);


        [SqlFile("Shougun.Core.Master.CourseNyuryoku.Sql.M_GENBA_TEIKI_HINMEI_CheckDataSql.sql")]
        DataTable CheckIchiranDataSql(string GYOUSHA_CD, string GENBA_CD, string DAY_CD, string COURSE_NAME_CD, string REC_ID, string dayCd);
        /// <summary>
        /// M_COURSE_DETAILの [GROUP BY DAY_CD,COURSE_NAME_CD] の MAX(REC_ID) を MAX_REC_ID で取得
        /// </summary>
        /// <param name="data">data_M_COURSE_DETAIL</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.CourseNyuryoku.Sql.M_COURSE_DETAIL_GetMaxIdByCdSql.sql")]
        DataTable GetMaxIdByCd(M_COURSE_DETAIL data_M_COURSE_DETAIL);

        /// <summary>
        /// M_COURSE_DETAILの [GROUP BY DAY_CD,COURSE_NAME_CD] の MAX(ROW_NO) を MAX_ROW_NO で取得
        /// </summary>
        /// <param name="data">data_M_COURSE_DETAIL</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.CourseNyuryoku.Sql.M_COURSE_DETAIL_GetMaxNoByCdSql.sql")]
        DataTable GetMaxNoByCd(M_COURSE_DETAIL data_M_COURSE_DETAIL);

        [SqlFile("Shougun.Core.Master.CourseNyuryoku.Sql.M_COURSE_DETAIL_GetDataForEntity.sql")]
        List<M_COURSE_DETAIL> GetDataForEntity(M_COURSE_DETAIL data);
    }

    [Bean(typeof(M_COURSE_DETAIL_ITEMS))]
    public interface M_COURSE_DETAIL_ITEMS_DaoCls : IS2Dao
    {
        /*
                DAY_CD
                COURSE_NAME_CD
                REC_ID
                REC_SEQ
                HINMEI_CD
                PRINT_STRING
                UNIT_CD
                YOUKI_CD
                KANSANCHI
                KANSAN_UNIT_CD
                MONDAY
                TUESDAY
                WEDNESDAY
                THURSDAY
                FRIDAY
                SATURDAY
                SUNDAY
                KEIYAKU_KBN
                TSUKI_HINMEI_CD
                TANKA
                KEIJYOU_KBN
                TANKA_TEKIYOU_BEGIN
                TANKA_TEKIYOU_END
                KAISHUU_DATE_END
                TIME_STAMP
        */

        [Sql("SELECT * FROM M_COURSE_DETAIL_ITEMS")]
        M_COURSE_DETAIL_ITEMS[] GetAllData();

        ///// <summary>
        ///// 削除フラグがたっていない適用期間内の情報を取得する
        ///// </summary>
        ///// <parameparam name="data">Entity</parameparam>
        ///// <returns>取得したデータのリスト</returns>
        //[SqlFile("")]
        //M_COURSE_DETAIL[] GetAllValidData(M_COURSE_DETAIL data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_COURSE_DETAIL_ITEMS data);

        [NoPersistentProps("DAY_CD", "COURSE_NAME_CD", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_COURSE_DETAIL_ITEMS data);

        int Delete(M_COURSE_DETAIL_ITEMS data);

        //[Sql("select M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_CD AS CD,M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_NAME_RYAKU AS NAME FROM M_CONTENA_JOUKYOU /*$whereSql*/ group by  M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_CD,M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_NAME_RYAKU")]
        //DataTable GetAllMasterDataForPopup(string whereSql);

        ///// <summary>
        ///// ユーザ指定の検索条件による一覧用データ取得
        ///// </summary>
        ///// <param name="path">SQLファイルパス</param>
        ///// <param name="data">Entity</param>
        ///// <returns></returns>
        //DataTable GetDataBySqlFile(string path, M_COURSE_DETAIL data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("DAY_CD = /*DAY_CD*/ AND COURSE_NAME_CD = /*COURSE_NAME_CD*/ AND REC_ID = /*REC_ID*/ AND REC_SEQ = /*REC_SEQ*/")]
        M_COURSE_DETAIL_ITEMS GetDataByCd(string DAY_CD, string COURSE_NAME_CD, string REC_ID, string REC_SEQ);

        // QN Tue Anh #158986 START
        [Sql("SELECT * FROM M_COURSE_DETAIL_ITEMS WHERE DAY_CD = /*DAY_CD*/ AND COURSE_NAME_CD = /*COURSE_NAME_CD*/ AND REC_ID = /*REC_ID*/")]
        M_COURSE_DETAIL_ITEMS[] GetCourseDetailItemsDataByCd(string DAY_CD, string COURSE_NAME_CD, string REC_ID);
        // QN Tue Anh #158986 END

        /// <summary>
        /// コンテナ状況画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.CourseNyuryoku.Sql.M_COURSE_DETAIL_ITEMS_GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_COURSE_DETAIL_ITEMS data_M_COURSE_DETAIL_ITEMS, M_GYOUSHA data_M_GYOUSHA, M_GENBA data_M_GENBA, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg, bool ichiranFlag, string dayCd);

        [SqlFile("Shougun.Core.Master.CourseNyuryoku.Sql.M_COURSE_DETAIL_ITEMS_GetMaxIdByCdSql.sql")]
        DataTable GetMaxIdByCd(M_COURSE_DETAIL_ITEMS data_M_COURSE_DETAIL_ITEMS);

        [SqlFile("Shougun.Core.Master.CourseNyuryoku.Sql.M_COURSE_DETAIL_ITEMS_GetDataForEntity.sql")]
        List<M_COURSE_DETAIL_ITEMS> GetDataForEntity(M_COURSE_DETAIL_ITEMS data);
    }

    [Bean(typeof(M_GENBA_TEIKI_HINMEI))]
    public interface M_GENBA_TEIKI_HINMEI_DaoCls : IS2Dao
    {
        /*
                DAY_CD
                COURSE_NAME_CD
           *    NIOROSHI_NO
           *    NIOROSHI_GYOUSHA_CD
           *    NIOROSHI_GENBA_CD
                TIME_STAMP
        */

        //[Sql("SELECT * FROM M_COURSE_NIOROSHI")]
        //M_COURSE_NIOROSHI[] GetAllData();

        ///// <summary>
        ///// 削除フラグがたっていない適用期間内の情報を取得する
        ///// </summary>
        ///// <parameparam name="data">Entity</parameparam>
        ///// <returns>取得したデータのリスト</returns>
        //[SqlFile("")]
        //M_COURSE_NIOROSHI[] GetAllValidData(M_COURSE_NIOROSHI data);

        //[NoPersistentProps("TIME_STAMP")]
        //int Insert(M_COURSE_NIOROSHI data);

        //[NoPersistentProps("DAY_CD", "COURSE_NAME_CD", "TIME_STAMP")]
        //int Update(M_COURSE_NIOROSHI data);

        //int Delete(M_COURSE_NIOROSHI data);

        //[Sql("select M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_CD AS CD,M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_NAME_RYAKU AS NAME FROM M_CONTENA_JOUKYOU /*$whereSql*/ group by  M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_CD,M_CONTENA_JOUKYOU.CONTENA_JOUKYOU_NAME_RYAKU")]
        //DataTable GetAllMasterDataForPopup(string whereSql);

        ///// <summary>
        ///// ユーザ指定の検索条件による一覧用データ取得
        ///// </summary>
        ///// <param name="path">SQLファイルパス</param>
        ///// <param name="data">Entity</param>
        ///// <returns></returns>
        //DataTable GetDataBySqlFile(string path, M_COURSE_NIOROSHI data);

        ///// <summary>
        ///// コードをもとにデータを取得する
        ///// </summary>
        ///// <returns>取得したデータ</returns>
        //[Query("DAY_CD = /*DAY_CD*/ AND COURSE_NAME_CD = /*COURSE_NAME_CD*/ AND ROW_NO = /*ROW_NO*/")]
        //M_COURSE_NIOROSHI GetDataByCd(string DAY_CD, string COURSE_NAME_CD, string ROW_NO);

        /// <summary>
        /// 現場_定期品名の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        //[SqlFile("Shougun.Core.Master.CourseNyuryoku.Sql.M_GENBA_TEIKI_HINMEI_GetIchiranDataSql.sql")]
        //DataTable GetIchiranDataSql(M_GENBA_TEIKI_HINMEI data_M_GENBA_TEIKI_HINMEI, M_GYOUSHA data_M_GYOUSHA, M_GENBA data_M_GENBA);

        /// <summary>
        /// 現場_定期品名の一覧データを取得
        /// </summary>
        /// <param name="data_M_GENBA_TEIKI_HINMEI"></param>
        /// <param name="shikuChousonCd"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.CourseNyuryoku.Sql.M_GENBA_TEIKI_HINMEI_GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_GENBA_TEIKI_HINMEI data_M_GENBA_TEIKI_HINMEI, string shikuChousonCd, string dayCd);
    }

    [Bean(typeof(M_GYOUSHA))]
    public interface M_GYOUSHA_DaoCls : IS2Dao
    {
        [SqlFile("Shougun.Core.Master.CourseNyuryoku.Sql.M_GYOUSHA_DataSql.sql")]
        DataTable GetDataSql(M_GYOUSHA data);

        // --20140120 oonaka add 業者フォーカスアウトチェック追加 start ---
        /// <summary>
        /// 業者CDフォーカスアウト時のチェックに使用
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        new DataTable GetDateForStringSql(string sql);
        // --20140120 oonaka add 業者フォーカスアウトチェック追加 end ---
    }

    [Bean(typeof(M_COURSE_NAME))]
    public interface M_COURSE_NAME_DaoCls : IS2Dao
    {
        [SqlFile("Shougun.Core.Master.CourseNyuryoku.Sql.M_COURSE_NAME_DataSql.sql")]
        DataTable GetDataSql(M_COURSE_NAME data, bool monday, bool tuesday, bool wednesday, bool thursday, bool friday, bool saturday, bool sunday);
    }

    /// <summary>
    /// 現場取得区分検索
    /// </summary>
    [Bean(typeof(M_GENBA))]
    public interface GetGenbaDaoCls : IS2Dao
    {
        // --20140117 oonaka delete 現場フォーカスアウトチェック修正 start ---
        ///// <summary>
        ///// Entityで絞り込んで値を取得する
        ///// </summary>
        //[SqlFile("Shougun.Core.Master.CourseNyuryoku.Sql.GetGenba.sql")]
        //new DataTable GetDataForEntity(GetGenbaDtoCls data);
        // --20140117 oonaka delete 現場フォーカスアウトチェック修正 end ---

        // --20140117 oonaka add 現場フォーカスアウトチェック修正 start ---
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.Master.CourseNyuryoku.Sql.GetGenba.sql")]
        DataTable GetDataForEntity(GetGenbaDtoCls data, bool nioroshiFlg = false);
        // --20140117 oonaka add 現場フォーカスアウトチェック修正 end ---
    }

    /// <summary>
    /// 地図表示用Dao取得
    /// </summary>
    [Bean(typeof(M_GENBA))]
    public interface GetMapGenbaDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで地図表示用の値を取得する(業者/現場)
        /// </summary>
        [SqlFile("Shougun.Core.Master.CourseNyuryoku.Sql.GetMapGenba.sql")]
        DataTable GetMapDataForEntity(GetGenbaDtoCls data);

        /// <summary>
        /// Entityで絞り込んで地図表示用の値を取得する(業者)
        /// </summary>
        [SqlFile("Shougun.Core.Master.CourseNyuryoku.Sql.GetMapGyousha.sql")]
        DataTable GetMapGyoushaDataForEntity(GetGenbaDtoCls data);
    }
}
