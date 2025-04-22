using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Utility;

namespace Shougun.Core.Common.MeisaihyoSyukeihyoJokenShiteiPopup
{
    #region - Classes -

    #region - CommonChouhyouBase -

    /// <summary>共通帳票を表すベースクラス</summary>
    public class CommonChouhyouBase
    {
        #region - Fields -

        /// <summary>テンプレートパスを保持するフィールド</summary>
        protected const string TemplatePath = @"..\..\..\Template\";

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="CommonChouhyouBase"/>class.</summary>
        /// <param name="windowID">画面ＩＤ</param>
        public CommonChouhyouBase(WINDOW_ID windowID)
        {
            #region - フィールド情報 -

            /// <summary>フィールド情報を保持するフィールド</summary>
            this.FieldInfo = new Dictionary<string, List<string>>()
            {
                {
                    "T_UKEIRE_ENTRY",

                    new List<string>()
                    {
                        "TAIRYUU_KBN",
                        "KYOTEN_CD",
                        "UKEIRE_NUMBER",
                        "DATE_NUMBER",
                        "YEAR_NUMBER",
                        "KAKUTEI_KBN",
                        "DENPYOU_DATE",
                        "URIAGE_DATE",
                        "SHIHARAI_DATE",
                        "TORIHIKISAKI_CD",
                        "TORIHIKISAKI_NAME",
                        "GYOUSHA_CD",
                        "GYOUSHA_NAME",
                        "GENBA_CD",
                        "GENBA_NAME",
                        "NIOROSHI_GYOUSHA_CD",
                        "NIOROSHI_GYOUSHA_NAME",
                        "NIOROSHI_GENBA_CD",
                        "NIOROSHI_GENBA_NAME",
                        "EIGYOU_TANTOUSHA_CD",
                        "EIGYOU_TANTOUSHA_NAME",
                        "NYUURYOKU_TANTOUSHA_CD",
                        "NYUURYOKU_TANTOUSHA_NAME",
                        "SHARYOU_CD",
                        "SHARYOU_NAME",
                        "SHASHU_CD",
                        "SHASHU_NAME",
                        "UNPAN_GYOUSHA_CD",
                        "UNPAN_GYOUSHA_NAME",
                        "UNTENSHA_NAME",
                        "NINZUU_CNT",
                        "KEITAI_KBN_CD",
                        "DAIKAN_KBN",
                        "CONTENA_SOUSA_CD",
                        "MANIFEST_SHURUI_CD",
                        "MANIFEST_TEHAI_CD",
                        "DENPYOU_BIKOU",
                        "TAIRYUU_BIKOU",
                        "UKETSUKE_NUMBER",
                        "KEIRYOU_NUMBER",
                        "RECEIPT_NUMBER",
                        "NET_TOTAL",
                        "URIAGE_SHOUHIZEI_RATE",
                        "URIAGE_KINGAKU_TOTAL",
                        "URIAGE_TAX_SOTO",
                        "URIAGE_TAX_UCHI",
                        "URIAGE_TAX_SOTO_TOTAL",
                        "URIAGE_TAX_UCHI_TOTAL",
                        "HINMEI_URIAGE_KINGAKU_TOTAL",
                        "HINMEI_URIAGE_TAX_SOTO_TOTAL",
                        "HINMEI_URIAGE_TAX_UCHI_TOTAL",
                        "SHIHARAI_SHOUHIZEI_RATE",
                        "SHIHARAI_KINGAKU_TOTAL",
                        "SHIHARAI_TAX_SOTO",
                        "SHIHARAI_TAX_UCHI",
                        "SHIHARAI_TAX_SOTO_TOTAL",
                        "SHIHARAI_TAX_UCHI_TOTAL",
                        "HINMEI_SHIHARAI_KINGAKU_TOTAL",
                        "HINMEI_SHIHARAI_TAX_SOTO_TOTAL",
                        "HINMEI_SHIHARAI_TAX_UCHI_TOTAL",
                        "URIAGE_ZEI_KEISAN_KBN_CD",
                        "URIAGE_ZEI_KBN_CD",
                        "URIAGE_TORIHIKI_KBN_CD",
                        "SHIHARAI_ZEI_KEISAN_KBN_CD",
                        "SHIHARAI_ZEI_KBN_CD",
                        "SHIHARAI_TORIHIKI_KBN_CD",
                    }
                },
                {
                    "T_UKEIRE_DETAIL",

                    new List<string>()
                    {
                        "UKEIRE_NUMBER",
                        "ROW_NO",
                        "KAKUTEI_KBN",
                        "URIAGESHIHARAI_DATE",
                        "STACK_JYUURYOU",
                        "EMPTY_JYUURYOU",
                        "WARIFURI_JYUURYOU",
                        "WARIFURI_PERCENT",
                        "CHOUSEI_JYUURYOU",
                        "CHOUSEI_PERCENT",
                        "YOUKI_CD",
                        "YOUKI_SUURYOU",
                        "YOUKI_JYUURYOU",
                        "DENPYOU_KBN_CD",
                        "HINMEI_CD",
                        "HINMEI_NAME",
                        "NET_JYUURYOU",
                        "SUURYOU",
                        "UNIT_CD",
                        "TANKA",
                        "KINGAKU",
                        "TAX_SOTO",
                        "TAX_UCHI",
                        "HINMEI_ZEI_KBN_CD",
                        "HINMEI_KINGAKU",
                        "HINMEI_TAX_SOTO",
                        "HINMEI_TAX_UCHI",
                        "MEISAI_BIKOU",
                        "NISUGATA_SUURYOU",
                        "NISUGATA_UNIT_CD",
                    }
                },
                {
                    "T_SHUKKA_ENTRY",

                    new List<string>()
                    {
                        "TAIRYUU_KBN",
                        "KYOTEN_CD",
                        "SHUKKA_NUMBER",
                        "DATE_NUMBER",
                        "YEAR_NUMBER",
                        "KAKUTEI_KBN",
                        "DENPYOU_DATE",
                        "URIAGE_DATE",
                        "SHIHARAI_DATE",
                        "TORIHIKISAKI_CD",
                        "TORIHIKISAKI_NAME",
                        "GYOUSHA_CD",
                        "GYOUSHA_NAME",
                        "GENBA_CD",
                        "GENBA_NAME",
                        "NIZUMI_GYOUSHA_CD",
                        "NIZUMI_GYOUSHA_NAME",
                        "NIZUMI_GENBA_CD",
                        "NIZUMI_GENBA_NAME",
                        "EIGYOU_TANTOUSHA_CD",
                        "EIGYOU_TANTOUSHA_NAME",
                        "NYUURYOKU_TANTOUSHA_CD",
                        "NYUURYOKU_TANTOUSHA_NAME",
                        "SHARYOU_CD",
                        "SHARYOU_NAME",
                        "SHASHU_CD",
                        "SHASHU_NAME",
                        "UNPAN_GYOUSHA_CD",
                        "UNPAN_GYOUSHA_NAME",
                        "UNTENSHA_CD",
                        "UNTENSHA_NAME",
                        "NINZUU_CNT",
                        "KEITAI_KBN_CD",
                        "DAIKAN_KBN",
                        "CONTENA_SOUSA_CD",
                        "MANIFEST_SHURUI_CD",
                        "MANIFEST_TEHAI_CD",
                        "DENPYOU_BIKOU",
                        "TAIRYUU_BIKOU",
                        "UKETSUKE_NUMBER",
                        "KEIRYOU_NUMBER",
                        "RECEIPT_NUMBER",
                        "NET_TOTAL",
                        "URIAGE_SHOUHIZEI_RATE",
                        "URIAGE_AMOUNT_TOTAL",
                        "URIAGE_TAX_SOTO",
                        "URIAGE_TAX_UCHI",
                        "URIAGE_TAX_SOTO_TOTAL",
                        "URIAGE_TAX_UCHI_TOTAL",
                        "HINMEI_URIAGE_KINGAKU_TOTAL",
                        "HINMEI_URIAGE_TAX_SOTO_TOTAL",
                        "HINMEI_URIAGE_TAX_UCHI_TOTAL",
                        "SHIHARAI_SHOUHIZEI_RATE",
                        "SHIHARAI_AMOUNT_TOTAL",
                        "SHIHARAI_TAX_SOTO",
                        "SHIHARAI_TAX_UCHI",
                        "SHIHARAI_TAX_SOTO_TOTAL",
                        "SHIHARAI_TAX_UCHI_TOTAL",
                        "HINMEI_SHIHARAI_KINGAKU_TOTAL",
                        "HINMEI_SHIHARAI_TAX_SOTO_TOTAL",
                        "HINMEI_SHIHARAI_TAX_UCHI_TOTAL",
                        "URIAGE_ZEI_KEISAN_KBN_CD",
                        "URIAGE_ZEI_KBN_CD",
                        "URIAGE_TORIHIKI_KBN_CD",
                        "SHIHARAI_ZEI_KEISAN_KBN_CD",
                        "SHIHARAI_ZEI_KBN_CD",
                        "SHIHARAI_TORIHIKI_KBN_CD",
                        "KENSHU_DATE",
                        "SHUKKA_NET_TOTAL",
                        "KENSHU_NET_TOTAL",
                        "SABUN",
                        "SHUKKA_KINGAKU_TOTAL",
                        "KENSHU_KINGAKU_TOTAL",
                        "SAGAKU",
                    }
                },
                {
                    "T_SHUKKA_DETAIL",

                    new List<string>()
                    {
                        "SHUKKA_NUMBER",
                        "ROW_NO",
                        "KAKUTEI_KBN",
                        "URIAGESHIHARAI_DATE",
                        "STACK_JYUURYOU",
                        "EMPTY_JYUURYOU",
                        "WARIFURI_JYUURYOU",
                        "WARIFURI_PERCENT",
                        "CHOUSEI_JYUURYOU",
                        "CHOUSEI_PERCENT",
                        "YOUKI_CD",
                        "YOUKI_SUURYOU",
                        "YOUKI_JYUURYOU",
                        "DENPYOU_KBN_CD",
                        "HINMEI_CD",
                        "HINMEI_NAME",
                        "NET_JYUURYOU",
                        "SUURYOU",
                        "UNIT_CD",
                        "TANKA",
                        "KINGAKU",
                        "TAX_SOTO",
                        "TAX_UCHI",
                        "HINMEI_ZEI_KBN_CD",
                        "HINMEI_KINGAKU",
                        "HINMEI_TAX_SOTO",
                        "HINMEI_TAX_UCHI",
                        "MEISAI_BIKOU",
                        "NISUGATA_SUURYOU",
                        "NISUGATA_UNIT_CD",
                    }
                },
                {
                    "T_UR_SH_ENTRY",

                    new List<string>()
                    {
                        "KYOTEN_CD",
                        "UR_SH_NUMBER",
                        "DATE_NUMBER",
                        "YEAR_NUMBER",
                        "KAKUTEI_KBN",
                        "DENPYOU_DATE",
                        "URIAGE_DATE",
                        "SHIHARAI_DATE",
                        "TORIHIKISAKI_CD",
                        "TORIHIKISAKI_NAME",
                        "GYOUSHA_CD",
                        "GYOUSHA_NAME",
                        "GENBA_CD",
                        "GENBA_NAME",
                        "NIZUMI_GYOUSHA_CD",
                        "NIZUMI_GYOUSHA_NAME",
                        "NIZUMI_GENBA_CD",
                        "NIZUMI_GENBA_NAME",
                        "NIOROSHI_GYOUSHA_CD",
                        "NIOROSHI_GYOUSHA_NAME",
                        "NIOROSHI_GENBA_CD",
                        "NIOROSHI_GENBA_NAME",
                        "EIGYOU_TANTOUSHA_CD",
                        "EIGYOU_TANTOUSHA_NAME",
                        "NYUURYOKU_TANTOUSHA_CD",
                        "NYUURYOKU_TANTOUSHA_NAME",
                        "SHARYOU_CD",
                        "SHARYOU_NAME",
                        "SHASHU_CD",
                        "SHASHU_NAME",
                        "UNPAN_GYOUSHA_CD",
                        "UNPAN_GYOUSHA_NAME",
                        "UNTENSHA_CD",
                        "UNTENSHA_NAME",
                        "NINZUU_CNT",
                        "KEITAI_KBN_CD",
                        "CONTENA_SOUSA_CD",
                        "MANIFEST_SHURUI_CD",
                        "MANIFEST_TEHAI_CD",
                        "DENPYOU_BIKOU",
                        "UKETSUKE_NUMBER",
                        "RECEIPT_NUMBER",
                        "URIAGE_SHOUHIZEI_RATE",
                        "URIAGE_AMOUNT_TOTAL",
                        "URIAGE_TAX_SOTO",
                        "URIAGE_TAX_UCHI",
                        "URIAGE_TAX_SOTO_TOTAL",
                        "URIAGE_TAX_UCHI_TOTAL",
                        "HINMEI_URIAGE_KINGAKU_TOTAL",
                        "HINMEI_URIAGE_TAX_SOTO_TOTAL",
                        "HINMEI_URIAGE_TAX_UCHI_TOTAL",
                        "SHIHARAI_SHOUHIZEI_RATE",
                        "SHIHARAI_AMOUNT_TOTAL",
                        "SHIHARAI_TAX_SOTO",
                        "SHIHARAI_TAX_UCHI",
                        "SHIHARAI_TAX_SOTO_TOTAL",
                        "SHIHARAI_TAX_UCHI_TOTAL",
                        "HINMEI_SHIHARAI_KINGAKU_TOTAL",
                        "HINMEI_SHIHARAI_TAX_SOTO_TOTAL",
                        "HINMEI_SHIHARAI_TAX_UCHI_TOTAL",
                        "URIAGE_ZEI_KEISAN_KBN_CD",
                        "URIAGE_ZEI_KBN_CD",
                        "URIAGE_TORIHIKI_KBN_CD",
                        "SHIHARAI_ZEI_KEISAN_KBN_CD",
                        "SHIHARAI_ZEI_KBN_CD",
                        "SHIHARAI_TORIHIKI_KBN_CD",
                        "TSUKI_CREATE_KBN",
                    }
                },
                {
                    "T_UR_SH_DETAIL",

                    new List<string>()
                    {
                        "UR_SH_NUMBER",
                        "ROW_NO",
                        "KAKUTEI_KBN",
                        "URIAGESHIHARAI_DATE",
                        "DENPYOU_KBN_CD",
                        "HINMEI_CD",
                        "HINMEI_NAME",
                        "SUURYOU",
                        "UNIT_CD",
                        "TANKA",
                        "KINGAKU",
                        "TAX_SOTO",
                        "TAX_UCHI",
                        "HINMEI_ZEI_KBN_CD",
                        "HINMEI_KINGAKU",
                        "HINMEI_TAX_SOTO",
                        "HINMEI_TAX_UCHI",
                        "MEISAI_BIKOU",
                        "NISUGATA_SUURYOU",
                        "NISUGATA_UNIT_CD",
                    }
                },
                {
                    "T_NYUUKIN_ENTRY",

                    new List<string>()
                    {
                        "KYOTEN_CD",
                        "NYUUKIN_NUMBER",
                        "DENPYOU_DATE",
                        "TORIHIKISAKI_CD",
                        "NYUUKINSAKI_CD",
                        "BANK_CD",
                        "BANK_SHITEN_CD",
                        "KOUZA_SHURUI",
                        "KOUZA_NO",
                        "KOUZA_NAME",
                        "EIGYOU_TANTOUSHA_CD",
                        "KARIUKEKIN",
                        "DENPYOU_BIKOU",
                        "NYUUKIN_AMOUNT_TOTAL",
                        "CHOUSEI_AMOUNT_TOTAL",
                        "KARIUKEKIN_WARIATE_TOTAL",
                        "CHOUSEI_DENPYOU_KBN",
                    }
                },
                {
                    "T_SHUKKIN_ENTRY",

                    new List<string>()
                    {
                        "KYOTEN_CD",
                        "SHUKKIN_NUMBER",
                        "DENPYOU_DATE",
                        "SHUKKINSAKI_CD",
                        "EIGYOU_TANTOUSHA_CD",
                        "DENPYOU_BIKOU",
                        "SHUKKIN_AMOUNT_TOTAL",
                        "CHOUSEI_AMOUNT_TOTAL",
                        "TORIHIKISAKI_CD",
                    }
                },
                {
                    "T_SEIKYUU_DENPYOU",

                    new List<string>()
                    {
                        "KYOTEN_CD",
                        "SHIMEBI",
                        "TORIHIKISAKI_CD",
                        "SHOSHIKI_KBN",
                        "SHOSHIKI_MEISAI_KBN",
                        "SEIKYUU_KEITAI_KBN",
                        "NYUUKIN_MEISAI_KBN",
                        "YOUSHI_KBN",
                        "SEIKYUU_DATE",
                        "NYUUKIN_YOTEI_BI",
                        "ZENKAI_KURIKOSI_GAKU",
                        "KONKAI_NYUUKIN_GAKU",
                        "KONKAI_CHOUSEI_GAKU",
                        "KONKAI_URIAGE_GAKU",
                        "KONKAI_SEI_UTIZEI_GAKU",
                        "KONKAI_SEI_SOTOZEI_GAKU",
                        "KONKAI_DEN_UTIZEI_GAKU",
                        "KONKAI_DEN_SOTOZEI_GAKU",
                        "KONKAI_MEI_UTIZEI_GAKU",
                        "KONKAI_MEI_SOTOZEI_GAKU",
                        "KONKAI_SEIKYU_GAKU",
                        "FURIKOMI_BANK_CD",
                        "FURIKOMI_BANK_NAME",
                        "FURIKOMI_BANK_SHITEN_CD",
                        "FURIKOMI_BANK_SHITEN_NAME",
                        "KOUZA_SHURUI",
                        "KOUZA_NO",
                        "KOUZA_NAME",
                        "HAKKOU_KBN",
                        "SHIME_JIKKOU_NO",
                    }
                },
                {
                    "T_SEISAN_DENPYOU",

                    new List<string>()
                    {
                        "KYOTEN_CD",
                        "TORIHIKISAKI_CD",
                        "SHIMEBI",
                        "SHOSHIKI_KBN",
                        "SHOSHIKI_MEISAI_KBN",
                        "SHIHARAI_KEITAI_KBN",
                        "SHUKKIN_MEISAI_KBN",
                        "YOUSHI_KBN",
                        "SEISAN_DATE",
                        "SHUKKIN_YOTEI_BI",
                        "ZENKAI_KURIKOSI_GAKU",
                        "KONKAI_SHUKKIN_GAKU",
                        "KONKAI_CHOUSEI_GAKU",
                        "KONKAI_SHIHARAI_GAKU",
                        "KONKAI_SEI_UTIZEI_GAKU",
                        "KONKAI_SEI_SOTOZEI_GAKU",
                        "KONKAI_DEN_UTIZEI_GAKU",
                        "KONKAI_DEN_SOTOZEI_GAKU",
                        "KONKAI_MEI_UTIZEI_GAKU",
                        "KONKAI_MEI_SOTOZEI_GAKU",
                        "HAKKOU_KBN",
                        "SHIME_JIKKOU_NO",
                        "KONKAI_SEISAN_GAKU",
                    }
                },
                {
                    "T_UKETSUKE_SS_ENTRY",

                    new List<string>()
                    {
                        "KYOTEN_CD",
                        "UKETSUKE_NUMBER",
                        "UKETSUKE_DATE",
                        "HAISHA_JOKYO_CD",
                        "HAISHA_JOKYO_NAME",
                        "HAISHA_SHURUI_CD",
                        "HAISHA_SHURUI_NAME",
                        "TORIHIKISAKI_CD",
                        "TORIHIKISAKI_NAME",
                        "GYOUSHA_CD",
                        "GYOUSHA_NAME",
                        "GYOSHA_TEL",
                        "GENBA_CD",
                        "GENBA_NAME",
                        "GENBA_TEL",
                        "TANTOSHA_NAME",
                        "TANTOSHA_TEL",
                        "UNPAN_GYOUSHA_CD",
                        "UNPAN_GYOUSHA_NAME",
                        "NIOROSHI_GYOUSHA_CD",
                        "NIOROSHI_GYOUSHA_NAME",
                        "NIOROSHI_GENBA_CD",
                        "NIOROSHI_GENBA_NAME",
                        "EIGYOU_TANTOUSHA_CD",
                        "EIGYOU_TANTOUSHA_NAME",
                        "NIOROSHI_DATE",
                        "SAGYOU_DATE",
                        "SAGYOU_DATE_BEGIN",
                        "SAGYOU_DATE_END",
                        "GENCHAKU_TIME_CD",
                        "GENCHAKU_TIME_NAME",
                        "GENCHAKU_TIME",
                        "SAGYOU_TIME",
                        "SAGYOU_TIME_BEGIN",
                        "SAGYOU_TIME_END",
                        "SHASHU_DAISU_GROUP_NUMBER",
                        "SHASHU_DAISU_NUMBER",
                        "SHARYOU_CD",
                        "SHARYOU_NAME",
                        "SHASHU_CD",
                        "SHASHU_NAME",
                        "UNTENSHA_CD",
                        "UNTENSHA_NAME",
                        "HOJOIN_CD",
                        "HOJOIN_NAME",
                        "MANIFEST_SHURUI_CD",
                        "MANIFEST_TEHAI_CD",
                        "CONTENA_SOUSA_CD",
                        "COURSE_KUMIKOMI_CD",
                        "COURSE_NAME_CD",
                        "HAISHA_SIJISHO_FLG",
                        "MAIL_SEND_FLG",
                        "UKETSUKE_BIKOU1",
                        "UKETSUKE_BIKOU2",
                        "UKETSUKE_BIKOU3",
                        "UNTENSHA_SIJIJIKOU1",
                        "UNTENSHA_SIJIJIKOU2",
                        "UNTENSHA_SIJIJIKOU3",
                        "KINGAKU_TOTAL",
                        "SHOUHIZEI_RATE",
                        "TAX_SOTO",
                        "TAX_UCHI",
                        "TAX_SOTO_TOTAL",
                        "TAX_UCHI_TOTAL",
                        "SHOUHIZEI_TOTAL",
                        "GOUKEI_KINGAKU_TOTAL",
                    }
                },
                {
                    "T_UKETSUKE_SK_ENTRY",

                    new List<string>()
                    {
                        "KYOTEN_CD",
                        "UKETSUKE_NUMBER",
                        "UKETSUKE_DATE",
                        "HAISHA_JOKYO_CD",
                        "HAISHA_JOKYO_NAME",
                        "HAISHA_SHURUI_CD",
                        "HAISHA_SHURUI_NAME",
                        "TORIHIKISAKI_CD",
                        "TORIHIKISAKI_NAME",
                        "GYOUSHA_CD",
                        "GYOUSHA_NAME",
                        "GYOSHA_TEL",
                        "GENBA_CD",
                        "GENBA_NAME",
                        "GENBA_TEL",
                        "TANTOSHA_NAME",
                        "TANTOSHA_TEL",
                        "UNPAN_GYOUSHA_CD",
                        "UNPAN_GYOUSHA_NAME",
                        "NIZUMI_GYOUSHA_CD",
                        "NIZUMI_GYOUSHA_NAME",
                        "NIZUMI_GENBA_CD",
                        "NIZUMI_GENBA_NAME",
                        "EIGYOU_TANTOUSHA_CD",
                        "EIGYOU_TANTOUSHA_NAME",
                        "NIZUMI_DATE",
                        "SAGYOU_DATE",
                        "SAGYOU_DATE_BEGIN",
                        "SAGYOU_DATE_END",
                        "GENCHAKU_TIME_CD",
                        "GENCHAKU_TIME_NAME",
                        "GENCHAKU_TIME",
                        "SAGYOU_TIME",
                        "SAGYOU_TIME_BEGIN",
                        "SAGYOU_TIME_END",
                        "SHASHU_DAISU_GROUP_NUMBER",
                        "SHASHU_DAISU_NUMBER",
                        "SHARYOU_CD",
                        "SHARYOU_NAME",
                        "SHASHU_CD",
                        "SHASHU_NAME",
                        "UNTENSHA_CD",
                        "UNTENSHA_NAME",
                        "HOJOIN_CD",
                        "HOJOIN_NAME",
                        "MANIFEST_SHURUI_CD",
                        "MANIFEST_TEHAI_CD",
                        "COURSE_KUMIKOMI_CD",
                        "COURSE_NAME_CD",
                        "HAISHA_SIJISHO_FLG",
                        "MAIL_SEND_FLG",
                        "UKETSUKE_BIKOU1",
                        "UKETSUKE_BIKOU2",
                        "UKETSUKE_BIKOU3",
                        "UNTENSHA_SIJIJIKOU1",
                        "UNTENSHA_SIJIJIKOU2",
                        "UNTENSHA_SIJIJIKOU3",
                        "KINGAKU_TOTAL",
                        "SHOUHIZEI_RATE",
                        "TAX_SOTO",
                        "TAX_UCHI",
                        "TAX_SOTO_TOTAL",
                        "TAX_UCHI_TOTAL",
                        "SHOUHIZEI_TOTAL",
                        "GOUKEI_KINGAKU_TOTAL",
                    }
                },
                {
                    "T_UKETSUKE_MK_ENTRY",

                    new List<string>()
                    {
                        "KYOTEN_CD",
                        "UKETSUKE_NUMBER",
                        "UKETSUKE_DATE",
                        "TORIHIKISAKI_CD",
                        "TORIHIKISAKI_NAME",
                        "GYOUSHA_CD",
                        "GYOUSHA_NAME",
                        "GYOSHA_TEL",
                        "GENBA_CD",
                        "GENBA_NAME",
                        "GENBA_TEL",
                        "TANTOSHA_NAME",
                        "TANTOSHA_TEL",
                        "UNPAN_GYOUSHA_CD",
                        "UNPAN_GYOUSHA_NAME",
                        "NIOROSHI_GYOUSHA_CD",
                        "NIOROSHI_GYOUSHA_NAME",
                        "NIOROSHI_GENBA_CD",
                        "NIOROSHI_GENBA_NAME",
                        "EIGYOU_TANTOUSHA_CD",
                        "EIGYOU_TANTOUSHA_NAME",
                        "SAGYOU_DATE",
                        "GENCHAKU_TIME_CD",
                        "GENCHAKU_TIME_NAME",
                        "GENCHAKU_TIME",
                        "SHASHU_DAISU_GROUP_NUMBER",
                        "SHASHU_DAISU_NUMBER",
                        "SHARYOU_CD",
                        "SHARYOU_NAME",
                        "SHASHU_CD",
                        "SHASHU_NAME",
                        "UKETSUKE_BIKOU1",
                        "UKETSUKE_BIKOU2",
                        "UKETSUKE_BIKOU3",
                        "KINGAKU_TOTAL",
                        "SHOUHIZEI_RATE",
                        "TAX_SOTO",
                        "TAX_UCHI",
                        "TAX_SOTO_TOTAL",
                        "TAX_UCHI_TOTAL",
                        "SHOUHIZEI_TOTAL",
                        "GOUKEI_KINGAKU_TOTAL",
                    }
                },
                {
                    "T_UKETSUKE_BP_ENTRY",

                    new List<string>()
                    {
                        "KYOTEN_CD",
                        "UKETSUKE_NUMBER",
                        "UKETSUKE_DATE",
                        "HAISHA_JOKYO_CD",
                        "HAISHA_JOKYO_NAME",
                        "HAISHA_SHURUI_CD",
                        "HAISHA_SHURUI_NAME",
                        "TORIHIKISAKI_CD",
                        "TORIHIKISAKI_NAME",
                        "GYOUSHA_CD",
                        "GYOUSHA_NAME",
                        "GYOSHA_TEL",
                        "GENBA_CD",
                        "GENBA_NAME",
                        "GENBA_TEL",
                        "TANTOSHA_NAME",
                        "TANTOSHA_TEL",
                        "EIGYOU_TANTOUSHA_CD",
                        "EIGYOU_TANTOUSHA_NAME",
                        "NOHIN_YOTEI_DATE",
                        "UNPAN_GYOUSHA_CD",
                        "UNPAN_GYOUSHA_NAME",
                        "SHARYOU_CD",
                        "SHARYOU_NAME",
                        "SHASHU_CD",
                        "SHASHU_NAME",
                        "UNTENSHA_CD",
                        "UNTENSHA_NAME",
                        "HOJOIN_CD",
                        "HOJOIN_NAME",
                        "BP_ZAIKO_M_CD",
                        "BP_ZAIKO_M_NAME",
                        "HAISHA_SIJISHO_FLG",
                        "MAIL_SEND_FLG",
                        "UKETSUKE_BIKOU1",
                        "UKETSUKE_BIKOU2",
                        "UKETSUKE_BIKOU3",
                        "BIKOU1",
                        "BIKOU2",
                        "BIKOU3",
                        "UNTENSHA_SIJIJIKOU1",
                        "UNTENSHA_SIJIJIKOU2",
                        "UNTENSHA_SIJIJIKOU3",
                        "KINGAKU_TOTAL",
                        "SHOUHIZEI_RATE",
                        "TAX_SOTO",
                        "TAX_UCHI",
                        "TAX_SOTO_TOTAL",
                        "TAX_UCHI_TOTAL",
                        "SHOUHIZEI_TOTAL",
                        "GOUKEI_KINGAKU_TOTAL",
                    }
                },
                {
                    "T_UNCHIN_ENTRY",

                    new List<string>()
                    {
                        "KYOTEN_CD",
                        "RENKEI_DENSHU_KBN_CD",
                        "RENKEI_SYSTEM_ID",
                        "DENPYOU_NUMBER",
                        "KAKUTEI_KBN",
                        "DENPYOU_DATE",
                        "URIAGE_DATE",
                        "SHIHARAI_DATE",
                        "TORIHIKISAKI_CD",
                        "TORIHIKISAKI_NAME",
                        "GYOUSHA_CD",
                        "GYOUSHA_NAME",
                        "UNPAN_GYOUSHA_CD",
                        "UNPAN_GYOUSHA_NAME",
                        "NIZUMI_GYOUSHA_CD",
                        "NIZUMI_GYOUSHA_NAME",
                        "NIZUMI_GENBA_CD",
                        "NIZUMI_GENBA_NAME",
                        "NIOROSHI_GYOUSHA_CD",
                        "NIOROSHI_GYOUSHA_NAME",
                        "NIOROSHI_GENBA_CD",
                        "NIOROSHI_GENBA_NAME",
                        "SHARYOU_CD",
                        "SHARYOU_NAME",
                        "SHASHU_CD",
                        "SHASHU_NAME",
                        "UNTENSHA_CD",
                        "UNTENSHA_NAME",
                        "TORIHIKI_KBN_CD",
                        "NET_TOTAL",
                        "URIAGE_SHOUHIZEI_RATE",
                        "URIAGE_KINGAKU_TOTAL",
                        "URIAGE_TAX_SOTO",
                        "URIAGE_TAX_UCHI",
                        "URIAGE_TAX_SOTO_TOTAL",
                        "URIAGE_TAX_UCHI_TOTAL",
                        "HINMEI_URIAGE_KINGAKU_TOTAL",
                        "HINMEI_URIAGE_TAX_SOTO_TOTAL",
                        "HINMEI_URIAGE_TAX_UCHI_TOTAL",
                        "SHIHARAI_SHOUHIZEI_RATE",
                        "SHIHARAI_KINGAKU_TOTAL",
                        "SHIHARAI_TAX_SOTO",
                        "SHIHARAI_TAX_UCHI",
                        "SHIHARAI_TAX_SOTO_TOTAL",
                        "SHIHARAI_TAX_UCHI_TOTAL",
                        "HINMEI_SHIHARAI_KINGAKU_TOTAL",
                        "HINMEI_SHIHARAI_TAX_SOTO_TOTAL",
                        "HINMEI_SHIHARAI_TAX_UCHI_TOTAL",
                        "URIAGE_ZEI_KEISAN_KBN_CD",
                        "URIAGE_ZEI_KBN_CD",
                        "URIAGE_TORIHIKI_KBN_CD",
                        "SHIHARAI_ZEI_KEISAN_KBN_CD",
                        "SHIHARAI_ZEI_KBN_CD",
                        "SHIHARAI_TORIHIKI_KBN_CD",
                    }
                },
                {
                    "T_KEIRYOU_ENTRY",

                    new List<string>()
                    {
                        "TAIRYUU_KBN",
                        "KYOTEN_CD",
                        "KEIRYOU_NUMBER",
                        "DATE_NUMBER",
                        "YEAR_NUMBER",
                        "KIHON_KEIRYOU",
                        "DENPYOU_DATE",
                        "TORIHIKISAKI_CD",
                        "TORIHIKISAKI_NAME",
                        "GYOUSHA_CD",
                        "GYOUSHA_NAME",
                        "GENBA_CD",
                        "GENBA_NAME",
                        "UNPAN_GYOUSHA_CD",
                        "UNPAN_GYOUSHA_NAME",
                        "NIZUMI_GYOUSHA_CD",
                        "NIZUMI_GYOUSHA_NAME",
                        "NIZUMI_GENBA_CD",
                        "NIZUMI_GENBA_NAME",
                        "NIOROSHI_GYOUSHA_CD",
                        "NIOROSHI_GYOUSHA_NAME",
                        "NIOROSHI_GENBA_CD",
                        "NIOROSHI_GENBA_NAME",
                        "EIGYOU_TANTOUSHA_CD",
                        "EIGYOU_TANTOUSHA_NAME",
                        "NYUURYOKU_TANTOUSHA_CD",
                        "NYUURYOKU_TANTOUSHA_NAME",
                        "SHARYOU_CD",
                        "SHARYOU_NAME",
                        "SHASHU_CD",
                        "SHASHU_NAME",
                        "UNTENSHA_CD",
                        "UNTENSHA_NAME",
                        "NINZUU_CNT",
                        "KEITAI_KBN_CD",
                        "CONTENA_SOUSA_CD",
                        "MANIFEST_SHURUI_CD",
                        "MANIFEST_TEHAI_CD",
                        "NET_TOTAL",
                        "DENPYOU_BIKOU",
                        "TAIRYUU_BIKOU",
                        "UKETSUKE_NUMBER",
                    }
                },
            };

            #endregion - フィールド情報 -

            this.UkeireEntryDao = DaoInitUtility.GetComponent<ITUkeireEntryDao>();
            this.ShukkaEntryDao = DaoInitUtility.GetComponent<ITShukkaEntryDao>();
            this.UriageShiharaiEntryDao = DaoInitUtility.GetComponent<ITUriageShiharaiEntryDao>();
            this.NyukinEntryDao = DaoInitUtility.GetComponent<ITNyukinEntryDao>();
            this.ShukkinEntryDao = DaoInitUtility.GetComponent<ITShukkinEntryDao>();
            this.SeikyuuDenpyouDao = DaoInitUtility.GetComponent<ITSeikyuuDenpyouDao>();
            this.SeisanDenpyouDao = DaoInitUtility.GetComponent<ITSeisanDenpyouDao>();
            this.MasterTorihikisakiDao = DaoInitUtility.GetComponent<IMTorihikisakiDao>();
            this.MasterTorihikiKbnDao = DaoInitUtility.GetComponent<IM_TORIHIKI_KBNDao>();
            this.MSysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            // 画面ＩＤ
            this.WindowID = windowID;

            // システム設定を取得し、保持しておく
            this.MSysInfo = this.MSysInfoDao.GetAllDataForCode("0");

            // リセット
            this.Reset();
        }

