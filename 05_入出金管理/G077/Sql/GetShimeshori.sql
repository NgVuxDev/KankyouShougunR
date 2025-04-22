SELECT 
	SHIME_JIKKOU_NO
FROM  
	T_SHIME_SHORI_CHUU
/*BEGIN*/
where 
/*IF !deletechuFlg*/ SHORI_KBN = 1 /*END*/
/*IF data.Torihikisaki_cd != null && data.Torihikisaki_cd != ''*/
AND	TORIHIKISAKI_CD = /*data.Torihikisaki_cd*//*END*/ 
/*IF data.Kyoten_cd != null && data.Kyoten_cd != ''*/
AND	KYOTEN_CD = case when KYOTEN_CD=99 then 99 else /*data.Kyoten_cd*/ end /*END*/ 
/*IF data.Denpyou_Date != null && data.Denpyou_Date != ''*/
AND	isnull(HIDUKE_HANI_BEGIN,'1900-01-01 00:00:00.000') <= /*data.Denpyou_Date*/
AND	isnull(HIDUKE_HANI_END,'2099-01-01 00:00:00.000') >= /*data.Denpyou_Date*//*END*/ 
/*END*/