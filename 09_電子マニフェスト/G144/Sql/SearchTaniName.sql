SELECT 
	UNIT_NAME_RYAKU AS NAME
FROM  
	M_UNIT
/*BEGIN*/
where 
/*IF !deletechuFlg*/ 
DENSHI_USE_KBN = 1
AND DELETE_FLG = 0 
/*END*/
/*IF data.Search_CD != null && data.Search_CD != ''*/
AND	UNIT_CD = /*data.Search_CD*//*END*/ 	
/*END*/