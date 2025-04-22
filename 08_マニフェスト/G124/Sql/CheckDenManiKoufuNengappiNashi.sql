SELECT
    (CASE WHEN R18.FIRST_MANIFEST_FLAG IS NULL or R18.FIRST_MANIFEST_FLAG = '' or ISNULL(HST_GYOUSHA.JISHA_KBN, 0) = 0
        THEN '0'
        ELSE '1'
    END) AS FIRST_MANIFEST_KBN,
    '4' AS HAIKI_KBN_CD,
    '電子' AS HAIKI_KBN_NAME,
    R18.MANIFEST_ID,
    R18.KANRI_ID AS SYSTEM_ID,
    '211' AS CK_KOUMOKU
FROM
    DT_R18 AS R18
    INNER JOIN DT_R18_EX AS R18EX ON R18.KANRI_ID = R18EX.KANRI_ID AND R18EX.DELETE_FLG = 0
    LEFT JOIN M_GYOUSHA AS HST_GYOUSHA ON R18EX.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD
WHERE
    (
            /*data.JOUKEN*/ = '5'
        AND exists
            (
                SELECT
                        DMT.KANRI_ID,
                        DMT.LATEST_SEQ
                FROM
                        DT_MF_TOC AS DMT
                WHERE
                        R18.KANRI_ID = DMT.KANRI_ID
                    AND R18.SEQ      = DMT.LATEST_SEQ
					/*IF data.YOYAKU_FLG==false*/
					AND DMT.STATUS_FLAG NOT IN ('1','2','3','9','99') 
					-- ELSE AND DMT.STATUS_FLAG NOT IN ('1','2','9') 
					/*END*/
            )
        AND (R18.HIKIWATASHI_DATE IS NULL OR  R18.HIKIWATASHI_DATE = '')
    )
	AND R18.CANCEL_FLAG <> '1' 
/*IF data.UPN_CD != null && data.UPN_CD != ''*/
AND
    (exists
        (
            SELECT
                    R19EX.KANRI_ID,
                    R19EX.SEQ
            FROM
                    DT_R19_EX AS R19EX
            WHERE
                    R18.KANRI_ID = R19EX.KANRI_ID
				AND R19EX.DELETE_FLG = 0
                AND R19EX.UPN_GYOUSHA_CD = /*data.UPN_CD*/''
        )
    )
/*END*/
/*IF data.SBNJ_CD != null && data.SBNJ_CD != ''*/AND R18EX.SBN_GYOUSHA_CD = /*data.SBNJ_CD*/''/*END*/
/*IF data.SBNB_CD != null && data.SBNB_CD != ''*/AND R18EX.SBN_GENBA_CD = /*data.SBNB_CD*/''/*END*/
GROUP BY
    R18.FIRST_MANIFEST_FLAG,
    R18.MANIFEST_ID,
    R18.KANRI_ID,
    HST_GYOUSHA.JISHA_KBN