SELECT 
    SHA.*
FROM 
    dbo.M_SHASHU SHA
/*BEGIN*/WHERE
 /*IF data.SHASHU_CD != null*/ SHA.SHASHU_CD LIKE '%' + /*data.SHASHU_CD*/ + '%'/*END*/
 /*IF data.SHASHU_NAME != null*/AND SHA.SHASHU_NAME LIKE '%' +  /*data.SHASHU_NAME*/ + '%'/*END*/
 /*IF data.SHASHU_NAME_RYAKU != null*/AND SHA.SHASHU_NAME_RYAKU LIKE '%' +  /*data.SHASHU_NAME_RYAKU*/ + '%'/*END*/
 /*IF data.SHASHU_FURIGANA != null*/AND SHA.SHASHU_FURIGANA LIKE '%' +  /*data.SHASHU_FURIGANA*/ + '%'/*END*/
 /*IF data.SHASHU_BIKOU != null*/AND SHA.SHASHU_BIKOU LIKE '%' +  /*data.SHASHU_BIKOU*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND SHA.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, SHA.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND SHA.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, SHA.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
 /*IF !deletechuFlg*/AND SHA.DELETE_FLG = /*deletechuFlg*/0/*END*/
/*END*/
ORDER BY SHA.SHASHU_CD
