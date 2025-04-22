SELECT 
    HAI.*
FROM 
    dbo.M_HAIKI_KBN HAI
/*BEGIN*/WHERE
 /*IF !data.HAIKI_KBN_CD.IsNull*/ CAST (HAI.HAIKI_KBN_CD AS VARCHAR(2)) LIKE '%' + CAST(/*data.HAIKI_KBN_CD*/0 AS VARCHAR(2)) + '%'/*END*/
 /*IF data.HAIKI_KBN_NAME != null && data.HAIKI_KBN_NAME != ''*/AND HAI.HAIKI_KBN_NAME LIKE '%' +  /*data.HAIKI_KBN_NAME*/ + '%'/*END*/
 /*IF data.HAIKI_KBN_NAME_RYAKU != null && data.HAIKI_KBN_NAME_RYAKU != ''*/AND HAI.HAIKI_KBN_NAME_RYAKU LIKE '%' +  /*data.HAIKI_KBN_NAME_RYAKU*/ + '%'/*END*/
 /*IF data.HAIKI_KBN_BIKOU != null && data.HAIKI_KBN_BIKOU != ''*/AND HAI.HAIKI_KBN_BIKOU LIKE '%' +  /*data.HAIKI_KBN_BIKOU*/ + '%'/*END*/
 /*IF data.UPDATE_USER != null*/AND HAI.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
	/*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, HAI.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
	/*IF data.CREATE_USER != null*/AND HAI.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
	/*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, HAI.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
/*END*/
ORDER BY HAI.HAIKI_KBN_CD