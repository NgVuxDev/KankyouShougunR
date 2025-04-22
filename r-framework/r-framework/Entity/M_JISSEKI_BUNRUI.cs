using System.Data.SqlTypes;

namespace r_framework.Entity
{
    /// <summary>
    /// 一般廃用報告書分類
    /// </summary>
    public class M_JISSEKI_BUNRUI : SuperEntity
    {
        /// <summary>実績分類CD</summary>
        public string JISSEKI_BUNRUI_CD { get; set; }
        /// <summary>実績分類名</summary>
        public string JISSEKI_BUNRUI_NAME { get; set; }
        /// <summary>実績分類略称</summary>
        public string JISSEKI_BUNRUI_NAME_RYAKU { get; set; }
        /// <summary>実績分類フリガナ</summary>
        public string JISSEKI_BUNRUI_FURIGANA { get; set; }
        /// <summary>実績分類備考</summary>
        public string JISSEKI_BUNRUI_BIKOU { get; set; }
        /// <summary>削除フラグ</summary>
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
