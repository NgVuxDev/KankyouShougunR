SELECT
    DETAIL.ZAIKO_HINMEI_CD,
    HINMEI.ZAIKO_HINMEI_NAME_RYAKU AS ZAIKO_HINMEI_NAME
    FROM 
(
    --受入量
    SELECT
        MZH.ZAIKO_HINMEI_CD
    FROM T_UKEIRE_ENTRY TUE
    INNER JOIN T_UKEIRE_DETAIL TUD 
    ON TUE.SYSTEM_ID = TUD.SYSTEM_ID
    AND TUE.SEQ = TUD.SEQ
    INNER JOIN T_ZAIKO_HINMEI_HURIWAKE MZH
    ON MZH.SYSTEM_ID = TUD.SYSTEM_ID
    AND MZH.SEQ = TUD.SEQ
    AND MZH.DETAIL_SYSTEM_ID = TUD.DETAIL_SYSTEM_ID
	AND MZH.DENSHU_KBN_CD = 1
    WHERE  1 = 1
	/*IF data.zaikoHinmeiFrom != null && data.zaikoHinmeiFrom!=''*/
    AND MZH.ZAIKO_HINMEI_CD >= /*data.zaikoHinmeiFrom*/
	/*END*/
	/*IF data.zaikoHinmeiTo != null && data.zaikoHinmeiTo!=''*/
    AND MZH.ZAIKO_HINMEI_CD <= /*data.zaikoHinmeiTo*/
	/*END*/
    AND ((TUE.DENPYOU_DATE >= /*data.dateFrom*/ AND TUE.DENPYOU_DATE <= /*data.dateTo*/)
	    OR (ZAIKO_RYOU != 0 AND TUE.DENPYOU_DATE < /*data.dateFrom*/))
	AND TUE.DELETE_FLG = 0
    GROUP BY ZAIKO_HINMEI_CD
    
    UNION 

    --出荷量
    SELECT
        MZH.ZAIKO_HINMEI_CD
    FROM T_SHUKKA_ENTRY TSE
    INNER JOIN T_SHUKKA_DETAIL TSD 
    ON TSE.SYSTEM_ID = TSD.SYSTEM_ID
    AND TSE.SEQ = TSD.SEQ
    INNER JOIN T_ZAIKO_HINMEI_HURIWAKE MZH
    ON MZH.SYSTEM_ID = TSD.SYSTEM_ID
    AND MZH.SEQ = TSD.SEQ
    AND MZH.DETAIL_SYSTEM_ID = TSD.DETAIL_SYSTEM_ID
	AND MZH.DENSHU_KBN_CD = 2
    WHERE  1 = 1
	/*IF data.zaikoHinmeiFrom != null && data.zaikoHinmeiFrom!=''*/
    AND MZH.ZAIKO_HINMEI_CD >= /*data.zaikoHinmeiFrom*/
	/*END*/
	/*IF data.zaikoHinmeiTo != null && data.zaikoHinmeiTo!=''*/
    AND MZH.ZAIKO_HINMEI_CD <= /*data.zaikoHinmeiTo*/
	/*END*/
    AND ((TSE.DENPYOU_DATE >= /*data.dateFrom*/ AND TSE.DENPYOU_DATE <= /*data.dateTo*/)
	    OR(ZAIKO_RYOU != 0 AND TSE.DENPYOU_DATE < /*data.dateFrom*/))
	AND TSE.DELETE_FLG = 0
    GROUP BY ZAIKO_HINMEI_CD
    
    UNION 

    --調整量
    SELECT
        TZTD.ZAIKO_HINMEI_CD
    FROM T_ZAIKO_TYOUSEI_ENTRY TZTE
    INNER JOIN T_ZAIKO_TYOUSEI_DETAIL TZTD
    ON TZTE.SYSTEM_ID = TZTD.SYSTEM_ID
    AND TZTE.SEQ = TZTD.SEQ
    WHERE 1 = 1
	/*IF data.zaikoHinmeiFrom != null && data.zaikoHinmeiFrom!=''*/
    AND TZTD.ZAIKO_HINMEI_CD >= /*data.zaikoHinmeiFrom*/
	/*END*/
	/*IF data.zaikoHinmeiTo != null && data.zaikoHinmeiTo!=''*/
    AND TZTD.ZAIKO_HINMEI_CD <= /*data.zaikoHinmeiTo*/
	/*END*/
    AND ((TZTE.TYOUSEI_DATE >= /*data.dateFrom*/ AND TZTE.TYOUSEI_DATE <= /*data.dateTo*/)
	    OR (TYOUSEI_RYOU != 0 AND TZTE.TYOUSEI_DATE < /*data.dateFrom*/))
	AND TZTE.DELETE_FLG = 0
    GROUP BY ZAIKO_HINMEI_CD
	
    UNION 

    SELECT
        TMLZ.ZAIKO_HINMEI_CD
    FROM T_MONTHLY_LOCK_ZAIKO TMLZ
    WHERE YEAR <= YEAR(/*data.dateFrom*/)
    AND  MONTH <= MONTH(/*data.dateFrom*/)
	/*IF data.zaikoHinmeiFrom != null && data.zaikoHinmeiFrom!=''*/
    AND TMLZ.ZAIKO_HINMEI_CD >= /*data.zaikoHinmeiFrom*/
	/*END*/
	/*IF data.zaikoHinmeiTo != null && data.zaikoHinmeiTo!=''*/
    AND TMLZ.ZAIKO_HINMEI_CD <= /*data.zaikoHinmeiTo*/
	/*END*/
    AND TMLZ.DELETE_FLG = 0
	
    UNION 

    SELECT
        MKZI.ZAIKO_HINMEI_CD
    FROM M_KAISHI_ZAIKO_INFO MKZI
    --WHERE CONVERT(DATE, ISNULL(TEKIYOU_BEGIN, DATEADD(day,-1,GETDATE()))) <= CONVERT(DATE, GETDATE()) and CONVERT(DATE, GETDATE()) <= CONVERT(DATE, ISNULL(TEKIYOU_END, DATEADD(day,1,GETDATE())))
	WHERE 1 = 1
	/*IF data.zaikoHinmeiFrom != null && data.zaikoHinmeiFrom!=''*/
    AND MKZI.ZAIKO_HINMEI_CD >= /*data.zaikoHinmeiFrom*/
	/*END*/
	/*IF data.zaikoHinmeiTo != null && data.zaikoHinmeiTo!=''*/
    AND MKZI.ZAIKO_HINMEI_CD <= /*data.zaikoHinmeiTo*/
	/*END*/
) AS DETAIL
LEFT JOIN M_ZAIKO_HINMEI HINMEI
ON HINMEI.ZAIKO_HINMEI_CD = DETAIL.ZAIKO_HINMEI_CD
ORDER BY
    DETAIL.ZAIKO_HINMEI_CD