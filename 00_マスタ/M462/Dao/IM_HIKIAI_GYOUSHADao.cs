// $Id: IM_HIKIAI_GYOUSHADao.cs 31689 2014-10-06 09:30:11Z y-hosokawa@takumi-sys.co.jp $
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.HikiaiGyousha.Dao
{
    /// <summary>
    /// �����Ǝ҃}�X�^Dao
    /// </summary>
    [Bean(typeof(M_HIKIAI_GYOUSHA))]
    public interface IM_HIKIAI_GYOUSHADao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM M_HIKIAI_GYOUSHA")]
        M_HIKIAI_GYOUSHA[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.GetHikiaiGyoushaAllValidData.sql")]
        M_HIKIAI_GYOUSHA[] GetAllValidData(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// �Ǝ҃R�[�h�̍ő�l���擾����
        /// </summary>
        /// <returns>�ő�l</returns>
        [Sql("SELECT ISNULL(MAX(GYOUSHA_CD),1) FROM M_HIKIAI_GYOUSHA WHERE ISNUMERIC(GYOUSHA_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxKey();

        /// <summary>
        /// �Ǝ҃R�[�h�̍ŏ��l���擾����
        /// </summary>
        /// <returns>�ŏ��l</returns>
        [Sql("SELECT ISNULL(MIN(GYOUSHA_CD),1) FROM M_HIKIAI_GYOUSHA WHERE ISNUMERIC(GYOUSHA_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMinKey();

        /// <summary>
        /// �Ǝ҃R�[�h�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(GYOUSHA_CD),0)+1 FROM M_HIKIAI_GYOUSHA WHERE ISNUMERIC(GYOUSHA_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxPlusKey();

        /// <summary>
        /// �Ǝ҃R�[�h�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT GYOUSHA_CD FROM M_HIKIAI_GYOUSHA WHERE SHOKUCHI_KBN = 1 and ISNUMERIC(GYOUSHA_CD) = 1 order by SHOKUCHI_KBN ASC")]
        M_HIKIAI_GYOUSHA[] GetDateByChokuchiKbn1();

        /// <summary>
        /// �Ǝ҃R�[�h�����Ƃɍ폜����Ă��Ȃ��Ǝ҂̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("GYOUSHA_CD = /*cd*/")]
        M_HIKIAI_GYOUSHA GetDataByCd(string cd);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// �����Ǝ҂Ɋ֘A�����������̃f�[�^�擾���s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>�擾����DataTable</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.GetIchiranGenbaDataSql.sql")]
        DataTable GetIchiranGenbaData(M_HIKIAI_GENBA data);

        /// <summary>
        /// �����Ǝ҂Ɋ֘A����n��̃f�[�^�擾���s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>�擾����DataTable</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.GetChiikiDataSql.sql")]
        DataTable GetChiikiData(M_CHIIKI data);

        /// <summary>
        /// �����Ǝ҂Ɋ֘A����|�b�v�A�b�v�f�[�^�擾���s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>�擾����DataTable</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.GetPopupDataSql.sql")]
        DataTable GetPopupData(M_SHAIN data);

        // 201400709 syunrei #947 ��19�@start
        /// <summary>
        /// �C�����[�h�ɂċƎ҃R�[�h���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.GetHikiaiGyoushaData.sql")]
        M_HIKIAI_GYOUSHA GetGyoushaData(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// ���ϓ���Data�iT_MITUMORI_ENTRY�j���X�V�s��
        /// </summary>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        /// <param name="newGYOUSHA_CD">newGYOUSHA_CD</param>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.UpdateMitsumoriEntryData.sql")]
        bool UpdateGYOUSHA_CD(string oldGYOUSHA_CD, string newGYOUSHA_CD);
        // 201400709 syunrei #947 ��19�@end

        // 2014007016 chinchisi EV005237_�������������������ɖ{�o�^(�ڍs)�������ɁA�����������g�p���Ă�������ƎҁE��������̎������{�o�^��ɕύX����@start
        /// <summary>
        /// �A�g�L���X�V
        /// </summary>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        /// <param name="newGYOUSHA_CD">newGYOUSHA_CD</param>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.UpdateGyoushaCD.sql")]
        bool UpdateGYOUSHA_CD_AFTER(string oldGYOUSHA_CD, string newGYOUSHA_CD);
        // 2014007016 chinchisi EV005237_�������������������ɖ{�o�^(�ڍs)�������ɁA�����������g�p���Ă�������ƎҁE��������̎������{�o�^��ɕύX����@end

        /// <summary>
        /// �Ǝ҃R�[�h�̍ŏ��̋󂫔Ԃ��擾����
        /// </summary>
        /// <param name="data">null��n��</param>
        /// <returns>�ŏ��̋󂫔�</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.GetMinBlankNo.sql")]
        int GetMinBlankNo(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// �������g�p���Ă���ƎҌ���̓K�p�J�n�����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.GetTeikiyouBeginDateSql.sql")]
        DataTable GetTekiyouBegin(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// �������g�p���Ă���ƎҌ���̓K�p�I�������擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.GetTeikiyouEndDateSql.sql")]
        DataTable GetTekiyouEnd(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// ���}�X�^�Ŏg�p���ꂢ�Ă��邩�`�F�b�N����
        /// </summary>
        /// <param name="GYOUSHA_CD"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.HikiaiGyousha.Sql.CheckDeleteHikiaiGyoushaSql.sql")]
        DataTable GetDataBySqlFileCheck(string GYOUSHA_CD);
    }
}