        #endregion - Constructors -

        #region - Enums -

        /// <summary>期間指定に関する列挙型</summary>
        public enum KIKAN_SHITEI_TYPE : int
        {
            /// <summary>当日に関する列挙型</summary>
            Toujitsu = 1,

            /// <summary>当月に関する列挙型</summary>
            Tougets,

            /// <summary>期間指定に関する列挙型</summary>
            Shitei,
        }

        /// <summary>伝票種類に関する列挙型</summary>
        public enum DENPYOU_SYURUI : int
        {
            /// <summary>受入に関する列挙型</summary>
            Ukeire = 1,

            /// <summary>収集に関する列挙型</summary>
            Syuusyuu = 1,

            /// <summary>出荷に関する列挙型</summary>
            Syutsuka = 2,

            /// <summary>売上／支払に関する列挙型</summary>
            UriageShiharai = 3,

            /// <summary>持込に関する列挙型</summary>
            Mochikomi = 3,

            /// <summary>全て</summary>
            Subete = 4,

            /// <summary>物販に関する列挙型</summary>
            Butsupan = 4,

            /// <summary>代納に関する列挙型</summary>
            Dainou = 4,

            /// <summary>運賃のみに関する列挙型</summary>
            UnchinNomi = 5,

            /// <summary>全てに関する列挙型</summary>
            Subete6 = 6,
        }

        /// <summary>伝票区分に関する列挙型</summary>
        public enum DENPYOU_KUBUN : int
        {
            /// <summary>売上に関する列挙型</summary>
            Uriage = 1,

            /// <summary>支払に関する列挙型</summary>
            Shiharai,

            /// <summary>全てに関する列挙型</summary>
            Subete,
        }

        #endregion - Enums -

        #region - Properties -

        /// <summary>受入エントリーDaoを保持するプロパティ</summary>
        public ITUkeireEntryDao UkeireEntryDao { get; set; }

        /// <summary>出荷エントリーDaoを保持するプロパティ</summary>
        public ITShukkaEntryDao ShukkaEntryDao { get; set; }

        /// <summary>売上支払エントリーDaoを保持するプロパティ</summary>
        public ITUriageShiharaiEntryDao UriageShiharaiEntryDao { get; set; }

        /// <summary>入金エントリーDaoを保持するプロパティ</summary>
        public ITNyukinEntryDao NyukinEntryDao { get; set; }

        /// <summary>出金エントリーDaoを保持するプロパティ</summary>
        public ITShukkinEntryDao ShukkinEntryDao { get; set; }

        /// <summary>請求伝票Daoを保持するプロパティ</summary>
        public ITSeikyuuDenpyouDao SeikyuuDenpyouDao { get; set; }

        /// <summary>精算伝票Daoを保持するプロパティ</summary>
        public ITSeisanDenpyouDao SeisanDenpyouDao { get; set; }

        /// <summary>取引先Daoを保持するプロパティ</summary>
        public IMTorihikisakiDao MasterTorihikisakiDao { get; set; }

        /// <summary>取引区分Daoを保持するプロパティ</summary>
        public IM_TORIHIKI_KBNDao MasterTorihikiKbnDao { get; set; }

        /// <summary>システム設定Daoを保持するプロパティ</summary>
        public IM_SYS_INFODao MSysInfoDao { get; set; }

        /// <summary>システム設定を保持するプロパティ</summary>
        public M_SYS_INFO MSysInfo { get; set; }

        /// <summary>会社略名を保持するプロパティ</summary>
        public string CorpRyakuName { get; set; }

        /// <summary>印刷開始日を保持するプロパティ</summary>
        public DateTime DateTimePrint { get; set; }

        /// <summary>帳票出力フルパスフォーム名を保持するプロパティ</summary>
        public string OutputFormFullPathName { get; set; }

        /// <summary>帳票出力フォームレイアウト名を保持するプロパティ</summary>
        public string OutputFormLayout { get; set; }

        /// <summary>集計項目を保持するプロパティ</summary>
        public List<SyuukeiKoumoku> SyuukeiKomokuList { get; private set; }

        /// <summary>画面ＩＤを保持するプロパティ</summary>
        public WINDOW_ID WindowID { get; internal set; }

        /// <summary>帳票名を保持するプロパティ</summary>
        public string Name { get; set; }

        /// <summary>期間指定（開始日）を保持するプロパティ</summary>
        public DateTime DateTimeStart { get; set; }

        /// <summary>期間指定（終了日）を保持するプロパティ</summary>
        public DateTime DateTimeEnd { get; set; }

        /// <summary>期間指定タイプを保持するプロパティ</summary>
        public KIKAN_SHITEI_TYPE KikanShiteiType { get; set; }

        /// <summary>拠点コードを保持するプロパティ</summary>
        public string KyotenCode { get; set; }

        /// <summary>拠点コード名を保持するプロパティ</summary>
        public string KyotenCodeName { get; set; }

        /// <summary>伝票種類を保持するプロパティ</summary>
        public DENPYOU_SYURUI DenpyouSyurui { get; set; }

        /// <summary>伝票種類のグループ化状態を保持するプロパティ</summary>
        /// <remarks>真の場合：グループ化有、偽の場合：グループ区分無</remarks>
        public bool IsDenpyouSyuruiGroupKubun { get; set; }

        /// <summary>伝票種類の有効・無効の状態を保持するプロパティ</summary>
        /// <remarks>真の場合：有効、偽の場合：無効</remarks>
        public bool IsDenpyouSyuruiEnable { get; protected set; }

        /// <summary>伝票種類名を保持するプロパティ</summary>
        public Dictionary<DENPYOU_SYURUI, string> DenpyouSyuruiName { get; protected set; }

        /// <summary>伝票区分の有効・無効の状態を保持するプロパティ</summary>
        /// <remarks>真の場合：有効、偽の場合：無効</remarks>
        public bool IsDenpyouKubunEnable { get; protected set; }

        /// <summary>伝票区分を保持するプロパティ</summary>
        public DENPYOU_KUBUN DenpyouKubun { get; set; }

        /// <summary>伝票区分名を保持するプロパティ</summary>
        public Dictionary<DENPYOU_KUBUN, string> DenpyouKubunName { get; protected set; }

        /// <summary>選択可能な集計項目グループ数を保持するプロパティ</summary>
        public int SelectEnableSyuukeiKoumokuGroupCount { get; set; }

        /// <summary>選択された集計項目を保持するプロパティ</summary>
        public List<int> SelectSyuukeiKoumokuList { get; set; }

        /// <summary>選択可能な集計項目を保持するプロパティ</summary>
        public List<int> SelectEnableSyuukeiKoumokuList { get; protected set; }

        /// <summary>出力可能項目（伝票）の有効・無効を保持するプロパティ</summary>
        public bool OutEnableKoumokuDenpyou { get; set; }

        /// <summary>出力可能項目（明細）の有効・無効を保持するプロパティ</summary>
        public bool OutEnableKoumokuMeisai { get; set; }

        /// <summary>選択可能な帳票出力項目（伝票）を保持するプロパティ</summary>
        public List<ChouhyouOutKoumokuGroup> SelectEnableChouhyouOutKoumokuDenpyouList { get; protected set; }

        /// <summary>選択された帳票出力項目（伝票）を保持するプロパティ</summary>
        public List<ChouhyouOutKoumokuGroup> SelectChouhyouOutKoumokuDepyouList { get; set; }

        /// <summary>選択可能な帳票出力項目（明細）を保持するプロパティ</summary>
        public List<ChouhyouOutKoumokuGroup> SelectEnableChouhyouOutKoumokuMeisaiList { get; protected set; }

        /// <summary>選択された帳票出力項目（明細）を保持するプロパティ</summary>
        public List<ChouhyouOutKoumokuGroup> SelectChouhyouOutKoumokuMeisaiList { get; set; }

        /// <summary>帳票用データテーブルを保持するプロパティ</summary>
        public DataTable ChouhyouDataTable { get; set; }

        /// <summary>対象テーブルリストを保持するフィールド</summary>
        public List<TaishouTable> TaishouTableList { get; protected set; }

        /// <summary>複数データーテーブル並べ替え結果を保持するプロパティ</summary>
        public DataTable DataTableMultiSort { get; protected set; }

        /// <summary>受渡用データーテーブルを保持するプロパティ</summary>
        public DataTable DataTableUkewatashi { get; protected set; }

        /// <summary>最大読み込み行数を保持するプロパティ</summary>
        public int MaxRowCount { get; private set; }

        /// <summary>一覧アラート件数を保持するプロパティ</summary>
        public int IchiranAlertCount { get; private set; }

        /// <summary>入力関連テーブル名を保持するプロパティ</summary>
        protected List<string> InKanrenTable { get; set; }

        /// <summary>入力関連データテーブルから取得したデータテーブルリストを保持するプロパティ</summary>
        protected List<DataTable> InDataTable { get; set; }

        /// <summary>出力関連テーブル名を保持するプロパティ</summary>
        protected List<string> OutKanrenTable { get; set; }

        /// <summary>出力関連データテーブルから取得したデータテーブルリストを保持するプロパティ</summary>
        protected DataTable OutDataTable { get; set; }

        /// <summary>帳票用データテーブルを保持するプロパティ</summary>
        protected DataTable[] InputDataTable { get; set; }

        /// <summary>フィールド情報を保持するプロパティ</summary>
        protected Dictionary<string, List<string>> FieldInfo { get; set; }

        /// <summary>取得した取引区分を保持するプロパティ</summary>
        public List<M_TORIHIKI_KBN> MTorihikiKbnList { get; set; }
        #endregion - Properties -

        #region - Methods -

        /// <summary>初期化処理を実行する</summary>
        public virtual void Initialize()
        {
            try
            {
                IS2Dao dao = this.UkeireEntryDao;
                DataTable dataTable = dao.GetDateForStringSql("SELECT M_CORP_INFO.CORP_RYAKU_NAME from M_CORP_INFO");

                this.CorpRyakuName = (string)dataTable.Rows[0].ItemArray[0];
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        /// <summary>リセット処理を実行する</summary>
        public void Reset()
        {
            try
            {
                #region - 集計項目 -

                // 集計項目
                this.SyuukeiKomokuList = new List<SyuukeiKoumoku>()
                {
                    #region - None -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.None,
                        0,
                        string.Empty,
                        WINDOW_ID.NONE,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        null,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        false,
                        new SyuukeiKoumokuHani()),

                    #endregion - None -

                    #region - 取引先別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.TorihikisakiBetsu,
                        6,
                        "取引先別",
                        WINDOW_ID.M_TORIHIKISAKI,
                        "TORIHIKISAKI_CD",
                        "TORIHIKISAKI_NAME_RYAKU",
                        "検索共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("取引先コードチェックandセッティング"),
                        },
                        "取引区分",
                        "取引区分",
                        "取引区分CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        true ,
                        new SyuukeiKoumokuHani()),

                    #endregion - 取引先別 -

                    #region - 業者別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.GyoshaBetsu,
                        6,
                        "業者別",
                        WINDOW_ID.M_GYOUSHA,
                        "GYOUSHA_CD",
                        "GYOUSHA_NAME_RYAKU",
                        "検索共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("業者マスタコードチェックandセッティング"),
                        },
                        "業者",
                        "業者",
                        "業者CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        true,
                        new SyuukeiKoumokuHani()),

                    #endregion - 業者別 -

