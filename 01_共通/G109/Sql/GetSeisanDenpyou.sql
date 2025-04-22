SELECT
	TSD.TORIHIKISAKI_CD

FROM
	T_SEISAN_DENPYOU AS TSD

WHERE
1=1
/*IF torihikisakiCd != ""*/
AND	TSD.TORIHIKISAKI_CD = /*torihikisakiCd*/
/*IF selectFromDate != ""*/
AND TSD.SEISAN_DATE >= CONVERT(DATETIME, /*selectFromDate*/, 20) /*END*/
/*IF selectToDate != "" */
AND TSD.SEISAN_DATE <= CONVERT(DATETIME, /*selectToDate*/, 20) /*END*/
/*END*/