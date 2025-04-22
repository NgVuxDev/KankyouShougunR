SELECT 
    DNS.*
FROM 
    dbo.M_DENSHU_KBN DNS
/*BEGIN*/WHERE
 /*IF !data.DENSHU_KBN_CD.IsNull */CAST(DNS.DENSHU_KBN_CD AS varchar(2)) LIKE '%' + CAST(/*data.DENSHU_KBN_CD*/0 AS varchar(2)) + '%'/*END*/
 /*IF data.DENSHU_KBN_NAME != null && data.DENSHU_KBN_NAME != ''*/AND DNS.DENSHU_KBN_NAME LIKE '%' +  /*data.DENSHU_KBN_NAME*/ + '%'/*END*/
 /*IF data.DENSHU_KBN_NAME_RYAKU != null && data.DENSHU_KBN_NAME_RYAKU != ''*/AND DNS.DENSHU_KBN_NAME_RYAKU LIKE '%' +  /*data.DENSHU_KBN_NAME_RYAKU*/ + '%'/*END*/
 /*IF data.DENSHU_KBN_BIKOU != null && data.DENSHU_KBN_BIKOU != ''*/AND DNS.DENSHU_KBN_BIKOU LIKE '%' +  /*data.DENSHU_KBN_BIKOU*/ + '%'/*END*/
 /*IF data.CREATE_USER != null && data.CREATE_USER != ''*/AND DNS.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, DNS.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null && data.UPDATE_USER != ''*/AND DNS.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, DNS.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
/*END*/
ORDER BY DNS.DENSHU_KBN_CD
