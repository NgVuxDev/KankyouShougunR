using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using Shougun.Core.Adjustment.Shiharaishimesyori.DTO;
using Shougun.Core.Common.BusinessCommon.Dto;
using System;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Adjustment.Shiharaishimesyori.DAO
{
    [Bean(typeof(M_OUTPUT_PATTERN))]
    public interface ShiharaiShimeShoriDao : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで自社マスタ値を取得する「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.GetCorpInfo.sql")]
        DataTable GetCorpDataForEntity(M_CORP_INFO data);

        /// <summary>
        /// Entityで絞り込んで拠点マスタ値を取得する「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.GetKyotenData.sql")]
        DataTable GetKyotenDataForEntity(M_KYOTEN data);

        /// <summary>
        /// Entityで絞り込んで取引先マスタ値を取得する「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.GetTorihikisakiData.sql")]
        DataTable GetTorihikisakiDataForEntity(M_TORIHIKISAKI data);

        /// <summary>
        /// 【期間締め】Entityで絞り込んで明細部に表示する取引先マスタ、取引先請求テーブルの情報を取得する「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.GetKikanShimeShoriDispData.sql")]
        DataTable GetDispDataForEntity(ShiharaiShimeShoriDispDto data);

        /// <summary>
        /// 【伝票締め】Entityで絞り込んで明細部に表示する受入入力・明細、出荷入力・明細、売上/支払・明細テーブルの情報を取得する「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.GetDenpyouShimeShoriDispData.sql")]
        DataTable GetDispDataDenpyouShimeForEntity(ShiharaiShimeShoriDispDto data);

        //VAN 20210429 #148578 S
        /// <summary>
        /// 【伝票締め】Entityで絞り込んで明細部に表示する入金入力、明細テーブルの情報を取得する「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.GetDenpyouShimeshoriDispDataForShukkin.sql")]
        DataTable GetDispDataDenpyouShimeShukkinForEntity(ShiharaiShimeShoriDispDto data);
        //VAN 20210429 #148578 E

        /// <summary>
        /// Entityで絞り込んで精算伝票テーブルをチェックする(締日=0)[期間・伝票締処理]「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.CheckDateSelectedZensha.sql")]
        int CheckDateSelectedZenshaForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで精算伝票テーブルをチェックする(締日=0以外)[期間・伝票締処理]「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.CheckDateSelectedNotZensha.sql")]
        int CheckDateSelectedNotZenshaForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで精算伝票テーブルから最新の精算日付を取得する[期間締処理]「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.SelectSeisanDenpyouNewDate.sql")]
        string SelectSeisanDenpyouNewDateForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで締め処理エラーテーブルから条件に一致するレコードがあるかチェック「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.CheckErrorTableData.sql")]
        int CheckErrorTableDataForEntity(CheckErrorMessageDTOClass data);

        /// <summary>
        /// Entityで絞り込んで締め処理エラーテーブルから条件に一致するレコードを取得する「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.SelectErrorTableData.sql")]
        DataTable SelectErrorTableDataForEntity(CheckErrorMessageDTOClass data);

        /// <summary>
        /// Entityで絞り込んで締め処理中テーブルから処理中かどうかチェックする「締め」
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.CheckShimeShoriUserData.sql")]
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
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.GetKaisyuutukiData.sql")]
        DataTable GetKaisyuutukiDataForEntity(M_TORIHIKISAKI data);

        /// <summary>
        /// Entityで絞り込んで繰越額を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.GetZenkaiKurikosigakuDataKikan.sql")]
        DataTable GetZenkaiKurikosigakuDataKikanForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで繰越額(取引先_支払情報マスタ)を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.GetZenkaiKurikosigakuKaisiDataKikan.sql")]
        DataTable GetZenkaiKurikosigakuKaisiDataKikanForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで売上額(受入(期間)、出荷(期間)、売上/支払(期間))を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.GetUriageDataKikan.sql")]
        DataTable GetUriageDataKikanForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで売上額(受入(伝票)、出荷(伝票)、売上/支払(伝票))を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.GetUriageDataDenpyou.sql")]
        DataTable GetUriageDataDenpyouForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで出金データ(期間)を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.GetShukkinDataKikan.sql")]
        DataTable GetShukkinDataKikanForEntity(SeikyuShimeShoriDto data);

        /// <summary>
        /// Entityで絞り込んで出金データ(伝票/明細)を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.GetShukkinDataDenpyouMeisai.sql")]
        DataTable GetShukkinDataDenpyouMeisaiForEntity(SeikyuShimeShoriDto data);

        //締め実行処理で使用===================================================
        /// <summary>
        /// Entityで絞り込んで精算伝票テーブルから行番号を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.GetRowNumber.sql")]
        DataTable GetRowNumberForEntity(T_SEISAN_DETAIL data);

        /// <summary>
        /// Entityで絞り込んで締め処理中テーブルを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.SelectShimeShoriChuuTableData.sql")]
        DataTable SelectShimeShoriChuuEntity(T_SHIME_SHORI_CHUU data);

        /// <summary>
        /// Entityで絞り込んで締め実行履歴テーブルを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.SelectshimejikkourirekiTableData.sql")]
        DataTable SelectshimejikkourirekiEntity(T_SHIME_JIKKOU_RIREKI data);

        /// <summary>
        /// Entityで絞り込んで精算伝票テーブルを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.SelectSeisanDenpyouTableData.sql")]
        DataTable SelectSeisanDenpyouEntity(T_SEISAN_DENPYOU data);

        /// <summary>
        /// Entityで絞り込んで精算伝票_鑑テーブルを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.SelectSeisanDenpyouKagamiTableData.sql")]
        DataTable SelectSeisanDenpyouKagamiEntity(T_SEISAN_DENPYOU_KAGAMI data);

        /// <summary>
        /// Entityで絞り込んで精算明細テーブルを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.SelectSeisanDetailTableData.sql")]
        DataTable SelectSeisanDetailEntity(T_SEISAN_DETAIL data);

        //取引先チェック===================================================
        /// <summary>
        /// 条件に合ったEntityを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.GetShimebiCheck.sql")]
        DataTable GetShimebiCheck(string torihikisakiCd, string shimebi);

        /// <summary>
        /// 条件に合ったEntityを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.CheckMishimeDate.sql")]
        DataTable CheckMishimeDate(string torihikisakiCd);

        /// <summary>
        /// 条件に合ったEntityを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.CheckShiharaData.sql")]
        DataTable CheckShiharaData(string shiharaCd, string shiharaDate, bool selectFlg);

        /// <summary>
        /// 再締精算データを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.GetSeisanSaishimeData.sql")]
        DataTable GetSeisanSaishimeData(SeikyuShimeShoriDto data);

        /// <summary>
        /// 再締精算データを削除
        /// </summary>
        /// <param name="seikyuuNumber"></param>
        /// <param name="deleteFlg"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.UpdateSeisanSaishimeiDeleteData.sql")]
        int UpdateSeisanSaishimeiDeleteData(Int64 seisanNumber, bool deleteFlg);


        /// <summary>
        /// 精算INXSアップロード状況チェック
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.CheckSeisanInxsUploadStatus.sql")]
        DataTable CheckSeisanInxsUploadStatus(Int64 seisanNumber);

        /// <summary>
        /// 最新請求書チェック（未来日の請求書があるか）
        /// </summary>
        /// <param name="torihikisakiCd"></param>
        /// <param name="seisanNumber"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.CheckLatestSeisanData.sql")]
        int CheckLatestSeisanData(string torihikisakiCd, Int64 seisanNumber);

        #region バーコード 160017
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.Barcode.GetUkeireData.sql")]
        DataTable BarcodeGetUkeireData(ShiharaiShimeShoriDispDto data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.Barcode.GetShukkaData.sql")]
        DataTable BarcodeGetShukkaData(ShiharaiShimeShoriDispDto data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.Barcode.GetUriageData.sql")]
        DataTable BarcodeGetUriageData(ShiharaiShimeShoriDispDto data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Sql("SELECT GETDATE() AS SYSTEM_DATE")]
        DateTime GetSystemDate(ShiharaiShimeShoriDispDto data);
        #endregion
    }

    [Bean(typeof(T_SEISAN_DENPYOU))]
    public interface SeisanDenpyouDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SEISAN_DENPYOU data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SEISAN_DENPYOU data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_SEISAN_DENPYOU data);

        /// <summary>
        /// Search
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.GetJikkouRirekiSearchData.sql")]
        DataTable Search(SaishimeSearchDTO data);
        //160020 S
        [Sql("SELECT * FROM T_SEISAN_DENPYOU WHERE SEISAN_NUMBER = /*SEISAN_NUMBER*/ AND DELETE_FLG = 0")]
        T_SEISAN_DENPYOU GetDataByKey(Int64 SEISAN_NUMBER);
        //160020 E
    }

    [Bean(typeof(T_SEISAN_DENPYOU_KAGAMI))]
    public interface SeisanDenpyouKagamiDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SEISAN_DENPYOU_KAGAMI data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SEISAN_DENPYOU_KAGAMI data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_SEISAN_DENPYOU_KAGAMI data);
    }

    [Bean(typeof(T_SEISAN_DETAIL))]
    public interface SeisanDetailDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SEISAN_DETAIL data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SEISAN_DETAIL data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_SEISAN_DETAIL data);
    }

    [Bean(typeof(T_SHIME_SHORI_CHUU))]
    public interface ShimeShoriChuuDao : IS2Dao
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
        [SqlFile("Shougun.Core.Adjustment.Shiharaishimesyori.Sql.DeleteShimeShoriChuu.sql")]
        int DesignateDelete(T_SHIME_SHORI_CHUU data);

        /// <summary>
        /// すべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT SHORI_KBN, CLIENT_COMPUTER_NAME, CLIENT_USER_NAME, CREATE_USER FROM T_SHIME_SHORI_CHUU")]
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