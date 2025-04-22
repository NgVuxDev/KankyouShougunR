SELECT            RIGHT('000000' + CONVERT(nvarchar, MIN(CONVERT(int, TORIHIKISAKI_CD)) + 1), 6) AS TORIHIKISAKI_CD
FROM              M_HIKIAI_TORIHIKISAKI
WHERE             (UPPER(TORIHIKISAKI_CD) NOT LIKE '%[A-Z]%') AND ((TORIHIKISAKI_CD + 1) NOT IN
                            (SELECT DISTINCT TORIHIKISAKI_CD
                               FROM              M_HIKIAI_TORIHIKISAKI AS M_HIKIAI_TORIHIKISAKI_1
                               WHERE             (UPPER(TORIHIKISAKI_CD) NOT LIKE '%[A-Z]%')))