SELECT
 *
FROM
 M_SBNB_PATTERN
/*BEGIN*/
WHERE
/*IF !data.SYSTEM_ID.IsNull*/ SYSTEM_ID = /*data.SYSTEM_ID*//*END*/ 
/*IF !data.SEQ.IsNull*/ AND SEQ = /*data.SEQ*//*END*/
/*IF !data.LAST_SBN_KBN.IsNull*/ AND LAST_SBN_KBN = /*data.LAST_SBN_KBN*//*END*/ 
/*IF data.PATTERN_NAME != null*/ AND PATTERN_NAME = /*data.PATTERN_NAME*//*END*/
/*END*/
AND DELETE_FLG = 0
