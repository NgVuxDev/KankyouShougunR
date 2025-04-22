SELECT
    (CASE WHEN R18.FIRST_MANIFEST_FLAG IS NULL or R18.FIRST_MANIFEST_FLAG = '' or ISNULL(HST_GYOUSHA.JISHA_KBN, 0) = 0
        THEN '0'
        ELSE '1'
    END) AS FIRST_MANIFEST_KBN,
    '4' AS HAIKI_KBN_CD,
    '電子' AS HAIKI_KBN_NAME,
    R18.MANIFEST_ID,
    R18.KANRI_ID AS SYSTEM_ID,
    '70_2' AS CK_KOUMOKU
FROM
    DT_R18 AS R18
    INNER JOIN (
            SELECT
                EX.KANRI_ID
                ,(CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.SBN_HOUHOU_CD ELSE EX.SBN_HOUHOU_CD END) AS SBN_HOUHOU_CD
                ,EX.HST_GYOUSHA_CD
            FROM DT_R18_EX AS EX
            LEFT JOIN (SELECT * FROM DT_R18_MIX WHERE DELETE_FLG = 0) AS MIX
            ON EX.SYSTEM_ID = MIX.SYSTEM_ID
            WHERE EX.DELETE_FLG = 0
        ) AS R18EX
    ON  R18EX.KANRI_ID = R18.KANRI_ID
    LEFT JOIN M_GYOUSHA AS HST_GYOUSHA ON R18EX.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD
	LEFT JOIN M_CHIIKIBETSU_SHOBUN AS CB
	ON R18EX.SBN_HOUHOU_CD = CB.SHOBUN_HOUHOU_CD
	AND CB.CHIIKI_CD = /*data.CHIIKI_CD*/0
WHERE
	ISNULL(R18EX.SBN_HOUHOU_CD, '') <> ''
AND	(CB.SHOBUN_HOUHOU_CD IS NULL OR CB.SHOBUN_HOUHOU_CD = '')
AND (
        (
                /*data.JOUKEN*/ = '1'
            AND
                (exists
                    (
                        SELECT
                                DMT.KANRI_ID,
                                DMT.LATEST_SEQ
                        FROM
                                DT_MF_TOC AS DMT
                        WHERE
                                R18.KANRI_ID = DMT.KANRI_ID
                            AND R18.SEQ      = DMT.LATEST_SEQ
							AND DMT.STATUS_FLAG NOT IN ('1','2','3', '9') 
                    )
                )
            AND REPLACE(/*data.DATE_START*/, '/', '') <= R18.HIKIWATASHI_DATE
            AND R18.HIKIWATASHI_DATE <= REPLACE(/*data.DATE_END*/, '/', '')
        )
    OR
        (
                /*data.JOUKEN*/ = '2'
            AND
                (exists
                    (
                        SELECT
                                DMT.KANRI_ID,
                                DMT.LATEST_SEQ
                        FROM
                                DT_MF_TOC AS DMT
                        WHERE
                                R18.KANRI_ID = DMT.KANRI_ID
                            AND R18.SEQ      = DMT.LATEST_SEQ
							AND DMT.STATUS_FLAG NOT IN ('1','2','3', '9') 
                    )
                )
            AND
                (exists
                    (
                        SELECT
                                R19.KANRI_ID,
                                R19.SEQ
                        FROM
                                DT_R19 AS R19
                        WHERE
                                R18.KANRI_ID = R19.KANRI_ID
                            AND R18.SEQ      = R19.SEQ
                            AND REPLACE(/*data.DATE_START*/, '/', '') <= R19.UPN_END_DATE
                            AND R19.UPN_END_DATE <= REPLACE(/*data.DATE_END*/, '/', '')
                    )
                )
        )
    OR
        (
                /*data.JOUKEN*/ = '3'
            AND (exists
                    (
                        SELECT
                                R18.KANRI_ID,
                                R18.SEQ
                        FROM
                                DT_MF_TOC AS DMT
                        WHERE
                                DMT.KANRI_ID   = R18.KANRI_ID
                            AND DMT.LATEST_SEQ = R18.SEQ
							AND DMT.STATUS_FLAG NOT IN ('1','2','3', '9') 
                    )
                )
            AND REPLACE(/*data.DATE_START*/, '/', '') <= R18.SBN_END_DATE
            AND R18.SBN_END_DATE <= REPLACE(/*data.DATE_END*/, '/', '')
        )
    OR
        (
                /*data.JOUKEN*/ = '4'
            AND (exists
                    (
                        SELECT
                                R18.KANRI_ID,
                                R18.SEQ
                        FROM
                                DT_MF_TOC AS DMT
                        WHERE
                                DMT.KANRI_ID   = R18.KANRI_ID
                            AND DMT.LATEST_SEQ = R18.SEQ
							AND DMT.STATUS_FLAG NOT IN ('1','2','3', '9') 
                    )
                )
            AND REPLACE(/*data.DATE_START*/, '/', '') <= R18.LAST_SBN_END_DATE
            AND R18.LAST_SBN_END_DATE <= REPLACE(/*data.DATE_END*/, '/', '')
        )
    )
	AND R18.CANCEL_FLAG <> '1' 
GROUP BY
    R18.FIRST_MANIFEST_FLAG,
    R18.MANIFEST_ID,
    R18.KANRI_ID,
    HST_GYOUSHA.JISHA_KBN