SELECT 
	SHARYOU_CD,SHARYOU_NAME_RYAKU
FROM  
	M_SHARYOU
/*BEGIN*/
where 
/*IF !deletechuFlg*/ 
DELETE_FLG = 0 
/*END*/ 
/*IF data.JIGYOUSHA_CD != null && data.JIGYOUSHA_CD != ''*/
AND GYOUSHA_CD = /*data.JIGYOUSHA_CD*//*END*/ 
/*IF !deletechuFlg*/ 
group by SHARYOU_CD, SHARYOU_NAME_RYAKU
/*END*/ 		
/*END*/