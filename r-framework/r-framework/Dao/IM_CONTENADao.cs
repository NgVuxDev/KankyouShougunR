using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_CONTENA))]
    public interface IM_CONTENADao : IS2Dao
    {

        [Sql("SELECT * FROM M_CONTENA")]
        M_CONTENA[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Contena.IM_CONTENADao_GetAllValidData.sql")]
        M_CONTENA[] GetAllValidData(M_CONTENA data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����(�}�X�^���ʃ|�b�v�A�b�v)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Contena.IM_CONTENADao_GetAllValidDataForPopUp.sql")]
        DataTable GetAllValidDataForPopUp(M_CONTENA data);

        int Insert(M_CONTENA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CONTENA data);

        int Delete(M_CONTENA data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, IM_CONTENADao data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <param name="contenaShuruiCd"></param>
        /// <param name="contenaCd"></param>
        /// <returns>�擾�����f�[�^</returns>
        [Query("CONTENA_SHURUI_CD = /*data.CONTENA_SHURUI_CD*/ and CONTENA_CD = /*data.CONTENA_CD*/")]
        M_CONTENA GetDataByCd(M_CONTENA data);

        [Sql("select M_CONTENA.CONTENA_CD as CD,M_CONTENA.CONTENA_NAME_RYAKU as NAME FROM M_CONTENA /*$whereSql*/ group by M_CONTENA.CONTENA_CD,M_CONTENA.CONTENA_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);
    }
}
