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

namespace Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup
{
    #region - Interface -

    #region - IMLPDao -

    /// <summary>マスターリストパターンDaoに関するインターフェイス</summary>
    [Bean(typeof(M_LIST_PATTERN))]
    public interface IMLPDao : IS2Dao
    {
        #region - Methods -

        /// <summary>Insert</summary>
        /// <param name="data">マスターリストパターンオブジェクト</param>
        /// <returns>？？？</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_LIST_PATTERN data);

        /// <summary>Update</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(M_LIST_PATTERN data);

        /// <summary>Delete</summary>
        /// <param name="data">マスターリストパターンオブジェクト</param>
        /// <returns>？？？</returns>
        int Delete(M_LIST_PATTERN data);

        /// <summary>使用しない</summary>
        /// <param name="whereSql">ＳＱＬ条件</param>
        /// <returns>データーテーブル</returns>
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>Entityで絞り込んで値を取得する</summary>
        /// <param name="data">マスターリストパターンオブジェクト</param>
        /// <returns>データーテーブル</returns>
        //[SqlFile("Shougun.Core.Billing.SeikyuushoHakkou.Sql.GetSeikyuuDenpyodataSql.sql")]
        new DataTable GetDataForEntity(M_LIST_PATTERN data);

        /// <summary>使用しない</summary>
        /// <param name="data">スーパーエンティティ</param>
        /// <returns>データーテーブル</returns>
        DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>SQL構文からデータの取得を行う</summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>データーテーブル</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>Entityで絞り込んでレコード数を取得する</summary>
        /// <returns>レコード数</returns>
        [SqlFile("Shougun.Core.Common.MeisaihyoSyukeihyoPatternSentakuPopup.Sql.MListPatternCount.sql")]
        int GetMListPatternCount();

        /// <summary>Entityで絞り込んで値を取得する</summary>
        /// <param name="windowID">画面ＩＤ</param>
        /// <returns>データーテーブル</returns>
        [SqlFile("Shougun.Core.Common.MeisaihyoSyukeihyoPatternSentakuPopup.Sql.MListPatternData.sql")]
        DataTable GetMListPatternData(int windowID);

        #endregion - Methods -
    }

    #endregion - IMLPDao -

    #region - IMLPCDao -

    /// <summary>マスターリストパターンカラムDaoに関するインターフェイス</summary>
    [Bean(typeof(M_LIST_PATTERN_COLUMN))]
    public interface IMLPCDao : IS2Dao
    {
        /// <summary>Insert</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_LIST_PATTERN_COLUMN data);

        /// <summary>Update</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_LIST_PATTERN_COLUMN data);

        /// <summary>Delete</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(M_LIST_PATTERN_COLUMN data);

        /// <summary>使用しない</summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>Entityで絞り込んで値を取得する</summary>
        /// <param name="date"></param>
        /// <returns></returns>
        //[SqlFile("Shougun.Core.Billing.SeikyuushoHakkou.Sql.GetSeikyuuDenpyodataSql.sql")]
        new DataTable GetDataForEntity(M_LIST_PATTERN_COLUMN date);

        /// <summary>使用しない</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>SQL構文からデータの取得を行う</summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        new DataTable GetDateForStringSql(string sql);

        /////// <summary>Entityで絞り込んでレコード数を取得する</summary>
        /////// <param name="data"></param>
        /////// <returns>レコード数</returns>
        ////[SqlFile("Shougun.Core.Common.MeisaihyoSyukeihyoPatternSentakuPopup.Sql.MListPatternCount.sql")]
        ////int GetMListPatternCount();

        /////// <summary>Entityで絞り込んで値を取得する</summary>
        /////// <param name="data"></param>
        /////// <returns>データーテーブル</returns>
        ////[SqlFile("Shougun.Core.Common.MeisaihyoSyukeihyoPatternSentakuPopup.Sql.MListPatternData.sql")]
        ////DataTable GetMListPatternData(int windowID);
    }

    #endregion - IMLPCDao -

    #region - IMLPFCDao -

    /// <summary>マスターリストパターンフィルコンドDaoに関するインターフェイス</summary>
    [Bean(typeof(M_LIST_PATTERN_FILL_COND))]
    public interface IMLPFCDao : IS2Dao
    {
        /// <summary>Insert</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_LIST_PATTERN_FILL_COND data);

        /// <summary>Update</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_LIST_PATTERN_FILL_COND data);

        /// <summary>Delete</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(M_LIST_PATTERN_FILL_COND data);

        /// <summary>使用しない</summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>Entityで絞り込んで値を取得する</summary>
        /// <param name="date"></param>
        /// <returns></returns>
        //[SqlFile("Shougun.Core.Billing.SeikyuushoHakkou.Sql.GetSeikyuuDenpyodataSql.sql")]
        new DataTable GetDataForEntity(M_LIST_PATTERN_FILL_COND date);