                    #region - 現場別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.GenbaBetsu,
                        6,
                        "現場別",
                        WINDOW_ID.M_GENBA,
                        "GENBA_CD",
                        "GENBA_NAME_RYAKU",
                        "複数キー用検索共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("現場マスタコードチェックandセッティング"),
                        },
                        "現場",
                        "現場",
                        "現場CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        true,
                        new SyuukeiKoumokuHani()),

                    #endregion - 現場別 -

                    #region - 運搬業者別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.UnpanGyoshaBetsu,
                        6,
                        "運搬業者別",
                        WINDOW_ID.M_GYOUSHA,
                        "GYOUSHA_CD",
                        "GYOUSHA_NAME_RYAKU",
                        "検索共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("業者マスタコードチェックandセッティング"),
                        },
                        "業者",
                        "業者",
                        "業者CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        true,
                        new SyuukeiKoumokuHani()),

                    #endregion - 運搬業者別 -

                    #region - 荷降業者別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu,
                        6,
                        "荷降業者別",
                        WINDOW_ID.M_GYOUSHA,
                        "GYOUSHA_CD",
                        "GYOUSHA_NAME_RYAKU",
                        "検索共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("業者マスタコードチェックandセッティング"),
                        },
                        "業者",
                        "業者",
                        "業者CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        true,
                        new SyuukeiKoumokuHani()),

                #endregion - 荷降業者別 -

                    #region - 荷降現場別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu,
                        6,
                        "荷降現場別",
                        WINDOW_ID.M_GENBA,
                        "GENBA_CD",
                        "GENBA_NAME_RYAKU", "複数キー用検索共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("現場マスタコードチェックandセッティング"),
                        },
                        "現場",
                        "現場",
                        "現場CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        true,
                        new SyuukeiKoumokuHani()),

                    #endregion - 荷降現場別 -

                    #region - 荷積業者別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu,
                        6,
                        "荷積業者別",
                        WINDOW_ID.M_GYOUSHA,
                        "GYOUSHA_CD",
                        "GYOUSHA_NAME_RYAKU",
                        "検索共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("業者マスタコードチェックandセッティング"),
                        },
                        "業者",
                        "業者",
                        "業者CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        true,
                        new SyuukeiKoumokuHani()),

                    #endregion - 荷積業者別 -

                    #region - 荷積現場別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu,
                        6,
                        "荷積現場別",
                        WINDOW_ID.M_GENBA,
                        "GENBA_CD",
                        "GENBA_NAME_RYAKU",
                        "複数キー用検索共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("現場マスタコードチェックandセッティング"),
                        },
                        "現場",
                        "現場",
                        "現場CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        true,
                        new SyuukeiKoumokuHani()),

                    #endregion - 荷積現場別 -

                    #region - 営業担当者別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.EigyoTantoshaBetsu,
                        6,
                        "営業担当者別",
                        WINDOW_ID.M_SHAIN,
                        "SHAIN_CD",
                        "SHAIN_NAME_RYAKU",
                        "マスタ共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("営業担当者マスタコードチェックandセッティング"),
                        },
                        "営業担当者",
                        "営業担当者",
                        "営業担当者CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        true,
                        new SyuukeiKoumokuHani()),

                    #endregion - 営業担当者別 -

                    #region - 拠点別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.KyotenBetsu,
                        2,
                        "拠点別",
                        WINDOW_ID.M_KYOTEN,
                        "KYOTEN_CD",
                        "KYOTEN_NAME_RYAKU",
                        "マスタ共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("拠点マスタコードチェックandセッティング"),
                        },
                        "拠点",
                        "拠点",
                        "拠点CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        false,
                        new SyuukeiKoumokuHani()),

                    #endregion - 拠点別 -

                    #region - 種類別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.SyuruiBetsu,
                        3,
                        "種類別",
                        WINDOW_ID.M_SHURUI,
                        "SHURUI_CD",
                        "SHURUI_NAME_RYAKU",
                        "マスタ共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("種類マスタコードチェックandセッティング"),
                        },
                        "種類",
                        "種類",
                        "種類CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        true,
                        new SyuukeiKoumokuHani()),

                    #endregion - 種類別 -

                    #region - 分類別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.BunruiBetsu,
                        3,
                        "分類別",
                        WINDOW_ID.M_BUNRUI,
                        "BUNRUI_CD",
                        "BUNRUI_NAME_RYAKU",
                        "マスタ共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("分類マスタコードチェックandセッティング"),
                        },
                        "分類",
                        "分類",
                        "分類CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        true,
                        new SyuukeiKoumokuHani()),

                    #endregion - 分類別 -

                    #region - 品名別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.HinmeiBetsu,
                        6,
                        "品名別",
                        WINDOW_ID.M_HINMEI,
                        "HINMEI_CD",
                        "HINMEI_NAME_RYAKU",
                        "マスタ共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("品名コードチェックandセッティング"),
                        },
                        "品名",
                        "品名",
                        "品名CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        true,
                        new SyuukeiKoumokuHani()),

                    #endregion - 品名別 -

                    #region - 車種別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.ShasyuBetsu,
                        3,
                        "車種別",
                        WINDOW_ID.M_SHASHU,
                        "SHASHU_CD",
                        "SHASHU_NAME_RYAKU",
                        "マスタ共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("車種マスタコードチェックandセッティング"),
                        },
                        "車種",
                        "車種",
                        "車種CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        true,
                        new SyuukeiKoumokuHani()),

                    #endregion - 車種別 -

                    #region - 車輌別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.SharyoBetsu,
                        6,
                        "車輌別",
                        WINDOW_ID.M_SHARYOU,
                        "SHARYOU_CD",
                        "SHARYOU_NAME_RYAKU",
                        "車両選択共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("車輌コードチェックandセッティング"),
                        },
                        "車輌",
                        "車輌",
                        "車輌CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        true,
                        new SyuukeiKoumokuHani()),

                    #endregion - 車輌別 -

                    #region - 運転者別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.UntenshaBetsu,
                        6,
                        "運転者別",
                        WINDOW_ID.M_SHAIN,
                        "UNTENSHA_CD",
                        "SHAIN_NAME_RYAKU",
                        "マスタ共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("社員マスタコードチェックandセッティング"),
                        },
                        "運転者",
                        "運転者",
                        "運転者CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        true,
                        new SyuukeiKoumokuHani()),

                    #endregion - 運転者別 -

                    #region - 伝票区分別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.DenpyoKubunBetsu,
                        1,
                        "伝票区分別",
                        WINDOW_ID.M_DENPYOU_KBN,
                        "DENPYOU_KBN_CD",
                        "DENPYOU_KBN_NAME_RYAKU",
                        "マスタ共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("伝票区分コードチェックandセッティング"),
                        },
                        "伝票区分",
                        "伝票区分",
                        "伝票区分CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        false,
                        new SyuukeiKoumokuHani()),

                    #endregion - 伝票区分別 -

                    #region - 伝種区分別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.DensyuKubunBetsu,
                        1,
                        "伝種区分別",
                        WINDOW_ID.M_DENSHU_KBN,
                        "DENSHU_KBN_CD",
                        "DENSHU_KBN_NAME_RYAKU",
                        "マスタ共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("伝種マスタコードチェックandセッティング"),
                        },
                        "伝種区分",
                        "伝種区分",
                        "伝種区分CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        false,
                        new SyuukeiKoumokuHani()),

                    #endregion - 伝種区分別 -

                    #region - 入金先別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.NyukinsakiBetsu,
                        6,
                        "入金先別",
                        WINDOW_ID.M_NYUUKINSAKI,
                        "NYUUKINSAKI_CD",
                        "NYUUKINSAKI_NAME_RYAKU",
                        "検索共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("入金先マスタコードチェックandセッティング"),
                        },
                        "入金先",
                        "入金先",
                        "入金先CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        true,
                        new SyuukeiKoumokuHani()),

                    #endregion - 入金先別 -

                    #region - 銀行別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.GinkoBetsu,
                        4,
                        "銀行別",
                        WINDOW_ID.M_BANK,
                        "BANK_CD",
                        "BANK_NAME_RYAKU",
                        "マスタ共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("銀行マスタコードチェックandセッティング"),
                        },
                        "銀行",
                        "銀行",
                        "銀行CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        true,
                        new SyuukeiKoumokuHani()),

                    #endregion - 銀行別 -

                    #region - 銀行支店別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.GinkoShitenBetsu,
                        3,
                        "銀行支店別",
                        WINDOW_ID.M_BANK_SHITEN,
                        "BANK_SHITEN_CD",
                        "BANK_SHIETN_NAME_RYAKU",
                        "複数キー用検索共通ポップアップ",
                        new Collection<SelectCheckDto>
                        {
                            new SelectCheckDto("銀行支店マスタコードチェックandセッティング"),
                        },
                        "銀行支店",
                        "銀行支店",
                        "銀行支店CDを指定してください（スペースキー押下にて、検索画面を表示します）",
                        true,
                        new SyuukeiKoumokuHani()),

                    #endregion - 銀行支店別 -

                    #region - 日付別 -

                    new SyuukeiKoumoku(
                        SYUKEUKOMOKU_TYPE.HidukeBetsu,
                        0,
                        "日付別",
                        WINDOW_ID.NONE,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        null,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        false,
                        new SyuukeiKoumokuHani()),

                    #endregion - 日付別 -
                };

                #endregion - 集計項目 -

                // 帳票名
                this.Name = string.Empty;

                // 期間指定（開始日）
                this.DateTimeStart = DateTime.Now;

                // 期間指定（終了日）
                this.DateTimeEnd = DateTime.Now;

                // 期間指定タイプ
                this.KikanShiteiType = KIKAN_SHITEI_TYPE.Toujitsu;

                // 拠点コード
                this.KyotenCode = "99";

                // 拠点コード名
                this.KyotenCodeName = string.Empty;

                // 伝票種類グループ区分有無
                this.IsDenpyouSyuruiGroupKubun = false;

                switch (this.WindowID)
                {
                    case WINDOW_ID.R_URIAGE_MEISAIHYOU:                     // R358(売上明細表)
                        // 伝票種類の有効・無効
                        this.IsDenpyouSyuruiEnable = false;
                        // 伝票種類
                        this.DenpyouSyurui = DENPYOU_SYURUI.Subete;
                        // 伝票種類名
                        this.DenpyouSyuruiName = new Dictionary<DENPYOU_SYURUI, string>()
                        {
                            { DENPYOU_SYURUI.Ukeire, "受入" },
                            { DENPYOU_SYURUI.Syutsuka, "出荷" },
                            { DENPYOU_SYURUI.UriageShiharai, "売上／支払" },
                            { DENPYOU_SYURUI.Subete, "全て" },
                        };
                        // 伝票区分の有効・無効
                        this.IsDenpyouKubunEnable = false;
                        // 伝票区分
                        this.DenpyouKubun = DENPYOU_KUBUN.Uriage;
                        break;
                    case WINDOW_ID.R_SHIHARAI_MEISAIHYOU:                   // R362(支払明細表)
                        // 伝票種類の有効・無効
                        this.IsDenpyouSyuruiEnable = false;
                        // 伝票種類
                        this.DenpyouSyurui = DENPYOU_SYURUI.Subete;
                        // 伝票種類名
                        this.DenpyouSyuruiName = new Dictionary<DENPYOU_SYURUI, string>()
                        {
                            { DENPYOU_SYURUI.Ukeire, "受入" },
                            { DENPYOU_SYURUI.Syutsuka, "出荷" },
                            { DENPYOU_SYURUI.UriageShiharai, "売上／支払" },
                            { DENPYOU_SYURUI.Subete, "全て" },
                        };
                        // 伝票区分の有効・無効
                        this.IsDenpyouKubunEnable = false;
                        // 伝票区分
                        this.DenpyouKubun = DENPYOU_KUBUN.Shiharai;
                        break;
                    case WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU:            // R355(売上／支払明細表)
                    case WINDOW_ID.R_URIAGE_SYUUKEIHYOU:                    // R359(売上集計表)
                    case WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU:                  // R363(支払集計表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU:           // R356(売上／支払集計表)
                    case WINDOW_ID.R_NYUUKIN_MEISAIHYOU:                    // R366(入金明細表)
                    case WINDOW_ID.R_SYUKKINN_MEISAIHYOU:                   // R373(出金明細表)
                    case WINDOW_ID.R_NYUUKIN_SYUUKEIHYOU:                   // R367(入金集計表)
                    case WINDOW_ID.R_SYUKKINN_ICHIRANHYOU:                  // R374(出金集計表)
                    case WINDOW_ID.R_SEIKYUU_MEISAIHYOU:                    // R379(請求明細表)
                    case WINDOW_ID.R_SHIHARAIMEISAI_MEISAIHYOU:             // R384(支払明細明細表)
                    case WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU:                 // R369(未入金一覧表)
                    case WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU:                 // R376(未出金一覧表)
                    case WINDOW_ID.R_NYUUKIN_YOTEI_ICHIRANHYOU:             // R370(入金予定一覧表)
                    case WINDOW_ID.R_SYUKKIN_YOTEI_ICHIRANHYOU:             // R377(出金予定一覧表)
                        // 伝票種類の有効・無効
                        this.IsDenpyouSyuruiEnable = false;

                        // 伝票種類
                        this.DenpyouSyurui = DENPYOU_SYURUI.Subete;

                        // 伝票種類名
                        this.DenpyouSyuruiName = new Dictionary<DENPYOU_SYURUI, string>()
                    {
                        { DENPYOU_SYURUI.Ukeire, "受入" },
                        { DENPYOU_SYURUI.Syutsuka, "出荷" },
                        { DENPYOU_SYURUI.UriageShiharai, "売上／支払" },
                        { DENPYOU_SYURUI.Subete, "全て" },
                    };

                        // 伝票区分の有効・無効
                        this.IsDenpyouKubunEnable = false;

                        // 伝票区分
                        this.DenpyouKubun = DENPYOU_KUBUN.Subete;

                        break;
                    case WINDOW_ID.R_URIAGE_SUIIHYOU:                       // R432(売上推移表)
                    case WINDOW_ID.R_URIAGE_JYUNNIHYOU:                     // R433(売上順位表)
                    case WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU:               // R434(売上前年対比表)
                        // 伝票種類の有効・無効
                        this.IsDenpyouSyuruiEnable = true;

                        // 伝票種類
                        this.DenpyouSyurui = DENPYOU_SYURUI.Ukeire;

                        // 伝票種類名
                        this.DenpyouSyuruiName = new Dictionary<DENPYOU_SYURUI, string>()
                    {
                        { DENPYOU_SYURUI.Ukeire, "受入" },
                        { DENPYOU_SYURUI.Syutsuka, "出荷" },
                        { DENPYOU_SYURUI.UriageShiharai, "売上／支払" },
                        { DENPYOU_SYURUI.Subete, "全て" },
                    };

                        // 伝票区分の有効・無効
                        this.IsDenpyouKubunEnable = false;

                        // 伝票区分
                        this.DenpyouKubun = DENPYOU_KUBUN.Uriage;

                        break;
                    case WINDOW_ID.R_SHIHARAI_SUIIHYOU:                     // R432(支払推移表)
                    case WINDOW_ID.R_SHIHARAI_JYUNNIHYOU:                   // R433(支払順位表)
                    case WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU:             // R434(支払前年対比表)
                        // 伝票種類の有効・無効
                        this.IsDenpyouSyuruiEnable = true;

                        // 伝票種類
                        this.DenpyouSyurui = DENPYOU_SYURUI.Ukeire;

                        // 伝票種類名
                        this.DenpyouSyuruiName = new Dictionary<DENPYOU_SYURUI, string>()
                    {
                        { DENPYOU_SYURUI.Ukeire, "受入" },
                        { DENPYOU_SYURUI.Syutsuka, "出荷" },
                        { DENPYOU_SYURUI.UriageShiharai, "売上／支払" },
                        { DENPYOU_SYURUI.Subete, "全て" },
                    };

                        // 伝票区分の有効・無効
                        this.IsDenpyouKubunEnable = false;

                        // 伝票区分
                        this.DenpyouKubun = DENPYOU_KUBUN.Shiharai;

                        break;
                    case WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU:              // R432(売上/支払推移表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_JYUNNIHYOU:            // R433(売上/支払順位表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU:      // R434(売上/支払前年対比表)
                        // 伝票種類の有効・無効
                        this.IsDenpyouSyuruiEnable = true;

                        // 伝票種類
                        this.DenpyouSyurui = DENPYOU_SYURUI.Ukeire;

                        // 伝票種類名
                        this.DenpyouSyuruiName = new Dictionary<DENPYOU_SYURUI, string>()
                    {
                        { DENPYOU_SYURUI.Ukeire, "受入" },
                        { DENPYOU_SYURUI.Syutsuka, "出荷" },
                        { DENPYOU_SYURUI.UriageShiharai, "売上／支払" },
                        { DENPYOU_SYURUI.Subete, "全て" },
                    };

                        // 伝票区分の有効・無効
                        this.IsDenpyouKubunEnable = false;

                        // 伝票区分
                        this.DenpyouKubun = DENPYOU_KUBUN.Subete;

                        break;
                    case WINDOW_ID.R_KEIRYOU_SUIIHYOU:                      // R432(計量推移表)
                    case WINDOW_ID.R_KEIRYOU_JYUNNIHYOU:                    // R433(計量順位表)
                    case WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU:              // R434(計量前年対比表)
                        // 伝票種類の有効・無効
                        this.IsDenpyouSyuruiEnable = true;

                        // 伝票種類
                        this.DenpyouSyurui = DENPYOU_SYURUI.Subete;

                        // 伝票種類名
                        this.DenpyouSyuruiName = new Dictionary<DENPYOU_SYURUI, string>()
                    {
                        { DENPYOU_SYURUI.Ukeire, "受入" },
                        { DENPYOU_SYURUI.Syutsuka, "出荷" },
                        { DENPYOU_SYURUI.UriageShiharai, "売上／支払" },
                        { DENPYOU_SYURUI.Subete, "全て" },
                    };

                        // 伝票区分の有効・無効
                        this.IsDenpyouKubunEnable = true;

                        // 伝票区分
                        this.DenpyouKubun = DENPYOU_KUBUN.Subete;

                        break;
                    case WINDOW_ID.R_UKETSUKE_MEISAIHYOU:                   // R342 受付明細表
                        // 伝票種類の有効・無効
                        this.IsDenpyouSyuruiEnable = false;

                        // 伝票種類
                        this.DenpyouSyurui = DENPYOU_SYURUI.Syuusyuu;

                        // 伝票種類名
                        this.DenpyouSyuruiName = new Dictionary<DENPYOU_SYURUI, string>()
                    {
                        { DENPYOU_SYURUI.Syuusyuu, "収集" },
                        { DENPYOU_SYURUI.Syutsuka, "出荷" },
                        { DENPYOU_SYURUI.Mochikomi, "持込" },
                        { DENPYOU_SYURUI.Butsupan, "物販" },
                    };

                        // 伝票区分の有効・無効
                        this.IsDenpyouKubunEnable = false;

                        // 伝票区分
                        this.DenpyouKubun = DENPYOU_KUBUN.Subete;

                        break;
                    case WINDOW_ID.R_UNNCHIN_MEISAIHYOU:                    // R398 運賃明細表
                        // 伝票種類の有効・無効
                        this.IsDenpyouSyuruiEnable = false;

                        // 伝票種類
                        this.DenpyouSyurui = DENPYOU_SYURUI.Subete;

                        // 伝票種類名
                        this.DenpyouSyuruiName = new Dictionary<DENPYOU_SYURUI, string>()
                    {
                        { DENPYOU_SYURUI.Ukeire, "受入" },
                        { DENPYOU_SYURUI.Syutsuka, "出荷" },
                        { DENPYOU_SYURUI.UriageShiharai, "売上／支払" },
                        { DENPYOU_SYURUI.Dainou, "代納" },
                        { DENPYOU_SYURUI.UnchinNomi, "運賃のみ" },
                        { DENPYOU_SYURUI.Subete6, "全て" },
                    };

                        // 伝票区分の有効・無効
                        this.IsDenpyouKubunEnable = false;

                        // 伝票区分
                        this.DenpyouKubun = DENPYOU_KUBUN.Subete;

                        break;
                    case WINDOW_ID.R_KEIRYOU_MEISAIHYOU:                    // R351 計量明細表
                    case WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU:                   // R352 計量集計表
                        // 伝票種類の有効・無効
                        this.IsDenpyouSyuruiEnable = false;

                        // 伝票種類
                        this.DenpyouSyurui = DENPYOU_SYURUI.Subete;

                        // 伝票種類名
                        this.DenpyouSyuruiName = new Dictionary<DENPYOU_SYURUI, string>()
                    {
                        { DENPYOU_SYURUI.Ukeire, "受入" },
                        { DENPYOU_SYURUI.Syutsuka, "出荷" },
                        { DENPYOU_SYURUI.UriageShiharai, "売上／支払" },
                        { DENPYOU_SYURUI.Subete, "全て" },
                    };

                        // 伝票区分の有効・無効
                        this.IsDenpyouKubunEnable = false;

                        // 伝票区分
                        this.DenpyouKubun = DENPYOU_KUBUN.Subete;

                        break;

                    default:
                        break;
                }

                // 伝票区分名を保持するプロパティ</summary>
                this.DenpyouKubunName = new Dictionary<DENPYOU_KUBUN, string>()
            {
                { DENPYOU_KUBUN.Uriage, "売上" },
                { DENPYOU_KUBUN.Shiharai, "支払" },
                { DENPYOU_KUBUN.Subete, "全て" },
            };

                // 選択可能な集計項目グループ数
                this.SelectEnableSyuukeiKoumokuGroupCount = 3;

                // 選択された集計項目
                this.SelectSyuukeiKoumokuList = new List<int>();
                this.SelectSyuukeiKoumokuList.Add((int)SYUKEUKOMOKU_TYPE.None);
                this.SelectSyuukeiKoumokuList.Add((int)SYUKEUKOMOKU_TYPE.None);
                this.SelectSyuukeiKoumokuList.Add((int)SYUKEUKOMOKU_TYPE.None);
                this.SelectSyuukeiKoumokuList.Add((int)SYUKEUKOMOKU_TYPE.None);

                // 出力可能項目（伝票）の有効・無効
                this.OutEnableKoumokuDenpyou = false;

                // 出力可能項目（明細）の有効・無効
                this.OutEnableKoumokuMeisai = false;

                // 選択可能な帳票出力可能項目（伝票）
                this.SelectEnableChouhyouOutKoumokuDenpyouList = new List<ChouhyouOutKoumokuGroup>();

                // 選択可能な帳票出力可能項目（明細）
                this.SelectEnableChouhyouOutKoumokuMeisaiList = new List<ChouhyouOutKoumokuGroup>();

                // 選択された帳票出力項目（伝票）
                this.SelectChouhyouOutKoumokuDepyouList = new List<ChouhyouOutKoumokuGroup>();

                // 選択された帳票出力項目（明細）
                this.SelectChouhyouOutKoumokuMeisaiList = new List<ChouhyouOutKoumokuGroup>();
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        /// <summary>帳票出力用データーテーブルの取得処理を実行する</summary>
        public virtual void GetOutDataTable()
        {
            try
            {
                bool densyuKubunBetsu = false;
                int startCD = -1;
                int endCD = -1;

                // 伝種区分別確認
                SyuukeiKoumoku syuukeiKoumoku;
                for (int i = 0; i < this.SelectSyuukeiKoumokuList.Count; i++)
                {
                    int item = this.SelectSyuukeiKoumokuList[i];

                    syuukeiKoumoku = this.SyuukeiKomokuList[item];

                    if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.DensyuKubunBetsu)
                    {   // 伝種区分別
                        densyuKubunBetsu = true;

                        if (this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU || this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU)
                        {   // R355(売上／支払明細表)又はR***(売上／支払集計表)
                            if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                            {
                                startCD = 1;
                                endCD = 3;
                            }
                            else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                            {
                                startCD = int.Parse(syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart);
                                endCD = 3;
                            }
                            else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                            {
                                startCD = 1;
                                endCD = int.Parse(syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd);
                            }
                            else
                            {
                                startCD = int.Parse(syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart);
                                endCD = int.Parse(syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd);
                            }
                        }
                        else if (this.WindowID == WINDOW_ID.R_URIAGE_MEISAIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_MEISAIHYOU ||
                                 this.WindowID == WINDOW_ID.R_URIAGE_SYUUKEIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU)
                        {   // R358(売上明細表)またはR362(支払明細表)またはR***(売上集計表)またはR***(支払集計表)
                            startCD = 1;
                            endCD = 2;
                        }
                    }
                }

                string sql;
                TaishouTable taishouTableTmp;
                this.InputDataTable = new DataTable[this.TaishouTableList.Count];

                // 最大読み込み行数
                this.MaxRowCount = 0;

                // 一覧アラート件数
                IS2Dao dao = this.UkeireEntryDao;
                sql = "SELECT M_SYS_INFO.ICHIRAN_ALERT_KENSUU FROM M_SYS_INFO";
                DataTable dataTableTmp = dao.GetDateForStringSql(sql);
                int index = dataTableTmp.Columns.IndexOf("ICHIRAN_ALERT_KENSUU");
                if (!this.IsDBNull(dataTableTmp.Rows[0].ItemArray[index]))
                {
                    this.IchiranAlertCount = (int)dataTableTmp.Rows[0].ItemArray[index];
                }
                else
                {
                    this.IchiranAlertCount = 0;
                }

                for (int i = 0; i < this.TaishouTableList.Count; i++)
                {
                    if (this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU || this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU)
                    {   // R355(売上支払明細表)・(売上支払集計表)

                        if (this.DenpyouSyurui == DENPYOU_SYURUI.Ukeire)
                        {   // 受入のみ
                            if (i != ((int)DENPYOU_SYURUI.Ukeire - 1))
                            {
                                continue;
                            }
                        }
                        else if (this.DenpyouSyurui == DENPYOU_SYURUI.Syutsuka)
                        {   // 出荷のみ
                            if (i != ((int)DENPYOU_SYURUI.Syutsuka - 1))
                            {
                                continue;
                            }
                        }
                        else if (this.DenpyouSyurui == DENPYOU_SYURUI.UriageShiharai)
                        {   // 売上／支払のみ
                            if (i != ((int)DENPYOU_SYURUI.UriageShiharai - 1))
                            {
                                continue;
                            }
                        }
                        else
                        {   // 全て
                        }
                    }
                    else if (this.WindowID == WINDOW_ID.R_UKETSUKE_MEISAIHYOU)
                    {   // R342(受付明細表)

                        if (this.DenpyouSyurui == DENPYOU_SYURUI.Syuusyuu)
                        {   // 収集のみ
                            if (i != ((int)DENPYOU_SYURUI.Syuusyuu - 1))
                            {
                                continue;
                            }
                        }
                        else if (this.DenpyouSyurui == DENPYOU_SYURUI.Syutsuka)
                        {   // 出荷のみ
                            if (i != ((int)DENPYOU_SYURUI.Syutsuka - 1))
                            {
                                continue;
                            }
                        }
                        else if (this.DenpyouSyurui == DENPYOU_SYURUI.Mochikomi)
                        {   // 持込のみ
                            if (i != ((int)DENPYOU_SYURUI.Mochikomi - 1))
                            {
                                continue;
                            }
                        }
                        else if (this.DenpyouSyurui == DENPYOU_SYURUI.Butsupan)
                        {   // 物販のみ
                            if (i != ((int)DENPYOU_SYURUI.Butsupan - 1))
                            {
                                continue;
                            }
                        }
                    }
                    else if (this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU ||
                             this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU ||
                             this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU ||
                             this.WindowID == WINDOW_ID.R_KEIRYOU_SUIIHYOU ||

                             this.WindowID == WINDOW_ID.R_URIAGE_JYUNNIHYOU ||
                             this.WindowID == WINDOW_ID.R_SHIHARAI_JYUNNIHYOU ||
                             this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_JYUNNIHYOU ||
                             this.WindowID == WINDOW_ID.R_KEIRYOU_JYUNNIHYOU ||

                             this.WindowID == WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU ||
                             this.WindowID == WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU ||
                             this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU ||
                             this.WindowID == WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU)
                    {   // R432(売上推移表)
                        // R432(支払推移表)
                        // R432(売上/支払推移表)
                        // R432(計量推移表)
                        // R433(売上順位表)
                        // R433(支払順位表)
                        // R433(売上／支払順位表)
                        // R433(計量順位表)
                        // R434(売上前年対比表)
                        // R434(支払前年対比表)
                        // R434(売上／支払前年対比表)
                        // R434(計量前年対比表)

                        if (this.DenpyouSyurui == DENPYOU_SYURUI.Ukeire)
                        {   // 受入のみ
                            if (i != ((int)DENPYOU_SYURUI.Ukeire - 1))
                            {
                                continue;
                            }
                        }
                        else if (this.DenpyouSyurui == DENPYOU_SYURUI.Syutsuka)
                        {   // 出荷のみ
                            if (i != ((int)DENPYOU_SYURUI.Syutsuka - 1))
                            {
                                continue;
                            }
                        }
                        else if (this.DenpyouSyurui == DENPYOU_SYURUI.UriageShiharai)
                        {   // 売上／支払のみ
                            if (i != ((int)DENPYOU_SYURUI.UriageShiharai - 1))
                            {
                                continue;
                            }
                        }
                    }

                    if (this.DenpyouSyurui == CommonChouhyouBase.DENPYOU_SYURUI.Subete && densyuKubunBetsu == true)
                    {   // 伝票種類が全てで伝種区分が指定されている場合
                        if (startCD <= i + 1 && i + 1 <= endCD)
                        {
                            taishouTableTmp = this.TaishouTableList[i];

                            // データベースからの抽出条件用文字列を作成し取得
                            sql = this.MakeChusyutsuJoken(i, taishouTableTmp.TableName);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        taishouTableTmp = this.TaishouTableList[i];

                        // データベースからの抽出条件用文字列を作成し取得
                        sql = this.MakeChusyutsuJoken(i, taishouTableTmp.TableName);
                    }

                    if (IntableExistColumns(i, taishouTableTmp.TableName))
                    {
                        // SQL文発行しTable情報を取得する
                        this.InputDataTable[i] = dao.GetDateForStringSql(sql);

                        // 最大読み込み行数
                        this.MaxRowCount += this.InputDataTable[i].Rows.Count;

                        if ((i == 1) &&
                            (this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU ||
                            this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU ||
                            this.WindowID == WINDOW_ID.R_URIAGE_JYUNNIHYOU ||
                            this.WindowID == WINDOW_ID.R_SHIHARAI_JYUNNIHYOU))
                        {
                            // 出荷かつ、売上(支払)推移表もしくは売上(支払)順位表出力時は追加で検収済みデータを抽出する

                            // 条件を「伝票区分」⇒「検収伝票区分」へ
                            sql = sql.Replace("T_SHUKKA_DETAIL.DENPYOU_KBN_CD = 1", "T_KENSHU_DETAIL.DENPYOU_KBN_CD = 1");

                            // 条件を「検収不要の伝票」⇒「要検収かつ検収日付が存在する伝票(検収済み伝票)」へ
                            sql = sql.Replace("T_SHUKKA_ENTRY.KENSHU_MUST_KBN = 0", "(T_SHUKKA_ENTRY.KENSHU_MUST_KBN = 1 AND T_SHUKKA_ENTRY.KENSHU_DATE IS NOT NULL)");

                            // 条件を「伝票日付」⇒「検収伝票日付」へ
                            sql = sql.Replace("T_SHUKKA_ENTRY.DENPYOU_DATE BETWEEN", "T_SHUKKA_ENTRY.KENSHU_DATE BETWEEN");

                            // 抽出対象を「出荷明細」⇒「検収明細」へ
                            sql = sql.Replace("T_SHUKKA_DETAIL.*", "T_KENSHU_DETAIL.*");

                            // 抽出対象を「伝票日付」⇒「検収伝票日付」へ
                            sql = sql.Replace("T_SHUKKA_ENTRY.DENPYOU_DATE", "T_SHUKKA_ENTRY.KENSHU_DATE AS DENPYOU_DATE");

                            // 抽出対象を「品名」⇒「検収品名」へ
                            sql = sql.Replace(", M_HINMEI.HINMEI_CD", ", KENSHU_HINMEI.HINMEI_CD");

                            // 並び順を「品名」⇒「検収品名」へ
                            sql = sql.Replace("ORDER BY M_HINMEI.HINMEI_CD", "ORDER BY KENSHU_HINMEI.HINMEI_CD");

                            // SQL文を再発行し検収Table情報を取得する
                            var table = dao.GetDateForStringSql(sql);
                            this.InputDataTable[i].Merge(table);

                            // 最大読み込み行数
                            this.MaxRowCount += table.Rows.Count;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        /// <summary>
        /// 選択した出力項目、集計項目が、指定したテーブルに存在するか
        /// </summary>
        /// <param name="indexTable"></param>
        /// <param name="tableList"></param>
        /// <returns>True:存在する False:存在しない</returns>
        private bool IntableExistColumns(int indexTable, string[] tableList)
        {
            bool ExistCol = true;

            // 選択された出力項目がテーブルに存在するか
            for (int i = 0; i < this.SelectChouhyouOutKoumokuDepyouList.Count; i++)
            {
                ChouhyouOutKoumokuGroup colGroup = this.SelectChouhyouOutKoumokuDepyouList[i];
                ChouhyouOutKoumoku outCol = colGroup.ChouhyouOutKoumokuList[indexTable];
                if (outCol != null)
                {
                    switch (outCol.FieldName)
                    {
                        case "NIZUMI_GYOUSHA_NAME":
                        case "NIZUMI_GENBA_NAME":
                            if (tableList[0].Equals("T_UKEIRE_ENTRY"))
                            {
                                // 受入データには存在しない項目
                                ExistCol = false;
                            }
                            break;
                        case "NIOROSHI_GYOUSHA_NAME":
                        case "NIOROSHI_GENBA_NAME":
                            if (tableList[0].Equals("T_SHUKKA_ENTRY"))
                            {
                                // 出荷データには存在しない項目
                                ExistCol = false;
                            }
                            break;
                    }
                }
                else
                {
                    // データには存在しない項目
                    ExistCol = false;
                }
            }

            // 選択された集計項目がテーブルに存在するか
            foreach (int selectSumCol in this.SelectSyuukeiKoumokuList)
            {
                SyuukeiKoumoku sumCol = this.SyuukeiKomokuList[selectSumCol];
                switch (sumCol.Type)
                {
                    case SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu:
                    case SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu:
                        if (tableList[0].Equals("T_UKEIRE_ENTRY"))
                        {
                            // 受入データには存在しない項目
                            ExistCol = false;
                        }
                        break;

                    case SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu:
                    case SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu:
                        if (tableList[0].Equals("T_SHUKKA_ENTRY"))
                        {
                            // 出荷データには存在しない項目
                            ExistCol = false;
                        }
                        break;
                }
            }

            return ExistCol;
        }

        /// <summary>複数テーブルの並べ替え処理を実行する</summary>
        protected void MultiSort()
        {
            try
            {
                DataGrid dataGrid = new DataGrid();
                DataSet dataSet = new DataSet("MultiSort");
                this.DataTableMultiSort = dataSet.Tables.Add("MultiSortTable");

                for (int k = 0; k < this.SelectSyuukeiKoumokuList.Count; k++)
                {
                    this.DataTableMultiSort.Columns.Add(string.Format("Field{0}", k));
                }

                this.DataTableMultiSort.Columns.Add("TableIndex");
                this.DataTableMultiSort.Columns.Add("RowIndex");

                SyuukeiKoumoku syuukeiKoumoku;
                int item;
                int index;
                for (int i = 0; i < this.InputDataTable.Length; i++)
                {
                    if (this.InputDataTable[i] == null)
                    {
                        continue;
                    }

                    if (this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU)
                    {   // 売上支払明細表

                        if (this.DenpyouSyurui == DENPYOU_SYURUI.Ukeire)
                        {   // 受入のみ
                            if (i != ((int)DENPYOU_SYURUI.Ukeire - 1))
                            {
                                continue;
                            }
                        }
                        else if (this.DenpyouSyurui == DENPYOU_SYURUI.Syutsuka)
                        {   // 出荷のみ
                            if (i != ((int)DENPYOU_SYURUI.Syutsuka - 1))
                            {
                                continue;
                            }
                        }
                        else if (this.DenpyouSyurui == DENPYOU_SYURUI.UriageShiharai)
                        {   // 売上／支払のみ
                            if (i != ((int)DENPYOU_SYURUI.UriageShiharai - 1))
                            {
                                continue;
                            }
                        }
                        else
                        {   // 全て
                        }
                    }
                    else if (this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU ||
                             this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU ||
                             this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU ||
                             this.WindowID == WINDOW_ID.R_KEIRYOU_SUIIHYOU ||

                             this.WindowID == WINDOW_ID.R_URIAGE_JYUNNIHYOU ||
                             this.WindowID == WINDOW_ID.R_SHIHARAI_JYUNNIHYOU ||
                             this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_JYUNNIHYOU ||
                             this.WindowID == WINDOW_ID.R_KEIRYOU_JYUNNIHYOU ||

                             this.WindowID == WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU ||
                             this.WindowID == WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU ||
                             this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU ||
                             this.WindowID == WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU)
                    {
                        // R432(売上推移表)
                        // R432(支払推移表)
                        // R432(売上/支払推移表)
                        // R432(計量推移表)
                        // R433(売上順位表)
                        // R433(支払順位表)
                        // R433(売上／支払順位表)
                        // R433(計量順位表)
                        // R434(売上前年対比表)
                        // R434(支払前年対比表)
                        // R434(売上／支払前年対比表)
                        // R434(計量前年対比表)

                        if (this.DenpyouSyurui == DENPYOU_SYURUI.Ukeire)
                        {   // 受入のみ
                            if (i != ((int)DENPYOU_SYURUI.Ukeire - 1))
                            {
                                continue;
                            }
                        }
                        else if (this.DenpyouSyurui == DENPYOU_SYURUI.Syutsuka)
                        {   // 出荷のみ
                            if (i != ((int)DENPYOU_SYURUI.Syutsuka - 1))
                            {
                                continue;
                            }
                        }
                        else if (this.DenpyouSyurui == DENPYOU_SYURUI.UriageShiharai)
                        {   // 売上／支払のみ
                            if (i != ((int)DENPYOU_SYURUI.UriageShiharai - 1))
                            {
                                continue;
                            }
                        }
                    }

                    object code;
                    for (int j = 0; j < this.InputDataTable[i].Rows.Count; j++)
                    {
                        if (this.SelectSyuukeiKoumokuList.Count > 0)
                        {
                            DataRow dataRowNew = this.DataTableMultiSort.NewRow();

                            for (int k = 0; k < this.SelectSyuukeiKoumokuList.Count; k++)
                            {
                                item = this.SelectSyuukeiKoumokuList[k];

                                syuukeiKoumoku = this.SyuukeiKomokuList[item];

                                if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.None)
                                {
                                    continue;
                                }

                                switch (syuukeiKoumoku.Type)
                                {
                                    case SYUKEUKOMOKU_TYPE.DensyuKubunBetsu:    // 伝種区分別
                                        code = i;

                                        break;
                                    case SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu: // 荷卸業者別
                                        index = this.InputDataTable[i].Columns.IndexOf("NIOROSHI_GYOUSHA_CD");
                                        if (index == -1)
                                        {
                                            code = string.Empty;
                                        }
                                        else
                                        {
                                            code = this.InputDataTable[i].Rows[j].ItemArray[index];
                                        }

                                        break;
                                    case SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu:  // 荷卸現場別
                                        index = this.InputDataTable[i].Columns.IndexOf("NIOROSHI_GENBA_CD");
                                        if (index == -1)
                                        {
                                            code = string.Empty;
                                        }
                                        else
                                        {
                                            code = this.InputDataTable[i].Rows[j].ItemArray[index];
                                        }

                                        break;
                                    case SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu:   // 荷積業者別
                                        index = this.InputDataTable[i].Columns.IndexOf("NIZUMI_GYOUSHA_CD");
                                        if (index == -1)
                                        {
                                            code = string.Empty;
                                        }
                                        else
                                        {
                                            code = this.InputDataTable[i].Rows[j].ItemArray[index];
                                        }

                                        break;
                                    case SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu:    // 荷積現場別
                                        index = this.InputDataTable[i].Columns.IndexOf("NIZUMI_GENBA_CD");
                                        if (index == -1)
                                        {
                                            code = string.Empty;
                                        }
                                        else
                                        {
                                            code = this.InputDataTable[i].Rows[j].ItemArray[index];
                                        }

                                        break;
                                    case SYUKEUKOMOKU_TYPE.EigyoTantoshaBetsu:    // 営業担当者別
                                        index = this.InputDataTable[i].Columns.IndexOf("EIGYOU_TANTOU_CD");
                                        if (index == -1)
                                        {
                                            code = string.Empty;
                                        }
                                        else
                                        {
                                            code = this.InputDataTable[i].Rows[j].ItemArray[index];
                                        }

                                        break;
                                    default:
                                        index = this.InputDataTable[i].Columns.IndexOf(syuukeiKoumoku.FieldCD);
                                        code = this.InputDataTable[i].Rows[j].ItemArray[index];

                                        break;
                                }

                                dataRowNew[string.Format("Field{0}", k)] = code;
                            }

                            dataRowNew["TableIndex"] = i.ToString();
                            dataRowNew["RowIndex"] = j.ToString();

                            this.DataTableMultiSort.Rows.Add(dataRowNew);
                        }
                    }
                }

                // 並べ替え条件
                string sortJyouken = string.Empty;
                for (int i = 0; i < this.SelectSyuukeiKoumokuList.Count; i++)
                {
                    item = this.SelectSyuukeiKoumokuList[i];

                    syuukeiKoumoku = this.SyuukeiKomokuList[item];

                    if (syuukeiKoumoku.Type == SYUKEUKOMOKU_TYPE.None)
                    {
                        continue;
                    }

                    sortJyouken += string.Format("Field{0} ", i) + "ASC,";
                }

                if (sortJyouken.Length != 0)
                {
                    sortJyouken = sortJyouken.Substring(0, sortJyouken.Length - 1);

                    // 並べ替え
                    this.DataTableMultiSort.DefaultView.Sort = sortJyouken;
                    dataGrid.SetDataBinding(this.DataTableMultiSort.DefaultView, string.Empty);
                }
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        /// <summary>データがDbNullかどうか取得する</summary>
        /// <param name="data">確認データオブジェクト</param>
        /// <returns>DBNullの場合は真</returns>
        protected bool IsDBNull(object data)
        {
            return DBNull.Value.Equals(data);
        }

        /// <summary>
        /// objectをdecimalに変換します（nullの場合は 0 を返します）
        /// </summary>
        /// <param name="data">変換元データ</param>
        /// <returns>変換した値</returns>
        internal decimal ConvertNull2Zero(object data)
        {
            decimal ret = 0;

            if (!this.IsDBNull(data))
            {
                ret = Convert.ToDecimal(data);
            }

            return ret;
        }

        /// <summary>受け渡し用データーテーブル（明細）フィールド名の設定処理を実行する</summary>
        protected void SetDetailFieldNameForUkewatashi()
        {
            try
            {
                // データテーブル生成
                this.DataTableUkewatashi = new DataTable();

                #region - Field Name Array -

                string[] fieldNameArray =
                {
                    "GROUP_LABEL",                  // グループラベル

                    "FILL_COND_1_CD",               // 集計項目CD1
                    "FILL_COND_1_NAME",             // 集計項目名1
                    "FILL_COND_2_CD",               // 集計項目CD2
                    "FILL_COND_2_NAME",             // 集計項目名2
                    "FILL_COND_3_CD",               // 集計項目CD3
                    "FILL_COND_3_NAME",             // 集計項目名3
                    "FILL_COND_4_CD",               // 集計項目CD4
                    "FILL_COND_4_NAME",             // 集計項目名4
                
                    "DENPYOU_NUMBER",               // 伝票番号
                    "DENPYOU_DATE",                 // 伝票日付

                    "OUTPUT_DENPYOU_1_CD",          // 出力（伝票）項目CD1
                    "OUTPUT_DENPYOU_1_VALUE",       // 出力（伝票）項目値1
                    "OUTPUT_DENPYOU_2_CD",          // 出力（伝票）項目CD2
                    "OUTPUT_DENPYOU_2_VALUE",       // 出力（伝票）項目値2
                    "OUTPUT_DENPYOU_3_CD",          // 出力（伝票）項目CD3
                    "OUTPUT_DENPYOU_3_VALUE",       // 出力（伝票）項目値3
                    "OUTPUT_DENPYOU_4_CD",          // 出力（伝票）項目CD4
                    "OUTPUT_DENPYOU_4_VALUE",       // 出力（伝票）項目値4
                    "OUTPUT_DENPYOU_5_CD",          // 出力（伝票）項目CD5
                    "OUTPUT_DENPYOU_5_VALUE",       // 出力（伝票）項目値5
                    "OUTPUT_DENPYOU_6_CD",          // 出力（伝票）項目CD6
                    "OUTPUT_DENPYOU_6_VALUE",       // 出力（伝票）項目値6
                    "OUTPUT_DENPYOU_7_CD",          // 出力（伝票）項目CD7
                    "OUTPUT_DENPYOU_7_VALUE",       // 出力（伝票）項目値7
                    "OUTPUT_DENPYOU_8_CD",          // 出力（伝票）項目CD8
                    "OUTPUT_DENPYOU_8_VALUE",       // 出力（伝票）項目値8
                
                    "ROW_NO",                       // 明細番号 / 行No

                    "OUTPUT_MEISAI_1_CD",           // 出力（伝票）項目CD1
                    "OUTPUT_MEISAI_1_VALUE",        // 出力（伝票）項目値1
                    "OUTPUT_MEISAI_2_CD",           // 出力（伝票）項目CD2
                    "OUTPUT_MEISAI_2_VALUE",        // 出力（伝票）項目値2
                    "OUTPUT_MEISAI_3_CD",           // 出力（伝票）項目CD3
                    "OUTPUT_MEISAI_3_VALUE",        // 出力（伝票）項目値3
                    "OUTPUT_MEISAI_4_CD",           // 出力（伝票）項目CD4
                    "OUTPUT_MEISAI_4_VALUE",        // 出力（伝票）項目値4
                    "OUTPUT_MEISAI_5_CD",           // 出力（伝票）項目CD5
                    "OUTPUT_MEISAI_5_VALUE",        // 出力（伝票）項目値5
                    "OUTPUT_MEISAI_6_CD",           // 出力（伝票）項目CD6
                    "OUTPUT_MEISAI_6_VALUE",        // 出力（伝票）項目値6
                    "OUTPUT_MEISAI_7_CD",           // 出力（伝票）項目CD7
                    "OUTPUT_MEISAI_7_VALUE",        // 出力（伝票）項目値7
                    "OUTPUT_MEISAI_8_CD",           // 出力（伝票）項目CD8
                    "OUTPUT_MEISAI_8_VALUE",        // 出力（伝票）項目値8

                    "OUTPUT_YEAR_MONTH_1",          // 出力年月1
                    "OUTPUT_YEAR_MONTH_2",          // 出力年月2
                    "OUTPUT_YEAR_MONTH_3",          // 出力年月3
                    "OUTPUT_YEAR_MONTH_4",          // 出力年月4
                    "OUTPUT_YEAR_MONTH_5",          // 出力年月5
                    "OUTPUT_YEAR_MONTH_6",          // 出力年月6
                    "OUTPUT_YEAR_MONTH_7",          // 出力年月7
                    "OUTPUT_YEAR_MONTH_8",          // 出力年月8
                    "OUTPUT_YEAR_MONTH_9",          // 出力年月9
                    "OUTPUT_YEAR_MONTH_10",         // 出力年月10
                    "OUTPUT_YEAR_MONTH_11",         // 出力年月11
                    "OUTPUT_YEAR_MONTH_12",         // 出力年月12

                    "OUTPUT_YEAR_MONTH_TOTAL",      // 出力年月合計

                    "OUTPUT_ITEM_1",                // 出力項目1
                    "OUTPUT_ITEM_2",                // 出力項目2
                    "OUTPUT_ITEM_3",                // 出力項目3
                    "OUTPUT_ITEM_4",                // 出力項目4
                
                    "DENPYOU_KINGAKU_TOTAL",        // 伝票毎金額合計
                    "DENPYOU_TAX_TOTAL",            // 伝票毎消費税合計
                    "DENPYOU_TOTAL",                // 伝票毎総合計

                    "FILL_COND_ID_1_TOTAL_CD",      // 集計項目CD1
                    "FILL_COND_ID_1_TOTAL_NAME",    // 集計項目名1
                    "FILL_COND_ID_1_KINGAKU_TOTAL", // 集計項目1毎金額合計
                    "FILL_COND_ID_1_TAX_TOTAL",     // 集計項目1毎消費税合計
                    "FILL_COND_ID_1_TOTAL",         // 集計項目1毎総合計
                    "FILL_COND_ID_2_TOTAL_CD",      // 集計項目CD2
                    "FILL_COND_ID_2_TOTAL_NAME",    // 集計項目名2
                    "FILL_COND_ID_2_KINGAKU_TOTAL", // 集計項目2毎金額合計
                    "FILL_COND_ID_2_TAX_TOTAL",     // 集計項目2毎消費税合計
                    "FILL_COND_ID_2_TOTAL",         // 集計項目2毎総合計
                    "FILL_COND_ID_3_TOTAL_CD",      // 集計項目CD3
                    "FILL_COND_ID_3_TOTAL_NAME",    // 集計項目名3
                    "FILL_COND_ID_3_KINGAKU_TOTAL", // 集計項目3毎金額合計
                    "FILL_COND_ID_3_TAX_TOTAL",     // 集計項目3毎消費税合計
                    "FILL_COND_ID_3_TOTAL",         // 集計項目3毎総合計
                    "FILL_COND_ID_4_TOTAL_CD",      // 集計項目CD4
                    "FILL_COND_ID_4_TOTAL_NAME",    // 集計項目名4
                    "FILL_COND_ID_4_KINGAKU_TOTAL", // 集計項目4毎金額合計
                    "FILL_COND_ID_4_TAX_TOTAL",     // 集計項目4毎消費税合計
                    "FILL_COND_ID_4_TOTAL",         // 集計項目4毎総合計

                    "ALL_KINGAKU_TOTAL",            // 総金額合計
                    "ALL_TAX_TOTAL",                // 総消費税合計
                    //"ALL_TOTAL",                    // 総合計

                    "FILL_COND_1_TOTAL_1",          // 集計項目1合計_1
                    "FILL_COND_1_TOTAL_2",          // 集計項目1合計_2
                    "FILL_COND_1_TOTAL_3",          // 集計項目1合計_3
                    "FILL_COND_1_TOTAL_4",          // 集計項目1合計_4
                    "FILL_COND_1_TOTAL_5",          // 集計項目1合計_5
                    "FILL_COND_1_TOTAL_6",          // 集計項目1合計_6
                    "FILL_COND_1_TOTAL_7",          // 集計項目1合計_7
                    "FILL_COND_1_TOTAL_8",          // 集計項目1合計_8
                    "FILL_COND_2_TOTAL_1",          // 集計項目2合計_1
                    "FILL_COND_2_TOTAL_2",          // 集計項目2合計_2
                    "FILL_COND_2_TOTAL_3",          // 集計項目2合計_3
                    "FILL_COND_2_TOTAL_4",          // 集計項目2合計_4
                    "FILL_COND_2_TOTAL_5",          // 集計項目2合計_5
                    "FILL_COND_2_TOTAL_6",          // 集計項目2合計_6
                    "FILL_COND_2_TOTAL_7",          // 集計項目2合計_7
                    "FILL_COND_2_TOTAL_8",          // 集計項目2合計_8
                    "FILL_COND_3_TOTAL_1",          // 集計項目3合計_1
                    "FILL_COND_3_TOTAL_2",          // 集計項目3合計_2
                    "FILL_COND_3_TOTAL_3",          // 集計項目3合計_3
                    "FILL_COND_3_TOTAL_4",          // 集計項目3合計_4
                    "FILL_COND_3_TOTAL_5",          // 集計項目3合計_5
                    "FILL_COND_3_TOTAL_6",          // 集計項目3合計_6
                    "FILL_COND_3_TOTAL_7",          // 集計項目3合計_7
                    "FILL_COND_3_TOTAL_8",          // 集計項目3合計_8

                    "ALL_YEAR_MONTH_TOTAL_1",       // 総年月合計_1
                    "ALL_YEAR_MONTH_TOTAL_2",       // 総年月合計_2
                    "ALL_YEAR_MONTH_TOTAL_3",       // 総年月合計_3
                    "ALL_YEAR_MONTH_TOTAL_4",       // 総年月合計_4
                    "ALL_YEAR_MONTH_TOTAL_5",       // 総年月合計_5
                    "ALL_YEAR_MONTH_TOTAL_6",       // 総年月合計_6
                    "ALL_YEAR_MONTH_TOTAL_7",       // 総年月合計_7
                    "ALL_YEAR_MONTH_TOTAL_8",       // 総年月合計_8
                    "ALL_YEAR_MONTH_TOTAL_9",       // 総年月合計_9
                    "ALL_YEAR_MONTH_TOTAL_10",      // 総年月合計_10
                    "ALL_YEAR_MONTH_TOTAL_11",      // 総年月合計_11
                    "ALL_YEAR_MONTH_TOTAL_12",      // 総年月合計_12

                    "ALL_TOTAL_1",                  // 総合計_1
                    "ALL_TOTAL_2",                  // 総合計_2
                    "ALL_TOTAL_3",                  // 総合計_3
                    "ALL_TOTAL_4",                  // 総合計_4
                    "ALL_TOTAL_5",                  // 総合計_5
                    "ALL_TOTAL_6",                  // 総合計_6
                    "ALL_TOTAL_7",                  // 総合計_7
                    "ALL_TOTAL_8",                  // 総合計_8
                };

                #endregion - Field Name Array -

                // フィールド名設定
                foreach (string field in fieldNameArray)
                {
                    this.DataTableUkewatashi.Columns.Add(field);
                }
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        /// <summary>対象データか否か取得する</summary>
        /// <param name="denpyouDate">伝票日付</param>
        /// <param name="denpyouKubunCD">伝票区分コードを表す数値</param>
        /// <param name="hinmeiCD">品名コードを表す文字列</param>
        /// <param name="unitCD">単位コードを表す数値</param>
        /// <param name="isTon">ｔかkgか否かを表す値</param>
        /// <param name="isKansanShikiUse">換算式使用有無を表す値</param>
        /// <param name="kansanShiki">換算式を表す数値</param>
        /// <param name="kansanValue">換算値を表す数値</param>
        /// <returns>集計対象の場合は真、集計対象外の場合は偽</returns>
        protected bool IsTaishou(DateTime denpyouDate, int denpyouKubunCD, string hinmeiCD, int unitCD, ref bool isTon, ref bool isKansanShikiUse, ref int kansanShiki, ref decimal kansanValue)
        {
            try
            {
                IS2Dao dao = this.UkeireEntryDao;
                DataTable dataTable;
                int index;
                short unitCode;
                string tmp;
                string sql = string.Empty;

                isTon = false;
                isKansanShikiUse = false;
                kansanShiki = 0;
                kansanValue = 0;

                sql = string.Format("SELECT M_UNIT.UNIT_NAME FROM M_UNIT WHERE M_UNIT.UNIT_CD = {0}", unitCD);
                dataTable = dao.GetDateForStringSql(sql);

                // 単位名
                index = dataTable.Columns.IndexOf("UNIT_NAME");
                if (dataTable.Rows.Count == 0)
                {
                    return false;
                }

                tmp = (string)dataTable.Rows[0].ItemArray[index];

                if (tmp.Equals("ｔ"))
                {   // t
                    isTon = true;

                    return true;
                }
                else if (tmp.Equals("kg"))
                {   // Kg
                    isTon = false;

                    return true;
                }

                // システム設定（換算値情報基本単位）
                sql = string.Format("SELECT M_SYS_INFO.KANSAN_KIHON_UNIT_CD FROM M_SYS_INFO WHERE M_SYS_INFO.SYS_ID = 0");
                dataTable = dao.GetDateForStringSql(sql);

                index = dataTable.Columns.IndexOf("KANSAN_KIHON_UNIT_CD");
                unitCode = (short)dataTable.Rows[0].ItemArray[index];

                sql = string.Format("SELECT M_UNIT.UNIT_NAME FROM M_UNIT WHERE M_UNIT.UNIT_CD = {0}", unitCode);
                dataTable = dao.GetDateForStringSql(sql);

                // 単位名
                index = dataTable.Columns.IndexOf("UNIT_NAME");
                tmp = (string)dataTable.Rows[0].ItemArray[index];
                if (tmp.Equals("ｔ"))
                {   // t
                    isTon = true;
                }
                else if (tmp.Equals("kg"))
                {   // Kg
                    isTon = false;
                }
                else
                {   // 集計対象外
                    return false;
                }

                // 換算値マスター
                sql = string.Format("SELECT M_KANSAN.KANSANSHIKI, M_KANSAN.KANSANCHI FROM M_KANSAN WHERE M_KANSAN.DENPYOU_KBN_CD = {0} AND M_KANSAN.HINMEI_CD = '{1}' AND M_KANSAN.UNIT_CD = {2}", denpyouKubunCD, hinmeiCD, unitCD);
                dataTable = dao.GetDateForStringSql(sql);
                if (dataTable.Rows.Count == 0)
                {   // 集計対象外
                    return false;
                }

                // 換算式
                index = dataTable.Columns.IndexOf("KANSANSHIKI");
                kansanShiki = (int)dataTable.Rows[0].ItemArray[index];

                // 換算値
                index = dataTable.Columns.IndexOf("KANSANCHI");
                kansanValue = (decimal)dataTable.Rows[0].ItemArray[index];

                isKansanShikiUse = true;

                return true;
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);

                return false;
            }
        }

        /// <summary>データベースからの抽出条件用文字列を作成し取得する</summary>
        /// <param name="indexTable">テーブルインデックス</param>
        /// <param name="tableList">テーブルリスト</param>
        /// <returns>抽出条件用文字列</returns>
        private string MakeChusyutsuJoken(int indexTable, string[] tableList)
        {
            try
            {
                string tmp;
                string orderByTmp;

                string select = "SELECT ";
                string table = string.Empty;
                string where = string.Empty;
                string group = string.Empty;
                string sql = string.Empty;

                // 伝票種類
                switch (this.WindowID)
                {
                    case WINDOW_ID.R_UNNCHIN_MEISAIHYOU:
                        switch (this.DenpyouSyurui)
                        {
                            case DENPYOU_SYURUI.Ukeire:
                                where += tableList[0] + ".RENKEI_DENSHU_KBN_CD = 1" + " AND ";

                                break;
                            case DENPYOU_SYURUI.Syutsuka:
                                where += tableList[0] + ".RENKEI_DENSHU_KBN_CD = 2" + " AND ";

                                break;
                            case DENPYOU_SYURUI.UriageShiharai:
                                where += tableList[0] + ".RENKEI_DENSHU_KBN_CD = 3" + " AND ";

                                break;
                            case DENPYOU_SYURUI.Dainou:
                                where += tableList[0] + ".RENKEI_DENSHU_KBN_CD = 170" + " AND ";

                                break;
                            case DENPYOU_SYURUI.UnchinNomi:
                                where += tableList[0] + ".RENKEI_DENSHU_KBN_CD = 160" + " AND ";

                                break;
                            default:
                                break;
                        }
                        break;
                }

                // 伝票区分
                switch (this.WindowID)
                {
                    case WINDOW_ID.R_URIAGE_MEISAIHYOU:
                    case WINDOW_ID.R_URIAGE_SYUUKEIHYOU:
                        if (indexTable == 0 || indexTable == 1 || indexTable == 2)
                        {
                            where += tableList[1] + ".DENPYOU_KBN_CD = 1" + " AND ";
                            where += tableList[0] + ".KAKUTEI_KBN = 1 " + " AND ";
                        }
                        if (indexTable == 0 || indexTable == 1)
                        {
                            where += tableList[0] + ".TAIRYUU_KBN = 0 " + " AND ";
                        }

                        break;
                    case WINDOW_ID.R_SHIHARAI_MEISAIHYOU:
                    case WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU:
                        if (indexTable == 0 || indexTable == 1 || indexTable == 2)
                        {
                            where += tableList[1] + ".DENPYOU_KBN_CD = 2" + " AND ";
                            where += tableList[0] + ".KAKUTEI_KBN = 1 " + " AND ";
                        }
                        if (indexTable == 0 || indexTable == 1)
                        {
                            where += tableList[0] + ".TAIRYUU_KBN = 0 " + " AND ";
                        }

                        break;
                    case WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU:
                    case WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU:
                        if (this.DenpyouKubun == DENPYOU_KUBUN.Uriage)
                        {
                            if (indexTable == 0 || indexTable == 1 || indexTable == 2)
                            {
                                where += tableList[1] + ".DENPYOU_KBN_CD = 1" + " AND ";
                                where += tableList[0] + ".KAKUTEI_KBN = 1 " + " AND ";
                            }
                            if (indexTable == 0 || indexTable == 1)
                            {
                                where += tableList[0] + ".TAIRYUU_KBN = 0 " + " AND ";
                            }
                        }
                        else if (this.DenpyouKubun == DENPYOU_KUBUN.Shiharai)
                        {
                            if (indexTable == 0 || indexTable == 1 || indexTable == 2)
                            {
                                where += tableList[1] + ".DENPYOU_KBN_CD = 2" + " AND ";
                                where += tableList[0] + ".KAKUTEI_KBN = 1 " + " AND ";
                            }
                            if (indexTable == 0 || indexTable == 1)
                            {
                                where += tableList[0] + ".TAIRYUU_KBN = 0 " + " AND ";
                            }
                        }
                        else if (this.DenpyouKubun == DENPYOU_KUBUN.Subete)
                        {
                            if (indexTable == 0 || indexTable == 1 || indexTable == 2)
                            {
                                where += tableList[0] + ".KAKUTEI_KBN = 1 " + " AND ";
                            }
                            if (indexTable == 0 || indexTable == 1)
                            {
                                where += tableList[0] + ".TAIRYUU_KBN = 0 " + " AND ";
                            }
                        }

                        break;
                    case WINDOW_ID.R_URIAGE_SUIIHYOU:           // R432(売上推移表)
                    case WINDOW_ID.R_URIAGE_JYUNNIHYOU:         // R433(売上順位表)
                    case WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU:   // R434(売上前年対比表)

                        // 売上
                        where += tableList[1] + ".DENPYOU_KBN_CD = 1" + " AND ";

                        break;
                    case WINDOW_ID.R_SHIHARAI_SUIIHYOU:         // R432(支払推移表)
                    case WINDOW_ID.R_SHIHARAI_JYUNNIHYOU:       // R433(支払順位表)
                    case WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU: // R434(支払前年対比表)

                        // 支払
                        where += tableList[1] + ".DENPYOU_KBN_CD = 2" + " AND ";

                        break;
                    case WINDOW_ID.R_UNNCHIN_MEISAIHYOU:                // R398(運賃明細表)

                    case WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU:          // R432(売上／支払推移表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_JYUNNIHYOU:        // R433(売上／支払順位表)
                    case WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU:  // R434(売上／支払前年対比表)

                    case WINDOW_ID.R_KEIRYOU_SUIIHYOU:                  // R432(計量推移表)
                    case WINDOW_ID.R_KEIRYOU_JYUNNIHYOU:                // R433(計量順位表)
                    case WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU:          // R434(計量前年対比表)
                        if (this.DenpyouKubun == DENPYOU_KUBUN.Uriage)
                        {   // 売上
                            where += tableList[1] + ".DENPYOU_KBN_CD = 1" + " AND ";
                        }
                        else if (this.DenpyouKubun == DENPYOU_KUBUN.Shiharai)
                        {   // 支払
                            where += tableList[1] + ".DENPYOU_KBN_CD = 2" + " AND ";
                        }

                        break;
                    case WINDOW_ID.R_SEIKYUU_MEISAIHYOU:
                        where += "";
                        break;
                }

                switch (this.WindowID)
                {
                    case WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU:          // R434(計量前年対比表)
                        where += tableList[0] + ".TAIRYUU_KBN = 0" + " AND ";

                        break;
                    default:

                        break;
                }

                // 固定条件の追加等、条件拡張
                //switch (this.WindowID)
                //{
                //    case :
                //    default:
                //        break;
                //}

                List<string> fieldList = this.FieldInfo[tableList[0]];
                string tableNameRef = string.Empty;
                string fieldName = string.Empty;
                string fieldNameRyaku = string.Empty;

                if (this.WindowID == WINDOW_ID.R_URIAGE_MEISAIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_MEISAIHYOU || this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU ||
                    this.WindowID == WINDOW_ID.R_URIAGE_SYUUKEIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU || this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU)
                {
                    switch (indexTable)
                    {
                        case 0: // 受入入力
                            select += "T_UKEIRE_ENTRY.UKEIRE_NUMBER, T_UKEIRE_ENTRY.DENPYOU_DATE, T_UKEIRE_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD, T_UKEIRE_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD, T_UKEIRE_DETAIL.ROW_NO, ";

                            select += "T_UKEIRE_ENTRY.URIAGE_KINGAKU_TOTAL + T_UKEIRE_ENTRY.HINMEI_URIAGE_KINGAKU_TOTAL AS URIAGE_KINGAKU_TOTAL, ";

                            select += "T_UKEIRE_ENTRY.SHIHARAI_KINGAKU_TOTAL + T_UKEIRE_ENTRY.HINMEI_SHIHARAI_KINGAKU_TOTAL AS SHIHARAI_KINGAKU_TOTAL, ";

                            select += "T_UKEIRE_DETAIL.DENPYOU_KBN_CD, ";

                            select += "CASE WHEN T_UKEIRE_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD = 1 THEN T_UKEIRE_ENTRY.URIAGE_TAX_SOTO + T_UKEIRE_ENTRY.HINMEI_URIAGE_TAX_SOTO_TOTAL ";
                            select += "WHEN T_UKEIRE_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD = 2 THEN 0 ";
                            select += "WHEN T_UKEIRE_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD = 3 THEN T_UKEIRE_ENTRY.URIAGE_TAX_SOTO + T_UKEIRE_ENTRY.HINMEI_URIAGE_TAX_SOTO_TOTAL ";
                            select += "ELSE 0 ";
                            select += "END URIAGE_TAX_SOTO, ";

                            select += "CASE WHEN T_UKEIRE_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD = 1 THEN T_UKEIRE_ENTRY.URIAGE_TAX_UCHI + T_UKEIRE_ENTRY.HINMEI_URIAGE_TAX_UCHI_TOTAL ";
                            select += "WHEN T_UKEIRE_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD = 2 THEN 0 ";
                            select += "WHEN T_UKEIRE_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD = 3 THEN T_UKEIRE_ENTRY.URIAGE_TAX_UCHI + T_UKEIRE_ENTRY.HINMEI_URIAGE_TAX_UCHI_TOTAL ";
                            select += "ELSE 0 ";
                            select += "END URIAGE_TAX_UCHI, ";

                            select += "CASE WHEN T_UKEIRE_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD = 1 THEN T_UKEIRE_ENTRY.SHIHARAI_TAX_SOTO + T_UKEIRE_ENTRY.HINMEI_SHIHARAI_TAX_SOTO_TOTAL ";
                            select += "WHEN T_UKEIRE_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD = 2 THEN 0 ";
                            select += "WHEN T_UKEIRE_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD = 3 THEN T_UKEIRE_ENTRY.SHIHARAI_TAX_SOTO + T_UKEIRE_ENTRY.HINMEI_SHIHARAI_TAX_SOTO_TOTAL ";
                            select += "ELSE 0 ";
                            select += "END SHIHARAI_TAX_SOTO, ";

                            select += "CASE WHEN T_UKEIRE_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD = 1 THEN T_UKEIRE_ENTRY.SHIHARAI_TAX_UCHI + T_UKEIRE_ENTRY.HINMEI_SHIHARAI_TAX_UCHI_TOTAL ";
                            select += "WHEN T_UKEIRE_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD = 2 THEN 0 ";
                            select += "WHEN T_UKEIRE_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD = 3 THEN T_UKEIRE_ENTRY.SHIHARAI_TAX_UCHI + T_UKEIRE_ENTRY.HINMEI_SHIHARAI_TAX_UCHI_TOTAL ";
                            select += "ELSE 0 ";
                            select += "END SHIHARAI_TAX_UCHI, ";

                            select += "T_UKEIRE_DETAIL.KINGAKU + T_UKEIRE_DETAIL.HINMEI_KINGAKU AS KINGAKU, ";

                            select += "T_UKEIRE_DETAIL.TAX_SOTO + T_UKEIRE_DETAIL.HINMEI_TAX_SOTO AS TAX_SOTO, ";

                            select += "T_UKEIRE_DETAIL.TAX_UCHI + T_UKEIRE_DETAIL.HINMEI_TAX_UCHI AS TAX_UCHI, ";

                            select += "T_UKEIRE_DETAIL.NET_JYUURYOU,";   // No.3781
                            break;
                        case 1: // 出荷入力
                            select += "T_SHUKKA_ENTRY.SHUKKA_NUMBER, T_SHUKKA_ENTRY.DENPYOU_DATE, T_SHUKKA_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD, T_SHUKKA_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD, T_SHUKKA_DETAIL.ROW_NO, ";

                            select += "T_SHUKKA_ENTRY.URIAGE_AMOUNT_TOTAL + T_SHUKKA_ENTRY.HINMEI_URIAGE_KINGAKU_TOTAL AS URIAGE_AMOUNT_TOTAL, ";

                            select += "T_SHUKKA_ENTRY.SHIHARAI_AMOUNT_TOTAL + T_SHUKKA_ENTRY.HINMEI_SHIHARAI_KINGAKU_TOTAL AS SHIHARAI_AMOUNT_TOTAL, ";

                            select += "T_SHUKKA_DETAIL.DENPYOU_KBN_CD, ";

                            select += "CASE WHEN T_SHUKKA_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD = 1 THEN T_SHUKKA_ENTRY.URIAGE_TAX_SOTO + T_SHUKKA_ENTRY.HINMEI_URIAGE_TAX_SOTO_TOTAL ";
                            select += "WHEN T_SHUKKA_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD = 2 THEN 0 ";
                            select += "WHEN T_SHUKKA_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD = 3 THEN T_SHUKKA_ENTRY.URIAGE_TAX_SOTO + T_SHUKKA_ENTRY.HINMEI_URIAGE_TAX_SOTO_TOTAL ";
                            select += "ELSE 0 ";
                            select += "END URIAGE_TAX_SOTO, ";

                            select += "CASE WHEN T_SHUKKA_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD = 1 THEN T_SHUKKA_ENTRY.URIAGE_TAX_UCHI + T_SHUKKA_ENTRY.HINMEI_URIAGE_TAX_UCHI_TOTAL ";
                            select += "WHEN T_SHUKKA_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD = 2 THEN 0 ";
                            select += "WHEN T_SHUKKA_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD = 3 THEN T_SHUKKA_ENTRY.URIAGE_TAX_UCHI + T_SHUKKA_ENTRY.HINMEI_URIAGE_TAX_UCHI_TOTAL ";
                            select += "ELSE 0 ";
                            select += "END URIAGE_TAX_UCHI, ";

                            select += "CASE WHEN T_SHUKKA_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD = 1 THEN T_SHUKKA_ENTRY.SHIHARAI_TAX_SOTO + T_SHUKKA_ENTRY.HINMEI_SHIHARAI_TAX_SOTO_TOTAL ";
                            select += "WHEN T_SHUKKA_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD = 2 THEN 0 ";
                            select += "WHEN T_SHUKKA_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD = 3 THEN T_SHUKKA_ENTRY.SHIHARAI_TAX_SOTO + T_SHUKKA_ENTRY.HINMEI_SHIHARAI_TAX_SOTO_TOTAL ";
                            select += "ELSE 0 ";
                            select += "END SHIHARAI_TAX_SOTO, ";

                            select += "CASE WHEN T_SHUKKA_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD = 1 THEN T_SHUKKA_ENTRY.SHIHARAI_TAX_UCHI + T_SHUKKA_ENTRY.HINMEI_SHIHARAI_TAX_UCHI_TOTAL ";
                            select += "WHEN T_SHUKKA_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD = 2 THEN 0 ";
                            select += "WHEN T_SHUKKA_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD = 3 THEN T_SHUKKA_ENTRY.SHIHARAI_TAX_UCHI + T_SHUKKA_ENTRY.HINMEI_SHIHARAI_TAX_UCHI_TOTAL ";
                            select += "ELSE 0 ";
                            select += "END SHIHARAI_TAX_UCHI, ";

                            select += "T_SHUKKA_DETAIL.KINGAKU + T_SHUKKA_DETAIL.HINMEI_KINGAKU AS KINGAKU, ";

                            select += "T_SHUKKA_DETAIL.TAX_SOTO + T_SHUKKA_DETAIL.HINMEI_TAX_SOTO AS TAX_SOTO, ";

                            select += "T_SHUKKA_DETAIL.TAX_UCHI + T_SHUKKA_DETAIL.HINMEI_TAX_UCHI AS TAX_UCHI, ";

                            select += "T_SHUKKA_DETAIL.NET_JYUURYOU,";   // No.3781
                            break;
                        case 2: // 売上支払入力
                            select += "T_UR_SH_ENTRY.UR_SH_NUMBER, T_UR_SH_ENTRY.DENPYOU_DATE, T_UR_SH_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD, T_UR_SH_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD, T_UR_SH_DETAIL.ROW_NO, ";

                            select += "T_UR_SH_ENTRY.URIAGE_AMOUNT_TOTAL + T_UR_SH_ENTRY.HINMEI_URIAGE_KINGAKU_TOTAL AS URIAGE_AMOUNT_TOTAL, ";

                            select += "T_UR_SH_ENTRY.SHIHARAI_AMOUNT_TOTAL + T_UR_SH_ENTRY.HINMEI_SHIHARAI_KINGAKU_TOTAL AS SHIHARAI_AMOUNT_TOTAL, ";

                            select += "T_UR_SH_DETAIL.DENPYOU_KBN_CD, ";

                            select += "CASE WHEN T_UR_SH_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD = 1 THEN T_UR_SH_ENTRY.URIAGE_TAX_SOTO + T_UR_SH_ENTRY.HINMEI_URIAGE_TAX_SOTO_TOTAL ";
                            select += "WHEN T_UR_SH_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD = 2 THEN 0 ";
                            select += "WHEN T_UR_SH_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD = 3 THEN T_UR_SH_ENTRY.URIAGE_TAX_SOTO + T_UR_SH_ENTRY.HINMEI_URIAGE_TAX_SOTO_TOTAL ";
                            select += "ELSE 0 ";
                            select += "END URIAGE_TAX_SOTO, ";

                            select += "CASE WHEN T_UR_SH_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD = 1 THEN T_UR_SH_ENTRY.URIAGE_TAX_UCHI + T_UR_SH_ENTRY.HINMEI_URIAGE_TAX_UCHI_TOTAL ";
                            select += "WHEN T_UR_SH_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD = 2 THEN 0 ";
                            select += "WHEN T_UR_SH_ENTRY.URIAGE_ZEI_KEISAN_KBN_CD = 3 THEN T_UR_SH_ENTRY.URIAGE_TAX_UCHI + T_UR_SH_ENTRY.HINMEI_URIAGE_TAX_UCHI_TOTAL ";
                            select += "ELSE 0 ";
                            select += "END URIAGE_TAX_UCHI, ";

                            select += "CASE WHEN T_UR_SH_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD = 1 THEN T_UR_SH_ENTRY.SHIHARAI_TAX_SOTO + T_UR_SH_ENTRY.HINMEI_SHIHARAI_TAX_SOTO_TOTAL ";
                            select += "WHEN T_UR_SH_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD = 2 THEN 0 ";
                            select += "WHEN T_UR_SH_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD = 3 THEN T_UR_SH_ENTRY.SHIHARAI_TAX_SOTO + T_UR_SH_ENTRY.HINMEI_SHIHARAI_TAX_SOTO_TOTAL ";
                            select += "ELSE 0 ";
                            select += "END SHIHARAI_TAX_SOTO, ";

                            select += "CASE WHEN T_UR_SH_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD = 1 THEN T_UR_SH_ENTRY.SHIHARAI_TAX_UCHI + T_UR_SH_ENTRY.HINMEI_SHIHARAI_TAX_UCHI_TOTAL ";
                            select += "WHEN T_UR_SH_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD = 2 THEN 0 ";
                            select += "WHEN T_UR_SH_ENTRY.SHIHARAI_ZEI_KEISAN_KBN_CD = 3 THEN T_UR_SH_ENTRY.SHIHARAI_TAX_UCHI + T_UR_SH_ENTRY.HINMEI_SHIHARAI_TAX_UCHI_TOTAL ";
                            select += "ELSE 0 ";
                            select += "END SHIHARAI_TAX_UCHI, ";

                            select += "T_UR_SH_DETAIL.KINGAKU + T_UR_SH_DETAIL.HINMEI_KINGAKU AS KINGAKU, ";

                            select += "T_UR_SH_DETAIL.TAX_SOTO + T_UR_SH_DETAIL.HINMEI_TAX_SOTO AS TAX_SOTO, ";

                            select += "T_UR_SH_DETAIL.TAX_UCHI + T_UR_SH_DETAIL.HINMEI_TAX_UCHI AS TAX_UCHI, ";

                            break;
                    }

                    if (this.WindowID == WINDOW_ID.R_URIAGE_SYUUKEIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU || this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU)
                    {
                        switch (indexTable)
                        {
                            case 0:
                                select += "T_UKEIRE_DETAIL.HINMEI_TAX_UCHI, ";

                                break;
                            case 1:
                                select += "T_SHUKKA_DETAIL.HINMEI_TAX_UCHI, ";

                                break;
                            case 2:
                                select += "T_UR_SH_DETAIL.HINMEI_TAX_UCHI, ";

                                break;
                        }
                    }
                }
                else if (this.WindowID == WINDOW_ID.R_NYUUKIN_MEISAIHYOU)
                {   // 入金明細表
                    select += "T_NYUUKIN_DETAIL.KINGAKU, ";
                    select += "T_NYUUKIN_DETAIL.ROW_NUMBER, ";
                }
                else if (this.WindowID == WINDOW_ID.R_NYUUKIN_SYUUKEIHYOU)
                {   // 入金集計表
                    select += "T_NYUUKIN_DETAIL.KINGAKU, ";
                }
                else if (this.WindowID == WINDOW_ID.R_SYUKKINN_MEISAIHYOU)
                {   // 出金明細表
                    select += "T_SHUKKIN_DETAIL.KINGAKU, ";
                    select += "T_SHUKKIN_DETAIL.ROW_NUMBER, ";
                }
                else if (this.WindowID == WINDOW_ID.R_SYUKKINN_ICHIRANHYOU)
                {   // 出金集計表
                    select += "T_SHUKKIN_DETAIL.KINGAKU, ";
                }
                else if (this.WindowID == WINDOW_ID.R_SEIKYUU_MEISAIHYOU)
                {
                    // 請求明細表
                    select += "ISNULL(T_SEIKYUU_DETAIL.ROW_COUNT, 0) AS ROW_COUNT, ";
                }
                else if (this.WindowID == WINDOW_ID.R_SHIHARAIMEISAI_MEISAIHYOU)
                {
                    // 支払明細表
                    select += "ISNULL(T_SEISAN_DETAIL.ROW_COUNT, 0) AS ROW_COUNT, ";
                }

                // 帳票出力項目（伝票）用
                if (this.WindowID != WINDOW_ID.R_URIAGE_SYUUKEIHYOU && this.WindowID != WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU && this.WindowID != WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU)
                {
                    if (tableList[0] != string.Empty)
                    {
                        if (this.SelectChouhyouOutKoumokuDepyouList.Count == 0)
                        {
                            foreach (string fieldNameTmp in fieldList)
                            {
                                select += tableList[0] + "." + fieldNameTmp + ", ";
                            }

                            if (tableList[1] != string.Empty)
                            {
                                select += tableList[1] + ".*, ";
                            }
                        }
                        else
                        {
                            for (int i = 0; i < this.SelectChouhyouOutKoumokuDepyouList.Count; i++)
                            {
                                ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = this.SelectChouhyouOutKoumokuDepyouList[i];
                                ChouhyouOutKoumoku chouhyouOutKoumoku = chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList[indexTable];
                                if (chouhyouOutKoumoku == null)
                                {
                                    continue;
                                }

                                chouhyouOutKoumoku.GetTableAndFieldNameRef(tableList[0], ref tableNameRef, ref fieldName, ref fieldNameRyaku);

                                switch (fieldName)
                                {
                                    case "TORIHIKISAKI_NAME":
                                        select += tableList[0] + ".TORIHIKISAKI_CD" + ", ";
                                        select += tableList[0] + ".TORIHIKISAKI_NAME" + ", ";

                                        break;
                                    case "GYOUSHA_NAME":
                                        select += tableList[0] + ".GYOUSHA_CD" + ", ";
                                        select += tableList[0] + ".GYOUSHA_NAME" + ", ";

                                        break;
                                    case "GENBA_NAME":
                                        select += tableList[0] + ".GENBA_CD" + ", ";
                                        select += tableList[0] + ".GENBA_NAME" + ", ";

                                        break;
                                    case "NIZUMI_GYOUSHA_NAME":
                                        if (tableList[0].Equals("T_SHUKKA_ENTRY") || tableList[0].Equals("T_UR_SH_ENTRY"))
                                        {
                                            select += tableList[0] + ".NIZUMI_GYOUSHA_CD" + ", ";
                                            select += tableList[0] + ".NIZUMI_GYOUSHA_NAME" + ", ";
                                        }

                                        break;
                                    case "NIZUMI_GENBA_NAME":
                                        if (tableList[0].Equals("T_SHUKKA_ENTRY") || tableList[0].Equals("T_UR_SH_ENTRY"))
                                        {
                                            select += tableList[0] + ".NIZUMI_GENBA_CD" + ", ";
                                            select += tableList[0] + ".NIZUMI_GENBA_NAME" + ", ";
                                        }

                                        break;
                                    case "NIOROSHI_GYOUSHA_NAME":
                                        if (tableList[0].Equals("T_UKEIRE_ENTRY") || tableList[0].Equals("T_UR_SH_ENTRY"))
                                        {
                                            select += tableList[0] + ".NIOROSHI_GYOUSHA_CD" + ", ";
                                            select += tableList[0] + ".NIOROSHI_GYOUSHA_NAME" + ", ";
                                        }

                                        break;
                                    case "NIOROSHI_GENBA_NAME":
                                        if (tableList[0].Equals("T_UKEIRE_ENTRY") || tableList[0].Equals("T_UR_SH_ENTRY"))
                                        {
                                            select += tableList[0] + ".NIOROSHI_GENBA_CD" + ", ";
                                            select += tableList[0] + ".NIOROSHI_GENBA_NAME" + ", ";
                                        }

                                        break;
                                    case "EIGYOU_TANTOUSHA_NAME":
                                        select += tableList[0] + ".EIGYOU_TANTOUSHA_CD" + ", ";
                                        select += tableList[0] + ".EIGYOU_TANTOUSHA_NAME" + ", ";

                                        break;
                                    case "NYUURYOKU_TANTOUSHA_NAME":
                                        select += tableList[0] + ".NYUURYOKU_TANTOUSHA_CD" + ", ";
                                        select += tableList[0] + ".NYUURYOKU_TANTOUSHA_NAME" + ", ";

                                        break;
                                    case "SHARYOU_NAME":
                                        select += tableList[0] + ".SHARYOU_CD" + ", ";
                                        select += tableList[0] + ".SHARYOU_NAME" + ", ";

                                        break;
                                    case "SHASHU_NAME":
                                        select += tableList[0] + ".SHASHU_CD" + ", ";
                                        select += tableList[0] + ".SHASHU_NAME" + ", ";

                                        break;
                                    case "UNPAN_GYOUSHA_NAME":
                                        select += tableList[0] + ".UNPAN_GYOUSHA_CD" + ", ";
                                        select += tableList[0] + ".UNPAN_GYOUSHA_NAME" + ", ";

                                        break;
                                    case "UNTENSHA_NAME":
                                        select += tableList[0] + ".UNTENSHA_CD" + ", ";
                                        select += tableList[0] + ".UNTENSHA_NAME" + ", ";

                                        break;
                                    case "KENSHU_DATE":
                                    case "SHUKKA_NET_TOTAL":
                                        if (indexTable == 1)
                                        {
                                            select += tableList[0] + "." + fieldName + ", ";
                                        }

                                        break;
                                    default:
                                        select += tableList[0] + "." + fieldName + ", ";

                                        break;
                                }
                            }
                        }
                    }
                }

                // 帳票出力項目（明細）用
                if (tableList[1] != string.Empty)
                {
                    for (int i = 0; i < this.SelectChouhyouOutKoumokuMeisaiList.Count; i++)
                    {
                        ChouhyouOutKoumokuGroup chouhyouOutKoumokuGroup = this.SelectChouhyouOutKoumokuMeisaiList[i];
                        ChouhyouOutKoumoku chouhyouOutKoumoku = chouhyouOutKoumokuGroup.ChouhyouOutKoumokuList[indexTable];

                        if (chouhyouOutKoumoku == null)
                        {
                            continue;
                        }

                        chouhyouOutKoumoku.GetTableAndFieldNameRef(tableList[1], ref tableNameRef, ref fieldName, ref fieldNameRyaku);

                        switch (fieldName)
                        {
                            case "HINMEI_NAME":
                                if (this.WindowID == WINDOW_ID.R_UKETSUKE_MEISAIHYOU ||
                                    this.WindowID == WINDOW_ID.R_KEIRYOU_MEISAIHYOU ||
                                    this.WindowID == WINDOW_ID.R_UNNCHIN_MEISAIHYOU)
                                {   // R342(受付明細票)・R351(計量明細票)・R398(運賃明細票)
                                    select += tableList[1] + ".HINMEI_CD" + ", ";
                                    select += tableList[1] + ".HINMEI_NAME" + ", ";
                                }
                                else
                                {
                                    select += "M_HINMEI.HINMEI_CD" + ", ";
                                    select += "M_HINMEI.HINMEI_NAME" + ", ";
                                }

                                break;
                            case "SHURUI_NAME":
                                select += "M_HINMEI.SHURUI_CD, ";

                                break;
                            case "BUNRUI_NAME":
                                select += "M_HINMEI.BUNRUI_CD, ";

                                break;
                            case "KINGAKU":
                                if (this.WindowID != WINDOW_ID.R_URIAGE_MEISAIHYOU && this.WindowID != WINDOW_ID.R_SHIHARAI_MEISAIHYOU && this.WindowID != WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU)
                                {
                                    select += tableList[1] + "." + fieldName + ", ";
                                }

                                break;
                            default:
                                select += tableList[1] + "." + fieldName + ", ";

                                break;
                        }
                    }
                }

                if (
                    this.WindowID == WINDOW_ID.R_URIAGE_MEISAIHYOU || this.WindowID == WINDOW_ID.R_URIAGE_SYUUKEIHYOU || this.WindowID == WINDOW_ID.R_SHIHARAI_MEISAIHYOU
                    || this.WindowID == WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU || this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU || this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU)
                {
                    select += "M_HINMEI.HINMEI_NAME_RYAKU, ";
                }
                else
                {
                    if (this.WindowID == WINDOW_ID.R_NYUUKIN_MEISAIHYOU || this.WindowID == WINDOW_ID.R_NYUUKIN_SYUUKEIHYOU)
                    {   // R366(入金明細表)・R367(入金集計表)
                        if (this.SelectChouhyouOutKoumokuDepyouList.Count != 0)
                        {
                            select += tableList[0] + ".NYUUKIN_AMOUNT_TOTAL, ";

                            // 調整
                            select += tableList[0] + ".CHOUSEI_AMOUNT_TOTAL, ";

                            select += tableList[0] + ".NYUUKIN_NUMBER, ";
                            select += tableList[0] + ".TORIHIKISAKI_CD, ";
                            select += tableList[0] + ".DENPYOU_DATE, ";
                            select += tableList[0] + ".EIGYOU_TANTOUSHA_CD, ";
                        }
                    }
                    else if (this.WindowID == WINDOW_ID.R_SYUKKINN_MEISAIHYOU || this.WindowID == WINDOW_ID.R_SYUKKINN_ICHIRANHYOU)
                    {   // R373(出金明細表)・R374(出金集計表)
                        if (this.SelectChouhyouOutKoumokuDepyouList.Count != 0)
                        {
                            select += tableList[0] + ".SHUKKIN_AMOUNT_TOTAL, ";

                            // 調整
                            select += tableList[0] + ".CHOUSEI_AMOUNT_TOTAL, ";

                            select += tableList[0] + ".SHUKKIN_NUMBER, ";
                            select += tableList[0] + ".TORIHIKISAKI_CD, ";
                            select += tableList[0] + ".DENPYOU_DATE, ";
                            select += tableList[0] + ".EIGYOU_TANTOUSHA_CD, ";
                        }
                    }
                    else if (this.WindowID == WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU)
                    {
                        select += tableList[0] + ".SEIKYUU_NUMBER, ";
                    }
                    else if (this.WindowID == WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU)
                    {
                        select += tableList[0] + ".SEISAN_NUMBER, ";
                    }
                    else if (this.WindowID == WINDOW_ID.R_UKETSUKE_MEISAIHYOU)
                    {   // R342(受付明細票)
                        select += tableList[0] + ".UKETSUKE_NUMBER, ";
                        select += tableList[1] + ".ROW_NO, ";
                        select += tableList[0] + ".UKETSUKE_DATE, ";

                        select += tableList[0] + ".KINGAKU_TOTAL, ";
                        select += tableList[0] + ".SHOUHIZEI_TOTAL, ";
                        select += tableList[0] + ".GOUKEI_KINGAKU_TOTAL, ";
                    }
                    else if (this.WindowID == WINDOW_ID.R_KEIRYOU_MEISAIHYOU)
                    {   // R351(計量明細票)
                        select += tableList[0] + ".KEIRYOU_NUMBER, ";
                        select += tableList[1] + ".ROW_NO, ";
                        select += tableList[0] + ".DENPYOU_DATE, ";
                        select += tableList[1] + ".CHOUSEI_JYUURYOU, ";
                        select += tableList[0] + ".NET_TOTAL, ";
                    }
                    else if (this.WindowID == WINDOW_ID.R_UNNCHIN_MEISAIHYOU)
                    {  // R398(運賃明細票)
                        select += tableList[1] + ".KINGAKU, ";
                        select += tableList[1] + ".HINMEI_KINGAKU, ";
                        select += tableList[1] + ".TAX_SOTO, ";
                        select += tableList[1] + ".TAX_UCHI, ";
                        select += tableList[1] + ".HINMEI_TAX_SOTO, ";
                        select += tableList[1] + ".HINMEI_TAX_UCHI, ";
                        select += tableList[1] + ".DENPYOU_NUMBER, ";
                        select += tableList[1] + ".ROW_NO, ";
                        select += tableList[0] + ".DENPYOU_DATE, ";

                        select += tableList[0] + ".KAKUTEI_KBN, ";
                        select += tableList[0] + ".URIAGE_KINGAKU_TOTAL, ";
                        select += tableList[0] + ".HINMEI_URIAGE_KINGAKU_TOTAL, ";
                        select += tableList[0] + ".SHIHARAI_KINGAKU_TOTAL, ";
                        select += tableList[0] + ".HINMEI_SHIHARAI_KINGAKU_TOTAL, ";
                        select += tableList[0] + ".URIAGE_TAX_SOTO, ";
                        select += tableList[0] + ".URIAGE_TAX_UCHI, ";
                        select += tableList[0] + ".URIAGE_TAX_SOTO_TOTAL, ";
                        select += tableList[0] + ".URIAGE_TAX_UCHI_TOTAL, ";
                        select += tableList[0] + ".HINMEI_URIAGE_TAX_SOTO_TOTAL, ";
                        select += tableList[0] + ".HINMEI_URIAGE_TAX_UCHI_TOTAL, ";
                        select += tableList[0] + ".SHIHARAI_TAX_SOTO, ";
                        select += tableList[0] + ".SHIHARAI_TAX_UCHI, ";
                        select += tableList[0] + ".SHIHARAI_TAX_SOTO_TOTAL, ";
                        select += tableList[0] + ".SHIHARAI_TAX_UCHI_TOTAL, ";
                        select += tableList[0] + ".HINMEI_SHIHARAI_TAX_SOTO_TOTAL, ";
                        select += tableList[0] + ".HINMEI_SHIHARAI_TAX_UCHI_TOTAL, ";
                    }
                }

                // 対象テーブル名
                if (
                    this.WindowID == WINDOW_ID.R_URIAGE_MEISAIHYOU ||
                    this.WindowID == WINDOW_ID.R_URIAGE_SYUUKEIHYOU ||
                    this.WindowID == WINDOW_ID.R_SHIHARAI_MEISAIHYOU ||
                    this.WindowID == WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU ||
                    this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU ||
                    this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_SYUUKEIHYOU ||

                    this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU ||
                    this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU ||
                    this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_SUIIHYOU ||
                    this.WindowID == WINDOW_ID.R_KEIRYOU_SUIIHYOU ||

                    this.WindowID == WINDOW_ID.R_URIAGE_JYUNNIHYOU ||
                    this.WindowID == WINDOW_ID.R_SHIHARAI_JYUNNIHYOU ||
                    this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_JYUNNIHYOU ||
                    this.WindowID == WINDOW_ID.R_KEIRYOU_JYUNNIHYOU ||

                    this.WindowID == WINDOW_ID.R_URIAGE_ZENNEN_TAIHIHYOU ||
                    this.WindowID == WINDOW_ID.R_SHIHARAI_ZENNEN_TAIHIHYOU ||
                    this.WindowID == WINDOW_ID.R_URIAGE_SHIHARAI_ZENNEN_TAIHIHYOU ||
                    this.WindowID == WINDOW_ID.R_KEIRYOU_ZENNEN_TAIHIHYOU)
                {
                    table += tableList[0] + " INNER JOIN (" + tableList[1] + " INNER JOIN M_HINMEI ON " + tableList[1] + ".HINMEI_CD = M_HINMEI.HINMEI_CD) ON (" + tableList[0] + ".SYSTEM_ID = " + tableList[1] + ".SYSTEM_ID) AND (" + tableList[0] + ".SEQ = " + tableList[1] + ".SEQ)";

                    if ((indexTable == 1) &&
                     (this.WindowID == WINDOW_ID.R_URIAGE_SUIIHYOU ||
                      this.WindowID == WINDOW_ID.R_SHIHARAI_SUIIHYOU ||
                      this.WindowID == WINDOW_ID.R_URIAGE_JYUNNIHYOU ||
                      this.WindowID == WINDOW_ID.R_SHIHARAI_JYUNNIHYOU))
                    {
                        // 出荷かつ、売上(支払)推移表もしくは売上(支払)順位表出力時は検収もJOINする
                        table += " LEFT JOIN (T_KENSHU_DETAIL INNER JOIN M_HINMEI AS KENSHU_HINMEI ON T_KENSHU_DETAIL.HINMEI_CD = KENSHU_HINMEI.HINMEI_CD) ON (" + tableList[1] + ".SYSTEM_ID = T_KENSHU_DETAIL.SYSTEM_ID) AND (" + tableList[1] + ".SEQ = T_KENSHU_DETAIL.SEQ) AND (" + tableList[1] + ".DETAIL_SYSTEM_ID = T_KENSHU_DETAIL.DETAIL_SYSTEM_ID)";
                        where += tableList[0] + ".KENSHU_MUST_KBN = 0 AND ";
                    }
                }
                else if (this.WindowID == WINDOW_ID.R_KEIRYOU_MEISAIHYOU ||
                         this.WindowID == WINDOW_ID.R_UKETSUKE_MEISAIHYOU ||
                         this.WindowID == WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU)
                {
                    table += tableList[0] + " INNER JOIN (" + tableList[1] + " INNER JOIN M_HINMEI ON " + tableList[1] + ".HINMEI_CD = M_HINMEI.HINMEI_CD) ON (" + tableList[0] + ".SYSTEM_ID = " + tableList[1] + ".SYSTEM_ID) AND (" + tableList[0] + ".SEQ = " + tableList[1] + ".SEQ)";
                }
                else if (this.WindowID == WINDOW_ID.R_SEIKYUU_MEISAIHYOU)
                {
                    table += tableList[0] + " LEFT JOIN (SELECT SEIKYUU_NUMBER, COUNT(*) AS ROW_COUNT FROM T_SEIKYUU_DETAIL WHERE DELETE_FLG = 0 GROUP BY SEIKYUU_NUMBER) AS T_SEIKYUU_DETAIL ON T_SEIKYUU_DENPYOU.SEIKYUU_NUMBER = T_SEIKYUU_DETAIL.SEIKYUU_NUMBER ";
                }
                else if (this.WindowID == WINDOW_ID.R_SHIHARAIMEISAI_MEISAIHYOU)
                {
                    table += tableList[0] + " LEFT JOIN (SELECT SEISAN_NUMBER, COUNT(*) AS ROW_COUNT FROM T_SEISAN_DETAIL WHERE DELETE_FLG = 0 GROUP BY SEISAN_NUMBER) AS T_SEISAN_DETAIL ON T_SEISAN_DENPYOU.SEISAN_NUMBER = T_SEISAN_DETAIL.SEISAN_NUMBER ";
                }
                else
                {
                    if (tableList[1] != string.Empty)
                    {
                        table += tableList[0] + " INNER JOIN " + tableList[1] + " ON (" + tableList[0] + ".SYSTEM_ID = " + tableList[1] + ".SYSTEM_ID) AND (" + tableList[0] + ".SEQ = " + tableList[1] + ".SEQ)";
                    }
                    else
                    {
                        table += tableList[0];
                    }
                }

                table += " ";

                where += tableList[0] + ".DELETE_FLG = 0 AND ";
                string orderBy = string.Empty;
                tmp = string.Empty;
                if (tableList[0] != string.Empty)
                {
                    if (this.KikanShiteiType == KIKAN_SHITEI_TYPE.Shitei)
                    {
                        // 日付範囲
                        if (tableList[0] == "T_SEIKYUU_DENPYOU")
                        {   // 請求関係
                            if ((this.WindowID == WINDOW_ID.R_NYUUKIN_YOTEI_ICHIRANHYOU) || (this.WindowID == WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU))
                            {   // 入金予定一覧表、未入金一覧表は請求日ではなく入金予定日による日付条件

                                if (!this.DateTimeStart.Equals(new DateTime(0)) && !this.DateTimeEnd.Equals(new DateTime(0)))
                                {   // 開始日及び終了日が共にある
                                    tmp = ".NYUUKIN_YOTEI_BI BETWEEN ";
                                }
                                else if (!this.DateTimeStart.Equals(new DateTime(0)) && this.DateTimeEnd.Equals(new DateTime(0)))
                                {   // 開始日があり及び終了日がない
                                    tmp = ".NYUUKIN_YOTEI_BI >= ";
                                }
                                else if (this.DateTimeStart.Equals(new DateTime(0)) && !this.DateTimeEnd.Equals(new DateTime(0)))
                                {   // 開始日がない及び終了日がある
                                    tmp = ".NYUUKIN_YOTEI_BI <= ";
                                }
                            }
                            else
                            {
                                if (!this.DateTimeStart.Equals(new DateTime(0)) && !this.DateTimeEnd.Equals(new DateTime(0)))
                                {   // 開始日及び終了日が共にある
                                    tmp = ".SEIKYUU_DATE BETWEEN ";
                                }
                                else if (!this.DateTimeStart.Equals(new DateTime(0)) && this.DateTimeEnd.Equals(new DateTime(0)))
                                {   // 開始日があり及び終了日がない
                                    tmp = ".SEIKYUU_DATE >= ";
                                }
                                else if (this.DateTimeStart.Equals(new DateTime(0)) && !this.DateTimeEnd.Equals(new DateTime(0)))
                                {   // 開始日がない及び終了日がある
                                    tmp = ".SEIKYUU_DATE <= ";
                                }
                            }
                        }
                        else if (tableList[0] == "T_SEISAN_DENPYOU")
                        {   // 精算関係
                            if ((this.WindowID == WINDOW_ID.R_SYUKKIN_YOTEI_ICHIRANHYOU) || (this.WindowID == WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU))
                            {   // 出金予定一覧表、未払金一覧表は精算日ではなく出金予定日による日付条件

                                if (!this.DateTimeStart.Equals(new DateTime(0)) && !this.DateTimeEnd.Equals(new DateTime(0)))
                                {   // 開始日及び終了日が共にある
                                    tmp = ".SHUKKIN_YOTEI_BI BETWEEN ";
                                }
                                else if (!this.DateTimeStart.Equals(new DateTime(0)) && this.DateTimeEnd.Equals(new DateTime(0)))
                                {   // 開始日があり及び終了日がない
                                    tmp = ".SHUKKIN_YOTEI_BI >= ";
                                }
                                else if (this.DateTimeStart.Equals(new DateTime(0)) && !this.DateTimeEnd.Equals(new DateTime(0)))
                                {   // 開始日がない及び終了日がある
                                    tmp = ".SHUKKIN_YOTEI_BI <= ";
                                }
                            }
                            else
                            {
                                if (!this.DateTimeStart.Equals(new DateTime(0)) && !this.DateTimeEnd.Equals(new DateTime(0)))
                                {   // 開始日及び終了日が共にある
                                    tmp = ".SEISAN_DATE BETWEEN ";
                                }
                                else if (!this.DateTimeStart.Equals(new DateTime(0)) && this.DateTimeEnd.Equals(new DateTime(0)))
                                {   // 開始日があり及び終了日がない
                                    tmp = ".SEISAN_DATE >= ";
                                }
                                else if (this.DateTimeStart.Equals(new DateTime(0)) && !this.DateTimeEnd.Equals(new DateTime(0)))
                                {   // 開始日がない及び終了日がある
                                    tmp = ".SEISAN_DATE <= ";
                                }
                            }
                        }
                        else if (tableList[0] == "T_UKETSUKE_SS_ENTRY" || tableList[0] == "T_UKETSUKE_SK_ENTRY" || tableList[0] == "T_UKETSUKE_MK_ENTRY" || tableList[0] == "T_UKETSUKE_BP_ENTRY")
                        {   // 受付関係
                            if (!this.DateTimeStart.Equals(new DateTime(0)) && !this.DateTimeEnd.Equals(new DateTime(0)))
                            {   // 開始日及び終了日が共にある
                                if (this.WindowID == WINDOW_ID.R_UKETSUKE_MEISAIHYOU)
                                {
                                    // R342 受付明細表
                                    if (tableList[0] == "T_UKETSUKE_BP_ENTRY")
                                    {
                                        tmp = ".NOHIN_YOTEI_DATE BETWEEN ";
                                    }
                                    else
                                    {
                                        tmp = ".SAGYOU_DATE BETWEEN ";
                                    }
                                }
                                else
                                {
                                    tmp = ".UKETSUKE_DATE BETWEEN ";
                                }
                            }
                            else if (!this.DateTimeStart.Equals(new DateTime(0)) && this.DateTimeEnd.Equals(new DateTime(0)))
                            {   // 開始日があり及び終了日がない
                                if (this.WindowID == WINDOW_ID.R_UKETSUKE_MEISAIHYOU)
                                {
                                    // R342 受付明細表
                                    if (tableList[0] == "T_UKETSUKE_BP_ENTRY")
                                    {
                                        tmp = ".NOHIN_YOTEI_DATE >= ";
                                    }
                                    else
                                    {
                                        tmp = ".SAGYOU_DATE >= ";
                                    }
                                }
                                else
                                {
                                    tmp = ".UKETSUKE_DATE >= ";
                                }
                            }
                            else if (this.DateTimeStart.Equals(new DateTime(0)) && !this.DateTimeEnd.Equals(new DateTime(0)))
                            {   // 開始日がない及び終了日がある
                                if (this.WindowID == WINDOW_ID.R_UKETSUKE_MEISAIHYOU)
                                {
                                    // R342 受付明細表
                                    if (tableList[0] == "T_UKETSUKE_BP_ENTRY")
                                    {
                                        tmp = ".NOHIN_YOTEI_DATE <= ";
                                    }
                                    else
                                    {
                                        tmp = ".SAGYOU_DATE <= ";
                                    }
                                }
                                else
                                {
                                    tmp = ".UKETSUKE_DATE <= ";
                                }
                            }
                        }
                        else
                        {
                            if (!this.DateTimeStart.Equals(new DateTime(0)) && !this.DateTimeEnd.Equals(new DateTime(0)))
                            {   // 開始日及び終了日が共にある
                                tmp = ".DENPYOU_DATE BETWEEN ";
                            }
                            else if (!this.DateTimeStart.Equals(new DateTime(0)) && this.DateTimeEnd.Equals(new DateTime(0)))
                            {   // 開始日があり及び終了日がない
                                tmp = ".DENPYOU_DATE >= ";
                            }
                            else if (this.DateTimeStart.Equals(new DateTime(0)) && !this.DateTimeEnd.Equals(new DateTime(0)))
                            {   // 開始日がない及び終了日がある
                                tmp = ".DENPYOU_DATE <= ";
                            }
                        }
                    }
                    else
                    {
                        if (tableList[0] == "T_SEIKYUU_DENPYOU")
                        {   // 請求関係
                            if ((this.WindowID == WINDOW_ID.R_NYUUKIN_YOTEI_ICHIRANHYOU) || (this.WindowID == WINDOW_ID.R_MINYUUKIN_ICHIRANHYOU))
                            {   //入金予定一覧表、未入金一覧表は請求日ではなく入金予定日による日付条件
                                tmp = ".NYUUKIN_YOTEI_BI BETWEEN ";
                            }
                            else
                            {
                                tmp = ".SEIKYUU_DATE BETWEEN ";
                            }
                        }
                        else if (tableList[0] == "T_SEISAN_DENPYOU")
                        {   // 精算関係
                            if ((this.WindowID == WINDOW_ID.R_SYUKKIN_YOTEI_ICHIRANHYOU) || (this.WindowID == WINDOW_ID.R_MISYUKKIN_ICHIRANHYOU))
                            {   //出金予定一覧表、未払金一覧表は精算日ではなく出金予定日による日付条件
                                tmp = ".SHUKKIN_YOTEI_BI BETWEEN ";
                            }
                            else
                            {
                                tmp = ".SEISAN_DATE BETWEEN ";
                            }
                        }
                        else if (tableList[0] == "T_UKETSUKE_SS_ENTRY" || tableList[0] == "T_UKETSUKE_SK_ENTRY" || tableList[0] == "T_UKETSUKE_MK_ENTRY" || tableList[0] == "T_UKETSUKE_BP_ENTRY")
                        {   // 受付関係
                            if (this.WindowID == WINDOW_ID.R_UKETSUKE_MEISAIHYOU)
                            {
                                // R342 受付明細表
                                if (tableList[0] == "T_UKETSUKE_BP_ENTRY")
                                {
                                    tmp = ".NOHIN_YOTEI_DATE BETWEEN ";
                                }
                                else
                                {
                                    tmp = ".SAGYOU_DATE BETWEEN ";
                                }
                            }
                            else
                            {
                                tmp = ".UKETSUKE_DATE BETWEEN ";
                            }
                        }
                        else
                        {
                            tmp = ".DENPYOU_DATE BETWEEN ";
                        }
                    }

                    DateTime today;
                    switch (this.KikanShiteiType)
                    {
                        case CommonChouhyouBase.KIKAN_SHITEI_TYPE.Tougets:
                            // 今日の0時0分0秒000を取得
                            today = DateTime.Today;

                            // todayから今日の日付を引くと先月末なのでその次の日
                            DateTime firstDay = today.AddDays(-today.Day + 1);

                            // 来月1日の1日前
                            DateTime endDay = firstDay.AddMonths(1).AddDays(-1);

                            where +=
                                "(" +
                                tableList[0] + tmp +
                                "'" +
                                firstDay.ToString("yyyy/MM/dd") + " 00:00:00" +
                                "'" +
                                " AND " +
                                "'" +
                                endDay.ToString("yyyy/MM/dd") + " 23:59:59" +
                                "'" +
                                ")" +
                                " AND ";

                            break;
                        case CommonChouhyouBase.KIKAN_SHITEI_TYPE.Shitei:
                            if (!this.DateTimeStart.Equals(new DateTime(0)) && !this.DateTimeEnd.Equals(new DateTime(0)))
                            {   // 開始日及び終了日が共にある
                                where +=
                                    "(" +
                                    tableList[0] + tmp +
                                    "'" +
                                    this.DateTimeStart.ToString("yyyy/MM/dd") + " 00:00:00" +
                                    "'" +
                                    " AND " +
                                    "'" +
                                    this.DateTimeEnd.ToString("yyyy/MM/dd") + " 23:59:59" +
                                    "'" +
                                    ")" +
                                    " AND ";
                            }
                            else if (!this.DateTimeStart.Equals(new DateTime(0)) && this.DateTimeEnd.Equals(new DateTime(0)))
                            {   // 開始日があり及び終了日がない
                                where +=
                                    "(" +
                                    tableList[0] + tmp +
                                    "'" +
                                    this.DateTimeStart.ToString("yyyy/MM/dd") + " 00:00:00" +
                                    "'" +
                                    ")" +
                                    " AND ";
                            }
                            else if (this.DateTimeStart.Equals(new DateTime(0)) && !this.DateTimeEnd.Equals(new DateTime(0)))
                            {   // 開始日がない及び終了日がある
                                where +=
                                    "(" +
                                    tableList[0] + tmp +
                                    "'" +
                                    this.DateTimeEnd.ToString("yyyy/MM/dd") + " 23:59:59" +
                                    "'" +
                                    ")" +
                                    " AND ";
                            }

                            break;
                        default:
                        case CommonChouhyouBase.KIKAN_SHITEI_TYPE.Toujitsu:
                            // 今日の0時0分0秒000を取得
                            today = DateTime.Today;
                            where +=
                                "(" +
                                tableList[0] + tmp +
                                "'" +
                                today.ToString("yyyy/MM/dd") + " 00:00:00" +
                                "'" +
                                " AND " +
                                "'" +
                                today.ToString("yyyy/MM/dd") + " 23:59:59" +
                                "'" +
                                ")" +
                                " AND ";

                            break;
                    }

                    // 拠点指定
                    if (!this.KyotenCode.Equals("99") && !this.KyotenCode.Equals(string.Empty))
                    {   // 99（全社）でないかつ空白でない
                        where +=
                            "(" +
                            tableList[0] + ".KYOTEN_CD = " +
                            this.KyotenCode +
                            ")" +
                            " AND ";
                    }

                    // 伝票種類指定

                    // 伝票区分指定

                    foreach (int selectSyuukeiKoumoku in this.SelectSyuukeiKoumokuList)
                    {
                        SyuukeiKoumoku syuukeiKoumoku = this.SyuukeiKomokuList[selectSyuukeiKoumoku];

                        switch (syuukeiKoumoku.Type)
                        {
                            case SYUKEUKOMOKU_TYPE.TorihikisakiBetsu:       //  1：取引先別

                                #region - 1：取引先別 -

                                select += tableList[0] + "." + syuukeiKoumoku.FieldCD + ", ";

                                if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始・終了コードが共にある
                                    where +=
                                        "(" +
                                        tableList[0] + ".TORIHIKISAKI_CD BETWEEN " +
                                        "'" + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart + "'" +
                                        " AND " +
                                        "'" + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd + "'" +
                                        ")" +
                                        " AND ";
                                }
                                else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードが無く、終了コードがある
                                    where +=
                                        "(" +
                                        tableList[0] + ".TORIHIKISAKI_CD <= " + "'" + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd + "'" +
                                        ")" +
                                        " AND ";
                                }
                                else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードがあり、終了コードが無い
                                    where +=
                                        "(" +
                                        tableList[0] + ".TORIHIKISAKI_CD >= " + "'" + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart + "'" +
                                        ")" +
                                        " AND ";
                                }
                                else
                                {   // 開始コードも終了コードもない
                                }

                                orderByTmp = tableList[0] + ".TORIHIKISAKI_CD, ";
                                if (!orderBy.Contains(orderByTmp))
                                {   // まだ含まれていない
                                    orderBy += orderByTmp;
                                }

                                #endregion - 1：取引先別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.GyoshaBetsu:             //  2：業者別

                                #region - 2：業者別 -

                                select += tableList[0] + "." + syuukeiKoumoku.FieldCD + ", ";

                                if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始・終了コードが共にある
                                    where +=
                                        "(" +
                                        tableList[0] + ".GYOUSHA_CD BETWEEN '" +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        "' AND '" +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        "')" +
                                        " AND ";
                                }
                                else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードが無く、終了コードがある
                                    where +=
                                        "(" +
                                        tableList[0] + ".GYOUSHA_CD <= '" + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        "')" +
                                        " AND ";
                                }
                                else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードがあり、終了コードが無い
                                    where +=
                                        "(" +
                                        tableList[0] + ".GYOUSHA_CD >= '" + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        "')" +
                                        " AND ";
                                }
                                else
                                {   // 開始コードも終了コードもない
                                }

                                orderByTmp = tableList[0] + ".GYOUSHA_CD, ";
                                if (!orderBy.Contains(orderByTmp))
                                {   // まだ含まれていない
                                    orderBy += orderByTmp;
                                }

                                #endregion - 2：業者別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.GenbaBetsu:              //  3：現場別

                                #region - 3：現場別 -

                                select += tableList[0] + "." + syuukeiKoumoku.FieldCD + ", ";

                                if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始・終了コードが共にある
                                    where +=
                                        "(" +
                                        tableList[0] + ".GENBA_CD BETWEEN " +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        " AND " +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        ")" +
                                        " AND ";
                                }
                                else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードが無く、終了コードがある
                                    where +=
                                        "(" +
                                        tableList[0] + ".GENBA_CD <= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        ")" +
                                        " AND ";
                                }
                                else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードがあり、終了コードが無い
                                    where +=
                                        "(" +
                                        tableList[0] + ".GENBA_CD >= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        ")" +
                                        " AND ";
                                }
                                else
                                {   // 開始コードも終了コードもない
                                }

                                orderByTmp = tableList[0] + ".GENBA_CD, ";
                                if (!orderBy.Contains(orderByTmp))
                                {   // まだ含まれていない
                                    orderBy += orderByTmp;
                                }

                                #endregion - 3：現場別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.UnpanGyoshaBetsu:        //  4：運搬業者別

                                #region - 4：運搬業者別 -

                                if (this.WindowID == WINDOW_ID.R_UKETSUKE_MEISAIHYOU ||
                                    this.WindowID == WINDOW_ID.R_KEIRYOU_MEISAIHYOU || this.WindowID == WINDOW_ID.R_KEIRYOU_SYUUKEIHYOU ||
                                    this.WindowID == WINDOW_ID.R_UNNCHIN_MEISAIHYOU)
                                {   // 受付明細表・計量明細表・計量集計表・運賃明細表
                                    select += tableList[0] + ".UNPAN_GYOUSHA_CD" + ", ";
                                }
                                else
                                {
                                    select += tableList[0] + "." + syuukeiKoumoku.FieldCD + ", ";
                                }

                                if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始・終了コードが共にある
                                    where +=
                                        "(" +
                                        tableList[0] + ".UNPAN_GYOUSHA_CD BETWEEN " +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        " AND " +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        ")" +
                                        " AND ";
                                }
                                else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードが無く、終了コードがある
                                    where +=
                                        "(" +
                                        tableList[0] + ".UNPAN_GYOUSHA_CD <= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        ")" +
                                        " AND ";
                                }
                                else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードがあり、終了コードが無い
                                    where +=
                                        "(" +
                                        tableList[0] + ".UNPAN_GYOUSHA_CD >= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        ")" +
                                        " AND ";
                                }
                                else
                                {   // 開始コードも終了コードもない
                                }

                                orderByTmp = tableList[0] + ".UNPAN_GYOUSHA_CD, ";
                                if (!orderBy.Contains(orderByTmp))
                                {   // まだ含まれていない
                                    orderBy += orderByTmp;
                                }

                                #endregion - 4：運搬業者別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.NioroshiGyoshaBetsu:     //  5：荷降業者別

                                #region - 5：荷降業者別 -

                                if (tableList[0] == "T_UKEIRE_ENTRY" || tableList[0] == "T_UR_SH_ENTRY" ||
                                    tableList[0] == "T_KEIRYOU_ENTRY" ||
                                    tableList[0] == "T_UKETSUKE_SS_ENTRY" || tableList[0] == "T_UKETSUKE_SK_ENTRY" || tableList[0] == "T_UKETSUKE_MK_ENTRY" || tableList[0] == "T_UKETSUKE_BP_ENTRY" ||
                                    tableList[0] == "T_UNCHIN_ENTRY")
                                {   // 受入関係・売上支払関係・計量関係・受付関係・運賃関係
                                    select += tableList[0] + ".NIOROSHI_GYOUSHA_CD, ";

                                    if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                    {   // 開始・終了コードが共にある
                                        where +=
                                            "(" +
                                            tableList[0] + ".NIOROSHI_GYOUSHA_CD BETWEEN " +
                                            syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                            " AND " +
                                            syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                            ")" +
                                            " AND ";
                                    }
                                    else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                    {   // 開始コードが無く、終了コードがある
                                        where +=
                                            "(" +
                                            tableList[0] + ".NIOROSHI_GYOUSHA_CD <= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                            ")" +
                                            " AND ";
                                    }
                                    else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                    {   // 開始コードがあり、終了コードが無い
                                        where +=
                                            "(" +
                                            tableList[0] + ".NIOROSHI_GYOUSHA_CD >= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                            ")" +
                                            " AND ";
                                    }
                                    else
                                    {   // 開始コードも終了コードもない
                                    }

                                    orderByTmp = tableList[0] + ".NIOROSHI_GYOUSHA_CD, ";
                                    if (!orderBy.Contains(orderByTmp))
                                    {   // まだ含まれていない
                                        orderBy += orderByTmp;
                                    }
                                }

                                #endregion - 5：荷降業者別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.NioroshiGenbaBetsu:      //  6：荷降現場別

                                #region - 6：荷降現場別 -

                                if (tableList[0] == "T_UKEIRE_ENTRY" || tableList[0] == "T_UR_SH_ENTRY" ||
                                    tableList[0] == "T_KEIRYOU_ENTRY" ||
                                    tableList[0] == "T_UKETSUKE_SS_ENTRY" || tableList[0] == "T_UKETSUKE_SK_ENTRY" || tableList[0] == "T_UKETSUKE_MK_ENTRY" || tableList[0] == "T_UKETSUKE_BP_ENTRY" ||
                                    tableList[0] == "T_UNCHIN_ENTRY")
                                {   // 受入関係・売上支払関係・計量関係・受付関係・運賃関係
                                    select += tableList[0] + ".NIOROSHI_GENBA_CD, ";

                                    if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                    {   // 開始・終了コードが共にある
                                        where +=
                                            "(" +
                                            tableList[0] + ".NIOROSHI_GENBA_CD BETWEEN " +
                                            syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                            " AND " +
                                            syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                            ")" +
                                            " AND ";
                                    }
                                    else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                    {   // 開始コードが無く、終了コードがある
                                        where +=
                                            "(" +
                                            tableList[0] + ".NIOROSHI_GENBA_CD <= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                            ")" +
                                            " AND ";
                                    }
                                    else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                    {   // 開始コードがあり、終了コードが無い
                                        where +=
                                            "(" +
                                            tableList[0] + ".NIOROSHI_GENBA_CD BETWEEN >= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                            ")" +
                                            " AND ";
                                    }
                                    else
                                    {   // 開始コードも終了コードもない
                                    }

                                    orderByTmp = tableList[0] + ".NIOROSHI_GENBA_CD, ";
                                    if (!orderBy.Contains(orderByTmp))
                                    {   // まだ含まれていない
                                        orderBy += orderByTmp;
                                    }
                                }

                                #endregion - 6：荷降現場別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.NizumiGyoshaBetsu:       //  7：荷積業者別

                                #region - 7：荷積業者別 -

                                if (tableList[0] == "T_SHUKKA_ENTRY" || tableList[0] == "T_UR_SH_ENTRY" ||
                                    tableList[0] == "T_KEIRYOU_ENTRY" ||
                                    tableList[0] == "T_UKETSUKE_SS_ENTRY" || tableList[0] == "T_UKETSUKE_SK_ENTRY" || tableList[0] == "T_UKETSUKE_MK_ENTRY" || tableList[0] == "T_UKETSUKE_BP_ENTRY" ||
                                    tableList[0] == "T_UNCHIN_ENTRY")
                                {   // 出荷関係・売上支払関係・計量関係・受付関係・運賃関係
                                    select += tableList[0] + ".NIZUMI_GYOUSHA_CD, ";

                                    if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                    {   // 開始・終了コードが共にある
                                        where +=
                                            "(" +
                                            tableList[0] + ".NIZUMI_GYOUSHA_CD BETWEEN " +
                                            syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                            " AND " +
                                            syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                            ")" +
                                            " AND ";
                                    }
                                    else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                    {   // 開始コードが無く、終了コードがある
                                        where +=
                                            "(" +
                                            tableList[0] + ".NIZUMI_GYOUSHA_CD <= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                            ")" +
                                            " AND ";
                                    }
                                    else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                    {   // 開始コードがあり、終了コードが無い
                                        where +=
                                            "(" +
                                            tableList[0] + ".NIZUMI_GYOUSHA_CD >= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                            ")" +
                                            " AND ";
                                    }
                                    else
                                    {   // 開始コードも終了コードもない
                                    }

                                    orderByTmp = tableList[0] + ".NIZUMI_GYOUSHA_CD, ";
                                    if (!orderBy.Contains(orderByTmp))
                                    {   // まだ含まれていない
                                        orderBy += orderByTmp;
                                    }
                                }

                                #endregion - 7：荷積業者別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.NizumiGenbaBetsu:        //  8：荷積現場別

                                #region - 8：荷積現場別 -

                                if (tableList[0] == "T_SHUKKA_ENTRY" || tableList[0] == "T_UR_SH_ENTRY" ||
                                    tableList[0] == "T_KEIRYOU_ENTRY" ||
                                    tableList[0] == "T_UKETSUKE_SS_ENTRY" || tableList[0] == "T_UKETSUKE_SK_ENTRY" || tableList[0] == "T_UKETSUKE_MK_ENTRY" || tableList[0] == "T_UKETSUKE_BP_ENTRY" ||
                                    tableList[0] == "T_UNCHIN_ENTRY")
                                {   // 出荷関係・売上支払関係・計量関係・受付関係・運賃関係
                                    select += tableList[0] + ".NIZUMI_GENBA_CD, ";

                                    if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                    {   // 開始・終了コードが共にある
                                        where +=
                                            "(" +
                                            tableList[0] + ".NIZUMI_GENBA_CD BETWEEN " +
                                            syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                            " AND " +
                                            syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                            ")" +
                                            " AND ";
                                    }
                                    else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                    {   // 開始コードが無く、終了コードがある
                                        where +=
                                            "(" +
                                            tableList[0] + ".NIZUMI_GENBA_CD <= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                            ")" +
                                            " AND ";
                                    }
                                    else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                    {   // 開始コードがあり、終了コードが無い
                                        where +=
                                            "(" +
                                            tableList[0] + ".NIZUMI_GENBA_CD >= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                            ")" +
                                            " AND ";
                                    }
                                    else
                                    {   // 開始コードも終了コードもない
                                    }

                                    orderByTmp = tableList[0] + ".NIZUMI_GENBA_CD, ";
                                    if (!orderBy.Contains(orderByTmp))
                                    {   // まだ含まれていない
                                        orderBy += orderByTmp;
                                    }
                                }

                                #endregion - 8：荷積現場別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.EigyoTantoshaBetsu:      //  9：営業担当者別

                                #region - 9：営業担当者別 -

                                //select += "," + tableList[0] + "." + syuukeiKoumoku.FieldCD + ", ";

                                if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始・終了コードが共にある

                                    if (tableList[0] == "T_SEIKYUU_DENPYOU" || tableList[0] == "T_SEISAN_DENPYOU")
                                    {   // 請求伝票又は精算伝票
                                        select += "M_TORIHIKISAKI.EIGYOU_TANTOU_CD, ";

                                        if (tableList[0] == "T_SEIKYUU_DENPYOU")
                                        {   // 請求伝票
                                            table += "INNER JOIN M_TORIHIKISAKI ON (T_SEIKYUU_DENPYOU.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD) ";
                                        }
                                        else
                                        {   // 精算伝票
                                            table += "INNER JOIN M_TORIHIKISAKI ON (T_SEISAN_DENPYOU.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD) ";
                                        }

                                        where +=
                                            "(" +
                                            "M_TORIHIKISAKI.EIGYOU_TANTOU_CD BETWEEN " +
                                            syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                            " AND " +
                                            syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                            ")" +
                                            " AND ";

                                        orderByTmp = "M_TORIHIKISAKI.EIGYOU_TANTOU_CD, ";
                                        if (!orderBy.Contains(orderByTmp))
                                        {   // まだ含まれていない
                                            orderBy += orderByTmp;
                                        }
                                    }
                                    else
                                    {
                                        select += tableList[0] + ".EIGYOU_TANTOUSHA_CD, ";
                                        where +=
                                            "(" +
                                            tableList[0] + ".EIGYOU_TANTOUSHA_CD BETWEEN " +
                                            syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                            " AND " +
                                            syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                            ")" +
                                            " AND ";

                                        orderByTmp = tableList[0] + ".EIGYOU_TANTOUSHA_CD, ";
                                        if (!orderBy.Contains(orderByTmp))
                                        {   // まだ含まれていない
                                            orderBy += orderByTmp;
                                        }
                                    }
                                }
                                else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードが無く、終了コードがある

                                    if (tableList[0] == "T_SEIKYUU_DENPYOU" || tableList[0] == "T_SEISAN_DENPYOU")
                                    {   // 請求伝票又は精算伝票
                                        select += "M_TORIHIKISAKI.EIGYOU_TANTOU_CD, ";

                                        if (tableList[0] == "T_SEIKYUU_DENPYOU")
                                        {   // 請求伝票
                                            table += "INNER JOIN M_TORIHIKISAKI ON (T_SEIKYUU_DENPYOU.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD) ";
                                        }
                                        else
                                        {   // 精算伝票
                                            table += "INNER JOIN M_TORIHIKISAKI ON (T_SEISAN_DENPYOU.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD) ";
                                        }

                                        where +=
                                            "(" +
                                            "M_TORIHIKISAKI.EIGYOU_TANTOU_CD <= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                            ")" +
                                            " AND ";

                                        orderByTmp = "M_TORIHIKISAKI.EIGYOU_TANTOU_CD, ";
                                        if (!orderBy.Contains(orderByTmp))
                                        {   // まだ含まれていない
                                            orderBy += orderByTmp;
                                        }
                                    }
                                    else
                                    {
                                        select += tableList[0] + ".EIGYOU_TANTOUSHA_CD, ";
                                        where +=
                                            "(" +
                                            tableList[0] + ".EIGYOU_TANTOUSHA_CD <= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                            ")" +
                                            " AND ";

                                        orderByTmp = tableList[0] + ".EIGYOU_TANTOUSHA_CD, ";
                                        if (!orderBy.Contains(orderByTmp))
                                        {   // まだ含まれていない
                                            orderBy += orderByTmp;
                                        }
                                    }
                                }
                                else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードがあり、終了コードが無い

                                    if (tableList[0] == "T_SEIKYUU_DENPYOU" || tableList[0] == "T_SEISAN_DENPYOU")
                                    {   // 請求伝票又は精算伝票
                                        select += "M_TORIHIKISAKI.EIGYOU_TANTOU_CD, ";

                                        if (tableList[0] == "T_SEIKYUU_DENPYOU")
                                        {   // 請求伝票
                                            table += "INNER JOIN M_TORIHIKISAKI ON (T_SEIKYUU_DENPYOU.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD) ";
                                        }
                                        else
                                        {   // 精算伝票
                                            table += "INNER JOIN M_TORIHIKISAKI ON (T_SEISAN_DENPYOU.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD) ";
                                        }

                                        where +=
                                            "(" +
                                            "M_TORIHIKISAKI.EIGYOU_TANTOU_CD >= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                            ")" +
                                            " AND ";

                                        orderByTmp = "M_TORIHIKISAKI.EIGYOU_TANTOU_CD, ";
                                        if (!orderBy.Contains(orderByTmp))
                                        {   // まだ含まれていない
                                            orderBy += orderByTmp;
                                        }
                                    }
                                    else
                                    {
                                        select += tableList[0] + ".EIGYOU_TANTOUSHA_CD, ";
                                        where +=
                                            "(" +
                                            tableList[0] + ".EIGYOU_TANTOUSHA_CD >= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                            ")" +
                                            " AND ";

                                        orderByTmp = tableList[0] + ".EIGYOU_TANTOUSHA_CD, ";
                                        if (!orderBy.Contains(orderByTmp))
                                        {   // まだ含まれていない
                                            orderBy += orderByTmp;
                                        }
                                    }
                                }
                                else
                                {   // 開始コードも終了コードもない
                                    if (tableList[0] == "T_SEIKYUU_DENPYOU" || tableList[0] == "T_SEISAN_DENPYOU")
                                    {   // 請求伝票又は精算伝票
                                        select += "M_TORIHIKISAKI.EIGYOU_TANTOU_CD, ";

                                        if (tableList[0] == "T_SEIKYUU_DENPYOU")
                                        {   // 請求伝票
                                            table += "INNER JOIN M_TORIHIKISAKI ON (T_SEIKYUU_DENPYOU.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD) ";
                                        }
                                        else
                                        {   // 精算伝票
                                            table += "INNER JOIN M_TORIHIKISAKI ON (T_SEISAN_DENPYOU.TORIHIKISAKI_CD = M_TORIHIKISAKI.TORIHIKISAKI_CD) ";
                                        }

                                        orderByTmp = "M_TORIHIKISAKI.EIGYOU_TANTOU_CD, ";
                                        if (!orderBy.Contains(orderByTmp))
                                        {   // まだ含まれていない
                                            orderBy += orderByTmp;
                                        }
                                    }
                                    else
                                    {
                                        select += tableList[0] + ".EIGYOU_TANTOUSHA_CD, ";

                                        orderByTmp = tableList[0] + ".EIGYOU_TANTOUSHA_CD, ";
                                        if (!orderBy.Contains(orderByTmp))
                                        {   // まだ含まれていない
                                            orderBy += orderByTmp;
                                        }
                                    }
                                }

                                #endregion - 9：営業担当者別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.KyotenBetsu:             // 10：拠点別

                                #region - 10：拠点別 -

                                select += tableList[0] + "." + syuukeiKoumoku.FieldCD + ", ";

                                if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始・終了コードが共にある

                                    where +=
                                        "(" +
                                        tableList[0] + ".KYOTEN_CD BETWEEN " +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        " AND " +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        ")" +
                                        " AND ";
                                }
                                else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードが無く、終了コードがある

                                    where +=
                                        "(" +
                                        tableList[0] + ".KYOTEN_CD <= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        ")" +
                                        " AND ";
                                }
                                else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードがあり、終了コードが無い

                                    where +=
                                        "(" +
                                        tableList[0] + ".KYOTEN_CD >= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        ")" +
                                        " AND ";
                                }
                                else
                                {   // 開始コードも終了コードもない
                                }

                                orderByTmp = tableList[0] + ".KYOTEN_CD, ";
                                if (!orderBy.Contains(orderByTmp))
                                {   // まだ含まれていない
                                    orderBy += orderByTmp;
                                }

                                #endregion - 10：拠点別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.SyuruiBetsu:             // 12：種類別

                                #region - 12：種類別 -

                                select += "M_HINMEI." + syuukeiKoumoku.FieldCD + ", ";

                                if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始・終了コードが共にある

                                    where +=
                                        "(" +
                                        "M_HINMEI.SHURUI_CD BETWEEN " +
                                        "'" + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart + "'" +
                                        " AND " +
                                        "'" + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd + "'" +
                                        ")" +
                                        " AND ";
                                }
                                else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードが無く、終了コードがある

                                    where +=
                                        "(" +
                                        "M_HINMEI.SHURUI_CD <= " + "'" + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd + "'" +
                                        ")" +
                                        " AND ";
                                }
                                else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードがあり、終了コードが無い

                                    where +=
                                        "(" +
                                        "M_HINMEI.SHURUI_CD >= " + "'" + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart + "'" +
                                        ")" +
                                        " AND ";
                                }
                                else
                                {   // 開始コードも終了コードもない
                                }

                                orderByTmp = "M_HINMEI.SHURUI_CD, ";
                                if (!orderBy.Contains(orderByTmp))
                                {   // まだ含まれていない
                                    orderBy += orderByTmp;
                                }

                                #endregion - 12：種類別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.BunruiBetsu:             // 13：分類別

                                #region - 13：分類別 -

                                select += "M_HINMEI." + syuukeiKoumoku.FieldCD + ", ";

                                if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始・終了コードが共にある

                                    where +=
                                        "(" +
                                        "M_HINMEI.BUNRUI_CD BETWEEN " +
                                        "'" + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart + "'" +
                                        " AND " +
                                        "'" + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd + "'" +
                                        ")" +
                                        " AND ";
                                }
                                else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードが無く、終了コードがある

                                    where +=
                                        "(" +
                                        "M_HINMEI.BUNRUI_CD <= " + "'" + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd + "'" +
                                        ")" +
                                        " AND ";
                                }
                                else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードがあり、終了コードが無い

                                    where +=
                                        "(" +
                                        "M_HINMEI.BUNRUI_CD >= " + "'" + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart + "'" +
                                        ")" +
                                        " AND ";
                                }
                                else
                                {   // 開始コードも終了コードもない
                                }

                                orderByTmp = "M_HINMEI.BUNRUI_CD, ";
                                if (!orderBy.Contains(orderByTmp))
                                {   // まだ含まれていない
                                    orderBy += orderByTmp;
                                }

                                #endregion - 13：分類別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.HinmeiBetsu:             // 14：品名別

                                #region - 14：品名別 -

                                select += "M_HINMEI." + syuukeiKoumoku.FieldCD + ", ";

                                if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始・終了コードが共にある

                                    where +=
                                        "(" +
                                        "M_HINMEI.HINMEI_CD BETWEEN " +
                                        "'" + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart + "'" +
                                        " AND " +
                                        "'" + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd + "'" +
                                        ")" +
                                        " AND ";
                                }
                                else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードが無く、終了コードがある

                                    where +=
                                        "(" +
                                        "M_HINMEI.HINMEI_CD <= " + "'" + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd + "'" +
                                        ")" +
                                        " AND ";
                                }
                                else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードがあり、終了コードが無い

                                    where +=
                                        "(" +
                                        "M_HINMEI.HINMEI_CD >= " + "'" + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart + "'" +
                                        ")" +
                                        " AND ";
                                }
                                else
                                {   // 開始コードも終了コードもない
                                }

                                orderByTmp = "M_HINMEI.HINMEI_CD, ";
                                if (!orderBy.Contains(orderByTmp))
                                {   // まだ含まれていない
                                    orderBy += orderByTmp;
                                }

                                #endregion - 14：品名別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.ShasyuBetsu:             // 15：車種別

                                #region - 15：車種別 -

                                select += tableList[0] + "." + syuukeiKoumoku.FieldCD + ", ";

                                if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始・終了コードが共にある

                                    where +=
                                        "(" +
                                        tableList[0] + ".SHASHU_CD BETWEEN " +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        " AND " +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        ")" +
                                        " AND ";
                                }
                                else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードが無く、終了コードがある

                                    where +=
                                        "(" +
                                        tableList[0] + ".SHASHU_CD <= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        ")" +
                                        " AND ";
                                }
                                else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードがあり、終了コードが無い

                                    where +=
                                        "(" +
                                        tableList[0] + ".SHASHU_CD >= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        ")" +
                                        " AND ";
                                }
                                else
                                {   // 開始コードも終了コードもない
                                }

                                orderByTmp = tableList[0] + ".SHASHU_CD, ";
                                if (!orderBy.Contains(orderByTmp))
                                {   // まだ含まれていない
                                    orderBy += orderByTmp;
                                }

                                #endregion - 15：車種別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.SharyoBetsu:             // 16：車輌別

                                #region - 16：車輌別 -

                                select += tableList[0] + "." + syuukeiKoumoku.FieldCD + ", ";

                                if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始・終了コードが共にある

                                    where +=
                                        "(" +
                                        tableList[0] + ".SHARYOU_CD BETWEEN " +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        " AND " +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        ")" +
                                        " AND ";
                                }
                                else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードが無く、終了コードがある

                                    where +=
                                        "(" +
                                        tableList[0] + ".SHARYOU_CD <= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        ")" +
                                        " AND ";
                                }
                                else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードがあり、終了コードが無い

                                    where +=
                                        "(" +
                                        tableList[0] + ".SHARYOU_CD >= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        ")" +
                                        " AND ";
                                }
                                else
                                {   // 開始コードも終了コードもない
                                    if (!syuukeiKoumoku.IsSelectGenbaAll)
                                    {
                                        where +=
                                            "(" +
                                            tableList[0] + ".SHARYOU_CD = '' OR " + tableList[0] +
                                            ".SHARYOU_CD IS NULL)" +
                                            " AND ";
                                    }
                                }

                                orderByTmp = tableList[0] + ".SHARYOU_CD, ";
                                if (!orderBy.Contains(orderByTmp))
                                {   // まだ含まれていない
                                    orderBy += orderByTmp;
                                }

                                #endregion - 16：車輌別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.UntenshaBetsu:           // 17：運転者別

                                #region - 17：運転者別 -

                                select += tableList[0] + "." + syuukeiKoumoku.FieldCD + ", ";

                                if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始・終了コードが共にある

                                    where +=
                                        "(" +
                                        tableList[0] + ".UNTENSHA_CD BETWEEN " +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        " AND " +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        ")" +
                                        " AND ";
                                }
                                else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードが無く、終了コードがある

                                    where +=
                                        "(" +
                                        tableList[0] + ".UNTENSHA_CD <= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        ")" +
                                        " AND ";
                                }
                                else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードがあり、終了コードが無い

                                    where +=
                                        "(" +
                                        tableList[0] + ".UNTENSHA_CD >= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        ")" +
                                        " AND ";
                                }
                                else
                                {   // 開始コードも終了コードもない
                                }

                                orderByTmp = tableList[0] + ".UNTENSHA_CD, ";
                                if (!orderBy.Contains(orderByTmp))
                                {   // まだ含まれていない
                                    orderBy += orderByTmp;
                                }

                                #endregion - 17：運転者別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.DenpyoKubunBetsu:        // 18：伝票区分別

                                #region - 18：伝票区分別 -

                                select += tableList[1] + "." + syuukeiKoumoku.FieldCD + ", ";

                                // 伝票区分は画面の種類によっては固定的に扱われる
                                switch (this.WindowID)
                                {
                                    case WINDOW_ID.R_URIAGE_MEISAIHYOU:
                                    case WINDOW_ID.R_URIAGE_SYUUKEIHYOU:
                                        where += "(" + tableList[1] + ".DENPYOU_KBN_CD BETWEEN 1 AND 1" + ")" + " AND ";
                                        break;
                                    case WINDOW_ID.R_SHIHARAI_SYUUKEIHYOU:
                                    case WINDOW_ID.R_SHIHARAI_MEISAIHYOU:
                                        where += "(" + tableList[1] + ".DENPYOU_KBN_CD BETWEEN 2 AND 2" + ")" + " AND ";
                                        break;
                                    default:
                                        if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                        {   // 開始・終了コードが共にある

                                            where +=
                                            "(" +
                                            tableList[1] + ".DENPYOU_KBN_CD BETWEEN " +
                                            syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                            " AND " +
                                            syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                            ")" +
                                            " AND ";
                                        }
                                        else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                        {   // 開始コードが無く、終了コードがある

                                            where +=
                                            "(" +
                                            tableList[1] + ".DENPYOU_KBN_CD <= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                            ")" +
                                            " AND ";
                                        }
                                        else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                        {   // 開始コードがあり、終了コードが無い

                                            where +=
                                            "(" +
                                            tableList[1] + ".DENPYOU_KBN_CD >= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                            ")" +
                                            " AND ";
                                        }
                                        else
                                        {   // 開始コードも終了コードもない
                                        }

                                        break;
                                }

                                orderByTmp = tableList[1] + ".DENPYOU_KBN_CD, ";
                                if (!orderBy.Contains(orderByTmp))
                                {   // まだ含まれていない
                                    orderBy += orderByTmp;
                                }

                                #endregion - 18：伝票区分別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.DensyuKubunBetsu:        // 19：伝種区分別

                                #region - 19：伝種区分別 -

                                #endregion - 19：伝種区分別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.NyukinsakiBetsu:         // 20：入金先別

                                #region - 20：入金先別 -

                                select += tableList[0] + "." + syuukeiKoumoku.FieldCD + ", ";

                                if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始・終了コードが共にある

                                    where +=
                                        "(" +
                                        tableList[0] + ".NYUUKINSAKI_CD BETWEEN " +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        " AND " +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        ")" +
                                        " AND ";
                                }
                                else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードが無く、終了コードがある

                                    where +=
                                        "(" +
                                        tableList[0] + ".NYUUKINSAKI_CD <= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        ")" +
                                        " AND ";
                                }
                                else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードがあり、終了コードが無い

                                    where +=
                                        "(" +
                                        tableList[0] + ".NYUUKINSAKI_CD >= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        ")" +
                                        " AND ";
                                }
                                else
                                {   // 開始コードも終了コードもない
                                }

                                orderByTmp = tableList[0] + ".NYUUKINSAKI_CD, ";
                                if (!orderBy.Contains(orderByTmp))
                                {   // まだ含まれていない
                                    orderBy += orderByTmp;
                                }

                                #endregion - 20：入金先別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.GinkoBetsu:              // 21：銀行別

                                #region - 21：銀行別 -

                                select += tableList[0] + "." + syuukeiKoumoku.FieldCD + ", ";

                                if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始・終了コードが共にある

                                    where +=
                                        "(" +
                                        tableList[0] + ".BANK_CD BETWEEN " +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        " AND " +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        ")" +
                                        " AND ";
                                }
                                else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードが無く、終了コードがある

                                    where +=
                                        "(" +
                                        tableList[0] + ".BANK_CD <= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        ")" +
                                        " AND ";
                                }
                                else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードがあり、終了コードが無い

                                    where +=
                                        "(" +
                                        tableList[0] + ".BANK_CD >= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        ")" +
                                        " AND ";
                                }
                                else
                                {   // 開始コードも終了コードもない
                                }

                                orderByTmp = tableList[0] + ".BANK_CD, ";
                                if (!orderBy.Contains(orderByTmp))
                                {   // まだ含まれていない
                                    orderBy += orderByTmp;
                                }

                                #endregion - 21：銀行別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.GinkoShitenBetsu:        // 22：銀行支店別

                                #region - 22：銀行支店別 -

                                select += tableList[0] + "." + syuukeiKoumoku.FieldCD + ", ";

                                if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始・終了コードが共にある

                                    where +=
                                        "(" +
                                        tableList[0] + ".BANK_SHITEN_CD BETWEEN " +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        " AND " +
                                        syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        ")" +
                                        " AND ";
                                }
                                else if (syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && !syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードが無く、終了コードがある

                                    where +=
                                        "(" +
                                        tableList[0] + ".BANK_SHITEN_CD <= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd +
                                        ")" +
                                        " AND ";
                                }
                                else if (!syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart.Equals(string.Empty) && syuukeiKoumoku.SyuukeiKoumokuHani.CodeEnd.Equals(string.Empty))
                                {   // 開始コードがあり、終了コードが無い

                                    where +=
                                        "(" +
                                        tableList[0] + ".BANK_SHITEN_CD >= " + syuukeiKoumoku.SyuukeiKoumokuHani.CodeStart +
                                        ")" +
                                        " AND ";
                                }
                                else
                                {   // 開始コードも終了コードもない
                                }

                                orderByTmp = tableList[0] + ".BANK_SHITEN_CD, ";
                                if (!orderBy.Contains(orderByTmp))
                                {   // まだ含まれていない
                                    orderBy += orderByTmp;
                                }

                                #endregion - 22：銀行支店別 -

                                break;
                            case SYUKEUKOMOKU_TYPE.HidukeBetsu:             // 23：日付別

                                #region - 23：日付別 -

                                if (tableList[0] == "T_SEIKYUU_DENPYOU")
                                {   // 請求関係
                                    orderByTmp = tableList[0] + ".SEIKYUU_DATE, ";
                                    if (!orderBy.Contains(orderByTmp))
                                    {   // まだ含まれていない
                                        orderBy += orderByTmp;
                                    }
                                }
                                else if (tableList[0] == "T_SEISAN_DENPYOU")
                                {   // 精算関係
                                    orderByTmp = tableList[0] + ".SEISAN_DATE, ";
                                    if (!orderBy.Contains(orderByTmp))
                                    {   // まだ含まれていない
                                        orderBy += orderByTmp;
                                    }
                                }

                                #endregion - 23：日付別 -

                                break;
                            default:

                                break;
                        }
                    }
                }

                // 末尾の", "を削除
                select = select.Substring(0, select.Length - 2) + " ";

                // 末尾の" AND "を削除
                where = where.Substring(0, where.Length - 5);

                sql = select + "FROM " + table + "WHERE " + where;

                if (group.Length != 0)
                {
                    // 末尾の", "を削除
                    group = group.Substring(0, group.Length - 2);

                    sql += " GROUP BY " + group;
                }

                // 追加ソート項目取得
                string addOrderBy = AddOrder(orderBy, indexTable);
                orderBy += addOrderBy;

                if (orderBy.Length != 0)
                {
                    // 末尾の", "を削除
                    orderBy = orderBy.Substring(0, orderBy.Length - 2);

                    sql += " ORDER BY " + orderBy;
                }

                return sql;
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);

                return string.Empty;
            }
        }

        /// <summary>
        /// 集計項目とは別に追加ソート項目を取得する
        /// </summary>
        /// <param name="order">作成済みのorder句</param>
        /// <returns></returns>
        private string AddOrder(string orderBy, int indexTable)
        {
            string addOrderBy = string.Empty;
            string orderByTmp = string.Empty;

            switch (this.WindowID)
            {
                case WINDOW_ID.R_SEIKYUU_MEISAIHYOU:
                    orderByTmp = "T_SEIKYUU_DENPYOU.SEIKYUU_NUMBER, ";
                    if (!orderBy.Contains(orderByTmp))
                    {
                        addOrderBy += orderByTmp;
                    }
                    break;
                case WINDOW_ID.R_SHIHARAIMEISAI_MEISAIHYOU:
                    orderByTmp = "T_SEISAN_DENPYOU.SEISAN_NUMBER, ";
                    if (!orderBy.Contains(orderByTmp))
                    {
                        addOrderBy += orderByTmp;
                    }
                    break;
                case WINDOW_ID.R_URIAGE_MEISAIHYOU:
                case WINDOW_ID.R_SHIHARAI_MEISAIHYOU:
                case WINDOW_ID.R_URIAGE_SHIHARAI_MEISAIHYOU:
                    if (0 == indexTable)
                    {
                        orderByTmp = "T_UKEIRE_ENTRY.UKEIRE_NUMBER, ";
                        if (!orderBy.Contains(orderByTmp))
                        {
                            addOrderBy += orderByTmp;
                        }
                        orderByTmp = "T_UKEIRE_DETAIL.ROW_NO, ";
                        if (!orderBy.Contains(orderByTmp))
                        {
                            addOrderBy += orderByTmp;
                        }
                    }
                    else if (1 == indexTable)
                    {
                        orderByTmp = "T_SHUKKA_ENTRY.SHUKKA_NUMBER, ";
                        if (!orderBy.Contains(orderByTmp))
                        {
                            addOrderBy += orderByTmp;
                        }
                        orderByTmp = "T_SHUKKA_DETAIL.ROW_NO, ";
                        if (!orderBy.Contains(orderByTmp))
                        {
                            addOrderBy += orderByTmp;
                        }
                    }
                    else if (2 == indexTable)
                    {
                        orderByTmp = "T_UR_SH_ENTRY.UR_SH_NUMBER, ";
                        if (!orderBy.Contains(orderByTmp))
                        {
                            addOrderBy += orderByTmp;
                        }
                        orderByTmp = "T_UR_SH_DETAIL.ROW_NO, ";
                        if (!orderBy.Contains(orderByTmp))
                        {
                            addOrderBy += orderByTmp;
                        }
                    }

                    break;
                default:
                    break;
            }

            return addOrderBy;
        }
        #endregion - Methods -

        #region - Inner Classes -

        /// <summary>対象テーブルを表すクラス・コントロール</summary>
        public class TaishouTable
        {
            #region - Constructors -

            /// <summary>Initializes a new instance of the <see cref="TaishouTable"/> class.</summary>
            /// <param name="table1">対象テーブル１を表す文字列</param>
            /// <param name="table2">対象テーブル２を表す文字列</param>
            public TaishouTable(string table1, string table2 = "")
            {
                this.TableName = new string[2];

                // 対象テーブル１
                this.TableName[0] = table1;

                // 対象テーブル２
                this.TableName[1] = table2;
            }

            #endregion - Constructors -

            #region - Properties -

            /// <summary>対象テーブル１を保持するプロパティ</summary>
            public string[] TableName { get; set; }

            #endregion - Properties -
        }

        /// <summary>複数テーブルソートデータを表すクラス・コントロール</summary>
        protected class MultiSortData
        {
            /// <summary>Initializes a new instance of the <see cref="MultiSortData"/> class.</summary>
            /// <param name="tableIndex">該当テーブルインデックスを表す数値</param>
            /// <param name="fieldIndex">該当フィールドインデックスを表す数値</param>
            /// <param name="data">該当フィールドデータを表すオブジェクト</param>
            /// <param name="rowIndex">該当Rowインデックスを表す数値</param>
            public MultiSortData(int tableIndex, int fieldIndex, object data, int rowIndex)
            {
                // 該当テーブルインデックス
                this.TableIndex = tableIndex;

                // 該当フィールドインデックス
                this.FieldIndex = fieldIndex;

                // 該当フィールドデータ
                this.Data = data;

                // 該当Rowインデックスを保持するプロパティ</summary>
                this.RowIndex = rowIndex;
            }

            /// <summary>該当テーブルインデックスを保持するプロパティ</summary>
            public int TableIndex { get; private set; }

            /// <summary>該当フィールドインデックスを保持するプロパティ</summary>
            public int FieldIndex { get; private set; }

            /// <summary>該当フィールドデータを保持するプロパティ</summary>
            public object Data { get; private set; }

            /// <summary>該当Rowインデックスを保持するプロパティ</summary>
            public int RowIndex { get; private set; }
        }

        #endregion - Inner Classes -
    }

    #endregion - CommonChouhyouBase -

    #endregion - Classes -
}
