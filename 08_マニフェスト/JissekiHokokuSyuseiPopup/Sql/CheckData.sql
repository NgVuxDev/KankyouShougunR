/*IF haikiKbn != 4*/
SELECT SYSTEM_ID, SEQ
  FROM T_MANIFEST_ENTRY
 WHERE SYSTEM_ID    = /*systemId*/'1'
   AND HAIKI_KBN_CD = /*haikiKbn*/'1'
   AND DELETE_FLG   = 0
/*END*/
/*IF haikiKbn == 4*/
SELECT KANRI_ID
  FROM DT_MF_TOC
 WHERE KANRI_ID  = /*kanriID*/'1'
/*END*/