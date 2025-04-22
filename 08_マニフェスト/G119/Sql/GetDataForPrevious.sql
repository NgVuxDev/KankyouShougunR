--マニフェスト
         SELECT TME.SYSTEM_ID,
                TME.SEQ,
                TME.HAIKI_KBN_CD,
                TME.FIRST_MANIFEST_KBN,
                TME.KYOTEN_CD,
                MK.KYOTEN_NAME_RYAKU AS KYOTEN_NAME,
                TME.TORIHIKISAKI_CD,
                MT.TORIHIKISAKI_NAME_RYAKU AS TORIHIKISAKI_NAME,
                TME.JIZEN_NUMBER,
                TME.JIZEN_DATE,
                TME.KOUFU_DATE,
                TME.KOUFU_KBN,
                TME.MANIFEST_ID,
                TME.SEIRI_ID,
                TME.KOUFU_TANTOUSHA,
                TME.KOUFU_TANTOUSHA_SHOZOKU,
                TME.HST_GYOUSHA_CD,
                TME.HST_GYOUSHA_NAME,
                TME.HST_GYOUSHA_POST,
                TME.HST_GYOUSHA_TEL,
                TME.HST_GYOUSHA_ADDRESS,
                TME.HST_GENBA_CD,
                TME.HST_GENBA_NAME,
                TME.HST_GENBA_POST,
                TME.HST_GENBA_TEL,
                TME.HST_GENBA_ADDRESS,
                TME.BIKOU,
                TME.MANIFEST_MERCURY_CHECK,
                TME.MERCURY_USED_SEIHIN_HAIKIBUTU_CHECK,
                TME.MERCURY_BAIJINNADO_HAIKIBUTU_CHECK,
                TME.ISIWAKANADO_HAIKIBUTU_CHECK,
                TME.TOKUTEI_SANGYOU_HAIKIBUTU_CHECK,
                TME.MANIFEST_KAMI_CHECK,
                TME.KONGOU_SHURUI_CD,
                MKS.KONGOU_SHURUI_NAME_RYAKU AS KONGOU_SHURUI_NAME,
                TME.HAIKI_SUU,
                TME.HAIKI_UNIT_CD,
                MU_TME.UNIT_NAME AS HAIKI_UNIT_NAME,
                TME.TOTAL_SUU,
                TME.TOTAL_KANSAN_SUU,
                TME.TOTAL_GENNYOU_SUU,
                TME.CHUUKAN_HAIKI_KBN,
                TME.CHUUKAN_HAIKI,
                TME.LAST_SBN_YOTEI_KBN,
                TME.LAST_SBN_YOTEI_GYOUSHA_CD,
                TME.LAST_SBN_YOTEI_GENBA_CD,
                TME.LAST_SBN_YOTEI_GENBA_NAME,
                TME.LAST_SBN_YOTEI_GENBA_POST,
                TME.LAST_SBN_YOTEI_GENBA_TEL,
                TME.LAST_SBN_YOTEI_GENBA_ADDRESS,
                TME.SBN_GYOUSHA_CD,
                TME.SBN_GYOUSHA_NAME,
                TME.SBN_GYOUSHA_POST,
                TME.SBN_GYOUSHA_TEL,
                TME.SBN_GYOUSHA_ADDRESS,
                TME.TMH_GYOUSHA_CD,
                TME.TMH_GYOUSHA_NAME,
                TME.TMH_GENBA_CD,
                TME.TMH_GENBA_NAME,
                TME.TMH_GENBA_POST,
                TME.TMH_GENBA_TEL,
                TME.TMH_GENBA_ADDRESS,
                TME.YUUKA_KBN,
                TME.YUUKA_UNIT_CD,
                TME.YUUKA_SUU,
                TME.SBN_JYURYOUSHA_CD,
                TME.SBN_JYURYOUSHA_NAME,
                TME.SBN_JYURYOU_TANTOU_CD,
                TME.SBN_JYURYOU_TANTOU_NAME,
                TME.SBN_JYURYOU_DATE,
                TME.SBN_JYUTAKUSHA_CD,
                TME.SBN_JYUTAKUSHA_NAME,
                TME.SBN_TANTOU_CD,
                TME.SBN_TANTOU_NAME,
                TME.LAST_SBN_GYOUSHA_CD,
                TME.LAST_SBN_GENBA_CD,
                TME.LAST_SBN_GENBA_NAME,
                TME.LAST_SBN_GENBA_POST,
                TME.LAST_SBN_GENBA_TEL,
                TME.LAST_SBN_GENBA_ADDRESS,
                TME.LAST_SBN_GENBA_NUMBER,
                TME.LAST_SBN_CHECK_NAME,
                TME.CHECK_B1,
                TME.CHECK_B2,
                TME.CHECK_B4,
                TME.CHECK_B6,
                TME.CHECK_D,
                TME.CHECK_E,
                TME.RENKEI_DENSHU_KBN_CD,
                TME.RENKEI_SYSTEM_ID,
                TME.RENKEI_MEISAI_SYSTEM_ID,
                TME.CREATE_USER,
                TME.CREATE_DATE,
                TME.UPDATE_USER,
                TME.UPDATE_DATE,
                CAST(TME.TIME_STAMP AS int) AS TME_TIME_STAMP,

