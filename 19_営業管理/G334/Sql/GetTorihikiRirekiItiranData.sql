SELECT 
     CASE WHEN s.TORIHIKISAKI_CD IS NULL THEN '' ELSE s.TORIHIKISAKI_CD END AS TORIHIKISAKI_CD
	, CASE WHEN mtr.TORIHIKISAKI_NAME_RYAKU IS NULL THEN '' ELSE mtr.TORIHIKISAKI_NAME_RYAKU END AS TORIHIKISAKI_NAME_RYAKU
    , CASE WHEN s.GYOUSHA_CD IS NULL THEN '' ELSE s.GYOUSHA_CD END AS GYOUSHA_CD
	, CASE WHEN mgs.GYOUSHA_NAME_RYAKU IS NULL THEN '' ELSE mgs.GYOUSHA_NAME_RYAKU END AS GYOUSHA_NAME_RYAKU
    , CASE WHEN s.GENBA_CD IS NULL THEN '' ELSE s.GENBA_CD END AS GENBA_CD
	, CASE WHEN mgb.GENBA_NAME_RYAKU IS NULL THEN '' ELSE mgb.GENBA_NAME_RYAKU END AS GENBA_NAME_RYAKU
    , s.DENPYOU_DATE
    , s.CREATE_DATE
    , s.DENPYOU_KBN
    , s.DENPYOU_NUMBER
	, s.DAINOU_FLG
