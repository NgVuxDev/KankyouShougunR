// $Id: IT_CONTENA_RESERVEDao.cs 36292 2014-12-02 02:43:29Z fangjk@oec-h.com $
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
    [Bean(typeof(T_CONTENA_RESERVE))]
    public interface IT_CONTENA_RESERVEDao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_CONTENA_RESERVE data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>����</returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_CONTENA_RESERVE data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_CONTENA_RESERVE data);        

        /// <summary>
        /// �V�X�e��ID�Ǝ}�Ԃ����ƂɃR���e�i�ғ��\��̃f�[�^���擾����
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetContenaReserveData.sql")]
        T_CONTENA_RESERVE[] GetContenaReserveData(DTOClass data);
    }
}
