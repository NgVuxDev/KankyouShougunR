SELECT
    CONVERT(VARCHAR, TME.FIRST_MANIFEST_KBN) AS FIRST_MANIFEST_KBN,
    TME.HAIKI_KBN_CD,
    MHK.HAIKI_KBN_NAME_RYAKU  AS HAIKI_KBN_NAME,
    TME.MANIFEST_ID,
    TME.SYSTEM_ID,
    '90' AS CK_KOUMOKU
FROM
    T_MANIFEST_ENTRY AS TME
    INNER JOIN T_MANIFEST_UPN AS UPN
    ON  UPN.SYSTEM_ID = TME.SYSTEM_ID
    AND UPN.SEQ = TME.SEQ
    AND (UPN.UPN_SAKI_GYOUSHA_CD IS NULL OR UPN.UPN_SAKI_GYOUSHA_CD = '')
    INNER JOIN M_HAIKI_KBN AS MHK
    ON  TME.HAIKI_KBN_CD = MHK.HAIKI_KBN_CD
WHERE
    TME.DELETE_FLG = 0
/* 20140623 ria EV004852 一覧と抽出条件の変更 start*/
/*IF data.BUNRUI != 5*/
AND TME.HAIKI_KBN_CD = /*data.BUNRUI*/0
/*END*/
/* 20140623 ria EV004852 一覧と抽出条件の変更 end*/
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

GROUP BY
    TME.FIRST_MANIFEST_KBN,
    TME.HAIKI_KBN_CD,
    MHK.HAIKI_KBN_NAME_RYAKU,
    TME.MANIFEST_ID,
    TME.SYSTEM_ID