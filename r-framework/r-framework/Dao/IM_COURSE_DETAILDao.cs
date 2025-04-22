using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_COURSE_DETAIL))]
    public interface IM_COURSE_DETAILDao : IS2Dao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_COURSE_DETAIL data);

        [NoPersistentProps("DAY_CD", "COURSE_NAME_CD", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_COURSE_DETAIL data);

        /// <summary>
        /// �R�[�X�ڍׂ̃��R�[�h���폜���܂�
        /// </summary>
        /// <param name="data">�����G���e�B�e�B</param>
        /// <returns>�폜��������</returns>
        int Delete(M_COURSE_DETAIL data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("DAY_CD = /*DAY_CD*/ AND COURSE_NAME_CD = /*COURSE_NAME_CD*/ AND REC_ID = /*REC_ID*/")]
        M_COURSE_DETAIL GetDataByCd(string DAY_CD, string COURSE_NAME_CD, string REC_ID);

        [Sql("SELECT * FROM M_COURSE_DETAIL")]
        M_COURSE_DETAIL[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.CourseDetail.IM_COURSE_DETAILDao_GetAllValidData.sql")]
        M_COURSE_DETAIL[] GetAllValidData(M_COURSE_DETAIL data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_COURSE_DETAIL data);

        /// <summary>
        /// �ƎҁA����ɊY������M_COURSE_DETAIL����M_COURSE_DETAIL_ITEMS�̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.CourseDetail.IM_COURSE_DETAILDao_GetCourseDetailItemsData.sql")]
        DataTable GetCourseDetailItemsData(M_COURSE_DETAIL data);

    }
}
