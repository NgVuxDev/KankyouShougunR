SELECT 
	SEIKYUU_NUMBER
FROM  
	T_NYUUKIN_KESHIKOMI
/*BEGIN*/
where 
/*IF !deletechuFlg*/ DELETE_FLG = 0 /*END*/
/*IF data.System_Id != null && data.System_Id != ''*/
AND	SYSTEM_ID = /*data.System_Id*//*END*/ 	
/*IF data.Kesikomi_Seq != null && data.Kesikomi_Seq != ''*/
AND	KESHIKOMI_SEQ = /*data.Kesikomi_Seq*//*END*/ 
/*END*/