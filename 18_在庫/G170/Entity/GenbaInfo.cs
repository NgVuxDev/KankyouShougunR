using r_framework.Entity;

namespace Shougun.Core.Stock.ZaikoShimeSyori.Entity
{
    /// <summary>
    /// ������f�[�^�N���X
    /// </summary>
    public class GenbaInfo : SuperEntity
    {
        public string RET_GYOUSHA_CD { get; set; } //�Ǝ�CD
        public string RET_GYOUSHA_NAME_RYAKU { get; set; } //�ƎҖ�
        public string RET_GENBA_CD { get; set; } //����CD
        public string RET_GENBA_NAME_RYAKU { get; set; } //���ꖼ
    }
}