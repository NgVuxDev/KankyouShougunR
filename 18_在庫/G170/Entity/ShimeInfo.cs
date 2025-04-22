using r_framework.Entity;

namespace Shougun.Core.Stock.ZaikoShimeSyori.Entity
{
    /// <summary>
    /// �݌ɒ��߃f�[�^�N���X
    /// </summary>
    public class ShimeInfo : SuperEntity
    {
        public string RET_SYSTEM_ID { get; set; } //�V�X�e��ID
        public string RET_ZAIKO_SHIME_DATE { get; set; } //�݌ɒ����s��
        public string RET_GYOUSHA_CD { get; set; } //�Ǝ�CD
        public string RET_GENBA_CD { get; set; } //����CD
        public string RET_GENBA_NAME_RYAKU { get; set; } //���ꖼ
        public string RET_ZAIKO_HINMEI_CD { get; set; } //�݌�CD
        public string RET_ZAIKO_HINMEI_RYAKU { get; set; } //�݌ɕi��
        public string RET_REMAIN_SUU { get; set; } //�O���c��
        public string RET_ENTER_SUU { get; set; } //���������
        public string RET_OUT_SUU { get; set; } //�����o�ח�
        public string RET_ADJUST_SUU { get; set; } //������
        public string RET_TOTAL_SUU { get; set; } //�����݌Ɏc
        public string RET_TANKA { get; set; } //�]���P��
        public string RET_MULT { get; set; } //�݌ɋ��z

    }
}