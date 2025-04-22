SELECT
	 reserv.CONTENA_SET_KBN as CONTENA_SET_KBN1
    ,reserv.CONTENA_SHURUI_CD as CONTENA_SHURUI_CD1
    ,MCS1.CONTENA_SHURUI_NAME_RYAKU as CONTENA_SHURUI_NAME_RYAKU1
    ,reserv.CONTENA_CD as CONTENA_CD1
    ,MC1.CONTENA_NAME_RYAKU as CONTENA_NAME_RYAKU1
    ,reserv.DAISUU_CNT as DAISUU_CNT1
    ,reserv.JISSEKI_FLG as JISSEKI_FLG1
	,results.CONTENA_SET_KBN as CONTENA_SET_KBN2
    ,results.CONTENA_SHURUI_CD as CONTENA_SHURUI_CD2
    ,MCS2.CONTENA_SHURUI_NAME_RYAKU as CONTENA_SHURUI_NAME_RYAKU2
    ,results.CONTENA_CD as CONTENA_CD2
    ,MC2.CONTENA_NAME_RYAKU as CONTENA_NAME_RYAKU2
    ,results.DAISUU_CNT as DAISUU_CNT2
    ,results.JISSEKI_FLG as JISSEKI_FLG2
FROM
    (
        -- 予定データ
        SELECT
            SEQ_NO,
            CONTENA_SEQ_NO,
            JISSEKI_FLG,
            CONTENA_SET_KBN,
            CONTENA_SHURUI_CD,
            CONTENA_CD,
            DAISUU_CNT
        FROM
            T_MOBISYO_RT_CONTENA
        WHERE
            JISSEKI_FLG = 1
        AND SEQ_NO = /*SEQ_NO*/0
    ) AS reserv
    FULL JOIN
        (
        -- 実績データ
            SELECT
                SEQ_NO,
                CONTENA_SEQ_NO,
                JISSEKI_FLG,
                CONTENA_SET_KBN,
                CONTENA_SHURUI_CD,
                CONTENA_CD,
                DAISUU_CNT
            FROM
                T_MOBISYO_RT_CONTENA
            WHERE
                JISSEKI_FLG = 2
            AND SEQ_NO = /*SEQ_NO*/0
        ) AS results
    ON  reserv.SEQ_NO = results.SEQ_NO
    AND reserv.CONTENA_SEQ_NO = results.CONTENA_SEQ_NO
    LEFT JOIN
        M_CONTENA_SHURUI AS MCS1
    ON  reserv.CONTENA_SHURUI_CD = MCS1.CONTENA_SHURUI_CD
    LEFT JOIN
        M_CONTENA AS MC1
    ON  reserv.CONTENA_SHURUI_CD = MC1.CONTENA_SHURUI_CD
    AND reserv.CONTENA_CD = MC1.CONTENA_CD
    LEFT JOIN
        M_CONTENA_SHURUI AS MCS2
    ON  results.CONTENA_SHURUI_CD = MCS2.CONTENA_SHURUI_CD
    LEFT JOIN
        M_CONTENA AS MC2
    ON  results.CONTENA_SHURUI_CD = MC2.CONTENA_SHURUI_CD
    AND results.CONTENA_CD = MC2.CONTENA_CD
ORDER BY
    CASE
        WHEN reserv.JISSEKI_FLG IS NULL THEN 3
        ELSE reserv.JISSEKI_FLG
    END,
    CASE
        WHEN reserv.CONTENA_SET_KBN IS NULL THEN 32767
        ELSE reserv.CONTENA_SET_KBN
    END,
    CASE
        WHEN reserv.CONTENA_SHURUI_CD IS NULL THEN 'ZZZ'
        ELSE reserv.CONTENA_SHURUI_CD
    END,
    CASE
        WHEN reserv.CONTENA_CD IS NULL THEN 'ZZZZZZZZZZ'
        ELSE reserv.CONTENA_CD
    END,
    CASE
        WHEN results.JISSEKI_FLG IS NULL THEN 3
        ELSE results.JISSEKI_FLG
    END,
    CASE
        WHEN results.CONTENA_SET_KBN IS NULL THEN 32767
        ELSE results.CONTENA_SET_KBN
    END,
    CASE
        WHEN results.CONTENA_SHURUI_CD IS NULL THEN 'ZZZ'
        ELSE results.CONTENA_SHURUI_CD
    END,
    CASE
        WHEN results.CONTENA_CD IS NULL THEN 'ZZZZZZZZZZ'
        ELSE results.CONTENA_CD
    END