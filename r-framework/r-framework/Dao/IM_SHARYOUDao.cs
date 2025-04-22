using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Data.SqlTypes;
namespace r_framework.Dao
{
    [Bean(typeof(M_SHARYOU))]
    public interface IM_SHARYOUDao : MasterAccess.Base.IMasterAccessDao<M_SHARYOU>
    {

        [Sql("SELECT * FROM M_SHARYOU")]
        M_SHARYOU[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Sharyou.IM_SHARYOUDao_GetAllValidData.sql")]
        M_SHARYOU[] GetAllValidData(M_SHARYOU data);

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する(マスタ共通ポップアップ)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Sharyou.IM_SHARYOUDao_GetAllValidDataForPopUp.sql")]
        DataTable GetAllValidDataForPopUp(M_SHARYOU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SHARYOU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SHARYOU data);

        int Delete(M_SHARYOU data);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SHARYOU data);
        
        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">車輌CDの配列</param>
        /// <param name="data">運搬業者CD</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] SHARYOU_CD, string UNPAN_GYOUSHA_CD);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns>取得したデータ</returns>
        [Query("GYOUSHA_CD = /*data.GYOUSHA_CD*/ and SHARYOU_CD = /*data.SHARYOU_CD*/")]
        M_SHARYOU GetDataByCd(M_SHARYOU data);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_SHARYOU data, bool deletechuFlg);

        [Sql("select M_SHARYOU.SHARYOU_CD AS CD,M_SHARYOU.SHARYOU_NAME_RYAKU AS NAME FROM M_SHARYOU /*$whereSql*/ group by  M_SHARYOU.SHARYOU_CD,M_SHARYOU.SHARYOU_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。START
        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する(業者区分参照)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <parameparam name="GYOUSHAKBN">業者区分 1受入　2出荷 9条件にしない</parameparam>
        /// <parameparam name="TEKIYOU_DATE">画面日付 1受入　2出荷</parameparam>
        /// <parameparam name="UNPAN_JUTAKUSHA_KAISHA_KBN">TRUE場合は運搬業者場合</parameparam>
        /// <parameparam name="ISNOT_NEED_TEKIYOU_FLG">TRUE場合適用期間必要がない</parameparam>
        /// <parameparam name="GYOUSHAKBN_MANI">TRUE場合適用はマニ業者場合</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.Sharyou.IM_SHARYOUDao_GetAllValidDataForGyoushaKbn.sql")]
        M_SHARYOU[] GetAllValidDataForGyoushaKbn(M_SHARYOU data, string GYOUSHAKBN, SqlDateTime TEKIYOU_DATE, SqlBoolean UNPAN_JUTAKUSHA_KAISHA_KBN, SqlBoolean ISNOT_NEED_TEKIYOU_FLG, SqlBoolean GYOUSHAKBN_MANI);
        // 2017/06/09 DIQ 標準修正 #100072 車輌CDの手入力を行う際の条件として、業者区分も参照する。END

        //MOD NHU 20180508 S
        [SqlFile("r_framework.Dao.SqlFile.Sharyou.IM_SHARYOUDao_GetAllValidDataMod.sql")]
        M_SHARYOU[] GetAllValidDataMod(M_SHARYOU data);
        //MOD NHU 20180508 E
    }
}
