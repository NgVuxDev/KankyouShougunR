SELECT 
    BSN.*
FROM 
    dbo.M_BANK_SHITEN BSN
/*BEGIN*/WHERE
 /*IF data.BANK_CD != null*/BSN.BANK_CD LIKE '%' +  /*data.BANK_CD*/ + '%'/*END*/
 /*IF data.BANK_SHITEN_CD != null*/
  AND BSN.BANK_SHITEN_CD LIKE '%' + /*data.BANK_SHITEN_CD*/'001' + '%'
 /*END*/
 /*IF data.BANK_SHITEN_NAME != null && data.BANK_SHITEN_NAME != ''*/AND BSN.BANK_SHITEN_NAME LIKE '%' +  /*data.BANK_SHITEN_NAME*/ + '%'/*END*/
 /*IF data.BANK_SHIETN_NAME_RYAKU != null && data.BANK_SHIETN_NAME_RYAKU != ''*/AND BSN.BANK_SHIETN_NAME_RYAKU LIKE '%' +  /*data.BANK_SHIETN_NAME_RYAKU*/ + '%'/*END*/
 /*IF data.BANK_SHITEN_FURIGANA != null && data.BANK_SHITEN_FURIGANA != ''*/AND BSN.BANK_SHITEN_FURIGANA LIKE '%' +  /*data.BANK_SHITEN_FURIGANA*/ + '%'/*END*/
 /*IF data.KOUZA_SHURUI != null && data.KOUZA_SHURUI != ''*/AND KOUZA_SHURUI LIKE '%' +  /*data.KOUZA_SHURUI*/ + '%'/*END*/
 /*IF data.KOUZA_NO != null && data.KOUZA_NO != ''*/AND KOUZA_NO LIKE '%' +  /*data.KOUZA_NO*/ + '%'/*END*/
 /*IF data.KOUZA_NAME != null && data.KOUZA_NAME != ''*/AND KOUZA_NAME LIKE '%' +  /*data.KOUZA_NAME*/ + '%'/*END*/
 /*IF data.RENKEI_CD != null*/AND RENKEI_CD LIKE '%' +  /*data.RENKEI_CD*/ + '%'/*END*/
 /*IF data.BANK_SHITEN_BIKOU != null && data.BANK_SHITEN_BIKOU != ''*/AND BSN.BANK_SHITEN_BIKOU LIKE '%' +  /*data.BANK_SHITEN_BIKOU*/ + '%'/*END*/
 /*IF data.CREATE_USER != null && data.CREATE_USER != ''*/AND BSN.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, BSN.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null && data.UPDATE_USER != ''*/AND BSN.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, BSN.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
 /*IF !deletechuFlg*/AND BSN.DELETE_FLG = /*deletechuFlg*/0/*END*/
/*END*/
ORDER BY BSN.BANK_CD,BANK_SHITEN_CD,KOUZA_SHURUI_CD,KOUZA_NO
