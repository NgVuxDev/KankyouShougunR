// $Id: IM_COURSE_DETAILDao.cs 9378 2013-12-03 07:33:31Z sys_dev_20 $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Allocation.TeikiHaishaNyuuryoku
{
    [Bean(typeof(M_COURSE_DETAIL))]
    public interface IM_COURSE_DETAILDao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_COURSE_DETAIL data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_COURSE_DETAIL data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_COURSE_DETAIL data);

        /// <summary>
        /// �R�[�XCD���L�[�Ƃ��ăR�[�X���ׂ̃f�[�^���擾����
        /// </summary>
        /// <parameparam name="data">��������</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetCourseDetailData.sql")]
        new DataTable GetCourseDetailData(DTOClass data);

        /// <summary>
        /// �R�[�XCD���L�[�Ƃ��ăR�[�X�׍~��̃f�[�^���擾����
        /// </summary>
        /// <parameparam name="data">��������</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetCourseNioroshiData.sql")]
        new DataTable GetCourseNioroshiData(DTOClass data);

        /// <summary>
        /// �R�[�X����CD�A���R�[�hID���L�[�Ƃ��ăR�[�X_���ד���̃f�[�^���擾����
        /// </summary>
        /// <parameparam name="data">��������</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetCourseDetailItemsData.sql")]
        new DataTable GetCourseDetailItemsData(DTOClass data);

        /// <summary>
        /// �R�[�X���̃|�b�v�A�b�v�f�[�^�̃��X�g���擾����
        /// </summary>
        /// <parameparam name="data">��������</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetCourseNameListForPopUp.sql")]
        new DataTable GetCourseNameListForPopUp(DTOClass data);
    }
}
