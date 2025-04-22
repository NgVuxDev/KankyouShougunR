using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using r_framework.Dao;

namespace Shougun.Core.Master.ContenaShuruiHoshu.Dao
{
    [Bean(typeof(M_CONTENA_SHURUI))]
    public interface DaoCls : IS2Dao
    {

        [Sql("SELECT * FROM M_CONTENA_SHURUI")]
        M_CONTENA_SHURUI[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.ContenaShurui.IM_CONTENA_SHURUIDao_GetAllValidData.sql")]
        M_CONTENA_SHURUI[] GetAllValidData(M_CONTENA_SHURUI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CONTENA_SHURUI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC","TIME_STAMP")]
        int Update(M_CONTENA_SHURUI data);

        int Delete(M_CONTENA_SHURUI data);

        [Sql("select M_CONTENA_SHURUI.CONTENA_SHURUI_CD AS CD,M_CONTENA_SHURUI.CONTENA_SHURUI_NAME_RYAKU AS NAME FROM M_CONTENA_SHURUI /*$whereSql*/ group by  M_CONTENA_SHURUI.CONTENA_SHURUI_CD,M_CONTENA_SHURUI.CONTENA_SHURUI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_CONTENA_SHURUI data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("CONTENA_SHURUI_CD = /*cd*/")]
        M_CONTENA_SHURUI GetDataByCd(string cd);

        /// <summary>
        /// �R���e�i��މ�ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ContenaShuruiHoshu.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_CONTENA_SHURUI data,bool deletechuFlg);

        /// <summary>
        /// �R���e�i��މ�ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ContenaShuruiHoshu.Sql.CheckDeleteContenaSql.sql")]
        DataTable GetDataContena(string[] CONTENA_SHURUI_CD);
    }
}
