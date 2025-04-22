SELECT
SHORI_KBN,
CHECK_KBN,
DENPYOU_SHURUI_CD,
SYSTEM_ID,
SEQ,
DETAIL_SYSTEM_ID,
GYO_NUMBER,
ERROR_NAIYOU,
RIYUU
FROM
T_SHIME_SHORI_ERROR
/*BEGIN*/WHERE
/*IF data.SHORI_KBN != null &&  data.SHORI_KBN != ""*/
 SHORI_KBN = /*data.SHORI_KBN*//*END*/
 /*IF data.CHECK_KBN != null &&  data.CHECK_KBN != ""*/
 AND CHECK_KBN = /*data.CHECK_KBN*//*END*/
 /*IF data.SYSTEM_ID != null &&  data.SYSTEM_ID != ""*/
 AND SYSTEM_ID = /*data.SYSTEM_ID*//*END*/
 /*IF data.SEQ != null &&  data.SEQ != ""*/
 AND SEQ = /*data.SEQ*//*END*/
 /*IF data.DETAIL_SYSTEM_ID != null &&  data.DETAIL_SYSTEM_ID != ""*/
 AND DETAIL_SYSTEM_ID = /*data.DETAIL_SYSTEM_ID*//*END*/
 /*IF data.DENPYOU_SHURUI_CD != null &&  data.DENPYOU_SHURUI_CD != ""*/
 AND DENPYOU_SHURUI_CD = /*data.DENPYOU_SHURUI_CD*//*END*/
/*END*/