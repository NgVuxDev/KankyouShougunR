SELECT UNIT_NAME_RYAKU,
       UNIT_NAME
  FROM M_UNIT
 WHERE DELETE_FLG = 'false'
   AND UNIT_CD = /*data.UNIT_CD*/ 
   /*IF data.KAMI_USE_KBN != null && data.KAMI_USE_KBN != ""*/ AND KAMI_USE_KBN = /*data.KAMI_USE_KBN*//*END*/