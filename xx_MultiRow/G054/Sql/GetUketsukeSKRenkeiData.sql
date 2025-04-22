SELECT
    'èoâ◊' AS 'ì`éÌ'
    , S.SHUKKA_NUMBER AS DENPYOU_NUMBER
FROM
    T_SHUKKA_ENTRY AS S
WHERE
    S.DELETE_FLG = 0
    AND S.UKETSUKE_NUMBER = /*uketsukeNumber*/0
    /*IF !densyuKbn.IsNull && densyuKbn == 2*/
        AND S.SHUKKA_NUMBER != /*filteringDenpyouNumber*/0
    /*END*/

UNION
SELECT
    'îÑè„éxï•' AS 'ì`éÌ'
    , UR.UR_SH_NUMBER AS DENPYOU_NUMBER
FROM
    T_UR_SH_ENTRY AS UR
WHERE
    UR.DELETE_FLG = 0

    AND UR.UKETSUKE_NUMBER = /*uketsukeNumber*/0
    /*IF !densyuKbn.IsNull && densyuKbn == 3*/
        AND UR.UR_SH_NUMBER != /*filteringDenpyouNumber*/0
    /*END*/