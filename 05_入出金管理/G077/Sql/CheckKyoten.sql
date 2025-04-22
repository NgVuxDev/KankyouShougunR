SELECT 
	KYOTEN_NAME_RYAKU
FROM  
	M_KYOTEN
/*BEGIN*/
where 
/*IF !deletechuFlg*/ 
	DELETE_FLG = 0
/*END*/
/*IF data.Kyoten_cd != null && data.Kyoten_cd != ''*/
AND	KYOTEN_CD = /*data.Kyoten_cd*//*END*/ 	
/*IF data.Denpyou_Date != null && data.Denpyou_Date != ''*/
AND	isnull(TEKIYOU_BEGIN,'1900-01-01 00:00:00.000') <= /*data.Denpyou_Date*/
AND	isnull(TEKIYOU_END,'2099-01-01 00:00:00.000') >= /*data.Denpyou_Date*//*END*/ 		
/*END*/