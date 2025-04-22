// $Id: IM_HIKIAI_TORIHIKISAKIDao.cs 32052 2014-10-10 00:40:20Z y-hosokawa@takumi-sys.co.jp $
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao
{
    /// <summary>
    /// ���������}�X�^Dao
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
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.GetAllValidData.sql")]
        M_HIKIAI_TORIHIKISAKI[] GetAllValidData(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// �����R�[�h�̍ő�l���擾����
        /// </summary>
        /// <returns>�ő�l</returns>
        [Sql("SELECT ISNULL(MAX(TORIHIKISAKI_CD), 1) FROM M_HIKIAI_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD) = 1 AND SHOKUCHI_KBN = 0")]
        int GetMaxKey();

        /// <summary>
        /// �����R�[�h�̍ŏ��l���擾����
        /// </summary>
        /// <returns>�ŏ��l</returns>
        [Sql("SELECT ISNULL(MIN(TORIHIKISAKI_CD), 1) FROM M_HIKIAI_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD) = 1 AND SHOKUCHI_KBN = 0")]
        int GetMinKey();

        /// <summary>
        /// �����R�[�h�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(TORIHIKISAKI_CD), 0) + 1 FROM M_HIKIAI_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD) = 1 AND SHOKUCHI_KBN = 0")]
        int GetMaxPlusKey();

        /// <summary>
        /// �����R�[�h�̍ŏ��̋󂫔Ԃ��擾����
        /// </summary>
        /// <param name="data">null��n��</param>
        /// <returns>�ŏ��̋󂫔�</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.GetMinBlankNo.sql")]
        int GetMinBlankNo(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// �����R�[�h�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT TORIHIKISAKI_CD FROM M_HIKIAI_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD) = 1 AND SHOKUCHI_KBN = 1")]
        M_HIKIAI_TORIHIKISAKI[] GetDateByChokuchiKbn1();

        /// <summary>
        /// �����R�[�h�����ƂɎ����̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.GetTorihikisakiData.sql")]
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
        DataTable GetIchiranDataSqlFile(string path, M_HIKIAI_TORIHIKISAKI data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        [Sql("SELECT M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_CD AS CD, M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU AS NAME" +
            " FROM M_HIKIAI_TORIHIKISAKI /*$whereSql*/" +
            " GROUP BY M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_CD, M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_NAME_RYAKU")]
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

        /// <summary>
        /// �����R�[�h�����ƂɎ����̃f�[�^���擾����
        /// </summary>
        /// <param name="data">Entity</param>  
        /// <returns>�擾�����f�[�^</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.GetInputCdDataHikiaiTorihikisakiSql.sql")]
        DataTable GetInputCdDataHikiaiTorihikisakiData(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// �����R�[�h�����ƂɈ����Ǝ҃}�X�^�̃f�[�^���擾����
        /// </summary>
        /// <param name="data">Entity</param>  
        /// <returns>�擾�����f�[�^</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.GetIchiranHikiaiGyoushaDataSql.sql")]
        DataTable GetIchiranHikiaiGyoushaData(M_HIKIAI_GYOUSHA data);

        // 2014007017 chinchisi EV005238_[F1]�ڍs����ۂɈ��������E�����Ǝ҂��o�^����Ă���ꍇ�̓A���[�g��\�������A�ȍ~�����Ȃ��悤�ɂ���@start
        /// <summary>
        /// �ڍs�Ȃ�AM_HIKIAI_GYOUSHA�Ɋ֘A�f�[�^���X�V
        /// </summary>
        /// <param name="oldGYOUSHA_CD">oldTORIHIKISAKI_CD</param>
        /// <param name="newGYOUSHA_CD">newTORIHIKISAKI_CD</param>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.UpdateGyoushaCD.sql")]
        bool UpdateGYOUSHA_CD(string oldTORIHIKISAKI_CD, string newTORIHIKISAKI_CD);

        /// <summary>
        /// �ڍs�Ȃ�AM_HIKIAI_GENBA�Ɋ֘A�f�[�^���X�V
        /// </summary>
        /// <param name="oldGYOUSHA_CD">oldTORIHIKISAKI_CD</param>
        /// <param name="newGYOUSHA_CD">newTORIHIKISAKI_CD</param>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.UpdateGenbaCD.sql")]
        bool UpdateGenba_CD(string oldTORIHIKISAKI_CD, string newTORIHIKISAKI_CD);
        // 2014007017 chinchisi EV005238_[F1]�ڍs����ۂɈ��������E�����Ǝ҂��o�^����Ă���ꍇ�̓A���[�g��\�������A�ȍ~�����Ȃ��悤�ɂ���@end

        /// <summary>
        /// �������g�p���Ă���ƎҌ���̓K�p�J�n�����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.GetTeikiyouBeginDateSql.sql")]
        DataTable GetTekiyouBegin(M_HIKIAI_TORIHIKISAKI data);

        /// <summary>
        /// �������g�p���Ă���ƎҌ���̓K�p�I�������擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.GetTeikiyouEndDateSql.sql")]
        DataTable GetTekiyouEnd(M_HIKIAI_TORIHIKISAKI data);

        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.CheckDeleteHikiaiTorihikisakiSql.sql")]
        DataTable GetDataBySqlFileCheck(string TORIHIKISAKI_CD);
    }
}