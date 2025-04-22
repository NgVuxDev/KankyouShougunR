// $Id: IT_URIAGE_MOTOCHODao.cs 40771 2015-01-27 10:39:28Z sanbongi $
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System;
using System.Data.SqlTypes;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Function.ShougunCSCommon.Dao
{
	/// <summary>
	/// 売上元帳Dao
	/// </summary>
	[Bean(typeof(M_TORIHIKISAKI))]
	public interface IT_URIAGE_MOTOCHODao : IS2Dao
	{
		/// <summary>
		/// 明細表示用一覧データテーブルの取得
		/// </summary>
		/// <param name="startCD">開始取引先CD</param>
		/// <param name="endCD">終了取引先CD</param>
		/// <param name="startDay">開始伝票日付</param>
		/// <param name="endDay">終了伝票日付</param>
		/// <param name="kakuteiKBN">システム確定登録単位区分</param>
		/// <param name="torihikiKBN">取引区分(元帳種類)</param>
		/// <param name="shimebi">締日</param>
		/// <param name="tyuusyutuKBN">抽出方法</param>
		/// <param name="QueryFlg">
		/// 前回残高と差引残高算出時でクエリの抽出条件をハンドリングするフラグです
		/// 前回残高計算時 = 1
		/// 差引残高算出時 = 0
		/// </param>
		/// <returns name="DataTable">データテーブル</returns>
		[SqlFile("Shougun.Function.ShougunCSCommon.Dao.SqlFile.UriageMotocho.IT_URIAGE_MOTOCHODao_GetIchiranData.sql")]
		DataTable GetIchiranData(string startCD, string endCD, string startDay, string endDay, int kakuteiKBN, int torihikiKBN, string shimebi, int tyuusyutuKBN, int QueryFlg);

		/// <summary>
		/// 明細表示用一覧データテーブルの取得(繰越残高計算用)
		/// </summary>
		/// <param name="startCD">開始取引先CD</param>
		/// <param name="endCD">終了取引先CD</param>
		/// <param name="startDay">開始伝票日付</param>
		/// <param name="kakuteiKBN">システム確定登録単位区分</param>
		/// <param name="torihikiKBN">取引区分(元帳種類)</param>
		/// <param name="shimebi">締日</param>
		/// <param name="tyuusyutuKBN">抽出方法</param>
		/// <returns name="DataTable">データテーブル</returns>
		[SqlFile("Shougun.Function.ShougunCSCommon.Dao.SqlFile.UriageMotocho.IT_URIAGE_MOTOCHODao_GetIchiranDataForMishimeData.sql")]
		DataTable GetIchiranDataForMishimeData(string startCD, string endCD, string startDay, int kakuteiKBN, int torihikiKBN, string shimebi, int tyuusyutuKBN);

		/// <summary>
		/// 指定された範囲の取引先Listを取得
		/// </summary>
		/// <param name="startCD">開始取引先CD</param>
		/// <param name="endCD">終了取引先CD</param>
		/// <param name="torihikiKBN">取引区分(元帳種類)</param>
		/// <param name="shimebi">締日</param>
        /// <param name="containsDeleteOutRange">適用期間外や削除済みも含めるか</param>
		/// <returns name="M_TORIHIKISAKI[]">取引先CDのリスト</returns>
		[SqlFile("Shougun.Function.ShougunCSCommon.Dao.SqlFile.UriageMotocho.IT_URIAGE_MOTOCHODao_GetTorihikisakiList.sql")]
		M_TORIHIKISAKI[] GetTorihikisakiList(string startCD, string endCD, int torihikiKBN, string shimebi, bool containsDeleteOutRange);

		/// <summary>
		/// 取引先毎の請求毎税一覧データテーブルの取得
		/// </summary>
		/// <param name="startCD">開始取引先CD</param>
		/// <param name="endCD">終了取引先CD</param>
		/// <param name="startDay">開始伝票日付</param>
		/// <param name="endDay">終了伝票日付</param>
		/// <returns name="DataTable">データテーブル</returns>
		[SqlFile("Shougun.Function.ShougunCSCommon.Dao.SqlFile.UriageMotocho.IT_URIAGE_MOTOCHODao_GetSeikyuuMaiZeiIchiran.sql")]
		DataTable GetSeikyuuMaiZeiIchiran(string torihikisakiCD, string startDay, string endDay);

		/// <summary>
		/// 指定された取引先CDの開始伝票日付より直近の請求データから差引残高、締実行日を取得
		/// </summary>
		/// <param name="torihikisakiCD">取引先CD</param>
		/// <param name="startDay">開始伝票日付</param>
		/// <returns name="DataTable">データテーブル</returns>
		[SqlFile("Shougun.Function.ShougunCSCommon.Dao.SqlFile.UriageMotocho.IT_URIAGE_MOTOCHODao_GetRecentSeikyuuZandaka.sql")]
		DataTable GetRecentSeikyuuZandaka(string torihikisakiCD, string startDay);

        /// <summary>
        /// 指定された年月の売上月次処理、売上月次調整データを取得
        /// </summary>
        /// <param name="startCD">開始取引先CD</param>
        /// <param name="endCD">終了取引先CD</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        [SqlFile("Shougun.Function.ShougunCSCommon.Dao.SqlFile.UriageMotocho.IT_URIAGE_MOTOCHODao_GetMonthlyData.sql")]
        DataTable GetMonthlyData(string startCD, string endCD, int year, int month, string shimebi);

        /// <summary>
        /// 取引先マスタテーブルの取得
        /// </summary>
        /// <param name="startCD">開始取引先CD</param>
        /// <param name="endCD">終了取引先CD</param>
        /// <returns></returns>
        [SqlFile("Shougun.Function.ShougunCSCommon.Dao.SqlFile.UriageMotocho.IT_URIAGE_MOTOCHODao_GetTorihikisakiSeikyuList.sql")]
        DataTable GetTorihikisakiSeikyuList(string startCD, string endCD, string shimebi);

        /// <summary>
        /// 指定された年月の売上締処理、売上締調整データを取得
        /// </summary>
        /// <param name="startCD">開始取引先CD</param>
        /// <param name="endCD">終了取引先CD</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        [SqlFile("Shougun.Function.ShougunCSCommon.Dao.SqlFile.UriageMotocho.IT_URIAGE_MOTOCHODao_GetSeikyuuMonthlyData.sql")]
        DataTable GetSeikyuuMonthlyData(string startCD, string endCD, string startDay, string shimebi);

        /// <summary>
        /// 指定された年月の売上月次処理、売上月次調整データを取得
        /// </summary>
        /// <param name="startCD">開始取引先CD</param>
        /// <param name="endCD">終了取引先CD</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        [SqlFile("Shougun.Function.ShougunCSCommon.Dao.SqlFile.UriageMotocho.IT_URIAGE_MOTOCHODao_GetAdjustTaxData.sql")]
        DataTable GetMonthlyAdjustTaxData(string startCD, string endCD, int fromYear, int toYear, int fromMonth, int toMonth);
    }
}