// $Id: IM_HIKIAI_GYOUSHADao.cs 12067 2013-12-19 11:21:15Z gai $
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.HikiaiGenbaHoshu.Dao
{
    /// <summary>
    /// �����Ǝ҃}�X�^Dao
    /// </summary>
    [Bean(typeof(M_HIKIAI_GYOUSHA))]
    public interface IM_HIKIAI_GYOUSHADao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM M_HIKIAI_GYOUSHA")]
        M_HIKIAI_GYOUSHA[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetHikiaiGyoushaAllValidData.sql")]
        M_HIKIAI_GYOUSHA[] GetAllValidData(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// �Ǝ҃R�[�h�̍ő�l���擾����
        /// </summary>
        /// <returns>�ő�l</returns>
        [Sql("SELECT ISNULL(MAX(GYOUSHA_CD), 1) FROM M_HIKIAI_GYOUSHA WHERE ISNUMERIC(GYOUSHA_CD) = 1 AND SHOKUCHI_KBN = 0")]
        int GetMaxKey();

        /// <summary>
        /// �Ǝ҃R�[�h�̍ŏ��l���擾����
        /// </summary>
        /// <returns>�ŏ��l</returns>
        [Sql("SELECT ISNULL(MIN(GYOUSHA_CD), 1) FROM M_HIKIAI_GYOUSHA WHERE ISNUMERIC(GYOUSHA_CD) = 1 AND SHOKUCHI_KBN = 0")]
        int GetMinKey();

        /// <summary>
        /// �Ǝ҃R�[�h�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(GYOUSHA_CD), 0) + 1 FROM M_HIKIAI_GYOUSHA WHERE ISNUMERIC(GYOUSHA_CD) = 1 AND SHOKUCHI_KBN = 0")]
        int GetMaxPlusKey();

        /// <summary>
        /// �Ǝ҃R�[�h�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT GYOUSHA_CD FROM M_HIKIAI_GYOUSHA WHERE SHOKUCHI_KBN = 1 AND ISNUMERIC(GYOUSHA_CD) = 1 ORDER BY SHOKUCHI_KBN ASC")]
        M_GYOUSHA[] GetDateByChokuchiKbn1();

        /// <summary>
        /// �Ǝ҃R�[�h�����Ƃɍ폜����Ă��Ȃ��Ǝ҂̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("GYOUSHA_CD = /*cd*/")]
        M_HIKIAI_GYOUSHA GetDataByCd(string cd);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}