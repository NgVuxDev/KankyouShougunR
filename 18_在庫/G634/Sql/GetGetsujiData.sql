select * from T_MONTHLY_LOCK_ZAIKO
WHERE GYOUSHA_CD = /*data.gyoushaCd*/
AND GENBA_CD = /*data.genbaCd*/
AND ZAIKO_HINMEI_CD = /*data.zaikoHinmeiCd*/
AND YEAR <= /*data.year*/
AND MONTH <= /*data.month*/
AND DELETE_FLG = 0
ORDER BY YEAR desc,MONTH desc