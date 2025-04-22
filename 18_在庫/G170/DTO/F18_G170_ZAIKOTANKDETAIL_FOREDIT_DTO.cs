using System.Data.SqlTypes;
using System.Collections.Generic;
using Shougun.Core.Stock.ZaikoShimeSyori.Entity;

namespace r_framework.Entity
{
    /// <summary>
    /// �ҏW�p��݌ɒ����ׁ@T_ZAIKO_TANK_DETAIL��f�[�^
    /// </summary>
    public class F18_G170_ZAIKOTANKDETAIL_FOREDIT_DTO
    {
        /// <summary>
        /// �挎���
        /// </summary>
        public ShimeInfo prewMonthInfo { get; set; }

        /// <summary>
        /// ���R�[�h��key�����
        /// </summary>
        /// <summary>
        /// �V�X�e��ID
        /// </summary>
        public SqlInt64 SYSTEM_ID { get; set; }
        /// <summary>
        /// �s�ԍ�
        /// </summary>
        public SqlInt32 ROW_NO { get; set; }
        /// <summary>
        /// �݌ɕi���R�[�h
        /// </summary>
        public string ZAIKO_HINMEI_CD { get; set; }

        /// <summary>
        /// ���R�[�h�̌v�Z�p���
        /// </summary>
        public List<ShimeTargetInfo> calculationList { get; set; }
    }
}