using System.Data.SqlTypes;

namespace r_framework.Entity
{
    /// <summary>
    /// ��ʔp�p�񍐏�����
    /// </summary>
    public class M_JISSEKI_BUNRUI : SuperEntity
    {
        /// <summary>���ѕ���CD</summary>
        public string JISSEKI_BUNRUI_CD { get; set; }
        /// <summary>���ѕ��ޖ�</summary>
        public string JISSEKI_BUNRUI_NAME { get; set; }
        /// <summary>���ѕ��ޗ���</summary>
        public string JISSEKI_BUNRUI_NAME_RYAKU { get; set; }
        /// <summary>���ѕ��ރt���K�i</summary>
        public string JISSEKI_BUNRUI_FURIGANA { get; set; }
        /// <summary>���ѕ��ޔ��l</summary>
        public string JISSEKI_BUNRUI_BIKOU { get; set; }
        /// <summary>�폜�t���O</summary>
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
