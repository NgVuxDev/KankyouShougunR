using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System;

namespace r_framework.Dao
{
    /// <summary>
    /// ����Ń}�X�^Dao
    /// </summary>
    [Bean(typeof(M_SHOUHIZEI))]
    public interface IM_SHOUHIZEIDao : IS2Dao
    {
        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM M_SHOUHIZEI")]
        M_SHOUHIZEI[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Shouhizei.IM_SHOUHIZEIDao_GetAllValidData.sql")]
        M_SHOUHIZEI[] GetAllValidData(M_SHOUHIZEI data);

        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SHOUHIZEI data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SHOUHIZEI data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_SHOUHIZEI data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SHOUHIZEI data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("SYS_ID = /*cd*/")]
        M_SHOUHIZEI GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_SHOUHIZEI data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        /// <summary>
        /// �V�X�e��ID�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(SYS_ID),0)+1 FROM M_SHOUHIZEI where ISNUMERIC(SYS_ID) = 1")]
        short GetMaxPlusKey();

        /// <summary>
        /// �V�X�e��ID�̍ő�l���擾����
        /// </summary>
        /// <returns>�ő�l</returns>
        [Sql("SELECT ISNULL(MAX(SYS_ID),1) FROM M_SHOUHIZEI where ISNUMERIC(SYS_ID) = 1")]
        short GetMaxKey();


        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="date">DateTime</param>
        /// <returns></returns>
        [SqlFile("r_framework.Dao.SqlFile.Shouhizei.IT_IM_SHOUHIZEIDao_GetDataBySqlFile.sql")]
        M_SHOUHIZEI GetDataByDate(DateTime date);

    }
}