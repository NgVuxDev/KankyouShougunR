using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// �����}�X�^��Dao�N���X
    /// </summary>
    [Bean(typeof(M_BUSHO))]
    public interface IM_BUSHODao : IS2Dao
    {
        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM M_BUSHO")]
        M_BUSHO[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Busho.IM_BUSHODao_GetAllValidData.sql")]
        M_BUSHO[] GetAllValidData(M_BUSHO data);

        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_BUSHO data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_BUSHO data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>  
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_BUSHO data);

        /// <summary>
        /// �_���폜�t���O�X�V�����i"DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC"�݂̂��X�V����j
        /// </summary>
        [PersistentProps("DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC")]
        int UpdateLogicalDeleteFlag(M_BUSHO data);

        [Sql("select M_BUSHO.BUSHO_CD as CD,M_BUSHO.BUSHO_NAME_RYAKU as NAME FROM M_BUSHO /*$whereSql*/ group by M_BUSHO.BUSHO_CD,M_BUSHO.BUSHO_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_BUSHO data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] BUSHO_CD);

        /// <summary>
        /// �����R�[�h�����Ƃɕ����̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("BUSHO_CD = /*cd*/")]
        M_BUSHO GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_BUSHO data, bool deletechuFlg);

    }
}