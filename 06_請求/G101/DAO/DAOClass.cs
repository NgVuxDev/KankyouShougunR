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

namespace Shougun.Core.Billing.SeikyuShimeShori
{
    [Bean(typeof(M_OUTPUT_PATTERN))]
    public interface SeikyuShimeShoriDao : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで自社マスタ値を取得する「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetCorpInfo.sql")]
        DataTable GetCorpDataForEntity(M_CORP_INFO data);

        /// <summary>
        /// Entityで絞り込んで拠点マスタ値を取得する「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetKyotenData.sql")]
        DataTable GetKyotenDataForEntity(M_KYOTEN data);

        /// <summary>
        /// Entityで絞り込んで取引先マスタ値を取得する「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetTorihikisakiData.sql")]
        DataTable GetTorihikisakiDataForEntity(M_TORIHIKISAKI data);

        /// <summary>
        /// 【期間締め】Entityで絞り込んで明細部に表示する取引先マスタ、取引先請求テーブルの情報を取得する「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetKikanShimeshoriDispData.sql")]
        DataTable GetDispDataForEntity(SeikyuShimeShoriDispDto data);

        /// <summary>
        /// 【伝票締め】Entityで絞り込んで明細部に表示する受入入力、明細テーブルの情報を取得する「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetDenpyouShimeshoriDispDataForUkeire.sql")]
        DataTable GetDispDataDenpyouShimeUkeireForEntity(SeikyuShimeShoriDispDto data);

        /// <summary>
        /// 【伝票締め】Entityで絞り込んで明細部に表示する出荷入力、明細テーブルの情報を取得する「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetDenpyouShimeshoriDispDataForShukka.sql")]
        DataTable GetDispDataDenpyouShimeShukkaForEntity(SeikyuShimeShoriDispDto data);

        /// <summary>
        /// 【伝票締め】Entityで絞り込んで明細部に表示する売上支払入力、明細テーブルの情報を取得する「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetDenpyouShimeshoriDispDataForUriage.sql")]
        DataTable GetDispDataDenpyouShimeUriageForEntity(SeikyuShimeShoriDispDto data);

        //VAN 20210429 #148577 S
        /// <summary>
        /// 【伝票締め】Entityで絞り込んで明細部に表示する入金入力、明細テーブルの情報を取得する「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetDenpyouShimeshoriDispDataForNyuukin.sql")]
        DataTable GetDispDataDenpyouShimeNyuukinForEntity(SeikyuShimeShoriDispDto data);
        //VAN 20210429 #148577 E

        /// <summary>
        /// Entityで絞り込んで請求伝票テーブルをチェックする(締日=0)[期間・伝票締処理]「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.CheckDateSelectedZensha.sql")]
        int CheckDateSelectedZenshaForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで請求伝票テーブルをチェックする(締日=0以外)[期間・伝票締処理]「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.CheckDateSelectedNotZensha.sql")]
        int CheckDateSelectedNotZenshaForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで請求伝票テーブルから最新の請求日付を取得する[期間締処理]「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.SelectSeikyuDenpyouNewDate.sql")]
        string SelectSeikyuDenpyouNewDateForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで締め処理エラーテーブルから条件に一致するレコードがあるかチェック「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.CheckErrorTableData.sql")]
        int CheckErrorTableDataForEntity(CheckErrorMssageDto data);

        /// <summary>
        /// Entityで絞り込んで締め処理エラーテーブルから条件に一致するレコードを取得する「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.SelectErrorTableData.sql")]
        DataTable SelectErrorTableDataForEntity(CheckErrorMssageDto data);

        /// <summary>
        /// Entityで絞り込んで締め処理中テーブルから処理中かどうかチェックする「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.CheckShimeShoriUserData.sql")]
        DataTable CheckShimeShoriUserDataForEntity(CheckDto data);

        /// <summary>
        /// SQL構文からデータの取得を行う「未使用」
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDataForStringSql(string sql);
        
        //===============================================================
        /// <summary>
        /// Entityで絞り込んで回収月情報を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetKaisyuutukiData.sql")]
        DataTable GetKaisyuutukiDataForEntity(M_TORIHIKISAKI data);

        /// <summary>
        /// Entityで絞り込んで繰越額を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetZenkaiKurikosigakuDataKikan.sql")]
        DataTable GetZenkaiKurikosigakuDataKikanForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで繰越額(取引先_請求情報マスタ)を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetZenkaiKurikosigakuKaisiDataKikan.sql")]
        DataTable GetZenkaiKurikosigakuKaisiDataKikanForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで売上額(受入(期間))を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetUriageUkeireDataKikan.sql")]
        DataTable GetUriageUkeireDataKikanForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで売上額(受入(伝票))を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetUriageUkeireDataDenpyou.sql")]
        DataTable GetUriageUkeireDataDenpyouForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで売上額(出荷(期間))を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetUriageShukkaDataKikan.sql")]
        DataTable GetUriageShukkaDataKikanForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで売上額(出荷(伝票))を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetUriageShukkaDataDenpyou.sql")]
        DataTable GetUriageShukkaDataDenpyouForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで売上額(売上/支払(期間))を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetUriageUrShDataKikan.sql")]
        DataTable GetUriageUrShDataKikanForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで売上額(売上/支払(伝票))を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetUriageUrShDataDenpyou.sql")]
        DataTable GetUriageUrShDataDenpyouForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで入金データ(期間)を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetNyuukinDataKikan.sql")]
        DataTable GetNyuukinDataKikanForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで入金データ(伝票/明細)を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetNyuukinDataDenpyouMeisai.sql")]
        DataTable GetNyuukinDataDenpyouMeisaiForEntity(SeikyuShimeShoriDto data);

