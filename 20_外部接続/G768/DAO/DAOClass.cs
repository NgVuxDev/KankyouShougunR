using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using Shougun.Core.ExternalConnection.SmsIchiran.DTO;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ExternalConnection.SmsIchiran.DAO
{
    [Bean(typeof(T_SMS))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_SMS data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_SMS data);

        /// <summary>
        /// SQLを実行してデータを取得する。
        /// </summary>
        /// <param name="sql"></param>
        [Sql("/*$sql*/")]
        DataTable GetDataTableSql(string sql);

        /// <summary>
        /// 受付伝票のSEQ最高値を取得する
        /// </summary>
        /// <param name="denpyou">対象伝票</param>
        /// <param name="denpyouNumber">伝票番号</param>
        /// <returns>SEQのMAX値</returns>
        [Sql("SELECT ISNULL(MAX(SEQ),1) FROM /*$denpyou*/ WHERE UKETSUKE_NUMBER = /*denpyouNumber*/")]
        string GetMaxUketsukeSeq(string denpyou, string denpyouNumber);

        /// <summary>
        /// 受付伝票のSEQ最高値を取得する（定期）
        /// </summary>
        /// <param name="denpyou">対象伝票</param>
        /// <param name="denpyouNumber">伝票番号</param>
        /// <returns>SEQのMAX値</returns>
        [Sql("SELECT ISNULL(MAX(SEQ),1) FROM /*$denpyou*/ WHERE TEIKI_HAISHA_NUMBER = /*teikiHaishaNumber*/")]
        string GetMaxTeikiSeq(string denpyou, string teikiHaishaNumber);

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞEntityを取得します
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM T_SMS WHERE SYSTEM_ID = /*systemId*/ AND DENPYOU_NUMBER = /*denpyouNumber*/")]
        T_SMS GetSearchSMSEntity(string systemId, string denpyouNumber);

        /// <summary>
        /// SYSTEM_ID採番
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT ISNULL(MAX(SYSTEM_ID),0)+1 FROM T_SMS")]
        string GetMaxSystemId();

        /// <summary>
        /// 伝票種類 = 1．収集
        /// </summary>
        /// <param name="table">対象テーブル名</param>
        /// <param name="data">検索用DTO</param>
        /// <returns name="DataTable">検索結果</returns>
        [SqlFile("Shougun.Core.ExternalConnection.SmsIchiran.Sql.GetIchiranSSData.sql")]
        DataTable GetIchiranSSDataSql(SearchDTO data);

        /// <summary>
        /// 伝票種類 = 2．出荷
        /// </summary>
        /// <param name="table">対象テーブル名</param>
        /// <param name="data">検索用DTO</param>
        /// <returns name="DataTable">検索結果</returns>
        [SqlFile("Shougun.Core.ExternalConnection.SmsIchiran.Sql.GetIchiranSKData.sql")]
        DataTable GetIchiranSKDataSql(SearchDTO data);

        /// <summary>
        /// 伝票種類 = 3．持込
        /// </summary>
        /// <param name="table">対象テーブル名</param>
        /// <param name="data">検索用DTO</param>
        /// <returns name="DataTable">検索結果</returns>
        [SqlFile("Shougun.Core.ExternalConnection.SmsIchiran.Sql.GetIchiranMKData.sql")]
        DataTable GetIchiranMKDataSql(SearchDTO data);

        /// <summary>
        /// 伝票種類 = 4．収集+出荷
        /// </summary>
        /// <param name="table">対象テーブル名</param>
        /// <param name="data">検索用DTO</param>
        /// <returns name="DataTable">検索結果</returns>
        [SqlFile("Shougun.Core.ExternalConnection.SmsIchiran.Sql.GetIchiranSS_SKData.sql")]
        DataTable GetIchiranSS_SKDataSql(SearchDTO data);

        /// <summary>
        /// 伝票種類 = 5．収集+持込
        /// </summary>
        /// <param name="table">対象テーブル名</param>
        /// <param name="data">検索用DTO</param>
        /// <returns name="DataTable">検索結果</returns>
        [SqlFile("Shougun.Core.ExternalConnection.SmsIchiran.Sql.GetIchiranSS_MKData.sql")]
        DataTable GetIchiranSS_MKDataSql(SearchDTO data);

        /// <summary>
        /// 伝票種類 = 6．定期
        /// </summary>
        /// <param name="table">対象テーブル名</param>
        /// <param name="data">検索用DTO</param>
        /// <returns name="DataTable">検索結果</returns>
        [SqlFile("Shougun.Core.ExternalConnection.SmsIchiran.Sql.GetIchiranTEIKIData.sql")]
        DataTable GetIchiranTEIKIDataSql(SearchDTO data);
    }
}
