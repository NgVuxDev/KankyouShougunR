using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuShoruiInfo
{
    [Bean(typeof(M_DENSHI_KEIYAKU_SHORUI_INFO))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// �S�f�[�^�擾
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Sql("SELECT * FROM M_DENSHI_KEIYAKU_SHORUI_INFO")]
        M_DENSHI_KEIYAKU_SHORUI_INFO[] GetAllData();

        /// <summary>
        /// �V�K�o�^
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_KEIYAKU_SHORUI_INFO data);

        /// <summary>
        /// �X�V
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC","TIME_STAMP")]
        int Update(M_DENSHI_KEIYAKU_SHORUI_INFO data);

        /// <summary>
        /// �폜
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        int Delete(M_DENSHI_KEIYAKU_SHORUI_INFO data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <param name="id">SHORUI_INFO_ID</param>
        /// <returns>�擾�����f�[�^</returns>
        [Query("SHORUI_INFO_ID = /*id*/")]
        M_DENSHI_KEIYAKU_SHORUI_INFO GetDataByCd(string id);

        /// <summary>
        /// ���ޏ����͉�ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <returns>�擾�����f�[�^</returns>
        [SqlFile("Shougun.Core.ExternalConnection.DenshiKeiyakuShoruiInfo.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_DENSHI_KEIYAKU_SHORUI_INFO data, bool deletechuFlg);
    }
}
