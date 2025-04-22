SELECT DISTINCT 
       HAIKI_KBN_CD
      ,CASE HAIKI_KBN_CD WHEN 1 THEN '産廃'
                         WHEN 2 THEN '建廃'
                         WHEN 3 THEN '積替'
                         WHEN 4 THEN '電子'
                         ELSE '' END AS SHURUI
      ,MANI_SYSTEM_ID
      ,MANI_SEQ
      ,MANIFEST_ID
      ,DEN_MANI_KANRI_ID
FROM T_JISSEKI_HOUKOKU_MANIFEST_DETAIL 
WHERE SYSTEM_ID = /*systemId*/'1' 
  AND SEQ = /*seq*/'1' 
  AND DETAIL_SYSTEM_ID = /*detailSystemId*/'1'