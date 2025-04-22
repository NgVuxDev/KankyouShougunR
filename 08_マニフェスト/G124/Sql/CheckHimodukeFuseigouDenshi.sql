SELECT
    '1'					AS FIRST_MANIFEST_KBN,
    '4'					AS HAIKI_KBN_CD,
    '電子'				AS HAIKI_KBN_NAME,
    R18EX.MANIFEST_ID	AS MANIFEST_ID,
    TMR.NEXT_SYSTEM_ID	AS SYSTEM_ID,
    '310'				AS CK_KOUMOKU
FROM
    T_MANIFEST_RELATION AS TMR
    INNER JOIN (
        -- マニフェスト番号を取得
        SELECT
            SYSTEM_ID,
            MAX(MANIFEST_ID) AS MANIFEST_ID
        FROM
            DT_R18_EX
        GROUP BY
            SYSTEM_ID
    ) AS R18EX ON TMR.NEXT_HAIKI_KBN_CD = 4 AND TMR.NEXT_SYSTEM_ID = R18EX.SYSTEM_ID
WHERE
    (
        /*data.JOUKEN*/ = '6'
--二次=電子 ,一次=紙の場合
    AND (
        NOT EXISTS (
            -- 二次電マニチェック
            SELECT
                R18.SYSTEM_ID
            FROM
                (
                    SELECT
                        (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.DETAIL_SYSTEM_ID ELSE EX.SYSTEM_ID END) AS SYSTEM_ID,
                        (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.DELETE_FLG ELSE EX.DELETE_FLG END) AS DELETE_FLG
                    FROM
                        DT_R18_EX AS EX
                        LEFT JOIN (SELECT * FROM DT_R18_MIX WHERE DELETE_FLG = 0) AS MIX
                            ON EX.SYSTEM_ID = MIX.SYSTEM_ID
                    WHERE
                        EX.DELETE_FLG = 0
                ) AS R18
			WHERE
                TMR.NEXT_HAIKI_KBN_CD = 4
                AND TMR.NEXT_SYSTEM_ID = R18.SYSTEM_ID
                AND R18.DELETE_FLG = 0
        )
        OR NOT EXISTS (
            -- 一次紙マニチェック
            SELECT
                MENTRY.SYSTEM_ID
            FROM
                T_MANIFEST_ENTRY AS MENTRY
                INNER JOIN T_MANIFEST_DETAIL AS MDETAIL
                    ON MENTRY.SYSTEM_ID = MDETAIL.SYSTEM_ID
                    AND MENTRY.SEQ = MDETAIL.SEQ
            WHERE
                TMR.FIRST_HAIKI_KBN_CD <> 4
                AND TMR.FIRST_SYSTEM_ID = MDETAIL.DETAIL_SYSTEM_ID
                AND MENTRY.DELETE_FLG = 0
        )
    )
    AND TMR.DELETE_FLG = 0
    AND TMR.NEXT_HAIKI_KBN_CD = 4
    AND TMR.FIRST_HAIKI_KBN_CD <> 4
    )

UNION

SELECT
    '1'					AS FIRST_MANIFEST_KBN,
    '4'					AS HAIKI_KBN_CD,
    '電子'				AS HAIKI_KBN_NAME,
    R18EX.MANIFEST_ID	AS MANIFEST_ID,
    TMR.NEXT_SYSTEM_ID	AS SYSTEM_ID,
    '310'				AS CK_KOUMOKU
FROM
    T_MANIFEST_RELATION AS TMR
    INNER JOIN (
        -- マニフェスト番号を取得
        SELECT
            SYSTEM_ID,
            MAX(MANIFEST_ID) AS MANIFEST_ID
        FROM
            DT_R18_EX
        GROUP BY
            SYSTEM_ID
    ) AS R18EX ON TMR.NEXT_HAIKI_KBN_CD = 4 AND TMR.NEXT_SYSTEM_ID = R18EX.SYSTEM_ID
WHERE
    (
        /*data.JOUKEN*/ = '6'
--二次=電子,一次=電子の場合
    AND (
        NOT EXISTS (
            -- 一次電マニチェック
            SELECT
                R18.SYSTEM_ID
            FROM
                (
                    SELECT
                        (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.DETAIL_SYSTEM_ID ELSE EX.SYSTEM_ID END) AS SYSTEM_ID,
                        (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.DELETE_FLG ELSE EX.DELETE_FLG END) AS DELETE_FLG
                    FROM
                        DT_R18_EX AS EX
                        LEFT JOIN (SELECT * FROM DT_R18_MIX WHERE DELETE_FLG = 0) AS MIX
                            ON EX.SYSTEM_ID = MIX.SYSTEM_ID
                    WHERE
                        EX.DELETE_FLG = 0
                ) AS R18
            WHERE
                TMR.NEXT_HAIKI_KBN_CD = 4
                AND TMR.NEXT_SYSTEM_ID = R18.SYSTEM_ID
                AND R18.DELETE_FLG = 0
        )
        OR NOT EXISTS (
            -- 二次電マニチェック
            SELECT
                R18.SYSTEM_ID
            FROM
                (
                    SELECT
                        (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.DETAIL_SYSTEM_ID ELSE EX.SYSTEM_ID END) AS SYSTEM_ID,
                        (CASE WHEN MIX.DETAIL_SYSTEM_ID IS NOT NULL THEN MIX.DELETE_FLG ELSE EX.DELETE_FLG END) AS DELETE_FLG
                    FROM
                        DT_R18_EX AS EX
                        LEFT JOIN (SELECT * FROM DT_R18_MIX WHERE DELETE_FLG = 0) AS MIX
                            ON EX.SYSTEM_ID = MIX.SYSTEM_ID
                    WHERE
                        EX.DELETE_FLG = 0
                ) AS R18
            WHERE
                TMR.FIRST_HAIKI_KBN_CD = 4
                AND TMR.FIRST_SYSTEM_ID = R18.SYSTEM_ID
                AND R18.DELETE_FLG = 0
        )
    )
    AND TMR.DELETE_FLG = 0
    AND TMR.NEXT_HAIKI_KBN_CD = 4
    AND TMR.FIRST_HAIKI_KBN_CD = 4
    )

GROUP BY
    TMR.NEXT_HAIKI_KBN_CD,
    TMR.NEXT_SYSTEM_ID,
    R18EX.MANIFEST_ID

ORDER BY SYSTEM_ID