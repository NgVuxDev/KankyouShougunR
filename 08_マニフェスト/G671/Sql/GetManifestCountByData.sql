SELECT *
 FROM T_MANIFEST_ENTRY
WHERE 
   CREATE_USER = 'MANIINPORT'
   AND SEQ = 1
   /*IF createFrom != null && createFrom != ''*/ AND CONVERT(VARCHAR, CREATE_DATE, 111) >= /*createFrom*/'2014/01/01' /*END*/
   /*IF createTo != null && createTo != ''*/ AND CONVERT(VARCHAR, CREATE_DATE, 111) <= /*createTo*/'2014/01/01'/*END*/