SELECT 
	SHARYOU_NAME_RYAKU AS NAME
FROM  
	M_SHARYOU
/*BEGIN*/
where 
1=1
/*IF data.Search_CD != null && data.Search_CD != ''*/
AND SHARYOU_CD = /*data.Search_CD*//*END*/ 	
/*IF data.JIGYOUSHA_CD != null && data.JIGYOUSHA_CD != ''*/
AND GYOUSHA_CD = /*data.JIGYOUSHA_CD*//*END*/ 		
/*END*/