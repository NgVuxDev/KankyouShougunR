using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Data.SqlTypes;
namespace r_framework.Dao
{
    [Bean(typeof(M_KIHON_HINMEI_TANKA))]
    public interface IM_KIHON_HINMEI_TANKADao : IS2Dao
    {

        [Sql("SELECT * FROM M_KIHON_HINMEI_TANKA")]
        M_KIHON_HINMEI_TANKA[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.KihonHinmeiTanka.IM_KIHON_HINMEI_TANKADao_GetAllValidData.sql")]
        M_KIHON_HINMEI_TANKA[] GetAllValidData(M_KIHON_HINMEI_TANKA data);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// ※適用期間との比較は現在日付ではなく引数の日付と比較します
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.KihonHinmeiTanka.IM_KIHON_HINMEI_TANKADao_GetAllValidDataSpecifyDate.sql")]
        M_KIHON_HINMEI_TANKA[] GetAllValidDataSpecifyDate(M_KIHON_HINMEI_TANKA data, SqlDateTime referenceDate);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KIHON_HINMEI_TANKA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KIHON_HINMEI_TANKA data);

        int Delete(M_KIHON_HINMEI_TANKA data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_KIHON_HINMEI_TANKA data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("SYS_ID = /*cd*/")]
        M_KIHON_HINMEI_TANKA GetDataByCd(string cd);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_KIHON_HINMEI_TANKA data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg, bool syuruishiteiFlg, string syurui);

        /// <summary>
        /// システムIDの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(SYS_ID),0)+1 FROM M_KIHON_HINMEI_TANKA where ISNUMERIC(SYS_ID) = 1")]
        long GetMaxPlusKey();

        /// <summary>
        /// システムIDの最大値を取得する
        /// </summary>
        /// <returns>最大値</returns>
        [Sql("SELECT ISNULL(MAX(SYS_ID),1) FROM M_KIHON_HINMEI_TANKA where ISNUMERIC(SYS_ID) = 1")]
        long GetMaxKey();
    }
}
