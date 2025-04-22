SELECT
    *
FROM (
    /*IF dto.IsKamiMani*/
    SELECT
        TME.FIRST_MANIFEST_KBN AS FIRST_MANIFEST_KBN,
        MHK.HAIKI_KBN_CD AS HAIKI_KBN_CD,                                   -- 廃棄区分CD
        MHK.HAIKI_KBN_NAME_RYAKU AS HAIKI_KBN,                              -- 廃棄区分
        CONVERT(varchar, TME.KOUFU_DATE, 112) AS KOUFU_DATE,                -- 交付年月日
        TME.MANIFEST_ID AS MANIFEST_ID,                                     -- 交付番号
        CONVERT(nvarchar,TME.SYSTEM_ID) AS ID,
        TME.HST_GYOUSHA_CD AS HST_GYOUSHA_CD,                               -- 排出事業者CD
        CASE
            WHEN HST_GYOUSHA.GYOUSHA_NAME_RYAKU IS NOT NULL THEN HST_GYOUSHA.GYOUSHA_NAME_RYAKU
            ELSE TME.HST_GYOUSHA_NAME
        END HST_GYOUSHA_NAME,                                               -- 排出事業者名
        TME.HST_GENBA_CD AS HST_GENBA_CD,                                   -- 排出事業場CD
        CASE
            WHEN HST_GENBA.GENBA_NAME_RYAKU IS NOT NULL THEN HST_GENBA.GENBA_NAME_RYAKU
            ELSE TME.HST_GENBA_NAME
        END HST_GENBA_NAME,                                                 -- 排出事業場名
        MHS.HOUKOKUSHO_BUNRUI_CD AS HOUKOKUSHO_BUNRUI_CD,                   -- 報告書分類CD
        MHB.HOUKOKUSHO_BUNRUI_NAME_RYAKU AS HOUKOKUSHO_BUNRUI,              -- 報告書分類
        TMD.HAIKI_SHURUI_CD AS HAIKI_SHURUI_CD,                             -- 廃棄物種類CD
        MHS.HAIKI_SHURUI_NAME_RYAKU AS HAIKI_SHURUI,                        -- 廃棄物種類
        TMD.HAIKI_NAME_CD AS HAIKI_NAME_CD,                                 -- 廃棄物名称CD
        MHN.HAIKI_NAME_RYAKU AS HAIKI_NAME,                                 -- 廃棄物名称
        TMD.HAIKI_SUU AS HAIKI_SUU,                                         -- 数量,
        TMD.HAIKI_UNIT_CD AS HAIKI_UNIT_CD,                                 -- 単位CD,
        MU.UNIT_NAME_RYAKU AS UNIT_NAME,                                    -- 単位
        TMD.KANSAN_SUU AS KANSAN_SUU,                                       -- 換算後数量
        MSI.MANI_KANSAN_KIHON_UNIT_CD AS KANSAN_UNIT_CD,                    -- 換算後単位CD
        MU2.UNIT_NAME_RYAKU AS KANSAN_UNIT,                                 -- 換算後単位
        TMU1.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD1,                             -- 運搬受託者CD1
        CASE
            WHEN UPN_GYOUSHA1.GYOUSHA_NAME_RYAKU IS NOT NULL THEN UPN_GYOUSHA1.GYOUSHA_NAME_RYAKU
            ELSE TMU1.UPN_JYUTAKUSHA_NAME
        END UPN_JYUTAKUSHA_NAME1,                                           -- 運搬受託者名1
        TMU1.UNTENSHA_NAME AS UNTENSHA_NAME1,                               -- 運転者1
        TMU2.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD2,                             -- 運搬受託者CD2
        CASE
            WHEN UPN_GYOUSHA2.GYOUSHA_NAME_RYAKU IS NOT NULL THEN UPN_GYOUSHA2.GYOUSHA_NAME_RYAKU
            ELSE TMU2.UPN_JYUTAKUSHA_NAME
        END UPN_JYUTAKUSHA_NAME2,                                           -- 運搬受託者名2
        TMU2.UNTENSHA_NAME AS UNTENSHA_NAME2,                               -- 運転者2
        TMU3.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD3,                             -- 運搬受託者CD3
        CASE
            WHEN UPN_GYOUSHA3.GYOUSHA_NAME_RYAKU IS NOT NULL THEN UPN_GYOUSHA3.GYOUSHA_NAME_RYAKU
            ELSE TMU3.UPN_JYUTAKUSHA_NAME
        END UPN_JYUTAKUSHA_NAME3,                                           -- 運搬受託者名3
        TMU3.UNTENSHA_NAME AS UNTENSHA_NAME3,                               -- 運転者3
        TMU4.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD4,                             -- 運搬受託者CD4
        CASE
            WHEN UPN_GYOUSHA4.GYOUSHA_NAME_RYAKU IS NOT NULL THEN UPN_GYOUSHA4.GYOUSHA_NAME_RYAKU
            ELSE TMU4.UPN_JYUTAKUSHA_NAME
        END UPN_JYUTAKUSHA_NAME4,                                           -- 運搬受託者名4
        TMU4.UNTENSHA_NAME AS UNTENSHA_NAME4,                               -- 運転者4
        TMU5.UPN_GYOUSHA_CD AS UPN_GYOUSHA_CD5,                             -- 運搬受託者CD5
        CASE
            WHEN UPN_GYOUSHA5.GYOUSHA_NAME_RYAKU IS NOT NULL THEN UPN_GYOUSHA5.GYOUSHA_NAME_RYAKU
            ELSE TMU5.UPN_JYUTAKUSHA_NAME
        END UPN_JYUTAKUSHA_NAME5,                                           -- 運搬受託者名5
        TMU5.UNTENSHA_NAME AS UNTENSHA_NAME5,                               -- 運転者5
        TMD.SBN_HOUHOU_CD AS SBN_HOUHOU_CD,                                 -- 処分方法CD
        MSH.SHOBUN_HOUHOU_NAME_RYAKU AS SBN_HOUHOU,                         -- 処分方法
        CASE
            WHEN TMU5.UPN_END_DATE IS NOT NULL THEN CONVERT(varchar, TMU5.UPN_END_DATE, 112)
            WHEN TMU4.UPN_END_DATE IS NOT NULL THEN CONVERT(varchar, TMU4.UPN_END_DATE, 112)
            WHEN TMU3.UPN_END_DATE IS NOT NULL THEN CONVERT(varchar, TMU3.UPN_END_DATE, 112)
            WHEN TMU2.UPN_END_DATE IS NOT NULL THEN CONVERT(varchar, TMU2.UPN_END_DATE, 112)
            ELSE CONVERT(varchar, TMU1.UPN_END_DATE, 112)
        END UPN_END_DATE,                                                   -- 運搬終了年月日
        TME.SBN_GYOUSHA_CD AS SBN_GYOUSHA_CD,                               -- 処分事業者CD
        CASE
            WHEN SBN_GYOUSHA.GYOUSHA_NAME_RYAKU IS NOT NULL THEN SBN_GYOUSHA.GYOUSHA_NAME_RYAKU
            ELSE TME.SBN_GYOUSHA_NAME
        END SBN_GYOUSHA,                                                    -- 処分事業者
        CASE
            WHEN TMU_EX1.UPN_SAKI_GENBA_CD IS NOT NULL AND TMU_EX1.UPN_SAKI_GENBA_CD <> ''
				THEN TMU_EX1.UPN_SAKI_GENBA_CD
            WHEN TMU_EX2.UPN_SAKI_GENBA_CD IS NOT NULL AND TMU_EX2.UPN_SAKI_GENBA_CD <> ''
				THEN TMU_EX2.UPN_SAKI_GENBA_CD
            WHEN TMU_EX3.UPN_SAKI_GENBA_CD IS NOT NULL AND TMU_EX3.UPN_SAKI_GENBA_CD <> ''
				THEN TMU_EX3.UPN_SAKI_GENBA_CD
            WHEN TMU_EX4.UPN_SAKI_GENBA_CD IS NOT NULL AND TMU_EX4.UPN_SAKI_GENBA_CD <> ''
				THEN TMU_EX4.UPN_SAKI_GENBA_CD
            ELSE TMU_EX5.UPN_SAKI_GENBA_CD
        END UPN_SAKI_GENBA_CD,                                              -- 処分事業場CD
        CASE
            WHEN UPN_GENBA1.GENBA_NAME_RYAKU IS NOT NULL AND UPN_GENBA1.GENBA_NAME_RYAKU <> ''
				THEN UPN_GENBA1.GENBA_NAME_RYAKU
            WHEN TMU_EX1.UPN_SAKI_GENBA_NAME IS NOT NULL AND TMU_EX1.UPN_SAKI_GENBA_NAME <> ''
				THEN TMU_EX1.UPN_SAKI_GENBA_NAME
            WHEN UPN_GENBA2.GENBA_NAME_RYAKU IS NOT NULL AND UPN_GENBA2.GENBA_NAME_RYAKU <> ''
				THEN UPN_GENBA2.GENBA_NAME_RYAKU
            WHEN TMU_EX2.UPN_SAKI_GENBA_NAME IS NOT NULL AND TMU_EX2.UPN_SAKI_GENBA_NAME <> ''
				THEN TMU_EX2.UPN_SAKI_GENBA_NAME
            WHEN UPN_GENBA3.GENBA_NAME_RYAKU IS NOT NULL AND UPN_GENBA3.GENBA_NAME_RYAKU <> ''
				THEN UPN_GENBA3.GENBA_NAME_RYAKU
            WHEN TMU_EX3.UPN_SAKI_GENBA_NAME IS NOT NULL AND TMU_EX3.UPN_SAKI_GENBA_NAME <> ''
				THEN TMU_EX3.UPN_SAKI_GENBA_NAME
            WHEN UPN_GENBA4.GENBA_NAME_RYAKU IS NOT NULL AND UPN_GENBA4.GENBA_NAME_RYAKU <> ''
				THEN UPN_GENBA4.GENBA_NAME_RYAKU
            WHEN TMU_EX4.UPN_SAKI_GENBA_NAME IS NOT NULL AND TMU_EX4.UPN_SAKI_GENBA_NAME <> ''
				THEN TMU_EX4.UPN_SAKI_GENBA_NAME
            WHEN UPN_GENBA5.GENBA_NAME_RYAKU IS NOT NULL AND UPN_GENBA5.GENBA_NAME_RYAKU <> ''
				THEN UPN_GENBA5.GENBA_NAME_RYAKU
            ELSE TMU_EX5.UPN_SAKI_GENBA_NAME
        END UPN_SAKI_GENBA_NAME,                                            -- 処分事業場名
        CONVERT(varchar, TMD.SBN_END_DATE, 112) AS SBN_END_DATE,            -- 処分終了年月日
        TMD.LAST_SBN_GYOUSHA_CD AS LAST_SBN_GYOUSHA_CD,                     -- 最終処分受託者
        TMD.LAST_SBN_GENBA_CD AS LAST_SBN_GENBA_CD,                         -- 最終処分場所CD
        LAST_SBN_GENBA.GENBA_NAME_RYAKU AS LAST_SBN_GENBA_NAME,             -- 最終処分場所名
        CONVERT(varchar, TMD.LAST_SBN_END_DATE, 112) AS LAST_SBN_END_DATE,  -- 最終処分終了年月日
        HIMOZUKE_MANIFEST.MANIFEST_ID AS HIMOZUKE_MANIFEST_ID,              -- 2次交付番号
        TMD.GENNYOU_SUU AS GENNYOU_SUU,                                     -- 減容後数量
        TME.TOTAL_SUU AS SUURYOU_NO_GOUKEI,                                 -- 数量の合計
        CASE WHEN TME.HAIKI_KBN_CD = 2 THEN
            CASE WHEN TMD.HAIKI_SHURUI_CD >= 1100 THEN '管理型品目'
            ELSE '安定型品目'
            END
        ELSE
            CASE WHEN TMD.HAIKI_SHURUI_CD >= 7000 THEN '特別管理産業廃棄物'
            ELSE '普通の産業廃棄物'
            END
        END AS HAIKIBUTU_KUBUN                                              -- 廃棄物区分
    FROM T_MANIFEST_ENTRY AS TME
    JOIN T_MANIFEST_DETAIL AS TMD ON TME.SYSTEM_ID = TMD.SYSTEM_ID AND TME.SEQ = TMD.SEQ
    LEFT JOIN M_SYS_INFO AS MSI ON 1 = 1
    LEFT JOIN M_GYOUSHA AS HST_GYOUSHA ON TME.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD
    LEFT JOIN M_GENBA AS HST_GENBA ON TME.HST_GYOUSHA_CD = HST_GENBA.GYOUSHA_CD AND TME.HST_GENBA_CD = HST_GENBA.GENBA_CD
    LEFT JOIN M_GYOUSHA AS SBN_GYOUSHA ON TME.SBN_GYOUSHA_CD = SBN_GYOUSHA.GYOUSHA_CD
    LEFT JOIN M_HAIKI_KBN AS MHK ON TME.HAIKI_KBN_CD = MHK.HAIKI_KBN_CD
    LEFT JOIN M_HAIKI_SHURUI AS MHS ON TME.HAIKI_KBN_CD = MHS.HAIKI_KBN_CD AND TMD.HAIKI_SHURUI_CD = MHS.HAIKI_SHURUI_CD
    LEFT JOIN M_HOUKOKUSHO_BUNRUI AS MHB ON MHS.HOUKOKUSHO_BUNRUI_CD = MHB.HOUKOKUSHO_BUNRUI_CD
    LEFT JOIN M_HAIKI_NAME AS MHN ON TMD.HAIKI_NAME_CD = MHN.HAIKI_NAME_CD
    LEFT JOIN M_UNIT AS MU ON TMD.HAIKI_UNIT_CD = MU.UNIT_CD
    LEFT JOIN M_UNIT AS MU2 ON MSI.MANI_KANSAN_KIHON_UNIT_CD = MU2.UNIT_CD
    LEFT JOIN M_SHOBUN_HOUHOU AS MSH ON TMD.SBN_HOUHOU_CD = MSH.SHOBUN_HOUHOU_CD
    LEFT JOIN T_MANIFEST_UPN AS TMU1 ON TME.SYSTEM_ID = TMU1.SYSTEM_ID AND TME.SEQ = TMU1.SEQ AND TMU1.UPN_ROUTE_NO = 1
    LEFT JOIN M_GYOUSHA AS UPN_GYOUSHA1 ON TMU1.UPN_GYOUSHA_CD = UPN_GYOUSHA1.GYOUSHA_CD
    LEFT JOIN T_MANIFEST_UPN AS TMU2 ON TME.SYSTEM_ID = TMU2.SYSTEM_ID AND TME.SEQ = TMU2.SEQ AND TMU2.UPN_ROUTE_NO = 2
    LEFT JOIN M_GYOUSHA AS UPN_GYOUSHA2 ON TMU2.UPN_GYOUSHA_CD = UPN_GYOUSHA2.GYOUSHA_CD
    LEFT JOIN T_MANIFEST_UPN AS TMU3 ON TME.SYSTEM_ID = TMU3.SYSTEM_ID AND TME.SEQ = TMU3.SEQ AND TMU3.UPN_ROUTE_NO = 3
    LEFT JOIN M_GYOUSHA AS UPN_GYOUSHA3 ON TMU3.UPN_GYOUSHA_CD = UPN_GYOUSHA3.GYOUSHA_CD
    LEFT JOIN T_MANIFEST_UPN AS TMU4 ON TME.SYSTEM_ID = TMU4.SYSTEM_ID AND TME.SEQ = TMU4.SEQ AND TMU4.UPN_ROUTE_NO = 4
    LEFT JOIN M_GYOUSHA AS UPN_GYOUSHA4 ON TMU4.UPN_GYOUSHA_CD = UPN_GYOUSHA4.GYOUSHA_CD
    LEFT JOIN T_MANIFEST_UPN AS TMU5 ON TME.SYSTEM_ID = TMU5.SYSTEM_ID AND TME.SEQ = TMU5.SEQ AND TMU5.UPN_ROUTE_NO = 5
    LEFT JOIN M_GYOUSHA AS UPN_GYOUSHA5 ON TMU5.UPN_GYOUSHA_CD = UPN_GYOUSHA5.GYOUSHA_CD
    LEFT JOIN M_GENBA AS LAST_SBN_GENBA ON TMD.LAST_SBN_GYOUSHA_CD = LAST_SBN_GENBA.GYOUSHA_CD AND TMD.LAST_SBN_GENBA_CD = LAST_SBN_GENBA.GENBA_CD
    LEFT JOIN T_MANIFEST_UPN AS TMU_EX1 ON TME.SYSTEM_ID = TMU_EX1.SYSTEM_ID AND TME.SEQ = TMU_EX1.SEQ AND TMU_EX1.UPN_ROUTE_NO = 1 AND (TMU_EX1.UPN_SAKI_KBN = 1 OR (TME.HAIKI_KBN_CD = '2' AND TMU_EX1.UPN_SAKI_KBN IS NULL)) 
    LEFT JOIN M_GENBA AS UPN_GENBA1 ON  TMU_EX1.UPN_SAKI_GYOUSHA_CD = UPN_GENBA1.GYOUSHA_CD AND TMU_EX1.UPN_SAKI_GENBA_CD = UPN_GENBA1.GENBA_CD
	LEFT JOIN T_MANIFEST_UPN AS TMU_EX2 ON TME.SYSTEM_ID = TMU_EX2.SYSTEM_ID AND TME.SEQ = TMU_EX2.SEQ AND TMU_EX2.UPN_ROUTE_NO = 2 AND (TMU_EX2.UPN_SAKI_KBN = 1 OR (TME.HAIKI_KBN_CD = '2' AND TMU_EX2.UPN_SAKI_KBN IS NULL)) 
    LEFT JOIN M_GENBA AS UPN_GENBA2 ON  TMU_EX2.UPN_SAKI_GYOUSHA_CD = UPN_GENBA2.GYOUSHA_CD AND TMU_EX2.UPN_SAKI_GENBA_CD = UPN_GENBA2.GENBA_CD
	LEFT JOIN T_MANIFEST_UPN AS TMU_EX3 ON TME.SYSTEM_ID = TMU_EX3.SYSTEM_ID AND TME.SEQ = TMU_EX3.SEQ AND TMU_EX3.UPN_ROUTE_NO = 3 AND (TMU_EX3.UPN_SAKI_KBN = 1 OR (TME.HAIKI_KBN_CD = '2' AND TMU_EX3.UPN_SAKI_KBN IS NULL)) 
    LEFT JOIN M_GENBA AS UPN_GENBA3 ON  TMU_EX3.UPN_SAKI_GYOUSHA_CD = UPN_GENBA3.GYOUSHA_CD AND TMU_EX3.UPN_SAKI_GENBA_CD = UPN_GENBA3.GENBA_CD
	LEFT JOIN T_MANIFEST_UPN AS TMU_EX4 ON TME.SYSTEM_ID = TMU_EX4.SYSTEM_ID AND TME.SEQ = TMU_EX4.SEQ AND TMU_EX4.UPN_ROUTE_NO = 4 AND (TMU_EX4.UPN_SAKI_KBN = 1 OR (TME.HAIKI_KBN_CD = '2' AND TMU_EX4.UPN_SAKI_KBN IS NULL)) 
    LEFT JOIN M_GENBA AS UPN_GENBA4 ON  TMU_EX4.UPN_SAKI_GYOUSHA_CD = UPN_GENBA4.GYOUSHA_CD AND TMU_EX4.UPN_SAKI_GENBA_CD = UPN_GENBA4.GENBA_CD
	LEFT JOIN T_MANIFEST_UPN AS TMU_EX5 ON TME.SYSTEM_ID = TMU_EX5.SYSTEM_ID AND TME.SEQ = TMU_EX5.SEQ AND TMU_EX5.UPN_ROUTE_NO = 5 AND (TMU_EX5.UPN_SAKI_KBN = 1 OR (TME.HAIKI_KBN_CD = '2' AND TMU_EX5.UPN_SAKI_KBN IS NULL)) 
    LEFT JOIN M_GENBA AS UPN_GENBA5 ON  TMU_EX5.UPN_SAKI_GYOUSHA_CD = UPN_GENBA5.GYOUSHA_CD AND TMU_EX5.UPN_SAKI_GENBA_CD = UPN_GENBA5.GENBA_CD
    /*IF dto.IchijiKbn == 1*/
    LEFT JOIN
    (
        SELECT DISTINCT
            MANIFEST_R.FIRST_SYSTEM_ID,
            MANIFEST_R.FIRST_HAIKI_KBN_CD,
            CASE WHEN MANIFEST_2.MANIFEST_ID IS NOT NULL AND MANIFEST_2.MANIFEST_ID <> ''
                THEN MANIFEST_2.MANIFEST_ID
                ELSE ELEC_MANI.MANIFEST_ID
            END AS MANIFEST_ID
        FROM T_MANIFEST_RELATION AS MANIFEST_R
            LEFT JOIN
            (
                SELECT
                    TME.SYSTEM_ID,
                    TME.SEQ,
                    TME.MANIFEST_ID,
					TMD.DETAIL_SYSTEM_ID
                FROM T_MANIFEST_ENTRY AS TME
                    JOIN T_MANIFEST_DETAIL AS TMD ON TME.SYSTEM_ID = TMD.SYSTEM_ID AND TME.SEQ = TMD.SEQ
                WHERE TME.DELETE_FLG = 0
            ) AS MANIFEST_2 ON MANIFEST_R.NEXT_SYSTEM_ID = MANIFEST_2.DETAIL_SYSTEM_ID AND MANIFEST_R.NEXT_HAIKI_KBN_CD <> 4
            LEFT JOIN 
            (
                SELECT
                    DR18EX.SYSTEM_ID,
                    DR18EX.SEQ,
                    DR18.MANIFEST_ID
                FROM
                    DT_MF_TOC AS TOC
                    INNER JOIN DT_R18 AS DR18
                        ON TOC.KANRI_ID = DR18.KANRI_ID AND TOC.LATEST_SEQ = DR18.SEQ
                    JOIN DT_R18_EX AS DR18EX ON DR18.KANRI_ID = DR18EX.KANRI_ID AND DR18EX.DELETE_FLG = 0
            ) AS ELEC_MANI
                ON MANIFEST_R.NEXT_SYSTEM_ID = ELEC_MANI.SYSTEM_ID AND MANIFEST_R.NEXT_HAIKI_KBN_CD = 4
        WHERE MANIFEST_R.DELETE_FLG = 0
    ) AS HIMOZUKE_MANIFEST ON TMD.DETAIL_SYSTEM_ID = HIMOZUKE_MANIFEST.FIRST_SYSTEM_ID AND HIMOZUKE_MANIFEST.FIRST_HAIKI_KBN_CD <> 4
    /*END*/
    /*IF dto.IchijiKbn == 2*/
    LEFT JOIN
    (
        SELECT DISTINCT NEXT_SYSTEM_ID AS MANIFEST_ID,
						NEXT_HAIKI_KBN_CD
        FROM T_MANIFEST_RELATION
        WHERE DELETE_FLG = 0 AND NEXT_HAIKI_KBN_CD <> 4
	) AS HIMOZUKE_MANIFEST ON TMD.DETAIL_SYSTEM_ID = HIMOZUKE_MANIFEST.MANIFEST_ID AND HIMOZUKE_MANIFEST.NEXT_HAIKI_KBN_CD <> 4

    /*END*/

    WHERE TME.DELETE_FLG = 0

    /*IF dto.KyotenCd != 99*/AND TME.KYOTEN_CD = /*dto.KyotenCd*/99/*END*/

    /*IF dto.DateFrom != null && dto.DateFrom != ''*/AND CONVERT(varchar, TME.UPDATE_DATE, 112) >= /*dto.DateFrom*/''/*END*/
    /*IF dto.DateTo != null && dto.DateTo != ''*/AND CONVERT(varchar, TME.UPDATE_DATE, 112) <= /*dto.DateTo*/''/*END*/

    /*IF dto.IchijiKbn == 1*/AND TME.FIRST_MANIFEST_KBN = 0/*END*/
    /*IF dto.IchijiKbn == 2*/AND TME.FIRST_MANIFEST_KBN = 1/*END*/

    /*IF dto.NijiHimozuke == 2*/AND HIMOZUKE_MANIFEST.MANIFEST_ID IS NOT NULL/*END*/
    /*IF dto.NijiHimozuke == 3*/AND HIMOZUKE_MANIFEST.MANIFEST_ID IS NULL/*END*/

    /*IF dto.KofuDateFrom != null && dto.KofuDateFrom != ''*/AND CONVERT(varchar, TME.KOUFU_DATE, 112) >= /*dto.KofuDateFrom*/''/*END*/
    /*IF dto.KofuDateTo != null && dto.KofuDateTo != ''*/AND CONVERT(varchar, TME.KOUFU_DATE, 112) <= /*dto.KofuDateTo*/''/*END*/

    AND ((1=1
	  /*IF dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != ''*/
      AND CONVERT(varchar, TMU1.UPN_END_DATE, 112) >= /*dto.UnpanEndDateFrom*/''/*END*/
	  /*IF dto.UnpanEndDateTo != null && dto.UnpanEndDateTo != ''*/
	  AND CONVERT(varchar, TMU1.UPN_END_DATE, 112) <= /*dto.UnpanEndDateTo*/''/*END*/)
	  OR(1=1
	  /*IF dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != ''*/
      AND CONVERT(varchar, TMU2.UPN_END_DATE, 112) >= /*dto.UnpanEndDateFrom*/''/*END*/
	  /*IF dto.UnpanEndDateTo != null && dto.UnpanEndDateTo != ''*/
	  AND CONVERT(varchar, TMU2.UPN_END_DATE, 112) <= /*dto.UnpanEndDateTo*/''/*END*/)
	  OR(1=1
	  /*IF dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != ''*/
      AND CONVERT(varchar, TMU3.UPN_END_DATE, 112) >= /*dto.UnpanEndDateFrom*/''/*END*/
	  /*IF dto.UnpanEndDateTo != null && dto.UnpanEndDateTo != ''*/
	  AND CONVERT(varchar, TMU3.UPN_END_DATE, 112) <= /*dto.UnpanEndDateTo*/''/*END*/)
	  OR(1=1
	  /*IF dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != ''*/
      AND CONVERT(varchar, TMU4.UPN_END_DATE, 112) >= /*dto.UnpanEndDateFrom*/''/*END*/
	  /*IF dto.UnpanEndDateTo != null && dto.UnpanEndDateTo != ''*/
	  AND CONVERT(varchar, TMU4.UPN_END_DATE, 112) <= /*dto.UnpanEndDateTo*/''/*END*/)
	  OR(1=1
	  /*IF dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != ''*/
      AND CONVERT(varchar, TMU5.UPN_END_DATE, 112) >= /*dto.UnpanEndDateFrom*/''/*END*/
	  /*IF dto.UnpanEndDateTo != null && dto.UnpanEndDateTo != ''*/
	  AND CONVERT(varchar, TMU5.UPN_END_DATE, 112) <= /*dto.UnpanEndDateTo*/''/*END*/)
    )

    /*IF dto.ShobunEndDateFrom != null && dto.ShobunEndDateFrom != ''*/AND CONVERT(varchar, TMD.SBN_END_DATE, 112) >= /*dto.ShobunEndDateFrom*/''/*END*/
    /*IF dto.ShobunEndDateTo != null && dto.ShobunEndDateTo != ''*/AND CONVERT(varchar, TMD.SBN_END_DATE, 112) <= /*dto.ShobunEndDateTo*/''/*END*/

    /*IF dto.LastShobunEndDateFrom != null && dto.LastShobunEndDateFrom != ''*/AND CONVERT(varchar, TMD.LAST_SBN_END_DATE, 112) >= /*dto.LastShobunEndDateFrom*/''/*END*/
    /*IF dto.LastShobunEndDateTo != null && dto.LastShobunEndDateTo != ''*/AND CONVERT(varchar, TMD.LAST_SBN_END_DATE, 112) <= /*dto.LastShobunEndDateTo*/''/*END*/

    /*IF dto.HaishutsuJigyoushaCdFrom != null && dto.HaishutsuJigyoushaCdFrom != ''*/AND TME.HST_GYOUSHA_CD >= /*dto.HaishutsuJigyoushaCdFrom*/''/*END*/
    /*IF dto.HaishutsuJigyoushaCdTo != null && dto.HaishutsuJigyoushaCdTo != ''*/AND TME.HST_GYOUSHA_CD <= /*dto.HaishutsuJigyoushaCdTo*/''/*END*/

    /*IF dto.HaishutsuJigyoujouCdFrom != null && dto.HaishutsuJigyoujouCdFrom != ''*/AND TME.HST_GENBA_CD >= /*dto.HaishutsuJigyoujouCdFrom*/''/*END*/
    /*IF dto.HaishutsuJigyoujouCdTo != null && dto.HaishutsuJigyoujouCdTo != ''*/AND TME.HST_GENBA_CD <= /*dto.HaishutsuJigyoujouCdTo*/''/*END*/

    AND ((1=1
	 /*IF dto.UnpanJutakushaCdFrom != null && dto.UnpanJutakushaCdFrom != ''*/
      AND TMU1.UPN_GYOUSHA_CD >= /*dto.UnpanJutakushaCdFrom*/''/*END*/
	  /*IF dto.UnpanJutakushaCdTo != null && dto.UnpanJutakushaCdTo != ''*/
	  AND TMU1.UPN_GYOUSHA_CD <= /*dto.UnpanJutakushaCdTo*/''/*END*/)
	  OR(1=1
	   /*IF dto.UnpanJutakushaCdFrom != null && dto.UnpanJutakushaCdFrom != ''*/
      AND TMU2.UPN_GYOUSHA_CD >= /*dto.UnpanJutakushaCdFrom*/''/*END*/
	  /*IF dto.UnpanJutakushaCdTo != null && dto.UnpanJutakushaCdTo != ''*/
	  AND TMU2.UPN_GYOUSHA_CD <= /*dto.UnpanJutakushaCdTo*/''/*END*/)
	  OR(1=1
	   /*IF dto.UnpanJutakushaCdFrom != null && dto.UnpanJutakushaCdFrom != ''*/
      AND TMU3.UPN_GYOUSHA_CD >= /*dto.UnpanJutakushaCdFrom*/''/*END*/
	  /*IF dto.UnpanJutakushaCdTo != null && dto.UnpanJutakushaCdTo != ''*/
	  AND TMU3.UPN_GYOUSHA_CD <= /*dto.UnpanJutakushaCdTo*/''/*END*/)
	  OR(1=1
	   /*IF dto.UnpanJutakushaCdFrom != null && dto.UnpanJutakushaCdFrom != ''*/
      AND TMU4.UPN_GYOUSHA_CD >= /*dto.UnpanJutakushaCdFrom*/''/*END*/
	  /*IF dto.UnpanJutakushaCdTo != null && dto.UnpanJutakushaCdTo != ''*/
	  AND TMU4.UPN_GYOUSHA_CD <= /*dto.UnpanJutakushaCdTo*/''/*END*/)
	  OR(1=1
	   /*IF dto.UnpanJutakushaCdFrom != null && dto.UnpanJutakushaCdFrom != ''*/
      AND TMU5.UPN_GYOUSHA_CD >= /*dto.UnpanJutakushaCdFrom*/''/*END*/
	  /*IF dto.UnpanJutakushaCdTo != null && dto.UnpanJutakushaCdTo != ''*/
	  AND TMU5.UPN_GYOUSHA_CD <= /*dto.UnpanJutakushaCdTo*/''/*END*/)
    )

    /*IF dto.ShobunJigyoushaCdFrom != null && dto.ShobunJigyoushaCdFrom != ''*/AND TME.SBN_GYOUSHA_CD >= /*dto.ShobunJigyoushaCdFrom*/''/*END*/
    /*IF dto.ShobunJigyoushaCdTo != null && dto.ShobunJigyoushaCdTo != ''*/AND TME.SBN_GYOUSHA_CD <= /*dto.ShobunJigyoushaCdTo*/''/*END*/

    AND ((1=1
	  /*IF dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != ''*/
      AND TMU1.UPN_SAKI_GENBA_CD >= /*dto.ShobunJigyoujouCdFrom*/'' AND (TME.HAIKI_KBN_CD IN (1,2) OR (TME.HAIKI_KBN_CD = 3 AND TMU1.UPN_SAKI_KBN = 1))/*END*/
	  /*IF dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != ''*/
	  AND TMU1.UPN_SAKI_GENBA_CD <= /*dto.ShobunJigyoujouCdTo*/'' AND (TME.HAIKI_KBN_CD IN (1,2) OR (TME.HAIKI_KBN_CD = 3 AND TMU1.UPN_SAKI_KBN = 1))/*END*/)
	  OR(1=1
      /*IF dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != ''*/
      AND TMU2.UPN_SAKI_GENBA_CD >= /*dto.ShobunJigyoujouCdFrom*/'' AND (TME.HAIKI_KBN_CD IN (1,2) OR (TME.HAIKI_KBN_CD = 3 AND TMU2.UPN_SAKI_KBN = 1))/*END*/
	  /*IF dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != ''*/
	  AND TMU2.UPN_SAKI_GENBA_CD <= /*dto.ShobunJigyoujouCdTo*/'' AND (TME.HAIKI_KBN_CD IN (1,2) OR (TME.HAIKI_KBN_CD = 3 AND TMU2.UPN_SAKI_KBN = 1))/*END*/)
	  OR(1=1
      /*IF dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != ''*/
      AND TMU3.UPN_SAKI_GENBA_CD >= /*dto.ShobunJigyoujouCdFrom*/'' AND (TME.HAIKI_KBN_CD IN (1,2) OR (TME.HAIKI_KBN_CD = 3 AND TMU3.UPN_SAKI_KBN = 1))/*END*/
	  /*IF dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != ''*/
	  AND TMU3.UPN_SAKI_GENBA_CD <= /*dto.ShobunJigyoujouCdTo*/'' AND (TME.HAIKI_KBN_CD IN (1,2) OR (TME.HAIKI_KBN_CD = 3 AND TMU3.UPN_SAKI_KBN = 1))/*END*/)
	  OR(1=1
      /*IF dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != ''*/
      AND TMU4.UPN_SAKI_GENBA_CD >= /*dto.ShobunJigyoujouCdFrom*/'' AND (TME.HAIKI_KBN_CD IN (1,2) OR (TME.HAIKI_KBN_CD = 3 AND TMU4.UPN_SAKI_KBN = 1))/*END*/
	  /*IF dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != ''*/
	  AND TMU4.UPN_SAKI_GENBA_CD <= /*dto.ShobunJigyoujouCdTo*/'' AND (TME.HAIKI_KBN_CD IN (1,2) OR (TME.HAIKI_KBN_CD = 3 AND TMU4.UPN_SAKI_KBN = 1))/*END*/)
	  OR(1=1
      /*IF dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != ''*/
      AND TMU5.UPN_SAKI_GENBA_CD >= /*dto.ShobunJigyoujouCdFrom*/'' AND (TME.HAIKI_KBN_CD IN (1,2) OR (TME.HAIKI_KBN_CD = 3 AND TMU5.UPN_SAKI_KBN = 1))/*END*/
	  /*IF dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != ''*/
	  AND TMU5.UPN_SAKI_GENBA_CD <= /*dto.ShobunJigyoujouCdTo*/'' AND (TME.HAIKI_KBN_CD IN (1,2) OR (TME.HAIKI_KBN_CD = 3 AND TMU5.UPN_SAKI_KBN = 1))/*END*/)
    )

    /*IF dto.LastShobunJigyoushaCdFrom != null && dto.LastShobunJigyoushaCdFrom != ''*/AND TMD.LAST_SBN_GYOUSHA_CD >= /*dto.LastShobunJigyoushaCdFrom*/''/*END*/
    /*IF dto.LastShobunJigyoushaCdTo != null && dto.LastShobunJigyoushaCdTo != ''*/AND TMD.LAST_SBN_GYOUSHA_CD <= /*dto.LastShobunJigyoushaCdTo*/''/*END*/

    /*IF dto.LastShobunJigyoujouCdFrom != null && dto.LastShobunJigyoujouCdFrom != ''*/AND TME.LAST_SBN_GENBA_CD >= /*dto.LastShobunJigyoujouCdTo*/''/*END*/
    /*IF dto.LastShobunJigyoujouCdTo != null && dto.LastShobunJigyoujouCdTo != ''*/AND TME.LAST_SBN_GENBA_CD <= /*dto.LastShobunJigyoujouCdTo*/''/*END*/

    /*IF dto.HoukokushoBunruiCdFrom != null && dto.HoukokushoBunruiCdFrom != ''*/AND MHS.HOUKOKUSHO_BUNRUI_CD >= /*dto.HoukokushoBunruiCdFrom*/''/*END*/
    /*IF dto.HoukokushoBunruiCdTo != null && dto.HoukokushoBunruiCdTo != ''*/AND MHS.HOUKOKUSHO_BUNRUI_CD <= /*dto.HoukokushoBunruiCdTo*/''/*END*/

    /*IF dto.HaikibutsuMeishouCdFrom != null && dto.HaikibutsuMeishouCdFrom != ''*/AND TMD.HAIKI_NAME_CD >= /*dto.HaikibutsuMeishouCdFrom*/''/*END*/
    /*IF dto.HaikibutsuMeishouCdTo != null && dto.HaikibutsuMeishouCdTo != ''*/AND TMD.HAIKI_NAME_CD <= /*dto.HaikibutsuMeishouCdTo*/''/*END*/

    /*IF dto.NisugataCdFrom != null && dto.NisugataCdFrom != ''*/AND TMD.NISUGATA_CD >= /*dto.NisugataCdFrom*/''/*END*/
    /*IF dto.NisugataCdTo != null && dto.NisugataCdTo != ''*/AND TMD.NISUGATA_CD <= /*dto.NisugataCdTo*/''/*END*/

    /*IF dto.ShobunHouhouCdFrom != null && dto.ShobunHouhouCdFrom != ''*/AND TMD.SBN_HOUHOU_CD >= /*dto.ShobunHouhouCdFrom*/''/*END*/
    /*IF dto.ShobunHouhouCdTo != null && dto.ShobunHouhouCdTo != ''*/AND TMD.SBN_HOUHOU_CD <= /*dto.ShobunHouhouCdTo*/''/*END*/

    /*IF dto.TorihikisakiCdFrom != null && dto.TorihikisakiCdFrom != ''*/AND TME.TORIHIKISAKI_CD >= /*dto.TorihikisakiCdFrom*/''/*END*/
    /*IF dto.TorihikisakiCdTo != null && dto.TorihikisakiCdTo != ''*/AND TME.TORIHIKISAKI_CD <= /*dto.TorihikisakiCdTo*/''/*END*/
    /*END*/

    /*IF dto.IsKamiMani && dto.IsDenMani*/
    UNION ALL
    /*END*/

    /*IF dto.IsDenMani*/
    SELECT
        DR18.FIRST_MANIFEST_FLAG AS FIRST_MANIFEST_KBN,
        4 AS HAIKI_KBN_CD,                                                  -- 廃棄区分CD
        '電子' AS HAIKI_KBN,                                                -- 廃棄区分
        DR18.HIKIWATASHI_DATE AS KOUFU_DATE,                                -- 交付年月日
        DR18.MANIFEST_ID AS MANIFEST_ID,                                    -- 交付番号
        DMT.KANRI_ID AS ID,
        HST_JIGYOUSHA.GYOUSHA_CD AS HST_GYOUSHA_CD,                         -- 排出事業者CD
        HST_JIGYOUSHA.GYOUSHA_NAME_RYAKU AS HST_GYOUSHA_NAME,               -- 排出事業者名
        HST_JIGYOUJOU.GENBA_CD AS HST_GENBA_CD,                             -- 排出事業場CD
        HST_JIGYOUJOU.GENBA_NAME_RYAKU AS HST_GENBA_NAME,                   -- 排出事業場名
        CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN MDHS_MIX.HOUKOKUSHO_BUNRUI_CD
            ELSE MDHS.HOUKOKUSHO_BUNRUI_CD
        END AS HOUKOKUSHO_BUNRUI_CD,                                        -- 報告書分類CD
        CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN MHB_MIX.HOUKOKUSHO_BUNRUI_CD
            ELSE MHB.HOUKOKUSHO_BUNRUI_NAME_RYAKU
        END AS HOUKOKUSHO_BUNRUI,                                           -- 報告書分類
        CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN MDHS_MIX.HAIKI_SHURUI_CD
            ELSE MDHS.HAIKI_SHURUI_CD
        END AS HAIKI_SHURUI_CD,                                             -- 廃棄物種類CD
        CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN MDHS_MIX.HAIKI_SHURUI_NAME
            ELSE MDHS.HAIKI_SHURUI_NAME
        END AS HAIKI_SHURUI,                                                -- 廃棄物種類
        CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN DR18MIX.HAIKI_NAME_CD
            ELSE DR18EX.HAIKI_NAME_CD
        END AS HAIKI_NAME_CD,                                               -- 廃棄物名称CD
        CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN MHN_MIX.HAIKI_NAME
            ELSE DR18.HAIKI_NAME
        END AS HAIKI_NAME,                                                  -- 廃棄物名称
        CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN DR18MIX.HAIKI_SUU
            ELSE DR18.HAIKI_SUU
        END AS HAIKI_SUU,                                                   -- 数量
        CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN DR18MIX.HAIKI_UNIT_CD
            ELSE DR18.HAIKI_UNIT_CODE
        END AS HAIKI_UNIT_CD,                                               -- 単位CD
        CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN MIX_MU.UNIT_NAME_RYAKU
            ELSE MU.UNIT_NAME_RYAKU
        END AS UNIT_NAME,                                                   -- 単位
        DR18EX.KANSAN_SUU AS KANSAN_SUU,                                    -- 換算後数量
        MU2.UNIT_CD AS KANSAN_UNIT_CD,                                      -- 換算後単位CD
        MU2.UNIT_NAME_RYAKU AS KANSAN_UNIT,                                 -- 換算後単位
        CASE
            WHEN UPN_1.GYOUSHA_CD IS NOT NULL THEN UPN_1.GYOUSHA_CD
            ELSE UPN_1.UPN_GYOUSHA_CD
        END UPN_GYOUSHA_CD1,                                                -- 運搬受託者CD1
        CASE
            WHEN UPN_1.GYOUSHA_NAME_RYAKU IS NOT NULL THEN UPN_1.GYOUSHA_NAME_RYAKU
            ELSE UPN_1.UPN_SHA_NAME
        END UPN_JYUTAKUSHA_NAME1,                                           -- 運搬受託者名1
        UPN_1.UPN_TAN_NAME AS UNTENSHA_NAME1,                        -- 運転者1
        CASE
            WHEN UPN_2.GYOUSHA_CD IS NOT NULL THEN UPN_2.GYOUSHA_CD
            ELSE UPN_2.UPN_GYOUSHA_CD
        END UPN_GYOUSHA_CD2,                                                -- 運搬受託者CD2
        CASE
            WHEN UPN_2.GYOUSHA_NAME_RYAKU IS NOT NULL THEN UPN_2.GYOUSHA_NAME_RYAKU
            ELSE UPN_2.UPN_SHA_NAME
        END UPN_JYUTAKUSHA_NAME2,                                           -- 運搬受託者名2
        UPN_2.UPN_TAN_NAME AS UNTENSHA_NAME2,                        -- 運転者2
        CASE
            WHEN UPN_3.GYOUSHA_CD IS NOT NULL THEN UPN_3.GYOUSHA_CD
            ELSE UPN_3.UPN_GYOUSHA_CD
        END UPN_GYOUSHA_CD3,                                                -- 運搬受託者CD3
        CASE
            WHEN UPN_3.GYOUSHA_NAME_RYAKU IS NOT NULL THEN UPN_3.GYOUSHA_NAME_RYAKU
            ELSE UPN_3.UPN_SHA_NAME
        END UPN_JYUTAKUSHA_NAME3,                                           -- 運搬受託者名3
        UPN_3.UPN_TAN_NAME AS UNTENSHA_NAME3,                        -- 運転者3
        CASE
            WHEN UPN_4.GYOUSHA_CD IS NOT NULL THEN UPN_4.GYOUSHA_CD
            ELSE UPN_4.UPN_GYOUSHA_CD
        END UPN_GYOUSHA_CD4,                                                -- 運搬受託者CD4
        CASE
            WHEN UPN_4.GYOUSHA_NAME_RYAKU IS NOT NULL THEN UPN_4.GYOUSHA_NAME_RYAKU
            ELSE UPN_4.UPN_SHA_NAME
        END UPN_JYUTAKUSHA_NAME4,                                           -- 運搬受託者名4
        UPN_4.UPN_TAN_NAME AS UNTENSHA_NAME4,                        -- 運転者4
        CASE
            WHEN UPN_5.GYOUSHA_CD IS NOT NULL THEN UPN_5.GYOUSHA_CD
            ELSE UPN_5.UPN_GYOUSHA_CD
        END UPN_GYOUSHA_CD5,                                                -- 運搬受託者CD5
        CASE
            WHEN UPN_5.GYOUSHA_NAME_RYAKU IS NOT NULL THEN UPN_5.GYOUSHA_NAME_RYAKU
            ELSE UPN_5.UPN_SHA_NAME
        END UPN_JYUTAKUSHA_NAME5,                                           -- 運搬受託者名5
        UPN_5.UPN_TAN_NAME AS UNTENSHA_NAME5,                        -- 運転者5
        CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN DR18MIX.SBN_HOUHOU_CD
            ELSE DR18EX.SBN_HOUHOU_CD
        END AS SBN_HOUHOU_CD,                                               -- 処分方法CD
        CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN MSH_MIX.SHOBUN_HOUHOU_NAME_RYAKU
            ELSE MSH.SHOBUN_HOUHOU_NAME_RYAKU
        END AS SBN_HOUHOU,                                                  -- 処分方法
        CASE
            WHEN UPN_5.UPN_END_DATE IS NOT NULL THEN UPN_5.UPN_END_DATE
            WHEN UPN_4.UPN_END_DATE IS NOT NULL THEN UPN_4.UPN_END_DATE
            WHEN UPN_3.UPN_END_DATE IS NOT NULL THEN UPN_3.UPN_END_DATE
            WHEN UPN_2.UPN_END_DATE IS NOT NULL THEN UPN_2.UPN_END_DATE
            ELSE UPN_1.UPN_END_DATE
        END UPN_END_DATE,                                                   -- 運搬終了年月日
        SBN_JIGYOUSHA.GYOUSHA_CD AS SBN_GYOUSHA_CD,                         -- 処分事業者CD
        SBN_JIGYOUSHA.GYOUSHA_NAME_RYAKU AS SBN_GYOUSHA,                    -- 処分事業者
        SBN_JIGYOUBA.GENBA_CD AS UPN_SAKI_GENBA_CD,							-- 処分事業場CD
        SBN_JIGYOUBA.GENBA_NAME_RYAKU AS UPN_SAKI_GENBA_NAME,               -- 処分事業場名
        DR18.SBN_END_DATE AS SBN_END_DATE,                                  -- 処分終了年月日
        /*IF dto.IchijiKbn == 1*/
            -- 紐付済みの場合は二次マニの最終処分情報を出力する
            -- 最終処分受託者
            CASE WHEN DR18EX.SBN_ENDREP_KBN IS NULL OR DR18EX.SBN_ENDREP_KBN = '' OR DR18EX.SBN_ENDREP_KBN != 2
                THEN
                    CASE WHEN HIMOZUKE_MANIFEST.MANIFEST_ID IS NULL OR HIMOZUKE_MANIFEST.MANIFEST_ID = ''
                        THEN LAST_SBN_JIGYOUJOU.GYOUSHA_CD
                    ELSE
                        CASE WHEN HIMOZUKE_MANIFEST.HAIKI_KBN_CD IS NOT NULL AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != '' AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != 4
                            THEN LAST_SBN_JIGYOUJOU_HIMOZUKE.GYOUSHA_CD
                            ELSE LAST_SBN_JIGYOUJOU_NEXT_ELEC.GYOUSHA_CD
                        END
                    END
                ELSE
                    SBN_JIGYOUSHA.GYOUSHA_CD
            END AS LAST_SBN_GYOUSHA_CD,
            -- 最終処分場所CD
            CASE WHEN DR18EX.SBN_ENDREP_KBN IS NULL OR DR18EX.SBN_ENDREP_KBN = '' OR DR18EX.SBN_ENDREP_KBN != 2
                THEN
                    CASE WHEN HIMOZUKE_MANIFEST.MANIFEST_ID IS NULL OR HIMOZUKE_MANIFEST.MANIFEST_ID = ''
                        THEN LAST_SBN_JIGYOUJOU.GENBA_CD
                    ELSE
                        CASE WHEN HIMOZUKE_MANIFEST.HAIKI_KBN_CD IS NOT NULL AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != '' AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != 4
                            THEN LAST_SBN_JIGYOUJOU_HIMOZUKE.GENBA_CD
                            ELSE LAST_SBN_JIGYOUJOU_NEXT_ELEC.GENBA_CD
                        END
                    END
                ELSE
                    SBN_JIGYOUBA.GENBA_CD
            END AS LAST_SBN_GENBA_CD,
            -- 最終処分場所名
            CASE WHEN DR18EX.SBN_ENDREP_KBN IS NULL OR DR18EX.SBN_ENDREP_KBN = '' OR DR18EX.SBN_ENDREP_KBN != 2
                THEN
                    CASE WHEN HIMOZUKE_MANIFEST.MANIFEST_ID IS NULL OR HIMOZUKE_MANIFEST.MANIFEST_ID = ''
                        THEN CASE
                            WHEN LAST_SBN_JIGYOUJOU.GENBA_NAME_RYAKU IS NOT NULL THEN LAST_SBN_JIGYOUJOU.GENBA_NAME_RYAKU
                            ELSE DR13_2.LAST_SBN_JOU_NAME
                        END
                    ELSE
                        CASE WHEN HIMOZUKE_MANIFEST.HAIKI_KBN_CD IS NOT NULL AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != '' AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != 4
                            THEN CASE
                                    WHEN LAST_SBN_JIGYOUJOU_HIMOZUKE.GENBA_NAME_RYAKU IS NOT NULL THEN LAST_SBN_JIGYOUJOU_HIMOZUKE.GENBA_NAME_RYAKU
                                    ELSE ''
                                END
                            ELSE CASE
                                WHEN LAST_SBN_JIGYOUJOU_NEXT_ELEC.GENBA_NAME_RYAKU IS NOT NULL THEN LAST_SBN_JIGYOUJOU_NEXT_ELEC.GENBA_NAME_RYAKU
                                ELSE NEXT_DR13.LAST_SBN_JOU_NAME
                            END
                        END
                    END
                ELSE
                    SBN_JIGYOUBA.GENBA_NAME_RYAKU
            END AS LAST_SBN_GENBA_NAME,
            -- 最終処分終了年月日
            CASE WHEN DR18EX.SBN_ENDREP_KBN IS NULL OR DR18EX.SBN_ENDREP_KBN = '' OR DR18EX.SBN_ENDREP_KBN != 2
                THEN
                    CASE WHEN HIMOZUKE_MANIFEST.MANIFEST_ID IS NULL OR HIMOZUKE_MANIFEST.MANIFEST_ID = ''
                        THEN DR13_2.MAX_LAST_SBN_END_DATE
                    ELSE
                        CASE WHEN HIMOZUKE_MANIFEST.HAIKI_KBN_CD IS NOT NULL AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != '' AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != 4
                            THEN CONVERT(varchar, HIMOZUKE_MANIFEST.LAST_SBN_END_DATE, 112)
                            ELSE NEXT_DR13.MAX_LAST_SBN_END_DATE
                        END
                    END
                ELSE
                    CONVERT(varchar, DR18EX.LAST_SBN_END_DATE, 112)
            END AS LAST_SBN_END_DATE,
        /*END*/
        /*IF dto.IchijiKbn == 2*/
        LAST_SBN_JIGYOUJOU.GYOUSHA_CD AS LAST_SBN_GYOUSHA_CD,               -- 最終処分受託者
        LAST_SBN_JIGYOUJOU.GENBA_CD AS LAST_SBN_GENBA_CD,                   -- 最終処分場所CD
        CASE
            WHEN LAST_SBN_JIGYOUJOU.GENBA_NAME_RYAKU IS NOT NULL THEN LAST_SBN_JIGYOUJOU.GENBA_NAME_RYAKU
            ELSE DR13.LAST_SBN_JOU_NAME
        END LAST_SBN_GENBA_NAME,                                            -- 最終処分場所名
        DR13.MAX_LAST_SBN_END_DATE AS LAST_SBN_END_DATE,                    -- 最終処分終了年月日
        /*END*/
        HIMOZUKE_MANIFEST.MANIFEST_ID AS HIMOZUKE_MANIFEST_ID,              -- ２次交付番号
        CASE WHEN DR18MIX.SYSTEM_ID IS NOT NULL
            THEN DR18MIX.GENNYOU_SUU
            ELSE DR18EX.GENNYOU_SUU
        END AS GENNYOU_SUU,                                                 -- 減容後数量
        DR18.HAIKI_SUU AS SUURYOU_NO_GOUKEI,                                -- 数量の合計
        CASE WHEN MDHS.HAIKI_KBN = '1' THEN '普通の産業廃棄物' 
             WHEN MDHS.HAIKI_KBN = '2' THEN '不可分一体産業廃棄物' 
             WHEN MDHS.HAIKI_KBN = '3' THEN '特別管理産業廃棄物' 
             WHEN MDHS.HAIKI_KBN = '4' THEN '特定産業廃棄物' 
             WHEN MDHS.HAIKI_KBN = '5' THEN '特定産業廃棄物（特別管理型）' 
             ELSE '' 
        END AS HAIKIBUTU_KUBUN                                              -- 廃棄物区分
    FROM DT_MF_TOC AS DMT
    JOIN DT_R18 AS DR18 ON DMT.KANRI_ID = DR18.KANRI_ID AND DMT.LATEST_SEQ = DR18.SEQ
    /*IF dto.IchijiKbn == 1*/
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
            CASE WHEN DT_R18_MIX.DETAIL_SYSTEM_ID IS NOT NULL
                THEN 0
                ELSE DT_R18_EX.GENNYOU_SUU
            END AS GENNYOU_SUU,
            DT_R18_EX.HST_GYOUSHA_CD,
            DT_R18_EX.HST_GENBA_CD,
            DT_R18_EX.SBN_GYOUSHA_CD,
			DT_R18_EX.SBN_GENBA_CD,
            DT_R18_EX.DELETE_FLG AS DELETE_FLG,
            DT_R18_MIX.SBN_ENDREP_KBN,
            DT_R18_MIX.LAST_SBN_END_DATE,
			DT_R18_EX.SBN_HOUHOU_CD
        FROM
            DT_R18_EX
            LEFT JOIN DT_R18_MIX
            ON DT_R18_EX.SYSTEM_ID = DT_R18_MIX.SYSTEM_ID
            AND DT_R18_MIX.DELETE_FLG = 0
    ) AS DR18EX ON DR18.KANRI_ID = DR18EX.KANRI_ID AND DR18EX.DELETE_FLG = 0
    LEFT JOIN DT_R18_MIX AS DR18MIX ON DR18EX.KANRI_ID = DR18MIX.KANRI_ID AND DR18EX.DETAIL_SYSTEM_ID = DR18MIX.DETAIL_SYSTEM_ID AND DR18MIX.DELETE_FLG = 0 AND DR18EX.DETAIL_SYSTEM_ID IS NOT NULL
    /*END*/
    /*IF dto.IchijiKbn == 2*/
    JOIN DT_R18_EX AS DR18EX ON DR18.KANRI_ID = DR18EX.KANRI_ID AND DR18EX.DELETE_FLG = 0
    LEFT JOIN DT_R18_MIX AS DR18MIX ON DR18EX.KANRI_ID = DR18MIX.KANRI_ID AND DR18EX.SYSTEM_ID = DR18MIX.SYSTEM_ID AND DR18MIX.DELETE_FLG = 0
    /*END*/
    LEFT JOIN (
        SELECT
            R13_1.KANRI_ID,
            R13_1.SEQ,
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
    LEFT JOIN M_SYS_INFO AS MSI ON 1 = 1
    LEFT JOIN (
        SELECT
            M_GYOUSHA.GYOUSHA_CD,
            M_GYOUSHA.GYOUSHA_NAME_RYAKU
        FROM M_GYOUSHA
            WHERE M_GYOUSHA.DELETE_FLG = 0
    ) AS HST_JIGYOUSHA ON  HST_JIGYOUSHA.GYOUSHA_CD = DR18EX.HST_GYOUSHA_CD
    LEFT JOIN (
        SELECT
            M_GENBA.GENBA_CD,
            M_GENBA.GYOUSHA_CD,
            M_GENBA.GENBA_NAME_RYAKU
            FROM M_GENBA
            WHERE M_GENBA.DELETE_FLG = 0
    ) AS HST_JIGYOUJOU ON HST_JIGYOUJOU.GYOUSHA_CD = DR18EX.HST_GYOUSHA_CD AND HST_JIGYOUJOU.GENBA_CD = DR18EX.HST_GENBA_CD
    LEFT JOIN (
        SELECT
            M_GYOUSHA.GYOUSHA_CD,
            M_GYOUSHA.GYOUSHA_NAME_RYAKU
        FROM M_GYOUSHA
            WHERE M_GYOUSHA.DELETE_FLG = 0
    ) AS SBN_JIGYOUSHA ON SBN_JIGYOUSHA.GYOUSHA_CD = DR18EX.SBN_GYOUSHA_CD
	LEFT JOIN (
        SELECT
			M_GENBA.GYOUSHA_CD,
            M_GENBA.GENBA_CD,
            M_GENBA.GENBA_NAME_RYAKU    
        FROM M_GENBA
            WHERE M_GENBA.DELETE_FLG = 0
    ) AS SBN_JIGYOUBA ON SBN_JIGYOUBA.GYOUSHA_CD = DR18EX.SBN_GYOUSHA_CD AND SBN_JIGYOUBA.GENBA_CD = DR18EX.SBN_GENBA_CD
    LEFT JOIN M_UNIT AS MU ON DR18.HAIKI_UNIT_CODE = MU.UNIT_CD
    LEFT JOIN M_UNIT AS MIX_MU ON DR18MIX.HAIKI_UNIT_CD = MIX_MU.UNIT_CD
    LEFT JOIN M_UNIT AS MU2 ON MSI.MANI_KANSAN_KIHON_UNIT_CD = MU2.UNIT_CD
    LEFT JOIN M_SHOBUN_HOUHOU AS MSH ON CONVERT(nvarchar, DR18EX.SBN_HOUHOU_CD) = MSH.SHOBUN_HOUHOU_CD
    LEFT JOIN M_SHOBUN_HOUHOU AS MSH_MIX ON CONVERT(nvarchar, DR18MIX.SBN_HOUHOU_CD) = MSH_MIX.SHOBUN_HOUHOU_CD
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
            DT_R19.UPN_TAN_NAME
        FROM DT_R19
            JOIN DT_R19_EX ON DT_R19.KANRI_ID = DT_R19_EX.KANRI_ID AND DT_R19.UPN_ROUTE_NO = DT_R19_EX.UPN_ROUTE_NO AND DT_R19_EX.DELETE_FLG = 0
            LEFT JOIN M_GYOUSHA ON DT_R19_EX.UPN_GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD AND M_GYOUSHA.DELETE_FLG = 0
        WHERE DT_R19.UPN_ROUTE_NO = 1
    ) AS UPN_1 ON UPN_1.KANRI_ID = DMT.KANRI_ID AND UPN_1.SEQ = DMT.LATEST_SEQ
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
            DT_R19.UPN_TAN_NAME
        FROM DT_R19
            JOIN DT_R19_EX ON DT_R19.KANRI_ID = DT_R19_EX.KANRI_ID AND DT_R19.UPN_ROUTE_NO = DT_R19_EX.UPN_ROUTE_NO AND DT_R19_EX.DELETE_FLG = 0
            LEFT JOIN M_GYOUSHA ON DT_R19_EX.UPN_GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD AND M_GYOUSHA.DELETE_FLG = 0
        WHERE DT_R19.UPN_ROUTE_NO = 2
    ) AS UPN_2 ON UPN_2.KANRI_ID = DMT.KANRI_ID AND UPN_2.SEQ = DMT.LATEST_SEQ
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
            DT_R19.UPN_TAN_NAME
        FROM DT_R19
            JOIN DT_R19_EX ON DT_R19.KANRI_ID = DT_R19_EX.KANRI_ID AND DT_R19.UPN_ROUTE_NO = DT_R19_EX.UPN_ROUTE_NO AND DT_R19_EX.DELETE_FLG = 0
            LEFT JOIN M_GYOUSHA ON DT_R19_EX.UPN_GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD AND M_GYOUSHA.DELETE_FLG = 0
        WHERE DT_R19.UPN_ROUTE_NO = 3
    ) AS UPN_3 ON UPN_3.KANRI_ID = DMT.KANRI_ID AND UPN_3.SEQ = DMT.LATEST_SEQ
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
            DT_R19.UPN_TAN_NAME
        FROM DT_R19
            JOIN DT_R19_EX ON DT_R19.KANRI_ID = DT_R19_EX.KANRI_ID AND DT_R19.UPN_ROUTE_NO = DT_R19_EX.UPN_ROUTE_NO AND DT_R19_EX.DELETE_FLG = 0
            LEFT JOIN M_GYOUSHA ON DT_R19_EX.UPN_GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD AND M_GYOUSHA.DELETE_FLG = 0
        WHERE DT_R19.UPN_ROUTE_NO = 4
    ) AS UPN_4 ON UPN_4.KANRI_ID = DMT.KANRI_ID AND UPN_4.SEQ = DMT.LATEST_SEQ
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
            DT_R19.UPN_TAN_NAME
        FROM DT_R19
            JOIN DT_R19_EX ON DT_R19.KANRI_ID = DT_R19_EX.KANRI_ID AND DT_R19.UPN_ROUTE_NO = DT_R19_EX.UPN_ROUTE_NO AND DT_R19_EX.DELETE_FLG = 0
            LEFT JOIN M_GYOUSHA ON DT_R19_EX.UPN_GYOUSHA_CD = M_GYOUSHA.GYOUSHA_CD AND M_GYOUSHA.DELETE_FLG = 0
        WHERE DT_R19.UPN_ROUTE_NO = 5
    ) AS UPN_5 ON UPN_5.KANRI_ID = DMT.KANRI_ID AND UPN_5.SEQ = DMT.LATEST_SEQ
    LEFT JOIN M_DENSHI_HAIKI_SHURUI AS MDHS ON (DR18.HAIKI_DAI_CODE + DR18.HAIKI_CHU_CODE + DR18.HAIKI_SHO_CODE) = MDHS.HAIKI_SHURUI_CD
    LEFT JOIN M_DENSHI_HAIKI_SHURUI AS MDHS_MIX ON (DR18MIX.HAIKI_DAI_CODE + DR18MIX.HAIKI_CHU_CODE + DR18MIX.HAIKI_SHO_CODE) = MDHS_MIX.HAIKI_SHURUI_CD
    LEFT JOIN M_DENSHI_HAIKI_NAME AS MHN_MIX ON DR18.HST_SHA_EDI_MEMBER_ID = MHN_MIX.EDI_MEMBER_ID AND DR18MIX.HAIKI_NAME_CD = MHN_MIX.HAIKI_NAME_CD
    LEFT JOIN M_HOUKOKUSHO_BUNRUI AS MHB ON MDHS.HOUKOKUSHO_BUNRUI_CD = MHB.HOUKOKUSHO_BUNRUI_CD
    LEFT JOIN M_HOUKOKUSHO_BUNRUI AS MHB_MIX ON MDHS_MIX.HOUKOKUSHO_BUNRUI_CD = MHB_MIX.HOUKOKUSHO_BUNRUI_CD
    /*IF dto.IchijiKbn == 1*/
    LEFT JOIN
    (
        SELECT DISTINCT
            MANIFEST_R.FIRST_SYSTEM_ID,
            MANIFEST_R.FIRST_HAIKI_KBN_CD,
            MANIFEST_2.KANRI_ID AS 'NEXT_KANRI_ID',
            MANIFEST_2.LATEST_SEQ AS 'NEXT_LATEST_SEQ',
            CASE WHEN MANIFEST_2.MANIFEST_ID IS NOT NULL AND MANIFEST_2.MANIFEST_ID <> ''
                THEN MANIFEST_2.MANIFEST_ID
                ELSE T_MANIFEST_ENTRY.MANIFEST_ID
            END AS MANIFEST_ID,
            MANIFEST_R.NEXT_HAIKI_KBN_CD AS 'HAIKI_KBN_CD',
            CASE WHEN MANIFEST_2.MANIFEST_ID IS NOT NULL AND MANIFEST_2.MANIFEST_ID <> ''
                THEN NULL
                ELSE T_MANIFEST_DETAIL.LAST_SBN_END_DATE
            END AS LAST_SBN_END_DATE,
            CASE WHEN MANIFEST_2.MANIFEST_ID IS NOT NULL AND MANIFEST_2.MANIFEST_ID <> ''
                THEN ''
                ELSE T_MANIFEST_DETAIL.LAST_SBN_GYOUSHA_CD
            END AS LAST_SBN_GYOUSHA_CD,
            CASE WHEN MANIFEST_2.MANIFEST_ID IS NOT NULL AND MANIFEST_2.MANIFEST_ID <> ''
                THEN ''
                ELSE T_MANIFEST_DETAIL.LAST_SBN_GENBA_CD
            END AS LAST_SBN_GENBA_CD
        FROM T_MANIFEST_RELATION AS MANIFEST_R
            LEFT JOIN
            (
                SELECT
                    TOC.KANRI_ID,
                    TOC.LATEST_SEQ,
                    DR18EX.SYSTEM_ID,
                    DR18EX.SEQ,
                    DR18.MANIFEST_ID
                FROM
                    DT_MF_TOC AS TOC
                    INNER JOIN DT_R18 AS DR18
                        ON TOC.KANRI_ID = DR18.KANRI_ID AND TOC.LATEST_SEQ = DR18.SEQ
                    JOIN DT_R18_EX AS DR18EX ON DR18.KANRI_ID = DR18EX.KANRI_ID AND DR18EX.DELETE_FLG = 0
            ) AS MANIFEST_2 ON MANIFEST_R.NEXT_SYSTEM_ID = MANIFEST_2.SYSTEM_ID AND MANIFEST_R.NEXT_HAIKI_KBN_CD = 4
          -- 2016/09/16時点で二次紙マニは1明細しか入力できないので、下記のJOINでも複数件抽出されない
            LEFT JOIN T_MANIFEST_DETAIL
                ON MANIFEST_R.NEXT_SYSTEM_ID = T_MANIFEST_DETAIL.DETAIL_SYSTEM_ID AND MANIFEST_R.NEXT_HAIKI_KBN_CD <> 4
            INNER JOIN T_MANIFEST_ENTRY
                ON T_MANIFEST_DETAIL.SYSTEM_ID = T_MANIFEST_ENTRY.SYSTEM_ID  AND T_MANIFEST_ENTRY.SEQ = T_MANIFEST_DETAIL.SEQ
                AND MANIFEST_R.NEXT_HAIKI_KBN_CD <> 4 AND T_MANIFEST_ENTRY.DELETE_FLG = 0
        WHERE MANIFEST_R.DELETE_FLG = 0
    ) AS HIMOZUKE_MANIFEST ON DR18EX.SYSTEM_ID = HIMOZUKE_MANIFEST.FIRST_SYSTEM_ID AND HIMOZUKE_MANIFEST.FIRST_HAIKI_KBN_CD = 4
    LEFT JOIN (
        SELECT
            R13_1.KANRI_ID,
            R13_1.SEQ,
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
        WHERE R13_1.REC_SEQ = (
								SELECT TOP 1 REC_SEQ
								FROM DT_R13 MAX_R13
								WHERE MAX_R13.KANRI_ID = R13_2.KANRI_ID
								AND MAX_R13.SEQ = R13_2.SEQ
								AND MAX_R13.LAST_SBN_END_DATE = R13_2.LAST_SBN_END_DATE
							  )
    ) AS DR13_2 ON DR18.KANRI_ID = DR13_2.KANRI_ID AND DR18.SEQ = DR13_2.SEQ
    LEFT JOIN (
        SELECT
            R13_1.KANRI_ID,
            R13_1.SEQ,
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
        WHERE R13_1.REC_SEQ = (
								SELECT TOP 1 REC_SEQ
								FROM DT_R13 MAX_R13
								WHERE MAX_R13.KANRI_ID = R13_2.KANRI_ID
								AND MAX_R13.SEQ = R13_2.SEQ
								AND MAX_R13.LAST_SBN_END_DATE = R13_2.LAST_SBN_END_DATE
							  )
    ) AS NEXT_DR13 ON HIMOZUKE_MANIFEST.NEXT_KANRI_ID = NEXT_DR13.KANRI_ID AND HIMOZUKE_MANIFEST.NEXT_LATEST_SEQ = NEXT_DR13.SEQ
    /*END*/
    /*IF dto.IchijiKbn == 2*/
    LEFT JOIN
    (
        SELECT DISTINCT NEXT_SYSTEM_ID AS MANIFEST_ID
        FROM T_MANIFEST_RELATION
        WHERE DELETE_FLG = 0 AND NEXT_HAIKI_KBN_CD = 4
    ) AS HIMOZUKE_MANIFEST ON DR18EX.SYSTEM_ID = HIMOZUKE_MANIFEST.MANIFEST_ID
    /*END*/
    /*IF dto.IchijiKbn == 1*/
    LEFT JOIN (
        SELECT
            M_GENBA.GYOUSHA_CD,
            M_GENBA.GENBA_CD,
            M_GENBA.GENBA_NAME_RYAKU,
            M_DENSHI_JIGYOUJOU.JIGYOUJOU_NAME,
            (ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS1, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS2, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS3, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS4, '')) AS JIGYOUJOU_ADDRESS
        FROM M_DENSHI_JIGYOUJOU
            JOIN M_GENBA ON M_DENSHI_JIGYOUJOU.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND M_DENSHI_JIGYOUJOU.GENBA_CD = M_GENBA.GENBA_CD
    ) AS LAST_SBN_JIGYOUJOU ON LAST_SBN_JIGYOUJOU.JIGYOUJOU_NAME = DR13_2.LAST_SBN_JOU_NAME AND LAST_SBN_JIGYOUJOU.JIGYOUJOU_ADDRESS = (ISNULL(DR13_2.LAST_SBN_JOU_ADDRESS1, '') + ISNULL(DR13_2.LAST_SBN_JOU_ADDRESS2, '') + ISNULL(DR13_2.LAST_SBN_JOU_ADDRESS3, '') + ISNULL(DR13_2.LAST_SBN_JOU_ADDRESS4, ''))
    LEFT JOIN (
        SELECT
            M_GENBA.GYOUSHA_CD,
            M_GENBA.GENBA_CD,
            M_GENBA.GENBA_NAME_RYAKU,
            M_DENSHI_JIGYOUJOU.JIGYOUJOU_NAME,
            (ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS1, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS2, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS3, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS4, '')) AS JIGYOUJOU_ADDRESS
        FROM M_DENSHI_JIGYOUJOU
            JOIN M_GENBA ON M_DENSHI_JIGYOUJOU.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND M_DENSHI_JIGYOUJOU.GENBA_CD = M_GENBA.GENBA_CD
    ) AS LAST_SBN_JIGYOUJOU_NEXT_ELEC ON LAST_SBN_JIGYOUJOU_NEXT_ELEC.JIGYOUJOU_NAME = NEXT_DR13.LAST_SBN_JOU_NAME AND LAST_SBN_JIGYOUJOU_NEXT_ELEC.JIGYOUJOU_ADDRESS = (ISNULL(NEXT_DR13.LAST_SBN_JOU_ADDRESS1, '') + ISNULL(NEXT_DR13.LAST_SBN_JOU_ADDRESS2, '') + ISNULL(NEXT_DR13.LAST_SBN_JOU_ADDRESS3, '') + ISNULL(NEXT_DR13.LAST_SBN_JOU_ADDRESS4, ''))
    LEFT JOIN M_GENBA AS LAST_SBN_JIGYOUJOU_HIMOZUKE
        ON HIMOZUKE_MANIFEST.LAST_SBN_GYOUSHA_CD = LAST_SBN_JIGYOUJOU_HIMOZUKE.GYOUSHA_CD
        AND HIMOZUKE_MANIFEST.LAST_SBN_GENBA_CD = LAST_SBN_JIGYOUJOU_HIMOZUKE.GENBA_CD
    /*END*/
    /*IF dto.IchijiKbn == 2*/
    LEFT JOIN (
        SELECT
            M_GENBA.GYOUSHA_CD,
            M_GENBA.GENBA_CD,
            M_GENBA.GENBA_NAME_RYAKU,
            M_DENSHI_JIGYOUJOU.JIGYOUJOU_NAME,
            (ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS1, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS2, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS3, '') + ISNULL(M_DENSHI_JIGYOUJOU.JIGYOUJOU_ADDRESS4, '')) AS JIGYOUJOU_ADDRESS
        FROM M_DENSHI_JIGYOUJOU
            JOIN M_GENBA ON M_DENSHI_JIGYOUJOU.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND M_DENSHI_JIGYOUJOU.GENBA_CD = M_GENBA.GENBA_CD
    ) AS LAST_SBN_JIGYOUJOU ON LAST_SBN_JIGYOUJOU.JIGYOUJOU_NAME = DR13.LAST_SBN_JOU_NAME AND LAST_SBN_JIGYOUJOU.JIGYOUJOU_ADDRESS = (ISNULL(DR13.LAST_SBN_JOU_ADDRESS1, '') + ISNULL(DR13.LAST_SBN_JOU_ADDRESS2, '') + ISNULL(DR13.LAST_SBN_JOU_ADDRESS3, '') + ISNULL(DR13.LAST_SBN_JOU_ADDRESS4, ''))
    /*END*/
    LEFT JOIN M_GYOUSHA AS HST_GYOUSHA ON DR18EX.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD

    WHERE
    DMT.STATUS_FLAG IN (3, 4)

    /*IF dto.DateFrom != null && dto.DateFrom != ''*/AND CONVERT(varchar, DR18.UPDATE_TS, 112) >= /*dto.DateFrom*/''/*END*/
    /*IF dto.DateTo != null && dto.DateTo != ''*/AND CONVERT(varchar, DR18.UPDATE_TS, 112) <= /*dto.DateTo*/''/*END*/

    /*IF dto.IchijiKbn == 1*/AND (DR18.FIRST_MANIFEST_FLAG IS NULL OR DR18.FIRST_MANIFEST_FLAG = '' OR ISNULL(HST_GYOUSHA.JISHA_KBN, 0) = 0)/*END*/
    /*IF dto.IchijiKbn == 2*/AND (DR18.FIRST_MANIFEST_FLAG <> '' AND ISNULL(HST_GYOUSHA.JISHA_KBN, 0) = 1)/*END*/

    /*IF dto.NijiHimozuke == 2*/AND HIMOZUKE_MANIFEST.MANIFEST_ID IS NOT NULL/*END*/
    /*IF dto.NijiHimozuke == 3*/AND HIMOZUKE_MANIFEST.MANIFEST_ID IS NULL/*END*/

    /*IF dto.KofuDateFrom != null && dto.KofuDateFrom != ''*/AND DR18.HIKIWATASHI_DATE >= /*dto.KofuDateFrom*/''/*END*/
    /*IF dto.KofuDateTo != null && dto.KofuDateTo != ''*/AND DR18.HIKIWATASHI_DATE <= /*dto.KofuDateTo*/''/*END*/

    AND ((1=1
	  /*IF dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != ''*/
      AND UPN_1.UPN_END_DATE >= /*dto.UnpanEndDateFrom*/''/*END*/
	  /*IF dto.UnpanEndDateTo != null && dto.UnpanEndDateTo != ''*/
      AND UPN_1.UPN_END_DATE <= /*dto.UnpanEndDateTo*/''/*END*/)
	  OR(1=1
	  /*IF dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != ''*/
      AND UPN_2.UPN_END_DATE >= /*dto.UnpanEndDateFrom*/''/*END*/
	  /*IF dto.UnpanEndDateTo != null && dto.UnpanEndDateTo != ''*/
      AND UPN_2.UPN_END_DATE <= /*dto.UnpanEndDateTo*/''/*END*/)
	  OR(1=1
	  /*IF dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != ''*/
      AND UPN_3.UPN_END_DATE >= /*dto.UnpanEndDateFrom*/''/*END*/
	  /*IF dto.UnpanEndDateTo != null && dto.UnpanEndDateTo != ''*/
      AND UPN_3.UPN_END_DATE <= /*dto.UnpanEndDateTo*/''/*END*/)
	  OR(1=1
	  /*IF dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != ''*/
      AND UPN_4.UPN_END_DATE >= /*dto.UnpanEndDateFrom*/''/*END*/
	  /*IF dto.UnpanEndDateTo != null && dto.UnpanEndDateTo != ''*/
      AND UPN_4.UPN_END_DATE <= /*dto.UnpanEndDateTo*/''/*END*/)
	  OR(1=1
	  /*IF dto.UnpanEndDateFrom != null && dto.UnpanEndDateFrom != ''*/
      AND UPN_5.UPN_END_DATE >= /*dto.UnpanEndDateFrom*/''/*END*/
	  /*IF dto.UnpanEndDateTo != null && dto.UnpanEndDateTo != ''*/
      AND UPN_5.UPN_END_DATE <= /*dto.UnpanEndDateTo*/''/*END*/)
    )

    /*IF dto.ShobunEndDateFrom != null && dto.ShobunEndDateFrom != ''*/AND DR18.SBN_END_DATE >= /*dto.ShobunEndDateFrom*/''/*END*/
    /*IF dto.ShobunEndDateTo != null && dto.ShobunEndDateTo != ''*/AND DR18.SBN_END_DATE <= /*dto.ShobunEndDateTo*/''/*END*/

    /*IF dto.IchijiKbn == 1*/
        /*IF dto.LastShobunEndDateFrom != null && dto.LastShobunEndDateFrom != ''*/
            AND (
                  ( (DR18EX.SBN_ENDREP_KBN IS NULL OR DR18EX.SBN_ENDREP_KBN = '' OR DR18EX.SBN_ENDREP_KBN != 2)
                    AND (( (HIMOZUKE_MANIFEST.MANIFEST_ID IS NULL OR HIMOZUKE_MANIFEST.MANIFEST_ID = '') AND DR13.MAX_LAST_SBN_END_DATE >= /*dto.LastShobunEndDateFrom*/'' )
                    OR ( HIMOZUKE_MANIFEST.HAIKI_KBN_CD IS NOT NULL AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != '' AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != 4 AND CONVERT(varchar, HIMOZUKE_MANIFEST.LAST_SBN_END_DATE, 112) >= /*dto.LastShobunEndDateFrom*/'' )
                    OR ( HIMOZUKE_MANIFEST.HAIKI_KBN_CD IS NOT NULL AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != '' AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD = 4 AND NEXT_DR13.MAX_LAST_SBN_END_DATE >= /*dto.LastShobunEndDateFrom*/'' ))
                  )
                  OR (
                    (DR18EX.SBN_ENDREP_KBN IS NOT NULL AND DR18EX.SBN_ENDREP_KBN != '' OR DR18EX.SBN_ENDREP_KBN = 2)
                    AND CONVERT(varchar, DR18EX.LAST_SBN_END_DATE, 112) >= /*dto.LastShobunEndDateFrom*/''
                  )
            )
        /*END*/
        /*IF dto.LastShobunEndDateTo != null && dto.LastShobunEndDateTo != ''*/
            AND (
                  ( ( DR18EX.SBN_ENDREP_KBN IS NULL OR DR18EX.SBN_ENDREP_KBN = '' OR DR18EX.SBN_ENDREP_KBN != 2 )
                    AND ( (HIMOZUKE_MANIFEST.MANIFEST_ID IS NULL OR HIMOZUKE_MANIFEST.MANIFEST_ID = '') AND DR13.MAX_LAST_SBN_END_DATE <= /*dto.LastShobunEndDateTo*/'' )
                    OR ( HIMOZUKE_MANIFEST.HAIKI_KBN_CD IS NOT NULL AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != '' AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != 4 AND CONVERT(varchar, HIMOZUKE_MANIFEST.LAST_SBN_END_DATE, 112) <= /*dto.LastShobunEndDateTo*/'' )
                    OR ( HIMOZUKE_MANIFEST.HAIKI_KBN_CD IS NOT NULL AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != '' AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD = 4 AND NEXT_DR13.MAX_LAST_SBN_END_DATE <= /*dto.LastShobunEndDateTo*/'' ) )
                  OR (
                    ( DR18EX.SBN_ENDREP_KBN IS NOT NULL AND DR18EX.SBN_ENDREP_KBN != '' AND DR18EX.SBN_ENDREP_KBN = 2 )
                    AND CONVERT(varchar, DR18EX.LAST_SBN_END_DATE, 112) <= /*dto.LastShobunEndDateTo*/''
                  )
            )
        /*END*/
    -- ELSE
        /*IF dto.LastShobunEndDateFrom != null && dto.LastShobunEndDateFrom != ''*/AND DR13.MAX_LAST_SBN_END_DATE >= /*dto.LastShobunEndDateFrom*/''/*END*/
        /*IF dto.LastShobunEndDateTo != null && dto.LastShobunEndDateTo != ''*/AND DR13.MAX_LAST_SBN_END_DATE <= /*dto.LastShobunEndDateTo*/''/*END*/
    /*END*/

    /*IF dto.HaishutsuJigyoushaCdFrom != null && dto.HaishutsuJigyoushaCdFrom != ''*/AND DR18EX.HST_GYOUSHA_CD >= /*dto.HaishutsuJigyoushaCdFrom*/''/*END*/
    /*IF dto.HaishutsuJigyoushaCdTo != null && dto.HaishutsuJigyoushaCdTo != ''*/AND DR18EX.HST_GYOUSHA_CD <= /*dto.HaishutsuJigyoushaCdTo*/''/*END*/

    /*IF dto.HaishutsuJigyoujouCdFrom != null && dto.HaishutsuJigyoujouCdFrom != ''*/AND DR18EX.HST_GENBA_CD >= /*dto.HaishutsuJigyoujouCdFrom*/''/*END*/
    /*IF dto.HaishutsuJigyoujouCdTo != null && dto.HaishutsuJigyoujouCdTo != ''*/AND DR18EX.HST_GENBA_CD <= /*dto.HaishutsuJigyoujouCdTo*/''/*END*/

    AND ((1=1
	  /*IF dto.UnpanJutakushaCdFrom != null && dto.UnpanJutakushaCdFrom != ''*/
      AND UPN_1.UPN_GYOUSHA_CD >= /*dto.UnpanJutakushaCdFrom*/''/*END*/
	  /*IF dto.UnpanJutakushaCdTo != null && dto.UnpanJutakushaCdTo != ''*/
	  AND UPN_1.UPN_GYOUSHA_CD <= /*dto.UnpanJutakushaCdTo*/''/*END*/)
	  OR(1=1
	  /*IF dto.UnpanJutakushaCdFrom != null && dto.UnpanJutakushaCdFrom != ''*/
      AND UPN_2.UPN_GYOUSHA_CD >= /*dto.UnpanJutakushaCdFrom*/''/*END*/
	  /*IF dto.UnpanJutakushaCdTo != null && dto.UnpanJutakushaCdTo != ''*/
	  AND UPN_2.UPN_GYOUSHA_CD <= /*dto.UnpanJutakushaCdTo*/''/*END*/)
	  OR(1=1
	  /*IF dto.UnpanJutakushaCdFrom != null && dto.UnpanJutakushaCdFrom != ''*/
      AND UPN_3.UPN_GYOUSHA_CD >= /*dto.UnpanJutakushaCdFrom*/''/*END*/
	  /*IF dto.UnpanJutakushaCdTo != null && dto.UnpanJutakushaCdTo != ''*/
	  AND UPN_3.UPN_GYOUSHA_CD <= /*dto.UnpanJutakushaCdTo*/''/*END*/)
	  OR(1=1
	  /*IF dto.UnpanJutakushaCdFrom != null && dto.UnpanJutakushaCdFrom != ''*/
      AND UPN_4.UPN_GYOUSHA_CD >= /*dto.UnpanJutakushaCdFrom*/''/*END*/
	  /*IF dto.UnpanJutakushaCdTo != null && dto.UnpanJutakushaCdTo != ''*/
	  AND UPN_4.UPN_GYOUSHA_CD <= /*dto.UnpanJutakushaCdTo*/''/*END*/)
	  OR(1=1
	  /*IF dto.UnpanJutakushaCdFrom != null && dto.UnpanJutakushaCdFrom != ''*/
      AND UPN_5.UPN_GYOUSHA_CD >= /*dto.UnpanJutakushaCdFrom*/''/*END*/
	  /*IF dto.UnpanJutakushaCdTo != null && dto.UnpanJutakushaCdTo != ''*/
	  AND UPN_5.UPN_GYOUSHA_CD <= /*dto.UnpanJutakushaCdTo*/''/*END*/)
     )

    /*IF dto.ShobunJigyoushaCdFrom != null && dto.ShobunJigyoushaCdFrom != ''*/AND DR18EX.SBN_GYOUSHA_CD >= /*dto.ShobunJigyoushaCdFrom*/''/*END*/
    /*IF dto.ShobunJigyoushaCdTo != null && dto.ShobunJigyoushaCdTo != ''*/AND DR18EX.SBN_GYOUSHA_CD <= /*dto.ShobunJigyoushaCdTo*/''/*END*/

    AND ((1=1
	  /*IF dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != ''*/
      AND UPN_1.UPNSAKI_GENBA_CD >= /*dto.ShobunJigyoujouCdFrom*/''/*END*/
	  /*IF dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != ''*/
	  AND UPN_1.UPNSAKI_GENBA_CD <= /*dto.ShobunJigyoujouCdTo*/''/*END*/)
	  OR(1=1
	  /*IF dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != ''*/
      AND UPN_2.UPNSAKI_GENBA_CD >= /*dto.ShobunJigyoujouCdFrom*/''/*END*/
	  /*IF dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != ''*/
	  AND UPN_2.UPNSAKI_GENBA_CD <= /*dto.ShobunJigyoujouCdTo*/''/*END*/)
      OR(1=1
	  /*IF dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != ''*/
      AND UPN_3.UPNSAKI_GENBA_CD >= /*dto.ShobunJigyoujouCdFrom*/''/*END*/
	  /*IF dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != ''*/
	  AND UPN_3.UPNSAKI_GENBA_CD <= /*dto.ShobunJigyoujouCdTo*/''/*END*/)
	  OR(1=1
	  /*IF dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != ''*/
      AND UPN_4.UPNSAKI_GENBA_CD >= /*dto.ShobunJigyoujouCdFrom*/''/*END*/
	  /*IF dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != ''*/
	  AND UPN_4.UPNSAKI_GENBA_CD <= /*dto.ShobunJigyoujouCdTo*/''/*END*/)
	  OR(1=1
	  /*IF dto.ShobunJigyoujouCdFrom != null && dto.ShobunJigyoujouCdFrom != ''*/
      AND UPN_5.UPNSAKI_GENBA_CD >= /*dto.ShobunJigyoujouCdFrom*/''/*END*/
	  /*IF dto.ShobunJigyoujouCdTo != null && dto.ShobunJigyoujouCdTo != ''*/
	  AND UPN_5.UPNSAKI_GENBA_CD <= /*dto.ShobunJigyoujouCdTo*/''/*END*/)
    )

    /*IF dto.IchijiKbn == 1*/
        /*IF dto.LastShobunJigyoushaCdFrom != null && dto.LastShobunJigyoushaCdFrom != ''*/
            AND ( 
                  ( (DR18EX.SBN_ENDREP_KBN IS NULL OR DR18EX.SBN_ENDREP_KBN = '' OR DR18EX.SBN_ENDREP_KBN != 2)
                    AND (( (HIMOZUKE_MANIFEST.MANIFEST_ID IS NULL OR HIMOZUKE_MANIFEST.MANIFEST_ID = '') AND LAST_SBN_JIGYOUJOU.GYOUSHA_CD >= /*dto.LastShobunJigyoushaCdFrom*/'' )
                    OR ( HIMOZUKE_MANIFEST.HAIKI_KBN_CD IS NOT NULL AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != '' AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != 4 AND LAST_SBN_JIGYOUJOU_HIMOZUKE.GYOUSHA_CD >= /*dto.LastShobunJigyoushaCdFrom*/'' )
                    OR ( HIMOZUKE_MANIFEST.HAIKI_KBN_CD IS NOT NULL AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != '' AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD = 4 AND LAST_SBN_JIGYOUJOU_NEXT_ELEC.GYOUSHA_CD >= /*dto.LastShobunJigyoushaCdFrom*/'' ) )
                  )
                  OR (
                    (DR18EX.SBN_ENDREP_KBN IS NOT NULL AND DR18EX.SBN_ENDREP_KBN != '' AND DR18EX.SBN_ENDREP_KBN = 2)
                    AND SBN_JIGYOUSHA.GYOUSHA_CD >= /*dto.LastShobunJigyoushaCdFrom*/''
                  )
            )
        /*END*/
        /*IF dto.LastShobunJigyoushaCdTo != null && dto.LastShobunJigyoushaCdTo != ''*/
            AND (
                  ( (DR18EX.SBN_ENDREP_KBN IS NULL OR DR18EX.SBN_ENDREP_KBN = '' OR DR18EX.SBN_ENDREP_KBN != 2)
                    AND (( (HIMOZUKE_MANIFEST.MANIFEST_ID IS NULL OR HIMOZUKE_MANIFEST.MANIFEST_ID = '') AND LAST_SBN_JIGYOUJOU.GYOUSHA_CD <= /*dto.LastShobunJigyoushaCdTo*/'' )
                    OR (HIMOZUKE_MANIFEST.HAIKI_KBN_CD IS NOT NULL AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != '' AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != 4 AND LAST_SBN_JIGYOUJOU_HIMOZUKE.GYOUSHA_CD <= /*dto.LastShobunJigyoushaCdTo*/'' )
                    OR (HIMOZUKE_MANIFEST.HAIKI_KBN_CD IS NOT NULL AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != '' AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD = 4 AND LAST_SBN_JIGYOUJOU_NEXT_ELEC.GYOUSHA_CD <= /*dto.LastShobunJigyoushaCdTo*/'' ))
                  )
                  OR (
                    (DR18EX.SBN_ENDREP_KBN IS NOT NULL AND DR18EX.SBN_ENDREP_KBN != '' AND DR18EX.SBN_ENDREP_KBN = 2)
                    AND SBN_JIGYOUSHA.GYOUSHA_CD <= /*dto.LastShobunJigyoushaCdTo*/''
                  )
            )
        /*END*/
    -- ELSE
        /*IF dto.LastShobunJigyoushaCdFrom != null && dto.LastShobunJigyoushaCdFrom != ''*/AND LAST_SBN_JIGYOUJOU.GYOUSHA_CD >= /*dto.LastShobunJigyoushaCdFrom*/''/*END*/
        /*IF dto.LastShobunJigyoushaCdTo != null && dto.LastShobunJigyoushaCdTo != ''*/AND LAST_SBN_JIGYOUJOU.GYOUSHA_CD <= /*dto.LastShobunJigyoushaCdTo*/''/*END*/
    /*END*/

    /*IF dto.IchijiKbn == 1*/
        /*IF dto.LastShobunJigyoujouCdFrom != null && dto.LastShobunJigyoujouCdFrom != ''*/
            AND (
                  ( (DR18EX.SBN_ENDREP_KBN IS NULL OR DR18EX.SBN_ENDREP_KBN = '' OR DR18EX.SBN_ENDREP_KBN != 2)
                    AND (( (HIMOZUKE_MANIFEST.MANIFEST_ID IS NULL OR HIMOZUKE_MANIFEST.MANIFEST_ID = '') AND LAST_SBN_JIGYOUJOU.GENBA_CD >= /*dto.LastShobunJigyoujouCdFrom*/'' )
                    OR ( HIMOZUKE_MANIFEST.HAIKI_KBN_CD IS NOT NULL AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != '' AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != 4 AND LAST_SBN_JIGYOUJOU_HIMOZUKE.GENBA_CD >= /*dto.LastShobunJigyoujouCdFrom*/'' )
                    OR ( HIMOZUKE_MANIFEST.HAIKI_KBN_CD IS NOT NULL AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != '' AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD = 4 AND LAST_SBN_JIGYOUJOU_NEXT_ELEC.GENBA_CD >= /*dto.LastShobunJigyoujouCdFrom*/'' ) )
                  )
                  OR (
                    (DR18EX.SBN_ENDREP_KBN IS NOT NULL AND DR18EX.SBN_ENDREP_KBN != '' AND DR18EX.SBN_ENDREP_KBN = 2)
                    AND SBN_JIGYOUBA.GENBA_CD >= /*dto.LastShobunJigyoujouCdFrom*/''
                  )
            )
        /*END*/
        /*IF dto.LastShobunJigyoujouCdTo != null && dto.LastShobunJigyoujouCdTo != ''*/
            AND (
                  ( (DR18EX.SBN_ENDREP_KBN IS NULL OR DR18EX.SBN_ENDREP_KBN = '' OR DR18EX.SBN_ENDREP_KBN != 2)
                    AND (( (HIMOZUKE_MANIFEST.MANIFEST_ID IS NULL OR HIMOZUKE_MANIFEST.MANIFEST_ID = '') AND LAST_SBN_JIGYOUJOU.GENBA_CD <= /*dto.LastShobunJigyoujouCdTo*/'' )
                    OR ( HIMOZUKE_MANIFEST.HAIKI_KBN_CD IS NOT NULL AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != '' AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != 4 AND LAST_SBN_JIGYOUJOU_HIMOZUKE.GENBA_CD <= /*dto.LastShobunJigyoujouCdTo*/'' )
                    OR ( HIMOZUKE_MANIFEST.HAIKI_KBN_CD IS NOT NULL AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD != '' AND HIMOZUKE_MANIFEST.HAIKI_KBN_CD = 4 AND LAST_SBN_JIGYOUJOU_NEXT_ELEC.GENBA_CD <= /*dto.LastShobunJigyoujouCdTo*/'' ) )
                  )
                  OR (
                    (DR18EX.SBN_ENDREP_KBN IS NOT NULL AND DR18EX.SBN_ENDREP_KBN != '' AND DR18EX.SBN_ENDREP_KBN = 2)
                    OR SBN_JIGYOUBA.GENBA_CD <= /*dto.LastShobunJigyoujouCdTo*/''
                  )
            )
        /*END*/
    -- ELSE
        /*IF dto.LastShobunJigyoujouCdFrom != null && dto.LastShobunJigyoujouCdFrom != ''*/AND LAST_SBN_JIGYOUJOU.GENBA_CD >= /*dto.LastShobunJigyoujouCdTo*/''/*END*/
        /*IF dto.LastShobunJigyoujouCdTo != null && dto.LastShobunJigyoujouCdTo != ''*/AND LAST_SBN_JIGYOUJOU.GENBA_CD <= /*dto.LastShobunJigyoujouCdTo*/''/*END*/
    /*END*/

    /*IF dto.HoukokushoBunruiCdFrom != null && dto.HoukokushoBunruiCdFrom != ''*/AND ((DR18MIX.SYSTEM_ID IS NULL AND MDHS.HOUKOKUSHO_BUNRUI_CD >= /*dto.HoukokushoBunruiCdFrom*/'') OR (DR18MIX.SYSTEM_ID IS NOT NULL AND MDHS_MIX.HOUKOKUSHO_BUNRUI_CD >= /*dto.HoukokushoBunruiCdFrom*/''))/*END*/
    /*IF dto.HoukokushoBunruiCdTo != null && dto.HoukokushoBunruiCdTo != ''*/AND ((DR18MIX.SYSTEM_ID IS NULL AND MDHS.HOUKOKUSHO_BUNRUI_CD <= /*dto.HoukokushoBunruiCdTo*/'') OR (DR18MIX.SYSTEM_ID IS NOT NULL AND MDHS_MIX.HOUKOKUSHO_BUNRUI_CD <= /*dto.HoukokushoBunruiCdTo*/''))/*END*/

    /*IF dto.HaikibutsuMeishouCdFrom != null && dto.HaikibutsuMeishouCdFrom != ''*/AND ((DR18MIX.SYSTEM_ID IS NULL AND DR18EX.HAIKI_NAME_CD >= /*dto.HaikibutsuMeishouCdFrom*/'') OR (DR18MIX.SYSTEM_ID IS NOT NULL AND DR18MIX.HAIKI_NAME_CD >= /*dto.HaikibutsuMeishouCdFrom*/''))/*END*/
    /*IF dto.HaikibutsuMeishouCdTo != null && dto.HaikibutsuMeishouCdTo != ''*/AND ((DR18MIX.SYSTEM_ID IS NULL AND DR18EX.HAIKI_NAME_CD <= /*dto.HaikibutsuMeishouCdTo*/'') OR (DR18MIX.SYSTEM_ID IS NOT NULL AND DR18MIX.HAIKI_NAME_CD <= /*dto.HaikibutsuMeishouCdTo*/''))/*END*/

    /*IF dto.NisugataCdFrom != null && dto.NisugataCdFrom != ''*/AND DR18.NISUGATA_CODE >= /*dto.NisugataCdFrom*/''/*END*/
    /*IF dto.NisugataCdTo != null && dto.NisugataCdTo != ''*/AND DR18.NISUGATA_CODE <= /*dto.NisugataCdTo*/''/*END*/

    /*IF dto.ShobunHouhouCdFrom != null && dto.ShobunHouhouCdFrom != ''*/AND ((DR18MIX.SYSTEM_ID IS NULL AND DR18.SBN_WAY_CODE >= /*dto.ShobunHouhouCdFrom*/'') OR (DR18MIX.SYSTEM_ID IS NOT NULL AND DR18MIX.SBN_HOUHOU_CD >= /*dto.ShobunHouhouCdFrom*/''))/*END*/
    /*IF dto.ShobunHouhouCdTo != null && dto.ShobunHouhouCdTo != ''*/AND ((DR18MIX.SYSTEM_ID IS NULL AND DR18.SBN_WAY_CODE <= /*dto.ShobunHouhouCdTo*/'') OR (DR18MIX.SYSTEM_ID IS NOT NULL AND DR18MIX.SBN_HOUHOU_CD <= /*dto.ShobunHouhouCdTo*/''))/*END*/
    /*END*/
) AS MANIFEST

