SELECT 
    GEN.*
FROM 
    dbo.M_GENBA GEN
/*BEGIN*/WHERE
 /*IF data.GYOUSHA_CD != null*/
 GEN.GYOUSHA_CD = /*data.GYOUSHA_CD*/'000001'
 /*END*/
  /*IF data.GENBA_CD != null*/
 AND GEN.GENBA_CD = /*data.GENBA_CD*/'000001'
 /*END*/
/*END*/

ORDER BY GEN.GENBA_CD