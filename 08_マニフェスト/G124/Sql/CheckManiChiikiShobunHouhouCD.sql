SELECT
    CONVERT(VARCHAR, TME.FIRST_MANIFEST_KBN) AS FIRST_MANIFEST_KBN,
    TME.HAIKI_KBN_CD,
    MHK.HAIKI_KBN_NAME_RYAKU  AS HAIKI_KBN_NAME,
    TME.MANIFEST_ID,
    TME.SYSTEM_ID,
    '70_2' AS CK_KOUMOKU
FROM
    T_MANIFEST_ENTRY AS TME
    LEFT JOIN
        T_MANIFEST_DETAIL AS MD
    ON  MD.SYSTEM_ID = TME.SYSTEM_ID
    AND MD.SEQ = TME.SEQ
    INNER JOIN M_HAIKI_KBN AS MHK
    ON  TME.HAIKI_KBN_CD = MHK.HAIKI_KBN_CD
	LEFT JOIN M_CHIIKIBETSU_SHOBUN AS CB
	ON MD.SBN_HOUHOU_CD = CB.SHOBUN_HOUHOU_CD
	AND CB.CHIIKI_CD = /*data.CHIIKI_CD*/0
WHERE
    TME.DELETE_FLG = 0
/* 20140623 ria EV004852 一覧と抽出条件の変更 start*/
/*IF data.BUNRUI != 5*/
AND TME.HAIKI_KBN_CD = /*data.BUNRUI*/0
/*END*/
/* 20140623 ria EV004852 一覧と抽出条件の変更 end*/
AND	ISNULL(MD.SBN_HOUHOU_CD, '') <> ''
AND (CB.SHOBUN_HOUHOU_CD IS NULL OR CB.SHOBUN_HOUHOU_CD = '')
AND (
        (
            /*data.JOUKEN*/ = '1'
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
        )
    OR  (
            /*data.JOUKEN*/ = '2'
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
        )
    OR  (
            /*data.JOUKEN*/ = '3'
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
        )
    OR  (
            /*data.JOUKEN*/ = '4'
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
        )
    )
/*IF data.KYOTEN != null && data.KYOTEN != '' && data.KYOTEN != '99'*/
AND TME.KYOTEN_CD = /*data.KYOTEN*/0
/*END*/
GROUP BY
    TME.FIRST_MANIFEST_KBN,
    TME.HAIKI_KBN_CD,
    MHK.HAIKI_KBN_NAME_RYAKU,
    TME.MANIFEST_ID,
    TME.SYSTEM_ID