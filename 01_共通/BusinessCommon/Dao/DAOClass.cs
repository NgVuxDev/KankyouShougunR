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
using Shougun.Core.Common.BusinessCommon.Dto;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Common.BusinessCommon.Dao
{
    /// <summary>
    /// 締めチェック処理DAOクラス
    /// </summary>
    [Bean(typeof(M_OUTPUT_PATTERN))]
    public interface SeikyuShimeShoriDao : IS2Dao
    {

        /// <summary>
        /// Entityで絞り込んで受入入力/明細テーブルから取得する[期間・伝票締処理：受入・対象データ取得]「共通」
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.CheckUkeireData.sql")]
        DataTable SelectUkeireCheckDataForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで出荷入力/明細テーブルから取得する[期間・伝票締処理：出荷・対象データ取得]「共通」
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.CheckShukkaData.sql")]
        DataTable SelectShukkaCheckDataForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで売上入力/明細テーブルから取得する[期間・伝票締処理：売上・対象データ取得]「共通」
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.CheckUriageShiharaiData.sql")]
        DataTable SelectUriageCheckDataForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで請求明細テーブルから取得する[期間・伝票 締処理：請求明細・対象データ取得]「共通」
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.SelectSeikyuMeisaiData.sql")]
        int SelectSeikyuMeisaiDataForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで受入入力テーブルから合計金額を取得する[期間・伝票締処理：受入・合計金額]「共通」
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.CheckUkeireGoukeiKingaku.sql")]
        DataTable CheckUkeireGoukeiKingakuForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで出荷入力テーブルから合計金額を取得する[期間・伝票締処理：出荷・合計金額]「共通」
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.CheckShukkaGoukeiKingaku.sql")]
        DataTable CheckShukkaGoukeiKingakuForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで出荷入力テーブルから合計金額を取得する[期間・伝票締処理：売上・合計金額]「共通」
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.CheckUriageGoukeiKingaku.sql")]
        DataTable CheckUriageGoukeiKingakuForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで受入入力テーブルから取得する[期間・伝票締処理：受入・未確定]「共通」
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.CheckUkeireMikakuteiData.sql")]
        DataTable CheckUkeireMikakuteiDataForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで出荷入力テーブルから取得する[期間・伝票締処理：出荷・未確定]「共通」
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.CheckShukkaMikakuteiData.sql")]
        DataTable CheckShukkaMikakuteiDataForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで売上入力テーブルから取得する[期間・伝票締処理：売上・未確定]「共通」
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.CheckUriageMikakuteiData.sql")]
        DataTable CheckUriageMikakuteiDataForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで入金入力/明細テーブルから取得する[期間・伝票締処理：入金・データ取得]「共通」
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.CheckNyuukinData.sql")]
        DataTable SelectNyuukinCheckDataForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで入金入力テーブルから合計金額を取得する[期間・伝票締処理:入金・未確定]「共通」
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.CheckNyuukinGoukeiKingaku.sql")]
        DataTable CheckNyuukinGoukeiKingakuForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで出金入力/明細テーブルから取得する[期間・伝票締処理：出金・データ取得]「共通」
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.CheckShukkinData.sql")]
        DataTable SelectShukkinCheckDataForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで出金入力テーブルから合計金額を取得する[期間・伝票締処理:出金・未確定]「共通」
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.CheckShukkinGoukeiKingaku.sql")]
        DataTable CheckShukkinGoukeiKingakuForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// SQL構文からデータの取得を行う「未使用」
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDataForStringSql(string sql);
        
        //=======================
        //請求チェック画面使用DAO
        //=======================
        /// <summary>
        /// Entityで絞り込んで受入入力/明細テーブルから取得する[期間・伝票締チェック画面]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Check_CheckUkeireData.sql")]
        DataTable Check_SelectUkeireCheckDataForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで受入入力テーブルから取得する[期間締チェック画面]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Check_CheckUkeireMikakuteiData.sql")]
        DataTable Check_CheckUkeireMikakuteiDataForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで出荷入力/明細テーブルから取得する[期間締チェック画面]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Check_CheckShukkaData.sql")]
        DataTable Check_SelectShukkaCheckDataForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで出荷入力テーブルから取得する[期間・伝票締チェック画面]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Check_CheckShukkaMikakuteiData.sql")]
        DataTable Check_CheckShukkaMikakuteiDataForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで売上入力/明細テーブルから取得する[期間締チェック画面]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Check_CheckUriageShiharaiData.sql")]
        DataTable Check_SelectUriageCheckDataForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで売上入力テーブルから取得する[期間・伝票締チェック画面]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Check_CheckUriageMikakuteiData.sql")]
        DataTable Check_CheckUriageMikakuteiDataForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで入金入力/明細テーブルから取得する[期間締処理]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Check_CheckNyuukinData.sql")]
        DataTable Check_SelectNyuukinCheckDataForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで出金入力/明細テーブルから取得する[期間締処理]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.Check_CheckShukkinData.sql")]
        DataTable Check_SelectShukkinCheckDataForEntity(SeikyuShimeShoriDto data);

        //電子系マスタ検索用
        /// <summary>
        /// 電子廃棄物種類コード名称検索用
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiHaikiShuruiSearchAndCheckSql.sql")]
        DataTable GetDenshiHaikiShuruiForEntity(DenshiSearchParameterDtoCls data);

        /// <summary>
        /// 電子廃棄物名称コードと名称検索用
        /// </summary>
        //  [M_DENSHI_HAIKI_NAME]
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiHaikiNameSearchAndCheckSql.sql")]
        DataTable GetDenshiHaikiNameForEntity(DenshiSearchParameterDtoCls data);

        /// <summary>
        /// 電子業者検索用
        /// </summary>
        ///  [M_DENSHI_JIGSHA]
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiGyoushaSearchAndCheckSql.sql")]
        DataTable GetDenshiGyoushaForEntity(DenshiSearchParameterDtoCls data);

        /// <summary>
        /// 電子事業場検索用Dao
        /// </summary>
        //  [M_DENSHI_JIGYOUJOU]
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiGenbaSearchAndCheckSql.sql")]
        DataTable GetDenshiGenbaForEntity(DenshiSearchParameterDtoCls data);

        /// <summary>
        /// 有害物質検索用
        /// </summary>
        //  [M_DENSHI_YUUGAI_BUSSHITSU]
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiYougaibushituSearchAndCheckSql.sql")]
        DataTable GetYougaibutujituForEntity(DenshiSearchParameterDtoCls data);

        /// <summary>
        /// 電子担当者検索用
        /// </summary>
        //  [M_DENSHI_TANTOUSHA]
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiTantoushaSearchAndCheckSql.sql")]
        DataTable GetTantoushaForEntity(DenshiSearchParameterDtoCls data);

        //適格請求書用新チェック
        /// <summary>
        /// Entityで絞り込んで受入入力/明細テーブルから取得する[期間・伝票締処理：受入・対象データ取得]「共通」
        /// 適格請求書新チェック用
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.CheckUkeireTaxData.sql")]
        DataTable SelectUkeireCheckTaxForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで出荷入力/明細テーブルから取得する[期間・伝票締処理：出荷・対象データ取得]「共通」
        /// 適格請求書新チェック用
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.CheckShukkaTaxData.sql")]
        DataTable SelectShukkaCheckTaxDataForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで売上入力/明細テーブルから取得する[期間・伝票締処理：売上・対象データ取得]「共通」
        /// 適格請求書新チェック用
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.CheckUriageTaxData.sql")]
        DataTable SelectUriageCheckTaxDataForEntity(SeikyuShimeShoriDto data);
    }

    /// 20141112 Houkakou 「受入入力」の締済期間チェックの追加　start
    /// <summary>
    /// 取引先_請求日付DAO
    /// </summary>
    [Bean(typeof(T_SEIKYUU_DENPYOU))]
    internal interface TSKDClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="torihikisakiCD">画面．取引先CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetSeikyuuData.sql")]
        string GetSeikyuuData(DateTime checkdate, string kyotencd, string torihikisakicd);
    }

    /// <summary>
    /// 取引先_精算日付DAO
    /// </summary>
    [Bean(typeof(T_SEISAN_DENPYOU))]
    internal interface TSSDClass : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="torihikisakiCD">画面．取引先CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.GetShiharaiData.sql")]
        string GetSeisanData(DateTime checkdate, string kyotencd, string torihikisakicd);
    }
    /// 20141112 Houkakou 「受入入力」の締済期間チェックの追加　end

}
