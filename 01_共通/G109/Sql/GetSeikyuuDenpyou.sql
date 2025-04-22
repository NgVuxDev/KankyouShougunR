SELECT
	TSD.TORIHIKISAKI_CD

FROM
	T_SEIKYUU_DENPYOU AS TSD

WHERE
1=1
/*IF torihikisakiCd != ""*/
AND	TSD.TORIHIKISAKI_CD = /*torihikisakiCd*/
/*IF selectFromDate != ""*/
AND TSD.SEIKYUU_DATE >= CONVERT(DATETIME, /*selectFromDate*/, 20) /*END*/
/*IF selectToDate != "" */
AND TSD.SEIKYUU_DATE <= CONVERT(DATETIME, /*selectToDate*/, 20) /*END*/
/*END*/