using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// �Ǝ�}�X�^Dao
    /// </summary>
    [Bean(typeof(M_GYOUSHU))]
    public interface IM_GYOUSHUDao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_GYOUSHU data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_GYOUSHU data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_GYOUSHU data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM M_GYOUSHU")]
        M_GYOUSHU[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Gyoushu.IM_GYOUSHUDao_GetAllValidData.sql")]
        M_GYOUSHU[] GetAllValidData(M_GYOUSHU data);
        /// <summary>
        /// �R�[�h�����ɋƎ�f�[�^���擾����
        /// </summary>
        /// <parameparam name="cd">�Ǝ�R�[�h</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Query("GYOUSHU_CD = /*cd*/")]
        M_GYOUSHU GetDataByCd(string cd);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_GYOUSHU data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">�Ǝ�f�[�^</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] GYOUSHU_CD);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_GYOUSHU data, bool deletechuFlg);

        [Sql("select M_GYOUSHU.GYOUSHU_CD as CD,M_GYOUSHU.GYOUSHU_NAME_RYAKU as NAME FROM M_GYOUSHU /*$whereSql*/ group by M_GYOUSHU.GYOUSHU_CD,M_GYOUSHU.GYOUSHU_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);
    }
}