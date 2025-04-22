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
using Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.DTO;
using Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.Entity;
using System.Data.SqlTypes;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.DAO
{
    /// <summary>
    /// 実績売上支払確定画面用 取引先情報取得DAO
    /// </summary>
    [Bean(typeof(T_SELECT_RESULT))]
    internal interface GetTorihikisakiDAOClass : IS2Dao
    {
        /// <summary>
        /// 実績売上支払確定対象の取引先情報を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.Sql.GetTorihikisakiData.sql")]
        List<T_SELECT_RESULT> GetTorihikisakiDataForEntity(SearchDTOClass data);

        /// <summary>
        /// 実績売上支払確定対象外(伝票区分：売上、支払以外)の取引先情報を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.Sql.GetTeikiJissekiDetailData.sql")]
        List<T_SELECT_RESULT> GetTeikiJissekiDetailDataForEntity(SearchDTOClass data);

        /// <summary>
        /// 売上支払情報を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.Sql.GetUrShData.sql")]
        DataTable GetUrShData(SearchDTOClass data);
    }

    /// <summary>
    /// 売上_支払入力テーブルを取得
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    [Bean(typeof(T_UR_SH_ENTRY))]
    public interface GetUrShDataDao : IS2Dao
    {
        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("UR_SH_NUMBER = /*number*/ AND DELETE_FLG = 0")]
        T_UR_SH_ENTRY GetDataByNumber(string number);
    }

    /// <summary>
    /// 売上_支払入力テーブルを登録、更新する
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    [Bean(typeof(T_UR_SH_ENTRY))]
    public interface SetUrShEntryDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UR_SH_ENTRY data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_UR_SH_ENTRY data);
    }

    /// <summary>
    /// 売上_支払入力テーブルを登録する
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    [Bean(typeof(T_UR_SH_DETAIL))]
    public interface SetUrShDetailDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UR_SH_DETAIL data);
    }

    /// <summary>
    /// 定期実績入力テーブルを更新する
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    [Bean(typeof(T_TEIKI_JISSEKI_ENTRY))]
    public interface SetTeikeiJissekiEntryDao : IS2Dao
    {
        /// <summary>
        /// Update ("UPDATE_USER", "UPDATE_DATE", "UPDATE_PC"の更新)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("KYOTEN_CD", "TEIKI_JISSEKI_NUMBER", "WEATHER", "FURIKAE_HAISHA_KBN", "DENPYOU_DATE",
            "SEARCH_DENPYOU_DATE", "SAGYOU_DATE", "SEARCH_SAGYOU_DATE", "COURSE_NAME_CD", "SHARYOU_CD",
            "SHASHU_CD", "UNTENSHA_CD", "HOJOIN_CD", "SHUKKO_METER", "SHUKKO_HOUR", "SHUKKO_MINUTE",
            "KIKO_METER", "KIKO_HOUR", "KIKO_MINUTE", "TEIKI_HAISHA_NUMBER", "MOBILE_SHOGUN_FILE_NAME",
            "CREATE_USER", "CREATE_DATE", "SEARCH_CREATE_DATE", "CREATE_PC", "SEARCH_UPDATE_DATE", "TIME_STAMP", "UNPAN_GYOUSHA_CD")]
        int Update(T_TEIKI_JISSEKI_ENTRY data);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_JISSEKI_ENTRY data);

        /// <summary>
        /// T_TEIKI_JISSEKI_ENTRYを検索
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM T_TEIKI_JISSEKI_ENTRY WHERE SYSTEM_ID = /*systemid*/ AND SEQ = /*seq*/ AND DELETE_FLG = 0")]
        T_TEIKI_JISSEKI_ENTRY GetTeikiJissekiEntryData(SqlInt64 systemid, SqlInt64 seq);

        [Query("SYSTEM_ID = /*systemId*/ AND SEQ = /*seq*/")]
        T_TEIKI_JISSEKI_ENTRY GetDataByKey(SqlInt64 systemId, SqlInt64 seq);
    }

    /// <summary>
    /// 定期実績明細テーブルを更新する
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    [Bean(typeof(T_TEIKI_JISSEKI_DETAIL))]
    public interface SetTeikeiJissekiDetailDao : IS2Dao
    {
        /// <summary>
        /// Update ("UR_SH_NUMBER"の更新)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TEIKI_JISSEKI_NUMBER", "ROW_NUMBER", "INPUT_KBN", "GYOUSHA_CD", "GENBA_CD", "HINMEI_CD",
            "SUURYOU","UNIT_CD","ANBUN_SUURYOU","NIOROSHI_NUMBER","TSUKIGIME_KBN","TIME_STAMP",
            "SHUUSHUU_TIME","KAISHUU_BIKOU","HINMEI_BIKOU","KANSAN_SUURYOU","KANSAN_UNIT_CD",
            "DENPYOU_KBN_CD", "KEIYAKU_KBN", "KANSAN_UNIT_MOBILE_OUTPUT_FLG", "ANBUN_FLG")]
        int Update(T_TEIKI_JISSEKI_DETAIL data);

        [Query("SYSTEM_ID = /*systemId*/ AND SEQ = /*seq*/ AND DETAIL_SYSTEM_ID = /*detailSystemId*/")]
        T_TEIKI_JISSEKI_DETAIL GetDataByKey(SqlInt64 systemId, SqlInt64 seq, SqlInt64 detailSystemId);
    }

    /// <summary>
    /// 実績売上支払確定画面用 取引先情報取得DAO
    /// </summary>
    [Bean(typeof(SHIME_DATA))]
    internal interface GetShimeDataDao : IS2Dao
    {
        /// <summary>
        /// 売上支払番号で請求明細を検索
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM T_SEIKYUU_DETAIL WHERE DENPYOU_SHURUI_CD = 3 AND DENPYOU_NUMBER IN /*data*/('aaa','bbb') AND DELETE_FLG = 0")]
        List<SHIME_DATA> GetSeikyuuData(long[] data);

        /// <summary>
        /// 売上支払番号で精算明細を検索
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM T_SEISAN_DETAIL WHERE DENPYOU_SHURUI_CD = 3 AND DENPYOU_NUMBER IN /*data*/('aaa','bbb') AND DELETE_FLG = 0")]
        List<SHIME_DATA> GetSeisanData(long[] data);
    }

    /// <summary>
    /// 取引先_請求締日マスタDAO
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI_SEIKYUU))]
    public interface GetSeikyuuShimebiDao : IS2Dao
    {
        /// <summary>
        /// 取引先CDで取引先請求エンティティを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="shimebi">締日</param>
        /// <returns>取引先請求エンティティ</returns>
        [Sql("SELECT MT.TORIHIKISAKI_CD,MTS.SHIMEBI1 FROM M_TORIHIKISAKI AS MT JOIN M_TORIHIKISAKI_SEIKYUU AS MTS ON MT.TORIHIKISAKI_CD = MTS.TORIHIKISAKI_CD WHERE MT.TORIHIKISAKI_CD = /*torihikisakiCd*/'000001' AND MTS.SHIMEBI1 = /*shimebi*/10")]
        M_TORIHIKISAKI_SEIKYUU GetTorihikisakiSeikyuuByTorihikisakiCdAndShimebi1(string torihikisakiCd, string shimebi);
    }

    /// <summary>
    /// 取引先_支払締日マスタDAO
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI_SHIHARAI))]
    public interface GetShiharaiShimebiDao : IS2Dao
    {
        /// <summary>
        /// 取引先CDで取引先支払エンティティを取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="shimebi">締日</param>
        /// <returns>取引先支払エンティティ</returns>
        [Sql("SELECT MT.TORIHIKISAKI_CD,MTS.SHIMEBI1 FROM M_TORIHIKISAKI AS MT JOIN M_TORIHIKISAKI_SHIHARAI AS MTS ON MT.TORIHIKISAKI_CD = MTS.TORIHIKISAKI_CD WHERE MT.TORIHIKISAKI_CD = /*torihikisakiCd*/'000001' AND MTS.SHIMEBI1 = /*shimebi*/10")]
        M_TORIHIKISAKI_SHIHARAI GetTorihikisakiShiharaiByTorihikisakiCdAndShimebi1(string torihikisakiCd, string shimebi);
    }
}