        /// <summary>使用しない</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>SQL構文からデータの取得を行う</summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        new DataTable GetDateForStringSql(string sql);

        /////// <summary>Entityで絞り込んでレコード数を取得する</summary>
        /////// <param name="data"></param>
        /////// <returns>レコード数</returns>
        ////[SqlFile("Shougun.Core.Common.MeisaihyoSyukeihyoPatternSentakuPopup.Sql.MListPatternCount.sql")]
        ////int GetMListPatternCount();

        /////// <summary>Entityで絞り込んで値を取得する</summary>
        /////// <param name="data"></param>
        /////// <returns>データーテーブル</returns>
        ////[SqlFile("Shougun.Core.Common.MeisaihyoSyukeihyoPatternSentakuPopup.Sql.MListPatternData.sql")]
        ////DataTable GetMListPatternData(int windowID);
    }

    #endregion - IMLPFCDao -

    #region - ISLCSDao -

    /// <summary>リストカラム選択Daoに関するインターフェイス</summary>
    [Bean(typeof(S_LIST_COLUMN_SELECT))]
    public interface ISLCSDao : IS2Dao
    {
        /// <summary>Insert</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(S_LIST_COLUMN_SELECT data);

        /// <summary>Update</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(S_LIST_COLUMN_SELECT data);

        /// <summary>Delete</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(S_LIST_COLUMN_SELECT data);

        /// <summary>使用しない</summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>Entityで絞り込んで値を取得する</summary>
        /// <param name="date"></param>
        /// <returns></returns>
        //[SqlFile("Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup.Sql.GetSeikyuuDenpyodataSql.sql")]
        new DataTable GetDataForEntity(S_LIST_COLUMN_SELECT date);

        /// <summary>使用しない</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>SQL構文からデータの取得を行う</summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        new DataTable GetDateForStringSql(string sql);

        /// <summary>出力選択可能項目を取得する</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        /// <param name="detailKbn">詳細区分（０：伝票用、１：明細用）</param>
        /// <returns>データーテーブル</returns>
        [SqlFile("Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup.Sql.GetOutputSelectEnableItems.sql")]
        DataTable OutputSelectEnableItems(int windowID, int detailKbn);
    }

    #endregion - ISLCSDao -

    #region - ITUkeireEntryDao -

    /// <summary>受入エントリーDaoに関するインターフェイス</summary>
    [Bean(typeof(T_UKEIRE_ENTRY))]
    public interface ITUkeireEntryDao : IS2Dao
    {
        /// <summary>Insert</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKEIRE_ENTRY data);

        /// <summary>Update</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UKEIRE_ENTRY data);

        /// <summary>Delete</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_UKEIRE_ENTRY data);

        /// <summary>使用しない</summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>Entityで絞り込んで値を取得する</summary>
        /// <param name="date"></param>
        /// <returns></returns>
        //[SqlFile("Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup.Sql.GetSeikyuuDenpyodataSql.sql")]
        new DataTable GetDataForEntity(T_UKEIRE_ENTRY date);

        /// <summary>使用しない</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>SQL構文からデータの取得を行う</summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }

    #endregion - ITUkeireEntryDao -

    #region - ITShukkaEntryDao -

    /// <summary>出荷エントリーDaoに関するインターフェイス</summary>
    [Bean(typeof(T_SHUKKA_ENTRY))]
    public interface ITShukkaEntryDao : IS2Dao
    {
        /// <summary>Insert</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SHUKKA_ENTRY data);

        /// <summary>Update</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SHUKKA_ENTRY data);

        /// <summary>Delete</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_SHUKKA_ENTRY data);

        /// <summary>使用しない</summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>Entityで絞り込んで値を取得する</summary>
        /// <param name="date"></param>
        /// <returns></returns>
        //[SqlFile("Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup.Sql.GetSeikyuuDenpyodataSql.sql")]
        new DataTable GetDataForEntity(T_SHUKKA_ENTRY date);

        /// <summary>使用しない</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>SQL構文からデータの取得を行う</summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }

    #endregion - ITShukkaEntryDao -

    #region - ITUriageShiharaiEntryDao -

    /// <summary>売上支払エントリーDaoに関するインターフェイス</summary>
    [Bean(typeof(T_UR_SH_ENTRY))]
    public interface ITUriageShiharaiEntryDao : IS2Dao
    {
        /// <summary>Insert</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UR_SH_ENTRY data);

        /// <summary>Update</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UR_SH_ENTRY data);

        /// <summary>Delete</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_UR_SH_ENTRY data);

        /// <summary>使用しない</summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>Entityで絞り込んで値を取得する</summary>
        /// <param name="date"></param>
        /// <returns></returns>
        //[SqlFile("Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup.Sql.GetSeikyuuDenpyodataSql.sql")]
        new DataTable GetDataForEntity(T_UR_SH_ENTRY date);

