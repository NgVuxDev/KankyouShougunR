SELECT 
    UNIT.*
FROM 
    dbo.M_UNIT UNIT
/*BEGIN*/WHERE
 /*IF !data.UNIT_CD.IsNull */UNIT.UNIT_CD =/*data.UNIT_CD*/0/*END*/
 /*IF data.UNIT_NAME != null*/AND UNIT.UNIT_NAME LIKE '%' +  /*data.UNIT_NAME*/ + '%'/*END*/
 /*IF data.UNIT_NAME_RYAKU != null*/AND UNIT.UNIT_NAME_RYAKU LIKE '%' +  /*data.UNIT_NAME_RYAKU*/ + '%'/*END*/
 /*IF !data.KAMI_USE_KBN.IsNull*/AND UNIT.KAMI_USE_KBN = /*data.KAMI_USE_KBN.Value*/0/*END*/
 /*IF !data.DENSHI_USE_KBN.IsNull*/AND UNIT.DENSHI_USE_KBN = /*data.DENSHI_USE_KBN.Value*/0/*END*/
 /*IF data.CREATE_USER != null*/AND UNIT.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, UNIT.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND UNIT.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, UNIT.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
 /*IF !deletechuFlg*/AND UNIT.DELETE_FLG =  /*deletechuFlg*/0/*END*/
/*END*/
ORDER BY UNIT.UNIT_CD
