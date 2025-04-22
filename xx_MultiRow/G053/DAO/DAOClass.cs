// $Id: DAOClass.cs 50371 2015-05-22 03:54:02Z wuq@oec-h.com $
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.SalesPayment.SyukkaNyuuryoku.DAO
{
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
        [SqlFile("Shougun.Core.SalesPayment.SyukkaNyuuryoku.Sql.GetTorihikisakiKBN_Seikyuu.sql")]
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
        [SqlFile("Shougun.Core.SalesPayment.SyukkaNyuuryoku.Sql.GetTorihikisakiKBN_Shiharai.sql")]
        string GetTorihikisakiKBN_Shiharai(string torihikisakiCD);
    }

    /// <summary>
    /// 在庫情報DAO
    /// </summary>
    [Bean(typeof(T_ZAIKO_SHUKKA_DETAIL))]
    internal interface TZSDClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="sysId">明細部．明細システムID</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.SyukkaNyuuryoku.Sql.GetZaikoInfo.sql")]
        List<T_ZAIKO_SHUKKA_DETAIL> GetZaikoInfo(T_ZAIKO_SHUKKA_DETAIL data);

        /// <summary>
        /// 検索条件に合った値を全取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns></returns>
        T_ZAIKO_SHUKKA_DETAIL GetDataForEntity(T_ZAIKO_SHUKKA_DETAIL data);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_ZAIKO_SHUKKA_DETAIL data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_ZAIKO_SHUKKA_DETAIL data);
    }

    // No.4578-->
    // 20150415 go 在庫品名振分DAO追加(修正後のG051と同様追加) Start
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
        [SqlFile("Shougun.Core.SalesPayment.SyukkaNyuuryoku.Sql.GetZaikoInfo2.sql")]
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
    // 20150415 go 在庫品名振分DAO追加 End
    // No.4578<--

    /// <summary>
    /// 受付（出荷）入力DAO
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

        [SqlFile("Shougun.Core.SalesPayment.SyukkaNyuuryoku.Sql.GetUketsukeSKData.sql")]
        DataTable GetUketsukeSKData(string uketsukeNum);

        /// <summary>
        /// 出荷受付入力の連携伝票を取得します。
        /// </summary>
        /// <param name="uketsukeNumber">検索対象の受付番号</param>
        /// <param name="densyuKbn">除外したい伝票がある場合、その伝票の伝種区分を指定</param>
        /// <param name="filteringDenpyouNumber">除外したい伝票がある場合、除外したい伝票番号を指定(denshuKbnと組み合わせて指定する)</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.SyukkaNyuuryoku.Sql.GetUketsukeSKRenkeiData.sql")]
        DataTable GetUketsukeSKRenkeiData(string uketsukeNumber, int densyuKbn, string filteringDenpyouNumber);
    }

    /// <summary>
    /// 受付（出荷）明細DAO
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

        [SqlFile("Shougun.Core.SalesPayment.SyukkaNyuuryoku.Sql.GetKeiryouData.sql")]
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
    /// 検収明細DAO
    /// </summary>
    [Bean(typeof(T_KENSHU_DETAIL))]
    internal interface TKSDClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="sysId">受入入力．システムID</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.SyukkaNyuuryoku.Sql.GetKenshuDetail.sql")]
        List<T_KENSHU_DETAIL> GetKenshuDetail(T_KENSHU_DETAIL data);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_KENSHU_DETAIL data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_KENSHU_DETAIL data);
    }

    // No.4578-->
    // 20150415 go 在庫品名DAO削除(修正後のG051と同様修正) Start
    ///// <summary>
    ///// 在庫品名DAO
    ///// </summary>
    //[Bean(typeof(M_ZAIKO_HINMEI))]
    //internal interface MZHClass : IS2Dao
    //{
    //    /// <summary>
    //    /// 検索条件に合った値を全取得する
    //    /// </summary>
    //    /// <param name="data">明細．在庫品名CD</param>
    //    /// <returns></returns>
    //    M_ZAIKO_HINMEI GetDataForEntity(M_ZAIKO_HINMEI zaikoHinmeiCd);
    //}
    // 20150415 go 在庫品名DAO削除 End

    // 20150415 go 在庫比率DAO(修正後のG051と同様追加) Start
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
        [SqlFile("Shougun.Core.SalesPayment.SyukkaNyuuryoku.Sql.GetZaikoHiritsu.sql")]
        DataTable GetZaikoHiritsuInfo(M_ZAIKO_HIRITSU data);
    }
    // 20150415 go 在庫比率DAO End
    // No.4578<--

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
        [SqlFile("Shougun.Core.SalesPayment.SyukkaNyuuryoku.Sql.GetKeitaiKbnCd.sql")]
        SqlInt16 GetKeitaiKbnCd(SqlInt16 denshuKbnCd);
    }

    /// <summary>
    /// 出荷番号DAO
    /// </summary>
    [Bean(typeof(T_SHUKKA_ENTRY))]
    internal interface TSEClass : IS2Dao
    {
        /// <summary>
        /// 指定された出荷番号の次に小さい番号を取得
        /// </summary>
        /// <param name="ShukkaNumber">出荷番号</param>
        /// <param name="KyotenCD">拠点CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.SyukkaNyuuryoku.Sql.GetPreShukkaNumber.sql")]
        SqlInt64 GetPreShukkaNumber(SqlInt64 ShukkaNumber, string KyotenCD);

        /// <summary>
        /// 指定された出荷番号の次に大きい番号を取得
        /// </summary>
        /// <param name="ShukkaNumber">出荷番号</param>
        /// <param name="KyotenCD">拠点CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.SalesPayment.SyukkaNyuuryoku.Sql.GetNextShukkaNumber.sql")]
        SqlInt64 GetNextShukkaNumber(SqlInt64 ShukkaNumber, string KyotenCD);
    }

    //internal class DAOClass : IS2Dao
    //{
    //    public int Insert(SuperEntity data)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public int Update(SuperEntity data)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public int Delete(SuperEntity data)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public System.Data.DataTable GetAllMasterDataForPopup(string whereSql)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public SuperEntity GetDataForEntity(SuperEntity date)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public System.Data.DataTable GetDateForStringSql(string sql)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
