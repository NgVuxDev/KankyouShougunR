using r_framework.Entity;
using System;

namespace Shougun.Core.Stock.ZaikoShimeSyori.Entity
{
    /// <summary>
    /// �݌ɒ��ߑΏۃf�[�^�N���X
    /// </summary>
    public class ShimeTargetInfo : SuperEntity
    {
        public string RET_GYOUSHA_CD { get; set; }          // �Ǝ�CD
        public string RET_GENBA_CD { get; set; }            // ����CD
        public string RET_GENBA_NAME { get; set; }          // ���ꖼ
        public string RET_ZAIKO_HINMEI_CD { get; set; }     // �݌�CD
        public string RET_ZAIKO_HINMEI_NAME { get; set; }   // �݌ɕi��
        public decimal RET_JYUURYOU { get; set; }             // �d��
        public decimal RET_TANKA { get; set; }                // �P��
        public decimal RET_KINGAKU { get; set; }              // ���z
        public DateTime RET_DENPYOU_DATE { get; set; }      // �`�[���t
        public int RET_TARGET_FLG { get; set; }             // �Ώۃf�[�^�t���O

    }
}