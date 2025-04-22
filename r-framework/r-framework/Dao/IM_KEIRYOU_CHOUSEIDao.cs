using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_KEIRYOU_CHOUSEI))]
    public interface IM_KEIRYOU_CHOUSEIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_KEIRYOU_CHOUSEI")]
        M_KEIRYOU_CHOUSEI[] GetAllData();
        
        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.KeiryouChousei.IM_KEIRYOU_CHOUSEIDao_GetAllValidData.sql")]
        M_KEIRYOU_CHOUSEI[] GetAllValidData(M_KEIRYOU_CHOUSEI data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����(�}�X�^���ʃ|�b�v�A�b�v)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.KeiryouChousei.IM_KEIRYOU_CHOUSEIDao_GetAllValidDataForPopUp.sql")]
        DataTable GetAllValidDataForPopUp(M_KEIRYOU_CHOUSEI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KEIRYOU_CHOUSEI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KEIRYOU_CHOUSEI data);

        int Delete(M_KEIRYOU_CHOUSEI data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_KEIRYOU_CHOUSEI data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// �擾���@����
        /// </summary>
        /// <param name="data"></param>
        /// <returns>�擾�����f�[�^</returns>
        [Query("TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/ and GYOUSHA_CD = /*data.GYOUSHA_CD*/ and GENBA_CD = /*data.GENBA_CD*/ and HINMEI_CD = /*data.HINMEI_CD*/ and UNIT_CD = /*data.UNIT_CD*/")]
        M_KEIRYOU_CHOUSEI GetDataByCd(M_KEIRYOU_CHOUSEI data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_KEIRYOU_CHOUSEI data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        [Sql("select M_KEIRYOU_CHOUSEI.TORIHIKISAKI_CD, M_KEIRYOU_CHOUSEI.GYOUSHA_CD FROM M_KEIRYOU_CHOUSEI /*$whereSql*/")]
        DataTable GetAllMasterDataForPopup(string whereSql);
    }
}
