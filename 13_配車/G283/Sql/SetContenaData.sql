UPDATE M_CONTENA
   SET 
 /*IF data.GENBA_CD != null*/ GENBA_CD = /*data.GENBA_CD*/ , /*END*/
 /*IF data.KYOTEN_CD != null*/ KYOTEN_CD = /*data.KYOTEN_CD*/ , /*END*/
 /*IF data.SECCHI_DATE != null*/ SECCHI_DATE = /*data.SECCHI_DATE*/ , /*END*/
 /*IF data.JOUKYOU_KBN != null*/ JOUKYOU_KBN = /*data.JOUKYOU_KBN*/ , /*END*/
 /*IF data.UPDATE_USER != null*/ UPDATE_USER = /*data.UPDATE_USER*/ , /*END*/
 /*IF data.UPDATE_DATE != null*/ UPDATE_DATE = /*data.UPDATE_DATE*/ , /*END*/
 /*IF data.UPDATE_PC != null*/ UPDATE_PC = /*data.UPDATE_PC*//*END*/
 WHERE DELETE_FLG = 0
 /*IF data.CONTENA_SHURUI_CD != null*/ AND CONTENA_SHURUI_CD = /*data.CONTENA_SHURUI_CD*//*END*/
 /*IF data.CONTENA_CD != null*/ AND CONTENA_CD = /*data.CONTENA_CD*//*END*/