        //締め実行処理で使用===================================================
        /// <summary>
        /// Entityで絞り込んで請求伝票テーブルから行番号を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetRowNumber.sql")]
        DataTable GetRowNumberForEntity(T_SEIKYUU_DETAIL data);

        /// <summary>
        /// Entityで締処理中テーブルから行番号を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.SelectshimeshorichuuTableData.sql")]
        DataTable SelectshimeshorichuuEntity(T_SHIME_SHORI_CHUU data);

        //取引先チェック===================================================
        /// <summary>
        /// 条件に合ったEntityを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetShimebiCheck.sql")]
        DataTable GetShimebiCheck(string torihikisakiCd, string shimebi);

        /// <summary>
        /// 条件に合ったEntityを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.CheckMishimeDate.sql")]
        DataTable CheckMishimeDate(string torihikisakiCd);

        /// <summary>
        /// 条件に合ったEntityを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.CheckSeikyuushuData.sql")]
        DataTable CheckSeikyuushuData(string seikyuCd, string seikyuuDate, bool selectFlg);

        /// <summary>
        /// 再締請求データを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetSeikyuuSaishimeData.sql")]
        DataTable GetSeikyuuSaishimeData(SeikyuShimeShoriDto data);

        /// <summary>
        /// 再締請求データを削除
        /// </summary>
        /// <param name="seikyuuNumber"></param>
        /// <param name="deleteFlg"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.UpdateSeikyuSaishimeiDeleteData.sql")]
        int UpdateSeikyuSaishimeiDeleteData(Int64 seikyuuNumber,bool deleteFlg);

        /// <summary>
        /// 入金消込チェック
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.CheckNyuukinKeshikomiData.sql")]
        DataTable CheckNyuukinKeshikomiData(Int64 seikyuuNumber);

        /// <summary>
        /// 請求INXSアップロード状況チェック
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.CheckSeikyuuInxsUploadStatus.sql")]
        DataTable CheckSeikyuuInxsUploadStatus(Int64 seikyuuNumber);

        /// <summary>
        /// 最新請求書チェック（未来日の請求書があるか）
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="seikyuuNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.CheckLatestSeikyuuData.sql")]
        int CheckLatestSeikyuuData(string torihikisakiCd, Int64 seikyuuNumber);

        #region バーコード 160013
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.Barcode.GetUkeireData.sql")]
        DataTable BarcodeGetUkeireData(SeikyuShimeShoriDispDto data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.Barcode.GetShukkaData.sql")]
        DataTable BarcodeGetShukkaData(SeikyuShimeShoriDispDto data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.Barcode.GetUriageData.sql")]
        DataTable BarcodeGetUriageData(SeikyuShimeShoriDispDto data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Sql("SELECT GETDATE() AS SYSTEM_DATE")]
        DateTime GetSystemDate(SeikyuShimeShoriDispDto data);
        #endregion
    }

    [Bean(typeof(T_SEIKYUU_DENPYOU))]
    public interface SeikyuuDenpyouDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SEIKYUU_DENPYOU data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Sql("DELETE FROM T_SEIKYUU_DENPYOU WHERE SEIKYUU_NUMBER = /*data.SEIKYUU_NUMBER*/")]
        int Delete(T_SEIKYUU_DENPYOU data);

        /// <summary>
        /// Search
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.GetJikkouRirekiSearchData.sql")]
        DataTable Search(SaishimeSearchDTO data);

        //160015 S
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SEIKYUU_DENPYOU data);

        [Sql("SELECT * FROM T_SEIKYUU_DENPYOU WHERE SEIKYUU_NUMBER = /*SEIKYUU_NUMBER*/ AND DELETE_FLG = 0")]
        T_SEIKYUU_DENPYOU GetDataByKey(Int64 SEIKYUU_NUMBER);
        //160015 E

    }

    [Bean(typeof(T_SEIKYUU_DENPYOU_KAGAMI))]
    public interface SeikyuuDenpyouKagamiDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SEIKYUU_DENPYOU_KAGAMI data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Sql("DELETE FROM T_SEIKYUU_DENPYOU_KAGAMI WHERE SEIKYUU_NUMBER = /*data.SEIKYUU_NUMBER*/")]
        int Delete(T_SEIKYUU_DENPYOU_KAGAMI data);
    }

    [Bean(typeof(T_SEIKYUU_DETAIL))]
    public interface SeikyuuDetailDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SEIKYUU_DETAIL data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SEIKYUU_DETAIL data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Sql("DELETE FROM T_SEIKYUU_DETAIL WHERE SEIKYUU_NUMBER = /*data.SEIKYUU_NUMBER*/")]
        int Delete(T_SEIKYUU_DETAIL data);

    }

    [Bean(typeof(T_SHIME_SHORI_CHUU))]
    public interface ShimeshorichuuDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SHIME_SHORI_CHUU data);


        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_SHIME_SHORI_CHUU data);

        /// <summary>
        /// 指定されたデータを削除します
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
         [SqlFile("Shougun.Core.Billing.SeikyuShimeShori.Sql.DeleteShimeShoriChuu.sql")]
        int DesignateDelete(T_SHIME_SHORI_CHUU data);

        /// <summary>
        /// すべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM T_SHIME_SHORI_CHUU")]
        T_SHIME_SHORI_CHUU[] GetAllData();

    }

    [Bean(typeof(T_SHIME_JIKKOU_RIREKI))]
    public interface ShimeJikkouRirekiDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SHIME_JIKKOU_RIREKI data);


        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_SHIME_JIKKOU_RIREKI data);
    }

    [Bean(typeof(T_SHIME_JIKKOU_RIREKI_TORIHIKISAKI))]
    public interface ShimeJikkouRirekiTorihikiDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SHIME_JIKKOU_RIREKI_TORIHIKISAKI data);
    }
}
