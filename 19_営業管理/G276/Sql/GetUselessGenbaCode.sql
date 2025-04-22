SELECT            RIGHT('000000' + CONVERT(nvarchar, MIN(CONVERT(int, GENBA_CD)) + 1), 6) AS Expr1
FROM              M_HIKIAI_GENBA AS a
WHERE             (UPPER(GENBA_CD) NOT LIKE '%[A-Z]%') AND ((GENBA_CD + 1) NOT IN
                            (SELECT DISTINCT GENBA_CD
                               FROM              M_HIKIAI_GENBA AS M_HIKIAI_GENBA_1
                               WHERE             (UPPER(GENBA_CD) NOT LIKE '%[A-Z]%')))