using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao
{
    /// <summary>
    /// ���ς���Dao
    /// </summary>
    [Bean(typeof(T_MITSUMORI_ENTRY))]
    public interface IT_MITSUMORI_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MITSUMORI_ENTRY data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MITSUMORI_ENTRY data);

        /// <summary>
        /// �����CD�R�[�h�����ƂɎ����_�������}�X�^�̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        T_MITSUMORI_ENTRY[] GetDataByCd(string cd);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="whereSql">���ς���f�[�^���X�V</param>
        /// <returns>0</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.UpdateMitsumoriEntryData.sql")]
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int UpdateTORIHIKISAKICD(string oldtorihikisakicd, string newtorihikisakicd);
    }
}