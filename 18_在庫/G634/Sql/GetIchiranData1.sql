SELECT distinct
    DETAIL.GYOUSHA_CD,
    MGY.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME,
    DETAIL.GENBA_CD,
    MGE.GENBA_NAME_RYAKU AS GENBA_NAME,
    DETAIL.ZAIKO_HINMEI_CD,
    HINMEI.ZAIKO_HINMEI_NAME_RYAKU AS ZAIKO_HINMEI_NAME
 FROM 
(
    --受入量
    SELECT TUE.NIOROSHI_GYOUSHA_CD AS GYOUSHA_CD,
        TUE.NIOROSHI_GENBA_CD AS GENBA_CD,
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
	/*IF data.gyoushaFrom != null && data.gyoushaFrom!=''*/
    AND TUE.NIOROSHI_GYOUSHA_CD >= /*data.gyoushaFrom*/
	/*END*/
	/*IF data.gyoushaTo != null && data.gyoushaTo!=''*/
    AND TUE.NIOROSHI_GYOUSHA_CD <= /*data.gyoushaTo*/
	/*END*/
	/*IF data.genbaFrom != null && data.genbaFrom!=''*/
    AND TUE.NIOROSHI_GENBA_CD >= /*data.genbaFrom*/
	/*END*/
	/*IF data.genbaTo != null && data.genbaTo!=''*/
    AND TUE.NIOROSHI_GENBA_CD <= /*data.genbaTo*/
	/*END*/
	/*IF data.zaikoHinmeiFrom != null && data.zaikoHinmeiFrom!=''*/
    AND MZH.ZAIKO_HINMEI_CD >= /*data.zaikoHinmeiFrom*/
	/*END*/
	/*IF data.zaikoHinmeiTo != null && data.zaikoHinmeiTo!=''*/
    AND MZH.ZAIKO_HINMEI_CD <= /*data.zaikoHinmeiTo*/
	/*END*/
    AND ((TUE.DENPYOU_DATE >= /*data.dateFrom*/ AND TUE.DENPYOU_DATE <= /*data.dateTo*/)
	    OR (ZAIKO_RYOU != 0 AND TUE.DENPYOU_DATE < /*data.dateFrom*/))
	AND TUE.DELETE_FLG = 0
    
    UNION 

    --出荷量
    SELECT
        TSE.NIZUMI_GYOUSHA_CD AS GYOUSHA_CD,
        TSE.NIZUMI_GENBA_CD AS GENBA_CD,
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
	/*IF data.gyoushaFrom != null && data.gyoushaFrom!=''*/
    AND TSE.NIZUMI_GYOUSHA_CD >= /*data.gyoushaFrom*/
	/*END*/
	/*IF data.gyoushaTo != null && data.gyoushaTo!=''*/
    AND TSE.NIZUMI_GYOUSHA_CD <= /*data.gyoushaTo*/
	/*END*/
	/*IF data.genbaFrom != null && data.genbaFrom!=''*/
    AND TSE.NIZUMI_GENBA_CD >= /*data.genbaFrom*/
	/*END*/
	/*IF data.genbaTo != null && data.genbaTo!=''*/
    AND TSE.NIZUMI_GENBA_CD <= /*data.genbaTo*/
	/*END*/
	/*IF data.zaikoHinmeiFrom != null && data.zaikoHinmeiFrom!=''*/
    AND MZH.ZAIKO_HINMEI_CD >= /*data.zaikoHinmeiFrom*/
	/*END*/
	/*IF data.zaikoHinmeiTo != null && data.zaikoHinmeiTo!=''*/
    AND MZH.ZAIKO_HINMEI_CD <= /*data.zaikoHinmeiTo*/
	/*END*/
    AND ((TSE.DENPYOU_DATE >= /*data.dateFrom*/ AND TSE.DENPYOU_DATE <= /*data.dateTo*/)
	    OR(ZAIKO_RYOU != 0 AND TSE.DENPYOU_DATE < /*data.dateFrom*/))
	AND TSE.DELETE_FLG = 0
    
    UNION 

    --調整量
    SELECT
        TZTE.GYOUSHA_CD,
        TZTE.GENBA_CD,
        TZTD.ZAIKO_HINMEI_CD
    FROM T_ZAIKO_TYOUSEI_ENTRY TZTE
    INNER JOIN T_ZAIKO_TYOUSEI_DETAIL TZTD
    ON TZTE.SYSTEM_ID = TZTD.SYSTEM_ID
    AND TZTE.SEQ = TZTD.SEQ
    WHERE 1 = 1
	/*IF data.gyoushaFrom != null && data.gyoushaFrom!=''*/
    AND TZTE.GYOUSHA_CD >= /*data.gyoushaFrom*/
	/*END*/
	/*IF data.gyoushaTo != null && data.gyoushaTo!=''*/
    AND TZTE.GYOUSHA_CD <= /*data.gyoushaTo*/
	/*END*/
	/*IF data.genbaFrom != null && data.genbaFrom!=''*/
    AND TZTE.GENBA_CD >= /*data.genbaFrom*/
	/*END*/
	/*IF data.genbaTo != null && data.genbaTo!=''*/
    AND TZTE.GENBA_CD <= /*data.genbaTo*/
	/*END*/
	/*IF data.zaikoHinmeiFrom != null && data.zaikoHinmeiFrom!=''*/
    AND TZTD.ZAIKO_HINMEI_CD >= /*data.zaikoHinmeiFrom*/
	/*END*/
	/*IF data.zaikoHinmeiTo != null && data.zaikoHinmeiTo!=''*/
    AND TZTD.ZAIKO_HINMEI_CD <= /*data.zaikoHinmeiTo*/
	/*END*/
    AND ((TZTE.TYOUSEI_DATE >= /*data.dateFrom*/ AND TZTE.TYOUSEI_DATE <= /*data.dateTo*/)
	    OR(TYOUSEI_RYOU != 0 AND TZTE.TYOUSEI_DATE < /*data.dateFrom*/))
	AND TZTE.DELETE_FLG = 0
    
    UNION 

    --移動量
    --該当現場から移動する移動量
    SELECT
        TZIE.GYOUSHA_CD,
        TZIE.GENBA_CD,
        TZIE.ZAIKO_HINMEI_CD
    FROM T_ZAIKO_IDOU_ENTRY TZIE
    INNER JOIN T_ZAIKO_IDOU_DETAIL TZID
    ON TZIE.SYSTEM_ID = TZID.SYSTEM_ID
    AND TZIE.SEQ = TZID.SEQ
    WHERE  1 = 1
	/*IF data.gyoushaFrom != null && data.gyoushaFrom!=''*/
    AND TZIE.GYOUSHA_CD >= /*data.gyoushaFrom*/
	/*END*/
	/*IF data.gyoushaTo != null && data.gyoushaTo!=''*/
    AND TZIE.GYOUSHA_CD <= /*data.gyoushaTo*/
	/*END*/
	/*IF data.genbaFrom != null && data.genbaFrom!=''*/
    AND TZIE.GENBA_CD >= /*data.genbaFrom*/
	/*END*/
	/*IF data.genbaTo != null && data.genbaTo!=''*/
    AND TZIE.GENBA_CD <= /*data.genbaTo*/
	/*END*/
	/*IF data.zaikoHinmeiFrom != null && data.zaikoHinmeiFrom!=''*/
    AND TZIE.ZAIKO_HINMEI_CD >= /*data.zaikoHinmeiFrom*/
	/*END*/
	/*IF data.zaikoHinmeiTo != null && data.zaikoHinmeiTo!=''*/
    AND TZIE.ZAIKO_HINMEI_CD <= /*data.zaikoHinmeiTo*/
	/*END*/
    AND ((TZIE.IDOU_DATE >= /*data.dateFrom*/ AND TZIE.IDOU_DATE <= /*data.dateTo*/)
	    OR(IDOU_RYOU != 0 AND TZIE.IDOU_DATE < /*data.dateFrom*/ AND TZIE.DELETE_FLG = 0))
	AND TZIE.DELETE_FLG = 0

    UNION 

    --該当現場に移動する移動量
    SELECT 
        TZIE.GYOUSHA_CD,
        TZID.GENBA_CD,
        TZIE.ZAIKO_HINMEI_CD
    FROM T_ZAIKO_IDOU_ENTRY TZIE
    INNER JOIN T_ZAIKO_IDOU_DETAIL TZID
    ON TZIE.SYSTEM_ID = TZID.SYSTEM_ID
    AND TZIE.SEQ = TZID.SEQ
    WHERE  1 = 1
	/*IF data.gyoushaFrom != null && data.gyoushaFrom!=''*/
    AND TZIE.GYOUSHA_CD >= /*data.gyoushaFrom*/
	/*END*/
	/*IF data.gyoushaTo != null && data.gyoushaTo!=''*/
    AND TZIE.GYOUSHA_CD <= /*data.gyoushaTo*/
	/*END*/
	/*IF data.genbaFrom != null && data.genbaFrom!=''*/
    AND TZID.GENBA_CD >= /*data.genbaFrom*/
	/*END*/
	/*IF data.genbaTo != null && data.genbaTo!=''*/
    AND TZID.GENBA_CD <= /*data.genbaTo*/
	/*END*/
	/*IF data.zaikoHinmeiFrom != null && data.zaikoHinmeiFrom!=''*/
    AND TZIE.ZAIKO_HINMEI_CD >= /*data.zaikoHinmeiFrom*/
	/*END*/
	/*IF data.zaikoHinmeiTo != null && data.zaikoHinmeiTo!=''*/
    AND TZIE.ZAIKO_HINMEI_CD <= /*data.zaikoHinmeiTo*/
	/*END*/
    AND ((TZIE.IDOU_DATE >= /*data.dateFrom*/ AND TZIE.IDOU_DATE <= /*data.dateTo*/)
	    OR(IDOU_RYOU != 0 AND  TZIE.IDOU_DATE < /*data.dateFrom*/ AND TZIE.DELETE_FLG = 0))
	AND TZIE.DELETE_FLG = 0
	
    UNION 

    SELECT
        TMLZ.GYOUSHA_CD,
        TMLZ.GENBA_CD,
        TMLZ.ZAIKO_HINMEI_CD
    FROM T_MONTHLY_LOCK_ZAIKO TMLZ
    WHERE YEAR <= YEAR(/*data.dateFrom*/)
    AND  MONTH <= MONTH(/*data.dateFrom*/)
	/*IF data.gyoushaFrom != null && data.gyoushaFrom!=''*/
    AND TMLZ.GYOUSHA_CD >= /*data.gyoushaFrom*/
	/*END*/
	/*IF data.gyoushaTo != null && data.gyoushaTo!=''*/
    AND TMLZ.GYOUSHA_CD <= /*data.gyoushaTo*/
	/*END*/
	/*IF data.genbaFrom != null && data.genbaFrom!=''*/
    AND TMLZ.GENBA_CD >= /*data.genbaFrom*/
	/*END*/
	/*IF data.genbaTo != null && data.genbaTo!=''*/
    AND TMLZ.GENBA_CD <= /*data.genbaTo*/
	/*END*/
	/*IF data.zaikoHinmeiFrom != null && data.zaikoHinmeiFrom!=''*/
    AND TMLZ.ZAIKO_HINMEI_CD >= /*data.zaikoHinmeiFrom*/
	/*END*/
	/*IF data.zaikoHinmeiTo != null && data.zaikoHinmeiTo!=''*/
    AND TMLZ.ZAIKO_HINMEI_CD <= /*data.zaikoHinmeiTo*/
	/*END*/
    AND TMLZ.DELETE_FLG = 0
	
    UNION 

    SELECT
        MKZI.GYOUSHA_CD,
        MKZI.GENBA_CD,
        MKZI.ZAIKO_HINMEI_CD
    FROM M_KAISHI_ZAIKO_INFO MKZI
    --WHERE CONVERT(DATE, ISNULL(TEKIYOU_BEGIN, DATEADD(day,-1,GETDATE()))) <= CONVERT(DATE, GETDATE()) and CONVERT(DATE, GETDATE()) <= CONVERT(DATE, ISNULL(TEKIYOU_END, DATEADD(day,1,GETDATE())))
	WHERE 1 = 1
	/*IF data.gyoushaFrom != null && data.gyoushaFrom!=''*/
    AND MKZI.GYOUSHA_CD >= /*data.gyoushaFrom*/
	/*END*/
	/*IF data.gyoushaTo != null && data.gyoushaTo!=''*/
    AND MKZI.GYOUSHA_CD <= /*data.gyoushaTo*/
	/*END*/
	/*IF data.genbaFrom != null && data.genbaFrom!=''*/
    AND MKZI.GENBA_CD >= /*data.genbaFrom*/
	/*END*/
	/*IF data.genbaTo != null && data.genbaTo!=''*/
    AND MKZI.GENBA_CD <= /*data.genbaTo*/
	/*END*/
	/*IF data.zaikoHinmeiFrom != null && data.zaikoHinmeiFrom!=''*/
    AND MKZI.ZAIKO_HINMEI_CD >= /*data.zaikoHinmeiFrom*/
	/*END*/
	/*IF data.zaikoHinmeiTo != null && data.zaikoHinmeiTo!=''*/
    AND MKZI.ZAIKO_HINMEI_CD <= /*data.zaikoHinmeiTo*/
	/*END*/
) AS DETAIL
LEFT JOIN M_GYOUSHA MGY
ON MGY.GYOUSHA_CD = DETAIL.GYOUSHA_CD
LEFT JOIN M_GENBA MGE
ON MGE.GYOUSHA_CD = DETAIL.GYOUSHA_CD
AND MGE.GENBA_CD = DETAIL.GENBA_CD
LEFT JOIN M_ZAIKO_HINMEI HINMEI
ON HINMEI.ZAIKO_HINMEI_CD = DETAIL.ZAIKO_HINMEI_CD
ORDER BY 
    DETAIL.GYOUSHA_CD,
    DETAIL.GENBA_CD,
    DETAIL.ZAIKO_HINMEI_CD