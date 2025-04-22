using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Collections.Generic;
namespace r_framework.Dao
{
    /// <summary>
    /// �X�֎����}�X�^Dao
    /// </summary>
    [Bean(typeof(S_ZIP_CODE))]
    public interface IS_ZIP_CODEDao : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM S_ZIP_CODE")]
        S_ZIP_CODE[] GetAllData();

        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(S_ZIP_CODE data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(S_ZIP_CODE data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        int Delete(S_ZIP_CODE data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, S_ZIP_CODE data);

        /// <summary>
        /// �X�֔ԍ��V����LIKE�����ɂ��f�[�^�擾
        /// </summary>
        /// <param name="post7"></param>
        /// <returns></returns>
        [Query("POST7 LIKE /*post7*/ ")]
        S_ZIP_CODE[] GetDataByPost7LikeSearch(string post7);

        /// <summary>
        /// �u�s�����{���̑��P�v����LIKE�����ɂ��f�[�^�擾
        /// </summary>
        /// <param name="jusho"></param>
        /// <returns></returns>
        [Query("SIKUCHOUSON + OTHER1 LIKE /*jusho*/ ")]
        S_ZIP_CODE[] GetDataByJushoLikeSearch(string jusho);

        /// <summary>
        /// �u�s���{���{�s�����{���̑��P�v����LIKE�����ɂ��f�[�^�擾
        /// </summary>
        /// <param name="jusho"></param>
        /// <returns></returns>
        [Query("TODOUFUKEN + SIKUCHOUSON + OTHER1 LIKE /*jusho*/ ")]
        S_ZIP_CODE[] GetDataByTodoufukenJushoLikeSearch(string jusho);

        /// <summary>
        /// �Z���̈ꕔ�f�[�^���������@�\
        /// </summary>
        /// <param name="path">SQL�t�@�C���̃p�X</param>
        /// <param name="oldPost">���X�֔ԍ�</param>
        /// <param name="oldAddress">���Z��</param>
        /// <param name="newPost">�V�X�֔ԍ�</param>
        /// <param name="newAddress">�V�Z��</param>
        /// <returns></returns>
        int UpdatePartData(string path, string oldPost, string oldAddress, string newPost, string newAddress);

        /// <summary>
        /// �s���{���A�s�撬���̑g�ݍ��킹�`�F�b�N
        /// </summary>
        /// <param name="todofuken">�s���{��</param>
        /// <param name="sikuchouson">�s�撬��</param>
        /// <returns></returns>
        [Query("TODOUFUKEN = /*todofuken*/ AND SIKUCHOUSON = /*sikuchouson*/ ")]
        S_ZIP_CODE[] GetDataByTdkScsSearch(string todofuken, string sikuchouson);

        /// <summary>
        /// �s���{���A�s�撬���iLIKE�����j�̑g�ݍ��킹�`�F�b�N
        /// </summary>
        /// <param name="todofuken">�s���{��</param>
        /// <param name="sikuchouson">�s�撬��</param>
        /// <returns></returns>
        [Query("TODOUFUKEN = /*todofuken*/ AND SIKUCHOUSON LIKE /*sikuchouson*/ ")]
        S_ZIP_CODE[] GetDataByTdkScsLikeSearch(string todofuken, string sikuchouson);

        /// <summary>
        /// �u�s���{���{�s�����v����LIKE�����ɂ�錋�ʐ��擾
        /// </summary>
        /// <param name="jusho">�Z���i�s���{���{�s�����j</param>
        /// <returns></returns>
        [Sql("SELECT COUNT(*) FROM S_ZIP_CODE WHERE /*jusho*/ LIKE TODOUFUKEN + SIKUCHOUSON + '%'")]
        int GetDataByJushoCountLikeSearch(string jusho);

        /// <summary>
        /// �u�s���{���{�s�����{���̑��P�v����LIKE�����ɂ��Z�������`�F�b�N
        /// </summary>
        /// <param name="jusho">�Z���i�s���{���{�s�����{���̑��P�j</param>
        /// <returns></returns>
        [Query("/*jusho*/ LIKE TODOUFUKEN + SIKUCHOUSON + OTHER1 + '%' COLLATE Japanese_CS_AS_KS_WS")]
        List<S_ZIP_CODE> GetDataByJushoSplitLikeSearch(string jusho);
    }
}
