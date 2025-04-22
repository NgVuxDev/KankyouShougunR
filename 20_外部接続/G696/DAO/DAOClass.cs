using System;
using System.Collections.Generic;
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ExternalConnection.HaisouKeikakuIchiran
{
    /// <summary>
    /// 配送計画DAO
    /// </summary>
    [Bean(typeof(T_LOGI_DELIVERY))]
    public interface LogiDeliveryDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_LOGI_DELIVERY data);

        /// <summary>
        /// Entityを元に更新処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_LOGI_DELIVERY data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_LOGI_DELIVERY data);

        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/")]
        T_LOGI_DELIVERY GetDataByCd(long systemId);

        /// <summary>
        /// 配送開始日と配送No（ロジコンパス側のPK）をもとにデータを取得する
        /// </summary>
        /// <param name="deliveryDate">配送開始日</param>
        /// <param name="deliveryNo">配送No</param>
        /// <returns>取得したデータ</returns>
        [Query("DELIVERY_DATE = /*deliveryDate*/ and DELIVERY_NO = /*deliveryNo*/")]
        T_LOGI_DELIVERY GetDataByLogiKey(DateTime deliveryDate, int deliveryNo);

        [Sql("/*$sql*/")]
        DataTable getdateforstringsql(string sql);
    }

    /// <summary>
    /// 配送計画明細DAO
    /// </summary>
    [Bean(typeof(T_LOGI_DELIVERY_DETAIL))]
    public interface LogiDeliveryDetailDAO : IS2Dao
    {
        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/")]
        List<T_LOGI_DELIVERY_DETAIL> GetDataBySystemId(long systemId);
    }

    /// <summary>
    /// 配送計画連携状況管理DAO
    /// </summary>
    [Bean(typeof(T_LOGI_LINK_STATUS))]
    public interface LogiLinkStatusDAO : IS2Dao
    {
        /// <summary>
        /// システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/")]
        T_LOGI_LINK_STATUS GetDataByCd(long systemId);

        /// <summary>
        /// Entityを元に更新処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_LOGI_LINK_STATUS data);
    }

    /// <summary>
    /// 配送計画to定期実績DAO
    /// </summary>
    [Bean(typeof(T_LOGI_TO_TEIKI))]
    public interface LogiToTeikiDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_LOGI_TO_TEIKI data);
    }

    /// <summary>
    /// 配送計画to売上支払DAO
    /// </summary>
    [Bean(typeof(T_LOGI_TO_URSH))]
    public interface LogiToUrshDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_LOGI_TO_URSH data);
    }

    /// <summary>
    /// 受付（収集）入力DAO
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SS_ENTRY))]
    public interface UketsukeSsEntryDAO : IS2Dao
    {
        /// <summary>
        /// 収集受付入力エンティティを追加します
        /// </summary>
        /// <param name="tUketsukeSsEntry">追加するエンティティ</param>
        /// <returns>追加した件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SS_ENTRY tUketsukeSsEntry);

        /// <summary>
        /// 収集受付入力エンティティを更新します
        /// </summary>
        /// <param name="tUketsukeSsEntry">更新するエンティティ</param>
        /// <returns>更新した件数</returns>
        [Sql("UPDATE T_UKETSUKE_SS_ENTRY SET DELETE_FLG = 1 WHERE SYSTEM_ID = /*tUketsukeSsEntry.SYSTEM_ID*/ AND SEQ = /*tUketsukeSsEntry.SEQ*/ ")]
        int Update(T_UKETSUKE_SS_ENTRY tUketsukeSsEntry);

        /// <summary>
        /// システムIDをもとに最新データを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ and DELETE_FLG = 0")]
        T_UKETSUKE_SS_ENTRY GetDataBySystemId(long systemId);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">画面．受付番号</param>
        /// <returns></returns>
        [Sql("SELECT * FROM T_UKETSUKE_SS_ENTRY WHERE UKETSUKE_NUMBER = /*uketsukeNum.UKETSUKE_NUMBER*/ AND DELETE_FLG = 0 ")]
        T_UKETSUKE_SS_ENTRY GetDataForEntity(T_UKETSUKE_SS_ENTRY uketsukeNum);
    }

    /// <summary>
    /// 受付（収集）明細DAO
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SS_DETAIL))]
    public interface UketsukeSsDetailDAO : IS2Dao
    {
        /// <summary>
        /// 収集受付詳細エンティティを追加します
        /// </summary>
        /// <param name="tUketsukeSsDetail">追加するエンティティ</param>
        /// <returns>追加した件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SS_DETAIL tUketsukeSsDetail);

        /// <summary>
        /// システムIDをもとに最新の明細データを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.ExternalConnection.HaisouKeikakuIchiran.Sql.GetUketsukeSsDetail.sql")]
        T_UKETSUKE_SS_DETAIL GetDataBySystemId(long systemId, long detailSystemId);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">画面．受付番号</param>
        /// <returns></returns>
        [Sql("SELECT * FROM T_UKETSUKE_SS_DETAIL WHERE SYSTEM_ID = /*uketsukeNum.SYSTEM_ID*/ AND SEQ = /*uketsukeNum.SEQ*/ ")]
        T_UKETSUKE_SS_DETAIL[] GetDataForEntity(T_UKETSUKE_SS_DETAIL uketsukeNum);
    }

    /// <summary>
    /// 受付（出荷）入力DAO
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SK_ENTRY))]
    public interface UketsukeSkEntryDAO : IS2Dao
    {
        /// <summary>
        /// 出荷受付入力エンティティを追加します
        /// </summary>
        /// <param name="tUketsukeSsEntry">追加するエンティティ</param>
        /// <returns>追加した件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SK_ENTRY tUketsukeSkEntry);

        /// <summary>
        /// 出荷受付入力エンティティを更新します
        /// </summary>
        /// <param name="tUketsukeSsEntry">更新するエンティティ</param>
        /// <returns>更新した件数</returns>
        [Sql("UPDATE T_UKETSUKE_SK_ENTRY SET DELETE_FLG = 1 WHERE SYSTEM_ID = /*tUketsukeSkEntry.SYSTEM_ID*/ AND SEQ = /*tUketsukeSkEntry.SEQ*/ ")]
        int Update(T_UKETSUKE_SK_ENTRY tUketsukeSkEntry);

        /// <summary>
        /// システムIDをもとに最新データを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ and DELETE_FLG = 0")]
        T_UKETSUKE_SK_ENTRY GetDataBySystemId(long systemId);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">画面．受付番号</param>
        /// <returns></returns>
        [Sql("SELECT * FROM T_UKETSUKE_SK_ENTRY WHERE UKETSUKE_NUMBER = /*uketsukeNum.UKETSUKE_NUMBER*/ AND DELETE_FLG = 0 ")]
        T_UKETSUKE_SK_ENTRY GetDataForEntity(T_UKETSUKE_SK_ENTRY uketsukeNum);
    }

    /// <summary>
    /// 受付（出荷）明細DAO
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SK_DETAIL))]
    public interface UketsukeSkDetailDAO : IS2Dao
    {
        /// <summary>
        /// 出荷受付詳細エンティティを追加します
        /// </summary>
        /// <param name="tUketsukeSsDetail">追加するエンティティ</param>
        /// <returns>追加した件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SK_DETAIL tUketsukeSkDetail);

        /// <summary>
        /// システムIDをもとに最新の明細データを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.ExternalConnection.HaisouKeikakuIchiran.Sql.GetUketsukeSkDetail.sql")]
        T_UKETSUKE_SK_DETAIL GetDataBySystemId(long systemId, long detailSystemId);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">画面．受付番号</param>
        /// <returns></returns>
        [Sql("SELECT * FROM T_UKETSUKE_SK_DETAIL WHERE SYSTEM_ID = /*uketsukeNum.SYSTEM_ID*/ AND SEQ = /*uketsukeNum.SEQ*/ ")]
        T_UKETSUKE_SK_DETAIL[] GetDataForEntity(T_UKETSUKE_SK_DETAIL uketsukeNum);
    }

    /// <summary>
    /// 売上／支払入力DAO
    /// </summary>
    [Bean(typeof(T_UR_SH_ENTRY))]
    public interface UrShEntryDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UR_SH_ENTRY data);
    }

    /// <summary>
    /// 売上／支払明細DAO
    /// </summary>
    [Bean(typeof(T_UR_SH_DETAIL))]
    public interface UrShDetailDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UR_SH_DETAIL data);
    }

    /// <summary>
    /// 定期配車入力DAO
    /// </summary>
    [Bean(typeof(T_TEIKI_HAISHA_ENTRY))]
    public interface TeikiHaishaEntryDAO : IS2Dao
    {
        /// <summary>
        /// システムIDをもとに最新データを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ and DELETE_FLG = 0")]
        T_TEIKI_HAISHA_ENTRY GetDataBySystemId(long systemId);

        /// <summary>
        /// 検索条件に紐付く、品名詳細を含む定期配車情報を取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.ExternalConnection.HaisouKeikakuIchiran.Sql.GetTeikiHinmeiInfo.sql")]
        DataTable GetTeikiHinmeiInfo(SearchTeikiTorikomiDTO data);

        /// <summary>
        /// 現場_定期品名マスタを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.HaisouKeikakuIchiran.Sql.GetGenbaTeikiHinmeiData.sql")]
        DataTable GetGenbaTeikiHinmeiDataForEntity(SearchTeikiTorikomiDTO data);

        /// <summary>品名マスタを取得する</summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.HaisouKeikakuIchiran.Sql.GetHinmeiData.sql")]
        DataTable GetHinmeiDataForEntity(SearchTeikiTorikomiDTO data);

        /// <summary>
        /// 定期配車番号に紐付く配車詳細伝票の換算値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.ExternalConnection.HaisouKeikakuIchiran.Sql.GetKansanData.sql")]
        DataTable GetKansanData(SearchTeikiTorikomiDTO data);
    }

    /// <summary>
    /// 定期配車明細DAO
    /// </summary>
    [Bean(typeof(T_TEIKI_HAISHA_DETAIL))]
    public interface TeikiHaishaDetailDAO : IS2Dao
    {
        /// <summary>
        /// システムID,SEQをもとに最新データを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq"></param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ and SEQ = /*seq*/")]
        List<T_TEIKI_HAISHA_DETAIL> GetDataBySystemIdSeq(long systemId, int seq);

        /// <summary>
        /// PKをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq"></param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ and SEQ = /*seq*/ and DETAIL_SYSTEM_ID = /*detailSystemId*/")]
        T_TEIKI_HAISHA_DETAIL GetDataByPK(long systemId, int seq, long detailSystemId);
    }

    /// <summary>
    /// 定期配車荷降DAO
    /// </summary>
    [Bean(typeof(T_TEIKI_HAISHA_NIOROSHI))]
    public interface TeikiHaishaNioroshiDAO : IS2Dao
    {
        /// <summary>
        /// システムID,SEQをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq"></param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ and SEQ = /*seq*/")]
        List<T_TEIKI_HAISHA_NIOROSHI> GetDataBySystemIdSeq(long systemId, int seq);
    }

    /// <summary>
    /// 定期配車詳細DAO
    /// </summary>
    [Bean(typeof(T_TEIKI_HAISHA_SHOUSAI))]
    public interface TeikiHaishaShousaiDAO : IS2Dao
    {
        /// <summary>
        /// システムID,SEQ,明細システムIDをもとにデータを取得する
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <param name="seq"></param>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/ and SEQ = /*seq*/ and DETAIL_SYSTEM_ID= /*detailSystemId*/")]
        List<T_TEIKI_HAISHA_SHOUSAI> GetDataByDetailSystemIdEtc(long systemId, int seq, long detailSystemId);
    }

    /// <summary>
    /// 定期実績入力DAO
    /// </summary>
    [Bean(typeof(T_TEIKI_JISSEKI_ENTRY))]
    public interface TeikiJissekiEntryDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_JISSEKI_ENTRY data);
    }

    /// <summary>
    /// 定期実績明細DAO
    /// </summary>
    [Bean(typeof(T_TEIKI_JISSEKI_DETAIL))]
    public interface TeikiJissekiDetailDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_JISSEKI_DETAIL data);
    }

    /// <summary>
    /// 定期実績荷降DAO
    /// </summary>
    [Bean(typeof(T_TEIKI_JISSEKI_NIOROSHI))]
    public interface TeikiJissekiNioroshiDAO : IS2Dao
    {
        /// <summary>
        /// Entityを元に追加処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_JISSEKI_NIOROSHI data);
    }

    /// <summary>
    /// コンテナ情報DAO
    /// </summary>
    [Bean(typeof(T_CONTENA_RESULT))]
    internal interface ContenaResultDAO : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="sysId">受入入力．システムID</param>
        /// <returns></returns>
        //[SqlFile("Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Sql.GetContena.sql")]
        //T_CONTENA_RESULT[] GetContena(string sysId);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_CONTENA_RESULT data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_CONTENA_RESULT data);
    }

    /// <summary>
    /// コンテナ稼動予定情報DAO
    /// </summary>
    [Bean(typeof(T_CONTENA_RESERVE))]
    internal interface ContenaReserveDAO : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="sysId">受入入力．システムID</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.HaisouKeikakuIchiran.Sql.GetContenaReserveData.sql")]
        T_CONTENA_RESERVE[] GetContenaReserve(string sysId, string SEQ);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_CONTENA_RESERVE data);
    }
}
