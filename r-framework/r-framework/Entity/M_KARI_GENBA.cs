using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_KARI_GENBA : SuperEntity
    {
        public SqlBoolean HIKIAI_GYOUSHA_USE_FLG { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public SqlBoolean HIKIAI_TORIHIKISAKI_USE_FLG { get; set; }
        public string TORIHIKISAKI_CD { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public string GENBA_NAME1 { get; set; }
        public string GENBA_NAME2 { get; set; }
        public string GENBA_NAME_RYAKU { get; set; }
        public string GENBA_FURIGANA { get; set; }
        public string GENBA_TEL { get; set; }
        public string GENBA_FAX { get; set; }
        public string GENBA_KEITAI_TEL { get; set; }
        public string GENBA_KEISHOU1 { get; set; }
        public string GENBA_KEISHOU2 { get; set; }
        public string EIGYOU_TANTOU_BUSHO_CD { get; set; }
        public string EIGYOU_TANTOU_CD { get; set; }
        public string GENBA_POST { get; set; }
        public SqlInt16 GENBA_TODOUFUKEN_CD { get; set; }
        public string GENBA_ADDRESS1 { get; set; }
        public string GENBA_ADDRESS2 { get; set; }
        public SqlInt16 TORIHIKI_JOUKYOU { get; set; }
        public string CHUUSHI_RIYUU1 { get; set; }
        public string CHUUSHI_RIYUU2 { get; set; }
        public string BUSHO { get; set; }
        public string TANTOUSHA { get; set; }
        public string KOUFU_TANTOUSHA { get; set; }
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
        //public SqlBoolean HAISHUTSU_JIGYOUJOU_KBN { get; set; }
        public SqlBoolean TSUMIKAEHOKAN_KBN { get; set; }
        //public SqlBoolean SHOBUN_JIGYOUJOU_KBN { get; set; }
        public SqlBoolean SAISHUU_SHOBUNJOU_KBN { get; set; }
        public SqlBoolean MANI_HENSOUSAKI_KBN { get; set; }
        //public SqlBoolean NIOROSHI_GENBA_KBN { get; set; }
        public SqlInt16 MANIFEST_SHURUI_CD { get; set; }
        public SqlInt16 MANIFEST_TEHAI_CD { get; set; }
        public string SHOBUNSAKI_NO { get; set; }
        public SqlBoolean SHOKUCHI_KBN { get; set; }
        public SqlBoolean DEN_MANI_SHOUKAI_KBN { get; set; }
        public SqlBoolean KENSHU_YOUHI { get; set; }
        public SqlInt16 ITAKU_KEIYAKU_USE_KBN { get; set; }
        public string MANI_HENSOUSAKI_TORIHIKISAKI_CD { get; set; }
        public string MANI_HENSOUSAKI_GYOUSHA_CD { get; set; }
        public string MANI_HENSOUSAKI_GENBA_CD { get; set; }
        public string MANI_HENSOUSAKI_NAME1 { get; set; }
        public string MANI_HENSOUSAKI_NAME2 { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU1 { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU2 { get; set; }
        public string MANI_HENSOUSAKI_POST { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS1 { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS2 { get; set; }
        public string MANI_HENSOUSAKI_BUSHO { get; set; }
        public string MANI_HENSOUSAKI_TANTOU { get; set; }
        public string SHIKUCHOUSON_CD { get; set; }
        public SqlDateTime TEKIYOU_BEGIN { get; set; }
        public string SEARCH_TEKIYOU_BEGIN { get; set; }
        public SqlDateTime TEKIYOU_END { get; set; }
        public string SEARCH_TEKIYOU_END { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }

        //2014.05.12 返送案内仕様変更　by　胡　start
        public string MANI_HENSOUSAKI_TORIHIKISAKI_CD_A { get; set; }
        public string MANI_HENSOUSAKI_GYOUSHA_CD_A { get; set; }
        public string MANI_HENSOUSAKI_GENBA_CD_A { get; set; }
        public string MANI_HENSOUSAKI_NAME1_A { get; set; }
        public string MANI_HENSOUSAKI_NAME2_A { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU1_A { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU2_A { get; set; }
        public string MANI_HENSOUSAKI_POST_A { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS1_A { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS2_A { get; set; }
        public string MANI_HENSOUSAKI_BUSHO_A { get; set; }
        public string MANI_HENSOUSAKI_TANTOU_A { get; set; }

        public string MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 { get; set; }
        public string MANI_HENSOUSAKI_GYOUSHA_CD_B1 { get; set; }
        public string MANI_HENSOUSAKI_GENBA_CD_B1 { get; set; }
        public string MANI_HENSOUSAKI_NAME1_B1 { get; set; }
        public string MANI_HENSOUSAKI_NAME2_B1 { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU1_B1 { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU2_B1 { get; set; }
        public string MANI_HENSOUSAKI_POST_B1 { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS1_B1 { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS2_B1 { get; set; }
        public string MANI_HENSOUSAKI_BUSHO_B1 { get; set; }
        public string MANI_HENSOUSAKI_TANTOU_B1 { get; set; }

        public string MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 { get; set; }
        public string MANI_HENSOUSAKI_GYOUSHA_CD_B2 { get; set; }
        public string MANI_HENSOUSAKI_GENBA_CD_B2 { get; set; }
        public string MANI_HENSOUSAKI_NAME1_B2 { get; set; }
        public string MANI_HENSOUSAKI_NAME2_B2 { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU1_B2 { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU2_B2 { get; set; }
        public string MANI_HENSOUSAKI_POST_B2 { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS1_B2 { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS2_B2 { get; set; }
        public string MANI_HENSOUSAKI_BUSHO_B2 { get; set; }
        public string MANI_HENSOUSAKI_TANTOU_B2 { get; set; }

        public string MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 { get; set; }
        public string MANI_HENSOUSAKI_GYOUSHA_CD_B4 { get; set; }
        public string MANI_HENSOUSAKI_GENBA_CD_B4 { get; set; }
        public string MANI_HENSOUSAKI_NAME1_B4 { get; set; }
        public string MANI_HENSOUSAKI_NAME2_B4 { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU1_B4 { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU2_B4 { get; set; }
        public string MANI_HENSOUSAKI_POST_B4 { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS1_B4 { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS2_B4 { get; set; }
        public string MANI_HENSOUSAKI_BUSHO_B4 { get; set; }
        public string MANI_HENSOUSAKI_TANTOU_B4 { get; set; }

        public string MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 { get; set; }
        public string MANI_HENSOUSAKI_GYOUSHA_CD_B6 { get; set; }
        public string MANI_HENSOUSAKI_GENBA_CD_B6 { get; set; }
        public string MANI_HENSOUSAKI_NAME1_B6 { get; set; }
        public string MANI_HENSOUSAKI_NAME2_B6 { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU1_B6 { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU2_B6 { get; set; }
        public string MANI_HENSOUSAKI_POST_B6 { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS1_B6 { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS2_B6 { get; set; }
        public string MANI_HENSOUSAKI_BUSHO_B6 { get; set; }
        public string MANI_HENSOUSAKI_TANTOU_B6 { get; set; }

        public string MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 { get; set; }
        public string MANI_HENSOUSAKI_GYOUSHA_CD_C1 { get; set; }
        public string MANI_HENSOUSAKI_GENBA_CD_C1 { get; set; }
        public string MANI_HENSOUSAKI_NAME1_C1 { get; set; }
        public string MANI_HENSOUSAKI_NAME2_C1 { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU1_C1 { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU2_C1 { get; set; }
        public string MANI_HENSOUSAKI_POST_C1 { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS1_C1 { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS2_C1 { get; set; }
        public string MANI_HENSOUSAKI_BUSHO_C1 { get; set; }
        public string MANI_HENSOUSAKI_TANTOU_C1 { get; set; }

        public string MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 { get; set; }
        public string MANI_HENSOUSAKI_GYOUSHA_CD_C2 { get; set; }
        public string MANI_HENSOUSAKI_GENBA_CD_C2 { get; set; }
        public string MANI_HENSOUSAKI_NAME1_C2 { get; set; }
        public string MANI_HENSOUSAKI_NAME2_C2 { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU1_C2 { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU2_C2 { get; set; }
        public string MANI_HENSOUSAKI_POST_C2 { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS1_C2 { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS2_C2 { get; set; }
        public string MANI_HENSOUSAKI_BUSHO_C2 { get; set; }
        public string MANI_HENSOUSAKI_TANTOU_C2 { get; set; }

        public string MANI_HENSOUSAKI_TORIHIKISAKI_CD_D { get; set; }
        public string MANI_HENSOUSAKI_GYOUSHA_CD_D { get; set; }
        public string MANI_HENSOUSAKI_GENBA_CD_D { get; set; }
        public string MANI_HENSOUSAKI_NAME1_D { get; set; }
        public string MANI_HENSOUSAKI_NAME2_D { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU1_D { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU2_D { get; set; }
        public string MANI_HENSOUSAKI_POST_D { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS1_D { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS2_D { get; set; }
        public string MANI_HENSOUSAKI_BUSHO_D { get; set; }
        public string MANI_HENSOUSAKI_TANTOU_D { get; set; }

        public string MANI_HENSOUSAKI_TORIHIKISAKI_CD_E { get; set; }
        public string MANI_HENSOUSAKI_GYOUSHA_CD_E { get; set; }
        public string MANI_HENSOUSAKI_GENBA_CD_E { get; set; }
        public string MANI_HENSOUSAKI_NAME1_E { get; set; }
        public string MANI_HENSOUSAKI_NAME2_E { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU1_E { get; set; }
        public string MANI_HENSOUSAKI_KEISHOU2_E { get; set; }
        public string MANI_HENSOUSAKI_POST_E { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS1_E { get; set; }
        public string MANI_HENSOUSAKI_ADDRESS2_E { get; set; }
        public string MANI_HENSOUSAKI_BUSHO_E { get; set; }
        public string MANI_HENSOUSAKI_TANTOU_E { get; set; }

        //2014.05.12 返送案内仕様変更　by　胡　end

        public SqlInt16 MANI_HENSOUSAKI_USE_A { get; set; }
        public SqlInt16 MANI_HENSOUSAKI_USE_B1 { get; set; }
        public SqlInt16 MANI_HENSOUSAKI_USE_B2 { get; set; }
        public SqlInt16 MANI_HENSOUSAKI_USE_B4 { get; set; }
        public SqlInt16 MANI_HENSOUSAKI_USE_B6 { get; set; }
        public SqlInt16 MANI_HENSOUSAKI_USE_C1 { get; set; }
        public SqlInt16 MANI_HENSOUSAKI_USE_C2 { get; set; }
        public SqlInt16 MANI_HENSOUSAKI_USE_D { get; set; }
        public SqlInt16 MANI_HENSOUSAKI_USE_E { get; set; }
        public SqlInt16 MANI_HENSOUSAKI_THIS_ADDRESS_KBN { get; set; }

        // 20151104 BUNN #12040 STR
        public SqlBoolean HAISHUTSU_NIZUMI_GENBA_KBN { get; set; }
        public SqlBoolean SHOBUN_NIOROSHI_GENBA_KBN { get; set; }
        // 20151104 BUNN #12040 EDN

        // 20160429 koukoukon v2.1_電子請求書 #16612 start
        public string HAKKOUSAKI_CD { get; set; }
        // 20160429 koukoukon v2.1_電子請求書 #16612 end

        //20250320
        public string CHIZU { get; set; }

    }
}