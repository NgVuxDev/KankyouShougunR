using System;
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using Shougun.Core.Allocation.MobileShougunTorikomi.DTO;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Allocation.MobileShougunTorikomi.DAO
{
    [Bean(typeof(M_OUTPUT_PATTERN))]
    public interface MobileShougunTorikomiDAOClass : IS2Dao
    {
        /// <summary>
        /// モバイル将軍用データ取込画面専用テーブルの有効データ(DELETE_FLG=false)を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetYuukou.sql")]
        DataTable GetYuukouDataForEntity(MobileShougunTorikomiDTOClass data);

        /// <summary>
        /// Entityで絞り込んでモバイル将軍用データ取込画面専用テーブルを削除する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.deleteIchiran.sql")]
        DataTable GetDeleteMobileShougunDataForEntity(MobileShougunTorikomiDTOClass data);

        /// <summary>
        /// モバイル将軍用データ取込画面専用テーブルのシーケンシャルナンバーのMAX値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetMaxSeq.sql")]
        DataTable GetMaxSeqForEntity(MobileShougunTorikomiDTOClass data);

        /// <summary>
        /// モバイル将軍用データ取込画面専用テーブルの枝番のMAX値を取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetMaxEdaban.sql")]
        DataTable GetMaxEdabanForEntity(MobileShougunTorikomiDTOClass data);

        /// <summary>
        /// コース名称マスタを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetCourseNameData.sql")]
        DataTable GetCourseNameDataForEntity(MobileShougunTorikomiDTOClass data);

        /// <summary>
        /// コンテナ種類マスタを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetContenaShuruiData.sql")]
        DataTable GetContenaShuruiDataForEntity(ContenaShuruiDTOClass data);

        /// <summary>
        /// コンテナマスタを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetContenaData.sql")]
        DataTable GetContenaDataForEntity(ContenaDTOClass data);

        /// <summary>
        /// コンテナ稼動予定テーブルを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetContenaReserveData.sql")]
        DataTable GetContenaReserveDataForEntity(ContenaReserveDTOClass data);       

        /// <summary>
        /// モバイル将軍用データ取込画面専用テーブルのコンテナデータを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetMobileSyogunDataInsertContenaData.sql")]
        DataTable GetMobileSyogunDataInsertContenaDataForEntity(MobileShougunTorikomiDTOClass data);

        /// <summary>
        /// 現場データを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetGenbaData.sql")]
        DataTable GetGenbaDataForEntity(GenbaDTOClass data);

        /// <summary>
        /// 受付(収集)入力マスタを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetUketsukeSsEntryData.sql")]
		DataTable GetUketsukeSsEntryForEntity(UketsukeSsDTOClass data);

        /// <summary>
        /// 受付(収集)明細マスタを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetUketsukeSsDetailData.sql")]
        DataTable GetUketsukeSsDetailForEntity(UketsukeSsDTOClass data);

        /// <summary>
        /// コース_明細内訳マスタを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetCourseDetailItemsData.sql")]
        DataTable GetCourseDetailItemsForEntity(MobileShougunTorikomiDTOClass data);

        /// <summary>
        /// コンテナマスタを更新する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.SetContenaData.sql")]
        DataTable SetContenaForEntity(ContenaDTOClass data);

        /// <summary>
        /// 現場_定期品名マスタを取得する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetGenbaTeikiHinmeiData.sql")]
        DataTable GetGenbaTeikiHinmeiDataForEntity(MobileShougunTorikomiDTOClass data);

        /// <summary>品名マスタを取得する</summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetHinmeiData.sql")]
        DataTable GetHinmeiDataForEntity(MobileShougunTorikomiDTOClass data);

        // 20151021 katen #13337 品名手入力に関する機能修正 start
        /// <summary>品名マスタを取得する</summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetKobetsuHinmeiData.sql")]
        DataTable GetKobetsuHinmeiDataForEntity(MobileShougunTorikomiDTOClass data);
        // 20151021 katen #13337 品名手入力に関する機能修正 end

        /// <summary>車輌マスタ情報を取得する</summary>
        /// <param name="sharyouCD">業者CDを表す文字列</param>
		/// <param name="sharyouCD">車輌CDを表す文字列</param>
		/// <returns>データテーブル</returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetTeikiDispData.sql")]
        DataTable GetTeikiDispData(string gyoushaCD, string sharyouCD);

		/// <summary>
		/// 定期配車番号に紐付く配車詳細伝票の換算値を取得する
		/// </summary>
		/// <param name="data"></param>
		/// <returns>DataTable</returns>
		[SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetKansanData.sql")]
		DataTable GetKansanData(MobileShougunTorikomiDTOClass data);

        /// <summary>
        /// 社員の一覧を取得する
        /// (Deleteフラグや適用日の絞込みは行わない)
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_SHAIN")]
        DataTable GetAllShainData();

        /// <summary>
        /// 定期配車入力伝票を取得する
        /// </summary>
        /// <param name="haishaNum">配車伝票番号</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetTeikiHaishaEntry.sql")]
        DataTable GetTeikiHaishaEntry(Int64 haishaNum);

        /// <summary>
        /// 定期実績入力のソート用に定期配車入力伝票を取得する
        /// </summary>
        /// <param name="haishaNum">定期配車番号</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetTeikiSortData.sql")]
        DataTable GetTeikiSortData(Int64 haishaNum);

        /// <summary>
        /// 検索条件に紐付く、品名詳細を含む定期配車情報を取得
		/// </summary>
		/// <param name="data"></param>
		/// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetTeikiHinmeiInfo.sql")]
        DataTable GetTeikiHinmeiInfo(MobileShougunTorikomiDTOClass data);
    }

    /// <summary>
    /// モバイル将軍用データ取込画面専用テーブルを登録する
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    [Bean(typeof(T_MOBILE_SYOGUN_DATA_INSERT))]
    public interface SetMobileSyogunDataInsertDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MOBILE_SYOGUN_DATA_INSERT data);
    }

    /// <summary>
    /// 定期実績入力テーブルを登録する
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    [Bean(typeof(T_TEIKI_JISSEKI_ENTRY))]
    public interface SetTeikiJissekiEntryDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_JISSEKI_ENTRY data);
    }

    /// <summary>
    /// 定期実績明細テーブルを登録する
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    [Bean(typeof(T_TEIKI_JISSEKI_DETAIL))]
    public interface SetTeikiJissekiDetailDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_JISSEKI_DETAIL data);
    }

    /// <summary>
    /// 定期実績荷卸テーブルを登録する
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    [Bean(typeof(T_TEIKI_JISSEKI_NIOROSHI))]
    public interface SetTeikiJissekiNioroshiDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_JISSEKI_NIOROSHI data);
    }

    /// <summary>
    /// 売上_支払入力テーブルを登録する
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

        [SqlFile("Shougun.Core.Allocation.MobileShougunTorikomi.Sql.GetUriageShiharaiEntryData.sql")]
        DataTable GetUriageShiharaiEntryData(Int64 UKETSUKE_NUMBER);
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

    ///// <summary>
    ///// コンテナマスタを更新する
    ///// </summary>
    ///// <param name="date"></param>
    ///// <returns></returns>
    //[Bean(typeof(M_CONTENA))]
    //public interface SetContenaDao : IS2Dao
    //{
    //    /// <summary>
    //    /// Update
    //    /// 更新対象に含めない項目をNoPersistentPropsに記載
    //    /// </summary>
    //    /// <param name="data"></param>
    //    /// <returns></returns>
    //    [NoPersistentProps("CONTENA_NAME", "CONTENA_NAME_RYAKU", "GYOUSHA_CD", "SHARYOU_CD", "KOUNYUU_DATE", "LAST_SHUUFUKU_DATE", "CONTENA_BIKOU", "TEKIYOU_BEGIN", "TEKIYOU_END", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "DELETE_FLG", "TIME_STAMP")]
    //    int Update(M_CONTENA data);
    //}
}
