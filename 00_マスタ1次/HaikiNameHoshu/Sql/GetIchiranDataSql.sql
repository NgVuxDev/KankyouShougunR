SELECT 
    HAIKI.*
FROM 
    dbo.M_HAIKI_NAME HAIKI
/*BEGIN*/WHERE
 /*IF data.HAIKI_NAME_CD != null*/
 HAIKI.HAIKI_NAME_CD LIKE '%' + /*data.HAIKI_NAME_CD*/'000001' + '%'
 /*END*/
 /*IF data.HAIKI_NAME != null*/AND HAIKI.HAIKI_NAME LIKE '%' +  /*data.HAIKI_NAME*/ + '%'/*END*/
 /*IF data.HAIKI_NAME_RYAKU != null*/AND HAIKI.HAIKI_NAME_RYAKU LIKE '%' +  /*data.HAIKI_NAME_RYAKU*/ + '%'/*END*/
 /*IF data.HAIKI_NAME_FURIGANA != null*/AND HAIKI.HAIKI_NAME_FURIGANA LIKE '%' +  /*data.HAIKI_NAME_FURIGANA*/ + '%'/*END*/
 /*IF data.HAIKI_NAME_BIKOU != null*/AND HAIKI.HAIKI_NAME_BIKOU LIKE '%' +  /*data.HAIKI_NAME_BIKOU*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND HAIKI.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, HAIKI.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND HAIKI.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, HAIKI.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
 /*IF !deletechuFlg*/AND HAIKI.DELETE_FLG = /*deletechuFlg*/0/*END*/
/*END*/
ORDER BY HAIKI.HAIKI_NAME_CD