ORDER BY

/*IF dto.Sort == 1*/ KOUFU_DATE, MANIFEST_ID, ID /*END*/
/*IF dto.Sort == 2*/ UPN_END_DATE, MANIFEST_ID, ID /*END*/
/*IF dto.Sort == 3*/ SBN_END_DATE, MANIFEST_ID, ID /*END*/
/*IF dto.Sort == 4*/ LAST_SBN_END_DATE, MANIFEST_ID, ID /*END*/
/*IF dto.Sort == 5*/ HST_GYOUSHA_CD, HST_GENBA_CD, KOUFU_DATE, MANIFEST_ID, ID /*END*/
/*IF dto.Sort == 6*/ UPN_GYOUSHA_CD1, KOUFU_DATE, MANIFEST_ID, ID /*END*/
/*IF dto.Sort == 7*/ UPN_GYOUSHA_CD2, KOUFU_DATE, MANIFEST_ID, ID /*END*/
/*IF dto.Sort == 8*/ SBN_GYOUSHA_CD, UPN_SAKI_GENBA_CD, KOUFU_DATE, MANIFEST_ID, ID /*END*/
/*IF dto.Sort == 9*/ LAST_SBN_GYOUSHA_CD, LAST_SBN_GENBA_CD, KOUFU_DATE, MANIFEST_ID, ID /*END*/
/*IF dto.Sort == 10*/HOUKOKUSHO_BUNRUI_CD, HAIKI_SHURUI_CD, KOUFU_DATE, MANIFEST_ID, ID /*END*/
/*IF dto.Sort == 11*/SBN_HOUHOU_CD, KOUFU_DATE, MANIFEST_ID, ID /*END*/
