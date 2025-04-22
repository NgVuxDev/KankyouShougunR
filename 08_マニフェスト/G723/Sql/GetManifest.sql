-- カラム名(CD,NAME)の命名規則
-- CD:S_LIST_COLUMN_SELECT_DETAIL.BUTSURI_NAME と一致させること
-- NAME:S_LIST_COLUMN_SELECT_DETAIL.BUTSURI_NAME + "_NAME" の形式で一致させること

WITH COMMON_TABLE AS
(
SELECT 
ISNULL(SUM(KANSANGO_SURYO),0) AS KANSANGO_SURYO,
BASE_DATE,
/*IF dto.GroupColumn != null*//*$dto.GroupColumn*/''/*END*/
FROM (
/*IF dto.IsKamiMani*/
    SELECT
		/*IF dto.KofuDateFrom != null && dto.KofuDateFrom != ''*/SUBSTRING(CONVERT(VARCHAR(10), TME.KOUFU_DATE, 111), 1, 7) as BASE_DATE,/*END*/
		/*IF dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != ''*/SUBSTRING(CONVERT(VARCHAR(10), TMU.UPN_END_DATE, 111), 1, 7) as BASE_DATE,/*END*/
		/*IF dto.ShobunEndDateFrom != null && dto.ShobunEndDateFrom != ''*/SUBSTRING(CONVERT(VARCHAR(10), TMD.SBN_END_DATE, 111), 1, 7) as BASE_DATE,/*END*/
		/*IF dto.LastShobunEndDateFrom != null && dto.LastShobunEndDateFrom != ''*/SUBSTRING(CONVERT(VARCHAR(10), TMD.LAST_SBN_END_DATE, 111), 1, 7) as BASE_DATE,/*END*/
        1 AS DUMMY,                                 --ダミー行
        TME.KYOTEN_CD,                              --拠点
        MK.KYOTEN_NAME_RYAKU AS KYOTEN_CD_NAME,     --拠点名
        TME.HST_GYOUSHA_CD,                         --排出事業者CD
        CASE
            WHEN HST_GYOUSHA.GYOUSHA_NAME_RYAKU IS NOT NULL THEN HST_GYOUSHA.GYOUSHA_NAME_RYAKU
            ELSE TME.HST_GYOUSHA_NAME
        END HST_GYOUSHA_CD_NAME,                    --排出事業者名
        TME.HST_GENBA_CD,                           --排出事業場CD
        CASE
            WHEN HST_GENBA.GENBA_NAME_RYAKU IS NOT NULL THEN HST_GENBA.GENBA_NAME_RYAKU
            ELSE TME.HST_GENBA_NAME
        END HST_GENBA_CD_NAME,                      --排出事業場名
        /*IF dto.SelectedColumnUnpanJutakushaCd*/
        TMU.UPN_GYOUSHA_CD,                         --運搬受託者CD
        CASE
            WHEN UPN_GYOUSHA.GYOUSHA_NAME_RYAKU IS NOT NULL THEN UPN_GYOUSHA.GYOUSHA_NAME_RYAKU
            ELSE TMU.UPN_JYUTAKUSHA_NAME
        END UPN_GYOUSHA_CD_NAME,                    --運搬受託者名
        /*END*/
        TME.SBN_GYOUSHA_CD,                         --処分事業者CD
        CASE
            WHEN SBN_GYOUSHA.GYOUSHA_NAME_RYAKU IS NOT NULL THEN SBN_GYOUSHA.GYOUSHA_NAME_RYAKU
            ELSE TME.SBN_GYOUSHA_NAME
        END SBN_GYOUSHA_CD_NAME,                    --処分事業者名
        /*IF dto.SelectedColumnShobunJigyoujouCd*/
        ISNULL(SBN_JOU_INFO.UPN_SAKI_GENBA_CD, '') AS UPN_SAKI_GENBA_CD,                      --処分事業場CD
        CASE
            WHEN SBN_GENBA.GENBA_NAME_RYAKU IS NOT NULL THEN SBN_GENBA.GENBA_NAME_RYAKU
            WHEN SBN_JOU_INFO.UPN_SAKI_GENBA_NAME IS NOT NULL THEN SBN_JOU_INFO.UPN_SAKI_GENBA_NAME
            ELSE ''
        END UPN_SAKI_GENBA_CD_NAME,                 --処分事業場名
        /*END*/
        TMD.LAST_SBN_GYOUSHA_CD,                    --最終処分業者
        LAST_SBN_GYOUSHA.GYOUSHA_NAME_RYAKU AS LAST_SBN_GYOUSHA_CD_NAME,  --最終処分受託者名
        TMD.LAST_SBN_GENBA_CD,                      --最終処分事業場
        LAST_SBN_GENBA.GENBA_NAME_RYAKU AS LAST_SBN_GENBA_CD_NAME,        --最終処分場所名
        MHS.HOUKOKUSHO_BUNRUI_CD AS HOUKOKUSHO_BUNRUI_CD,                 --廃棄物種類(報告書分類)
        MHB.HOUKOKUSHO_BUNRUI_NAME_RYAKU AS HOUKOKUSHO_BUNRUI_CD_NAME,    --報告書分類名
        TMD.HAIKI_SHURUI_CD AS HAIKI_SHURUI_CD,                             -- 廃棄物種類CD
        MHS.HAIKI_KBN_CD,                                                   -- 廃棄物区分CD
        MHS.HAIKI_SHURUI_NAME_RYAKU AS HAIKI_SHURUI_CD_NAME,                -- 廃棄物種類
        ISNULL(TMD.HAIKI_NAME_CD,'') AS HAIKI_NAME_CD,                          --廃棄物名称CD
        ISNULL(MHN.HAIKI_NAME_RYAKU,'') AS HAIKI_NAME_CD_NAME, --廃棄物名称
        CONVERT(nvarchar, TMD.SBN_HOUHOU_CD) AS SBN_HOUHOU_CD,            --処分方法
        MSH.SHOBUN_HOUHOU_NAME_RYAKU AS SBN_HOUHOU_CD_NAME,               --処分方法名
        TME.TORIHIKISAKI_CD,                        --取引先
        MT.TORIHIKISAKI_NAME_RYAKU AS TORIHIKISAKI_CD_NAME,               --取引先名
        --換算後数量
        /*IF !dto.SelectedColumnUnpanJutakushaCd && dto.SelectedColumnShobunJigyoujouCd*/
        CASE WHEN DISP_MAX_ROUTE_NO.MAX_UPN_ROUTE_NO = TMU.UPN_ROUTE_NO
            THEN TMD.KANSAN_SUU
            ELSE NULL
        END AS KANSANGO_SURYO
        -- ELSE
        TMD.KANSAN_SUU AS KANSANGO_SURYO
        /*END*/
    FROM
        T_MANIFEST_ENTRY AS TME
        INNER JOIN T_MANIFEST_DETAIL AS TMD ON  TME.SYSTEM_ID = TMD.SYSTEM_ID AND TME.SEQ = TMD.SEQ
        /*IF dto.SelectedColumnUnpanJutakushaCd || dto.SelectedColumnShobunJigyoujouCd 
          || (dto.UnpanJutakushaCdFrom != null && dto.UnpanJutakushaCdFrom != '') 
          || (dto.UnpanJutakushaCdTo != null && dto.UnpanJutakushaCdTo != '') 
          || (dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != '') 
          || (dto.UnpanEndDateTo != null && dto.UnpanEndDateTo != '') 
          || (dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != '') 
          || (dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != '')*/
        LEFT JOIN (
			SELECT 
				SYSTEM_ID, 
				SEQ, 
				UPN_GYOUSHA_CD, 
				MAX(ISNULL(UPN_ROUTE_NO, 0))AS MAX_UPN_ROUTE_NO 
			FROM 
				T_MANIFEST_UPN 
            WHERE
                ISNULL(UPN_SAKI_GYOUSHA_CD, '') <> ''
                /*IF (dto.UnpanJutakushaCdFrom != null && dto.UnpanJutakushaCdFrom != '')*/
                    AND UPN_GYOUSHA_CD >= /*dto.UnpanJutakushaCdFrom*/''
                /*END*/
                /*IF (dto.UnpanJutakushaCdTo != null && dto.UnpanJutakushaCdTo != '')*/
                    AND UPN_GYOUSHA_CD <= /*dto.UnpanJutakushaCdTo*/''
                /*END*/
                /*IF (dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != '')*/
                    AND CONVERT(varchar, UPN_END_DATE, 112) >= /*dto.UnpanEndDateFrom*/''
                /*END*/
                /*IF (dto.UnpanEndDateTo != null && dto.UnpanEndDateTo != '')*/
                    AND CONVERT(varchar, UPN_END_DATE, 112) <= /*dto.UnpanEndDateTo*/''
                /*END*/
                /*IF (dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != '') 
                  || (dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != '')*/
                    AND ( UPN_SAKI_KBN = 1 OR  UPN_SAKI_KBN IS NULL )
                    /*IF dto.ShobunJigyoushaCdFrom != null && dto.ShobunJigyoushaCdFrom != ''*/AND UPN_SAKI_GYOUSHA_CD >= /*dto.ShobunJigyoushaCdFrom*/''/*END*/
                    /*IF dto.ShobunJigyoushaCdTo != null && dto.ShobunJigyoushaCdTo != ''*/AND UPN_SAKI_GYOUSHA_CD <= /*dto.ShobunJigyoushaCdTo*/''/*END*/
                /*END*/
                /*IF (dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != '')*/
                    AND UPN_SAKI_GENBA_CD >= /*dto.ShobunJigyoujouCdFrom*/''
                /*END*/
                /*IF (dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != '')*/
                    AND UPN_SAKI_GENBA_CD <= /*dto.ShobunJigyoujouCdTo*/''
                /*END*/			
			GROUP BY SYSTEM_ID, SEQ, UPN_GYOUSHA_CD
		) AS TMU_U ON TME.SYSTEM_ID = TMU_U.SYSTEM_ID AND TME.SEQ = TMU_U.SEQ
        LEFT JOIN T_MANIFEST_UPN AS TMU ON  TMU_U.SYSTEM_ID = TMU.SYSTEM_ID AND TMU_U.SEQ = TMU.SEQ AND TMU_U.MAX_UPN_ROUTE_NO = TMU.UPN_ROUTE_NO
		LEFT JOIN (
            SELECT
                SYSTEM_ID,
                SEQ,
                MAX(ISNULL(UPN_ROUTE_NO, 0))AS MAX_UPN_ROUTE_NO
            FROM
                T_MANIFEST_UPN AS UPN
            WHERE
                ISNULL(UPN.UPN_SAKI_GYOUSHA_CD, '') <> ''
                /*IF (dto.UnpanJutakushaCdFrom != null && dto.UnpanJutakushaCdFrom != '')*/
                    AND UPN_GYOUSHA_CD >= /*dto.UnpanJutakushaCdFrom*/''
                /*END*/
                /*IF (dto.UnpanJutakushaCdTo != null && dto.UnpanJutakushaCdTo != '')*/
                    AND UPN_GYOUSHA_CD <= /*dto.UnpanJutakushaCdTo*/''
                /*END*/
                /*IF (dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != '')*/
                    AND CONVERT(varchar, UPN_END_DATE, 112) >= /*dto.UnpanEndDateFrom*/''
                /*END*/
                /*IF (dto.UnpanEndDateTo != null && dto.UnpanEndDateTo != '')*/
                    AND CONVERT(varchar, UPN_END_DATE, 112) <= /*dto.UnpanEndDateTo*/''
                /*END*/
                /*IF (dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != '') 
                  || (dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != '')*/
                    AND ( UPN.UPN_SAKI_KBN = 1 OR  UPN.UPN_SAKI_KBN IS NULL )
                    /*IF dto.ShobunJigyoushaCdFrom != null && dto.ShobunJigyoushaCdFrom != ''*/AND UPN.UPN_SAKI_GYOUSHA_CD >= /*dto.ShobunJigyoushaCdFrom*/''/*END*/
                    /*IF dto.ShobunJigyoushaCdTo != null && dto.ShobunJigyoushaCdTo != ''*/AND UPN.UPN_SAKI_GYOUSHA_CD <= /*dto.ShobunJigyoushaCdTo*/''/*END*/
                /*END*/
                /*IF (dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != '')*/
                    AND UPN_SAKI_GENBA_CD >= /*dto.ShobunJigyoujouCdFrom*/''
                /*END*/
                /*IF (dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != '')*/
                    AND UPN_SAKI_GENBA_CD <= /*dto.ShobunJigyoujouCdTo*/''
                /*END*/
            GROUP BY
                SYSTEM_ID, SEQ
        ) AS DISP_MAX_ROUTE_NO ON TMU.SYSTEM_ID = DISP_MAX_ROUTE_NO.SYSTEM_ID AND TMU.SEQ = DISP_MAX_ROUTE_NO.SEQ
        LEFT JOIN T_MANIFEST_UPN AS DISP_MAX_ROUTE_INFO ON DISP_MAX_ROUTE_NO.SYSTEM_ID = DISP_MAX_ROUTE_INFO.SYSTEM_ID AND DISP_MAX_ROUTE_NO.SEQ = DISP_MAX_ROUTE_INFO.SEQ AND DISP_MAX_ROUTE_NO.MAX_UPN_ROUTE_NO = DISP_MAX_ROUTE_INFO.UPN_ROUTE_NO
        LEFT JOIN (
            SELECT
                SYSTEM_ID,
                SEQ,
                MAX(ISNULL(UPN_ROUTE_NO, 0))AS MAX_UPN_ROUTE_NO
            FROM
                T_MANIFEST_UPN AS UPN
            WHERE
                ISNULL(UPN.UPN_SAKI_GYOUSHA_CD, '') <> ''
            GROUP BY
                SYSTEM_ID, SEQ
        ) AS SBN_JOU_ROUTE_NO ON TMU.SYSTEM_ID = SBN_JOU_ROUTE_NO.SYSTEM_ID AND TMU.SEQ = SBN_JOU_ROUTE_NO.SEQ
        LEFT JOIN T_MANIFEST_UPN AS SBN_JOU_INFO ON SBN_JOU_ROUTE_NO.SYSTEM_ID = SBN_JOU_INFO.SYSTEM_ID AND SBN_JOU_ROUTE_NO.SEQ = SBN_JOU_INFO.SEQ AND SBN_JOU_ROUTE_NO.MAX_UPN_ROUTE_NO = SBN_JOU_INFO.UPN_ROUTE_NO
        LEFT JOIN M_GENBA AS SBN_GENBA ON SBN_JOU_INFO.UPN_SAKI_GYOUSHA_CD = SBN_GENBA.GYOUSHA_CD AND SBN_JOU_INFO.UPN_SAKI_GENBA_CD = SBN_GENBA.GENBA_CD
        /*END*/
        LEFT JOIN M_KYOTEN AS MK ON  TME.KYOTEN_CD = MK.KYOTEN_CD
        LEFT JOIN M_GYOUSHA AS HST_GYOUSHA ON  TME.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD
        LEFT JOIN M_GENBA AS HST_GENBA ON  TME.HST_GYOUSHA_CD = HST_GENBA.GYOUSHA_CD AND TME.HST_GENBA_CD = HST_GENBA.GENBA_CD
        /*IF dto.SelectedColumnUnpanJutakushaCd*/
        LEFT JOIN M_GYOUSHA AS UPN_GYOUSHA ON  TMU.UPN_GYOUSHA_CD = UPN_GYOUSHA.GYOUSHA_CD
        /*END*/
        LEFT JOIN M_GYOUSHA AS SBN_GYOUSHA ON  TME.SBN_GYOUSHA_CD = SBN_GYOUSHA.GYOUSHA_CD
        /*IF dto.SelectedColumnShobunJigyoujouCd*/
        LEFT JOIN M_GENBA AS UPN_GENBA ON  TMU.UPN_SAKI_GYOUSHA_CD = UPN_GENBA.GYOUSHA_CD AND TMU.UPN_SAKI_GENBA_CD = UPN_GENBA.GENBA_CD
        /*END*/
        LEFT JOIN M_GYOUSHA AS LAST_SBN_GYOUSHA ON  TMD.LAST_SBN_GYOUSHA_CD = LAST_SBN_GYOUSHA.GYOUSHA_CD
        LEFT JOIN M_GENBA AS LAST_SBN_GENBA ON  TMD.LAST_SBN_GYOUSHA_CD = LAST_SBN_GENBA.GYOUSHA_CD AND TMD.LAST_SBN_GENBA_CD = LAST_SBN_GENBA.GENBA_CD
        LEFT JOIN M_HAIKI_SHURUI AS MHS ON  TME.HAIKI_KBN_CD = MHS.HAIKI_KBN_CD AND TMD.HAIKI_SHURUI_CD = MHS.HAIKI_SHURUI_CD
        LEFT JOIN M_HOUKOKUSHO_BUNRUI AS MHB ON  MHS.HOUKOKUSHO_BUNRUI_CD = MHB.HOUKOKUSHO_BUNRUI_CD
        LEFT JOIN M_HAIKI_NAME AS MHN ON  TMD.HAIKI_NAME_CD = MHN.HAIKI_NAME_CD
        LEFT JOIN M_SHOBUN_HOUHOU AS MSH ON  TMD.SBN_HOUHOU_CD = MSH.SHOBUN_HOUHOU_CD
        LEFT JOIN M_TORIHIKISAKI AS MT ON  TME.TORIHIKISAKI_CD = MT.TORIHIKISAKI_CD
    WHERE
        TME.DELETE_FLG = 0

    /*IF dto.KyotenCd != 99*/AND TME.KYOTEN_CD = /*dto.KyotenCd*/99/*END*/

    /*IF dto.IchijiNijiKbn == 1*/AND TME.FIRST_MANIFEST_KBN = 0/*END*/
    /*IF dto.IchijiNijiKbn == 2*/AND TME.FIRST_MANIFEST_KBN = 1/*END*/

    /*IF dto.KofuDateFrom != null && dto.KofuDateFrom != ''*/AND CONVERT(varchar, TME.KOUFU_DATE, 112) >= /*dto.KofuDateFrom*/''/*END*/
    /*IF dto.KofuDateTo != null && dto.KofuDateTo != ''*/AND CONVERT(varchar, TME.KOUFU_DATE, 112) <= /*dto.KofuDateTo*/''/*END*/

    /*IF dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != ''*/AND CONVERT(varchar, TMU.UPN_END_DATE, 112) >= /*dto.UnpanEndDateFrom*/''/*END*/
    /*IF dto.UnpanEndDateTo != null && dto.UnpanEndDateTo != ''*/AND CONVERT(varchar, TMU.UPN_END_DATE, 112) <= /*dto.UnpanEndDateTo*/''/*END*/

    /*IF dto.ShobunEndDateFrom != null && dto.ShobunEndDateFrom != ''*/AND CONVERT(varchar, TMD.SBN_END_DATE, 112) >= /*dto.ShobunEndDateFrom*/''/*END*/
    /*IF dto.ShobunEndDateTo != null && dto.ShobunEndDateTo != ''*/AND CONVERT(varchar, TMD.SBN_END_DATE, 112) <= /*dto.ShobunEndDateTo*/''/*END*/

    /*IF dto.LastShobunEndDateFrom != null && dto.LastShobunEndDateFrom != ''*/AND CONVERT(varchar, TMD.LAST_SBN_END_DATE, 112) >= /*dto.LastShobunEndDateFrom*/''/*END*/
    /*IF dto.LastShobunEndDateTo != null && dto.LastShobunEndDateTo != ''*/AND CONVERT(varchar, TMD.LAST_SBN_END_DATE, 112) <= /*dto.LastShobunEndDateTo*/''/*END*/

    /*IF dto.HaishutsuJigyoushaCdFrom != null && dto.HaishutsuJigyoushaCdFrom != ''*/AND TME.HST_GYOUSHA_CD >= /*dto.HaishutsuJigyoushaCdFrom*/''/*END*/
    /*IF dto.HaishutsuJigyoushaCdTo != null && dto.HaishutsuJigyoushaCdTo != ''*/AND TME.HST_GYOUSHA_CD <= /*dto.HaishutsuJigyoushaCdTo*/''/*END*/

    /*IF dto.HaishutsuJigyoujouCdFrom != null && dto.HaishutsuJigyoujouCdFrom != ''*/AND TME.HST_GENBA_CD >= /*dto.HaishutsuJigyoujouCdFrom*/''/*END*/
    /*IF dto.HaishutsuJigyoujouCdTo != null && dto.HaishutsuJigyoujouCdTo != ''*/AND TME.HST_GENBA_CD <= /*dto.HaishutsuJigyoujouCdTo*/''/*END*/

    /*IF dto.UnpanJutakushaCdFrom != null && dto.UnpanJutakushaCdFrom != ''*/AND TMU.UPN_GYOUSHA_CD >= /*dto.UnpanJutakushaCdFrom*/''/*END*/
    /*IF dto.UnpanJutakushaCdTo != null && dto.UnpanJutakushaCdTo != ''*/AND TMU.UPN_GYOUSHA_CD <= /*dto.UnpanJutakushaCdTo*/''/*END*/

    /*IF dto.ShobunJigyoushaCdFrom != null && dto.ShobunJigyoushaCdFrom != ''*/AND TME.SBN_GYOUSHA_CD >= /*dto.ShobunJigyoushaCdFrom*/''/*END*/
    /*IF dto.ShobunJigyoushaCdTo != null && dto.ShobunJigyoushaCdTo != ''*/AND TME.SBN_GYOUSHA_CD <= /*dto.ShobunJigyoushaCdTo*/''/*END*/

    /*IF dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != ''*/AND TMU.UPN_SAKI_GENBA_CD >= /*dto.ShobunJigyoujouCdFrom*/''/*END*/
    /*IF dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != ''*/AND TMU.UPN_SAKI_GENBA_CD <= /*dto.ShobunJigyoujouCdTo*/''/*END*/

    /*IF (dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != '') 
        || (dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != '')*/
        AND (
            (TMU.UPN_SAKI_KBN = 1 OR TMU.UPN_SAKI_KBN IS NULL )
            /*IF dto.ShobunJigyoushaCdFrom != null && dto.ShobunJigyoushaCdFrom != ''*/AND TMU.UPN_SAKI_GYOUSHA_CD >= /*dto.ShobunJigyoushaCdFrom*/''/*END*/
            /*IF dto.ShobunJigyoushaCdTo != null && dto.ShobunJigyoushaCdTo != ''*/AND TMU.UPN_SAKI_GYOUSHA_CD <= /*dto.ShobunJigyoushaCdTo*/''/*END*/
            /*IF dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != ''*/AND TMU.UPN_SAKI_GENBA_CD >= /*dto.ShobunJigyoujouCdFrom*/''/*END*/
            /*IF dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != ''*/AND TMU.UPN_SAKI_GENBA_CD <= /*dto.ShobunJigyoujouCdTo*/''/*END*/
        )
    /*END*/

    /*IF dto.LastShobunJigyoushaCdFrom != null && dto.LastShobunJigyoushaCdFrom != ''*/AND TMD.LAST_SBN_GYOUSHA_CD >= /*dto.LastShobunJigyoushaCdFrom*/''/*END*/
    /*IF dto.LastShobunJigyoushaCdTo != null && dto.LastShobunJigyoushaCdTo != ''*/AND TMD.LAST_SBN_GYOUSHA_CD <= /*dto.LastShobunJigyoushaCdTo*/''/*END*/

    /*IF dto.LastShobunJigyoujouCdFrom != null && dto.LastShobunJigyoujouCdFrom != ''*/AND TMD.LAST_SBN_GENBA_CD >= /*dto.LastShobunJigyoujouCdFrom*/''/*END*/
    /*IF dto.LastShobunJigyoujouCdTo != null && dto.LastShobunJigyoujouCdTo != ''*/AND TMD.LAST_SBN_GENBA_CD <= /*dto.LastShobunJigyoujouCdTo*/''/*END*/

    /*IF dto.HoukokushoBunruiCdFrom != null && dto.HoukokushoBunruiCdFrom != ''*/AND MHS.HOUKOKUSHO_BUNRUI_CD >= /*dto.HoukokushoBunruiCdFrom*/''/*END*/
    /*IF dto.HoukokushoBunruiCdTo != null && dto.HoukokushoBunruiCdTo != ''*/AND MHS.HOUKOKUSHO_BUNRUI_CD <= /*dto.HoukokushoBunruiCdTo*/''/*END*/

    /*IF dto.HaikibutsuMeishouCdFrom != null && dto.HaikibutsuMeishouCdFrom != ''*/AND TMD.HAIKI_NAME_CD >= /*dto.HaikibutsuMeishouCdFrom*/''/*END*/
    /*IF dto.HaikibutsuMeishouCdTo != null && dto.HaikibutsuMeishouCdTo != ''*/AND TMD.HAIKI_NAME_CD <= /*dto.HaikibutsuMeishouCdTo*/''/*END*/

    /*IF dto.ShobunHouhouCdFrom != null && dto.ShobunHouhouCdFrom != ''*/AND TMD.SBN_HOUHOU_CD >= /*dto.ShobunHouhouCdFrom*/''/*END*/
    /*IF dto.ShobunHouhouCdTo != null && dto.ShobunHouhouCdTo != ''*/AND TMD.SBN_HOUHOU_CD <= /*dto.ShobunHouhouCdTo*/''/*END*/

    /*IF dto.TorihikisakiCdFrom != null && dto.TorihikisakiCdFrom != ''*/AND TME.TORIHIKISAKI_CD >= /*dto.TorihikisakiCdFrom*/''/*END*/
    /*IF dto.TorihikisakiCdTo != null && dto.TorihikisakiCdTo != ''*/AND TME.TORIHIKISAKI_CD <= /*dto.TorihikisakiCdTo*/''/*END*/
    -- 産廃マニ(積替)の場合、必ず3区間分のデータをT_MANIFEST_UPNに登録している仕組み上
    -- 空行や処分受託者が表示されてしまう。
    -- そのため、以下の絞り込みで運搬以外を表示しないようにする。
    /*IF dto.SelectedColumnUnpanJutakushaCd*/AND ISNULL(TMU.UPN_GYOUSHA_CD, '') <> ''/*END*/
/*END*/

/*IF dto.IsKamiMani && dto.IsDenMani*/
    UNION ALL
/*END*/

/*IF dto.IsDenMani*/
    SELECT
		/*IF dto.KofuDateFrom != null && dto.KofuDateFrom != ''*/SUBSTRING(DR18.HIKIWATASHI_DATE, 1, 4) + '/' + SUBSTRING(DR18.HIKIWATASHI_DATE, 5, 2) as BASE_DATE,/*END*/
		/*IF dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != ''*/SUBSTRING(UPN.UPN_END_DATE, 1, 4) + '/' + SUBSTRING(UPN.UPN_END_DATE, 5, 2) as BASE_DATE,/*END*/
		/*IF dto.ShobunEndDateFrom != null && dto.ShobunEndDateFrom != ''*/SUBSTRING(DR18.SBN_END_DATE, 1, 4) + '/' + SUBSTRING(DR18.SBN_END_DATE, 5, 2) as BASE_DATE,/*END*/
		/*IF dto.LastShobunEndDateFrom != null && dto.LastShobunEndDateFrom != ''*/SUBSTRING(DR13.MAX_LAST_SBN_END_DATE, 1, 4) + '/' + SUBSTRING(DR18.SBN_END_DATE, 5, 2) as BASE_DATE,/*END*/
        1 AS DUMMY,                                                         -- ダミー行
        NULL AS KYOTEN_CD,                                                  -- 拠点
        NULL AS KYOTEN_CD_NAME,                                             -- 拠点名
        HST_JIGYOUSHA.GYOUSHA_CD AS HST_GYOUSHA_CD,                         -- 排出事業者CD
        CASE
            WHEN HST_JIGYOUSHA.GYOUSHA_NAME_RYAKU IS NOT NULL THEN HST_JIGYOUSHA.GYOUSHA_NAME_RYAKU
            ELSE DR18.HST_SHA_NAME
        END HST_GYOUSHA_CD_NAME,                                            -- 排出事業者名
        HST_JIGYOUJOU.GENBA_CD AS HST_GENBA_CD,                             -- 排出事業場CD
        CASE
            WHEN HST_JIGYOUJOU.GENBA_NAME_RYAKU IS NOT NULL THEN HST_JIGYOUJOU.GENBA_NAME_RYAKU
            ELSE DR18.HST_JOU_NAME
        END HST_GENBA_CD_NAME,                                              -- 排出事業場名
        /*IF dto.SelectedColumnUnpanJutakushaCd*/
        CASE
            WHEN UPN.GYOUSHA_CD IS NOT NULL THEN UPN.GYOUSHA_CD
            ELSE UPN.UPN_GYOUSHA_CD
        END UPN_GYOUSHA_CD,                                                 -- 運搬受託者CD
        CASE
            WHEN UPN.GYOUSHA_NAME_RYAKU IS NOT NULL THEN UPN.GYOUSHA_NAME_RYAKU
            ELSE UPN.UPN_SHA_NAME
        END UPN_GYOUSHA_CD_NAME,                                            -- 運搬受託者名
        /*END*/
        SBN_JIGYOUSHA.GYOUSHA_CD AS SBN_GYOUSHA_CD,                         -- 処分事業者CD
        CASE
            WHEN SBN_JIGYOUSHA.GYOUSHA_NAME_RYAKU IS NOT NULL THEN SBN_JIGYOUSHA.GYOUSHA_NAME_RYAKU
            ELSE DR18.SBN_SHA_NAME
        END SBN_GYOUSHA_CD_NAME,                                            -- 処分事業者
        /*IF dto.SelectedColumnShobunJigyoujouCd*/
        CASE
            WHEN SBN_JOU_INFO.GENBA_CD IS NOT NULL THEN SBN_JOU_INFO.GENBA_CD
            ELSE SBN_JOU_INFO.UPNSAKI_GENBA_CD
        END UPN_SAKI_GENBA_CD,                                              -- 処分事業場CD
        CASE
            WHEN SBN_JOU_INFO.GENBA_NAME_RYAKU IS NOT NULL THEN SBN_JOU_INFO.GENBA_NAME_RYAKU
            ELSE SBN_JOU_INFO.UPNSAKI_JOU_NAME
        END UPN_SAKI_GENBA_CD_NAME,                                         -- 処分事業場名
        /*END*/
        LAST_SBN_JIGYOUJOU.GYOUSHA_CD AS LAST_SBN_GYOUSHA_CD,               -- 最終処分受託者CD
        LAST_SBN_JIGYOUJOU.GYOUSHA_NAME_RYAKU AS LAST_SBN_GYOUSHA_CD_NAME,  -- 最終処分受託者名
        LAST_SBN_JIGYOUJOU.GENBA_CD AS LAST_SBN_GENBA_CD,                   -- 最終処分場所CD
        CASE
            WHEN LAST_SBN_JIGYOUJOU.GENBA_NAME_RYAKU IS NOT NULL THEN LAST_SBN_JIGYOUJOU.GENBA_NAME_RYAKU
            ELSE DR13.LAST_SBN_JOU_NAME
        END LAST_SBN_GENBA_CD_NAME,                                         -- 最終処分場所名
        CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN MDHS_MIX.HOUKOKUSHO_BUNRUI_CD
            ELSE MDHS.HOUKOKUSHO_BUNRUI_CD
        END AS HOUKOKUSHO_BUNRUI_CD,                                        -- 報告書分類CD
        CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN MHB_MIX.HOUKOKUSHO_BUNRUI_NAME_RYAKU
            ELSE MHB.HOUKOKUSHO_BUNRUI_NAME_RYAKU
        END AS HOUKOKUSHO_BUNRUI_CD_NAME,                                   -- 報告書分類
        CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN MDHS_MIX.HAIKI_SHURUI_CD
            ELSE MDHS.HAIKI_SHURUI_CD
        END AS HAIKI_SHURUI_CD,                                             -- 廃棄物種類CD
        4 AS HAIKI_KBN_CD,                                                  -- 廃棄物区分CD(電子固定)
        CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN MDHS_MIX.HAIKI_SHURUI_NAME
            ELSE MDHS.HAIKI_SHURUI_NAME
        END AS HAIKI_SHURUI_CD_NAME,                                        -- 廃棄物種類
        ISNULL(CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN DR18MIX.HAIKI_NAME_CD
            ELSE DR18EX.HAIKI_NAME_CD
        END,'') AS HAIKI_NAME_CD,                                               -- 廃棄物名称CD
        ISNULL(CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN MHN_MIX.HAIKI_NAME
            ELSE DR18.HAIKI_NAME
        END,'') AS HAIKI_NAME_CD_NAME,                                          -- 廃棄物名称
        CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN CONVERT(nvarchar, DR18MIX.SBN_HOUHOU_CD)
            ELSE CONVERT(nvarchar, DR18EX.SBN_HOUHOU_CD)
        END AS SBN_HOUHOU_CD,                                               -- 処分方法CD
        CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN MSH_MIX.SHOBUN_HOUHOU_NAME_RYAKU
            ELSE MSH.SHOBUN_HOUHOU_NAME_RYAKU
        END AS SBN_HOUHOU_CD_NAME,                                          -- 処分方法
        NULL AS TORIHIKISAKI_CD,                                            -- 取引先
        NULL AS TORIHIKISAKI_CD_NAME,                                       -- 取引先名
        --換算後数量
        /*IF !dto.SelectedColumnUnpanJutakushaCd && dto.SelectedColumnShobunJigyoujouCd*/
        CASE WHEN DISP_MAX_ROUTE_NO.MAX_UPN_ROUTE_NO = UPN.UPN_ROUTE_NO
            THEN DR18EX.KANSAN_SUU
            ELSE NULL
        END AS KANSANGO_SURYO
        -- ELSE
        DR18EX.KANSAN_SUU AS KANSANGO_SURYO
        /*END*/
    FROM DT_MF_TOC AS DMT
    JOIN DT_R18 AS DR18 ON DMT.KANRI_ID = DR18.KANRI_ID AND DMT.LATEST_SEQ = DR18.SEQ
    /*IF dto.IchijiNijiKbn == 1 || dto.IchijiNijiKbn == 3*/
    JOIN 
    (
        SELECT
            DT_R18_EX.KANRI_ID,
            DT_R18_EX.SEQ,
            DT_R18_MIX.DETAIL_SYSTEM_ID,
            CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL
                THEN DT_R18_MIX.DETAIL_SYSTEM_ID
                ELSE DT_R18_EX.SYSTEM_ID
            END AS SYSTEM_ID,
            CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL
                THEN DT_R18_MIX.HAIKI_NAME_CD
                ELSE DT_R18_EX.HAIKI_NAME_CD
            END AS HAIKI_NAME_CD,
            CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL
                THEN DT_R18_MIX.KANSAN_SUU
                ELSE DT_R18_EX.KANSAN_SUU
            END AS KANSAN_SUU,
            DT_R18_EX.HST_GYOUSHA_CD,
            DT_R18_EX.HST_GENBA_CD,
            DT_R18_EX.SBN_GYOUSHA_CD,
            DT_R18_EX.SBN_HOUHOU_CD,
            DT_R18_EX.DELETE_FLG AS DELETE_FLG
        FROM
            DT_R18_EX
            LEFT JOIN DT_R18_MIX
            ON DT_R18_EX.SYSTEM_ID = DT_R18_MIX.SYSTEM_ID
            AND DT_R18_MIX.DELETE_FLG = 0
    ) AS DR18EX ON DR18.KANRI_ID = DR18EX.KANRI_ID AND DR18EX.DELETE_FLG = 0
    LEFT JOIN DT_R18_MIX AS DR18MIX ON DR18EX.KANRI_ID = DR18MIX.KANRI_ID AND DR18EX.DETAIL_SYSTEM_ID = DR18MIX.DETAIL_SYSTEM_ID AND DR18MIX.DELETE_FLG = 0 AND DR18EX.DETAIL_SYSTEM_ID IS NOT NULL
    /*END*/
    /*IF dto.IchijiNijiKbn == 2*/
    JOIN DT_R18_EX AS DR18EX ON DR18.KANRI_ID = DR18EX.KANRI_ID AND DR18EX.DELETE_FLG = 0
    LEFT JOIN DT_R18_MIX AS DR18MIX ON DR18EX.KANRI_ID = DR18MIX.KANRI_ID AND DR18EX.SYSTEM_ID = DR18MIX.SYSTEM_ID AND DR18MIX.DELETE_FLG = 0
    /*END*/
    LEFT JOIN M_GYOUSHA AS HST_GYOUSHA ON DR18EX.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD
    LEFT JOIN (
        SELECT
            R13_1.KANRI_ID,
            R13_1.SEQ,
			R13_1.REC_SEQ,
            R13_1.LAST_SBN_END_DATE,
            R13_1.LAST_SBN_JOU_NAME,
            R13_1.LAST_SBN_JOU_ADDRESS1,
            R13_1.LAST_SBN_JOU_ADDRESS2,
            R13_1.LAST_SBN_JOU_ADDRESS3,
            R13_1.LAST_SBN_JOU_ADDRESS4,
            R13_2.CNT,
            R13_2.LAST_SBN_END_DATE AS MAX_LAST_SBN_END_DATE
        FROM DT_R13 AS R13_1
            JOIN (
                SELECT
                     KANRI_ID,
                     SEQ,
                     COUNT(KANRI_ID) AS CNT,
                     MAX(LAST_SBN_END_DATE) AS LAST_SBN_END_DATE
                FROM DT_R13
                GROUP BY KANRI_ID, SEQ
            ) AS R13_2 ON R13_1.KANRI_ID = R13_2.KANRI_ID AND R13_1.SEQ = R13_2.SEQ
        WHERE R13_1.REC_SEQ = 1
    ) AS DR13 ON DR18.KANRI_ID = DR13.KANRI_ID AND DR18.SEQ = DR13.SEQ
	LEFT JOIN DT_R13_EX AS DR13EX ON DR13EX.SEQ = DR13.SEQ AND DR13EX.REC_SEQ = DR13.REC_SEQ AND DR13EX.KANRI_ID = DR13.KANRI_ID
    LEFT JOIN M_SYS_INFO AS MSI ON 1 = 1
    LEFT JOIN (
        SELECT
            M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID,
            M_GYOUSHA.GYOUSHA_CD,
            M_GYOUSHA.GYOUSHA_NAME_RYAKU
        FROM M_DENSHI_JIGYOUSHA
            JOIN M_GYOUSHA ON M_DENSHI_JIGYOUSHA.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD
    ) AS HST_JIGYOUSHA ON HST_JIGYOUSHA.EDI_MEMBER_ID = DR18.HST_SHA_EDI_MEMBER_ID
    LEFT JOIN (
        SELECT DISTINCT
			M_GENBA.GYOUSHA_CD,
            M_GENBA.GENBA_CD,
            M_GENBA.GENBA_NAME_RYAKU,
            M_DENSHI_JIGYOUJOU.JIGYOUJOU_NAME,
            (ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS1, 0) + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS2, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS3, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS4, '')) AS JIGYOUJOU_ADDRESS
        FROM M_DENSHI_JIGYOUJOU
            JOIN M_GENBA ON M_DENSHI_JIGYOUJOU.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND M_DENSHI_JIGYOUJOU.GENBA_CD = M_GENBA.GENBA_CD
    ) AS HST_JIGYOUJOU ON (HST_JIGYOUJOU.GYOUSHA_CD = DR18EX.HST_GYOUSHA_CD AND HST_JIGYOUJOU.GENBA_CD = DR18EX.HST_GENBA_CD) AND HST_JIGYOUJOU.JIGYOUJOU_NAME = DR18.HST_JOU_NAME AND HST_JIGYOUJOU.JIGYOUJOU_ADDRESS = (ISNULL(DR18.HST_JOU_ADDRESS1, '') + ISNULL(DR18.HST_JOU_ADDRESS2, '') + ISNULL(DR18.HST_JOU_ADDRESS3, '') + ISNULL(DR18.HST_JOU_ADDRESS4, ''))
    LEFT JOIN (
        SELECT
            M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID,
            M_GYOUSHA.GYOUSHA_CD,
            M_GYOUSHA.GYOUSHA_NAME_RYAKU
        FROM M_DENSHI_JIGYOUSHA
            JOIN M_GYOUSHA ON M_DENSHI_JIGYOUSHA.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD
    ) AS SBN_JIGYOUSHA ON SBN_JIGYOUSHA.EDI_MEMBER_ID = DR18.SBN_SHA_MEMBER_ID
    LEFT JOIN M_UNIT AS MU ON DR18.HAIKI_UNIT_CODE = MU.UNIT_CD
    LEFT JOIN M_UNIT AS MIX_MU ON DR18MIX.HAIKI_UNIT_CD = MIX_MU.UNIT_CD
    LEFT JOIN M_UNIT AS MU2 ON MSI.MANI_KANSAN_KIHON_UNIT_CD = MU2.UNIT_CD
    LEFT JOIN M_SHOBUN_HOUHOU AS MSH ON CONVERT(nvarchar, DR18EX.SBN_HOUHOU_CD) = MSH.SHOBUN_HOUHOU_CD
    LEFT JOIN M_SHOBUN_HOUHOU AS MSH_MIX ON CONVERT(nvarchar, DR18MIX.SBN_HOUHOU_CD) = MSH_MIX.SHOBUN_HOUHOU_CD
    /*IF dto.SelectedColumnUnpanJutakushaCd || dto.SelectedColumnShobunJigyoujouCd 
      || (dto.UnpanJutakushaCdFrom != null && dto.UnpanJutakushaCdFrom != '') 
      || (dto.UnpanJutakushaCdTo != null && dto.UnpanJutakushaCdTo != '') 
      || (dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != '') 
      || (dto.UnpanEndDateTo != null && dto.UnpanEndDateTo != '') 
      || (dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != '') 
      || (dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != '')*/
    LEFT JOIN (
        SELECT
            DT_MF_TOC.KANRI_ID,
            DT_MF_TOC.LATEST_SEQ,
            DT_R19_EX.UPN_GYOUSHA_CD,
            MAX(ISNULL(DT_R19.UPN_ROUTE_NO, 0)) AS MAX_UPN_ROUTE_NO
        FROM
            DT_MF_TOC
            INNER JOIN DT_R19 ON DT_MF_TOC.KANRI_ID = DT_R19.KANRI_ID AND DT_MF_TOC.LATEST_SEQ = DT_R19.SEQ
            INNER JOIN DT_R19_EX ON DT_R19.KANRI_ID = DT_R19_EX.KANRI_ID AND DT_R19.UPN_ROUTE_NO = DT_R19_EX.UPN_ROUTE_NO AND DT_R19_EX.DELETE_FLG = 0
        WHERE
            ISNULL(DT_R19_EX.UPNSAKI_GENBA_CD, '') <> ''
            /*IF (dto.UnpanJutakushaCdFrom != null && dto.UnpanJutakushaCdFrom != '')*/
                AND DT_R19_EX.UPN_GYOUSHA_CD >= /*dto.UnpanJutakushaCdFrom*/''
            /*END*/
            /*IF (dto.UnpanJutakushaCdTo != null && dto.UnpanJutakushaCdTo != '')*/
                AND DT_R19_EX.UPN_GYOUSHA_CD <= /*dto.UnpanJutakushaCdTo*/''
            /*END*/
            /*IF (dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != '')*/
                AND CONVERT(varchar, DT_R19.UPN_END_DATE, 112) >= /*dto.UnpanEndDateFrom*/''
            /*END*/
            /*IF (dto.UnpanEndDateTo != null && dto.UnpanEndDateTo != '')*/
                AND CONVERT(varchar, DT_R19.UPN_END_DATE, 112) <= /*dto.UnpanEndDateTo*/''
            /*END*/
            /*IF (dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != '')*/
                AND DT_R19_EX.UPNSAKI_GENBA_CD >= /*dto.ShobunJigyoujouCdFrom*/''
            /*END*/
            /*IF (dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != '')*/
                AND DT_R19_EX.UPNSAKI_GENBA_CD <= /*dto.ShobunJigyoujouCdTo*/''
            /*END*/
		GROUP BY DT_MF_TOC.KANRI_ID, DT_MF_TOC.LATEST_SEQ, DT_R19_EX.UPN_GYOUSHA_CD
    ) AS UPN_U ON UPN_U.KANRI_ID = DMT.KANRI_ID AND UPN_U.LATEST_SEQ = DMT.LATEST_SEQ
    LEFT JOIN (
        SELECT
            DT_R19.KANRI_ID,
            DT_R19.SEQ,
            DT_R19.UPN_END_DATE,
            DT_R19_EX.UPN_GYOUSHA_CD,
            DT_R19.UPN_SHA_NAME,
            M_GYOUSHA.GYOUSHA_CD,
            M_GYOUSHA.GYOUSHA_NAME_RYAKU,
            DT_R19_EX.UPNSAKI_GENBA_CD,
            DT_R19.UPNSAKI_JOU_NAME,
            M_GENBA.GENBA_CD,
            M_GENBA.GENBA_NAME_RYAKU,
            DT_R19.UPNREP_UPN_TAN_NAME,
            DT_R19.UPN_ROUTE_NO
        FROM DT_R19
            JOIN DT_R19_EX ON DT_R19.KANRI_ID = DT_R19_EX.KANRI_ID AND DT_R19.UPN_ROUTE_NO = DT_R19_EX.UPN_ROUTE_NO AND DT_R19_EX.DELETE_FLG = 0
            LEFT JOIN M_DENSHI_JIGYOUSHA ON DT_R19.UPN_SHA_EDI_MEMBER_ID = M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID
            LEFT JOIN M_GYOUSHA ON M_DENSHI_JIGYOUSHA.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD
            LEFT JOIN M_DENSHI_JIGYOUJOU ON DT_R19.UPNSAKI_JOU_NAME = M_DENSHI_JIGYOUJOU.JIGYOUJOU_NAME AND DT_R19.UPNSAKI_EDI_MEMBER_ID = M_DENSHI_JIGYOUJOU.EDI_MEMBER_ID
                AND (ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS1, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS2, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS3, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS4, ''))
                    = (ISNULL(DT_R19.UPNSAKI_JOU_ADDRESS1, '') + ISNULL(DT_R19.UPNSAKI_JOU_ADDRESS2, '') + ISNULL(DT_R19.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(DT_R19.UPNSAKI_JOU_ADDRESS4, ''))
            LEFT JOIN M_GENBA ON M_DENSHI_JIGYOUJOU.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND M_DENSHI_JIGYOUJOU.GENBA_CD = M_GENBA.GENBA_CD
    ) AS UPN ON UPN.KANRI_ID = UPN_U.KANRI_ID AND UPN.SEQ = UPN_U.LATEST_SEQ AND UPN.UPN_ROUTE_NO = UPN_U.MAX_UPN_ROUTE_NO
    LEFT JOIN
    (
        SELECT
            TOC.KANRI_ID,
            TOC.LATEST_SEQ,
            MAX(ISNULL(R19.UPN_ROUTE_NO, 0)) AS MAX_UPN_ROUTE_NO
        FROM
            DT_MF_TOC TOC
            INNER JOIN DT_R19 R19 ON TOC.KANRI_ID = R19.KANRI_ID AND TOC.LATEST_SEQ = R19.SEQ
            INNER JOIN DT_R19_EX R19EX ON R19.KANRI_ID = R19EX.KANRI_ID AND R19.UPN_ROUTE_NO = R19EX.UPN_ROUTE_NO
        WHERE
            ISNULL(R19EX.UPNSAKI_GENBA_CD, '') <> ''
            /*IF (dto.UnpanJutakushaCdFrom != null && dto.UnpanJutakushaCdFrom != '')*/
                AND R19EX.UPN_GYOUSHA_CD >= /*dto.UnpanJutakushaCdFrom*/''
            /*END*/
            /*IF (dto.UnpanJutakushaCdTo != null && dto.UnpanJutakushaCdTo != '')*/
                AND R19EX.UPN_GYOUSHA_CD <= /*dto.UnpanJutakushaCdTo*/''
            /*END*/
            /*IF (dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != '')*/
                AND CONVERT(varchar, R19.UPN_END_DATE, 112) >= /*dto.UnpanEndDateFrom*/''
            /*END*/
            /*IF (dto.UnpanEndDateTo != null && dto.UnpanEndDateTo != '')*/
                AND CONVERT(varchar, R19.UPN_END_DATE, 112) <= /*dto.UnpanEndDateTo*/''
            /*END*/
            /*IF (dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != '')*/
                AND R19EX.UPNSAKI_GENBA_CD >= /*dto.ShobunJigyoujouCdFrom*/''
            /*END*/
            /*IF (dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != '')*/
                AND R19EX.UPNSAKI_GENBA_CD <= /*dto.ShobunJigyoujouCdTo*/''
            /*END*/
        GROUP BY
            TOC.KANRI_ID, TOC.LATEST_SEQ
    ) AS  DISP_MAX_ROUTE_NO
        ON DISP_MAX_ROUTE_NO.KANRI_ID = DMT.KANRI_ID AND DISP_MAX_ROUTE_NO.LATEST_SEQ = DMT.LATEST_SEQ
    LEFT JOIN 
    (
        SELECT
            R19.KANRI_ID,
            R19.SEQ,
            R19.UPN_END_DATE,
            DT_R19_EX.UPN_GYOUSHA_CD,
            R19.UPN_SHA_NAME,
            M_GYOUSHA.GYOUSHA_CD,
            M_GYOUSHA.GYOUSHA_NAME_RYAKU,
            DT_R19_EX.UPNSAKI_GENBA_CD,
            R19.UPNSAKI_JOU_NAME,
            M_GENBA.GENBA_CD,
            M_GENBA.GENBA_NAME_RYAKU,
            R19.UPNREP_UPN_TAN_NAME,
            R19.UPN_ROUTE_NO
        FROM
            DT_R19 AS R19
            JOIN DT_R19_EX ON R19.KANRI_ID = DT_R19_EX.KANRI_ID AND R19.UPN_ROUTE_NO = DT_R19_EX.UPN_ROUTE_NO AND DT_R19_EX.DELETE_FLG = 0
            LEFT JOIN M_DENSHI_JIGYOUSHA ON R19.UPN_SHA_EDI_MEMBER_ID = M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID
            LEFT JOIN M_GYOUSHA ON M_DENSHI_JIGYOUSHA.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD AND M_GYOUSHA.DELETE_FLG = 0
            LEFT JOIN M_DENSHI_JIGYOUJOU ON R19.UPNSAKI_JOU_NAME = M_DENSHI_JIGYOUJOU.JIGYOUJOU_NAME AND R19.UPNSAKI_EDI_MEMBER_ID = M_DENSHI_JIGYOUJOU.EDI_MEMBER_ID
                AND (ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS1, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS2, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS3, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS4, ''))
                    = (ISNULL(R19.UPNSAKI_JOU_ADDRESS1, '') + ISNULL(R19.UPNSAKI_JOU_ADDRESS2, '') + ISNULL(R19.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(R19.UPNSAKI_JOU_ADDRESS4, ''))
            LEFT JOIN M_GENBA ON M_DENSHI_JIGYOUJOU.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND M_DENSHI_JIGYOUJOU.GENBA_CD = M_GENBA.GENBA_CD
    )AS DISP_MAX_ROUTE_INFO
        ON DISP_MAX_ROUTE_INFO.KANRI_ID = DISP_MAX_ROUTE_NO.KANRI_ID
        AND DISP_MAX_ROUTE_INFO.SEQ = DISP_MAX_ROUTE_NO.LATEST_SEQ
        AND DISP_MAX_ROUTE_INFO.UPN_ROUTE_NO = DISP_MAX_ROUTE_NO.MAX_UPN_ROUTE_NO
    LEFT JOIN 
    (
        SELECT
            R19.KANRI_ID,
            R19.SEQ,
            R19.UPN_END_DATE,
            DT_R19_EX.UPN_GYOUSHA_CD,
            R19.UPN_SHA_NAME,
            M_GYOUSHA.GYOUSHA_CD,
            M_GYOUSHA.GYOUSHA_NAME_RYAKU,
            DT_R19_EX.UPNSAKI_GENBA_CD,
            R19.UPNSAKI_JOU_NAME,
            M_GENBA.GENBA_CD,
            M_GENBA.GENBA_NAME_RYAKU,
            R19.UPNREP_UPN_TAN_NAME,
            R19.UPN_ROUTE_NO
        FROM
            DT_R19 AS R19
            JOIN DT_R19_EX ON R19.KANRI_ID = DT_R19_EX.KANRI_ID AND R19.UPN_ROUTE_NO = DT_R19_EX.UPN_ROUTE_NO AND DT_R19_EX.DELETE_FLG = 0
            LEFT JOIN M_DENSHI_JIGYOUSHA ON R19.UPN_SHA_EDI_MEMBER_ID = M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID
            LEFT JOIN M_GYOUSHA ON M_DENSHI_JIGYOUSHA.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD
            LEFT JOIN M_DENSHI_JIGYOUJOU ON R19.UPNSAKI_JOU_NAME = M_DENSHI_JIGYOUJOU.JIGYOUJOU_NAME AND R19.UPNSAKI_EDI_MEMBER_ID = M_DENSHI_JIGYOUJOU.EDI_MEMBER_ID
                AND (ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS1, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS2, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS3, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS4, ''))
                    = (ISNULL(R19.UPNSAKI_JOU_ADDRESS1, '') + ISNULL(R19.UPNSAKI_JOU_ADDRESS2, '') + ISNULL(R19.UPNSAKI_JOU_ADDRESS3, '') + ISNULL(R19.UPNSAKI_JOU_ADDRESS4, ''))
            LEFT JOIN M_GENBA ON M_DENSHI_JIGYOUJOU.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND M_DENSHI_JIGYOUJOU.GENBA_CD = M_GENBA.GENBA_CD
    )AS SBN_JOU_INFO
        ON SBN_JOU_INFO.KANRI_ID = DMT.KANRI_ID
        AND SBN_JOU_INFO.SEQ = DMT.LATEST_SEQ
        AND SBN_JOU_INFO.UPN_ROUTE_NO = DR18.UPN_ROUTE_CNT
    /*END*/
    LEFT JOIN M_DENSHI_HAIKI_SHURUI AS MDHS ON (DR18.HAIKI_DAI_CODE + DR18.HAIKI_CHU_CODE + DR18.HAIKI_SHO_CODE) = MDHS.HAIKI_SHURUI_CD
    LEFT JOIN M_DENSHI_HAIKI_SHURUI AS MDHS_MIX ON (DR18MIX.HAIKI_DAI_CODE + DR18MIX.HAIKI_CHU_CODE + DR18MIX.HAIKI_SHO_CODE) = MDHS_MIX.HAIKI_SHURUI_CD
    LEFT JOIN M_DENSHI_HAIKI_NAME AS MHN_MIX ON DR18.HST_SHA_EDI_MEMBER_ID = MHN_MIX.EDI_MEMBER_ID AND DR18MIX.HAIKI_NAME_CD = MHN_MIX.HAIKI_NAME_CD
    LEFT JOIN M_HOUKOKUSHO_BUNRUI AS MHB ON MDHS.HOUKOKUSHO_BUNRUI_CD = MHB.HOUKOKUSHO_BUNRUI_CD
    LEFT JOIN M_HOUKOKUSHO_BUNRUI AS MHB_MIX ON MDHS_MIX.HOUKOKUSHO_BUNRUI_CD = MHB_MIX.HOUKOKUSHO_BUNRUI_CD
    LEFT JOIN (
        SELECT
            M_GENBA.GYOUSHA_CD,
            M_GYOUSHA.GYOUSHA_NAME_RYAKU,
            M_GENBA.GENBA_CD,
            M_GENBA.GENBA_NAME_RYAKU,
            M_DENSHI_JIGYOUJOU.JIGYOUJOU_NAME,
            (ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS1, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS2, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS3, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS4, '')) AS JIGYOUJOU_ADDRESS
        FROM M_DENSHI_JIGYOUJOU
            JOIN M_GENBA ON M_DENSHI_JIGYOUJOU.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND M_DENSHI_JIGYOUJOU.GENBA_CD = M_GENBA.GENBA_CD
            JOIN M_GYOUSHA ON M_DENSHI_JIGYOUJOU.GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD
    ) AS LAST_SBN_JIGYOUJOU ON LAST_SBN_JIGYOUJOU.GYOUSHA_CD = DR13EX.LAST_SBN_GYOUSHA_CD AND LAST_SBN_JIGYOUJOU.GENBA_CD = DR13EX.LAST_SBN_GENBA_CD
    WHERE
    DMT.STATUS_FLAG IN (3, 4)

    /*IF dto.IchijiNijiKbn == 1*/AND (DR18.FIRST_MANIFEST_FLAG IS NULL OR DR18.FIRST_MANIFEST_FLAG = '' OR ISNULL(HST_GYOUSHA.JISHA_KBN, 0) = 0)/*END*/
    /*IF dto.IchijiNijiKbn == 2*/AND (DR18.FIRST_MANIFEST_FLAG <> '' AND ISNULL(HST_GYOUSHA.JISHA_KBN, 0) = 1)/*END*/

    /*IF dto.KofuDateFrom != null && dto.KofuDateFrom != ''*/AND DR18.HIKIWATASHI_DATE >= /*dto.KofuDateFrom*/''/*END*/
    /*IF dto.KofuDateTo != null && dto.KofuDateTo != ''*/AND DR18.HIKIWATASHI_DATE <= /*dto.KofuDateTo*/''/*END*/

    /*IF dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != ''*/AND UPN.UPN_END_DATE >= /*dto.UnpanEndDateFrom*/''/*END*/
    /*IF dto.UnpanEndDateTo != null && dto.UnpanEndDateTo != ''*/AND UPN.UPN_END_DATE <= /*dto.UnpanEndDateTo*/''/*END*/

    /*IF dto.ShobunEndDateFrom != null && dto.ShobunEndDateFrom != ''*/AND DR18.SBN_END_DATE >= /*dto.ShobunEndDateFrom*/''/*END*/
    /*IF dto.ShobunEndDateTo != null && dto.ShobunEndDateTo != ''*/AND DR18.SBN_END_DATE <= /*dto.ShobunEndDateTo*/''/*END*/

    /*IF dto.LastShobunEndDateFrom != null && dto.LastShobunEndDateFrom != ''*/AND DR13.MAX_LAST_SBN_END_DATE >= /*dto.LastShobunEndDateFrom*/''/*END*/
    /*IF dto.LastShobunEndDateTo != null && dto.LastShobunEndDateTo != ''*/AND DR13.MAX_LAST_SBN_END_DATE <= /*dto.LastShobunEndDateTo*/''/*END*/

    /*IF dto.HaishutsuJigyoushaCdFrom != null && dto.HaishutsuJigyoushaCdFrom != ''*/AND DR18EX.HST_GYOUSHA_CD >= /*dto.HaishutsuJigyoushaCdFrom*/''/*END*/
    /*IF dto.HaishutsuJigyoushaCdTo != null && dto.HaishutsuJigyoushaCdTo != ''*/AND DR18EX.HST_GYOUSHA_CD <= /*dto.HaishutsuJigyoushaCdTo*/''/*END*/

    /*IF dto.HaishutsuJigyoujouCdFrom != null && dto.HaishutsuJigyoujouCdFrom != ''*/AND DR18EX.HST_GENBA_CD >= /*dto.HaishutsuJigyoujouCdFrom*/''/*END*/
    /*IF dto.HaishutsuJigyoujouCdTo != null && dto.HaishutsuJigyoujouCdTo != ''*/AND DR18EX.HST_GENBA_CD <= /*dto.HaishutsuJigyoujouCdTo*/''/*END*/

    /*IF dto.UnpanJutakushaCdFrom != null && dto.UnpanJutakushaCdFrom != ''*/AND UPN.UPN_GYOUSHA_CD >= /*dto.UnpanJutakushaCdFrom*/''/*END*/
    /*IF dto.UnpanJutakushaCdTo != null && dto.UnpanJutakushaCdTo != ''*/AND UPN.UPN_GYOUSHA_CD <= /*dto.UnpanJutakushaCdTo*/''/*END*/

    /*IF dto.ShobunJigyoushaCdFrom != null && dto.ShobunJigyoushaCdFrom != ''*/AND DR18EX.SBN_GYOUSHA_CD >= /*dto.ShobunJigyoushaCdFrom*/''/*END*/
    /*IF dto.ShobunJigyoushaCdTo != null && dto.ShobunJigyoushaCdTo != ''*/AND DR18EX.SBN_GYOUSHA_CD <= /*dto.ShobunJigyoushaCdTo*/''/*END*/

    /*IF dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != ''*/AND UPN.UPNSAKI_GENBA_CD >= /*dto.ShobunJigyoujouCdFrom*/''/*END*/
    /*IF dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != ''*/AND UPN.UPNSAKI_GENBA_CD <= /*dto.ShobunJigyoujouCdTo*/''/*END*/

    /*IF (dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != '') 
        || (dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != '')*/
        AND UPN.UPN_ROUTE_NO = DR18.UPN_ROUTE_CNT
    /*END*/

    /*IF dto.LastShobunJigyoushaCdFrom != null && dto.LastShobunJigyoushaCdFrom != ''*/AND LAST_SBN_JIGYOUJOU.GYOUSHA_CD >= /*dto.LastShobunJigyoushaCdFrom*/''/*END*/
    /*IF dto.LastShobunJigyoushaCdTo != null && dto.LastShobunJigyoushaCdTo != ''*/AND LAST_SBN_JIGYOUJOU.GYOUSHA_CD <= /*dto.LastShobunJigyoushaCdTo*/''/*END*/

    /*IF dto.LastShobunJigyoujouCdFrom != null && dto.LastShobunJigyoujouCdFrom != ''*/AND LAST_SBN_JIGYOUJOU.GENBA_CD >= /*dto.LastShobunJigyoujouCdTo*/''/*END*/
    /*IF dto.LastShobunJigyoujouCdTo != null && dto.LastShobunJigyoujouCdTo != ''*/AND LAST_SBN_JIGYOUJOU.GENBA_CD <= /*dto.LastShobunJigyoujouCdTo*/''/*END*/

    /*IF dto.HoukokushoBunruiCdFrom != null && dto.HoukokushoBunruiCdFrom != ''*/AND ((DR18MIX.SYSTEM_ID IS NULL AND MDHS.HOUKOKUSHO_BUNRUI_CD >= /*dto.HoukokushoBunruiCdFrom*/'') OR (DR18MIX.SYSTEM_ID IS NOT NULL AND MDHS_MIX.HOUKOKUSHO_BUNRUI_CD >= /*dto.HoukokushoBunruiCdFrom*/''))/*END*/
    /*IF dto.HoukokushoBunruiCdTo != null && dto.HoukokushoBunruiCdTo != ''*/AND ((DR18MIX.SYSTEM_ID IS NULL AND MDHS.HOUKOKUSHO_BUNRUI_CD <= /*dto.HoukokushoBunruiCdTo*/'') OR (DR18MIX.SYSTEM_ID IS NOT NULL AND MDHS_MIX.HOUKOKUSHO_BUNRUI_CD <= /*dto.HoukokushoBunruiCdTo*/''))/*END*/

    /*IF dto.HaikibutsuMeishouCdFrom != null && dto.HaikibutsuMeishouCdFrom != ''*/AND ((DR18MIX.SYSTEM_ID IS NULL AND DR18EX.HAIKI_NAME_CD >= /*dto.HaikibutsuMeishouCdFrom*/'') OR (DR18MIX.SYSTEM_ID IS NOT NULL AND DR18MIX.HAIKI_NAME_CD >= /*dto.HaikibutsuMeishouCdFrom*/''))/*END*/
    /*IF dto.HaikibutsuMeishouCdTo != null && dto.HaikibutsuMeishouCdTo != ''*/AND ((DR18MIX.SYSTEM_ID IS NULL AND DR18EX.HAIKI_NAME_CD <= /*dto.HaikibutsuMeishouCdTo*/'') OR (DR18MIX.SYSTEM_ID IS NOT NULL AND DR18MIX.HAIKI_NAME_CD <= /*dto.HaikibutsuMeishouCdTo*/''))/*END*/

    /*IF dto.ShobunHouhouCdFrom != null && dto.ShobunHouhouCdFrom != ''*/AND ((DR18MIX.SYSTEM_ID IS NULL AND DR18EX.SBN_HOUHOU_CD >= /*dto.ShobunHouhouCdFrom*/'') OR (DR18MIX.SYSTEM_ID IS NOT NULL AND DR18MIX.SBN_HOUHOU_CD >= /*dto.ShobunHouhouCdFrom*/''))/*END*/
    /*IF dto.ShobunHouhouCdTo != null && dto.ShobunHouhouCdTo != ''*/AND ((DR18MIX.SYSTEM_ID IS NULL AND DR18EX.SBN_HOUHOU_CD <= /*dto.ShobunHouhouCdTo*/'') OR (DR18MIX.SYSTEM_ID IS NOT NULL AND DR18MIX.SBN_HOUHOU_CD <= /*dto.ShobunHouhouCdTo*/''))/*END*/

