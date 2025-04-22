// $Id: DAOClass.cs 9694 2013-12-05 05:27:09Z sys_dev_22 $
using r_framework.Dao;
using Seasar.Dao.Attrs;
using System.Data;
using System.Data.SqlTypes;
using r_framework.Entity;
using System.Collections.Generic;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Allocation.MobileJoukyouIchiran
{
    [Bean(typeof(DTOClass))]
    public interface ITeikihaishaDao : IS2Dao
    {
        /// <summary>
        /// 登録済定期配車データを取得(読込系)
        /// </summary>
        /// <param name="data">DTOClass</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetDetailForTourokuSumiHaisha.sql")]
        DataTable GetDetailForTourokuSumiHaisha(DTOClass data);

        /// <summary>
        /// 登録済定期配車データを取得(読込系) 詳細表示
        /// </summary>
        /// <param name="data">DTOClass</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetDetailForTourokuSumiHaishaDetail.sql")]
        DataTable GetDetailForTourokuSumiHaishaDetail(DTOClass data);

        /// <summary>
        /// 登録済収集受付データを取得(読込系)
        /// </summary>
        /// <param name="data">DTOClass</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetDetailForTourokuSumiUketuke.sql")]
        DataTable GetDetailForTourokuSumiUketuke(DTOClass data);

        /// <summary>
        /// 登録済収集受付データを取得(読込系) 詳細表示
        /// </summary>
        /// <param name="data">DTOClass</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetDetailForTourokuSumiUketukeDetail.sql")]
        DataTable GetDetailForTourokuSumiUketukeDetail(DTOClass data);

        /// <summary>
        /// 未登録定期配車データを取得(登録系)
        /// </summary>
        /// <param name="data">DTOClass</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetDetailForMiTourokuHaisha.sql")]
        DataTable GetDetailForMiTourokuHaisha(DTOClass data);

        /// <summary>
        /// 未登録収集受付データを取得(登録系)
        /// </summary>
        /// <param name="data">DTOClass</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetDetailForMiTourokuUketuke.sql")]
        DataTable GetDetailForMiTourokuUketuke(DTOClass data);

        /// <summary>
        /// 定期配車荷降データを取得
        /// </summary>
        /// <param name="SYSTEM_ID">SYSTEM_ID</param>
        /// <param name="SEQ">SEQ</param>
        /// <param name="NIOROSHI_NUMBER">NIOROSHI_NUMBER</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetTeikiHaishaNioroshiData.sql")]
        DataTable GetTeikiHaishaNioroshiData(SqlInt64 SYSTEM_ID, SqlInt32 SEQ, SqlInt32 NIOROSHI_NUMBER);

        /// <summary>
        /// 収集受付データを取得
        /// </summary>
        /// <param name="SYSTEM_ID">SYSTEM_ID</param>
        /// <param name="SEQ">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetSyuusyuuUketukeDetailData.sql")]
        DataTable GetSyuusyuuUketukeDetailData(SqlInt64 SYSTEM_ID, SqlInt32 SEQ);

        /// <summary>
        /// 出荷データを取得
        /// </summary>
        /// <param name="SYSTEM_ID">SYSTEM_ID</param>
        /// <param name="SEQ">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetShukkaUketukeDetailData.sql")]
        DataTable GetShukkaUketukeDetailData(SqlInt64 SYSTEM_ID, SqlInt32 SEQ);

        /// <summary>
        /// 収集受付コンテナデータを取得
        /// </summary>
        /// <param name="SYSTEM_ID">SYSTEM_ID</param>
        /// <param name="SEQ">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetSyuusyuuUketukeContenaData.sql")]
        DataTable GetSyuusyuuUketukeContenaData(SqlInt64 SYSTEM_ID, SqlInt32 SEQ);

        /// <summary>
        /// 収集受付コンテナデータを取得
        /// </summary>
        /// <param name="SYSTEM_ID">SYSTEM_ID</param>
        /// <param name="SEQ">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetUrShContenaData.sql")]
        DataTable GetUrShContenaData(SqlInt64 SEQ_NO);

        /// <summary>
        /// 回収実績の件数を取得
        /// </summary>
        /// <param name="HAISHA_KBN">0定期配車：1スポット</param>
        /// <param name="HAISHA_DENPYOU_NO">HAISHA_DENPYOU_NO</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetKaisyuuJissekiCount.sql")]
        DataTable GetKaisyuuJissekiCount(SqlInt16 HAISHA_KBN, SqlInt64 HAISHA_DENPYOU_NO);

        /// <summary>
        /// 定期配車拠点取得
        /// </summary>
        /// <param name="HAISHA_DENPYOU_NO">HAISHA_DENPYOU_NO</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetTeikiHaishaKyouteiData.sql")]
        DataTable GetTeikiHaishaKyouteiData(SqlInt64 TEIKI_HAISHA_NUMBER);

        /// <summary>
        /// 収集受付拠点取得
        /// </summary>
        /// <param name="HAISHA_DENPYOU_NO">HAISHA_DENPYOU_NO</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetSyuusyuuKyouteiData.sql")]
        DataTable GetSyuusyuuKyouteiData(SqlInt64 TEIKI_HAISHA_NUMBER);

        /// <summary>
        /// 現場_定期品名マスタを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetGenbaTeikiHinmeiData.sql")]
        DataTable GetGenbaTeikiHinmeiDataForEntity(MobileShougunTorikomiDTOClass data);

        /// <summary>
        /// 定期配車番号に紐付く配車詳細伝票の換算値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetKansanData.sql")]
        DataTable GetKansanData(MobileShougunTorikomiDTOClass data);

        /// <summary>
        /// 検索条件に紐付く、品名詳細を含む定期配車情報を取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetTeikiHinmeiInfo.sql")]
        DataTable GetTeikiHinmeiInfo(MobileShougunTorikomiDTOClass data);

        /// <summary>品名マスタを取得する</summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetHinmeiData.sql")]
        DataTable GetHinmeiDataForEntity(MobileShougunTorikomiDTOClass data);

        /// <summary>登録用売上支払データを取得する</summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetUrShDataEntity.sql")]
        DataTable GetUrShDataEntity(SqlInt64 HAISHA_DENPYOU_NO, string UKETSUKE_KBN);

        /// <summary>品名マスタを取得する</summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetKobetsuHinmeiData.sql")]
        DataTable GetKobetsuHinmeiDataForEntity(MobileShougunTorikomiDTOClass data);

        /// <summary>
        /// 受付(収集)入力マスタを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetUketsukeSsEntryData.sql")]
        DataTable GetUketsukeSsEntryForEntity(UketsukeSsDTOClass data);

        /// <summary>
        /// 受付(出荷)入力マスタを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetUketsukeSkEntryData.sql")]
        DataTable GetUketsukeSkEntryForEntity(UketsukeSkDTOClass data);

        /// <summary>
        /// 受付(収集)明細マスタを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetUketsukeSsDetailData.sql")]
        DataTable GetUketsukeSsDetailForEntity(UketsukeSsDTOClass data);

        /// <summary>
        /// 受付(出荷)明細マスタを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetUketsukeSkDetailData.sql")]
        DataTable GetUketsukeSkDetailForEntity(UketsukeSkDTOClass data);

        /// <summary>
        /// 定期配車荷降行取得
        /// </summary>
        /// <param name="HAISHA_DENPYOU_NO">HAISHA_DENPYOU_NO</param>
        /// <param name="NIOROSHI_NUMBER">NIOROSHI_NUMBER</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetMobilNioroshiData.sql")]
        DataTable GetMobilNioroshiData(SqlInt64 TEIKI_HAISHA_NUMBER, int NIOROSHI_NUMBER);

        /// <summary>
        /// 搬入削除取得
        /// </summary>
        /// <param name="HAISHA_DENPYOU_NO">HAISHA_DENPYOU_NO</param>
        /// <param name="JISSEKI">実績true,予定false</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetDeleteHannyuuDataByCD.sql")]
        DataTable GetDeleteHannyuuDataByCD(SqlInt64 TEIKI_HAISHA_NUMBER, bool JISSEKI);

        /// <summary>
        /// 定期配車実績配車NO検索
        /// </summary>
        /// <param name="HAISHA_DENPYOU_NO">HAISHA_DENPYOU_NO</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetTeikiHaishaJissekiNo.sql")]
        DataTable GetTeikiHaishaJissekiNo(SqlInt64 TEIKI_HAISHA_NUMBER);

        /// <summary>
        /// 定期配車配車NO検索
        /// </summary>
        /// <param name="HAISHA_DENPYOU_NO">HAISHA_DENPYOU_NO</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetTeikiHaishaNo.sql")]
        DataTable GetTeikiHaishaNo(SqlInt64 TEIKI_HAISHA_NUMBER);

        /// <summary>
        /// 受付伝票区分検索
        /// </summary>
        /// <param name="UKETSUKE_NUMBER">UKETSUKE_NUMBER</param>
        /// <param name="HINMEI_CD">HINMEI_CD</param>
        /// <param name="DTL_EDABAN">DTL_EDABAN</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetDenpyoKbnByUktukeNo.sql")]
        DataTable GetDenpyoKbnByUktukeNo(SqlInt64 UKETSUKE_NUMBER, string HINMEI_CD, SqlInt16 DTL_EDABAN);

        /// <summary>
        /// 定期配車取引先有無検索
        /// </summary>
        /// <param name="HAISHA_DENPYOU_NO">HAISHA_DENPYOU_NO</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetTeikiHaishaTorihikisakiUmu.sql")]
        DataTable GetTeikiHaishaTorihikisakiUmu(SqlInt64 TEIKI_HAISHA_NUMBER);
        
    }

    [Bean(typeof(T_MOBISYO_RT))]
    public interface T_MOBISYO_RTDao : IS2Dao
    {
        /// <summary>
        /// モバイル将軍業務TBL削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetRtDataByCD.sql")]
        T_MOBISYO_RT[] GetRtDataByCD(SqlInt64 HAISHA_DENPYOU_NO, int HAISHA_KBN);
        
        /// <summary>
        /// モバイル将軍業務TBL削除フラグがたっていないすべてのデータを取得する（実績未登録データ）
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetRtDataByCDM.sql")]
        T_MOBISYO_RT[] GetRtDataByCDM(SqlInt64 HAISHA_DENPYOU_NO, int HAISHA_KBN);

        /// <summary>
        /// モバイル将軍業務TBL削除フラグがたっていないすべてのデータを取得する（行指定）
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetRtDataByCDR.sql")]
        T_MOBISYO_RT[] GetRtDataByCDR(SqlInt64 HAISHA_DENPYOU_NO, SqlInt64 HAISHA_ROW_NUMBER, int HAISHA_KBN);
    
    }

    [Bean(typeof(T_MOBISYO_RT_CONTENA))]
    public interface T_MOBISYO_RT_CONTENADao : IS2Dao
    {
        /// <summary>
        /// モバイル将軍業務TBL削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetRtCTNDataByCD.sql")]
        T_MOBISYO_RT_CONTENA[] GetRtCTNDataByCD(SqlInt64 SEQ_NO);
    }

    [Bean(typeof(T_MOBISYO_RT_DTL))]
    public interface T_MOBISYO_RT_DTLDao : IS2Dao
    {
        /// <summary>
        /// モバイル将軍業務詳細TBL削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetDtlDataByCD.sql")]
        T_MOBISYO_RT_DTL[] GetDtlDataByCD(SqlInt64 SEQ_NO);
    }

    [Bean(typeof(T_MOBISYO_RT_HANNYUU))]
    public interface T_MOBISYO_RT_HANNYUUDao : IS2Dao
    {
        /// <summary>
        /// モバイル将軍業務搬入TBL 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetHannyuuDataByCD.sql")]
        T_MOBISYO_RT_HANNYUU[] GetHannyuuDataByCD(SqlInt64 HANYU_JISSEKI_SEQ_NO);
    }

    /// <summary>
    /// 定期配車実績entry
    /// </summary>
    [Bean(typeof(T_TEIKI_JISSEKI_ENTRY))]
    public interface IT_TEIKI_JISSEKI_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_JISSEKI_ENTRY data);
    }

    /// <summary>
    /// 定期配車実績detail
    /// </summary>
    [Bean(typeof(T_TEIKI_JISSEKI_DETAIL))]
    public interface IT_TEIKI_JISSEKI_DETAILDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_JISSEKI_DETAIL data);
    }

    /// <summary>
    /// 定期配車実績nioroso
    /// </summary>
    [Bean(typeof(T_TEIKI_JISSEKI_NIOROSHI))]
    public interface IT_TEIKI_JISSEKI_NIOROSHIDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_JISSEKI_NIOROSHI data);
    }

    /// <summary>
    /// 売上支払entry
    /// </summary>
    [Bean(typeof(T_UR_SH_ENTRY))]
    public interface IT_UR_SH_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UR_SH_ENTRY data);
    }

    /// <summary>
    /// 売上支払detail
    /// </summary>
    [Bean(typeof(T_UR_SH_DETAIL))]
    public interface IT_UR_SH_DETAILDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UR_SH_DETAIL data);
    }

    #region 収集受付
    /// <summary>
    /// 収集受付入力DAO
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SS_ENTRY))]
    internal interface IT_UKETSUKE_SS_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// 収集受付入力エンティティを追加します
        /// </summary>
        /// <param name="tUketsukeSsEntry">追加するエンティティ</param>
        /// <returns>追加した件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SS_ENTRY tUketsukeSsEntry);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">画面．受付番号</param>
        /// <returns></returns>
        [Sql("SELECT * FROM T_UKETSUKE_SS_ENTRY WHERE UKETSUKE_NUMBER = /*uketsukeNum.UKETSUKE_NUMBER*/ AND DELETE_FLG = 0 ")]
        T_UKETSUKE_SS_ENTRY GetDataForEntity(T_UKETSUKE_SS_ENTRY uketsukeNum);

        /// <summary>
        /// 収集受付入力エンティティを更新します
        /// </summary>
        /// <param name="tUketsukeSsEntry">更新するエンティティ</param>
        /// <returns>更新した件数</returns>
        [Sql("UPDATE T_UKETSUKE_SS_ENTRY SET DELETE_FLG = 1 WHERE SYSTEM_ID = /*tUketsukeSsEntry.SYSTEM_ID*/ AND SEQ = /*tUketsukeSsEntry.SEQ*/ ")]
        int Update(T_UKETSUKE_SS_ENTRY tUketsukeSsEntry);
    }

    /// <summary>
    /// 収集受付入力詳細DAO
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SS_DETAIL))]
    internal interface IT_UKETSUKE_SS_DETAILDao : IS2Dao
    {
        /// <summary>
        /// 収集受付詳細エンティティを追加します
        /// </summary>
        /// <param name="tUketsukeSsDetail">追加するエンティティ</param>
        /// <returns>追加した件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SS_DETAIL tUketsukeSsDetail);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">画面．受付番号</param>
        /// <returns></returns>
        [Sql("SELECT * FROM T_UKETSUKE_SS_DETAIL WHERE SYSTEM_ID = /*uketsukeNum.SYSTEM_ID*/ AND SEQ = /*uketsukeNum.SEQ*/ ")]
        T_UKETSUKE_SS_DETAIL[] GetDataForEntity(T_UKETSUKE_SS_DETAIL uketsukeNum);
    }

    /// <summary>
    /// 収集受付入力コンテナ詳細DAO
    /// </summary>
    [Bean(typeof(T_CONTENA_RESERVE))]
    internal interface IT_CONTENA_RESERVEDao : IS2Dao
    {
        /// <summary>
        /// 収集受付詳細エンティティを追加します
        /// </summary>
        /// <param name="tUketsukeSsDetail">追加するエンティティ</param>
        /// <returns>追加した件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_CONTENA_RESERVE tUketsukeSsDetail);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">画面．受付番号</param>
        /// <returns></returns>
        [Sql("SELECT * FROM T_CONTENA_RESERVE WHERE SYSTEM_ID = /*contenaEntry.SYSTEM_ID*/ AND SEQ = /*contenaEntry.SEQ*/ ")]
        T_CONTENA_RESERVE[] GetDataForEntity(T_CONTENA_RESERVE contenaEntry);

        /// <summary>
        /// 収集受付入力エンティティを更新します
        /// </summary>
        /// <param name="tUketsukeSsEntry">更新するエンティティ</param>
        /// <returns>更新した件数</returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_CONTENA_RESERVE data);

        /// <summary>
        /// モバイル将軍にて登録したコンテナ情報を取得
        /// </summary>
        /// <param name="SYSTEM_ID">SYSTEM_ID</param>
        /// <param name="SEQ">SEQ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetContenaDataByMobisyo.sql")]
        DataTable GetContenaDataByMobisyo(T_CONTENA_RESERVE data);

        /// <summary>
        /// コンテナを取得
        /// </summary>
        /// <param name="SEQ_NO"></param>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetContenaDetail.sql")]
        DataTable GetContenaDetail(SqlInt64 SEQ_NO);
    }

    /// <summary>
    /// 売上支払コンテナ詳細DAO
    /// </summary>
    [Bean(typeof(T_CONTENA_RESULT))]
    internal interface IT_CONTENA_RESULTDao : IS2Dao
    {
        /// <summary>
        /// 収集受付詳細エンティティを追加します
        /// </summary>
        /// <param name="tUketsukeSsDetail">追加するエンティティ</param>
        /// <returns>追加した件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_CONTENA_RESULT tContenaDetail);
    }

    /// <summary>
    /// コンテナマスタDAO
    /// </summary>
    [Bean(typeof(M_CONTENA))]
    public interface IM_CONTENADao : IS2Dao
    {
        /// <summary>
        /// キー項目よりコンテナマスタ取得
        /// </summary>
        /// <param name="ContenaShuruiCd">コンテナ種類CD</param>
        /// <param name="ContenaCd">コンテナCD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetContenaMaster.sql")]
        M_CONTENA GetContenaMasterEntity(string ContenaShuruiCd, string ContenaCd);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CONTENA data);
    }

    /// <summary>
    /// 出荷受付入力DAO
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SK_ENTRY))]
    internal interface IT_UKETSUKE_SK_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// 出荷受付入力エンティティを追加します
        /// </summary>
        /// <param name="tUketsukeSsEntry">追加するエンティティ</param>
        /// <returns>追加した件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SK_ENTRY tUketsukeSkEntry);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">画面．受付番号</param>
        /// <returns></returns>
        [Sql("SELECT * FROM T_UKETSUKE_SK_ENTRY WHERE UKETSUKE_NUMBER = /*uketsukeNum.UKETSUKE_NUMBER*/ AND DELETE_FLG = 0 ")]
        T_UKETSUKE_SK_ENTRY GetDataForEntity(T_UKETSUKE_SK_ENTRY uketsukeNum);

        /// <summary>
        /// 出荷受付入力エンティティを更新します
        /// </summary>
        /// <param name="tUketsukeSsEntry">更新するエンティティ</param>
        /// <returns>更新した件数</returns>
        [Sql("UPDATE T_UKETSUKE_SK_ENTRY SET DELETE_FLG = 1 WHERE SYSTEM_ID = /*tUketsukeSkEntry.SYSTEM_ID*/ AND SEQ = /*tUketsukeSkEntry.SEQ*/ ")]
        int Update(T_UKETSUKE_SK_ENTRY tUketsukeSkEntry);
    }

    /// <summary>
    /// 出荷受付入力詳細DAO
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SK_DETAIL))]
    internal interface IT_UKETSUKE_SK_DETAILDao : IS2Dao
    {
        /// <summary>
        /// 出荷受付詳細エンティティを追加します
        /// </summary>
        /// <param name="tUketsukeSsDetail">追加するエンティティ</param>
        /// <returns>追加した件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SK_DETAIL tUketsukeSkDetail);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">画面．受付番号</param>
        /// <returns></returns>
        [Sql("SELECT * FROM T_UKETSUKE_SK_DETAIL WHERE SYSTEM_ID = /*uketsukeNum.SYSTEM_ID*/ AND SEQ = /*uketsukeNum.SEQ*/ ")]
        T_UKETSUKE_SK_DETAIL[] GetDataForEntity(T_UKETSUKE_SK_DETAIL uketsukeNum);
    }
    #endregion

    #region 稼働状況
    /// <summary>
    /// 稼働状況DAO
    /// </summary>
    [Bean(typeof(T_MOBISYO_RT_KADOUJYOUKYOU))]
    internal interface IT_MOBISYO_RT_KADOUJYOUKYOUDao : IS2Dao
    {
        /// <summary>
        /// システム日付時点のデータを全取得する
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM T_MOBISYO_RT_KADOUJYOUKYOU WHERE CONVERT(nvarchar,ICHI_UPDATE_DATE,111) = CONVERT(nvarchar,GETDATE(),111) ORDER BY UNTENSHA_CD")]
        T_MOBISYO_RT_KADOUJYOUKYOU[] GetDataForSystemDate();

        /// <summary>
        /// 回収情報
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetKaisyuuInfo.sql")]
        DataTable GetKaisyuuInfo(string untenshaCd, string sharyouCd, string gyoushaCd, string sagyouDate);

        /// <summary>
        /// 回収情報の現場カウント用
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileJoukyouIchiran.Sql.GetKaisyuuInfoForGenbaCnt.sql")]
        DataTable GetKaisyuuInfoForGenbaCnt(string untenshaCd, string sharyouCd, string courseNameCd, string seq, string joken, string sagyouDate);
    }
    #endregion
}
