SELECT
      /*IF IsFirstRelation*/
      FIRST_SYSTEM_ID, FIRST_HAIKI_KBN_CD
      --ELSE
      NEXT_SYSTEM_ID, NEXT_HAIKI_KBN_CD
      /*END*/
 FROM T_MANIFEST_RELATION
WHERE 
   CREATE_USER = 'MANIRELATION'
   /*IF createFrom != null && createFrom != ''*/ AND CONVERT(VARCHAR, CREATE_DATE, 111) >= /*createFrom*/'2014/01/01' /*END*/
   /*IF createTo != null && createTo != ''*/ AND CONVERT(VARCHAR, CREATE_DATE, 111) <= /*createTo*/'2014/01/01'/*END*/
/*IF IsFirstRelation*/
   GROUP BY FIRST_SYSTEM_ID, FIRST_HAIKI_KBN_CD
--ELSE
   GROUP BY NEXT_SYSTEM_ID, NEXT_HAIKI_KBN_CD
/*END*/