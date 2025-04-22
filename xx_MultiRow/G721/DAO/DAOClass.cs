// $Id$
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.SalesPayment.UkeireNyuuryoku2.DAO
{
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
        [SqlFile("Shougun.Core.SalesPayment.UkeireNyuuryoku2.Sql.GetContena.sql")]
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
        [SqlFile("Shougun.Core.SalesPayment.UkeireNyuuryoku2.Sql.GetContenaReserveData.sql")]
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
        [SqlFile("Shougun.Core.SalesPayment.UkeireNyuuryoku2.Sql.GetContenaMaster.sql")]
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
        [SqlFile("Shougun.Core.SalesPayment.UkeireNyuuryoku2.Sql.GetTorihikisakiKBN_Seikyuu.sql")]
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
        [SqlFile("Shougun.Core.SalesPayment.UkeireNyuuryoku2.Sql.GetTorihikisakiKBN_Shiharai.sql")]
        string GetTorihikisakiKBN_Shiharai(string torihikisakiCD);
    }

    /// <summary>
    /// 在庫情報DAO
    /// </summary>
    [Bean(typeof(T_ZAIKO_UKEIRE_DETAIL))]
    internal interface TZUDClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="sysId">明細部．明細システムID</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UkeireNyuuryoku2.Sql.GetZaikoInfo.sql")]
        List<T_ZAIKO_UKEIRE_DETAIL> GetZaikoInfo(T_ZAIKO_UKEIRE_DETAIL data);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns></returns>
        T_ZAIKO_UKEIRE_DETAIL GetDataForEntity(T_ZAIKO_UKEIRE_DETAIL data);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_ZAIKO_UKEIRE_DETAIL data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_ZAIKO_UKEIRE_DETAIL data);
    }

    // No.4578-->
    // 20150409 go 在庫品名振分DAO追加 Start
    /// <summary>
    /// 在庫品名振分DAO
    /// </summary>
    [Bean(typeof(T_ZAIKO_HINMEI_HURIWAKE))]
    internal interface TZHHClass : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UkeireNyuuryoku2.Sql.GetZaikoInfo2.sql")]
        List<T_ZAIKO_HINMEI_HURIWAKE> GetZaikoInfo(T_ZAIKO_HINMEI_HURIWAKE data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        T_ZAIKO_HINMEI_HURIWAKE GetDataForEntity(T_ZAIKO_HINMEI_HURIWAKE data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_ZAIKO_HINMEI_HURIWAKE data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_ZAIKO_HINMEI_HURIWAKE data);
    }
    // 20150409 go 在庫品名振分DAO追加 End
    // No.4578<--

    /// <summary>
    /// 受付（持込）入力DAO
    /// </summary>
    [Bean(typeof(T_UKETSUKE_MK_ENTRY))]
    internal interface TUMEClass : IS2Dao
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

        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="sysId"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UkeireNyuuryoku2.Sql.GetUketsukeData.sql")]
        DataTable GetUketsukeData(string uketsukeNum);

        /// <summary>
        /// 持込受付入力エンティティを取得します
        /// </summary>
        /// <param name="systemId">SYSTEM_ID</param>
        /// <param name="seq">SEQ</param>
        /// <returns>持込受付入力エンティティ</returns>
        [Sql("SELECT * FROM T_UKETSUKE_MK_ENTRY WHERE SYSTEM_ID = /*systemId*/ AND SEQ = /*seq*/ AND DELETE_FLG = 0")]
        T_UKETSUKE_MK_ENTRY GetDataByKey(string systemId, string seq);
    }

    /// <summary>
    /// 受付（持込）明細DAO
    /// </summary>
    [Bean(typeof(T_UKETSUKE_MK_DETAIL))]
    internal interface TUMDClass : IS2Dao
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

        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="sysId"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UkeireNyuuryoku2.Sql.GetKeiryouData.sql")]
        DataTable GetKeiryouData(string keiryouNum);

    }

    /// <summary>
    /// 計量明細DAO
    /// </summary>
    [Bean(typeof(T_KEIRYOU_DETAIL))]
    internal interface TKDClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">画面．計量番号</param>
        /// <returns></returns>
        T_KEIRYOU_DETAIL[] GetDataForEntity(T_KEIRYOU_DETAIL keiryouNum);
    }

    /// <summary>
    /// 計量入力の受入実績DAO
    /// </summary>
    [Bean(typeof(T_UKEIRE_JISSEKI_ENTRY))]
    internal interface TKUJEClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="sysId"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UkeireNyuuryoku2.Sql.GetKeiryouJissekiData.sql")]
        DataTable GetKeiryouJissekiData(string keiryouNum);

    }

    /// <summary>
    /// 在庫比率DAO
    /// </summary>
    [Bean(typeof(M_ZAIKO_HIRITSU))]
    internal interface MZHIClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UkeireNyuuryoku2.Sql.GetZaikoHiritsu.sql")]
        DataTable GetZaikoHiritsuInfo(M_ZAIKO_HIRITSU data);
    }

    /// <summary>
    /// 受付（収集）入力DAO
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SS_ENTRY))]
    internal interface TUSEClass : IS2Dao
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
        /// 検索条件に合った収集受付入力（DataTable）を取得する
        /// </summary>
        /// <param name="uketsukeNum">受付番号</param>
        /// <returns>収集受付入力（DataTable）</returns>
        [SqlFile("Shougun.Core.SalesPayment.UkeireNyuuryoku2.Sql.GetUketsukeSSData.sql")]
        DataTable GetUketsukeData(string uketsukeNum);

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
        /// 収集受付入力の連携伝票を取得します。
        /// </summary>
        /// <param name="uketsukeNumber">検索対象の受付番号</param>
        /// <param name="densyuKbn">除外したい伝票がある場合、その伝票の伝種区分を指定</param>
        /// <param name="filteringDenpyouNumber">除外したい伝票がある場合、除外したい伝票番号を指定(denshuKbnと組み合わせて指定する)</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UkeireNyuuryoku2.Sql.GetUketsukeSSRenkeiData.sql")]
        DataTable GetUketsukeSSRenkeiData(string uketsukeNumber, int densyuKbn, string filteringDenpyouNumber);
    }

    /// <summary>
    /// 収集受付詳細Dao
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SS_DETAIL))]
    internal interface TUSDClass : IS2Dao
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
    }

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
        [SqlFile("Shougun.Core.SalesPayment.UkeireNyuuryoku2.Sql.GetKeitaiKbnCd.sql")]
        SqlInt16 GetKeitaiKbnCd(SqlInt16 denshuKbnCd);
    }

    /// <summary>
    /// 受入番号DAO
    /// </summary>
    [Bean(typeof(T_UKEIRE_ENTRY))]
    internal interface TUEClass : IS2Dao
    {
        /// <summary>
        /// 指定された受入番号の次に小さい番号を取得
        /// </summary>
        /// <param name="UkeireNumber">受入番号</param>
        /// <param name="KyotenCD">拠点CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UkeireNyuuryoku2.Sql.GetPreUkeireNumber.sql")]
        SqlInt64 GetPreUkeireNumber(SqlInt64 UkeireNumber, string KyotenCD);

        /// <summary>
        /// 指定された受入番号の次に大きい番号を取得
        /// </summary>
        /// <param name="UkeireNumber">受入番号</param>
        /// <param name="KyotenCD">拠点CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UkeireNyuuryoku2.Sql.GetNextUkeireNumber.sql")]
        SqlInt64 GetNextUkeireNumber(SqlInt64 UkeireNumber, string KyotenCD);

        /// <summary>
        /// 業者CDと業者名を取得
        /// </summary>
        /// <param name="denpyouDateFrom">3か月前の作業日</param>
        /// <param name="denpyouDateTo">作業日</param>
        /// <param name="sharyouName">車両名</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UkeireNyuuryoku2.Sql.GetUketsuke3MonthData.sql")]
        List<T_UKEIRE_ENTRY> GetUketsuke3MonthData(string denpyouDateFrom, string denpyouDateTo, string sharyouName, SqlInt16 kyotenCd);

        /// <summary>
        /// 品名ＣＤと品名を取得
        /// </summary>
        /// <param name="denpyouDateFrom">3か月前の作業日</param>
        /// <param name="denpyouDateTo">作業日</param>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UkeireNyuuryoku2.Sql.GetUketsukeHinmei3MonthData.sql")]
        DataTable GetUketsukeHinmei3MonthData(string denpyouDateFrom, string denpyouDateTo,
            string torihikisakiCd, string gyoushaCd, string genbaCd, SqlInt16 kyotenCd);

        //PhuocLoc 2020/12/01 #136219 -Start
        /// <summary>
        /// 滞留一覧を取得
        /// </summary>
        /// <param name="kyotenCd">拠点CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.UkeireNyuuryoku2.Sql.GetTairyuuIchiran.sql")]
        DataTable GetTairyuuIchiran(SqlInt16 kyotenCd, string sharyou, string unpanGyousha);
        //PhuocLoc 2020/12/01 #136219 -End
    }
}
