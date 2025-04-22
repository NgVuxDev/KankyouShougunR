using System.Collections.Generic;
using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
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
        /// ���Y�Ǝ҃}�X�^�������Ǝ҂���ڍs���ꂽ�ꍇ�́A�ڍs���̈����Ǝ҃}�X�^���擾
        /// </summary>
        /// <param name="gyoushaCdAfter"></param>
        /// <returns></returns>
        [Query("GYOUSHA_CD_AFTER = /*gyoushaCdAfter*/ AND DELETE_FLG = 1")]
        M_HIKIAI_GYOUSHA GetHikiaiGyousha(string gyoushaCdAfter);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        List<M_HIKIAI_GYOUSHA> GetHikiaiGyoushaList(M_HIKIAI_GYOUSHA entity);

        [Sql("UPDATE M_HIKIAI_GYOUSHA SET HIKIAI_TORIHIKISAKI_USE_FLG = 0, TORIHIKISAKI_CD = /*afterTorihikisakiCd*/ WHERE HIKIAI_TORIHIKISAKI_USE_FLG = 1 AND TORIHIKISAKI_CD = /*torihikisakiCd*/ AND DELETE_FLG = 0")]
        int UpdateHikiaiTorihikisakiCd(string torihikisakiCd, string afterTorihikisakiCd);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGyousha.IM_HIKIAI_GYOUSHADao_GetHikiaiGyoushaAllValidData.sql")]
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
        /// �����Ǝ҂Ɋ֘A�����������̃f�[�^�擾���s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>�擾����DataTable</returns>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGyousha.IM_HIKIAI_GYOUSHADao_GetIchiranGenbaDataSql.sql")]
        DataTable GetIchiranGenbaData(M_HIKIAI_GENBA data);

        /// <summary>
        /// �����Ǝ҂Ɋ֘A����n��̃f�[�^�擾���s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>�擾����DataTable</returns>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGyousha.IM_HIKIAI_GYOUSHADao_GetChiikiDataSql.sql")]
        DataTable GetChiikiData(M_CHIIKI data);

        /// <summary>
        /// �����Ǝ҂Ɋ֘A����|�b�v�A�b�v�f�[�^�擾���s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>�擾����DataTable</returns>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGyousha.IM_HIKIAI_GYOUSHADao_GetPopupDataSql.sql")]
        DataTable GetPopupData(M_SHAIN data);

        // 201400709 syunrei #947 ��19�@start
        /// <summary>
        /// �C�����[�h�ɂċƎ҃R�[�h���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGyousha.IM_HIKIAI_GYOUSHADao_GetHikiaiGyoushaData.sql")]
        M_HIKIAI_GYOUSHA GetGyoushaData(M_HIKIAI_GYOUSHA data);

        /// <summary>
        /// ���ϓ���Data�iT_MITUMORI_ENTRY�j���X�V�s��
        /// </summary>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        /// <param name="newGYOUSHA_CD">newGYOUSHA_CD</param>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGyousha.IM_HIKIAI_GYOUSHADao_UpdateMitsumoriEntryData.sql")]
        bool UpdateGYOUSHA_CD(string oldGYOUSHA_CD, string newGYOUSHA_CD);
        // 201400709 syunrei #947 ��19�@end

        // 2014007016 chinchisi EV005237_�������������������ɖ{�o�^(�ڍs)�������ɁA�����������g�p���Ă�������ƎҁE��������̎������{�o�^��ɕύX����@start
        /// <summary>
        /// �A�g�L���X�V
        /// </summary>
        /// <param name="oldGYOUSHA_CD">oldGYOUSHA_CD</param>
        /// <param name="newGYOUSHA_CD">newGYOUSHA_CD</param>
        [SqlFile("r-framework.Dao.SqlFile.HikiaiGyousha.IM_HIKIAI_GYOUSHADao_UpdateGyoushaCD.sql")]
        bool UpdateGYOUSHA_CD_AFTER(string oldGYOUSHA_CD, string newGYOUSHA_CD);
        // 2014007016 chinchisi EV005237_�������������������ɖ{�o�^(�ڍs)�������ɁA�����������g�p���Ă�������ƎҁE��������̎������{�o�^��ɕύX����@end
    }
}