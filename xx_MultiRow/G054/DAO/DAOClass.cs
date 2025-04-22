// $Id: DAOClass.cs 36292 2014-12-02 02:43:29Z fangjk@oec-h.com $
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

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku
{
    internal class DAOClass : IS2Dao
    {
        public DAOClass()
        {
        }
        #region Utility
        public int Insert(SuperEntity data)
        {
            throw new NotImplementedException();
        }

        public int Update(SuperEntity data)
        {
            throw new NotImplementedException();
        }

        public int Delete(SuperEntity data)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetAllMasterDataForPopup(string whereSql)
        {
            throw new NotImplementedException();
        }

        public SuperEntity GetDataForEntity(SuperEntity date)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetDateForStringSql(string sql)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    /// <summary>
    /// コンテナ情報DAO
    /// </summary>
    [Bean(typeof(T_CONTENA_RESULT))]
    internal interface TCRClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="sysId">受入入力．システムID</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Sql.GetContena.sql")]
        T_CONTENA_RESULT[] GetContena(string sysId);

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
    internal interface TCREClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="sysId">受入入力．システムID</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Sql.GetContenaReserveData.sql")]
        T_CONTENA_RESERVE[] GetContenaReserve(string sysId, string SEQ);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_CONTENA_RESERVE data);
    }
    
    /// <summary>
    /// コンテナマスタDAO
    /// </summary>
    [Bean(typeof(M_CONTENA))]
    public interface MCClass : IS2Dao
    {
        /// <summary>
        /// キー項目よりコンテナマスタ取得
        /// </summary>
        /// <param name="ContenaShuruiCd">コンテナ種類CD</param>
        /// <param name="ContenaCd">コンテナCD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Sql.GetContenaMaster.sql")]
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
    /// 取引先_請求情報DAO
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI_SEIKYUU))]
    internal interface MTSeiClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="torihikisakiCD">画面．取引先CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Sql.GetTorihikisakiKBN_Seikyuu.sql")]
        string GetTorihikisakiKBN_Seikyuu(string torihikisakiCD);
    }

    /// <summary>
    /// 取引先_支払情報DAO
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI_SHIHARAI))]
    internal interface MTShihaClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="torihikisakiCD">画面．取引先CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Sql.GetTorihikisakiKBN_Shiharai.sql")]
        string GetTorihikisakiKBN_Shiharai(string torihikisakiCD);
    }

    #region 収集受付
    /// <summary>
    /// 収集受付入力DAO
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SS_ENTRY))]
    internal interface TUSSEClass : IS2Dao
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
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_UKETSUKE_SS_ENTRY tUketsukeSsEntry);

        /// <summary>
        /// 収集受付入力エンティティを取得します
        /// </summary>
        /// <param name="systemId">SYSTEM_ID</param>
        /// <param name="seq">SEQ</param>
        /// <returns>収集受付入力エンティティ</returns>
        [Sql("SELECT * FROM T_UKETSUKE_SS_ENTRY WHERE SYSTEM_ID = /*systemId*/ AND SEQ = /*seq*/ AND DELETE_FLG = 0")]
        T_UKETSUKE_SS_ENTRY GetDataByKey(string systemId, string seq);

        /// <summary>
        /// 有効な収集受付入力エンティティを取得します
        /// </summary>
        /// <param name="systemId">SYSTEM_ID</param>
        /// <returns>収集受付入力エンティティ</returns>
        [Sql("SELECT * FROM T_UKETSUKE_SS_ENTRY WHERE SYSTEM_ID = /*systemId*/ AND DELETE_FLG = 0")]
        T_UKETSUKE_SS_ENTRY GetDataBySystemId(string systemId);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">画面．受付番号</param>
        /// <returns></returns>
        T_UKETSUKE_SS_ENTRY GetDataForEntity(T_UKETSUKE_SS_ENTRY uketsukeNum);

        [SqlFile("Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Sql.GetUketsukeSSData.sql")]
        DataTable GetUketsukeSSData(string uketsukeNum);

        [SqlFile("Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Sql.GetUketsukeSSRenkeiData.sql")]
        DataTable GetUketsukeSSRenkeiData(string uketsukeNumber, int densyuKbn, string filteringDenpyouNumber);
    }

    /// <summary>
    /// 収集受付入力詳細DAO
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SS_DETAIL))]
    internal interface TUSSDClass : IS2Dao
    {
        /// <summary>
        /// 収集受付詳細エンティティを追加します
        /// </summary>
        /// <param name="tUketsukeSsDetail">追加するエンティティ</param>
        /// <returns>追加した件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SS_DETAIL tUketsukeSsDetail);

        /// <summary>
        /// 収集受付詳細エンティティを取得します
        /// </summary>
        /// <param name="systemId">SYSTEM_ID</param>
        /// <param name="seq">SEQ</param>
        /// <returns>収集受付詳細エンティティ</returns>
        [Sql("SELECT * FROM T_UKETSUKE_SS_DETAIL WHERE SYSTEM_ID = /*systemId*/ AND SEQ = /*seq*/")]
        List<T_UKETSUKE_SS_DETAIL> GetDataByKey(string systemId, string seq);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">画面．受付番号</param>
        /// <returns></returns>
        T_UKETSUKE_SS_DETAIL[] GetDataForEntity(T_UKETSUKE_SS_DETAIL uketsukeNum);
    }
    #endregion

    #region 出荷受付
    /// <summary>
    /// 出荷受付入力DAO
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SK_ENTRY))]
    internal interface TUSKEClass : IS2Dao
    {
        /// <summary>
        /// 出荷受付入力エンティティを追加します
        /// </summary>
        /// <param name="tUketsukeSkEntry">追加する出荷受付入力エンティティ</param>
        /// <returns>追加した件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SK_ENTRY tUketsukeSkEntry);

        /// <summary>
        /// 出荷受付入力エンティティを更新します
        /// </summary>
        /// <param name="tUketsukeSkEntry">更新する出荷受付入力エンティティ</param>
        /// <returns>更新した件数</returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_UKETSUKE_SK_ENTRY tUketsukeSkEntry);

        /// <summary>
        /// 出荷受付入力エンティティを取得します
        /// </summary>
        /// <param name="systemId">SYSTEM_ID</param>
        /// <param name="seq">SEQ</param>
        /// <returns>出荷受付入力エンティティ</returns>
        [Sql("SELECT * FROM T_UKETSUKE_SK_ENTRY WHERE SYSTEM_ID = /*systemId*/ AND SEQ = /*seq*/ AND DELETE_FLG = 0")]
        T_UKETSUKE_SK_ENTRY GetDataByKey(string systemId, string seq);

        /// <summary>
        /// 有効な出荷受付入力エンティティを取得します
        /// </summary>
        /// <param name="systemId">SYSTEM_ID</param>
        /// <returns>出荷受付入力エンティティ</returns>
        [Sql("SELECT * FROM T_UKETSUKE_SK_ENTRY WHERE SYSTEM_ID = /*systemId*/ AND DELETE_FLG = 0")]
        T_UKETSUKE_SK_ENTRY GetDataBySystemId(string systemId);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">画面．受付番号</param>
        /// <returns></returns>
        T_UKETSUKE_SK_ENTRY GetDataForEntity(T_UKETSUKE_SK_ENTRY uketsukeNum);

        [SqlFile("Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Sql.GetUketsukeSKData.sql")]
        DataTable GetUketsukeSKData(string uketsukeNum);

        [SqlFile("Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Sql.GetUketsukeSKRenkeiData.sql")]
        DataTable GetUketsukeSKRenkeiData(string uketsukeNumber, int densyuKbn, string filteringDenpyouNumber);
    }

    /// <summary>
    /// 出荷受付詳細DAO
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SK_DETAIL))]
    internal interface TUSKDClass : IS2Dao
    {
        /// <summary>
        /// 出荷受付詳細エンティティを追加します
        /// </summary>
        /// <param name="tUketsukeSkDetail">追加する出荷受付入力エンティティ</param>
        /// <returns>追加した件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SK_DETAIL tUketsukeSkDetail);

        /// <summary>
        /// 出荷受付詳細エンティティを取得します
        /// </summary>
        /// <param name="systemId">SYSTEM_ID</param>
        /// <param name="seq">SEQ</param>
        /// <returns>取得した出荷受付詳細エンティティ</returns>
        [Sql("SELECT * FROM T_UKETSUKE_SK_DETAIL WHERE SYSTEM_ID = /*systemId*/ AND SEQ = /*seq*/")]
        List<T_UKETSUKE_SK_DETAIL> GetDataByKey(string systemId, string seq);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">画面．受付番号</param>
        /// <returns></returns>
        T_UKETSUKE_SK_DETAIL[] GetDataForEntity(T_UKETSUKE_SK_DETAIL uketsukeNum);
    }
    #endregion

    #region 計量番号相関
    // 20140512 kayo No.679 計量番号連携 start
    /// <summary>
    /// 計量入力DAO
    /// </summary>
    [Bean(typeof(T_KEIRYOU_ENTRY))]
    internal interface TKEClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">画面．計量番号</param>
        /// <returns></returns>
        T_KEIRYOU_ENTRY GetDataForEntity(T_KEIRYOU_ENTRY keiryouNum);

        [SqlFile("Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Sql.GetKeiryouData.sql")]
        DataTable GetKeiryouData(string keiryouNum);


        /// <summary>
        /// 受付番号より該当受付番号を持つ計量データの番号を取得する
        /// </summary>
        /// <param name="uketsukeNum"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Sql.GetKeiryouDataByUketsukeNumber.sql")]
        SqlInt64 GetKeiryouDataByUketsukeNumber(string uketsukeNum);
    }

    // 20140512 kayo No.679 計量番号連携 end
    #endregion

    #region 持込受付入力
    // 20140512 kayo No.679 計量番号連携 start
    /// <summary>
    /// 持込受付入力DAO
    /// </summary>
    [Bean(typeof(T_UKETSUKE_MK_ENTRY))]
    internal interface TUMKEClass : IS2Dao
    {
        /// <summary>
        /// 持込受付入力エンティティを追加します
        /// </summary>
        /// <param name="tUketsukeMkEntry">追加するエンティティ</param>
        /// <returns>追加した件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_MK_ENTRY tUketsukeMkEntry);

        /// <summary>
        /// 持込受付入力エンティティを更新します
        /// </summary>
        /// <param name="tUketsukeMkEntry">更新するエンティティ</param>
        /// <returns>更新した件数</returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_UKETSUKE_MK_ENTRY tUketsukeMkEntry);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">画面．受付番号</param>
        /// <returns></returns>
        T_UKETSUKE_MK_ENTRY GetDataForEntity(T_UKETSUKE_MK_ENTRY uketsukeNum);

        [SqlFile("Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Sql.GetUketsukeMKData.sql")]
        DataTable GetUketsukeMKData(string uketsukeNum);

        /// <summary>
        /// 持込受付入力エンティティを取得します
        /// </summary>
        /// <param name="systemId">SYSTEM_ID</param>
        /// <param name="seq">SEQ</param>
        /// <returns>持込受付入力エンティティ</returns>
        [Sql("SELECT * FROM T_UKETSUKE_MK_ENTRY WHERE SYSTEM_ID = /*systemId*/ AND SEQ = /*seq*/ AND DELETE_FLG = 0")]
        T_UKETSUKE_MK_ENTRY GetDataByKey(string systemId, string seq);
    }

    // 20140512 kayo No.679 計量番号連携 end

    /// <summary>
    /// 受付（持込）明細DAO
    /// </summary>
    [Bean(typeof(T_UKETSUKE_MK_DETAIL))]
    internal interface TUMKDClass : IS2Dao
    {
        /// <summary>
        /// 持込受付詳細エンティティを追加します
        /// </summary>
        /// <param name="tUketsukeMkDetail">追加するエンティティ</param>
        /// <returns>追加した件数</returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_MK_DETAIL tUketsukeMkDetail);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">画面．受付番号</param>
        /// <returns></returns>
        T_UKETSUKE_MK_DETAIL[] GetDataForEntity(T_UKETSUKE_MK_DETAIL uketsukeNum);

        /// <summary>
        /// 持込受付詳細エンティティを取得します
        /// </summary>
        /// <param name="systemId">SYSTEM_ID</param>
        /// <param name="seq">SEQ</param>
        /// <returns>持込受付詳細エンティティ</returns>
        [Sql("SELECT * FROM T_UKETSUKE_MK_DETAIL WHERE SYSTEM_ID = /*systemId*/ AND SEQ = /*seq*/")]
        List<T_UKETSUKE_MK_DETAIL> GetDataByKey(string systemId, string seq);
    }
    #endregion

    /// <summary>
    /// 形態区分DAO
    /// </summary>
    [Bean(typeof(M_KEITAI_KBN))]
    internal interface MKKClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値のうち、一番若いコードを取得
        /// </summary>
        /// <param name="denshuKbnCd">伝種区分CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Sql.GetKeitaiKbnCd.sql")]
        SqlInt16 GetKeitaiKbnCd(SqlInt16 denshuKbnCd);
    }

    /// <summary>
    /// 売上／支払番号DAO
    /// </summary>
    [Bean(typeof(T_UR_SH_ENTRY))]
    internal interface TUEClass : IS2Dao
    {
        /// <summary>
        /// 指定された売上／支払番号の次に小さい番号を取得
        /// </summary>
        /// <param name="UrshNumber">売上／支払番号</param>
        /// <param name="KyotenCD">拠点CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Sql.GetPreUrshNumber.sql")]
        SqlInt64 GetPreUrshNumber(SqlInt64 UrshNumber, string KyotenCD);

        /// <summary>
        /// 指定された売上／支払番号の次に大きい番号を取得
        /// </summary>
        /// <param name="UrshNumber">売上／支払番号</param>
        /// <param name="KyotenCD">拠点CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Sql.GetNextUrshNumber.sql")]
        SqlInt64 GetNextUrshNumber(SqlInt64 UrshNumber, string KyotenCD);
    }

    /// <summary>
    /// 定期実績明細DAO
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    [Bean(typeof(T_TEIKI_JISSEKI_DETAIL))]
    public interface TJDClass : IS2Dao
    {
        /// <summary>
        /// 売上支払番号より定期実績Detail取得
        /// </summary>
        /// <param name="UrshNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UriageShiharaiNyuuryoku.Sql.GetTeikiJissekiDetailData.sql")]
        T_TEIKI_JISSEKI_DETAIL[] GetTeikiJissekiDetailEntity(string UrshNumber);

        /// <summary>
        /// Update ("UR_SH_NUMBER"の更新)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TEIKI_JISSEKI_NUMBER", "ROW_NUMBER", "GYOUSHA_CD", "GENBA_CD", "HINMEI_CD",
            "SUURYOU", "UNIT_CD", "ANBUN_SUURYOU", "NIOROSHI_NUMBER", "TSUKIGIME_KBN", "TIME_STAMP",
            "SHUUSHUU_TIME", "KAISHUU_BIKOU", "HINMEI_BIKOU", "KANSAN_SUURYOU", "KANSAN_UNIT_CD",
            "DENPYOU_KBN_CD", "KEIYAKU_KBN", "KANSAN_UNIT_MOBILE_OUTPUT_FLG")]
        int Update(T_TEIKI_JISSEKI_DETAIL data);
    }
}
