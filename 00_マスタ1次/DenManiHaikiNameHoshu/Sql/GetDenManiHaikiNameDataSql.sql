SELECT 
    HAI.*
FROM 
    dbo.M_DENSHI_HAIKI_NAME HAI
/*BEGIN*/WHERE
 /*IF data.EDI_MEMBER_ID != null*/
 HAI.EDI_MEMBER_ID LIKE '%' + /*data.EDI_MEMBER_ID*/'0000001' + '%'
 /*END*/
 /*IF data.HAIKI_NAME_CD != null*/AND HAI.HAIKI_NAME_CD LIKE '%' +  /*data.HAIKI_NAME_CD*/ + '%'/*END*/
 /*IF data.CREATE_USER != null*/AND HAI.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND HAI.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
 /*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, HAI.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
 /*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, HAI.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
/*END*/
ORDER BY HAI.HAIKI_NAME_CD
