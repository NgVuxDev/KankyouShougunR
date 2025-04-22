using System.Data;
using System.Data.SqlTypes;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System;
namespace r_framework.Dao
{
    [Bean(typeof(M_KOBETSU_HINMEI_TANKA))]
    public interface IM_KOBETSU_HINMEI_TANKADao : IS2Dao
    {

        [Sql("SELECT * FROM M_KOBETSU_HINMEI_TANKA")]
        M_KOBETSU_HINMEI_TANKA[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.KobetsuHinmeiTanka.IM_KOBETSU_HINMEI_TANKADao_GetAllValidData.sql")]
        M_KOBETSU_HINMEI_TANKA[] GetAllValidData(M_KOBETSU_HINMEI_TANKA data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// ���K�p���ԂƂ̔�r�͌��ݓ��t�ł͂Ȃ������̓��t�Ɣ�r���܂�
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.KobetsuHinmeiTanka.IM_KOBETSU_HINMEI_TANKADao_GetAllValidDataSpecifyDate.sql")]
        M_KOBETSU_HINMEI_TANKA[] GetAllValidDataSpecifyDate(M_KOBETSU_HINMEI_TANKA data, SqlDateTime referenceDate);

        /// <summary>
        /// 
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.KobetsuHinmeiTanka.IM_KOBETSU_HINMEI_TANKADao_GetDataByHinmei.sql")]
        M_KOBETSU_HINMEI_TANKA GetDataByHinmei(M_KOBETSU_HINMEI_TANKA data);

        /// <summary>
        /// 
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.KobetsuHinmeiTanka.IM_KOBETSU_HINMEI_TANKADao_GetDataForHinmei.sql")]
        M_KOBETSU_HINMEI_TANKA GetDataForHinmei(M_KOBETSU_HINMEI_TANKA data, DateTime date);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KOBETSU_HINMEI_TANKA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KOBETSU_HINMEI_TANKA data);

        int Delete(M_KOBETSU_HINMEI_TANKA data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_KOBETSU_HINMEI_TANKA data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("SYS_ID = /*cd*/")]
        M_KOBETSU_HINMEI_TANKA GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_KOBETSU_HINMEI_TANKA data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg, bool syuruishiteiFlg, string syurui);

        /// <summary>
        /// �V�X�e��ID�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(SYS_ID),0)+1 FROM M_KOBETSU_HINMEI_TANKA where ISNUMERIC(SYS_ID) = 1")]
        long GetMaxPlusKey();

        /// <summary>
        /// �V�X�e��ID�̍ő�l���擾����
        /// </summary>
        /// <returns>�ő�l</returns>
        [Sql("SELECT ISNULL(MAX(SYS_ID),1) FROM M_KOBETSU_HINMEI_TANKA where ISNUMERIC(SYS_ID) = 1")]
        long GetMaxKey();

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
