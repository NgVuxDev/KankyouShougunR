SELECT *
FROM
(
SELECT DISTINCT 
'' AS SHORI_KBN,          
KIHON.SYSTEM_ID AS ITAKU_KEIYAKU_SYSTEM_ID,
KIHON.ITAKU_KEIYAKU_NO,
KIHON.ITAKU_KEIYAKU_SHURUI, 
KIHON.KOUSHIN_SHUBETSU   ,  
KIHON.YUUKOU_BEGIN AS ITAKU_KEIYAKU_DATE_BEGIN,
KIHON.YUUKOU_END AS ITAKU_KEIYAKU_DATE_END,  
KIHON.SHOBUN_PATTERN_SYSTEM_ID AS SHOBUN_PATTERN_SYSYTEM_ID,
KIHON.SHOBUN_PATTERN_SEQ,
KIHON.SHOBUN_PATTERN_NAME,  
KIHON.LAST_SHOBUN_PATTERN_SYSTEM_ID AS LAST_SHOBUN_PATTERN_SYSYTEM_ID,
KIHON.LAST_SHOBUN_PATTERN_SEQ, 
KIHON.LAST_SHOBUN_PATTERN_NAME,  
GYOUSHA.GYOUSHA_CD,
GYOUSHA.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME, 
GYOUSHA.GYOUSHA_ADDRESS1 AS GYOUSHA_ADDRESS, 
GENBA.GENBA_CD,  
GENBA.GENBA_NAME_RYAKU AS GENBA_NAME,  
GENBA.GENBA_ADDRESS1 AS GENBA_ADDRESS,
'' AS TIME_STAMP
FROM             
M_ITAKU_KEIYAKU_KIHON AS KIHON 
LEFT OUTER JOIN
M_GYOUSHA AS GYOUSHA 
ON 
KIHON.HAISHUTSU_JIGYOUSHA_CD = GYOUSHA.GYOUSHA_CD 
AND GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN = 1 
LEFT OUTER JOIN
M_GENBA AS GENBA 
ON 
KIHON.HAISHUTSU_JIGYOUSHA_CD = GENBA.GYOUSHA_CD 
AND KIHON.HAISHUTSU_JIGYOUJOU_CD = GENBA.GENBA_CD 
AND GENBA.HAISHUTSU_NIZUMI_GENBA_KBN = 1 
/*IF data.Unpan_Gyousha_Cd != null && data.Unpan_Gyousha_Cd != ''*/
INNER JOIN
M_ITAKU_KEIYAKU_BETSU2 AS UNPAN 
ON 
KIHON.SYSTEM_ID = UNPAN.SYSTEM_ID 
AND KIHON.ITAKU_KEIYAKU_NO = UNPAN.ITAKU_KEIYAKU_NO 
LEFT OUTER JOIN
M_GYOUSHA AS UNGYOUSHA 
ON 
UNPAN.UNPAN_GYOUSHA_CD = UNGYOUSHA.GYOUSHA_CD 
AND UNGYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN = 1
/*END*/
/*BEGIN*/WHERE
/*IF !deletechuFlg*/ KIHON.DELETE_FLG = 0 /*END*/
/*IF data.Hst_Gyousha_Cd != null && data.Hst_Gyousha_Cd != ''*/
  AND GYOUSHA.GYOUSHA_CD   = /*data.Hst_Gyousha_Cd*//*END*/
/*END*/
/*IF data.Hst_Genba_Cd != null && data.Hst_Genba_Cd != ''*/
  AND GENBA.GENBA_CD = /*data.Hst_Genba_Cd*//*END*/
/*END*/
/*IF data.Unpan_Gyousha_Cd != null && data.Unpan_Gyousha_Cd != ''*/
  AND UNPAN.UNPAN_GYOUSHA_CD = /*data.Unpan_Gyousha_Cd*//*END*/
/*END*/
/*IF data.Shobun_Pattern_Name != null && data.Shobun_Pattern_Name != ''*/
  AND KIHON.SHOBUN_PATTERN_NAME = /*data.Shobun_Pattern_Name*//*END*/
/*END*/
/*IF data.Last_Shobun_Pattern_Name != null && data.Last_Shobun_Pattern_Name != ''*/
  AND KIHON.LAST_SHOBUN_PATTERN_NAME = /*data.Last_Shobun_Pattern_Name*//*END*/
/*END*/
-- 契約開始日終了日の絞り込み条件
/*IF (data.Keiyaku_Begin != null && data.Keiyaku_Begin != '') || (data.Keiyaku_Begin_To != null && data.Keiyaku_Begin_To != '')*/
  AND ( KIHON.YUUKOU_BEGIN IS NULL OR (
    /*IF data.Keiyaku_Begin != null && data.Keiyaku_Begin != ''*/
        KIHON.YUUKOU_BEGIN >= CONVERT(DATETIME, /*data.Keiyaku_Begin*/null, 120)
    -- ELSE
        1 = 1
    /*END*/
    /*IF data.Keiyaku_Begin_To != null && data.Keiyaku_Begin_To != ''*/
        AND KIHON.YUUKOU_BEGIN <= CONVERT(DATETIME, /*data.Keiyaku_Begin_To*/null, 120)
    -- ELSE
        AND 1 = 1
    /*END*/
  ) )
/*END*/
/*IF (data.Keiyaku_End != null && data.Keiyaku_End != '') || (data.Keiyaku_End_To != null && data.Keiyaku_End_To != '')*/
  AND (
        -- 更新種別：単発
        (KIHON.KOUSHIN_SHUBETSU != 1
          /*IF data.Keiyaku_End_To == null || data.Keiyaku_End_To == ''*/
            AND KIHON.YUUKOU_END IS NULL OR (
          -- ELSE
            AND 
          /*END*/
            /*IF data.Keiyaku_End != null && data.Keiyaku_End != ''*/
                KIHON.YUUKOU_END >=  CONVERT(DATETIME, /*data.Keiyaku_End*/null, 120)
            -- ELSE
                1 = 1
            /*END*/
            /*IF data.Keiyaku_End_To != null && data.Keiyaku_End_To != ''*/
                AND KIHON.YUUKOU_END <=  CONVERT(DATETIME, /*data.Keiyaku_End_To*/null, 120)
            -- ELSE
                AND 1 = 1
            /*END*/
          /*IF data.Keiyaku_End_To == null || data.Keiyaku_End_To == ''*/
            )
          /*END*/
        )
        -- 更新種別：自動
        OR (KIHON.KOUSHIN_SHUBETSU = 1
          /*IF data.Keiyaku_End_To == null || data.Keiyaku_End_To == ''*/
            AND KIHON.KOUSHIN_END_DATE IS NULL OR (
          -- ELSE
            AND
          /*END*/
            /*IF data.Keiyaku_End != null && data.Keiyaku_End != ''*/
                KIHON.KOUSHIN_END_DATE >=  CONVERT(DATETIME, /*data.Keiyaku_End*/null, 120)
            -- ELSE
                1 = 1
            /*END*/
            /*IF data.Keiyaku_End_To != null && data.Keiyaku_End_To != ''*/
                AND KIHON.KOUSHIN_END_DATE <=  CONVERT(DATETIME, /*data.Keiyaku_End_To*/null, 120)
            -- ELSE
                AND 1 = 1
            /*END*/
          /*IF data.Keiyaku_End_To == null || data.Keiyaku_End_To == ''*/
            )
          /*END*/
        )
  )
/*END*/

/*IF data.Update_Shubetsu != null && data.Update_Shubetsu != ''*/
  AND KIHON.KOUSHIN_SHUBETSU = /*data.Update_Shubetsu*//*END*/
/*END*/
/*IF data.Keiyakusho_Shurui != null && data.Keiyakusho_Shurui != ''*/
  AND KIHON.ITAKU_KEIYAKU_SHURUI = /*data.Keiyakusho_Shurui*//*END*/
/*END*/

UNION

SELECT DISTINCT  
'' AS SHORI_KBN,         
KIHON1.SYSTEM_ID AS ITAKU_KEIYAKU_SYSTEM_ID, 
KIHON1.ITAKU_KEIYAKU_NO, 
KIHON1.ITAKU_KEIYAKU_SHURUI,
KIHON1.KOUSHIN_SHUBETSU  , 
KIHON1.YUUKOU_BEGIN AS ITAKU_KEIYAKU_DATE_BEGIN,
KIHON1.YUUKOU_END AS ITAKU_KEIYAKU_DATE_END, 
KIHON1.SHOBUN_PATTERN_SYSTEM_ID AS SHOBUN_PATTERN_SYSYTEM_ID,
KIHON1.SHOBUN_PATTERN_SEQ,
KIHON1.SHOBUN_PATTERN_NAME, 
KIHON1.LAST_SHOBUN_PATTERN_SYSTEM_ID AS LAST_SHOBUN_PATTERN_SYSYTEM_ID,
KIHON1.LAST_SHOBUN_PATTERN_SEQ,
KIHON1.LAST_SHOBUN_PATTERN_NAME, 
M_GENBA.GYOUSHA_CD, 
M_GYOUSHA.GYOUSHA_NAME_RYAKU, 
M_GYOUSHA.GYOUSHA_ADDRESS1 AS GYOUSHA_ADDRESS,
M_GENBA.GENBA_CD,
M_GENBA.GENBA_NAME_RYAKU,
M_GENBA.GENBA_ADDRESS1 AS GENBA_ADDRESS,
'' AS TIME_STAMP
FROM             
 M_ITAKU_KEIYAKU_KIHON AS KIHON1 
INNER JOIN
M_ITAKU_KEIYAKU_KIHON_HST_GENBA AS HSTGENBA 
ON KIHON1.SYSTEM_ID = HSTGENBA.SYSTEM_ID 
AND    KIHON1.ITAKU_KEIYAKU_NO = HSTGENBA.ITAKU_KEIYAKU_NO 
AND KIHON1.DELETE_FLG = 0
LEFT OUTER JOIN
M_GENBA 
ON M_GENBA.GENBA_CD = HSTGENBA.HAISHUTSU_JIGYOUJOU_CD 
AND M_GENBA.GYOUSHA_CD = HSTGENBA.HAISHUTSU_JIGYOUSHA_CD 
AND M_GENBA.HAISHUTSU_NIZUMI_GENBA_KBN = 1 
LEFT OUTER JOIN
M_GYOUSHA ON M_GYOUSHA.GYOUSHA_CD = M_GENBA.GYOUSHA_CD
 /*BEGIN*/WHERE
 /*IF !deletechuFlg*/ KIHON1.DELETE_FLG = 0 /*END*/
/*IF data.Hst_Genba_Cd != null && data.Hst_Genba_Cd != ''*/
 AND HSTGENBA.HAISHUTSU_JIGYOUJOU_CD= /*data.Hst_Genba_Cd*//*END*/
  /*END*/
/*IF data.Hst_Gyousha_Cd != null && data.Hst_Gyousha_Cd != ''*/
  AND HSTGENBA.HAISHUTSU_JIGYOUSHA_CD   = /*data.Hst_Gyousha_Cd*//*END*/
  /*END*/

/*IF data.Shobun_Pattern_Name != null && data.Shobun_Pattern_Name != ''*/
  AND KIHON1.SHOBUN_PATTERN_NAME = /*data.Shobun_Pattern_Name*//*END*/
/*END*/
/*IF data.Last_Shobun_Pattern_Name != null && data.Last_Shobun_Pattern_Name != ''*/
  AND KIHON1.LAST_SHOBUN_PATTERN_NAME = /*data.Last_Shobun_Pattern_Name*//*END*/
/*END*/
-- 契約開始日終了日の絞り込み条件
/*IF (data.Keiyaku_Begin != null && data.Keiyaku_Begin != '') || (data.Keiyaku_Begin_To != null && data.Keiyaku_Begin_To != '')*/
  AND ( KIHON1.YUUKOU_BEGIN IS NULL OR (
    /*IF data.Keiyaku_Begin != null && data.Keiyaku_Begin != ''*/
        KIHON1.YUUKOU_BEGIN >= CONVERT(DATETIME, /*data.Keiyaku_Begin*/null, 120)
    -- ELSE
        1 = 1
    /*END*/
    /*IF data.Keiyaku_Begin_To != null && data.Keiyaku_Begin_To != ''*/
        AND KIHON1.YUUKOU_BEGIN <= CONVERT(DATETIME, /*data.Keiyaku_Begin_To*/null, 120)
    -- ELSE
        AND 1 = 1
    /*END*/
  ) )
/*END*/
/*IF (data.Keiyaku_End != null && data.Keiyaku_End != '') || (data.Keiyaku_End_To != null && data.Keiyaku_End_To != '')*/
  AND (
        -- 更新種別：単発
        (KIHON1.KOUSHIN_SHUBETSU != 1
          /*IF data.Keiyaku_End_To == null || data.Keiyaku_End_To == ''*/
            AND KIHON1.YUUKOU_END IS NULL OR (
          -- ELSE
            AND 
          /*END*/
            /*IF data.Keiyaku_End != null && data.Keiyaku_End != ''*/
                KIHON1.YUUKOU_END >=  CONVERT(DATETIME, /*data.Keiyaku_End*/null, 120)
            -- ELSE
                1 = 1
            /*END*/
            /*IF data.Keiyaku_End_To != null && data.Keiyaku_End_To != ''*/
                AND KIHON1.YUUKOU_END <=  CONVERT(DATETIME, /*data.Keiyaku_End_To*/null, 120)
            -- ELSE
                AND 1 = 1
            /*END*/
          /*IF data.Keiyaku_End_To == null || data.Keiyaku_End_To == ''*/
            )
          /*END*/
        )
        -- 更新種別：自動
        OR (KIHON1.KOUSHIN_SHUBETSU = 1
          /*IF data.Keiyaku_End_To == null || data.Keiyaku_End_To == ''*/
            AND KIHON1.KOUSHIN_END_DATE IS NULL OR (
          -- ELSE
            AND
          /*END*/
            /*IF data.Keiyaku_End != null && data.Keiyaku_End != ''*/
                KIHON1.KOUSHIN_END_DATE >=  CONVERT(DATETIME, /*data.Keiyaku_End*/null, 120)
            -- ELSE
                1 = 1
            /*END*/
            /*IF data.Keiyaku_End_To != null && data.Keiyaku_End_To != ''*/
                AND KIHON1.KOUSHIN_END_DATE <=  CONVERT(DATETIME, /*data.Keiyaku_End_To*/null, 120)
            -- ELSE
                AND 1 = 1
            /*END*/
          /*IF data.Keiyaku_End_To == null || data.Keiyaku_End_To == ''*/
            )
          /*END*/
        )
  )
/*END*/

/*IF data.Update_Shubetsu != null && data.Update_Shubetsu != ''*/
  AND KIHON1.KOUSHIN_SHUBETSU = /*data.Update_Shubetsu*//*END*/
/*END*/
/*IF data.Keiyakusho_Shurui != null && data.Keiyakusho_Shurui != ''*/
  AND KIHON1.ITAKU_KEIYAKU_SHURUI = /*data.Keiyakusho_Shurui*//*END*/
/*END*/
/*END*/
) AS A
ORDER BY    
A.ITAKU_KEIYAKU_SYSTEM_ID, A.ITAKU_KEIYAKU_NO