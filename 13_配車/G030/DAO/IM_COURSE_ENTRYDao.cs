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
    [Bean(typeof(M_COURSE))]
    public interface IM_COURSE_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// �R�[�X���̃|�b�v�A�b�v�f�[�^�̃��X�g���擾����
        /// </summary>
        /// <parameparam name="data">��������</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetCourseEntryData.sql")]
        new DataTable GetCourseEntryData(DTOClass data);
    }
}