        /// <summary>使用しない</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>SQL構文からデータの取得を行う</summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }

    #endregion - ITUriageShiharaiEntryDao -

    #region - ITNyukinEntryDao -

    /// <summary>入金エントリーDaoに関するインターフェイス</summary>
    [Bean(typeof(T_NYUUKIN_ENTRY))]
    public interface ITNyukinEntryDao : IS2Dao
    {
        /// <summary>Insert</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_NYUUKIN_ENTRY data);

        /// <summary>Update</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_NYUUKIN_ENTRY data);

        /// <summary>Delete</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_NYUUKIN_ENTRY data);

        /// <summary>使用しない</summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>Entityで絞り込んで値を取得する</summary>
        /// <param name="date"></param>
        /// <returns></returns>
        //[SqlFile("Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup.Sql.GetSeikyuuDenpyodataSql.sql")]
        new DataTable GetDataForEntity(T_NYUUKIN_ENTRY date);

        /// <summary>使用しない</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>SQL構文からデータの取得を行う</summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }

    #endregion - ITNyukinEntryDao -

    #region - ITShukkinEntryDao -

    /// <summary>出金エントリーDaoに関するインターフェイス</summary>
    [Bean(typeof(T_SHUKKIN_ENTRY))]
    public interface ITShukkinEntryDao : IS2Dao
    {
        /// <summary>Insert</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SHUKKIN_ENTRY data);

        /// <summary>Update</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SHUKKIN_ENTRY data);

        /// <summary>Delete</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_SHUKKIN_ENTRY data);

        /// <summary>使用しない</summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>Entityで絞り込んで値を取得する</summary>
        /// <param name="date"></param>
        /// <returns></returns>
        //[SqlFile("Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup.Sql.GetSeikyuuDenpyodataSql.sql")]
        new DataTable GetDataForEntity(T_SHUKKIN_ENTRY date);

        /// <summary>使用しない</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>SQL構文からデータの取得を行う</summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }

    #endregion - ITShukkinEntryDao -

    #region - ITSeikyuuDenpyouDao -

    /// <summary>請求伝票Daoに関するインターフェイス</summary>
    [Bean(typeof(T_SEIKYUU_DENPYOU))]
    public interface ITSeikyuuDenpyouDao : IS2Dao
    {
        /// <summary>Insert</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SEIKYUU_DENPYOU data);

        /// <summary>Update</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SEIKYUU_DENPYOU data);

        /// <summary>Delete</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_SEIKYUU_DENPYOU data);

        /// <summary>使用しない</summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>Entityで絞り込んで値を取得する</summary>
        /// <param name="date"></param>
        /// <returns></returns>
        //[SqlFile("Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup.Sql.GetSeikyuuDenpyodataSql.sql")]
        new DataTable GetDataForEntity(T_SEIKYUU_DENPYOU date);

        /// <summary>使用しない</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>SQL構文からデータの取得を行う</summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }

    #endregion - ITSeikyuuDenpyouDao -

    #region - ITSeisanDenpyouDao -

    /// <summary>精算伝票Daoに関するインターフェイス</summary>
    [Bean(typeof(T_SEISAN_DENPYOU))]
    public interface ITSeisanDenpyouDao : IS2Dao
    {
        /// <summary>Insert</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SEISAN_DENPYOU data);

        /// <summary>Update</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SEISAN_DENPYOU data);

        /// <summary>Delete</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_SEISAN_DENPYOU data);

        /// <summary>使用しない</summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>Entityで絞り込んで値を取得する</summary>
        /// <param name="date"></param>
        /// <returns></returns>
        //[SqlFile("Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup.Sql.GetSeikyuuDenpyodataSql.sql")]
        new DataTable GetDataForEntity(T_SEISAN_DENPYOU date);

        /// <summary>使用しない</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>SQL構文からデータの取得を行う</summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }

    #endregion - ITSeisanDenpyouDao -

    #region - IMTorihikisakiDao -

    /// <summary>取引先Daoに関するインターフェイス</summary>
    [Bean(typeof(M_TORIHIKISAKI))]
    public interface IMTorihikisakiDao : IS2Dao
    {
        /// <summary>Insert</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SEISAN_DENPYOU data);

        /// <summary>Update</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SEISAN_DENPYOU data);

        /// <summary>Delete</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_SEISAN_DENPYOU data);

        /// <summary>使用しない</summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>Entityで絞り込んで値を取得する</summary>
        /// <param name="date"></param>
        /// <returns></returns>
        //[SqlFile("Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup.Sql.GetSeikyuuDenpyodataSql.sql")]
        new DataTable GetDataForEntity(T_SEISAN_DENPYOU date);

        /// <summary>使用しない</summary>
        /// <param name="data"></param>
        /// <returns></returns>
        DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>SQL構文からデータの取得を行う</summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }

    #endregion - IMTorihikisakiDao -

    #endregion - Interface -
}
