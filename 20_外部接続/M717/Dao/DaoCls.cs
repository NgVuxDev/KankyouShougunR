using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.ExternalConnection.ClientIdNyuuryoku
{
    [Bean(typeof(M_DENSHI_KEIYAKU_CLIENT_ID))]
    public interface DaoCls : IS2Dao
    {
        [Sql("SELECT * FROM M_DENSHI_KEIYAKU_CLIENT_ID")]
        M_DENSHI_KEIYAKU_CLIENT_ID[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_KEIYAKU_CLIENT_ID data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_KEIYAKU_CLIENT_ID data);

        int Delete(M_DENSHI_KEIYAKU_CLIENT_ID data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("SHAIN_CD = /*cd*/")]
        M_DENSHI_KEIYAKU_CLIENT_ID GetDataByCd(string cd);

        /// <summary>
        /// �N���C�A���gID���͉�ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.ClientIdNyuuryoku.Sql.GetIchiranClientIdDataSql.sql")]
        DataTable GetIchiranClientIdDataSql(M_DENSHI_KEIYAKU_CLIENT_ID data, bool deletechuFlg);

        /// <summary>
        /// �N���C�A���gID���͉�ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ExternalConnection.ClientIdNyuuryoku.Sql.GetIchiranShainDataSql.sql")]
        DataTable GetIchiranShainDataSql(M_SHAIN data, bool deletechuFlg);
    }
}
