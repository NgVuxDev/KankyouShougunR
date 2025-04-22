using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    [Bean(typeof(M_SMS_RECEIVER))]
    public interface IM_SMS_RECEIVERDao : IchiranBaseDao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SMS_RECEIVER data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SMS_RECEIVER data);

        int Delete(M_SMS_RECEIVER data);

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
        /// 携帯電話番号をもとにデータを取得
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [Query("MOBILE_PHONE_NUMBER = /*phoneNumber*/")]
        M_SMS_RECEIVER GetDataByPhoneNumber(string phoneNumber);

        /// <summary>
        /// 送信リストから削除を行った携帯電話番号をｼｮｰﾄﾒｯｾｰｼﾞ受信者マスタより削除を行う
        /// </summary>
        /// <param name="deletePhoneNumber"></param>
        /// <returns></returns>
        [Query("MOBILE_PHONE_NUMBER = /*deletePhoneNumber*/ AND DELETE_FLG = 1")]
        M_SMS_RECEIVER GetData_Delete(string deletePhoneNumber);

        [Sql("SELECT (CASE WHEN /*entity.RENKEI_FLG*/ = 1 AND /*entity.DELETE_FLG*/ = 0 THEN '連携済' WHEN /*entity.RENKEI_FLG*/ = 0 AND /*entity.DELETE_FLG*/ = 0 THEN '送信待' ELSE '' END) AS STATUS FROM M_SMS_RECEIVER WHERE SYSTEM_ID = /*entity.SYSTEM_ID*/")]
        DataTable GetStatus(M_SMS_RECEIVER entity);
    }
}
