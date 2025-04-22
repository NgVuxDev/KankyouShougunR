SELECT 
	IsNull(Max(KESHIKOMI_SEQ), 0) as KESHIKOMI_SEQ
FROM  
	T_NYUUKIN_KESHIKOMI
/*BEGIN*/
where 
/*IF !deletechuFlg*/ DELETE_FLG = 0 /*END*/
/*IF data.System_Id != null && data.System_Id != ''*/
AND	SYSTEM_ID = /*data.System_Id*//*END*/ 	
/*END*/