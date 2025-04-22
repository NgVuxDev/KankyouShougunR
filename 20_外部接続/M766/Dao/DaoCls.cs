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
        /// �V�X�e��ID�̔�
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT ISNULL(MAX(SYSTEM_ID),0)+1 FROM M_SMS_RECEIVER where ISNUMERIC(SYSTEM_ID) = 1 ")]
        int GetMaxPlusKey();

        /// <summary>
        /// �V�X�e��ID�����ƂɃf�[�^���i�荞��
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("SYSTEM_ID = /*systemId*/")]
        M_SMS_RECEIVER GetDataBySystemId(string systemId);

        /// <summary>
        /// �g�ѓd�b�ԍ������ƂɃf�[�^���i�荞��
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("MOBILE_PHONE_NUMBER = /*phoneNumber*/")]
        M_SMS_RECEIVER GetDataByPhoneNumber(string phoneNumber);

        /// <summary>
        /// �g�ѓd�b�ԍ������Ƃɑ��M���X�g����폜���s��
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [Query("MOBILE_PHONE_NUMBER = /*phoneNumber*/ AND DELETE_FLG = 0")]
        M_SMS_RECEIVER GetData_NotDelete(string phoneNumber);

        /// <summary>
        /// ���M���X�g����폜���s�����g�ѓd�b�ԍ��𼮰�ү���ގ�M�҃}�X�^���폜���s��
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        [Query("MOBILE_PHONE_NUMBER = /*phoneNumber*/ AND DELETE_FLG = 1")]
        M_SMS_RECEIVER GetData_Delete(string phoneNumber);

        /// <summary>
        /// ����ү���ގ�M�ғ��͉�ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="sql"></param>
        [Sql("SELECT * FROM M_SMS_RECEIVER WHERE /*$sql*/")]
        DataTable GetIchiranReceiverDataSql(string sql);

        /// <summary>
        /// ����ү���ގ�M�ғ��͉�ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        //[SqlFile("Shougun.Core.ExternalConnection.SmsReceiverNyuuryoku.Sql.GetIchiranPhoneNumberDataSql.sql")]
        //DataTable GetIchiranPhoneNumberDataSql(M_SMS_RECEIVER data, bool deletechuFlg);
        [Query("/*sql*/")]
        DataTable GetIchiranPhoneNumberDataSql(string sql);
    }
}
