using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// WAN-Sign�����ڍ׏��
    /// </summary>
    [Bean(typeof(M_WANSIGN_KEIYAKU_INFO))]
    public interface IM_WANSIGN_KEIYAKU_INFODAO : IS2Dao
    {
        /// <summary>
        /// �S�f�[�^���擾����
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_WANSIGN_KEIYAKU_INFO")]
        M_WANSIGN_KEIYAKU_INFO[] GetAllData();

        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_WANSIGN_KEIYAKU_INFO data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_WANSIGN_KEIYAKU_INFO data);

        /// <summary>
        /// �V�X�e��ID�����ƂɃf�[�^���擾����
        /// </summary>
        /// <param name="systemId">�V�X�e��ID</param>
        /// <returns>�擾�����f�[�^</returns>
        [Query("WANSIGN_SYSTEM_ID = /*wanSignSystemId*/ AND DELETE_FLG = 0")]
        M_WANSIGN_KEIYAKU_INFO GetDataBySystemId(string wanSignSystemId);

        /// <summary>
        /// �g�����U�N�V����ID�����ƂɃf�[�^���擾����
        /// </summary>
        /// <param name="controlNumber">�֘A�R�[�h</param>
        /// <returns>�擾�����f�[�^</returns>
        [Query("CONTROL_NUMBER = /*controlNumber*/ AND DELETE_FLG = 0")]
        M_WANSIGN_KEIYAKU_INFO GetDataByControlNumber(string controlNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        DataTable getDateForStringSql(string sql);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT ISNULL(MAX(WANSIGN_SYSTEM_ID), 0) + 1 FROM M_WANSIGN_KEIYAKU_INFO")]
        long GetMaxPlusKey();

        /// <summary>
        /// �f�[�^���擾����
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_WANSIGN_KEIYAKU_INFO WHERE ORIGINAL_CONTROL_NUMBER = /*originalControlNumber*/")]
        M_WANSIGN_KEIYAKU_INFO[] GetDataByKanriBango(string originalControlNumber);

        /// <summary>
        /// �f�[�^���擾����
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_WANSIGN_KEIYAKU_INFO WHERE DOCUMENT_ID = /*documentId*/ ORDER BY CONTROL_NUMBER ASC")]
        M_WANSIGN_KEIYAKU_INFO[] GetDataByDocumentId(string documentId);

        //PhuocLoc 2022/03/08 #161248 -Start
        /// <summary>
        /// �f�[�^���擾����
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_WANSIGN_KEIYAKU_INFO WHERE DOCUMENT_ID <> /*documentId*/ AND ORIGINAL_CONTROL_NUMBER = /*originalControlNumber*/")]
        M_WANSIGN_KEIYAKU_INFO[] GetDataDuplicate(string documentId, string originalControlNumber);
        //PhuocLoc 2022/03/08 #161248 -End
    }
}