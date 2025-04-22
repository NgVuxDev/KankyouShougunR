SELECT 
	TORIHIKISAKI_NAME_RYAKU
FROM  
	M_TORIHIKISAKI
/*BEGIN*/
where 
/*IF !deletechuFlg*/ 
	DELETE_FLG = 0
/*END*/
/*IF data.Torihikisaki_cd != null && data.Torihikisaki_cd != ''*/
AND	TORIHIKISAKI_CD = /*data.Torihikisaki_cd*//*END*/ 	
/*IF data.Denpyou_Date != null && data.Denpyou_Date != ''*/
AND	isnull(TEKIYOU_BEGIN,'1900-01-01 00:00:00.000') <= /*data.Denpyou_Date*/
AND	isnull(TEKIYOU_END,'2099-01-01 00:00:00.000') >= /*data.Denpyou_Date*//*END*/ 
/*END*/