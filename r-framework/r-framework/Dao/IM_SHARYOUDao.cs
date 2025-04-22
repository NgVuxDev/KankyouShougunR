using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Data.SqlTypes;
namespace r_framework.Dao
{
    [Bean(typeof(M_SHARYOU))]
    public interface IM_SHARYOUDao : MasterAccess.Base.IMasterAccessDao<M_SHARYOU>
    {

        [Sql("SELECT * FROM M_SHARYOU")]
        M_SHARYOU[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Sharyou.IM_SHARYOUDao_GetAllValidData.sql")]
        M_SHARYOU[] GetAllValidData(M_SHARYOU data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����(�}�X�^���ʃ|�b�v�A�b�v)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Sharyou.IM_SHARYOUDao_GetAllValidDataForPopUp.sql")]
        DataTable GetAllValidDataForPopUp(M_SHARYOU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SHARYOU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SHARYOU data);

        int Delete(M_SHARYOU data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SHARYOU data);
        
        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">���qCD�̔z��</param>
        /// <param name="data">�^���Ǝ�CD</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] SHARYOU_CD, string UNPAN_GYOUSHA_CD);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <param name="data"></param>
        /// <returns>�擾�����f�[�^</returns>
        [Query("GYOUSHA_CD = /*data.GYOUSHA_CD*/ and SHARYOU_CD = /*data.SHARYOU_CD*/")]
        M_SHARYOU GetDataByCd(M_SHARYOU data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_SHARYOU data, bool deletechuFlg);

        [Sql("select M_SHARYOU.SHARYOU_CD AS CD,M_SHARYOU.SHARYOU_NAME_RYAKU AS NAME FROM M_SHARYOU /*$whereSql*/ group by  M_SHARYOU.SHARYOU_CD,M_SHARYOU.SHARYOU_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        // 2017/06/09 DIQ �W���C�� #100072 ���qCD�̎���͂��s���ۂ̏����Ƃ��āA�Ǝҋ敪���Q�Ƃ���BSTART
        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����(�Ǝҋ敪�Q��)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <parameparam name="GYOUSHAKBN">�Ǝҋ敪 1����@2�o�� 9�����ɂ��Ȃ�</parameparam>
        /// <parameparam name="TEKIYOU_DATE">��ʓ��t 1����@2�o��</parameparam>
        /// <parameparam name="UNPAN_JUTAKUSHA_KAISHA_KBN">TRUE�ꍇ�͉^���Ǝҏꍇ</parameparam>
        /// <parameparam name="ISNOT_NEED_TEKIYOU_FLG">TRUE�ꍇ�K�p���ԕK�v���Ȃ�</parameparam>
        /// <parameparam name="GYOUSHAKBN_MANI">TRUE�ꍇ�K�p�̓}�j�Ǝҏꍇ</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Sharyou.IM_SHARYOUDao_GetAllValidDataForGyoushaKbn.sql")]
        M_SHARYOU[] GetAllValidDataForGyoushaKbn(M_SHARYOU data, string GYOUSHAKBN, SqlDateTime TEKIYOU_DATE, SqlBoolean UNPAN_JUTAKUSHA_KAISHA_KBN, SqlBoolean ISNOT_NEED_TEKIYOU_FLG, SqlBoolean GYOUSHAKBN_MANI);
        // 2017/06/09 DIQ �W���C�� #100072 ���qCD�̎���͂��s���ۂ̏����Ƃ��āA�Ǝҋ敪���Q�Ƃ���BEND

        //MOD NHU 20180508 S
        [SqlFile("r_framework.Dao.SqlFile.Sharyou.IM_SHARYOUDao_GetAllValidDataMod.sql")]
        M_SHARYOU[] GetAllValidDataMod(M_SHARYOU data);
        //MOD NHU 20180508 E
    }
}
