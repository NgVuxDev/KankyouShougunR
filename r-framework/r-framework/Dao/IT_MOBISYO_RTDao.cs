using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(T_MOBISYO_RT))]
    public interface IT_MOBISYO_RTDao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_MOBISYO_RT data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_MOBISYO_RT data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM T_MOBISYO_RT")]
        T_MOBISYO_RT[] GetAllData();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="haishaKbn"></param>
        /// <param name="haishaDenpyouNo"></param>
        /// <returns></returns>
        [SqlFile("r_framework.Dao.SqlFile.MobisyoRt.GetRenkeiData.sql")]
        DataTable GetRenkeiData(string haishaKbn, string haishaDenpyouNo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="haishaDenpyouNo"></param>
        /// <returns></returns>
        [SqlFile("r_framework.Dao.SqlFile.MobisyoRt.GetRenkeiDataForTeikiHaisha.sql")]
        DataTable GetRenkeiDataForTeikiHaisha(string haishaDenpyouNo, string rowNo);
    }
}
