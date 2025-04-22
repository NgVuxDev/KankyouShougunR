select * from T_MONTHLY_LOCK_ZAIKO
WHERE GYOUSHA_CD = /*gyoushaCd*/
AND GENBA_CD = /*genbaCd*/
AND ZAIKO_HINMEI_CD = /*zaikoHinmeiCd*/
AND DELETE_FLG = 0
ORDER BY YEAR desc,MONTH desc,SEQ desc