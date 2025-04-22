// $Id: DaoCls.cs 55358 2015-07-10 09:17:26Z nagata $
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Collections.Generic;

namespace Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku
{
    [Bean(typeof(T_TEIKI_JISSEKI_ENTRY))]
    public interface IT_TEIKI_JISSEKI_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_JISSEKI_ENTRY data);

        /// <summary>
        /// Entityを元にアップデート処理を行う（論理削除）
        /// </summary>
        /// <parameparam name="data">T_TEIKI_HAISHA_ENTRY</parameparam>
        [NoPersistentProps("KYOTEN_CD", "TEIKI_JISSEKI_NUMBER", "WEATHER", "SHUKKO_METER", "SHUKKO_HOUR", "SHUKKO_MINUTE",
             "KIKO_METER", "KIKO_HOUR", "KIKO_MINUTE", "TEIKI_HAISHA_NUMBER", "MOBILE_SHOGUN_FILE_NAME", "DENPYOU_DATE",
            "SAGYOU_DATE", "COURSE_NAME_CD", "SHARYOU_CD", "SHASHU_CD", "UNTENSHA_CD", "HOJOIN_CD", "DAY_CD",
            "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_TEIKI_JISSEKI_ENTRY data);

        /// <summary>
        /// Entityを元に削除処理を行う 
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>       
        int Delete(T_TEIKI_JISSEKI_ENTRY data);

        /// <summary>
        /// 定期実績番号をもとに定期実績入力のデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku.Sql.GetEntryData.sql")]
        new DataTable GetEntryData(DTOClass data);

        /// <summary>
        /// 定期配車番号をもとに定期実績入力のデータをあるかとうか判断します。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku.Sql.ExistHaishaNumber.sql")]
        new DataTable ExistHaishaNumber(DTOClass data);
        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// 定期配車番号をもとに定期実績入力のデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku.Sql.GetHaisyaJissekiEntryData.sql")]
        new DataTable GetHaisyaJissekiEntryData(DTOClass data);
    }

    [Bean(typeof(T_TEIKI_JISSEKI_DETAIL))]
    public interface IT_TEIKI_JISSEKI_DETAILDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_JISSEKI_DETAIL data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_TEIKI_JISSEKI_DETAIL data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_TEIKI_JISSEKI_DETAIL data);

        /// <summary>
        /// システムIDと枝番をもとに定期実績明細のデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku.Sql.GetDetailData.sql")]
        new DataTable GetDetailData(DTOClass data);

        /// <summary>
        /// システムIDと枝番をもとに定期実績明細のデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku.Sql.GetKansanData.sql")]
        new DataTable GetKansanData(DTOClass data);

        /// <summary>
        /// システムIDと枝番をもとに定期実績明細のデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku.Sql.GetCourseKansanData.sql")]
        new DataTable GetCourseKansanData(DTOClass data);

        /// <summary>
        /// システムIDと枝番をもとに定期実績明細のデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku.Sql.GetTeikiKansanData.sql")]
        new DataTable GetTeikiKansanData(DTOClass data);

        /// <summary>
        /// 定期実績明細のデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku.Sql.GetTeikiJissekiDetailData.sql")]
        T_TEIKI_JISSEKI_DETAIL[] GetTeikiJissekiDetailData(string UrshNumber);
    }

    [Bean(typeof(T_TEIKI_JISSEKI_NIOROSHI))]
    public interface IT_TEIKI_JISSEKI_NIOROSHIDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_JISSEKI_NIOROSHI data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_TEIKI_JISSEKI_NIOROSHI data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_TEIKI_JISSEKI_NIOROSHI data);

        /// <summary>
        /// システムIDと枝番をもとに定期実績荷卸のデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku.Sql.GetNioroshiData.sql")]
        new DataTable GetNioroshiData(DTOClass data);
    }

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
        [NoPersistentProps("KYOTEN_CD", "TEIKI_HAISHA_NUMBER", "DENPYOU_DATE",
            "SAGYOU_DATE", "SAGYOU_BEGIN_HOUR", "SAGYOU_BEGIN_MINUTE", "SAGYOU_END_HOUR", "SAGYOU_END_MINUTE",
            "COURSE_NAME_CD", "SHARYOU_CD", "SHASHU_CD", "UNTENSHA_CD", "HOJOIN_CD",
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
        [SqlFile("Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku.Sql.GetHaisyaEntryData.sql")]
        new DataTable GetHaisyaEntryData(DTOClass data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }

    [Bean(typeof(T_TEIKI_HAISHA_DETAIL))]
    public interface IT_TEIKI_HAISHA_DETAILDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_HAISHA_DETAIL data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_TEIKI_HAISHA_DETAIL data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_TEIKI_HAISHA_DETAIL data);

        /// <summary>
        /// システムIDと枝番をもとに定期配車明細のデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku.Sql.GetHaisyaDetailData.sql")]
        new DataTable GetHaisyaDetailData(DTOClass data);
    }

    [Bean(typeof(T_TEIKI_HAISHA_NIOROSHI))]
    public interface IT_TEIKI_HAISHA_NIOROSHIDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_HAISHA_NIOROSHI data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_TEIKI_HAISHA_NIOROSHI data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_TEIKI_HAISHA_NIOROSHI data);

        /// <summary>
        /// システムIDと枝番をもとに定期配車荷降のデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku.Sql.GetHaisyaNioroshiData.sql")]
        new DataTable GetHaisyaNioroshiData(DTOClass data);
    }

    [Bean(typeof(M_COURSE))]
    public interface IM_COURSEDao : IS2Dao
    {
        /// <summary>
        /// 曜日CD、コース名称CDをもとにコースデータを取得します
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku.Sql.GetCourseData.sql")]
        new DataTable GetCourseData(DTOClass data);

        /// <summary>
        /// 曜日CD、コース名称CDをもとにコース_荷降先データを取得します
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku.Sql.GetCourseNioroshiData.sql")]
        new DataTable GetCourseNioroshiData(DTOClass data);

        /// <summary>
        /// 曜日CD、コース名称CDをもとにコース_明細データを取得します
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku.Sql.GetCourseDetailData.sql")]
        new DataTable GetCourseDetailData(DTOClass data);
    }

    /// <summary>
    /// コース詳細Daoインタフェース
    /// </summary>
    [Bean(typeof(M_COURSE_DETAIL))]
    public interface IM_COURSE_DETAILDao : IS2Dao
    {
        /// <summary>
        /// ポップアップ表示用のコース名称リストを取得します
        /// </summary>
        /// <param name="dto">条件Dto</param>
        /// <returns>コース名称リストデータテーブル</returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku.Sql.GetCourseNameListForPopUp.sql")]
        DataTable GetCourseNameListForPopup(CourseNameListDto dto);
    }

    [Bean(typeof(M_GENBA))]
    public interface IM_CUSTOM_GENBADao : IS2Dao
    {
        List<M_GENBA> GetGenba(M_GENBA entity);

        /// <summary>
        /// 有効な業者CD、現場CDから関連データを取得する
        /// </summary>
        /// <param name="sharyouCD"></param>
        /// <param name="shashuCD"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku.Sql.GetCheckGenbaAllValidData.sql")]
        M_GENBA[] GetCheckGenbaAllValidData(string query);
    }
}
