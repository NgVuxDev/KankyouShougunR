using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// �Ǝ҃}�X�^Dao
    /// </summary>
    [Bean(typeof(M_GYOUSHA))]
    public interface IM_GYOUSHADao : MasterAccess.Base.IMasterAccessDao<M_GYOUSHA>
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_GYOUSHA data);
        // 201400709 syunrei #947 ��19�@start
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int InsertGyosha(M_GYOUSHA data);
        // 201400709 syunrei #947 ��19�@end
        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_GYOUSHA data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_GYOUSHA data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM M_GYOUSHA")]
        M_GYOUSHA[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Gyousha.IM_GYOUSHADao_GetAllValidData.sql")]
        M_GYOUSHA[] GetAllValidData(M_GYOUSHA data);

        /// <summary>
        /// �R�[�h�����ɋƎ҃f�[�^���擾����
        /// </summary>
        /// <parameparam name="cd">�Ǝ҃R�[�h</parameparam>
        /// <returns>�擾�����f�[�^</returns>
        [Query("GYOUSHA_CD = /*cd*/")]
        M_GYOUSHA GetDataByCd(string cd);

        /// <summary>
        /// �Ǝ҃R�[�h�A�����R�[�h�����ɋƎ҃f�[�^���擾����
        /// </summary>
        /// <parameparam name="gyoushaCd">�Ǝ҃R�[�h</parameparam>
        /// <parameparam name="torihikisakiCd">�����R�[�h</parameparam>
        /// <returns>�擾�����f�[�^</returns>
        [Query("GYOUSHA_CD = /*gyoushaCd*/ and TORIHIKISAKI_CD = /*torihikisakiCd*/")]
        M_GYOUSHA GetDataByTorihikisakiCd(string gyoushaCd, string torihikisakiCd);

        /// <summary>
        /// �Ǝ҃R�[�h�����ɉ^�����Ǝ҂��擾����
        /// </summary>
        /// <parameparam name="cd">�Ǝ҃R�[�h</parameparam>
        /// <returns>�擾�����f�[�^</returns>
        [Query("GYOUSHA_CD = /*cd*/ and UNPAN_JUTAKUSHA = 1")]
        M_GYOUSHA GetUnpanJutakusha(string cd);

        /// <summary>
        /// �^���Ǝҏ��̎擾
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("r_framework.Dao.SqlFile.Gyousha.IM_GYOUSHADao_GetUnpanGyoushaData.sql")]
        M_GYOUSHA GetUnpanGyoushaData(M_GYOUSHA data);

        [Sql("select M_GYOUSHA.GYOUSHA_CD AS CD,M_GYOUSHA.GYOUSHA_NAME_RYAKU AS NAME FROM M_GYOUSHA /*$whereSql*/ group by M_GYOUSHA.GYOUSHA_CD,M_GYOUSHA.GYOUSHA_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_GYOUSHA data);

        /// <summary>
        /// �Ǝ҃R�[�h�̍ő�l���擾����
        /// </summary>
        /// <returns>�ő�l</returns>
        [Sql("SELECT ISNULL(MAX(GYOUSHA_CD),1) FROM M_GYOUSHA WHERE ISNUMERIC(GYOUSHA_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxKey();

        /// <summary>
        /// �Ǝ҃R�[�h�̍ŏ��l���擾����
        /// </summary>
        /// <returns>�ŏ��l</returns>
        [Sql("SELECT ISNULL(MIN(GYOUSHA_CD),1) FROM M_GYOUSHA WHERE ISNUMERIC(GYOUSHA_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMinKey();

        /// <summary>
        /// �Ǝ҃R�[�h�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(GYOUSHA_CD),0)+1 FROM M_GYOUSHA WHERE ISNUMERIC(GYOUSHA_CD + '.0e0') = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxPlusKey();

        /// <summary>
        /// �Ǝ҃R�[�h�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT GYOUSHA_CD FROM M_GYOUSHA WHERE SHOKUCHI_KBN = 1 and ISNUMERIC(GYOUSHA_CD) = 1 order by SHOKUCHI_KBN ASC")]
        M_GYOUSHA[] GetDateByChokuchiKbn1();

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_GYOUSHA data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// �Z���̈ꕔ�f�[�^���������@�\
        /// </summary>
        /// <param name="path">SQL�t�@�C���̃p�X</param>
        /// <param name="data">�Ǝ҃}�X�^�G���e�B�e�B</param>
        /// <param name="oldPost">���X�֔ԍ�</param>
        /// <param name="oldAddress">���Z��</param>
        /// <param name="newPost">�V�X�֔ԍ�</param>
        /// <param name="newAddress">�V�Z��</param>
        /// <returns></returns>
        int UpdatePartData(string path, M_GYOUSHA data, string oldPost, string oldAddress, string newPost, string newAddress);

        /// <summary>
        /// �Ǝ҃R�[�h�̍ŏ��̋󂫔Ԃ��擾����
        /// </summary>
        /// <param name="data">null��n��</param>
        /// <returns>�ŏ��̋󂫔�</returns>
        [SqlFile("r_framework.Dao.SqlFile.Gyousha.IM_GYOUSHADao_GetMinBlankNo.sql")]
        int GetMinBlankNo(M_GYOUSHA data);

        /// <summary>
        /// (��[����)�^���Ǝҏ��̎擾
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("r_framework.Dao.SqlFile.Gyousha.IM_GYOUSHADao_GetDainouUnpanGyoushaData.sql")]
        M_GYOUSHA GetDainouUnpanGyoushaData(M_GYOUSHA data);

        /// <summary>
        /// �Ǝ҂�CTI�A���f�[�^���擾����
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("r_framework.Dao.SqlFile.Gyousha.IM_GYOUSHADao_GetCtiRenkeiData.sql")]
        DataTable GetCtiRenkeiData(string tel, string gyousha, string selectType);

        // Begin: LANDUONG - 20220209 - refs#160050
        [Query("DELETE_FLG = 0 AND RAKURAKU_CUSTOMER_CD = /*code*/")]
        M_GYOUSHA[] GetDataByRakurakuCode(string code);
        // End: LANDUONG - 20220209 - refs#160050
    }
}