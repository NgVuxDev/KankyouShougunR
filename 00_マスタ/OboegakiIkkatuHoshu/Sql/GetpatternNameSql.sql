SELECT
*
FROM
M_SBNB_PATTERN
 /*BEGIN*/WHERE
  /*IF data.LAST_SBN_KBN != null*/
 LAST_SBN_KBN= /*data.LAST_SBN_KBN*/
 /*END*/ 
 /*IF data.PATTERN_NAME != null*/
 AND PATTERN_NAME = /*data.PATTERN_NAME*/
 /*END*/
  /*IF data.GYOUSHA_CD != null*/
 AND GYOUSHA_CD = /*data.GYOUSHA_CD*/
 /*END*/
  /*IF data.GENBA_CD != null*/
 AND GENBA_CD = /*data.GENBA_CD*/
 /*END*/
/*END*/
