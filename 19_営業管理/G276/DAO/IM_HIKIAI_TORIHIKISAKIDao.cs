using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO
{
    /// <summary>
    /// �����}�X�^Dao
    /// </summary>
    [Bean(typeof(M_HIKIAI_TORIHIKISAKI))]
    public interface IM_HIKIAI_TORIHIKISAKIDao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM M_HIKIAI_TORIHIKISAKI")]
        M_HIKIAI_TORIHIKISAKI[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.SqlFile.HikiaiTorihikisaki.IM_HIKIAI_TORIHIKISAKIDao_GetAllValidData.sql")]
        M_HIKIAI_TORIHIKISAKI[] GetAllValidData(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// �����R�[�h�̍ő�l���擾����
        /// </summary>
        /// <returns>�ő�l</returns>
        [Sql("SELECT ISNULL(MAX(TORIHIKISAKI_CD),1) FROM M_HIKIAI_TORIHIKISAKI  where ISNUMERIC(TORIHIKISAKI_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxKey();

        /// <summary>
        /// �����R�[�h�̍ŏ��l���擾����
        /// </summary>
        /// <returns>�ŏ��l</returns>
        [Sql("SELECT ISNULL(MIN(TORIHIKISAKI_CD),1) FROM M_HIKIAI_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMinKey();

        /// <summary>
        /// �����R�[�h�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(TORIHIKISAKI_CD),0)+1 FROM M_HIKIAI_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxPlusKey();

        /// <summary>
        /// �����R�[�h�̍ŏ��̋󂫔Ԃ��擾����
        /// </summary>
        /// <param name="data">null��n��</param>
        /// <returns>�ŏ��̋󂫔�</returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.SqlFile.Nyuukinsaki.IM_NYUUKINSAKIDao_GetMinBlankNo.sql")]
        int GetMinBlankNo(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// �����R�[�h�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT TORIHIKISAKI_CD FROM M_HIKIAI_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD) = 1 and SHOKUCHI_KBN = 1")]
        M_HIKIAI_TORIHIKISAKI[] GetDateByChokuchiKbn1();


        /// <summary>
        /// �����R�[�h�ő�l���擾
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetMaxTorihikisakiCode.sql")]
        string GetMaxTorihikisakiCode(M_HIKIAI_TORIHIKISAKI data);


        /// <summary>
        /// �����R�[�h�󂫔ԍ����擾
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetUselessTorihikisakiCode.sql")]
        string GetUselessTorihikisakiCode(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// �����R�[�h�����ƂɎ����̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.SqlFile.HikiaiTorihikisaki.IM_HIKIAI_TORIHIKISAKIDao_GetTorihikisakiData.sql")]
        M_HIKIAI_TORIHIKISAKI GetTorihikisakiData(string torihikisakiCd);

        /// <summary>
        /// �����R�[�h�����Ƃɍ폜����Ă��Ȃ������̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_HIKIAI_TORIHIKISAKI GetDataByCd(string cd);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetCustomDataGridViewDataSqlFile(string path, M_HIKIAI_TORIHIKISAKI data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        [Sql("select M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_CD AS CD,M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU AS NAME FROM M_HIKIAI_TORIHIKISAKI /*$whereSql*/ group by M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_CD,M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// �Z���̈ꕔ�f�[�^���������@�\
        /// </summary>
        /// <param name="path">SQL�t�@�C���̃p�X</param>
        /// <param name="data">�����}�X�^�G���e�B�e�B</param>
        /// <param name="oldPost">���X�֔ԍ�</param>
        /// <param name="oldAddress">���Z��</param>
        /// <param name="newPost">�V�X�֔ԍ�</param>
        /// <param name="newAddress">�V�Z��</param>
        /// <returns></returns>
        int UpdatePartData(string path, M_HIKIAI_TORIHIKISAKI data, string oldPost, string oldAddress, string newPost, string newAddress);
    }
}