FROM
(
    -- グループ化(受入、出荷、売上／支払) --
    SELECT
    
        g.TORIHIKISAKI_CD
        , g.GYOUSHA_CD
        , g.GENBA_CD
        , g.DENPYOU_DATE
        , g.CREATE_DATE
        , g.DENPYOU_KBN
        , g.DENPYOU_NUMBER
		, g.DAINOU_FLG

    FROM
    (
        -- 受入入力 --
        SELECT 
            t.TORIHIKISAKI_CD AS TORIHIKISAKI_CD
            , t.GYOUSHA_CD AS GYOUSHA_CD
            , t.GENBA_CD AS GENBA_CD
            , CAST(t.DENPYOU_DATE AS DATE) AS DENPYOU_DATE
            , t.CREATE_DATE AS CREATE_DATE
			, t.UKEIRE_NUMBER AS DENPYOU_NUMBER
            , 1 AS DENPYOU_KBN
			, 0 AS DAINOU_FLG
        FROM dbo.T_UKEIRE_ENTRY AS t
        INNER JOIN dbo.T_UKEIRE_DETAIL AS d 
        ON 
            d.SYSTEM_ID = t.SYSTEM_ID 
            AND d.SEQ = t.SEQ 
            AND d.DENPYOU_KBN_CD = 1  
            AND d.KAKUTEI_KBN = 1 
        WHERE 
            t.DELETE_FLG = 0
            AND CONVERT(nvarchar, t.DENPYOU_DATE, 111) >=  CONVERT(nvarchar, /*data.DENPYOU_DATE_FROM*/null, 111)
            AND CONVERT(nvarchar, t.DENPYOU_DATE, 111) <=  CONVERT(nvarchar, /*data.DENPYOU_DATE_TO*/null, 111)
            AND t.TAIRYUU_KBN = 0
            /*IF data.KYOTEN_CD != null */ 
            AND t.KYOTEN_CD = /*data.KYOTEN_CD*/0 /*END*/
            /*IF data.EIGYOU_TANTOUSHA_CD != null */ 
            AND t.EIGYOU_TANTOUSHA_CD = /*data.EIGYOU_TANTOUSHA_CD*/'0' /*END*/
            /*IF data.TORIHIKISAKI_CD != null */ 
            AND t.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/'0' /*END*/
            /*IF data.GYOUSHA_CD != null */ 
            AND t.GYOUSHA_CD =  /*data.GYOUSHA_CD*/'0' /*END*/
            /*IF data.GENBA_CD != null */ 
            AND t.GENBA_CD =  /*data.GENBA_CD*/'0' /*END*/
            
        -- 出荷入力 --
        UNION
        SELECT 
            t.TORIHIKISAKI_CD AS TORIHIKISAKI_CD
            , t.GYOUSHA_CD AS GYOUSHA_CD
            , t.GENBA_CD AS GENBA_CD
            , CAST(t.DENPYOU_DATE AS DATE) AS DENPYOU_DATE
            , t.CREATE_DATE AS CREATE_DATE
			, t.SHUKKA_NUMBER AS DENPYOU_NUMBER
            , 2 AS DENPYOU_KBN
			, 0 AS DAINOU_FLG
        FROM dbo.T_SHUKKA_ENTRY AS t
        INNER JOIN dbo.T_SHUKKA_DETAIL AS d 
        ON 
            d.SYSTEM_ID = t.SYSTEM_ID 
            AND d.SEQ = t.SEQ 
            AND d.DENPYOU_KBN_CD = 1  
            AND d.KAKUTEI_KBN = 1 
        WHERE 
            t.DELETE_FLG = 0
            AND CONVERT(nvarchar, t.DENPYOU_DATE, 111) >=  CONVERT(nvarchar, /*data.DENPYOU_DATE_FROM*/null, 111)
            AND CONVERT(nvarchar, t.DENPYOU_DATE, 111) <=  CONVERT(nvarchar, /*data.DENPYOU_DATE_TO*/null, 111)
            AND t.TAIRYUU_KBN = 0
            /*IF data.KYOTEN_CD != null */ 
            AND t.KYOTEN_CD = /*data.KYOTEN_CD*/0 /*END*/
            /*IF data.EIGYOU_TANTOUSHA_CD != null */ 
            AND t.EIGYOU_TANTOUSHA_CD = /*data.EIGYOU_TANTOUSHA_CD*/'0' /*END*/
            /*IF data.TORIHIKISAKI_CD != null */ 
            AND t.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/'0' /*END*/
            /*IF data.GYOUSHA_CD != null */ 
            AND t.GYOUSHA_CD =  /*data.GYOUSHA_CD*/'0' /*END*/
            /*IF data.GENBA_CD != null */ 
            AND t.GENBA_CD =  /*data.GENBA_CD*/'0' /*END*/
    
        -- 売上／支払入力 --
        UNION
        SELECT 
            t.TORIHIKISAKI_CD AS TORIHIKISAKI_CD
            , t.GYOUSHA_CD AS GYOUSHA_CD
            , t.GENBA_CD AS GENBA_CD
            , CAST(t.DENPYOU_DATE AS DATE) AS DENPYOU_DATE
            , t.CREATE_DATE AS CREATE_DATE
			, t.UR_SH_NUMBER AS DENPYOU_NUMBER
            , 3 AS DENPYOU_KBN
            , ISNULL(DAINOU_FLG,0) AS DAINOU_FLG
        FROM dbo.T_UR_SH_ENTRY AS t
        INNER JOIN dbo.T_UR_SH_DETAIL AS d 
        ON 
            d.SYSTEM_ID = t.SYSTEM_ID 
            AND d.SEQ = t.SEQ 
            AND((d.DENPYOU_KBN_CD = 1
                AND d.KAKUTEI_KBN = 1
                AND ISNULL(t.DAINOU_FLG,0) = 0)
                OR (t.DAINOU_FLG = 1))
        WHERE 
            t.DELETE_FLG = 0
            AND CONVERT(nvarchar, t.DENPYOU_DATE, 111) >=  CONVERT(nvarchar, /*data.DENPYOU_DATE_FROM*/null, 111)
            AND CONVERT(nvarchar, t.DENPYOU_DATE, 111) <=  CONVERT(nvarchar, /*data.DENPYOU_DATE_TO*/null, 111)
            /*IF data.KYOTEN_CD != null */ 
            AND t.KYOTEN_CD = /*data.KYOTEN_CD*/0 /*END*/
            /*IF data.EIGYOU_TANTOUSHA_CD != null */ 
            AND t.EIGYOU_TANTOUSHA_CD = /*data.EIGYOU_TANTOUSHA_CD*/'0' /*END*/
            /*IF data.TORIHIKISAKI_CD != null */ 
            AND t.TORIHIKISAKI_CD = /*data.TORIHIKISAKI_CD*/'0' /*END*/
            /*IF data.GYOUSHA_CD != null */ 
            AND t.GYOUSHA_CD =  /*data.GYOUSHA_CD*/'0' /*END*/
            /*IF data.GENBA_CD != null */ 
            AND t.GENBA_CD =  /*data.GENBA_CD*/'0' /*END*/
    ) AS g
    GROUP BY
        g.TORIHIKISAKI_CD
        , g.GYOUSHA_CD
        , g.GENBA_CD
        , g.DENPYOU_DATE
        , g.CREATE_DATE
        , g.DENPYOU_KBN
        , g.DENPYOU_NUMBER
        , g.DAINOU_FLG
) AS s

-- 取引先マスタ --
LEFT OUTER JOIN dbo.M_TORIHIKISAKI AS mtr
ON 
    mtr.TORIHIKISAKI_CD = s.TORIHIKISAKI_CD

-- 業者マスタ --
LEFT OUTER JOIN dbo.M_GYOUSHA AS mgs
ON 
    mgs.GYOUSHA_CD = s.GYOUSHA_CD

-- 現場マスタ --
LEFT OUTER JOIN dbo.M_GENBA AS mgb
ON 
    mgb.GYOUSHA_CD = s.GYOUSHA_CD
    AND mgb.GENBA_CD = s.GENBA_CD

-- 昇順(取引先CD、業者CD、現場CD)、降順(伝票日) --
ORDER BY
    s.TORIHIKISAKI_CD ASC
    ,s.GYOUSHA_CD ASC
    ,s.GENBA_CD ASC
    ,s.DENPYOU_DATE DESC