SELECT
    '•i–¼CDF' + TUD.HINMEI_CD AS HINMEI_CD
    ,TUD.HINMEI_NAME
FROM
    T_UKEIRE_ENTRY TUE
    LEFT JOIN T_UKEIRE_DETAIL TUD ON TUD.SYSTEM_ID = TUE.SYSTEM_ID AND TUD.SEQ = TUE.SEQ
WHERE
    1 = 1
/*IF torihikisakiCd != null*/AND TUE.TORIHIKISAKI_CD = /*torihikisakiCd*//*END*/
/*IF gyoushaCd != null*/AND TUE.GYOUSHA_CD = /*gyoushaCd*//*END*/
/*IF genbaCd != null*/AND TUE.GENBA_CD = /*genbaCd*//*END*/
    AND TUE.DENPYOU_DATE >= CONVERT(DATETIME ,/*denpyouDateFrom*/,120)
    AND TUE.DENPYOU_DATE <= CONVERT(DATETIME ,/*denpyouDateTo*/,120)
    AND TUE.DELETE_FLG = 0
    AND TUD.HINMEI_CD IS NOT NULL
    /*IF !kyotenCd.IsNull*/
    AND TUE.KYOTEN_CD = /*kyotenCd*/
    /*END*/
GROUP BY
    TUD.HINMEI_CD
    ,TUD.HINMEI_NAME