SELECT
    CONVERT(VARCHAR, TME.FIRST_MANIFEST_KBN) AS FIRST_MANIFEST_KBN,
    TME.HAIKI_KBN_CD,
    MHK.HAIKI_KBN_NAME_RYAKU  AS HAIKI_KBN_NAME,
    TME.MANIFEST_ID,
    TME.SYSTEM_ID,
    CASE
      WHEN MUPN.UPN_ROUTE_NO = '1' THEN '81'
      WHEN MUPN.UPN_ROUTE_NO = '2' THEN '92'
      WHEN MUPN.UPN_ROUTE_NO = '3' THEN '93'
    ELSE ''
    END AS CK_KOUMOKU
FROM
    T_MANIFEST_ENTRY AS TME
    INNER JOIN
        T_MANIFEST_UPN AS MUPN
    ON  MUPN.SYSTEM_ID = TME.SYSTEM_ID
    AND MUPN.SEQ = TME.SEQ
    LEFT OUTER JOIN M_GYOUSHA AS MG
    ON  MG.GYOUSHA_CD = MUPN.UPN_GYOUSHA_CD
    INNER JOIN M_HAIKI_KBN AS MHK
    ON  TME.HAIKI_KBN_CD = MHK.HAIKI_KBN_CD
	LEFT JOIN (SELECT TMU1.* FROM T_MANIFEST_ENTRY TME1 INNER JOIN T_MANIFEST_UPN TMU1 ON TMU1.SYSTEM_ID = TME1.SYSTEM_ID AND TMU1.SEQ = TME1.SEQ AND TMU1.UPN_ROUTE_NO = 1 
		WHERE (TME1.HAIKI_KBN_CD = 1 OR TME1.HAIKI_KBN_CD =2) AND TME1.DELETE_FLG =0 AND TME1.SEQ = (SELECT MAX(SEQ) FROM T_MANIFEST_ENTRY WHERE SYSTEM_ID = TME1.SYSTEM_ID)
		UNION
		SELECT TMU2.* FROM T_MANIFEST_ENTRY TME2 INNER JOIN T_MANIFEST_UPN TMU2 ON TMU2.SYSTEM_ID = TME2.SYSTEM_ID AND TMU2.SEQ = TME2.SEQ AND TME2.HAIKI_KBN_CD = 3  
		AND TMU2.UPN_ROUTE_NO = (
		SELECT MIN(UPN_ROUTE_NO) FROM T_MANIFEST_UPN WHERE TME2.SYSTEM_ID = SYSTEM_ID AND SEQ = TME2.SEQ AND TME2.HAIKI_KBN_CD = 3 AND UPN_SAKI_KBN = 1) AND TME2.DELETE_FLG = 0) TMU 
		ON TMU.SYSTEM_ID = TME.SYSTEM_ID AND TMU.SEQ = TME.SEQ
WHERE
    TME.DELETE_FLG = 0
/* 20140623 ria EV004852 一覧と抽出条件の変更 start*/
/*IF data.BUNRUI != 5*/
AND TME.HAIKI_KBN_CD = /*data.BUNRUI*/0
/*END*/
/* 20140623 ria EV004852 一覧と抽出条件の変更 end*/
/*IF !data.UPN_ROUTE_NO.IsNull && data.UPN_ROUTE_NO != ''*/AND MUPN.UPN_ROUTE_NO = /*data.UPN_ROUTE_NO*//*END*/
AND ((MUPN.UPN_GYOUSHA_CD IS NOT NULL OR MUPN.UPN_GYOUSHA_CD <> '') AND MG.GYOUSHA_CD IS NULL)
/*IF data.UPN_CD != null && data.UPN_CD != ''*/
AND exists (
    SELECT
        TEMP.SYSTEM_ID
    FROM
        (
            SELECT
                TM.SYSTEM_ID,
                TM.SEQ
            FROM
                T_MANIFEST_ENTRY AS TM
                INNER JOIN
                    T_MANIFEST_UPN AS MUPN
                ON  MUPN.SYSTEM_ID = TM.SYSTEM_ID
                AND MUPN.SEQ = TM.SEQ
            WHERE
                MUPN.UPN_GYOUSHA_CD = /*data.UPN_CD*/''
            GROUP BY
                TM.SYSTEM_ID,
                TM.SEQ
        ) AS TEMP
    WHERE
        TME.SYSTEM_ID = TEMP.SYSTEM_ID
    AND TME.SEQ = TEMP.SEQ
	AND TME.DELETE_FLG = 0
    )
