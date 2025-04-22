// $Id: IM_COURSE_DETAILDao.cs 6914 2013-11-14 04:12:37Z sys_dev_20 $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei
{
    [Bean(typeof(M_COURSE_DETAIL_ITEMS))]
    public interface IM_COURSE_DETAIL_ITEMSDao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_COURSE_DETAIL_ITEMS data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_COURSE_DETAIL_ITEMS data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_COURSE_DETAIL_ITEMS data);

        /// <summary>
        /// �R�[�XCD���L�[�Ƃ��ăR�[�X���ׂ̃f�[�^���擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei.Sql.GetCourseDetailData.sql")]
        new DataTable GetCourseDetailData(DTOClass data);

        /// <summary>
        /// �R�[�XCD���L�[�Ƃ��ăR�[�X�׍~��̃f�[�^���擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei.Sql.GetCourseNioroshiData.sql")]
        new DataTable GetCourseNioroshiData(DTOClass data);

        /// <summary>
        /// �R�[�X����CD�A���R�[�hID���L�[�Ƃ��ăR�[�X_���ד���̃f�[�^���擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei.Sql.GetCourseDetailItemsData.sql")]
        new DataTable GetCourseDetailItemsData(DTOClass data);
    }
}