--マニフェスト運搬
                TMU.UPN_ROUTE_NO,
                TMU.UPN_GYOUSHA_CD,
                TMU.UPN_GYOUSHA_NAME,
                TMU.UPN_GYOUSHA_POST,
                TMU.UPN_GYOUSHA_TEL,
                TMU.UPN_GYOUSHA_ADDRESS,
                TMU.UPN_HOUHOU_CD,
                MUH.UNPAN_HOUHOU_NAME_RYAKU AS UNPAN_HOUHOU_NAME,
                TMU.SHASHU_CD,
                MSS.SHASHU_NAME_RYAKU AS SHASHU_NAME,
                TMU.SHASHU_NAME AS INPUT_SHASHU_NAME,
                TMU.SHARYOU_CD,
                MSR.SHARYOU_NAME_RYAKU AS SHARYOU_NAME,
                TMU.SHARYOU_NAME AS INPUT_SHARYOU_NAME,
                TMU.TMH_KBN,
                TMU.UPN_SAKI_KBN,
                TMU.UPN_SAKI_GYOUSHA_CD,
                TMU.UPN_SAKI_GENBA_CD,
                TMU.UPN_SAKI_GENBA_NAME,
                TMU.UPN_SAKI_GENBA_POST,
                TMU.UPN_SAKI_GENBA_TEL,
                TMU.UPN_SAKI_GENBA_ADDRESS,
                TMU.UPN_JYUTAKUSHA_CD,
                TMU.UPN_JYUTAKUSHA_NAME,
                TMU.UNTENSHA_CD,
                TMU.UNTENSHA_NAME,
                TMU.UPN_END_DATE,
                TMU.YUUKA_SUU AS TMU_YUUKA_SUU,
                TMU.YUUKA_UNIT_CD AS TMU_YUUKA_UNIT_CD,
                MU_TMU.UNIT_NAME AS TMU_YUUKA_UNIT_NAME,
                CAST(TMU.TIME_STAMP AS int) AS TMU_TIME_STAMP,

--マニフェスト印字
                TMP.PRT_FUTSUU_HAIKIBUTSU,
                TMP.PRT_TOKUBETSU_HAIKIBUTSU,
                TMP.PRT_SUU,
                TMP.PRT_UNIT_CD,
                MU_TMP.UNIT_NAME AS PRT_UNIT_NAME,
                TMP.PRT_NISUGATA_CD,
                TMP.PRT_NISUGATA_NAME,
                TMP.PRT_HAIKI_NAME_CD,
                TMP.PRT_HAIKI_NAME,
                TMP.PRT_YUUGAI_CD,
                TMP.PRT_YUUGAI_NAME,
                TMP.PRT_SBN_HOUHOU_CD,
                TMP.PRT_SBN_HOUHOU_NAME,
                TMP.SLASH_YUUGAI_FLG,
                TMP.SLASH_BIKOU_FLG,
                TMP.SLASH_CHUUKAN_FLG,
                TMP.SLASH_TSUMIHO_FLG,
                TMP.SLASH_JIZENKYOUGI_FLG,
                TMP.SLASH_UPN_GYOUSHA2_FLG,
                TMP.SLASH_UPN_GYOUSHA3_FLG,
                TMP.SLASH_UPN_JYUTAKUSHA2_FLG,
                TMP.SLASH_UPN_JYUTAKUSHA3_FLG,
                TMP.SLASH_UPN_SAKI_GENBA2_FLG,
                TMP.SLASH_UPN_SAKI_GENBA3_FLG,
                TMP.SLASH_B1_FLG,
                TMP.SLASH_B2_FLG,
                TMP.SLASH_B4_FLG,
                TMP.SLASH_B6_FLG,
                TMP.SLASH_D_FLG,
                TMP.SLASH_E_FLG,
                CAST(TMP.TIME_STAMP AS int) AS TMP_TIME_STAMP,

