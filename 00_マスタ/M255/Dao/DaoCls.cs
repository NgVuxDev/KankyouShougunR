using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using r_framework.Dao;

namespace Shougun.Core.Master.ManiFestTeHaiHoshu.Dao
{
    [Bean(typeof(M_MANIFEST_TEHAI))]
    internal interface DaoCls : IS2Dao
    {

        [Sql("SELECT * FROM M_MANIFEST_TEHAI")]
        M_MANIFEST_TEHAI[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.ManifestTehai.IM_MANIFEST_TEHAIDao_GetAllValidData.sql")]
        M_MANIFEST_TEHAI[] GetAllValidData(M_MANIFEST_TEHAI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_MANIFEST_TEHAI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC","TIME_STAMP")]
        int Update(M_MANIFEST_TEHAI data);

        int Delete(M_MANIFEST_TEHAI data);

        [Sql("select M_MANIFEST_TEHAI.MANIFEST_TEHAI_CD AS CD,M_MANIFEST_TEHAI.MANIFEST_TEHAI_NAME_RYAKU AS NAME FROM M_MANIFEST_TEHAI /*$whereSql*/ group by  M_MANIFEST_TEHAI.MANIFEST_TEHAI_CD,M_MANIFEST_TEHAI.MANIFEST_TEHAI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_MANIFEST_TEHAI data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("MANIFEST_TEHAI_CD = /*cd*/")]
        M_MANIFEST_TEHAI GetDataByCd(string cd);

        /// <summary>
        /// �R���e�i��މ�ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ManiFestTeHaiHoshu.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_MANIFEST_TEHAI data, bool deletechuFlg);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">�}�j�t�F�X�g��z</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ManiFestTeHaiHoshu.Sql.CheckDeleteManiTehai.sql")]
        DataTable GetDataBySqlFileCheck(string[] MANIFEST_TEHAI_CD);
    }
}
