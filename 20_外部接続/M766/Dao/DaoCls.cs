using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.ExternalConnection.SmsReceiverNyuuryoku
{
    [Bean(typeof(M_SMS_RECEIVER))]
    public interface DaoCls : IS2Dao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SMS_RECEIVER data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SMS_RECEIVER data);

        /// <summary>
        /// システムID採番
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT ISNULL(MAX(SYSTEM_ID),0)+1 FROM M_SMS_RECEIVER where ISNUMERIC(SYSTEM_ID) = 1 ")]
        int GetMaxPlusKey();

        /// <summary>
        /// システムIDをもとにデータを絞り込む
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("SYSTEM_ID = /*systemId*/")]
        M_SMS_RECEIVER GetDataBySystemId(string systemId);

        /// <summary>
        /// 携帯電話番号をもとにデータを絞り込む
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("MOBILE_PHONE_NUMBER = /*phoneNumber*/")]
        M_SMS_RECEIVER GetDataByPhoneNumber(string phoneNumber);

        /// <summary>
        /// 携帯電話番号をもとに送信リストから削除を行う
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [Query("MOBILE_PHONE_NUMBER = /*phoneNumber*/ AND DELETE_FLG = 0")]
        M_SMS_RECEIVER GetData_NotDelete(string phoneNumber);

        /// <summary>
        /// 送信リストから削除を行った携帯電話番号をｼｮｰﾄﾒｯｾｰｼﾞ受信者マスタより削除を行う
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [Query("MOBILE_PHONE_NUMBER = /*phoneNumber*/ AND DELETE_FLG = 1")]
        M_SMS_RECEIVER GetData_Delete(string phoneNumber);

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者入力画面用の一覧データを取得
        /// </summary>
        /// <param name="sql"></param>
        [Sql("SELECT * FROM M_SMS_RECEIVER WHERE /*$sql*/")]
        DataTable GetIchiranReceiverDataSql(string sql);

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者入力画面用の一覧データを取得
        /// </summary>
        //[SqlFile("Shougun.Core.ExternalConnection.SmsReceiverNyuuryoku.Sql.GetIchiranPhoneNumberDataSql.sql")]
        //DataTable GetIchiranPhoneNumberDataSql(M_SMS_RECEIVER data, bool deletechuFlg);
        [Query("/*sql*/")]
        DataTable GetIchiranPhoneNumberDataSql(string sql);
    }
}