/*END*/
/*IF data.SBNJ_CD != null && data.SBNJ_CD != ''*/AND TME.SBN_GYOUSHA_CD = /*data.SBNJ_CD*/''/*END*/
/*IF data.SBNB_CD != null && data.SBNB_CD != ''*/AND TMU.UPN_SAKI_GENBA_CD = /*data.SBNB_CD*/''/*END*/
/*IF data.JOUKEN == '1'*/
        AND exists (
                SELECT
                    TM.SYSTEM_ID
                FROM
                    T_MANIFEST_ENTRY AS TM
                WHERE
                    TME.SYSTEM_ID = TM.SYSTEM_ID
                AND TME.SEQ = TM.SEQ
                AND (TM.KOUFU_DATE >= CONVERT(DATETIME, CONVERT(nvarchar, /*data.DATE_START*/, 111), 120)
                    AND CONVERT(DATETIME, CONVERT(nvarchar, /*data.DATE_END*/, 111), 120) >= TM.KOUFU_DATE)
                )
/*END*/
/*IF data.JOUKEN == '2'*/
        AND exists (
                SELECT
                    TEMP.SYSTEM_ID
                FROM
                    (
                        SELECT
                            TM.SYSTEM_ID,
                            TM.SEQ
                        FROM
                            T_MANIFEST_ENTRY AS TM
                            INNER JOIN
                                T_MANIFEST_UPN AS MUPN
                            ON  MUPN.SYSTEM_ID = TM.SYSTEM_ID
                            AND MUPN.SEQ = TM.SEQ
                        WHERE
                            (MUPN.UPN_END_DATE >= CONVERT(DATETIME, CONVERT(nvarchar, /*data.DATE_START*/, 111), 120)
                                AND CONVERT(DATETIME, CONVERT(nvarchar, /*data.DATE_END*/, 111), 120) >= MUPN.UPN_END_DATE)
                        GROUP BY
                            TM.SYSTEM_ID,
                            TM.SEQ
                    ) AS TEMP
                WHERE
                    TME.SYSTEM_ID = TEMP.SYSTEM_ID
                AND TME.SEQ = TEMP.SEQ
                AND TME.DELETE_FLG = 0
                )
/*END*/
/*IF data.JOUKEN == '3'*/
        AND exists (
                SELECT
                    TEMP.SYSTEM_ID
                FROM
                    (
                        SELECT
                            TM.SYSTEM_ID,
                            TM.SEQ
                        FROM
                            T_MANIFEST_ENTRY AS TM
                            INNER JOIN
                                T_MANIFEST_DETAIL AS MD
                            ON  MD.SYSTEM_ID = TM.SYSTEM_ID
                            AND MD.SEQ = TM.SEQ
                        WHERE
                            (MD.SBN_END_DATE >= CONVERT(DATETIME, CONVERT(nvarchar, /*data.DATE_START*/, 111), 120)
                                AND CONVERT(DATETIME, CONVERT(nvarchar, /*data.DATE_END*/, 111), 120) >= MD.SBN_END_DATE)
                        GROUP BY
                            TM.SYSTEM_ID,
                            TM.SEQ
                    ) AS TEMP
                WHERE
                    TME.SYSTEM_ID = TEMP.SYSTEM_ID
                AND TME.SEQ = TEMP.SEQ
                AND TME.DELETE_FLG = 0
                )
/*END*/
/*IF data.JOUKEN == '4'*/
        AND exists (
                SELECT
                    TEMP.SYSTEM_ID
                FROM
                    (
                        SELECT
                            TM.SYSTEM_ID,
                            TM.SEQ
                        FROM
                            T_MANIFEST_ENTRY AS TM
                            INNER JOIN
                                T_MANIFEST_DETAIL AS MD
                            ON  MD.SYSTEM_ID = TM.SYSTEM_ID
                            AND MD.SEQ = TM.SEQ
                        WHERE
                            (MD.LAST_SBN_END_DATE >= CONVERT(DATETIME, CONVERT(nvarchar, /*data.DATE_START*/, 111), 120)
                                AND CONVERT(DATETIME, CONVERT(nvarchar, /*data.DATE_END*/, 111), 120) >= MD.LAST_SBN_END_DATE)
                        GROUP BY
                            TM.SYSTEM_ID,
                            TM.SEQ
                    ) AS TEMP
                WHERE
                    TME.SYSTEM_ID = TEMP.SYSTEM_ID
                AND TME.SEQ = TEMP.SEQ
                AND TME.DELETE_FLG = 0
                )
/*END*/
/*IF data.KYOTEN != null && data.KYOTEN != '' && data.KYOTEN != '99'*/
AND TME.KYOTEN_CD = /*data.KYOTEN*/0
/*END*/

GROUP BY
    TME.FIRST_MANIFEST_KBN,
    TME.HAIKI_KBN_CD,
    MHK.HAIKI_KBN_NAME_RYAKU,
    TME.MANIFEST_ID,
    TME.SYSTEM_ID,
    MUPN.UPN_ROUTE_NO