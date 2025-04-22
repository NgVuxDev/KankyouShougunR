SELECT
SYSTEM_ID,		
SEQ		,
DENPYOU_NUMBER	,	
MEMO_UPDATE_DATE,		
MEMO		,
HST_GYOUSHA_CD,		
HST_GYOUSHA_NAME,		
HST_GENBA_CD	,	
HST_GENBA_NAME	,	
UNPAN_GYOUSHA_CD,		
UNPAN_GYOUSHA_NAME,		
SHOBUN_PATTERN_SYSTEM_ID	,	
SHOBUN_PATTERN_SEQ		,
SHOBUN_PATTERN_NAME		,
LAST_SHOBUN_PATTERN_SYSTEM_ID	,	
LAST_SHOBUN_PATTERN_SEQ		,
LAST_SHOBUN_PATTERN_NAME,		
KEIYAKU_BEGIN	,	
KEIYAKU_END		,
UPDATE_SHUBETSU		,
KEIYAKUSHO_SHURUI		,
SHOBUN_UPDATE_KBN		,
UPD_SHOBUN_PATTERN_SYSTEM_ID	,	
UPD_SHOBUN_PATTERN_SEQ		,
UPD_SHOBUN_PATTERN_NAME		,
LAST_SHOBUN_UPDATE_KBN		,
UPD_LAST_SHOBUN_PATTERN_SYSTEM_ID		,
UPD_LAST_SHOBUN_PATTERN_SEQ		,
UPD_LAST_SHOBUN_PATTERN_NAME	,	
CREATE_USER		,
CREATE_DATE	,	
CREATE_PC	,	
UPDATE_USER	,	
UPDATE_DATE	,	
UPDATE_PC	,	
DELETE_FLG		
FROM
T_ITAKU_MEMO_IKKATSU_ENTRY 
/*BEGIN*/WHERE
/*IF !deletechuFlg*/ DELETE_FLG = 0/*END*/
/*IF data.Denpyou_Number != null && data.Denpyou_Number != ''*/
  AND DENPYOU_NUMBER = /*data.Denpyou_Number*//*END*/
/*END*/
/*IF data.Hst_Gyousha_Cd != null && data.Hst_Gyousha_Cd != ''*/
  AND HST_GYOUSHA_CD = /*data.Hst_Gyousha_Cd*//*END*/
/*END*/
/*IF data.Hst_Genba_Cd != null && data.Hst_Genba_Cd != ''*/
  AND HST_GENBA_CD = /*data.Hst_Genba_Cd*//*END*/
/*END*/
/*IF data.Unpan_Gyousha_Cd != null && data.Unpan_Gyousha_Cd != ''*/
  AND UNPAN_GYOUSHA_CD = /*data.Unpan_Gyousha_Cd*//*END*/
/*END*/
/*IF data.Shobun_Pattern_Name != null && data.Shobun_Pattern_Name != ''*/
  AND SHOBUN_PATTERN_NAME = /*data.Shobun_Pattern_Name*//*END*/
/*END*/
/*IF data.Last_Shobun_Pattern_Name != null && data.Last_Shobun_Pattern_Name != ''*/
  AND LAST_SHOBUN_PATTERN_NAME = /*data.Last_Shobun_Pattern_Name*//*END*/
/*END*/
/*IF data.Keiyaku_Begin != null && data.Keiyaku_Begin != ''*/
  AND KEIYAKU_BEGIN = CONVERT(DATETIME, /*data.Keiyaku_Begin*/null, 120)
/*END*/
/*IF data.Keiyaku_End != null && data.Keiyaku_End != ''*/
  AND KEIYAKU_END =  CONVERT(DATETIME, /*data.Keiyaku_End*/null, 120)
/*END*/
/*IF data.Update_Shubetsu != null && data.Update_Shubetsu != ''*/
  AND UPDATE_SHUBETSU = /*data.Update_Shubetsu*//*END*/
/*END*/
/*IF data.Keiyakusho_Shurui != null && data.Keiyakusho_Shurui != ''*/
  AND KEIYAKUSHO_SHURUI = /*data.Keiyakusho_Shurui*//*END*/
/*END*/

ORDER BY DENPYOU_NUMBER