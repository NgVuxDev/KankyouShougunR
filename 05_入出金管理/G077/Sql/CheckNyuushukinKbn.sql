SELECT 
	NYUUSHUKKIN_KBN_NAME_RYAKU
FROM  
	M_NYUUSHUKKIN_KBN
/*BEGIN*/
where 
/*IF !deletechuFlg*/ 
	DELETE_FLG = 0
/*END*/
/*IF data.NyuushukinKbn != null && data.NyuushukinKbn != ''*/
AND	NYUUSHUKKIN_KBN_CD = /*data.NyuushukinKbn*//*END*/ 
/*IF data.Denpyou_Date != null && data.Denpyou_Date != ''*/
AND	isnull(TEKIYOU_BEGIN,'1900-01-01 00:00:00.000') <= /*data.Denpyou_Date*/
AND	isnull(TEKIYOU_END,'2099-01-01 00:00:00.000') >= /*data.Denpyou_Date*//*END*/ 
/*END*/