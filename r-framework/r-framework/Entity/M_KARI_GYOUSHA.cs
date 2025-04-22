using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_KARI_GYOUSHA : SuperEntity
    {
        public string GYOUSHA_CD { get; set; }
        public SqlInt16 TORIHIKISAKI_UMU_KBN { get; set; }
        public SqlBoolean GYOUSHAKBN_UKEIRE { get; set; }
        public SqlBoolean GYOUSHAKBN_SHUKKA { get; set; }
        public SqlBoolean GYOUSHAKBN_MANI { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public string GYOUSHA_NAME1 { get; set; }
        public string GYOUSHA_NAME2 { get; set; }
        public string GYOUSHA_NAME_RYAKU { get; set; }
        public string GYOUSHA_FURIGANA { get; set; }
        public string GYOUSHA_TEL { get; set; }
        public string GYOUSHA_FAX { get; set; }
        public string GYOUSHA_KEITAI_TEL { get; set; }
        public string GYOUSHA_KEISHOU1 { get; set; }
        public string GYOUSHA_KEISHOU2 { get; set; }
        public string EIGYOU_TANTOU_BUSHO_CD { get; set; }
        public string EIGYOU_TANTOU_CD { get; set; }
        public string GYOUSHA_DAIHYOU { get; set; }
        public string GYOUSHA_POST { get; set; }
        public SqlInt16 GYOUSHA_TODOUFUKEN_CD { get; set; }
        public string GYOUSHA_ADDRESS1 { get; set; }
        public string GYOUSHA_ADDRESS2 { get; set; }
        public SqlInt16 TORIHIKI_JOUKYOU { get; set; }
        public string CHUUSHI_RIYUU1 { get; set; }
        public string CHUUSHI_RIYUU2 { get; set; }
        public string BUSHO { get; set; }
        public string TANTOUSHA { get; set; }
        public string SHUUKEI_ITEM_CD { get; set; }
        public string GYOUSHU_CD { get; set; }
        public string CHIIKI_CD { get; set; }
        public string BIKOU1 { get; set; }
        public string BIKOU2 { get; set; }
        public string BIKOU3 { get; set; }
        public string BIKOU4 { get; set; }
        public string SEIKYUU_SOUFU_NAME1 { get; set; }
        public string SEIKYUU_SOUFU_NAME2 { get; set; }
        public string SEIKYUU_SOUFU_KEISHOU1 { get; set; }
        public string SEIKYUU_SOUFU_KEISHOU2 { get; set; }
        public string SEIKYUU_SOUFU_POST { get; set; }
        public string SEIKYUU_SOUFU_ADDRESS1 { get; set; }
        public string SEIKYUU_SOUFU_ADDRESS2 { get; set; }
        public string SEIKYUU_SOUFU_BUSHO { get; set; }
        public string SEIKYUU_SOUFU_TANTOU { get; set; }
        public string SEIKYUU_SOUFU_TEL { get; set; }
        public string SEIKYUU_SOUFU_FAX { get; set; }
        public string SEIKYUU_TANTOU { get; set; }
        public SqlInt16 SEIKYUU_DAIHYOU_PRINT_KBN { get; set; }
        public SqlInt16 SEIKYUU_KYOTEN_PRINT_KBN { get; set; }
        public SqlInt16 SEIKYUU_KYOTEN_CD { get; set; }
        public string SHIHARAI_SOUFU_NAME1 { get; set; }
        public string SHIHARAI_SOUFU_NAME2 { get; set; }
        public string SHIHARAI_SOUFU_KEISHOU1 { get; set; }
        public string SHIHARAI_SOUFU_KEISHOU2 { get; set; }
        public string SHIHARAI_SOUFU_POST { get; set; }
        public string SHIHARAI_SOUFU_ADDRESS1 { get; set; }
        public string SHIHARAI_SOUFU_ADDRESS2 { get; set; }
        public string SHIHARAI_SOUFU_BUSHO { get; set; }
        public string SHIHARAI_SOUFU_TANTOU { get; set; }
        public string SHIHARAI_SOUFU_TEL { get; set; }
        public string SHIHARAI_SOUFU_FAX { get; set; }
        public SqlInt16 SHIHARAI_KYOTEN_PRINT_KBN { get; set; }
        public SqlInt16 SHIHARAI_KYOTEN_CD { get; set; }
        public SqlBoolean JISHA_KBN { get; set; }
        //public SqlBoolean HAISHUTSU_JIGYOUSHA_KBN { get; set; }
        //public SqlBoolean UNPAN_JUTAKUSHA_KBN { get; set; }
        //public SqlBoolean SHOBUN_JUTAKUSHA_KBN { get; set; }
        public SqlBoolean MANI_HENSOUSAKI_KBN { get; set; }
        //public SqlBoolean UNPAN_KAISHA_KBN { get; set; }
        //public SqlBoolean NIOROSHI_GHOUSHA_KBN { get; set; }
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
        public SqlInt16 MANI_HENSOUSAKI_THIS_ADDRESS_KBN { get; set; }

        // 20151104 BUNN #12040 STR
        public SqlBoolean HAISHUTSU_NIZUMI_GYOUSHA_KBN { get; set; }
        public SqlBoolean UNPAN_JUTAKUSHA_KAISHA_KBN { get; set; }
        public SqlBoolean SHOBUN_NIOROSHI_GYOUSHA_KBN { get; set; }
        // 20151104 BUNN #12040 END

        // 20160429 koukoukon v2.1_電子請求書 #16612 start
        public string HAKKOUSAKI_CD { get; set; }
        // 20160429 koukoukon v2.1_電子請求書 #16612 end

        //20250320
        public string URIAGE_GURUPU_CD { get; set; }
        public string SHIHARAI_GURUPU_CD { get; set; }
    }
}