--マニフェスト返却日
                TMRD.SEQ AS TMRD_SEQ,
                TMRD.SEND_A,
                TMRD.SEND_B1,
                TMRD.SEND_B2,
                TMRD.SEND_B4,
                TMRD.SEND_B6,
                TMRD.SEND_C1,
                TMRD.SEND_C2,
                TMRD.SEND_D,
                TMRD.SEND_E,
                CAST(TMRD.TIME_STAMP AS int) AS TMRD_TIME_STAMP

--マニフェスト
           FROM (SELECT TOP 1 *
                   FROM (SELECT *,
                                0 AS KBN
                           FROM T_MANIFEST_ENTRY WITH(NOLOCK)
                          WHERE DELETE_FLG   = 'false'
                            AND HAIKI_KBN_CD = /*data.HAIKI_KBN_CD*/'2'
                            /*IF data.SYSTEM_ID != null && data.SYSTEM_ID != ''*/
                            AND SYSTEM_ID    < /*data.SYSTEM_ID*/'0'
                            /*END*/
                          UNION ALL
                         SELECT *,
                                1 AS KBN
                           FROM T_MANIFEST_ENTRY WITH(NOLOCK)
                          WHERE DELETE_FLG   = 'false'
                            AND HAIKI_KBN_CD = /*data.HAIKI_KBN_CD*/'2'
                                ) AS TEMP
                  WHERE DELETE_FLG     = 'false'
                    /*IF data.KYOTEN != null && data.KYOTEN != ''*/
                    AND TEMP.KYOTEN_CD = /*data.KYOTEN*/'0'
                    /*END*/
               ORDER BY KBN,
                        SYSTEM_ID DESC,
                        SEQ DESC
                ) AS TME
LEFT OUTER JOIN M_KYOTEN AS MK WITH(NOLOCK)
             ON TME.KYOTEN_CD = MK.KYOTEN_CD
            AND MK.DELETE_FLG = 'false'
LEFT OUTER JOIN M_TORIHIKISAKI AS MT WITH(NOLOCK)
             ON TME.TORIHIKISAKI_CD = MT.TORIHIKISAKI_CD
            AND MT.DELETE_FLG = 'false'
LEFT OUTER JOIN M_KONGOU_SHURUI AS MKS WITH(NOLOCK)
             ON TME.HAIKI_KBN_CD = MKS.HAIKI_KBN_CD
            AND TME.KONGOU_SHURUI_CD = MKS.KONGOU_SHURUI_CD
            AND MKS.DELETE_FLG = 'false'
LEFT OUTER JOIN M_UNIT AS MU_TME WITH(NOLOCK)
             ON TME.HAIKI_UNIT_CD = MU_TME.UNIT_CD
            AND MU_TME.DELETE_FLG = 'false'

--マニフェスト運搬
LEFT OUTER JOIN T_MANIFEST_UPN AS TMU WITH(NOLOCK)
             ON TME.SYSTEM_ID = TMU.SYSTEM_ID 
            AND TME.SEQ = TMU.SEQ
LEFT OUTER JOIN M_UNPAN_HOUHOU AS MUH WITH(NOLOCK)
             ON TMU.UPN_HOUHOU_CD = MUH.UNPAN_HOUHOU_CD
            AND MUH.DELETE_FLG = 'false'
LEFT OUTER JOIN M_SHASHU AS MSS WITH(NOLOCK)
             ON TMU.SHASHU_CD = MSS.SHASHU_CD
            AND MSS.DELETE_FLG = 'false'
LEFT OUTER JOIN M_SHARYOU AS MSR WITH(NOLOCK)
             ON TMU.UPN_GYOUSHA_CD = MSR.GYOUSHA_CD
            AND TMU.SHARYOU_CD = MSR.SHARYOU_CD
            AND MSR.DELETE_FLG = 'false'
LEFT OUTER JOIN M_UNIT AS MU_TMU WITH(NOLOCK)
             ON TMU.YUUKA_UNIT_CD = MU_TMU.UNIT_CD
            AND MU_TMU.DELETE_FLG = 'false'

--マニフェスト印字
LEFT OUTER JOIN T_MANIFEST_PRT AS TMP WITH(NOLOCK)
             ON TME.SYSTEM_ID = TMP.SYSTEM_ID
            AND TME.SEQ = TMP.SEQ
LEFT OUTER JOIN M_UNIT AS MU_TMP WITH(NOLOCK)
             ON TMP.PRT_UNIT_CD = MU_TMP.UNIT_CD
            AND MU_TMP.DELETE_FLG = 'false'

--マニフェスト返却日
LEFT OUTER JOIN T_MANIFEST_RET_DATE AS TMRD WITH(NOLOCK)
             ON TME.SYSTEM_ID = TMRD.SYSTEM_ID
            AND TMRD.DELETE_FLG = 'false'