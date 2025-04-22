SELECT 
    CASE KIHON_KEIRYOU WHEN 1 THEN 'éÛì¸' WHEN 2 THEN 'èoâ◊' ELSE '' END AS DENSHU_KBN_NAME,
	DENPYOU_DATE,
	KEIRYOU_NUMBER AS DENPYOU_NUMBER,
    SHASHU_NAME,
    SHARYOU_NAME,
    ISNULL(UNPAN_GYOUSHA_NAME,'') + char(10) + ISNULL(UNTENSHA_NAME,'') + char(10) + ISNULL(TAIRYUU_BIKOU,'') AS BIKOU,
    SYSTEM_ID
FROM T_KEIRYOU_ENTRY
WHERE DELETE_FLG = 0 
AND TAIRYUU_KBN = 1
AND KYOTEN_CD = /*data.kyotenCd*/
/*IF data.upnGyoushaCd != null && data.upnGyoushaCd != ''*/
AND UNPAN_GYOUSHA_CD = /*data.upnGyoushaCd*/
/*END*/
/*IF data.sharyouCd != null && data.sharyouCd != ''*/
AND SHARYOU_CD = /*data.sharyouCd*/
/*END*/