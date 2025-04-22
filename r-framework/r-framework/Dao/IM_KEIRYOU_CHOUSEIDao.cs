using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_KEIRYOU_CHOUSEI))]
    public interface IM_KEIRYOU_CHOUSEIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_KEIRYOU_CHOUSEI")]
        M_KEIRYOU_CHOUSEI[] GetAllData();
        
        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.KeiryouChousei.IM_KEIRYOU_CHOUSEIDao_GetAllValidData.sql")]
        M_KEIRYOU_CHOUSEI[] GetAllValidData(M_KEIRYOU_CHOUSEI data);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する(マスタ共通ポップアップ)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.KeiryouChousei.IM_KEIRYOU_CHOUSEIDao_GetAllValidDataForPopUp.sql")]
        DataTable GetAllValidDataForPopUp(M_KEIRYOU_CHOUSEI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KEIRYOU_CHOUSEI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KEIRYOU_CHOUSEI data);

        int Delete(M_KEIRYOU_CHOUSEI data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_KEIRYOU_CHOUSEI data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// 取得方法未定
        /// </summary>
        /// <param name="data"></param>
        /// <returns>取得したデータ</returns>
        [Query("TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/ and GYOUSHA_CD = /*data.GYOUSHA_CD*/ and GENBA_CD = /*data.GENBA_CD*/ and HINMEI_CD = /*data.HINMEI_CD*/ and UNIT_CD = /*data.UNIT_CD*/")]
        M_KEIRYOU_CHOUSEI GetDataByCd(M_KEIRYOU_CHOUSEI data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_KEIRYOU_CHOUSEI data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        [Sql("select M_KEIRYOU_CHOUSEI.TORIHIKISAKI_CD, M_KEIRYOU_CHOUSEI.GYOUSHA_CD FROM M_KEIRYOU_CHOUSEI /*$whereSql*/")]
        DataTable GetAllMasterDataForPopup(string whereSql);
    }
}
