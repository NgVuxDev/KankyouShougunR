// $Id: IM_HIKIAI_TORIHIKISAKIDao.cs 32052 2014-10-10 00:40:20Z y-hosokawa@takumi-sys.co.jp $
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao
{
    /// <summary>
    /// 引合取引先マスタDao
    /// </summary>
    [Bean(typeof(M_HIKIAI_TORIHIKISAKI))]
    public interface IM_HIKIAI_TORIHIKISAKIDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// 削除フラグがたっていないすべてのデータを取得する
        /// </summary>
        /// <returns>取得したデータのリスト</returns>
        [Sql("SELECT * FROM M_HIKIAI_TORIHIKISAKI")]
        M_HIKIAI_TORIHIKISAKI[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.GetAllValidData.sql")]
        M_HIKIAI_TORIHIKISAKI[] GetAllValidData(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// 取引先コードの最大値を取得する
        /// </summary>
        /// <returns>最大値</returns>
        [Sql("SELECT ISNULL(MAX(TORIHIKISAKI_CD), 1) FROM M_HIKIAI_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD) = 1 AND SHOKUCHI_KBN = 0")]
        int GetMaxKey();

        /// <summary>
        /// 取引先コードの最小値を取得する
        /// </summary>
        /// <returns>最小値</returns>
        [Sql("SELECT ISNULL(MIN(TORIHIKISAKI_CD), 1) FROM M_HIKIAI_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD) = 1 AND SHOKUCHI_KBN = 0")]
        int GetMinKey();

        /// <summary>
        /// 取引先コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT ISNULL(MAX(TORIHIKISAKI_CD), 0) + 1 FROM M_HIKIAI_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD) = 1 AND SHOKUCHI_KBN = 0")]
        int GetMaxPlusKey();

        /// <summary>
        /// 取引先コードの最小の空き番を取得する
        /// </summary>
        /// <param name="data">nullを渡す</param>
        /// <returns>最小の空き番</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.GetMinBlankNo.sql")]
        int GetMinBlankNo(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// 取引先コードの最大値+1を取得する
        /// </summary>
        /// <returns>最大値+1</returns>
        [Sql("SELECT TORIHIKISAKI_CD FROM M_HIKIAI_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD) = 1 AND SHOKUCHI_KBN = 1")]
        M_HIKIAI_TORIHIKISAKI[] GetDateByChokuchiKbn1();

        /// <summary>
        /// 取引先コードをもとに取引先のデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.GetTorihikisakiData.sql")]
        M_HIKIAI_TORIHIKISAKI GetTorihikisakiData(string torihikisakiCd);

        /// <summary>
        /// 取引先コードをもとに削除されていない取引先のデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_HIKIAI_TORIHIKISAKI GetDataByCd(string cd);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したSQL分</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// マスタ画面用の一覧データを取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_HIKIAI_TORIHIKISAKI data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        [Sql("SELECT M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_CD AS CD, M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU AS NAME" +
            " FROM M_HIKIAI_TORIHIKISAKI /*$whereSql*/" +
            " GROUP BY M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_CD, M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// 住所の一部データ書き換え機能
        /// </summary>
        /// <param name="path">SQLファイルのパス</param>
        /// <param name="data">取引先マスタエンティティ</param>
        /// <param name="oldPost">旧郵便番号</param>
        /// <param name="oldAddress">旧住所</param>
        /// <param name="newPost">新郵便番号</param>
        /// <param name="newAddress">新住所</param>
        /// <returns></returns>
        int UpdatePartData(string path, M_HIKIAI_TORIHIKISAKI data, string oldPost, string oldAddress, string newPost, string newAddress);

        /// <summary>
        /// 取引先コードをもとに取引先のデータを取得する
        /// </summary>
        /// <param name="data">Entity</param>  
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.GetInputCdDataHikiaiTorihikisakiSql.sql")]
        DataTable GetInputCdDataHikiaiTorihikisakiData(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// 取引先コードをもとに引合業者マスタのデータを取得する
        /// </summary>
        /// <param name="data">Entity</param>  
        /// <returns>取得したデータ</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.GetIchiranHikiaiGyoushaDataSql.sql")]
        DataTable GetIchiranHikiaiGyoushaData(M_HIKIAI_GYOUSHA data);

        // 2014007017 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする　start
        /// <summary>
        /// 移行なら、M_HIKIAI_GYOUSHAに関連データを更新
        /// </summary>
        /// <param name="oldGYOUSHA_CD">oldTORIHIKISAKI_CD</param>
        /// <param name="newGYOUSHA_CD">newTORIHIKISAKI_CD</param>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.UpdateGyoushaCD.sql")]
        bool UpdateGYOUSHA_CD(string oldTORIHIKISAKI_CD, string newTORIHIKISAKI_CD);

        /// <summary>
        /// 移行なら、M_HIKIAI_GENBAに関連データを更新
        /// </summary>
        /// <param name="oldGYOUSHA_CD">oldTORIHIKISAKI_CD</param>
        /// <param name="newGYOUSHA_CD">newTORIHIKISAKI_CD</param>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.UpdateGenbaCD.sql")]
        bool UpdateGenba_CD(string oldTORIHIKISAKI_CD, string newTORIHIKISAKI_CD);
        // 2014007017 chinchisi EV005238_[F1]移行する際に引合取引先・引合業者が登録されている場合はアラートを表示させ、以降させないようにする　end

        /// <summary>
        /// 取引先を使用している業者現場の適用開始日を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.GetTeikiyouBeginDateSql.sql")]
        DataTable GetTekiyouBegin(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// 取引先を使用している業者現場の適用終了日を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.GetTeikiyouEndDateSql.sql")]
        DataTable GetTekiyouEnd(M_HIKIAI_TORIHIKISAKI data);

        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.CheckDeleteHikiaiTorihikisakiSql.sql")]
        DataTable GetDataBySqlFileCheck(string TORIHIKISAKI_CD);
    }
}