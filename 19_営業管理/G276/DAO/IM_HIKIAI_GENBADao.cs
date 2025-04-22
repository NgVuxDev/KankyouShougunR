using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO
{
    /// <summary>
    /// ����}�X�^Dao
    /// </summary>
    [Bean(typeof(M_HIKIAI_GENBA))]
    public interface IM_HIKIAI_GENBADao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_HIKIAI_GENBA data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_HIKIAI_GENBA data);

        /// <summary>
        /// ����R�[�h�ő�l���擾
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetMaxGenbaCode.sql")]
        string GetMaxGenbaCode(M_HIKIAI_GENBA data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_HIKIAI_GENBA data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM M_HIKIAI_GENBA")]
        M_HIKIAI_GENBA[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.SqlFile.HikiaiGenba.IM_HIKIAI_GENBADao_GetAllValidData.sql")]
        M_HIKIAI_GENBA[] GetAllValidData(M_HIKIAI_GENBA data);

        /// <summary>
        /// �ڋq�ꗗ�\���p�f�[�^�擾
        /// </summary>
        /// <parameparam name="searchString">��������</parameparam>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.SqlFile.HikiaiGenba.IM_HIKIAI_GENBADao_GetKokyakuCustomDataGridViewData.sql")]
        new DataTable GetCustomDataGridViewData(string searchString);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <parameparam name="sql">��������</parameparam>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.SqlFile.HikiaiGenba.IM_HIKIAI_GENBADao_GetUserSettingData.sql")]
        new DataTable GetUserSettingData(string sql);

        /// <summary>
        /// �ڋq�}�X�^�f�[�^�擾
        /// </summary>
        /// <parameparam name="gyoushaCD">�Ǝ҃R�[�h</parameparam>
        /// <parameparam name="genbaCD">����R�[�h</parameparam>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.SqlFile.HikiaiGenba.IM_HIKIAI_GENBADao_GetKokyakuMasterData.sql")]
        DataTable GetKokyakuMasterData(string gyoushaCD, string genbaCD);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����(�}�X�^���ʃ|�b�v�A�b�v)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.DAO.SqlFile.HikiaiGenba.IM_HIKIAI_GENBADao_GetAllValidDataForPopUp.sql")]
        DataTable GetAllValidDataForPopUp(M_HIKIAI_GENBA data);

        /// <summary>
        /// ����R�[�h�����Ɍ�������擾
        /// </summary>
        /// <param name="data"></param>
        [Query("GYOUSHA_CD = /*data.GYOUSHA_CD*/ and GENBA_CD = /*data.GENBA_CD*/")]
        M_HIKIAI_GENBA GetDataByCd(M_HIKIAI_GENBA data);

        /// <summary>
        /// �����R�[�h�A�Ǝ҃R�[�h�A����R�[�h���w�肵�}�X�^�f�[�^���擾
        /// </summary>
        /// <parameparam name="torihikisakiCd">�����R�[�h</parameparam>
        /// <parameparam name="gyoushaCd">�Ǝ҃R�[�h</parameparam>
        /// <parameparam name="genbaCd">����R�[�h</parameparam>
        [Query("TORIHIKISAKI_CD = /*torihikisakiCd*/ and GYOUSHA_CD = /*gyoushaCd*/ and GENBA_CD = /*genbaCd*/")]
        M_HIKIAI_GENBA GetGenbaData(string torihikisakiCd, string gyoushaCd, string genbaCd);

        /// <summary>
        /// ����R�[�h�����ɍ폜����Ă��Ȃ������擾
        /// </summary>
        /// <parameparam name="genbaCd">����R�[�h</parameparam>
        [Query("GENBA_CD = /*genbaCd*/ and SHOBUN_JIGYOUJOU = 1")]
        M_HIKIAI_GENBA GetShobunjigyousya(string genbaCd);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_HIKIAI_GENBA data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾(�Ǝ҃G���e�B�e�B�t)
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="gyousha">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileWithGyousha(string path, M_HIKIAI_GENBA data, M_HIKIAI_GYOUSHA gyousha);

        /// <summary>
        /// �Ǝ҃R�[�h�̍ő�l���擾����
        /// </summary>
        /// <returns>�ő�l</returns>
        [Sql("SELECT ISNULL(MAX(GENBA_CD),1) FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxKey();

        /// <summary>
        /// ����R�[�h�󂫔ԍ����擾
        /// </summary>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.MitsumoriNyuryoku.Sql.GetUselessGenbaCode.sql")]
        string GetUselessGenbaCode(M_HIKIAI_GENBA data);


        /// <summary>
        /// �Ǝ҃R�[�h�̍ŏ��l���擾����
        /// </summary>
        /// <returns>�ŏ��l</returns>
        [Sql("SELECT ISNULL(MIN(GENBA_CD),1) FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMinKey();

        ///// <summary>
        ///// �Ǝ҃R�[�h�̍ő�l+1���擾����
        ///// </summary>
        ///// <returns>�ő�l+1</returns>
        //[Sql("SELECT ISNULL(MAX(GENBA_CD),0)+1 FROM M_HIKIAI_GENBA WHERE SHOKUCHI_KBN = 0")]
        //int GetMaxPlusKey();

        /// <summary>
        /// �Ǝ҃R�[�h�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(GENBA_CD),0)+1 FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 and SHOKUCHI_KBN = 0 and GYOUSHA_CD = /*gyoushaCd*/")]
        int GetMaxPlusKeyByGyoushaCd(string gyoushaCd);

        /// <summary>
        /// �Ǝ҃R�[�h�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT GENBA_CD FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 and SHOKUCHI_KBN = 1 and GYOUSHA_CD = /*gyoushaCd*/")]
        M_HIKIAI_GENBA[] GetDataByShokuchiKbn1(string gyoushaCd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetCustomDataGridViewDataSqlFile(string path, M_HIKIAI_GENBA data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        [Sql("select M_HIKIAI_GENBA.GENBA_CD as CD,M_HIKIAI_GENBA.GENBA_NAME_RYAKU as NAME FROM M_HIKIAI_GENBA /*$whereSql*/ group by M_HIKIAI_GENBA.GENBA_CD,M_HIKIAI_GENBA.GENBA_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

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
        /// <param name="data">����}�X�^�G���e�B�e�B</param>
        /// <param name="oldPost">���X�֔ԍ�</param>
        /// <param name="oldAddress">���Z��</param>
        /// <param name="newPost">�V�X�֔ԍ�</param>
        /// <param name="newAddress">�V�Z��</param>
        /// <returns></returns>
        int UpdatePartData(string path, M_HIKIAI_GENBA data, string oldPost, string oldAddress, string newPost, string newAddress);
    }
}