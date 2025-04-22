using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(CHANGE_LOG_M_COURSE))]
    public interface ICHANGE_LOG_M_COURSEDao : IS2Dao
    {

        [Sql("SELECT * FROM CHANGE_LOG_M_COURSE")]
        CHANGE_LOG_M_COURSE[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(CHANGE_LOG_M_COURSE data);

        [NoPersistentProps("TIME_STAMP")]
        int Update(CHANGE_LOG_M_COURSE data);

        int Delete(CHANGE_LOG_M_COURSE data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, CHANGE_LOG_M_COURSE data);
    }
}
