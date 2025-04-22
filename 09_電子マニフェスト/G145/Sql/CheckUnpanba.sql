SELECT 
	JIGYOUJOU_NAME
FROM  
	M_DENSHI_JIGYOUJOU
/*BEGIN*/
where 
/*IF !deletechuFlg*/ 
	(JIGYOUJOU_KBN = 2 or JIGYOUJOU_KBN = 3)
/*END*/
/*IF data.JIGYOUSHA_CD != null && data.JIGYOUSHA_CD != ''*/
AND	EDI_MEMBER_ID = /*data.JIGYOUSHA_CD*//*END*/ 		
/*IF data.JIGYOUBA_CD != null && data.JIGYOUBA_CD != ''*/
AND	JIGYOUJOU_CD = /*data.JIGYOUBA_CD*//*END*/ 	
/*END*/
