// $Id: IM_HIKIAI_GENBADao.cs 32015 2014-10-09 08:23:38Z y-hosokawa@takumi-sys.co.jp $
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.HikiaiGenbaHoshu.Dao
{
    /// <summary>
    /// ��������}�X�^Dao
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
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetHikiaiGenbaAllValidData.sql")]
        M_HIKIAI_GENBA[] GetAllValidData(M_HIKIAI_GENBA data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetHikiaiGenbaAllValidDataMinCols.sql")]
        M_HIKIAI_GENBA[] GetAllValidDataMinCols(M_HIKIAI_GENBA data);

        /// <summary>
        /// �Ǝ҃R�[�h�̍ő�l���擾����
        /// </summary>
        /// <returns>�ő�l</returns>
        [Sql("SELECT ISNULL(MAX(GENBA_CD), 1) FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 AND SHOKUCHI_KBN = 0")]
        int GetMaxKey();

        /// <summary>
        /// �Ǝ҃R�[�h�̍ŏ��l���擾����
        /// </summary>
        /// <returns>�ŏ��l</returns>
        [Sql("SELECT ISNULL(MIN(GENBA_CD), 1) FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 AND SHOKUCHI_KBN = 0")]
        int GetMinKey();

        /// <summary>
        /// �Ǝ҃R�[�h�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(GENBA_CD), 0) + 1 FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 AND SHOKUCHI_KBN = 0 AND GYOUSHA_CD = /*gyoushaCd*/ AND HIKIAI_GYOUSHA_USE_FLG = /*hikiaiFlg*/")]
        int GetMaxPlusKeyByGyoushaCd(string gyoushaCd, string hikiaiFlg);

        /// <summary>
        /// �Ǝ҃R�[�h�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT GENBA_CD FROM M_HIKIAI_GENBA WHERE ISNUMERIC(GENBA_CD) = 1 AND SHOKUCHI_KBN = 1 AND GYOUSHA_CD = /*gyoushaCd*/ AND HIKIAI_GYOUSHA_USE_FLG = /*hikiaiFlg*/")]
        M_HIKIAI_GENBA[] GetDataByShokuchiKbn1(string gyoushaCd, string hikiaiFlg);

        /// <summary>
        /// ����R�[�h�����Ɍ�������擾
        /// </summary>
        /// <param name="data"></param>
        [Query("HIKIAI_GYOUSHA_USE_FLG = /*data.HIKIAI_GYOUSHA_USE_FLG*/ AND GYOUSHA_CD = /*data.GYOUSHA_CD*/ AND GENBA_CD = /*data.GENBA_CD*/")]
        M_HIKIAI_GENBA GetDataByCd(M_HIKIAI_GENBA data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_HIKIAI_GENBA data);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// �����Ǝ҂Ɋ֘A����n��̃f�[�^�擾���s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>�擾����DataTable</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetChiikiDataSql.sql")]
        DataTable GetChiikiData(M_CHIIKI data);

        /// <summary>
        /// �����Ǝ҂Ɋ֘A����|�b�v�A�b�v�f�[�^�擾���s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>�擾����DataTable</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetPopupDataSql.sql")]
        DataTable GetPopupData(M_SHAIN data);

        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetHinmeiUriageShiharaiDataSql.sql")]
        DataTable SqlGetHinmeiUriageShiharaiData(M_KOBETSU_HINMEI data);

        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetHinmeiUriageShiharaiDataMinCols.sql")]
        DataTable SqlGetHinmeiUriageShiharaiDataMinCols(M_HINMEI data);

        // 201400709 syunrei #947 ��19 start
        /// <summary>
        /// �C�����[�h�ɂċƎ҃R�[�h���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetHikiaiGenbaData.sql")]
        M_HIKIAI_GENBA GetGenbaData(M_HIKIAI_GENBA data);

        /// <summary>
        /// ���ϓ���Data�iT_MITUMORI_ENTRY�j���X�V�s��
        /// </summary>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        /// <param name="newGYOUSHA_CD">newGYOUSHA_CD</param>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.UpdateMitsumoriEntryData.sql")]
        bool UpdateGYOUSHA_CD(string oldGENBA_CD, string newGENBA_CD, string oldGYOUSHA_CD);

        // 201400709 syunrei #947 ��19 end

        // 20140718 ria EV005242 ����������ڍs������Ƃ��A���������^�u�ƌ��ɏ��^�u�݈̂ڍs����Ȃ� start
        /// <summary>
        /// ����_������}�X�^�o�^�iM_GENBA_TEIKI_HINMEI�j
        /// </summary>
        /// <param name="oldGENBA_CD">oldGENBA_CD</param>
        /// <param name="newGENBA_CD">newGENBA_CD</param>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.InsertGenbaTeikiHinmeiData.sql")]
        [NoPersistentProps("TIME_STAMP")]
        int InsertTEIKI_HINMEI(string oldGENBA_CD, string newGENBA_CD, string oldGYOUSHA_CD);

        /// <summary>
        /// ����_���ɏ��iM_GENBA_TSUKI_HINMEI�j���X�V�s��
        /// </summary>
        /// <param name="oldGENBA_CD">oldGENBA_CD</param>
        /// <param name="newGENBA_CD">newGENBA_CD</param>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.InsertGenbaTsukiHinmeiData.sql")]
        [NoPersistentProps("TIME_STAMP")]
        int InsertTSUKI_HINMEI(string oldGENBA_CD, string newGENBA_CD, string oldGYOUSHA_CD);

        // 20140718 ria EV005242 ����������ڍs������Ƃ��A���������^�u�ƌ��ɏ��^�u�݈̂ڍs����Ȃ� end

        /// <summary>
        /// ����R�[�h�̍ŏ��̋󂫔Ԃ��擾����
        /// </summary>
        /// <param name="data">null��n��</param>
        /// <returns>�ŏ��̋󂫔�</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetMinBlankNo.sql")]
        int GetMinBlankNo(string gyoushaCd, string hikiaiFlg);
    }
}