/*END*/
) AS MANIFEST
GROUP BY BASE_DATE /*IF dto.GroupColumn != null*/, /*$dto.GroupColumn*/''/*END*/
)

SELECT 
   PIV.* 
  ,CONVERT(MONEY, SUM_TABLE.KANSANGO_SURYO) SUM_KANSANGO_SURYO 
  ,CONVERT(MONEY, ROUND((SUM_TABLE.KANSANGO_SURYO / /*$dto.MonthCount*/0), /*$dto.ManiKetasu*/0)) AVR
FROM
(
SELECT
/*IF dto.GroupColumn != null*/ROW_NUMBER() OVER (ORDER BY /*$dto.GroupColumn*/'' ) ROWNUM/*END*/
/*IF dto.GroupColumn != null*/,/*$dto.GroupColumn*/''/*END*/
/*IF dto.SelectDate != null*/,/*$dto.SelectDate*/''/*END*/
FROM
COMMON_TABLE
 TEMP PIVOT (SUM(KANSANGO_SURYO) FOR BASE_DATE in (/*$dto.Pivot*/'')) PV
) PIV,

(
SELECT
ISNULL(SUM(KANSANGO_SURYO),0) AS KANSANGO_SURYO,
/*IF dto.GroupColumn != null*//*$dto.GroupColumn*/''/*END*/
/*IF dto.GroupColumn != null*/,ROW_NUMBER() OVER (ORDER BY /*$dto.GroupColumn*/'' ) ROWNUM/*END*/
FROM
COMMON_TABLE
 GROUP BY /*IF dto.GroupColumn != null*//*$dto.GroupColumn*/''/*END*/
) SUM_TABLE
WHERE 
  PIV.ROWNUM = SUM_TABLE.ROWNUM 
