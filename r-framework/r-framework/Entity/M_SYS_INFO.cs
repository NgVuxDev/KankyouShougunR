using System;
using System.Data.SqlTypes;

namespace r_framework.Entity
{
    [Serializable()]
    public class M_SYS_INFO : SuperEntity
    {
        public SqlInt16 SYS_ID { get; set; }
        public SqlBoolean ICHIRAN_HYOUJI_JOUKEN_TEKIYOU { get; set; }
        public SqlBoolean ICHIRAN_HYOUJI_JOUKEN_DELETED { get; set; }
        public SqlBoolean ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI { get; set; }
        public SqlInt32 ICHIRAN_ALERT_KENSUU { get; set; }
        public SqlInt16 SYS_ZEI_KEISAN_KBN_USE_KBN { get; set; }
        public SqlInt16 SYS_SUURYOU_FORMAT_CD { get; set; }
        public string SYS_SUURYOU_FORMAT { get; set; }
        public SqlInt16 SYS_JYURYOU_FORMAT_CD { get; set; }
        public string SYS_JYURYOU_FORMAT { get; set; }
        public SqlInt16 SYS_TANKA_FORMAT_CD { get; set; }
        public string SYS_TANKA_FORMAT { get; set; }
        public SqlInt16 SYS_RENBAN_HOUHOU_KBN { get; set; }
        public SqlInt16 SYS_RECEIPT_RENBAN_HOUHOU_KBN { get; set; }
        public SqlInt16 SYS_MANI_KEITAI_KBN { get; set; }
        public SqlInt16 SYS_KAKUTEI__TANNI_KBN { get; set; }
        public SqlInt16 SYS_PWD_SAVE_KBN { get; set; }
        public SqlInt16 COMMON_PASSWORD_DISP { get; set; }
        public SqlInt16 MENU_KENGEN_SETTEI_KIJYUNN { get; set; }
        public SqlInt16 SEIKYUU_TORIHIKI_KBN { get; set; }
        public SqlInt16 SEIKYUU_SHIMEBI1 { get; set; }
        public SqlInt16 SEIKYUU_SHIMEBI2 { get; set; }
        public SqlInt16 SEIKYUU_SHIMEBI3 { get; set; }
        public SqlInt16 SEIKYUU_HICCHAKUBI { get; set; }
        public SqlInt16 SEIKYUU_KAISHUU_MONTH { get; set; }
        public SqlInt16 SEIKYUU_KAISHUU_DAY { get; set; }
        public SqlInt16 SEIKYUU_KAISHUU_HOUHOU { get; set; }
        public SqlInt16 SEIKYUU_SHOSHIKI_KBN { get; set; }
        public SqlInt16 SEIKYUU_SHOSHIKI_MEISAI_KBN { get; set; }
        public SqlInt16 SEIKYUU_SHOSHIKI_GENBA_KBN { get; set; }
        public SqlInt16 SEIKYUU_TAX_HASUU_CD { get; set; }
        public SqlInt16 SEIKYUU_KINGAKU_HASUU_CD { get; set; }
        public SqlInt16 SEIKYUU_KEITAI_KBN { get; set; }
        public SqlInt16 SEIKYUU_NYUUKIN_MEISAI_KBN { get; set; }
        public SqlInt16 SEIKYUU_YOUSHI_KBN { get; set; }
        public SqlInt16 SEIKYUU_ZEI_KEISAN_KBN_CD { get; set; }
        public SqlInt16 SEIKYUU_ZEI_KBN_CD { get; set; }
        public SqlInt16 SEIKYUU_DAIHYOU_PRINT_KBN { get; set; }
        public SqlInt16 SEIKYUU_KYOTEN_PRINT_KBN { get; set; }
        public SqlInt16 SEIKYUU_KYOTEN_CD { get; set; }
        public SqlInt16 SHIHARAI_TORIHIKI_KBN { get; set; }
        public SqlInt16 SHIHARAI_SHIMEBI1 { get; set; }
        public SqlInt16 SHIHARAI_SHIMEBI2 { get; set; }
        public SqlInt16 SHIHARAI_SHIMEBI3 { get; set; }
        public SqlInt16 SHIHARAI_MONTH { get; set; }
        public SqlInt16 SHIHARAI_DAY { get; set; }
        public SqlInt16 SHIHARAI_HOUHOU { get; set; }
        public SqlInt16 SHIHARAI_SHOSHIKI_KBN { get; set; }
        public SqlInt16 SHIHARAI_SHOSHIKI_MEISAI_KBN { get; set; }
        public SqlInt16 SHIHARAI_SHOSHIKI_GENBA_KBN { get; set; }
        public SqlInt16 SHIHARAI_TAX_HASUU_CD { get; set; }
        public SqlInt16 SHIHARAI_KINGAKU_HASUU_CD { get; set; }
        public SqlInt16 SHIHARAI_KEITAI_KBN { get; set; }
        public SqlInt16 SHIHARAI_SHUKKIN_MEISAI_KBN { get; set; }
        public SqlInt16 SHIHARAI_YOUSHI_KBN { get; set; }
        public SqlInt16 SHIHARAI_ZEI_KEISAN_KBN_CD { get; set; }
        public SqlInt16 SHIHARAI_ZEI_KBN_CD { get; set; }
        public SqlInt16 SHIHARAI_KYOTEN_PRINT_KBN { get; set; }
        public SqlInt16 SHIHARAI_KYOTEN_CD { get; set; }
        public SqlBoolean GYOUSHA_KBN_UKEIRE { get; set; }
        public SqlBoolean GYOUSHA_KBN_SHUKKA { get; set; }
        public SqlBoolean GYOUSHA_KBN_MANI { get; set; }
        public SqlInt16 GYOUSHA_TORIHIKI_CHUUSHI { get; set; }
        public SqlBoolean GYOUSHA_JISHA_KBN { get; set; }
        //public SqlBoolean GYOUSHA_HAISHUTSU_JIGYOUSHA_KBN { get; set; }
        //public SqlBoolean GYOUSHA_UNPAN_JUTAKUSHA_KBN { get; set; }
        //public SqlBoolean GYOUSHA_SHOBUN_JUTAKUSHA_KBN { get; set; }
        public SqlBoolean GYOUSHA_MANI_HENSOUSAKI_KBN { get; set; }
        public SqlBoolean GYOUSHA_UNPAN_KAISHA_KBN { get; set; }
        public SqlBoolean GYOUSHA_NIOROSHI_GYOUSHA_KBN { get; set; }
        public SqlInt16 GYOUSHA_SEIKYUU_DAIHYOU_PRINT_KBN { get; set; }
        public SqlInt16 GYOUSHA_SEIKYUU_KYOTEN_PRINT_KBN { get; set; }
        public SqlInt16 GYOUSHA_SEIKYUU_KYOTEN_CD { get; set; }
        public SqlInt16 GYOUSHA_SHIHARAI_KYOTEN_PRINT_KBN { get; set; }
        public SqlInt16 GYOUSHA_SHIHARAI_KYOTEN_CD { get; set; }
        public SqlBoolean GENBA_JISHA_KBN { get; set; }
        //public SqlBoolean GENBA_HAISHUTSU_JIGYOUJOU_KBN { get; set; }
        public SqlBoolean GENBA_TSUMIKAEHOKAN_KBN { get; set; }
        //public SqlBoolean GENBA_SHOBUN_JIGYOUJOU_KBN { get; set; }
        public SqlBoolean GENBA_SAISHUU_SHOBUNJOU_KBN { get; set; }
        public SqlBoolean GENBA_MANI_HENSOUSAKI_KBN { get; set; }
        public SqlBoolean GENBA_NIOROSHI_GENBA_KBN { get; set; }
        public SqlInt16 GENBA_MANIFEST_SHURUI_CD { get; set; }
        public SqlInt16 GENBA_MANIFEST_TEHAI_CD { get; set; }
        public SqlInt16 GENBA_SEIKYUU_DAIHYOU_PRINT_KBN { get; set; }
        public SqlInt16 GENBA_SEIKYUU_KYOTEN_PRINT_KBN { get; set; }
        public SqlInt16 GENBA_SEIKYUU_KYOTEN_CD { get; set; }
        public SqlInt16 GENBA_SHIHARAI_KYOTEN_PRINT_KBN { get; set; }
        public SqlInt16 GENBA_SHIHARAI_KYOTEN_CD { get; set; }
        public SqlInt16 NYUUKIN_TORIKOMI_KBN { get; set; }
        public SqlInt16 SHUKKIN_TORIKOMI_KBN { get; set; }
        public SqlInt16 HINMEI_UNIT_CD { get; set; }
        public SqlInt16 HINMEI_DENSHU_KBN_CD { get; set; }
        public SqlInt16 HINMEI_DENPYOU_KBN_CD { get; set; }
        public SqlInt16 HINMEI_ZEI_KBN_CD { get; set; }
        public SqlInt16 KYUUJITSU_SUNDAY_CHECK { get; set; }
        public SqlInt16 KIHON_HINMEI_DEFAULT { get; set; }
        public SqlInt16 KOBETSU_HINMEI_DEFAULT { get; set; }
        public SqlInt16 KANSAN_DEFAULT { get; set; }
        public SqlInt16 KANSAN_KIHON_UNIT_CD { get; set; }
        public SqlInt16 KANSAN_UNIT_CD { get; set; }
        public SqlInt16 MANI_KANSAN_KIHON_UNIT_CD { get; set; }
        public SqlInt16 MANI_KANSAN_UNIT_CD { get; set; }
        public SqlInt16 ITAKU_KEIYAKU_CHECK { get; set; }
        public SqlInt16 ITAKU_KEIYAKU_ALERT { get; set; }
        public SqlInt16 ITAKU_KEIYAKU_ALERT_AUTH { get; set; }
        public SqlInt16 ITAKU_KEIYAKU_KOUSHIN_SHUBETSU { get; set; }
        public SqlInt32 ITAKU_KEIYAKU_ALERT_END_DAYS { get; set; }
        public SqlInt16 ITAKU_KEIYAKU_SUURYOU_FORMAT_CD { get; set; }
        public string ITAKU_KEIYAKU_SUURYOU_FORMAT { get; set; }
        public SqlInt16 ITAKU_KEIYAKU_TANKA_FORMAT_CD { get; set; }
        public string ITAKU_KEIYAKU_TANKA_FORMAT { get; set; }
        public SqlInt16 ITAKU_KEIYAKU_TYPE { get; set; }
        public SqlInt16 ITAKU_KEIYAKU_SHURUI { get; set; }
        public SqlInt16 ITAKU_KEIYAKU_TOUROKU_HOUHOU { get; set; }
        public SqlInt16 CONTENA_MAX_SET_KEIKA_DATE { get; set; }
        public string UKETSUKE_UPN_GYOUSHA_CD_DEFALUT { get; set; }
        public SqlInt16 UKETSUKE_SIJISHO_PRINT_KBN { get; set; }
        public SqlInt16 UKETSUKE_SIJISHO_SUB_PRINT_KBN { get; set; }
        public SqlInt16 HAISHA_NIPPOU_LAYOUT_KBN { get; set; }
        public SqlInt16 HAISHA_ONEDAY_NYUURYOKU_KBN { get; set; }
        public SqlInt16 UKEIRE_KAKUTEI_USE_KBN { get; set; }
        public SqlInt16 UKEIRE_KAKUTEI_FLAG { get; set; }
        public SqlInt16 UKEIRE_CALC_BASE_KBN { get; set; }
        public SqlInt16 UKEIRE_WARIFURI_HASU_CD { get; set; }
        public SqlInt16 UKEIRE_WARIFURI_HASU_KETA { get; set; }
        public SqlInt16 UKEIRE_WARIFURI_WARIAI_HASU_CD { get; set; }
        public SqlInt16 UKEIRE_WARIFURI_WARIAI_HASU_KETA { get; set; }
        public SqlInt16 UKEIRE_CHOUSEI_HASU_CD { get; set; }
        public SqlInt16 UKEIRE_CHOUSEI_HASU_KETA { get; set; }
        public SqlInt16 UKEIRE_CHOUSEI_WARIAI_HASU_CD { get; set; }
        public SqlInt16 UKEIRE_CHOUSEI_WARIAI_HASU_KETA { get; set; }
        public string UKEIRE_SEIKYUU_KEIRYOU_PRINT_TITLE1 { get; set; }
        public string UKEIRE_SEIKYUU_KEIRYOU_PRINT_TITLE2 { get; set; }
        public string UKEIRE_SEIKYUU_KEIRYOU_PRINT_TITLE3 { get; set; }
        public string UKEIRE_SHIHARAI_KEIRYOU_PRINT_TITLE1 { get; set; }
        public string UKEIRE_SHIHARAI_KEIRYOU_PRINT_TITLE2 { get; set; }
        public string UKEIRE_SHIHARAI_KEIRYOU_PRINT_TITLE3 { get; set; }
        public SqlInt16 SHUKKA_KAKUTEI_USE_KBN { get; set; }
        public SqlInt16 SHUKKA_KAKUTEI_FLAG { get; set; }
        public SqlInt16 SHUKKA_CALC_BASE_KBN { get; set; }
        public SqlInt16 SHUKKA_WARIFURI_HASU_CD { get; set; }
        public SqlInt16 SHUKKA_WARIFURI_HASU_KETA { get; set; }
        public SqlInt16 SHUKKA_WARIFURI_WARIAI_HASU_CD { get; set; }
        public SqlInt16 SHUKKA_WARIFURI_WARIAI_HASU_KETA { get; set; }
        public SqlInt16 SHUKKA_CHOUSEI_HASU_CD { get; set; }
        public SqlInt16 SHUKKA_CHOUSEI_HASU_KETA { get; set; }
        public SqlInt16 SHUKKA_CHOUSEI_WARIAI_HASU_CD { get; set; }
        public SqlInt16 SHUKKA_CHOUSEI_WARIAI_HASU_KETA { get; set; }
        public string SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE1 { get; set; }
        public string SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE2 { get; set; }
        public string SHUKKA_SEIKYUU_KEIRYOU_PRINT_TITLE3 { get; set; }
        public string SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE1 { get; set; }
        public string SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE2 { get; set; }
        public string SHUKKA_SHIHARAI_KEIRYOU_PRINT_TITLE3 { get; set; }
        public string KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE1 { get; set; }
        public string KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE2 { get; set; }
        public string KENSHUU_SEIKYUU_KEIRYOU_PRINT_TITLE3 { get; set; }
        public string KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE1 { get; set; }
        public string KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE2 { get; set; }
        public string KENSHUU_SHIHARAI_KEIRYOU_PRINT_TITLE3 { get; set; }
        public SqlInt16 UR_SH_KAKUTEI_USE_KBN { get; set; }
        public SqlInt16 UR_SH_KAKUTEI_FLAG { get; set; }
        public SqlInt16 UR_SH_CALC_BASE_KBN { get; set; }
        public string UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE1 { get; set; }
        public string UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE2 { get; set; }
        public string UR_SH_SEIKYUU_KEIRYOU_PRINT_TITLE3 { get; set; }
        public string UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE1 { get; set; }
        public string UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE2 { get; set; }
        public string UR_SH_SHIHARAI_KEIRYOU_PRINT_TITLE3 { get; set; }
        public SqlInt16 KEIRYOU_TORIHIKISAKI_DISP_KBN { get; set; }
        public SqlInt16 KEIRYOU_LAYOUT_KBN { get; set; }
        public SqlInt16 KEIRYOU_GOODS_KBN { get; set; }
        public string KEIRYOU_HYOU_TITLE_1 { get; set; }
        public string KEIRYOU_HYOU_TITLE_2 { get; set; }
        public string KEIRYOU_HYOU_TITLE_3 { get; set; }
        public SqlInt16 KEIRYOU_KIHON_KEIRYOU { get; set; }
        public SqlInt16 KEIRYOU_HYOU_PRINT_KBN { get; set; }
        public SqlInt16 KEIRYOU_CHOUSEI_HASU_CD { get; set; }
        public SqlInt16 KEIRYOU_CHOUSEI_HASU_KETA { get; set; }
        public SqlInt16 KEIRYOU_CHOUSEI_WARIAI_HASU_CD { get; set; }
        public SqlInt16 KEIRYOU_CHOUSEI_WARIAI_HASU_KETA { get; set; }
        public SqlInt16 KEIRYOU_CHOUSEI_USE_KBN { get; set; }
        public SqlInt16 KEIRYOU_YOUKI_USE_KBN { get; set; }
        public SqlInt16 KEIRYOU_TANKA_KINGAKU_USE_KBN { get; set; }
        public SqlInt16 KEIRYOU_MANIFEST_HAIKI_KBN_CD { get; set; }
        public SqlInt16 KEIRYOU_SEIKYUU_TORIHIKI_KBN_CD { get; set; }
        public SqlInt16 KEIRYOU_SEIKYUU_ZEI_KEISAN_KBN_CD { get; set; }
        public SqlInt16 KEIRYOU_SEIKYUU_ZEI_KBN_CD { get; set; }
        public SqlInt16 KEIRYOU_SHIHARAI_TORIHIKI_KBN_CD { get; set; }
        public SqlInt16 KEIRYOU_SHIHARAI_ZEI_KEISAN_KBN_CD { get; set; }
        public SqlInt16 KEIRYOU_SHIHARAI_ZEI_KBN_CD { get; set; }
        public SqlInt16 KEIRYOU_UKEIRE_KEITAI_KBN_CD { get; set; }
        public SqlInt16 KEIRYOU_UKEIRE_KEIRYOU_KBN_CD { get; set; }
        public SqlInt16 KEIRYOU_SHUKKA_KEITAI_KBN_CD { get; set; }
        public SqlInt16 KEIRYOU_SHUKKA_KEIRYOU_KBN_CD { get; set; }
        public SqlInt16 URIAGE_KAKUTEI_USE_KBN { get; set; }
        public SqlInt16 URIAGE_HYOUJI_JOUKEN { get; set; }
        public SqlBoolean URIAGE_HYOUJI_JOUKEN_UKEIRE { get; set; }
        public SqlBoolean URIAGE_HYOUJI_JOUKEN_SHUKKA { get; set; }
        public SqlBoolean URIAGE_HYOUJI_JOUKEN_URIAGESHIHARAI { get; set; }
        public string URIAGE_KAKUTEI_KAIJO_PASSWORD { get; set; }
        public SqlInt16 SHIHARAI_KAKUTEI_USE_KBN { get; set; }
        public SqlInt16 SHIHARAI_HYOUJI_JOUKEN { get; set; }
        public SqlBoolean SHIHARAI_HYOUJI_JOUKEN_UKEIRE { get; set; }
        public SqlBoolean SHIHARAI_HYOUJI_JOUKEN_SHUKKA { get; set; }
        public SqlBoolean SHIHARAI_HYOUJI_JOUKEN_URIAGESHIHARAI { get; set; }
        public string SHIHARAI_KAKUTEI_KAIJO_PASSWORD { get; set; }
        public SqlDecimal NYUUKIN_HANDAN_BEGIN { get; set; }
        public SqlDecimal NYUUKIN_HANDAN_END { get; set; }
        public SqlInt16 NYUUKIN_NYUUSHUKKIN_KBN_CD { get; set; }
        public SqlInt16 NYUUKIN_IKKATSU_KBN_DISP_SUU { get; set; }
        public SqlInt16 NYUUKIN_IKKATSU_NYUUSHUKKINKIN_KBN_CD { get; set; }
        public SqlInt16 SEIKYUU_SHIME_SHORI_KBN { get; set; }
        public SqlInt16 SEIKYUU_SHIME_SEIKYUU_CHECK { get; set; }
        public SqlInt16 SHIHARAI_SHIME_SHORI_KBN { get; set; }
        public SqlInt16 SHIHARAI_SHIME_SHIHARAI_CHECK { get; set; }
        public SqlInt16 MANIFEST_JYUUYOU_DISP_KBN { get; set; }
        public SqlInt16 MANIFEST_OSHIRASE_DISP_KBN { get; set; }
        public SqlInt16 MANIFEST_RIREKI_DISP_KBN { get; set; }
        public SqlInt16 MANIFEST_TUUCHI_BEGIN { get; set; }
        public SqlInt16 MANIFEST_TUUCHI_END { get; set; }
        public SqlInt16 MANIFEST_REPORT_SUU_KBN { get; set; }
        public SqlInt16 MANIFEST_SUURYO_FORMAT_CD { get; set; }
        public string MANIFEST_SUURYO_FORMAT { get; set; }
        public SqlInt16 MANIFEST_USE_A { get; set; }
        public SqlInt16 MANIFEST_USE_B1 { get; set; }
        public SqlInt16 MANIFEST_USE_B2 { get; set; }
        public SqlInt16 MANIFEST_USE_B4 { get; set; }
        public SqlInt16 MANIFEST_USE_B6 { get; set; }
        public SqlInt16 MANIFEST_USE_C1 { get; set; }
        public SqlInt16 MANIFEST_USE_C2 { get; set; }
        public SqlInt16 MANIFEST_USE_D { get; set; }
        public SqlInt16 MANIFEST_USE_E { get; set; }
        public string MANIFEST_HENSOU_NATSUIN_1 { get; set; }
        public string MANIFEST_HENSOU_NATSUIN_2 { get; set; }
        public string MANIFEST_HENSOU_RENRAKU_1 { get; set; }
        public string MANIFEST_HENSOU_RENRAKU_2 { get; set; }
        public string MANIFEST_PRINTERMAKERNAME { get; set; }
        public string MANIFEST_PRINTERNAME { get; set; }
        public SqlInt16 MANIFEST_ENDDATE_USE_KBN { get; set; }
        public SqlInt16 MANIFEST_UNPAN_DAYS { get; set; }
        public SqlInt16 MANIFEST_SBN_DAYS { get; set; }
        public SqlInt16 MANIFEST_TOK_SBN_DAYS { get; set; }
        public SqlInt16 MANIFEST_LAST_SBN_DAYS { get; set; }
        public SqlInt16 MANIFEST_VALIDATION_CHECK { get; set; }
        public SqlInt16 MANIFEST_RENKEI_KBN { get; set; }
        public SqlInt16 COPY_MODE { get; set; }
        public string MITSUMORI_SUBJECT_DEFAULT { get; set; }
        public string MITSUMORI_KOUMOKU1 { get; set; }
        public string MITSUMORI_KOUMOKU2 { get; set; }
        public string MITSUMORI_KOUMOKU3 { get; set; }
        public string MITSUMORI_KOUMOKU4 { get; set; }
        public string MITSUMORI_NAIYOU1 { get; set; }
        public string MITSUMORI_NAIYOU2 { get; set; }
        public string MITSUMORI_NAIYOU3 { get; set; }
        public string MITSUMORI_NAIYOU4 { get; set; }
        public string MITSUMORI_BIKOU1 { get; set; }
        public string MITSUMORI_BIKOU2 { get; set; }
        public string MITSUMORI_BIKOU3 { get; set; }
        public string MITSUMORI_BIKOU4 { get; set; }
        public string MITSUMORI_BIKOU5 { get; set; }
        public SqlInt16 ZAIKO_KANRI { get; set; }
        public SqlInt16 ZAIKO_HYOUKA_HOUHOU { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
        public SqlInt16 BUSHO_NAME_PRINT { get; set; }
        public SqlInt16 MITSUMORI_INJI_KYOTEN_CD1 { get; set; }
        public SqlInt16 MITSUMORI_INJI_KYOTEN_CD2 { get; set; }
        public SqlInt16 MITSUMORI_ZEIKEISAN_KBN_CD { get; set; }
        public SqlInt16 MITSUMORI_ZEI_KBN_CD { get; set; }
        public SqlInt16 CONTENA_KANRI_HOUHOU { get; set; }
        public SqlInt16 UKEIRE_ZANDAKA_JIDOU_KBN { get; set; }
        public SqlInt16 SHUKKA_ZANDAKA_JIDOU_KBN { get; set; }
        public SqlInt16 UR_SH_ZANDAKA_JIDOU_KBN { get; set; }
        // 代納
        public SqlInt16 DAINO_KAKUTEI_USE_KBN { get; set; }
        public SqlInt16 DAINO_KAKUTEI_FLAG { get; set; }
        public SqlInt16 DAINO_CALC_BASE_KBN { get; set; }
        public SqlInt16 DAINO_ZANDAKA_JIDOU_KBN { get; set; }
		
        public SqlInt16 HAISHA_WARIATE_KAISHI { get; set; }
        public SqlInt16 HAISHA_WARIATE_KUUHAKU { get; set; }

        // 20151105 BUNN #12040 STR
        public SqlBoolean GYOUSHA_HAISHUTSU_NIZUMI_GYOUSHA_KBN { get; set; }
        public SqlBoolean GYOUSHA_UNPAN_JUTAKUSHA_KAISHA_KBN { get; set; }
        public SqlBoolean GYOUSHA_SHOBUN_NIOROSHI_GYOUSHA_KBN { get; set; }
        public SqlBoolean GENBA_HAISHUTSU_NIZUMI_GENBA_KBN { get; set; }
        public SqlBoolean GENBA_SHOBUN_NIOROSHI_GENBA_KBN { get; set; }
        // 20151105 BUNN #12040 END

        // 20160429 koukoukon v2.1_電子請求書 #16612 start
        public SqlInt16 SEIKYUU_OUTPUT_KBN { get; set; }
        // 20160429 koukoukon v2.1_電子請求書 #16612 end

        public SqlInt16 SANPAI_MANIFEST_MERCURY_CHECK { get; set; }
        public SqlInt16 KENPAI_MANIFEST_MERCURY_CHECK { get; set; }

        public string DIGI_CORP_ID { get; set; }
        public string DIGI_UID { get; set; }
        public string DIGI_PWD { get; set; }
        public SqlInt16 DIGI_RANGE_RADIUS { get; set; }
        public SqlInt16 DIGI_CARRY_ORVER_NEXT_DAY { get; set; }

        // NAVITIME
        public string NAVI_CORP_ID { get; set; }
        public string NAVI_ACCOUNT { get; set; }
        public string NAVI_PWD { get; set; }
        public SqlInt16 NAVI_SAGYOU_TIME { get; set; }
        public SqlBoolean NAVI_TRAFFIC { get; set; }
        public SqlBoolean NAVI_SMART_IC { get; set; }
        public SqlBoolean NAVI_TOLL { get; set; }
        public SqlInt16 NAVI_GET_TIME { get; set; }

        // 電子契約
        public string DENSHI_KEIYAKU_MESSAGE { get; set; }
        public SqlInt16 DENSHI_KEIYAKU_SOUFUHOUHOU { get; set; }
        public SqlInt16 DENSHI_KEIYAKU_ACCESS_CODE_CHECK { get; set; }
        public string DENSHI_KEIYAKU_ACCESS_CODE { get; set; }
        public string DENSHI_KEIYAKU_X_APP_ID { get; set; }
        public SqlInt16 DENSHI_KEIYAKU_KYOUYUUSAKI_CC { get; set; }
        
        public string SUPPORT_TOOL_URL_PATH { get; set; }
        public SqlInt16 MAX_WINDOW_COUNT { get; set; }

        public SqlInt16 HINMEI_SEARCH_CHUSYUTSU_JOKEN { get; set; }
        public SqlInt16 HINMEI_SEARCH_DENPYOU_KBN { get; set; }

        public SqlInt16 UKEIRESHUKA_GAMEN_SIZE { get; set; }
        public SqlInt16 DENPYOU_HAKOU_HYOUJI { get; set; }

        //請求書備考
        public string SEIKYUU_BIKOU_1 { get; set; }
        public string SEIKYUU_BIKOU_2 { get; set; }
        //旧請求書
        public SqlInt16 OLD_SEIKYUU_PRINT_KBN { get; set; }
        //角印印刷位置(上)
        public SqlInt16 KAKUIN_POSITION_TOP { get; set; }
        //角印印刷位置(左)
        public SqlInt16 KAKUIN_POSITION_LEFT { get; set; }
        //角印サイズ
        public SqlInt16 KAKUIN_SIZE { get; set; }
        //車輛名印字
        public SqlInt16 SHARYOU_NAME_INGI { get; set; }
        //支払明細書備考
        public string SHIHARAI_BIKOU_1 { get; set; }
        public string SHIHARAI_BIKOU_2 { get; set; }
        //旧支払明細書
        public SqlInt16 OLD_SHIHARAI_PRINT_KBN { get; set; }
        //取引先
        public SqlInt16 TORIHIKISAKI_UMU_KBN { get; set; }
        public SqlInt16 GENBA_KANSAN_UNIT_CD { get; set; }
        public SqlBoolean YOUKI_NYUU { get; set; }
        public SqlBoolean JISSUU { get; set; }
        public SqlInt16 GENBA_TEIKI_KEIYAKU_KBN { get; set; }
        public SqlInt16 GENBA_TEIKI_KEIJYOU_KBN { get; set; }

        // オプション項目
        public SqlInt16 MAX_INSERT_CAPACITY { get; set; }
        public SqlDecimal MAX_FILE_SIZE { get; set; }
        public string DB_FILE_CONNECT { get; set; }
        public string DB_INXS_SUBAPP_CONNECT_STRING { get; set; }
        public string DB_INXS_SUBAPP_CONNECT_NAME { get; set; }

        //履歴データ削除
        public SqlInt16 HISTORY_DELETE_RANGE { get; set; }


        // mapbox
        public string MAPBOX_ACCESS_TOKEN { get; set; }
        public string MAPBOX_MAP_STYLE { get; set; }

        public SqlInt16 HAISHA_HENKOU_SAGYOU_DATE_KBN { get; set; }

        public string TORIHIKISAKI_KEISHOU1 { get; set; }
        public string TORIHIKISAKI_KEISHOU2 { get; set; }
        public string GYOUSHA_KEISHOU1 { get; set; }
        public string GYOUSHA_KEISHOU2 { get; set; }
        public string GENBA_KEISHOU1 { get; set; }
        public string GENBA_KEISHOU2 { get; set; }

        // 計票票バーコード
        public SqlInt16 KEIRYOU_BARCODE_KBN { get; set; }

        //CongBinh 20210712 #152799 S
        public SqlInt16 SUPPORT_KBN { get; set; }
        public string SUPPORT_URL { get; set; }
        //CongBinh 20210712 #152799 E

        //QN_QUAN add 20211229 #158952 S
        public string DB_LOG_CONNECT { get; set; }

        //QN_QUAN add 20211229 #158952 E
        public string MOBILE_SYSTEM_SETTEI_OPEN_PASSWORD { get; set; }

        //160029 S
        public SqlInt16 SYS_BARCODO_SHINKAKU_KBN { get; set; }
        public SqlInt16 UKEIRE_BARCODE_JOUDAN_KBN { get; set; }
        public SqlInt16 UKEIRE_BARCODE_CHUUDAN_KBN { get; set; }
        public SqlInt16 SHUKKA_BARCODE_JOUDAN_KBN { get; set; }
        public SqlInt16 SHUKKA_BARCODE_CHUUDAN_KBN { get; set; }
        public SqlInt16 KENSHUU_BARCODE_JOUDAN_KBN { get; set; }
        public SqlInt16 KENSHUU_BARCODE_CHUUDAN_KBN { get; set; }
        public SqlInt16 UR_SH_BARCODE_JOUDAN_KBN { get; set; }
        public SqlInt16 UR_SH_BARCODE_CHUUDAN_KBN { get; set; }
        //160029 E

        //160028 S
        public SqlInt16 SEIKYUU_KAISHUU_BETSU_KBN { get; set; }
        public SqlInt16 SEIKYUU_KAISHUU_BETSU_NICHIGO { get; set; }
        public SqlInt16 SHIHARAI_KAISHUU_BETSU_KBN { get; set; }
        public SqlInt16 SHIHARAI_KAISHUU_BETSU_NICHIGO { get; set; }
        //160028 E

        //PhuocLoc 2022/01/04 #158897, #158898 -Start
        public string SECRET_KEY { get; set; }
        public string CUSTOMER_ID { get; set; }
        public string WAN_SIGN_BIKOU_1 { get; set; }
        public string WAN_SIGN_BIKOU_2 { get; set; }
        public string WAN_SIGN_BIKOU_3 { get; set; }
        public SqlInt16 WAN_SIGN_FIELD_1 { get; set; }
        public SqlInt16 WAN_SIGN_FIELD_2 { get; set; }
        public SqlInt16 WAN_SIGN_FIELD_3 { get; set; }
        public SqlInt16 WAN_SIGN_FIELD_4 { get; set; }
        public SqlInt16 WAN_SIGN_FIELD_5 { get; set; }
        public string WAN_SIGN_FIELD_NAME_1 { get; set; }
        public string WAN_SIGN_FIELD_NAME_2 { get; set; }
        public string WAN_SIGN_FIELD_NAME_3 { get; set; }
        public string WAN_SIGN_FIELD_NAME_4 { get; set; }
        public string WAN_SIGN_FIELD_NAME_5 { get; set; }
        public SqlInt16 WAN_SIGN_FIELD_ATTRIBUTE_1 { get; set; }
        public SqlInt16 WAN_SIGN_FIELD_ATTRIBUTE_2 { get; set; }
        public SqlInt16 WAN_SIGN_FIELD_ATTRIBUTE_3 { get; set; }
        public SqlInt16 WAN_SIGN_FIELD_ATTRIBUTE_4 { get; set; }
        public SqlInt16 WAN_SIGN_FIELD_ATTRIBUTE_5 { get; set; }
        //PhuocLoc 2022/01/04 #158897, #158898 -End

        // 空電プッシュ（ショートメッセージオプション）
        public string KARADEN_ACCESS_KEY { get; set; }
        public string KARADEN_SECURITY_CODE { get; set; }
        public int KARADEN_MAX_WORD_COUNT { get; set;}

        public int SMS_ALERT_CHARACTER_LIMIT { get; set;}
        public SqlInt16 SMS_SEND_JOKYO { get; set; }
        public SqlInt16 SMS_DENPYOU_SHURUI { get; set; }
        public SqlInt16 SMS_HAISHA_JOKYO { get; set; }

        public string SMS_GREETINGS { get; set; }
        public string SMS_SIGNATURE { get; set; }

        // 楽楽明細連携
        public SqlInt16 RAKURAKU_CODE_NUMBERING_KBN { get; set; }

        //20250303
        public string SHONIN_RAN_1 { get; set; }
        public string SHONIN_RAN_2 { get; set; }
        public string SHONIN_RAN_3 { get; set; }
        public string SHONIN_RAN_4 { get; set; }
        public string SHONIN_RAN_5 { get; set; }
        public string SHONIN_RAN_6 { get; set; }
        public string SHONIN_RAN_7 { get; set; }
        public string SHONIN_RAN_8 { get; set; }
        public string SHONIN_RAN_9 { get; set; }
        public string SHONIN_RAN_10 { get; set; }

        public SqlInt16 HIDZUKE_INJI_CHECK { get; set; }
        
        //20250304
        public SqlDateTime KOKAI_KIKAN_FROM { get; set; }
        public SqlDateTime KOKAI_KIKAN_TO { get; set; }
        public string KEIJIBAN_TEKISUTO_BOKKUSU { get; set; }

        public SqlBoolean KEIRYO_ORIJINARU { get; set; }

        //20250305
        public string FURIKOMI_BANK_CD { get; set; }
        public string FURIKOMI_BANK_SHITEN_CD { get; set; }
        public string KOUZA_SHURUI { get; set; }
        public string KOUZA_NO { get; set; }
        public string KOUZA_NAME { get; set; }

        //20250306
        public string FURIKOMI_BANK_NAME { get; set; }
        public string FURIKOMI_BANK_SHITEN_NAME { get; set; }

        //20250307
        public SqlBoolean HIDZUKE_CHECK { get; set; }
        public SqlBoolean SHORI_RAIN_CHECK { get; set; }

        //20250318
        public SqlInt16 GURUPU_NYURYOKU_DEFAULT { get; set; }

        //20250416
        public string MITSUMORI_SUBJECT_DEFAULT_1 { get; set; }

        public string MITSUMORI_KOUMOKU5 { get; set; }
        public string MITSUMORI_NAIYOU5 { get; set; }

    }
}