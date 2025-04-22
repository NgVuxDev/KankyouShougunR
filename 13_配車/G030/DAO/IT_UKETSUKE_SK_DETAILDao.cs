// $Id: IT_UKETSUKE_SK_DETAILDao.cs 6767 2013-11-13 05:16:21Z sys_dev_20 $
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
    [Bean(typeof(T_UKETSUKE_SK_DETAIL))]
    public interface IT_UKETSUKE_SK_DETAILDao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SK_DETAIL data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>����</returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UKETSUKE_SK_DETAIL data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_UKETSUKE_SK_DETAIL data);        

        /// <summary>
        /// �V�X�e��ID�Ǝ}�Ԃ����ƂɎ�t�i�o�ׁj���ׂ̃f�[�^���擾����
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetUketsukeSKDetailData.sql")]
        T_UKETSUKE_SK_DETAIL[] GetUketsukeSKDetailData(DTOClass data);
    }
}
