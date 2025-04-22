using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    [Bean(typeof(M_TORIHIKISAKI_SEIKYUU))]
    public interface IM_TORIHIKISAKI_SEIKYUUDao : IS2Dao
    {
        [Sql("SELECT * FROM M_TORIHIKISAKI_SEIKYUU")]
        M_TORIHIKISAKI_SEIKYUU[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_TORIHIKISAKI_SEIKYUU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_TORIHIKISAKI_SEIKYUU data);

        int Delete(M_TORIHIKISAKI_SEIKYUU data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_TORIHIKISAKI_SEIKYUU data);

        /// <summary>
        /// �����CD�R�[�h�����ƂɎ����_�������}�X�^�̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_TORIHIKISAKI_SEIKYUU GetDataByCd(string cd);

        /// <summary>
        /// �Z���̈ꕔ�f�[�^���������@�\
        /// </summary>
        /// <param name="path">SQL�t�@�C���̃p�X</param>
        /// <param name="data">����搿�����}�X�^�G���e�B�e�B</param>
        /// <param name="oldPost">���X�֔ԍ�</param>
        /// <param name="oldAddress">���Z��</param>
        /// <param name="newPost">�V�X�֔ԍ�</param>
        /// <param name="newAddress">�V�Z��</param>
        /// <returns></returns>
        int UpdatePartData(string path, M_TORIHIKISAKI_SEIKYUU data, string oldPost, string oldAddress, string newPost, string newAddress);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        //Begin: LANDUONG - 20220209 - refs#160050
        [SqlFile("r_framework.Dao.SqlFile.Torihikisaki.GetMaxPlusRakurakuCustomerCode.sql")]
        decimal GetMaxPlusRakurakuCode(M_TORIHIKISAKI_SEIKYUU data);

        [SqlFile("r_framework.Dao.SqlFile.Torihikisaki.GetAllRakurakuCustomerCode.sql")]
        string[] GetAllRakurakuCodeData(M_TORIHIKISAKI_SEIKYUU data);

        [SqlFile("r_framework.Dao.SqlFile.Torihikisaki.GetMinBlankRakurakuCustomerCode.sql")]
        decimal GetMinBlankRakurakuCode(M_TORIHIKISAKI_SEIKYUU data);

        [Sql("SELECT * FROM M_TORIHIKISAKI_SEIKYUU MTS INNER JOIN M_TORIHIKISAKI MT ON MTS.TORIHIKISAKI_CD = MT.TORIHIKISAKI_CD WHERE MT.DELETE_FLG = 0 AND RAKURAKU_CUSTOMER_CD = /*code*/")]
        M_TORIHIKISAKI_SEIKYUU[] GetDataByRakurakuCode(string code);
        //End: LANDUONG - 20220209 - refs#160050
    }
}
