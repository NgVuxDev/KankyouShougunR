SELECT 
	JIGYOUSHA_NAME
FROM  
	M_DENSHI_JIGYOUSHA
/*BEGIN*/
where 
/*IF !deletechuFlg*/ 
	UPN_KBN = 1
AND EDI_PASSWORD <> ''
/*END*/
/*IF data.JIGYOUSHA_CD != null && data.JIGYOUSHA_CD != ''*/
AND	EDI_MEMBER_ID = /*data.JIGYOUSHA_CD*//*END*/ 		
/*END*/