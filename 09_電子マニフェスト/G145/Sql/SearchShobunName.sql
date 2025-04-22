SELECT 
	TANTOUSHA_NAME AS NAME
FROM  
	M_DENSHI_TANTOUSHA
/*BEGIN*/
where 
/*IF !deletechuFlg*/ 
	TANTOUSHA_KBN = 5
/*END*/
/*IF data.Search_CD != null && data.Search_CD != ''*/
AND	TANTOUSHA_CD = /*data.Search_CD*//*END*/ 
/*IF data.JIGYOUSHA_CD != null && data.JIGYOUSHA_CD != ''*/
AND EDI_MEMBER_ID = /*data.JIGYOUSHA_CD*//*END*/ 			
/*END*/