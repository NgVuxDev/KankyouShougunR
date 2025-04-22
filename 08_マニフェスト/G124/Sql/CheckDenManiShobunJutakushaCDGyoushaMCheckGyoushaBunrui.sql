SELECT
    (CASE WHEN R18.FIRST_MANIFEST_FLAG IS NULL or R18.FIRST_MANIFEST_FLAG = '' or ISNULL(HST_GYOUSHA.JISHA_KBN, 0) = 0
        THEN '0'
        ELSE '1'
    END) AS FIRST_MANIFEST_KBN,
    '4' AS HAIKI_KBN_CD,
    '電子' AS HAIKI_KBN_NAME,
    R18.MANIFEST_ID,
    R18.KANRI_ID AS SYSTEM_ID,
    '91_3' AS CK_KOUMOKU
FROM
    DT_R18 AS R18
    INNER JOIN
        DT_R18_EX AS R18EX
    ON  R18EX.KANRI_ID = R18.KANRI_ID
    AND R18EX.DELETE_FLG = 0
    INNER JOIN DT_R19_EX AS R19EX
    ON  R19EX.SYSTEM_ID = R18EX.SYSTEM_ID
    AND R19EX.SEQ = R18EX.SEQ
    LEFT OUTER JOIN
        M_GYOUSHA AS MG
    ON  MG.GYOUSHA_CD = R18EX.SBN_GYOUSHA_CD
    LEFT JOIN M_GYOUSHA AS HST_GYOUSHA ON R18EX.HST_GYOUSHA_CD = HST_GYOUSHA.GYOUSHA_CD
WHERE
    (R18EX.SBN_GYOUSHA_CD IS NOT NULL OR R18EX.SBN_GYOUSHA_CD <> '')
AND ISNULL(MG.SHOBUN_NIOROSHI_GYOUSHA_KBN, 0) != 1
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
/*IF data.JOUKEN == '1'*/
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
							/*IF data.YOYAKU_FLG==false*/
							AND DMT.STATUS_FLAG NOT IN ('1','2','3','9','99') 
							-- ELSE AND DMT.STATUS_FLAG NOT IN ('1','2','9') 
							/*END*/
                    )
                )
            AND REPLACE(/*data.DATE_START*/, '/', '') <= R18.HIKIWATASHI_DATE
            AND R18.HIKIWATASHI_DATE <= REPLACE(/*data.DATE_END*/, '/', '')
/*END*/
/*IF data.JOUKEN == '2'*/
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
							/*IF data.YOYAKU_FLG==false*/
							AND DMT.STATUS_FLAG NOT IN ('1','2','3','9','99') 
							-- ELSE AND DMT.STATUS_FLAG NOT IN ('1','2','9') 
							/*END*/
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
						UNION
						SELECT 
								R19.KANRI_ID,
								R19.SEQ
						FROM
								DT_MF_TOC AS DMT_2
						LEFT JOIN DT_R19  AS R19
								ON DMT_2.KANRI_ID = R19.KANRI_ID
								AND DMT_2.LATEST_SEQ = R19.SEQ
								AND R19.UPN_ROUTE_NO = 1
						WHERE 
                                R18.KANRI_ID = R19.KANRI_ID
                            AND R18.SEQ      = R19.SEQ
							AND (ISNULL(R18.HST_SHA_EDI_MEMBER_ID, '') = ISNULL(R19.UPN_SHA_EDI_MEMBER_ID, ''))
							AND REPLACE(/*data.DATE_START*/, '/', '') <= R18.HIKIWATASHI_DATE
                            AND R18.HIKIWATASHI_DATE <= REPLACE(/*data.DATE_END*/, '/', '')
							/*IF data.YOYAKU_FLG==false*/
							AND DMT_2.STATUS_FLAG = '4'
							--ELSE AND DMT_2.STATUS_FLAG IN ('3','4') 
							/*END*/
                    )
                )
/*END*/
/*IF data.JOUKEN == '3'*/
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
							/*IF data.YOYAKU_FLG==false*/
							AND DMT.STATUS_FLAG NOT IN ('1','2','3','9','99') 
							-- ELSE AND DMT.STATUS_FLAG NOT IN ('1','2','9') 
							/*END*/
                    )
                )
            AND REPLACE(/*data.DATE_START*/, '/', '') <= R18.SBN_END_DATE
            AND R18.SBN_END_DATE <= REPLACE(/*data.DATE_END*/, '/', '')
/*END*/
/*IF data.JOUKEN == '4'*/
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
							/*IF data.YOYAKU_FLG==false*/
							AND DMT.STATUS_FLAG NOT IN ('1','2','3','9','99') 
							-- ELSE AND DMT.STATUS_FLAG NOT IN ('1','2','9') 
							/*END*/
                    )
                )
            AND REPLACE(/*data.DATE_START*/, '/', '') <= R18.LAST_SBN_END_DATE
            AND R18.LAST_SBN_END_DATE <= REPLACE(/*data.DATE_END*/, '/', '')
/*END*/
	AND R18.CANCEL_FLAG <> '1' 
GROUP BY
    R18.FIRST_MANIFEST_FLAG,
    R18.MANIFEST_ID,
    R18.KANRI_ID,
    HST_GYOUSHA.JISHA_KBN