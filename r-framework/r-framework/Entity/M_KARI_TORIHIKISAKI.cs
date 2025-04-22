using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_KARI_TORIHIKISAKI : SuperEntity
    {
        public string TORIHIKISAKI_CD { get; set; }
        public SqlInt16 TORIHIKISAKI_KYOTEN_CD { get; set; }
        public string TORIHIKISAKI_NAME1 { get; set; }
        public string TORIHIKISAKI_NAME2 { get; set; }
        public string TORIHIKISAKI_NAME_RYAKU { get; set; }
        public string TORIHIKISAKI_FURIGANA { get; set; }
        public string TORIHIKISAKI_TEL { get; set; }
        public string TORIHIKISAKI_FAX { get; set; }
        public string TORIHIKISAKI_KEISHOU1 { get; set; }
        public string TORIHIKISAKI_KEISHOU2 { get; set; }
        public string EIGYOU_TANTOU_BUSHO_CD { get; set; }
        public string EIGYOU_TANTOU_CD { get; set; }
        public string TORIHIKISAKI_POST { get; set; }
        public SqlInt16 TORIHIKISAKI_TODOUFUKEN_CD { get; set; }
        public string TORIHIKISAKI_ADDRESS1 { get; set; }
        public string TORIHIKISAKI_ADDRESS2 { get; set; }
        public SqlInt16 TORIHIKI_JOUKYOU { get; set; }
        public string CHUUSHI_RIYUU1 { get; set; }
        public string CHUUSHI_RIYUU2 { get; set; }
        public string BUSHO { get; set; }
        public string TANTOUSHA { get; set; }
        public string SHUUKEI_ITEM_CD { get; set; }
        public string GYOUSHU_CD { get; set; }
        public string BIKOU1 { get; set; }
        public string BIKOU2 { get; set; }
        public string BIKOU3 { get; set; }
        public string BIKOU4 { get; set; }
        public SqlInt16 NYUUKINSAKI_KBN { get; set; }
        public SqlInt16 DAIHYOU_PRINT_KBN { get; set; }
        public SqlBoolean MANI_HENSOUSAKI_KBN { get; set; }
        public SqlBoolean SHOKUCHI_KBN { get; set; }
        public string MANI_HENSOUSAKI_NAME1 { get; set; }
        public string MANI_HENSOUSAKI_NAME2 { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU1 { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU2 { get; set; }
        public string MANI_HENSOUSAKI_POST { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS1 { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS2 { get; set; }
        public string MANI_HENSOUSAKI_BUSHO { get; set; }
        public string MANI_HENSOUSAKI_TANTOU { get; set; }
        public SqlDateTime TEKIYOU_BEGIN { get; set; }
        public string SEARCH_TEKIYOU_BEGIN { get; set; }
        public SqlDateTime TEKIYOU_END { get; set; }
        public string SEARCH_TEKIYOU_END { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
        public string TORIHIKISAKI_CD_AFTER { get; set; }
        public SqlInt16 MANI_HENSOUSAKI_THIS_ADDRESS_KBN { get; set; }
    }
}