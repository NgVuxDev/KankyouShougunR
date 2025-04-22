SELECT
  TSD.SEISAN_NUMBER                                               --¸ŽZ”Ô†
  , TSDKE.KAGAMI_NUMBER                                           --ŠÓ”Ô†
  , TSDKE.ROW_NUMBER                                              --s”Ô†
  , TSDKE.DENPYOU_SHURUI_CD                                       --“`•[Ží—ÞCD
  , TSDKE.DENPYOU_SYSTEM_ID                                       --“`•[ƒVƒXƒeƒ€ID
  , TSDKE.DENPYOU_SEQ                                             --“`•[Ž}”Ô
  , TSDKE.DETAIL_SYSTEM_ID                                        --–¾×ƒVƒXƒeƒ€ID
  , TSDKE.DENPYOU_NUMBER                                          --“`•[”Ô†
  , TSDKE.DENPYOU_DATE                                            --“`•[“ú•t
  , TSDKE.TSDE_TORIHIKISAKI_CD                                    --ŽæˆøæCD
  , TSDKE.TSDE_GYOUSHA_CD                                         --‹ÆŽÒCD
  , TSDKE.GYOUSHA_NAME1                                           --‹ÆŽÒ–¼1
  , TSDKE.GYOUSHA_NAME2                                           --‹ÆŽÒ–¼2
  , TSDKE.TSDE_GENBA_CD                                           --Œ»êCD
  , TSDKE.GENBA_NAME1                                             --Œ»ê–¼1
  , TSDKE.GENBA_NAME2                                             --Œ»ê–¼2
  , TSDKE.HINMEI_CD                                               --•i–¼CD
  , TSDKE.HINMEI_NAME                                             --•i–¼
  , TSDKE.SUURYOU                                                 --”—Ê
  , TSDKE.UNIT_CD                                                 --’PˆÊCD
  , TSDKE.UNIT_NAME                                               --’PˆÊ–¼
  , TSDKE.TANKA						                              --’P‰¿
  , ISNULL(TSDKE.KINGAKU,0) AS KINGAKU                            --‹àŠz
  , ISNULL(TSDKE.UCHIZEI_GAKU,0) AS UCHIZEI_GAKU                  --“àÅŠz
  , ISNULL(TSDKE.SOTOZEI_GAKU,0) AS SOTOZEI_GAKU                  --ŠOÅŠz
  , ISNULL(TSDKE.DENPYOU_UCHIZEI_GAKU,0) AS DENPYOU_UCHIZEI_GAKU  --“`•[“àÅŠz
  , ISNULL(TSDKE.DENPYOU_SOTOZEI_GAKU,0) AS DENPYOU_SOTOZEI_GAKU  --“`•[ŠOÅŠz
  , TSDKE.DENPYOU_ZEI_KBN_CD                                      --“`•[Å‹æ•ªCD
  , TSDKE.MEISAI_ZEI_KBN_CD                                       --–¾×Å‹æ•ªCD
  , TSDKE.MEISAI_BIKOU                                            --–¾×”õl
  , TSDKE.DENPYOU_ZEI_KEISAN_KBN_CD                               --“`•[ÅŒvŽZ‹æ•ª
  , TSDKE.TSDK_TORIHIKISAKI_CD                                    --ŽæˆøæCD
  , TSDKE.TSDK_GYOUSHA_CD                                         --‹ÆŽÒCD
  , TSDKE.TSDK_GENBA_CD                                           --Œ»êCD
  , TSDKE.DAIHYOU_PRINT_KBN                                       --‘ã•\ŽÒˆóŽš‹æ•ª
  , TSDKE.CORP_NAME                                               --‰ïŽÐ–¼
  , TSDKE.CORP_DAIHYOU                                            --‘ã•\ŽÒ–¼
  , TSDKE.KYOTEN_NAME_PRINT_KBN                                   --‹’“_–¼ˆóŽš‹æ•ª
  , TSDKE.TSDK_KYOTEN_CD                                          --‹’“_CD
  , TSDKE.KYOTEN_NAME                                             --‹’“_–¼
  , TSDKE.KYOTEN_DAIHYOU                                          --‹’“_‘ã•\ŽÒ–¼
  , TSDKE.KYOTEN_POST                                             --‹’“_—X•Ö”Ô†
  , TSDKE.KYOTEN_ADDRESS1                                         --‹’“_ZŠ1
  , TSDKE.KYOTEN_ADDRESS2                                         --‹’“_ZŠ2
  , TSDKE.KYOTEN_TEL                                              --‹’“_TEL
  , TSDKE.KYOTEN_FAX                                              --‹’“_FAX
  , TSDKE.SHIHARAI_SOUFU_NAME1                                    --Žx•¥–¾×‘‘—•tæ1
  , TSDKE.SHIHARAI_SOUFU_NAME2                                    --Žx•¥–¾×‘‘—•tæ2
  , TSDKE.SHIHARAI_SOUFU_KEISHOU1                                 --Žx•¥–¾×‘‘—•tæŒhÌ1
  , TSDKE.SHIHARAI_SOUFU_KEISHOU2                                 --Žx•¥–¾×‘‘—•tæŒhÌ2
  , TSDKE.SHIHARAI_SOUFU_POST                                     --Žx•¥–¾×‘‘—•tæ—X•Ö”Ô†
  , TSDKE.SHIHARAI_SOUFU_ADDRESS1                                 --Žx•¥–¾×‘‘—•tæZŠ1
  , TSDKE.SHIHARAI_SOUFU_ADDRESS2                                 --Žx•¥–¾×‘‘—•tæZŠ2
  , TSDKE.SHIHARAI_SOUFU_BUSHO                                    --Žx•¥–¾×‘‘—•tæ•”
  , TSDKE.SHIHARAI_SOUFU_TANTOU                                   --Žx•¥–¾×‘‘—•tæ’S“–ŽÒ
  , TSDKE.SHIHARAI_SOUFU_TEL                                      --Žx•¥–¾×‘‘—•tæTEL
  , TSDKE.SHIHARAI_SOUFU_FAX                                      --Žx•¥–¾×‘‘—•tæFAX
  , TSDKE.BIKOU_1												  --”õl1
  , TSDKE.BIKOU_2												  --”õl2
  , ISNULL(TSDKE.KONKAI_SHIHARAI_GAKU,0) AS TSDK_KONKAI_SHIHARAI_GAKU        --¡‰ñŽx•¥Šz
  , ISNULL(TSDKE.KONKAI_SEI_UTIZEI_GAKU,0) AS TSDK_KONKAI_SEI_UTIZEI_GAKU    --¡‰ñ¿“àÅŠz
  , ISNULL(TSDKE.KONKAI_SEI_SOTOZEI_GAKU,0) AS TSDK_KONKAI_SEI_SOTOZEI_GAKU  --¡‰ñ¿ŠOÅŠz
  , ISNULL(TSDKE.KONKAI_DEN_UTIZEI_GAKU,0) AS TSDK_KONKAI_DEN_UTIZEI_GAKU    --¡‰ñ“`“àÅŠz
  , ISNULL(TSDKE.KONKAI_DEN_SOTOZEI_GAKU,0) AS TSDK_KONKAI_DEN_SOTOZEI_GAKU  --¡‰ñ“`ŠOÅŠz
  , ISNULL(TSDKE.KONKAI_MEI_UTIZEI_GAKU,0) AS TSDK_KONKAI_MEI_UTIZEI_GAKU    --¡‰ñ–¾“àÅŠz
  , ISNULL(TSDKE.KONKAI_MEI_SOTOZEI_GAKU,0) AS TSDK_KONKAI_MEI_SOTOZEI_GAKU  --¡‰ñ–¾ŠOÅŠz
  , TSD.KYOTEN_CD AS TSD_KYOTEN_CD								--‹’“_CD
  , TSD.SHIMEBI													--’÷“ú
  , TSD.TORIHIKISAKI_CD AS TSD_TORIHIKISAKI_CD					--ŽæˆøæCD
  , TSD.SHOSHIKI_KBN											--‘Ž®‹æ•ª
  , TSD.SHOSHIKI_MEISAI_KBN										--‘Ž®–¾×‹æ•ª
  , TSD.SHIHARAI_KEITAI_KBN										--Žx•¥Œ`‘Ô‹æ•ª
  , TSD.SHUKKIN_MEISAI_KBN										--“ü‹à–¾×‹æ•ª
  , TSD.YOUSHI_KBN												--—pŽ†‹æ•ª
  , TSD.SEISAN_DATE												--¸ŽZ“ú•t
  , TSD.SHUKKIN_YOTEI_BI										--o‹à—\’è“ú
  , ISNULL(TSD.ZENKAI_KURIKOSI_GAKU,0) AS ZENKAI_KURIKOSI_GAKU  --‘O‰ñŒJ‰zŠz
  , ISNULL(TSD.KONKAI_SHUKKIN_GAKU,0) AS KONKAI_SHUKKIN_GAKU    --¡‰ño‹àŠz
  , ISNULL(TSD.KONKAI_CHOUSEI_GAKU,0) AS KONKAI_CHOUSEI_GAKU    --¡‰ñ’²®Šz
  , ISNULL(TSD.KONKAI_SHIHARAI_GAKU,0) AS TSD_KONKAI_SHIHARAI_GAKU          --¡‰ñŽx•¥Šz
  , ISNULL(TSD.KONKAI_SEI_UTIZEI_GAKU,0) AS TSD_KONKAI_SEI_UTIZEI_GAKU      --¡‰ñ¿“àÅŠz
  , ISNULL(TSD.KONKAI_SEI_SOTOZEI_GAKU,0) AS TSD_KONKAI_SEI_SOTOZEI_GAKU    --¡‰ñ¿ŠOÅŠz
  , ISNULL(TSD.KONKAI_DEN_UTIZEI_GAKU,0) AS TSD_KONKAI_DEN_UTIZEI_GAKU      --¡‰ñ“`“àÅŠz
  , ISNULL(TSD.KONKAI_DEN_SOTOZEI_GAKU,0) AS TSD_KONKAI_DEN_SOTOZEI_GAKU    --¡‰ñ“`ŠOÅŠz
  , ISNULL(TSD.KONKAI_MEI_UTIZEI_GAKU,0) AS TSD_KONKAI_MEI_UTIZEI_GAKU      --¡‰ñ–¾“àÅŠz
  , ISNULL(TSD.KONKAI_MEI_SOTOZEI_GAKU,0) AS TSD_KONKAI_MEI_SOTOZEI_GAKU    --¡‰ñ–¾ŠOÅŠz
  , ISNULL(TSD.KONKAI_SEISAN_GAKU,0) AS KONKAI_SEISAN_GAKU                  --¡‰ñŒä¸ŽZŠz
  , TSD.HAKKOU_KBN                                              --”­s‹æ•ª
  , TSD.SHIME_JIKKOU_NO                                         --’÷ŽÀs”Ô†
  , (ISNULL(TSD.ZENKAI_KURIKOSI_GAKU,0) - ISNULL(TSD.KONKAI_SHUKKIN_GAKU,0) - ISNULL(TSD.KONKAI_CHOUSEI_GAKU,0)) AS SASIHIKIGAKU --·ˆøŒJ‰zŠz
  , (ISNULL(TSDKE.KONKAI_SEI_UTIZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_SEI_SOTOZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_DEN_UTIZEI_GAKU,0) 
		+ ISNULL(TSDKE.KONKAI_DEN_SOTOZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_MEI_UTIZEI_GAKU,0) + ISNULL(TSDKE.KONKAI_MEI_SOTOZEI_GAKU,0)) AS SYOUHIZEIGAKU --Á”ïÅŠz
  , (ISNULL(TSDKE.UCHIZEI_GAKU,0) + ISNULL(TSDKE.SOTOZEI_GAKU,0)) AS MEISEI_SYOHIZEI
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD,TSDKE.DENPYOU_DATE,TSDKE.DENPYOU_SHURUI_CD,TSDKE.DENPYOU_NUMBER) AS RANK_DENPYO_1 --“`•[ƒ‰ƒ“ƒN
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD,TSDKE.DENPYOU_DATE,TSDKE.DENPYOU_SHURUI_CD,TSDKE.DENPYOU_NUMBER) AS DENPYO_KINGAKU_1 --“`•[‹àŠz‡Œv
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS RANK_GENBA_1 --Œ»êƒ‰ƒ“ƒN
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS GENBA_UCHIZEI --Œ»ê“àÅÁ”ïÅ‡Œv
  , SUM(ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS GENBA_SOTOZEI --Œ»êŠOÅÁ”ïÅ‡Œv
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD,TSDKE.TSDE_GENBA_CD) AS GENBA_KINGAKU_1 --Œ»ê‹àŠz‡Œv
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS RANK_GYOUSHA_1 --‹ÆŽÒƒ‰ƒ“ƒN
  , SUM(ISNULL(TSDKE.UCHIZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS GYOUSHA_UCHIZEI --‹ÆŽÒ“àÅÁ”ïÅ‡Œv
  , SUM(ISNULL(TSDKE.SOTOZEI_GAKU,0)) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS GYOUSHA_SOTOZEI --‹ÆŽÒŠOÅÁ”ïÅ‡Œv
  , SUM(TSDKE.KINGAKU) OVER (PARTITION BY TSDKE.KAGAMI_NUMBER,TSDKE.TSDE_GYOUSHA_CD) AS GYOUSHA_KINGAKU_1 --‹ÆŽÒ‹àŠz‡Œv
  , RANK() OVER (ORDER BY TSDKE.KAGAMI_NUMBER) AS RANK_SEISAN_1 --¸ŽZƒ‰ƒ“ƒN
  , TSD.TOUROKU_NO
  , TSD.INVOICE_KBN
  , TSDKE.KONKAI_KAZEI_KBN_1     --¡‰ñ‰ÛÅ‹æ•ª‚P
  , TSDKE.KONKAI_KAZEI_RATE_1    --¡‰ñ‰ÛÅÅ—¦‚P
  , TSDKE.KONKAI_KAZEI_GAKU_1    --¡‰ñ‰ÛÅÅ”²‹àŠz‚P
  , TSDKE.KONKAI_KAZEI_ZEIGAKU_1 --¡‰ñ‰ÛÅÅŠz‚P
  , TSDKE.KONKAI_KAZEI_KBN_2     --¡‰ñ‰ÛÅ‹æ•ª‚Q
  , TSDKE.KONKAI_KAZEI_RATE_2    --¡‰ñ‰ÛÅÅ—¦‚Q
  , TSDKE.KONKAI_KAZEI_GAKU_2    --¡‰ñ‰ÛÅÅ”²‹àŠz‚Q
  , TSDKE.KONKAI_KAZEI_ZEIGAKU_2 --¡‰ñ‰ÛÅÅŠz‚Q
  , TSDKE.KONKAI_KAZEI_KBN_3     --¡‰ñ‰ÛÅ‹æ•ª‚R
  , TSDKE.KONKAI_KAZEI_RATE_3    --¡‰ñ‰ÛÅÅ—¦‚R
  , TSDKE.KONKAI_KAZEI_GAKU_3    --¡‰ñ‰ÛÅÅ”²‹àŠz‚R
  , TSDKE.KONKAI_KAZEI_ZEIGAKU_3 --¡‰ñ‰ÛÅÅŠz‚R
  , TSDKE.KONKAI_KAZEI_KBN_4     --¡‰ñ‰ÛÅ‹æ•ª‚S
  , TSDKE.KONKAI_KAZEI_RATE_4    --¡‰ñ‰ÛÅÅ—¦‚S
  , TSDKE.KONKAI_KAZEI_GAKU_4    --¡‰ñ‰ÛÅÅ”²‹àŠz‚S
  , TSDKE.KONKAI_KAZEI_ZEIGAKU_4 --¡‰ñ‰ÛÅÅŠz‚S
  , TSDKE.KONKAI_HIKAZEI_KBN     --¡‰ñ”ñ‰ÛÅ‹æ•ª
  , TSDKE.KONKAI_HIKAZEI_GAKU    --¡‰ñ”ñ‰ÛÅŠz
FROM
  T_SEISAN_DENPYOU TSD 
  INNER JOIN (
	SELECT
		TSDK.SEISAN_NUMBER                                              --ŠÓ”Ô†
		, TSDK.KAGAMI_NUMBER                                            --ŠÓ”Ô†
		, TSDE.ROW_NUMBER                                               --s”Ô†
		, TSDE.DENPYOU_SHURUI_CD                                        --“`•[Ží—ÞCD
		, TSDE.DENPYOU_SYSTEM_ID                                        --“`•[ƒVƒXƒeƒ€ID
		, TSDE.DENPYOU_SEQ                                              --“`•[Ž}”Ô
		, TSDE.DETAIL_SYSTEM_ID                                         --–¾×ƒVƒXƒeƒ€ID
		, TSDE.DENPYOU_NUMBER                                           --“`•[”Ô†
		, TSDE.DENPYOU_DATE                                             --“`•[“ú•t
		, TSDE.TORIHIKISAKI_CD AS TSDE_TORIHIKISAKI_CD                  --ŽæˆøæCD
		, TSDE.GYOUSHA_CD AS TSDE_GYOUSHA_CD                            --‹ÆŽÒCD
		, TSDE.GYOUSHA_NAME1                                            --‹ÆŽÒ–¼1
		, TSDE.GYOUSHA_NAME2                                            --‹ÆŽÒ–¼2
		, TSDE.GENBA_CD AS TSDE_GENBA_CD                                --Œ»êCD
		, TSDE.GENBA_NAME1                                              --Œ»ê–¼1
		, TSDE.GENBA_NAME2                                              --Œ»ê–¼2
		, TSDE.HINMEI_CD                                                --•i–¼CD
		, TSDE.HINMEI_NAME                                              --•i–¼
		, TSDE.SUURYOU                                                  --”—Ê
		, TSDE.UNIT_CD                                                  --’PˆÊCD
		, TSDE.UNIT_NAME                                                --’PˆÊ–¼
		, TSDE.TANKA					                                --’P‰¿
		, TSDE.KINGAKU                                                  --‹àŠz
		, ISNULL(TSDE.UCHIZEI_GAKU,0) AS UCHIZEI_GAKU                   --“àÅŠz
		, ISNULL(TSDE.SOTOZEI_GAKU,0) AS SOTOZEI_GAKU                   --ŠOÅŠz
		, ISNULL(TSDE.DENPYOU_UCHIZEI_GAKU,0) AS DENPYOU_UCHIZEI_GAKU   --“`•[“àÅŠz
		, ISNULL(TSDE.DENPYOU_SOTOZEI_GAKU,0) AS DENPYOU_SOTOZEI_GAKU   --“`•[ŠOÅŠz
		, TSDE.DENPYOU_ZEI_KBN_CD                                       --“`•[Å‹æ•ªCD
		, TSDE.MEISAI_ZEI_KBN_CD                                        --–¾×Å‹æ•ªCD
		, TSDE.MEISAI_BIKOU                                             --–¾×”õl
		, TSDE.DENPYOU_ZEI_KEISAN_KBN_CD                                --“`•[ÅŒvŽZ‹æ•ª
		, TSDK.TORIHIKISAKI_CD AS TSDK_TORIHIKISAKI_CD                  --ŽæˆøæCD
		, TSDK.GYOUSHA_CD AS TSDK_GYOUSHA_CD                            --‹ÆŽÒCD
		, TSDK.GENBA_CD AS TSDK_GENBA_CD                                --Œ»êCD
		, TSDK.DAIHYOU_PRINT_KBN                                        --‘ã•\ŽÒˆóŽš‹æ•ª
		, TSDK.CORP_NAME                                                --‰ïŽÐ–¼
		, TSDK.CORP_DAIHYOU                                             --‘ã•\ŽÒ–¼
		, TSDK.KYOTEN_NAME_PRINT_KBN                                    --‹’“_–¼ˆóŽš‹æ•ª
		, TSDK.KYOTEN_CD AS TSDK_KYOTEN_CD                              --‹’“_CD
		, TSDK.KYOTEN_NAME                                              --‹’“_–¼
		, TSDK.KYOTEN_DAIHYOU                                           --‹’“_‘ã•\ŽÒ–¼
		, TSDK.KYOTEN_POST                                              --‹’“_—X•Ö”Ô†
		, TSDK.KYOTEN_ADDRESS1                                          --‹’“_ZŠ1
		, TSDK.KYOTEN_ADDRESS2                                          --‹’“_ZŠ2
		, TSDK.KYOTEN_TEL                                               --‹’“_TEL
		, TSDK.KYOTEN_FAX                                               --‹’“_FAX
		, TSDK.SHIHARAI_SOUFU_NAME1                                     --Žx•¥–¾×‘‘—•tæ1
		, TSDK.SHIHARAI_SOUFU_NAME2                                     --Žx•¥–¾×‘‘—•tæ2
		, TSDK.SHIHARAI_SOUFU_KEISHOU1                                  --Žx•¥–¾×‘‘—•tæŒhÌ1
		, TSDK.SHIHARAI_SOUFU_KEISHOU2                                  --Žx•¥–¾×‘‘—•tæŒhÌ2
		, TSDK.SHIHARAI_SOUFU_POST                                      --Žx•¥–¾×‘‘—•tæ—X•Ö”Ô†
		, TSDK.SHIHARAI_SOUFU_ADDRESS1                                  --Žx•¥–¾×‘‘—•tæZŠ1
		, TSDK.SHIHARAI_SOUFU_ADDRESS2                                  --Žx•¥–¾×‘‘—•tæZŠ2
		, TSDK.SHIHARAI_SOUFU_BUSHO                                     --Žx•¥–¾×‘‘—•tæ•”
		, TSDK.SHIHARAI_SOUFU_TANTOU                                    --Žx•¥–¾×‘‘—•tæ’S“–ŽÒ
		, TSDK.SHIHARAI_SOUFU_TEL                                       --Žx•¥–¾×‘‘—•tæTEL
		, TSDK.SHIHARAI_SOUFU_FAX                                       --Žx•¥–¾×‘‘—•tæFAX
		, ISNULL(TSDK.KONKAI_SHIHARAI_GAKU,0) AS KONKAI_SHIHARAI_GAKU        --¡‰ñŽx•¥Šz
		, ISNULL(TSDK.KONKAI_SEI_UTIZEI_GAKU,0) AS KONKAI_SEI_UTIZEI_GAKU    --¡‰ñ¿“àÅŠz
		, ISNULL(TSDK.KONKAI_SEI_SOTOZEI_GAKU,0) AS KONKAI_SEI_SOTOZEI_GAKU  --¡‰ñ¿ŠOÅŠz
		, ISNULL(TSDK.KONKAI_DEN_UTIZEI_GAKU,0) AS KONKAI_DEN_UTIZEI_GAKU    --¡‰ñ“`“àÅŠz
		, ISNULL(TSDK.KONKAI_DEN_SOTOZEI_GAKU,0) AS KONKAI_DEN_SOTOZEI_GAKU  --¡‰ñ“`ŠOÅŠz
		, ISNULL(TSDK.KONKAI_MEI_UTIZEI_GAKU,0) AS KONKAI_MEI_UTIZEI_GAKU    --¡‰ñ–¾“àÅŠz
		, ISNULL(TSDK.KONKAI_MEI_SOTOZEI_GAKU,0) AS KONKAI_MEI_SOTOZEI_GAKU  --¡‰ñ–¾ŠOÅŠz
		, TSDK.BIKOU_1														 --”õl1
		, TSDK.BIKOU_2														 --”õl2
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_1,0) AS KONKAI_KAZEI_KBN_1            --¡‰ñ‰ÛÅ‹æ•ª‚P
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_1,0) AS KONKAI_KAZEI_RATE_1			 --¡‰ñ‰ÛÅÅ—¦‚P
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_1,0) AS KONKAI_KAZEI_GAKU_1			 --¡‰ñ‰ÛÅÅ”²‹àŠz‚P
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_1,0) AS KONKAI_KAZEI_ZEIGAKU_1	 --¡‰ñ‰ÛÅÅŠz‚P
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_2,0) AS KONKAI_KAZEI_KBN_2            --¡‰ñ‰ÛÅ‹æ•ª‚Q
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_2,0) AS KONKAI_KAZEI_RATE_2			 --¡‰ñ‰ÛÅÅ—¦‚Q
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_2,0) AS KONKAI_KAZEI_GAKU_2			 --¡‰ñ‰ÛÅÅ”²‹àŠz‚Q
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_2,0) AS KONKAI_KAZEI_ZEIGAKU_2    --¡‰ñ‰ÛÅÅŠz‚Q
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_3,0) AS KONKAI_KAZEI_KBN_3            --¡‰ñ‰ÛÅ‹æ•ª‚R
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_3,0) AS KONKAI_KAZEI_RATE_3			 --¡‰ñ‰ÛÅÅ—¦‚R
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_3,0) AS KONKAI_KAZEI_GAKU_3			 --¡‰ñ‰ÛÅÅ”²‹àŠz‚R
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_3,0) AS KONKAI_KAZEI_ZEIGAKU_3    --¡‰ñ‰ÛÅÅŠz‚R
        , ISNULL(TSDK.KONKAI_KAZEI_KBN_4,0) AS KONKAI_KAZEI_KBN_4            --¡‰ñ‰ÛÅ‹æ•ª‚S
		, ISNULL(TSDK.KONKAI_KAZEI_RATE_4,0) AS KONKAI_KAZEI_RATE_4			 --¡‰ñ‰ÛÅÅ—¦‚S
		, ISNULL(TSDK.KONKAI_KAZEI_GAKU_4,0) AS KONKAI_KAZEI_GAKU_4			 --¡‰ñ‰ÛÅÅ”²‹àŠz‚S
		, ISNULL(TSDK.KONKAI_KAZEI_ZEIGAKU_4,0) AS KONKAI_KAZEI_ZEIGAKU_4    --¡‰ñ‰ÛÅÅŠz‚S
		, ISNULL(TSDK.KONKAI_HIKAZEI_KBN,0) AS KONKAI_HIKAZEI_KBN			 --¡‰ñ”ñ‰ÛÅ‹æ•ª
		, ISNULL(TSDK.KONKAI_HIKAZEI_GAKU,0) AS KONKAI_HIKAZEI_GAKU			 --¡‰ñ”ñ‰ÛÅŠz
	FROM
		T_SEISAN_DENPYOU_KAGAMI TSDK 
		LEFT JOIN T_SEISAN_DETAIL TSDE 
        ON TSDK.SEISAN_NUMBER = TSDE.SEISAN_NUMBER AND TSDK.KAGAMI_NUMBER = TSDE.KAGAMI_NUMBER
  ) TSDKE 
  ON TSD.SEISAN_NUMBER = TSDKE.SEISAN_NUMBER
 WHERE
  TSD.DELETE_FLG = 0
  AND TSD.SEISAN_NUMBER = /*seisanNumber*/
 ORDER BY
   TSDKE.KAGAMI_NUMBER
   /*$orderBy*/
  , TSDKE.DENPYOU_DATE
  , TSDKE.DENPYOU_SHURUI_CD
  , TSDKE.DENPYOU_NUMBER
  , TSDKE.ROW_NUMBER
  