// $Id: IM_HIKIAI_GENBADao.cs 21035 2014-05-20 14:44:52Z gai $
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.HikiaiGenbaIchiran.Dao
{
    /// <summary>
    /// ��������}�X�^Dao
    /// </summary>
    [Bean(typeof(M_HIKIAI_GENBA))]
    public interface IM_HIKIAI_GENBADao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_HIKIAI_GENBA data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_HIKIAI_GENBA data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_HIKIAI_GENBA data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM M_HIKIAI_GENBA")]
        M_HIKIAI_GENBA[] GetAllData();

        /// <summary>
        /// ����R�[�h�����Ɍ�������擾
        /// </summary>
        /// <param name="data"></param>
        [Query("HIKIAI_GYOUSHA_USE_FLG = /*flg*/ AND GYOUSHA_CD = /*cd1*/ AND GENBA_CD = /*cd2*/")]
        M_HIKIAI_GENBA GetDataByCd(bool flg, string cd1, string cd2);

        /// <summary>
        /// ����R�[�h�����Ɍ�������擾
        /// </summary>
        /// <param name="data"></param>
        [Query("GENBA_CD = /*cd*/")]
        M_HIKIAI_GENBA[] GetDataByCdOnly(string cd);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}