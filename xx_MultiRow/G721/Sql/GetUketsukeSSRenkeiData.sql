SELECT
    'éÛì¸' AS 'ì`éÌ'
    , U.UKEIRE_NUMBER AS DENPYOU_NUMBER
FROM
    T_UKEIRE_ENTRY AS U
WHERE
    U.DELETE_FLG = 0
    AND U.UKETSUKE_NUMBER = /*uketsukeNumber*/0
    /*IF !densyuKbn.IsNull && densyuKbn == 1*/
        AND U.UKEIRE_NUMBER != /*filteringDenpyouNumber*/0
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