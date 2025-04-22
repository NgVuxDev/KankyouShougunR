SELECT            RIGHT('000000' + CONVERT(nvarchar, MIN(CONVERT(int, GYOUSHA_CD)) + 1), 6) AS Expr1
FROM              M_HIKIAI_GYOUSHA
WHERE             (UPPER(GYOUSHA_CD) NOT LIKE '%[A-Z]%') AND ((GYOUSHA_CD + 1) NOT IN
                            (SELECT         DISTINCT   GYOUSHA_CD
                               FROM              M_HIKIAI_GYOUSHA AS M_HIKIAI_GYOUSHA_1
                               WHERE             (UPPER(GYOUSHA_CD) NOT LIKE '%[A-Z]%')))